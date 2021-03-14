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


//Тема: Параллельное программирование
//Задание №1
//Приложение считывает набор целых значений из файла в список.
//Необходимо посчитать сумму всех чисел, максимум, минимум. Для решения
//задачи используйте возможности PLINQ.
//Задание №2
//Приложение считывает набор целых значений из файла в список.
//Необходимо посчитать количество уникальных значений. Для решения задачи
//используйте возможности PLINQ.




namespace WindowsFormsAppPrallelReadingFromFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static List<int> GetFromFile(string filename)
        {
            List<int> list = new List<int>();
            string textFromFile = "";
            byte[] array = null;
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    array = new byte[fs.Length];
                    fs.Read(array, 0, array.Length);

                    textFromFile = Encoding.Default.GetString(array);

                    foreach (var item in textFromFile.Split(' '))
                    {
                        list.Add(int.Parse(item));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return list;

        }

        static string getPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dialog.Filter = "text files (*.txt)|*.txt";
          
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }

            return "undefined";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>(GetFromFile(getPath()));


            var sumArr = list.AsParallel<int>().Sum();
            this.textBox1.Text = sumArr.ToString();

            var min = list.AsParallel<int>().Min();
            this.textBox2.Text = min.ToString();

            var max = list.AsParallel<int>().Max();
            this.textBox3.Text = max.ToString();

            IEnumerable<int> distinct = list.AsParallel<int>().Distinct();


            foreach (var item in distinct)
            {
                this.textBox4.Text += item + " | ";
            }

           



          
        }
    }
}
