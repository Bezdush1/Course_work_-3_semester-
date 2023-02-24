using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace курсовая_самуйлов
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        int size;
        List<int> list_basic = new List<int>(); //односвязный список базовый
        List<int> list = new List<int>(); //односвязный список для сортировок

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //размерность списка
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //процент упорядоченности
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //сортировака прямым включение
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //сортировака двоичным включение
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //сортировака методом шелла
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //кнопка отсортировать
            size = (int)numericUpDown1.Value;
            if (size == 0)
            {
                MessageBox.Show("Вы не ввели размерность односвязного списка!");
                Application.Restart();
            }
            else if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked)
            {
                MessageBox.Show("Выберите один из четырёх типов упорядоченности!");
                Application.Restart();
            }


            ClearGraphic();
            CleatTable();
            int count_nax = 10;//кол-во шагов
            int right = size / 10;//правая граница
            int nax = size / 10;//промежуток между шагами
            int TimeBasic = 0;
            int TimeDouble = 0;
            int TimeShell = 0;
            

            for (int k = 0; k < count_nax; k++)
            {
                update_list(list_basic, list,right);
                TimeBasic += inclusionSort(list, right);// прямым включением

                update_list(list_basic, list, right);
                TimeDouble += doubleinclusionSort(list, right);//двоичным включением

                update_list(list_basic, list, right);
                TimeShell += ShellSort(list, right);//сортировка шелла
                
                Graphic(TimeBasic, TimeDouble, TimeShell, right);
                Table(TimeBasic, TimeDouble, TimeShell, right);

                right = nax * (k + 2);
            }
        }

        private void update_list(List<int> list_basic, List<int> list,  int right)
        {
            list.Clear();
            for (int i = 0; i < right; i++)
            {
                list.Add(list_basic.ElementAt(i));
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //кнопка выйти
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //неупорядоченный
            size = (int)numericUpDown1.Value;
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                list_basic.Add(rnd.Next(0, size));
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //упорядоченный
            size = (int)numericUpDown1.Value;
            Random rnd = new Random();
            list_basic.Add(rnd.Next(0, size));
            for (int i = 1; i < size; i++)
            {
                list_basic.Add(list_basic.ElementAt(i - 1) + rnd.Next(0, size));
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //упорядоченный в обратном порядке
            size = (int)numericUpDown1.Value;
            Random rnd = new Random();
            list_basic.Add(rnd.Next(0, size));
            for (int i = 1; i < size; i++)
            {
                list_basic.Add(list_basic.ElementAt(i - 1) + rnd.Next(0, size));
            }
            list_basic.Reverse();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //частично упорядоченный
            size = (int)numericUpDown1.Value;
            int percent = (int)numericUpDown2.Value;
            int count = (size * percent) / 100;
            Random rnd = new Random();
            list_basic.Add(rnd.Next(0, size));
            for (int i = 1; i < count; i++)
            {
                list_basic.Add(list_basic.ElementAt(i - 1) + rnd.Next(0, size));
            }
            for (int i = count; i < size; i++)
            {
                list_basic.Add(rnd.Next(0, size));
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClearGraphic()
        {
            for (int i = 0; i < 3; i++)
            {
                this.chart1.Series[i].Points.Clear();
            }
            this.chart1.Update();
        }

        private void CleatTable()
        {
            dataGridView1.Rows.Clear();
        }

        private void Graphic(int first_sort, int second_sort, int third_sort, int number)
        {
            this.chart1.Series[0].Points.AddXY(number, first_sort);
            this.chart1.Series[1].Points.AddXY(number, second_sort);
            this.chart1.Series[2].Points.AddXY(number, third_sort);
        }

        private void Table(int first_sort, int second_sort, int third_sort, int number)
        {
            dataGridView1.Rows.Add(number, first_sort, second_sort, third_sort);
        }

        int inclusionSort(List<int> list, int size)
        {
            int StartTime = Environment.TickCount;
            for (int i = 1; i < size; i++)
            {
                int value = list[i]; 
                int index = i;     
                while ((index > 0) && (list[index - 1] > value))
                {   
                    list[index] = list[index - 1];
                    index--;    
                }
                list[index] = value; 
            }
            return Environment.TickCount - StartTime;
        }

        int ShellSort(List<int> list, int size)
        {
            int StartTime = Environment.TickCount;
            int i, j, step;
            int tmp;
            for (step = size / 2; step > 0; step /= 2)
                for (i = step; i < size; i++)
                {
                    tmp = list[i];
                    for (j = i; j >= step; j -= step)
                    {
                        if (tmp < list[j - step])
                            list[j] = list[j - step];
                        else
                            break;
                    }
                    list[j] = tmp;
                }
            return Environment.TickCount - StartTime;
        }

        int doubleinclusionSort(List<int> list, int size)
        {
            int StartTime = Environment.TickCount;
            int j, m, left, right, x;
            for (int i = 1; i < size; i++)
                if (list[i - 1] > list[i])
                {
                    x = list[i];
                    left = 0;
                    right = i - 1;
                    do
                    {
                        m = (left + right) / 2;
                        if (list[m] < x) left = m + 1;
                        else right = m - 1;
                    } while (left <= right);
                    for (j = i - 1; j >= left; j--)
                        list[j + 1] = list[j];
                    list[left] = x;
                }
            return Environment.TickCount - StartTime;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
