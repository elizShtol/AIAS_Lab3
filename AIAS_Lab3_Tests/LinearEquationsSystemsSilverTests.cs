using System;
using System.Collections.Generic;
using System.Linq;
using AIAS_Lab3;
using FluentAssertions;
using NUnit.Framework;

namespace AIAS_Lab3_Tests
{
    [TestFixture]
    public class LinearEquationsSystemsSolverTests
    {
        public static IEnumerable<TestCaseData> LinearSolverCombinations()
        {
            var test = new TestCaseData(new Matrix(new double[,] {{2, 3}, {5, 3}}), new double[] {1, 7},
                new double[] {2, -1});
            test.TestName = "MainMatrix2x2";
            yield return test;
            test = new TestCaseData(new Matrix(new double[,] {{1,2, 3,4,5}, {2,1,3,0,2}, {4,5,6,1,8,},{1,3,3,5,6}, {7,6,3,2,0}}), new double[] {1, 5,2,7,1},
                new [] {-133.0/3, 33, 38, -1.0/3, -80.0/3});
            test.TestName = "MainMatrix5x5";
            yield return test;
            test = new TestCaseData(new Matrix(new double[,] {{2, 3}, {5, 3}}), new double[] {0, 0},
                new double[] {0, 0});
            test.TestName = "MainMatrix2x2HomogeneousSystem";
            yield return test;
            test = new TestCaseData(new Matrix(new double[,] {{1,2, 3,4,5}, {2,1,3,0,2}, {4,5,6,1,8,},{1,3,3,5,6}, {7,6,3,2,0}}), new double[] {0, 0,0,0,0},
                new double[] {0,0,0,0,0});
            test.TestName = "MainMatrix5x5HomogeneousSystem";
            yield return test;
        }
        
        public static IEnumerable<TestCaseData> LinearSolverThrowsCombinations()
        {
            var test = new TestCaseData(new Matrix(new double[,] {{2, 2}, {3, 3}}), new double[] {1, 7});
            test.TestName = "DeterminantIsZero";
            yield return test;
            test = new TestCaseData(new Matrix(new double[,] {{2, 3}, {5, 3}}), new double[] {1, 0, 0});
            test.TestName = "MatrixAndFreeVectorDifferentSize";
            yield return test;
        }
        
        [Test]
        [TestCaseSource("LinearSolverCombinations")]
        public void Solve(Matrix mainMatrix, double[] freeVector, double[] expected)
        {
            var actual = LinearEquationsSystemsSolver.Solve(mainMatrix, freeVector);
            for (int i = 0; i < actual.Length; i++)
            {
                (Math.Abs(actual[i] - expected[i]) < 1e-10).Should().BeTrue();
            }
        }
        
        [Test]
        [TestCaseSource("LinearSolverThrowsCombinations")]
        public void SolveThrows(Matrix mainMatrix, double[] freeVector)
        {
            Assert.Throws<Exception>(() => { LinearEquationsSystemsSolver.Solve(mainMatrix, freeVector); });
        }
    }
}