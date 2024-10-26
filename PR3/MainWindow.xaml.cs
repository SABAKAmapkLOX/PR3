using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using LibMas;
using Lib_6;
using System.IO;
using System;
namespace PR3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void SaveMatrix(int[,] matrix)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы *.*|*.*|Текстовый файл|*txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                if (matrix != null)
                {
                    StreamWriter file = new StreamWriter(save.FileName);
                    file.WriteLine(matrix.GetLength(0));
                    file.WriteLine(matrix.GetLength(1));
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            file.Write(matrix[i, j] + ";");
                        }
                        file.WriteLine();
                    }
                    file.Close();
                }
                else MessageBox.Show("Нету матрицы!!!");
            }
        }
        public void OpenMatrix(int[,] matrix)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы *.*|*.*|Текстовый файл|*txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                StreamReader file = new StreamReader(open.FileName);
                string[] str = file.ReadToEnd().Split('\n');

                int row = Convert.ToInt32(str[0]);
                int column = Convert.ToInt32(str[1]);
                matrix = new int[row, column];
                for (int i = 0; i < row; i++)
                {
                    string[] line = str[i + 2].Split(';');
                    for (int j = 0; j < column; j++)
                    {
                        matrix[i, j] = int.Parse(line[j]);
                    }
                }
                file.Close();
                Calculater.SumMatrix(out int sumMatrix, matrix);
                tbCalc.Text = Convert.ToString(sumMatrix);
            }
        }
        public void CleanMatrix(int[,] matrix)
        {
            matrix = null;
            tbCalc.Text = Convert.ToString(matrix);
        }
        int[,] matrix;
        private void btCalc_Click(object sender, RoutedEventArgs e)
        {
            bool boolColumn = int.TryParse(tbColumnMatrix.Text, out int columnMatrix);
            bool boolRandom = int.TryParse(tbRandom.Text, out int randomMatrix);
            bool boolRow = int.TryParse(tbRowMatrix.Text, out int rowMatrix);
            if (boolRandom == true & boolColumn == true & boolRow == true & columnMatrix > 0)
            {
                int sumMas;
                int column = Convert.ToInt32(columnMatrix);
                int row = Convert.ToInt32(rowMatrix);
                int randMax = Convert.ToInt32(randomMatrix);
                RandomMatrix.InitMatrix(out matrix, column, row, randMax);
                Calculater.SumMatrix(out sumMas, matrix);
                tbCalc.Text = Convert.ToString(sumMas);
            }
            else MessageBox.Show("Введите числа !!!!!");
        }

        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сделал:Ермаков Павел \nПрактическая работа №3 \nЗадание:Дана матрица размера M × N. Для каждого столбца матрицы с четным номером (2, 
4, …) найти сумму его элементов. Условный оператор не использовать. ");
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as MenuItem;
            switch (s.Header)
            {
                case "Сохранить матрицу": SaveMatrix(matrix); ; break;
                case "Открыть матрицу": OpenMatrix(matrix); ; break;
                case "Очистить матрицу": CleanMatrix(matrix); ; break;
                case "Выход": Close(); break;
            }
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
