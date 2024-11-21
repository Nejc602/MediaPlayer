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

namespace WpfApp1
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

        public void importMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Import menu clicked");
        }
        public void exportMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export menu clicked");
        }
        public void exitMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exit menu clicked");
        }
        public void addMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add menu clicked");
        }
        public void deleteMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete menu clicked");
        }
        public void editMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit menu clicked");
        }
        public void settingsMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings menu clicked");
        }

        public void button1Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 1 clicked");
        }
    }
}