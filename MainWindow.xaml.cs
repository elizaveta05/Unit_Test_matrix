using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ПР_Тестирование_Матрицы
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] matrix1;
        private int[,] matrix2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_create1_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tb_stroke1.Text, out int rows) && int.TryParse(tb_stolb1.Text, out int columns))
            {
                if (rows > 1 && columns > 1)
                {
                    DataTable dt = new DataTable();

                    // Создаем столбцы в DataTable
                    for (int i = 0; i < columns; i++)
                    {
                        dt.Columns.Add(new DataColumn($"Column {i}", typeof(int)));
                    }

                    // Создаем строки с нулевыми значениями
                    for (int i = 0; i < rows; i++)
                    {
                        DataRow row = dt.NewRow();
                        for (int j = 0; j < columns; j++)
                        {
                            row[j] = 0;
                        }
                        dt.Rows.Add(row);
                    }

                    // Привязываем DataTable к DataGrid
                    dg1.ItemsSource = dt.DefaultView;
                }
                else
                {
                    MessageBox.Show("Количество строк и столбцов должно быть больше 1.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для строк и столбцов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_create2_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tb_stroke2.Text, out int rows) && int.TryParse(tb_stolb2.Text, out int columns))
            {
                if (rows > 1 && columns > 1)
                {
                    DataTable dt = new DataTable();

                    // Создаем столбцы в DataTable
                    for (int i = 0; i < columns; i++)
                    {
                        dt.Columns.Add(new DataColumn($"Column {i}", typeof(int)));
                    }

                    // Создаем строки с нулевыми значениями
                    for (int i = 0; i < rows; i++)
                    {
                        DataRow row = dt.NewRow();
                        for (int j = 0; j < columns; j++)
                        {
                            row[j] = 0;
                        }
                        dt.Rows.Add(row);
                    }

                    // Привязываем DataTable к DataGrid
                    dg2.ItemsSource = dt.DefaultView;
                }
                else
                {
                    MessageBox.Show("Количество строк и столбцов должно быть больше 1.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для строк и столбцов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt1 = ((DataView)dg1.ItemsSource).ToTable();
            DataTable dt2 = ((DataView)dg2.ItemsSource).ToTable();

            if (dt1 == null || dt2 == null)
            {
                MessageBox.Show("Сначала создайте обе матрицы");
                return;
            }

            int rows1 = dt1.Rows.Count;
            int cols1 = dt1.Columns.Count;
            int rows2 = dt2.Rows.Count;
            int cols2 = dt2.Columns.Count;

            matrix1 = new int[rows1, cols1]; // Используем поле класса matrix1
            matrix2 = new int[rows2, cols2]; // Используем поле класса matrix2

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    if (int.TryParse(dt1.Rows[i][j].ToString(), out int value))
                    {
                        matrix1[i, j] = value;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка в ячейке ({i}, {j}) первой матрицы. Введите только цифры.");
                        return;
                    }
                }
            }

            for (int i = 0; i < rows2; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    if (int.TryParse(dt2.Rows[i][j].ToString(), out int value))
                    {
                        matrix2[i, j] = value;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка в ячейке ({i}, {j}) второй матрицы. Введите только цифры.");
                        return;
                    }
                }
            }

            MessageBox.Show("Данные успешно сохранены.");
        }
        private DataTable ConvertToDataTable(int[,] matrix)
        {
            DataTable dt = new DataTable();

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                dt.Columns.Add(new DataColumn($"Column {j}", typeof(int)));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                dt.Rows.Add(row);
            }

            return dt;
        }
        private int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int cols2 = matrix2.GetLength(1);
            int[,] result = new int[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    for (int k = 0; k < cols1; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }

        private int[,] AddMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            int[,] result = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }

        private int[,] SubtractMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows = matrix1.GetLength(0);
            int cols = matrix1.GetLength(1);
            int[,] result = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return result;
        }

        private void btn_multiply_Click(object sender, RoutedEventArgs e)
        {
            if (matrix1 == null || matrix2 == null)
            {
                MessageBox.Show("Сначала создайте обе матрицы.");
                return;
            }

            if (matrix1.GetLength(1) != matrix2.GetLength(0))
            {
                MessageBox.Show("Нельзя умножать матрицы с данными размерами.");
                return;
            }

            int[,] resultMatrix = MultiplyMatrices(matrix1, matrix2);

            DataTable dtResult = ConvertToDataTable(resultMatrix);
            dg3.ItemsSource = dtResult.DefaultView;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (matrix1 == null || matrix2 == null)
            {
                MessageBox.Show("Сначала создайте обе матрицы.");
                return;
            }

            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                MessageBox.Show("Нельзя складывать матрицы с разными размерами.");
                return;
            }

            int[,] resultMatrix = AddMatrices(matrix1, matrix2);

            DataTable dtResult = ConvertToDataTable(resultMatrix);
            dg3.ItemsSource = dtResult.DefaultView;
        }

        private void btn_subtract_Click(object sender, RoutedEventArgs e)
        {
            if (matrix1 == null || matrix2 == null)
            {
                MessageBox.Show("Сначала создайте обе матрицы.");
                return;
            }

            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
            {
                MessageBox.Show("Нельзя вычитать матрицы с разными размерами.");
                return;
            }

            int[,] resultMatrix = SubtractMatrices(matrix1, matrix2);

            DataTable dtResult = ConvertToDataTable(resultMatrix);
            dg3.ItemsSource = dtResult.DefaultView;
        }

        private void btn_transponirovanie_Click(object sender, RoutedEventArgs e)
        {
            if (dg3.Items.IsEmpty)
            {
                MessageBox.Show("Матрица для транспонирования пустая.");
                return;
            }

            DataTable dt = ((DataView)dg3.ItemsSource).ToTable();
            int rows = dt.Rows.Count;
            int cols = dt.Columns.Count;
            int[,] inputMatrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(dt.Rows[i][j].ToString(), out inputMatrix[i, j]))
                    {
                        MessageBox.Show($"Ошибка в ячейке ({i}, {j}) матрицы для транспонирования. Введите только цифры.");
                        return;
                    }
                }
            }

            // Дополнительная проверка на квадратность матрицы для транспонирования
            if (rows != cols)
            {
                MessageBox.Show("Для транспонирования матрица должна быть квадратной.");
                return;
            }

            int[,] transposedMatrix = TransposeMatrix(inputMatrix);

            DataTable dtTransposed = ConvertToDataTable(transposedMatrix);
            dg3.ItemsSource = dtTransposed.DefaultView;
        }

        private int[,] TransposeMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[,] transposedMatrix = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }

            return transposedMatrix;
        }

    }
}
