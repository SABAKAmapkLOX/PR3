namespace Lib_6
{
    public class Calculater
    {
        public static void SumMatrix(out int sumMatrix,int[,] matrix)
        {
            sumMatrix = 0;
            for (int j = 0; j < matrix.GetLength(0); j += 2)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    sumMatrix += matrix[i, j];
                }
            }
        }
    }
}
