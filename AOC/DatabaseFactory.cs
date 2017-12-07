/// <summary>
/// Talk to the database
/// </summary>
namespace AutoTaskCostReport.DAL
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DatabaseFactory
    {
        private readonly SqlConnection _conn;
        //private readonly log4net.ILog _log;


        public DatabaseFactory(string connectionstring)
        {
           // _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _conn = new SqlConnection(connectionstring);
        }

        /// <summary>
        /// Abstract method to return query results from a sql database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public IEnumerable<DataRow> ProcessSqlQuery(string query, SqlParameter[] parms = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                   // cmd.CommandType = CommandType.StoredProcedure;
                    if (parms != null)
                        cmd.Parameters.AddRange(parms);
                    _conn.Open();
                    var adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    _conn.Close();
                    //_log.Debug("This query was executed Successfully :" + query);

                    return dt.AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                //_log.Error("Function: ProcessSqlQuery | File : DatabaseFactory.cs | " + ex.Message);
                _conn.Close();
                return null;
            }
        }
        public static void UpdateDeleteProcedure(string query, SqlConnection _conn )
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //if (parms != null)
                      //  cmd.Parameters.AddRange(parms);
                    _conn.Open();
                    cmd.ExecuteNonQuery();
                    //_log.Info("This storedProcedure was executed Successfully :" + storedProcedureName);
                    _conn.Close();
                }
            }
            catch (Exception ex)
            {
               // _log.Error("Funciton: UpdateDeleteProcedure | File : DatabaseFactory.cs | " + ex.Message);
                _conn.Close();
            }
        }
        /// <summary>
        /// handy utility mash sql return values to a class
        /// </summary>
        /// <param name="tClass"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public object SetValuesToObject(Type tClass, DataRow dr)
        {
            try
            {
                Object oClass = System.Activator.CreateInstance(tClass);
                MemberInfo[] methods = tClass.GetMethods();
                ArrayList aMethods = new ArrayList();
                object[] aoParam = new object[1];

                //Get simple SET methods
                foreach (MethodInfo method in methods)
                {
                    if (method.DeclaringType == tClass && method.Name.StartsWith("set_"))
                        aMethods.Add(method);
                }

                //Invoke each set method with mapped value
                for (int i = 0; i < aMethods.Count; i++)
                {
                    try
                    {
                        MethodInfo mInvoke = (MethodInfo)aMethods[i];
                        //Remove “set_” from method name
                        string sColumn = mInvoke.Name.Remove(0, 4);
                        //If row contains value for method…
                        if (dr.Table.Columns.Contains(sColumn))
                        {
                            //Get the parameter (always one for a set property)
                            ParameterInfo[] api = mInvoke.GetParameters();
                            ParameterInfo pi = api[0];

                            //Convert value to parameter type
                            aoParam[0] = Convert.ChangeType(dr[sColumn], pi.ParameterType);

                            //Invoke the method
                            mInvoke.Invoke(oClass, aoParam);
                        }
                    }
                    catch (Exception ex)
                    {
                        //_log.Error("Function: SetValuesToObject | File : DatabaseFactory.cs | " + ex.Message);
                        System.Diagnostics.Debug.Assert(false, "SetValuesToObject failed to set a value to an object");
                        
                    }
                }
                return oClass;
            }
            catch (Exception ex)
            {
               // _log.Error("Function: SetValuesToObject | File : DatabaseFactory.cs | " + ex.Message);
                System.Diagnostics.Debug.Assert(false, "SetValuesToObject failed to create an object");
                return null;
            }
        }

    }
}