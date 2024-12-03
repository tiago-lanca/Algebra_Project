using System.Data;
using System.Runtime.CompilerServices;

namespace AlgebraProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*double[,] M = new double M[2,3];
             M[0,0] = 3;
             M[1,2] = 5;*/
            //Console.WriteLine(M.GetLength(0));
            //Console.WriteLine($"Determinante |M|: {Matrix_Determinant2x2(M)}");

            //double[,] M = Matrix_Read();           

            //MatrixPrint(M);

            //double[,] M = { { 1, 2, 3, 1}, {2, 3, 1, 2 }, { 3, 1, 2, 3 }, { 1, 2, 3, 1 } };
            double[,] matrix1 = { { 1, 2 }, { 2, 4 }, { 1, 0 } };
            double[,] matrix2 = { { 2, 1, 4 }, { 0, 1, 2 } };

            Console.WriteLine("");
            //Console.Write("Soma de Matrizes: \n");
            //MatrixPrint(Matrix_Sum(matrix1, matrix2));
            MatrixPrint(Matrix_DotProduct(matrix1, matrix2));
            Console.WriteLine("");


            //Console.WriteLine($"Determinante: {Calculate_Determinant(M)}\n\n");

            double[,] Matrix_Sum(double[,] matrix1, double[,] matrix2)
            {
                if (matrix1.GetLength(0) == matrix2.GetLength(0) && matrix1.GetLength(1) == matrix2.GetLength(1))
                {
                    double[,] matrix_sum = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

                    for (int row = 0; row < matrix1.GetLength(0); row++)
                    {
                        for (int col = 0; col < matrix1.GetLength(1); col++)
                        {
                            matrix_sum[row, col] = matrix1[row, col] + matrix2[row, col];
                        }
                    }
                    return matrix_sum;
                }

                else 
                    throw new ArgumentException("Tem de ter mesmo numero de linhas e colunas.");
            }

            double[,] Matrix_DotProduct(double[,] matrix1, double[,] matrix2)
            {
                double[,] dot_product;
                int dot_row = 0, dot_col;

                if (matrix1.GetLength(1) == matrix2.GetLength(0))
                {
                    dot_product = new double[matrix1.GetLength(0), matrix2.GetLength(1)];

                    for (int row = 0; row < dot_product.GetLength(0); row++)
                    {

                        for (int col = 0; col < dot_product.GetLength(1); col++)
                        {
                            for (int col_matrix1 = 0; col_matrix1 < matrix1.GetLength(1); col_matrix1++)
                            {
                                dot_product[row, col] += matrix1[row, col_matrix1] * matrix2[col_matrix1, col];
                            }
                        }
                    }

                    return dot_product;
                }
                else
                    throw new ArgumentException("O numero de linhas da matriz 1 nao é igual ao numero de colunas das matriz 2.");
                
            }

            double Matrix_Determinant1x1(double[,] a)
            {
                if (a.GetLength(0) != a.GetLength(1))
                    throw new ArgumentException("Tem de ter mesmo nr de linhas e colunas");

                return a[0, 0];
            }

            double Matrix_Determinant2x2(double[,] a)
            {
                if (a.GetLength(0) != a.GetLength(1))
                    throw new ArgumentException("Tem de ter mesmo nr de linhas e colunas");

                return a[0, 0] * a[1, 1] - a[1, 0] * a[0, 1];
            }

            double Matrix_Determinant3x3(double[,] a)
            {
                if (a.GetLength(0) != a.GetLength(1))
                    throw new ArgumentException("Tem de ter mesmo nr de linhas e colunas");

               /* // Ampliar matriz
                double[,] newMatrix = new double[a.GetLength(0), a.GetLength(1) + 2];

                for(int row = 0; row < a.GetLength(0); row++)
                {
                    for(int col = 0; col < a.GetLength(1); col++)
                    {
                        newMatrix[row, col] = a[row, col];
                    }
                    newMatrix[row, newMatrix.GetLength(1) - 2] = a[row, 0];
                    newMatrix[row, newMatrix.GetLength(1) - 1] = a[row, 1];
                }

                //MatrixPrint(newMatrix);

                double det1 = 0, det2 = 0;
                double nr_det1 = 1, nr_det2 = 1;

                for (int columns = 0; columns < 3; columns++)
                {
                    int row = 0;
                    nr_det1 = 1;
                    for (int col = columns; col < columns+3; col++)
                    {
                        nr_det1 *= newMatrix[row, col];
                        row++;
                    }
                    det1 += nr_det1;
                }

                for (int columns = 0; columns < 3; columns++)
                {
                    int row = newMatrix.GetLength(0) - 1;
                    nr_det2 = 1;
                    for (int col = 0; col < columns +2; col++)
                    {
                        nr_det2 *= newMatrix[row, col];
                        row--;
                    }
                    det2 += nr_det2;
                }
                

                return det1 - det2;*/
               
                
                return a[0,0]*a[1,1]*a[2,2] + a[0,1]*a[1,2]*a[2,0] + a[0,2]*a[1,0]*a[1,1] - a[2,0]*a[1,1]*a[0,2] - a[2,1]*a[1,2]*a[0,0] - a[2,2]*a[1,0]*a[0,2];

            }

            double Matrix_DeterminantNxN(double[,] matrix)
            {
                double det = 0;

                if (matrix.GetLength(0) != matrix.GetLength(1))
                    throw new ArgumentException("Tem de ter mesmo nr de linhas e colunas");

                if(matrix.GetLength(0) == 2)
                {
                    return Matrix_Determinant2x2(matrix);
                }

                // LAPLACE
                for(int col = 0; col < matrix.GetLength(1); col++)
                {
                    double[,] minorMatrix = CreateMinorMatrix(matrix, col);
                    MatrixPrint(minorMatrix);
                    Console.WriteLine("");

                    // Calcula determinantes das matrizes menores e soma todas
                    det += Math.Pow(-1, 1 + (col + 1)) * matrix[0, col] * Matrix_DeterminantNxN(minorMatrix);                    
                }
                
                return det;
            }

            double[,] CreateMinorMatrix(double[,] matrix, int columnToRemove)
            {
                int minorMatrix_row = 0, minorMatrix_col;
                double[,] minorMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];                

                for(int row = 1; row < matrix.GetLength(0); row++)
                {
                    minorMatrix_col = 0;

                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {                        
                        if (col == columnToRemove) continue;

                        minorMatrix[minorMatrix_row, minorMatrix_col] = matrix[row, col];
                        minorMatrix_col++;
                        
                    }
                    minorMatrix_row++;
                }

                return minorMatrix;
            }

            void MatrixPrint(double[,] a)
            {
                for (int row = 0; row < a.GetLength(0); row++)
                {
                    Console.Write("[");
                    for (int col = 0; col < a.GetLength(1); col++)
                    {
                        Console.Write($" {a[row, col]} ");
                    }
                    Console.WriteLine("]");
                }
            }

            double[,] Matrix_Read()
            {
                Console.Write("Digite o nr. linhas: ");
                int rows = int.Parse(Console.ReadLine());

                Console.Write("Digite o nr. colunas: ");
                int columns = int.Parse(Console.ReadLine());

                double[,] v = new double[rows, columns];
                for (int row = 0; row < v.GetLength(0); row++)
                {
                    for (int column = 0; column < v.GetLength(1); column++)
                    {
                        Console.Write($"Digite o valor do componente {row + 1}{column + 1}:");
                        v[row, column] = double.Parse(Console.ReadLine());
                    }
                }

                return v;
            }

            double Calculate_Determinant(double[,] a)
            {

                switch (a.GetLength(0))
                {
                    case 1:
                        return Matrix_Determinant1x1(a);

                    case 2:
                        return Matrix_Determinant2x2(a);

                    case 3:
                        return Matrix_Determinant3x3(a);

                    default:
                        return Matrix_DeterminantNxN(a);
                }
            }

        }
    }
}
