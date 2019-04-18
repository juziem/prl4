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

            if (adt.ShowDialog() == true)
            {               
                string sql = "INSERT INTO test_table (nb_field, fio_field,ph_field,mt_field) VALUES (" + adt.nb.Text + ",'" + adt.fio.Text + "'," + adt.ph.Text + "," + adt.mt.Text + ")";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                var d = new CTest { nb_field = int.Parse(adt.nb.Text), fio_field = adt.fio.Text, ph_field = int.Parse(adt.ph.Text), mt_field = int.Parse(adt.mt.Text) };
                data.Items.Add(d);
            }
        }

        private void rd_Click(object sender, RoutedEventArgs e)
        {            
            string sql = "SELECT * FROM test_table";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var d = new CTest { nb_field = int.Parse(reader["nb_field"].ToString()), fio_field = reader["fio_field"].ToString(), ph_field = int.Parse(reader["ph_field"].ToString()), mt_field = int.Parse(reader["mt_field"].ToString()) };
                data.Items.Add(d);
            }
        }

        private void cg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void del_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}