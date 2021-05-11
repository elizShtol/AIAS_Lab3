using System;

namespace AIAS_Lab3
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Введите количество неизвестных в системе уравнений");
                var unknownsCount = int.Parse(Console.ReadLine());
                var mainMatrix = new double[unknownsCount, unknownsCount];
                var freeVector = new double[unknownsCount];
                Console.WriteLine("Последовательно вводите коэффициенты при неизвестных и свободные члены каждого из уравнений через пробел");
                try
                {
                    ReadLinearSystem(ref mainMatrix, ref freeVector, unknownsCount);
                    var result = LinearEquationsSystemsSolver.Solve(new Matrix(mainMatrix), freeVector);
                    Console.WriteLine("Вектор решения системы линейных уравнений: ");
                    foreach (var item in result)
                        Console.Write($"{item} ");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Для завершения работы программы нажмите y");
                if(Console.ReadKey() == new ConsoleKeyInfo('y', ConsoleKey.Y, false, false, false))
                    break;
            }
            
        }

        private static void ReadLinearSystem(ref double[,] mainMatrix, ref double[] freeVector, int unknownsCount)
        {
            for (int i = 0; i < unknownsCount; i++)
            {
                while (true)
                {
                    var items = Console.ReadLine().Trim().Split();
                    if (items.Length == unknownsCount + 1)
                    {
                        for (int j = 0; j < unknownsCount; j++)
                        {
                            mainMatrix[i, j] = double.Parse(items[j]);
                        }

                        freeVector[i] = double.Parse(items[unknownsCount]);
                        break;
                    }
                        
                    Console.WriteLine($"Введите {unknownsCount} коэффициентов при неизвестных и 1 своюодный член");
                }
            }
        }
    }
}