using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Olympiada_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try//Находим ошибки ввода
            {             
                string path = @textBox1.Text;
                string[] lines = File.ReadAllLines(path);//Читаем все строки в файле и записываем их в массив
                int kolvo = 0;
                Int32.TryParse(lines[0], out kolvo);//Находим кол-во строк             

                int[,] rows_and_places = new int[kolvo, 2];//Создаём двухмерный массив рядов и мест

                progressBar1.Maximum = rows_and_places.GetLength(0);//Значение максимума для загрузки

                for (int i = 1; i < lines.Length; i++)//С помощью этого цикла делим числа из строки на ряда и места и записываем в двухмерный массив
                {
                    char[] numbers = lines[i].ToCharArray();//Записываем все символы из строки в массив символов
                    string row = "";
                    string place = "";
                    int space = 0;
                    for (int k = 0; k < numbers.Length; k++)//Ищем пробел в строке и записываем его номер в переменную
                    {
                        if (numbers[k] == ' ')
                        {
                            space = k;
                        }
                    }
                    for (int l = 0; l < space; l++)// Записываем числа в строку до пробела
                    {
                        row += numbers[l];
                    }
                    rows_and_places[i - 1, 0] = Int32.Parse(row);//Записываем числа из строки в массив

                    for (int j = space; j < numbers.Length; j++)
                    {
                        place += numbers[j];
                    }
                    rows_and_places[i - 1, 1] = Int32.Parse(place) * -1;


                }

                for (int i = 0; i < rows_and_places.GetLength(0); i++)//Цикл для сортировки массива
                {
                    progressBar1.Value += 1;//Добавляется значение после каждого повтора цикла
                    for (int j = 0; j < rows_and_places.GetLength(0) - 1; j++)
                    {
                        //Условия для сортировки столбцов по возрастанию
                        if (rows_and_places[j, 0] > rows_and_places[j + 1, 0] || rows_and_places[j, 0] == rows_and_places[j + 1, 0] && rows_and_places[j, 1] > rows_and_places[j + 1, 1])
                        {
                            for (int c = 0; c < rows_and_places.GetLength(1); c++)//Сортировка методом пузырька
                            {                               
                                var temp = rows_and_places[j, c];
                                rows_and_places[j, c] = rows_and_places[j + 1, c];
                                rows_and_places[j + 1, c] = temp;
                            }
                        }
                    }
                }

                int most_row = 0;
                int least_place = 0;
                for (int i = 1; i < kolvo - 1; i++)//Находим среди чисел нужные нам значения
                {
                    
                    if (rows_and_places[i, 0] == rows_and_places[i - 1, 0])//Если номер ряда совпадают, то код идёт дальше
                    {
                        if ((rows_and_places[i, 1] - rows_and_places[i - 1, 1]) == 14)// Если вычесть из места одного ряда значения другого и получилось 14, тогда записываем ответы в переменную
                        {
                            most_row = rows_and_places[i, 0];
                            least_place = rows_and_places[i, 1] - 1;

                        }
                    }
                }
                textBox2.Text = $"{most_row}, {least_place * (-1)}";//Выводим ответы
            }
            catch (IOException)//Если введён неправильный путь, выведет ошибку
            {
                MessageBox.Show("Неправильно введён путь файла");
            }
            catch (Exception)//Если в файле первая строка неправильного типа, выдаст ошибку
            {
                MessageBox.Show("Неправильный тип входных данных");
            }
        }

    }
}
