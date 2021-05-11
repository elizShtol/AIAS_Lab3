using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AIAS_Lab3
{
    public class Matrix
    {
        public readonly double[,] Items;
        public readonly int Size;
        private double? determinant; 


        public Matrix(double[,] array)
        {
            if (array.GetLength(0) != array.GetLength(1))
                throw new Exception("Array must have the same width and height more then 1");
            Size = array.GetLength(0);
            Items = array;
        }

        

        public double Determinant
        {
            get
            {
                determinant ??= GetDeterminant(this);
                return (double)determinant;
            }
        }

        public Matrix GetInverseMatrix()
        {
            if (Determinant == 0)
                throw new InvalidOperationException("Determinant is 0");
            return 1 / Determinant * GetAlgebraicComplementsMatrix().Transpose();
        }

        public static double[] operator *(Matrix m, double[] vector)
        {
            if (m.Size != vector.Length)
                throw new InvalidOperationException("Size of the matrix and length of the vector must be the same");
            var result = new double[vector.Length];
            for (int i = 0; i < m.Size; i++)
            {
                for (int j = 0; j < m.Size; j++)
                {
                    result[i] += m.Items[i, j] * vector[j];
                }
            }

            return result;
        }
        
        public static Matrix operator * (double a, Matrix m)
        {
            var result = new double[m.Size, m.Size];
            for (int i = 0; i < m.Size; i++)
            {
                for (int j = 0; j < m.Size; j++)
                {
                    result[i, j] = m.Items[i, j] * a;
                }
            }

            return new Matrix(result);
        }

        public Matrix GetAlgebraicComplementsMatrix()
        {
            var result = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    result[i, j] = Math.Pow(-1, i + j) * GetDeterminant(GetMinor(this, i, j));
                }
            }

            return new Matrix(result);
        }

        public Matrix Transpose()
        {
            var result = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    result[j, i] = Items[i, j];
                }
            }

            return new Matrix(result);
        }

        private static double GetDeterminant(Matrix matrix)
        {
            if (matrix.Size == 1)
                return matrix.Items[0, 0];
            if (matrix.Size == 2)
                return matrix.Items[0, 0] * matrix.Items[1, 1] - matrix.Items[0, 1] * matrix.Items[1, 0];
            var determinant = 0.0;
            var sign = 1;
            for (var i = 0; i < matrix.Size; i++)
            {
                var minor = GetMinor(matrix, 0, i);
                determinant += sign * matrix.Items[0, i] * minor.Determinant;
                sign = -sign;
            }
            return determinant;
        }

        private static Matrix GetMinor(Matrix matrix, int i, int j)
        {
            var array = new double[matrix.Size-1, matrix.Size-1];
            for (int k = 0; k < matrix.Size; k++)
            {
                if(k == i)
                    continue;
                for (int l = 0; l < matrix.Size; l++)
                {
                    if(l==j)
                        continue;
                    var x = k < i ? k : k - 1;
                    var y = l < j ? l : l - 1;
                    array[x, y] = matrix.Items[k, l];
                }
            }

            return new Matrix(array);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Matrix))
                return false;
            var matrix = obj as Matrix;
            if (matrix.Size != Size)
                return false;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Math.Abs(matrix.Items[i, j] - Items[i, j]) > 1e-10)
                        return false;
                }
            }

            return true;
        }
    }
}