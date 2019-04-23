using System;
using System.Collections.Generic;
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
using System.Data.SQLite;


namespace lb4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        kak adt;
        SQLiteConnection m_dbConnection;

        public class CTest
        {
            public int nb_field { get; set; }
            public string fio_field { get; set; }
            public int ph_field { get; set; }
            public int mt_field { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.ShowDialog();
            string db_name = dlg.FileName;

            m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");

            m_dbConnection.Open();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            adt = new kak();
            try
            {
                if (adt.ShowDialog() == true)
                {
                    string sql = "INSERT INTO test_table (nb_field, fio_field,ph_field,mt_field) VALUES (" + adt.nb.Text + ",'" + adt.fio.Text + "'," + adt.ph.Text + "," + adt.mt.Text + ")";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();

                    var d = new CTest { nb_field = int.Parse(adt.nb.Text), fio_field = adt.fio.Text, ph_field = int.Parse(adt.ph.Text), mt_field = int.Parse(adt.mt.Text) };
                    data.Items.Add(d);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Ошибка!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат записи!");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Пусто!");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Переполнение!");
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Пусто!");
            }
        }

        private void rd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                data.Items.Clear();
                string sql = "SELECT * FROM test_table";

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        var d = new CTest { nb_field = int.Parse(reader["nb_field"].ToString()), fio_field = reader["fio_field"].ToString(), ph_field = int.Parse(reader["ph_field"].ToString()), mt_field = int.Parse(reader["mt_field"].ToString()) };
                        data.Items.Add(d);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверный формат записи!");
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Пусто!");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Переполнение!");
                }
                catch (SQLiteException)
                {
                    MessageBox.Show("Пусто!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Файл не найден!");
            }
        }
    

        private void cg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CTest test = (CTest)data.SelectedItem;
                adt = new kak(test.nb_field.ToString(), test.fio_field.ToString(), test.ph_field.ToString(), test.mt_field.ToString());

                if (adt.ShowDialog() == true)
                {
                    data.Items.RemoveAt(data.SelectedIndex);

                    string sql = "UPDATE test_table SET nb_field = " + adt.nb.Text + ", fio_field = '" + adt.fio.Text + "', ph_field = " + adt.ph.Text + ", mt_field = " + adt.mt.Text + " WHERE nb_field = " + test.nb_field;
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();

                    var d = new CTest { nb_field = int.Parse(adt.nb.Text), fio_field = adt.fio.Text, ph_field = int.Parse(adt.ph.Text), mt_field = int.Parse(adt.mt.Text) };
                    data.Items.Add(d);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Ошибка!");
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат записи!");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Пусто!");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Переполнение!");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выберите элемент!");
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Пусто!");
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CTest test = (CTest)data.SelectedItem;

                data.Items.RemoveAt(data.SelectedIndex);

                string sql = "DELETE FROM test_table WHERE nb_field = " + test.nb_field;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выберите элемент!");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Выберите элемент!");
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Пусто!");
            }
        }

  
    }
}