using System;
using System.Collections.Generic;
using AIAS_Lab3;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AIAS_Lab3_Tests
{
    [TestFixture]
    public class MatrixTests
    {
        public static IEnumerable<TestCaseData> CtorIncorrectCombinations()
        {
            var test = new TestCaseData(new double[,] {{1, 2}, {1, 2}, {1, 2}});
            test.TestName = "DifferentLinearSizes";
            yield return test;
        }
        
        public static IEnumerable<TestCaseData> InverseMatrixIncorrectCombinations()
        {
            var test = new TestCaseData(new double[,] {{1, 1, 1}, {1, 1, 1}, {1,2,3}});
            test.TestName = "DeterminantIsZero";
            yield return test;
        }

        public static IEnumerable<TestCaseData> MultiplyMatrixVectorIncorrectCombinations()
        {
            var test = new TestCaseData(new double[,] {{1, 3}, {2, 0}}, new double[]{3,4,5});
            test.TestName = "DifferentMatrixVectorSize";
            yield return test;
        }
        
        public static IEnumerable<TestCaseData> DeterminantsTestsCombinations()
        {
            var test = new TestCaseData(new double[,] {{0, 0}, {0, 0}}, 0.0);
            test.TestName = "ZeroMatrix2x2";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 2}, {3, 4}}, -2.0);
            test.TestName = "Matrix2x2NotZeroDeterminant";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 2}, {1, 2}}, 0.0);
            test.TestName = "Matrix2x2ZeroDeterminant";
            yield return test;
            test = new TestCaseData(new double[,] {{0,0,0,0,0,0}, {0,0,0,0,0,0}, {0,0,0,0,0,0}, {0,0,0,0,0,0},{0,0,0,0,0,0},{0,0,0,0,0,0}}, 0.0);
            test.TestName = "ZeroMatrix4x4";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 2, 3, 4, 5, 6}, {2, 5, 3, 4, 1, 7}, {1,1,1,1,1,1}, {3,4,5,6,8,3},{0,5,6,2,1,9},{4,0,2,1,4,0}}, -69.0);
            test.TestName = "Matrix6x6NotZeroDeterminant";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 1, 1, 1, 1, 1}, {2, 5, 3, 4, 1, 7}, {1,1,1,1,1,1}, {3,4,5,6,8,3},{0,5,6,2,1,9},{4,0,2,1,4,0}}, 0.0);
            test.TestName = "Matrix6x6ZeroDeterminant";
            yield return test;
        }

        public static IEnumerable<TestCaseData> InverseTestsCombinations()
        {
            var test = new TestCaseData(new double[,] {{1, 3}, {2, 0}}, new Matrix( new double[,] {{0,0.5},{1.0/3, -1.0/6}}));
            test.TestName = "Matrix2x2";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 2, 3, 4, 5}, {2, 5, 3, 4, 1}, {1,1,1,1,1}, {3,4,5,6,8},{0,5,6,2,1}}, 
                1.0/37 * new Matrix(new double[,]{{4,-5,76,-11,-3}, {-63, 14, -124, 53, 1},{40,-13,94,-36,7}, {56,4,65,-43,-5},{-37,0,-74,37,0}}));
            test.TestName = "Matrix5x5";
            yield return test;
        }
        
        public static IEnumerable<TestCaseData> MultiplyMatrixVectorCombinations()
        {
            var test = new TestCaseData(new double[,] {{1, 3}, {2, 0}}, new double[]{3, 4}, new double[]{15,6});
            test.TestName = "Matrix2x2";
            yield return test;
            test = new TestCaseData(new double[,] {{0, 0}, {0, 0}}, new double[]{3, 4}, new double[]{0,0});
            test.TestName = "ZeroMatrix2x2";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 3}, {2, 0}}, new double[]{0, 0}, new double[]{0,0});
            test.TestName = "Matrix2x2ZeroVector";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 0}, {0, 1}}, new double[]{3, 4}, new double[]{3,4});
            test.TestName = "UnitMatrix2x2";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 0, 0, 0, 0, 0}, {0,1,0,0,0,0}, {0,0,1,0,0,0}, {0,0,0,1,0,0},{0,0,0,0,1,0}, {0,0,0,0,0,1}}, new double[]{1,2,3,4,5,6},new double[]{1,2,3,4,5,6});
            test.TestName = "UnitMatrix6x6";
            yield return test;
            test = new TestCaseData(new double[,] {{1, 2, 3, 4, 5, 6}, {2, 5, 3, 4, 1, 5}, {1,1,1,1,1, 1}, {3,4,5,6,8, 2},{0,5,6,2,1,3},{1,3,6,2,1,3}}, new double[]{1,2,3,4,5,6}, new double[]{91, 72, 21, 102, 59, 56});
            test.TestName = "Matrix6x6";
            yield return test;
        }



        
        [Test]
        [TestCaseSource("DeterminantsTestsCombinations")]
        public void Determinant(double[,] arrayForMatrix, double expected)
        {
            var determinant = new Matrix(arrayForMatrix).Determinant;
            Assert.AreEqual(expected, determinant);
        }

        [Test]
        [TestCaseSource("InverseTestsCombinations")]
        public void GetInverseMatrix(double[,] arrayForMatrix, Matrix expected)
        {
            var matrix = new Matrix(arrayForMatrix);
            Assert.AreEqual(matrix.GetInverseMatrix(), expected);
        }

        [Test]
        [TestCaseSource("MultiplyMatrixVectorCombinations")]
        public void MultiplyMatrixVector(double[,] arrayForMatrix, double[] vector, double[] expected)
        {
            var matrix = new Matrix(arrayForMatrix);
            Assert.AreEqual(expected, matrix*vector);
        }

        [Test]
        [TestCaseSource("MultiplyMatrixVectorIncorrectCombinations")]
        public void MultiplyMatrixVectorTrows(double[,] arrayForMatrix, double[] vector)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var a = new Matrix(arrayForMatrix) * vector;
            });
        }
        
        [Test]
        [TestCaseSource("CtorIncorrectCombinations")]
        public void CtorThrows(double[,] arrayForMatrix)
        {
            Assert.Throws<Exception>(() =>
            {
                var a = new Matrix(arrayForMatrix);
            });
        }
        
        [Test]
        [TestCaseSource("InverseMatrixIncorrectCombinations")]
        public void GetInverseMatrixThrows(double[,] arrayForMatrix)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var a = new Matrix(arrayForMatrix).GetInverseMatrix();
            });
        }
    }
}