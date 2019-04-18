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
using System.Windows.Shapes;

namespace lb4
{
    /// <summary>
    /// Логика взаимодействия для kak.xaml
    /// </summary>
    public partial class kak : Window
    {
        public kak()
        {
            InitializeComponent();

        }

        private void add_Click(object sender, RoutedEventArgs e)
            {
                this.DialogResult = true;

            }

        private void cn_Click(object sender, RoutedEventArgs e)
            {
                this.DialogResult = false;
            }

    }
}
