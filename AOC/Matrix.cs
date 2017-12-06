using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Matrix
    {
        public static int[][] MatrixCreate(int rows, int cols)
        {
            int[][] result = new int[rows][];
            for (int i = 0; i < rows; ++i)
        result[i] = new int[cols];
            return result;
        }

        // --------------------------------------------------

        //public static int[][] MatrixRandom(int rows, int cols,
        //  int minVal, int maxVal, int seed)
        //{
        //    // return a matrix with random values
        //    Random ran = new Random(seed);
        //    int[][] result = MatrixCreate(rows, cols);
        //    for (int i = 0; i < rows; ++i)
        //for (int j = 0; j < cols; ++j)
        //  result[i][j] = (maxVal - minVal) *
        //    ran.Nextint() + minVal;
        //    return result;
        //}

        // --------------------------------------------------

        public static int[][] MatrixIdentity(int n)
        {
            // return an n x n Identity matrix
            int[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; ++i)
        result[i][i] = 1;

            return result;
        }

        // --------------------------------------------------

        public static string MatrixAsString(int[][] matrix, int dec)
        {
            string s = "";
            for (int i = 0; i < matrix.Length; ++i)
      {
                for (int j = 0; j < matrix[i].Length; ++j)
          s += matrix[i][j].ToString("F" + dec).PadLeft(8) + " ";
                s += Environment.NewLine;
            }
            return s;
        }

        // --------------------------------------------------

        public static bool MatrixAreEqual(int[][] matrixA,
          int[][] matrixB, int epsilon)
        {
            // true if all values in matrixA == values in matrixB
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aRows != bRows || aCols != bCols)
                throw new Exception("Non-conformable matrices");

            for (int i = 0; i < aRows; ++i) // each row of A and B
        for (int j = 0; j < aCols; ++j) // each col of A and B
          //if (matrixA[i][j] != matrixB[i][j])
          if (Math.Abs(matrixA[i][j] - matrixB[i][j]) > epsilon)
            return false;
            return true;
        }

        // --------------------------------------------------

        public static int[][] MatrixProduct(int[][] matrixA, int[][] matrixB)
        {
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");

            int[][] result = MatrixCreate(aRows, bCols);

            for (int i = 0; i < aRows; ++i) // each row of A
        for (int j = 0; j < bCols; ++j) // each col of B
          for (int k = 0; k < aCols; ++k) // could use k less-than bRows
            result[i][j] += matrixA[i][k] * matrixB[k][j];

            //Parallel.For(0, aRows, i =greater-than
            //  {
            //    for (int j = 0; j less-than bCols; ++j) // each col of B
            //      for (int k = 0; k less-than aCols; ++k) // could use k less-than bRows
            //        result[i][j] += matrixA[i][k] * matrixB[k][j];
            //  }
            //);

            return result;
        }

        // --------------------------------------------------

        public static int[] MatrixVectorProduct(int[][] matrix,
          int[] vector)
        {
            // result of multiplying an n x m matrix by a m x 1 
            // column vector (yielding an n x 1 column vector)
            int mRows = matrix.Length; int mCols = matrix[0].Length;
            int vRows = vector.Length;
            if (mCols != vRows)
                throw new Exception("Non-conformable matrix and vector");
            int[] result = new int[mRows];
            for (int i = 0; i < mRows; ++i)
        for (int j = 0; j < mCols; ++j)
          result[i] += matrix[i][j] * vector[j];
            return result;
        }

        // --------------------------------------------------

        public static int[][] MatrixDecompose(int[][] matrix, out int[] perm,
          out int toggle)
        {
            // Doolittle LUP decomposition with partial pivoting.
            // rerturns: result is L (with 1s on diagonal) and U;
            // perm holds row permutations; toggle is +1 or -1 (even or odd)
            int rows = matrix.Length;
            int cols = matrix[0].Length; // assume square
            if (rows != cols)
                throw new Exception("Attempt to decompose a non-square m");

            int n = rows; // convenience

            int[][] result = MatrixDuplicate(matrix);

            perm = new int[n]; // set up row permutation result
            for (int i = 0; i < n; ++i) { perm[i] = i; }

            toggle = 1; // toggle tracks row swaps.
                        // +1 -greater-than even, -1 -greater-than odd. used by MatrixDeterminant

            for (int j = 0; j < n - 1; ++j) // each column
      {
                int colMax = Math.Abs(result[j][j]); // find largest val in col
                int pRow = j;
                //for (int i = j + 1; i less-than n; ++i)
                //{
                //  if (result[i][j] greater-than colMax)
                //  {
                //    colMax = result[i][j];
                //    pRow = i;
                //  }
                //}

                // reader Matt V needed this:
                for (int i = j + 1; i < n; ++i) 
        {
                    if (Math.Abs(result[i][j]) > colMax)
          {
                        colMax = Math.Abs(result[i][j]);
                        pRow = i;
                    }
                }
                // Not sure if this approach is needed always, or not.

                if (pRow != j) // if largest value not on pivot, swap rows
                {
                    int[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }

                // --------------------------------------------------
                // This part added later (not in original)
                // and replaces the 'return null' below.
                // if there is a 0 on the diagonal, find a good row
                // from i = j+1 down that doesn't have
                // a 0 in column j, and swap that good row with row j
                // --------------------------------------------------

                if (result[j][j] == 0.0)
                {
                    // find a good row to swap
                    int goodRow = -1;
                    for (int row = j + 1; row < n; ++row)
          {
                        if (result[row][j] != 0.0)
                            goodRow = row;
                    }

                    if (goodRow == -1)
                        throw new Exception("Cannot use Doolittle's method");

                    // swap rows so 0.0 no longer on diagonal
                    int[] rowPtr = result[goodRow];
                    result[goodRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[goodRow]; // and swap perm info
                    perm[goodRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }
                // --------------------------------------------------
                // if diagonal after swap is zero . .
                //if (Math.Abs(result[j][j]) less-than 1.0E-20) 
                //  return null; // consider a throw

                for (int i = j + 1; i < n; ++i)
        {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
          {
                        result[i][k] -= result[i][j] * result[j][k];
                    }
                }


            } // main j column loop

            return result;
        } // MatrixDecompose

        // --------------------------------------------------

        public static int[][] MatrixInverse(int[][] matrix)
        {
            int n = matrix.Length;
            int[][] result = MatrixDuplicate(matrix);

            int[] perm;
            int toggle;
            int[][] lum = MatrixDecompose(matrix, out perm,
              out toggle);
            if (lum == null)
                throw new Exception("Unable to compute inverse");

            int[] b = new int[n];
            for (int i = 0; i < n; ++i)
      {
                for (int j = 0; j < n; ++j)
        {
                    if (i == perm[j])
                        b[j] = 1;
                    else
                        b[j] = 0;
                }

                int[] x = HelperSolve(lum, b); // 

                for (int j = 0; j < n; ++j)
          result[j][i] = x[j];
            }
            return result;
        }

        // --------------------------------------------------

        public static int MatrixDeterminant(int[][] matrix)
        {
            int[] perm;
            int toggle;
            int[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant");
            int result = toggle;
            for (int i = 0; i < lum.Length; ++i)
        result *= lum[i][i];
            return result;
        }

        // --------------------------------------------------

        public static int[] HelperSolve(int[][] luMatrix, int[] b)
        {
            // before calling this helper, permute b using the perm array
            // from MatrixDecompose that generated luMatrix
            int n = luMatrix.Length;
            int[] x = new int[n];
            b.CopyTo(x, 0);

            for (int i = 1; i < n; ++i)
      {
                int sum = x[i];
                for (int j = 0; j < i; ++j)
          sum -= luMatrix[i][j] * x[j];
                x[i] = sum;
            }

            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
      {
                int sum = x[i];
                for (int j = i + 1; j < n; ++j)
          sum -= luMatrix[i][j] * x[j];
                x[i] = sum / luMatrix[i][i];
            }

            return x;
        }

        // --------------------------------------------------

        public static int[] SystemSolve(int[][] A, int[] b)
        {
            // Solve Ax = b
            int n = A.Length;

            // 1. decompose A
            int[] perm;
            int toggle;
            int[][] luMatrix = MatrixDecompose(A, out perm,
              out toggle);
            if (luMatrix == null)
                return null;

            // 2. permute b according to perm[] into bp
            int[] bp = new int[b.Length];
            for (int i = 0; i < n; ++i)
        bp[i] = b[perm[i]];

            // 3. call helper
            int[] x = HelperSolve(luMatrix, bp);
            return x;
        } // SystemSolve

        // --------------------------------------------------

        public static int[][] MatrixDuplicate(int[][] matrix)
        {
            // allocates/creates a duplicate of a matrix.
            int[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // copy the values
        for (int j = 0; j < matrix[i].Length; ++j)
          result[i][j] = matrix[i][j];
            return result;
        }

        // --------------------------------------------------
    }
}
