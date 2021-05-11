using System;

namespace AIAS_Lab3
{
    public class LinearEquationsSystemsSolver
    {
        public static double[] Solve(Matrix mainMatrix, double[] freeVector)
        {
            if (freeVector.Length != mainMatrix.Size)
                throw new Exception("Main matrix and free vector size must be the same");
            if (mainMatrix.Determinant == 0)
                throw new Exception("Нет решений или решений бесконечно много");
            return mainMatrix.GetInverseMatrix() * freeVector;
        }
    }
}