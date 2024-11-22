using System.IO;
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

namespace GyakFel 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Adatok> Lakossag = new List<Adatok>();
        public MainWindow()
        {
            InitializeComponent();
            
            using StreamReader sr = new($"../../../src/bevolkerung.txt", encoding:Encoding.UTF8);
            string line = sr.ReadLine();
            while (!sr.EndOfStream) {
                Lakossag.Add(new Adatok(sr.ReadLine()));
            }
            

            for (int i = 1; i < 41; i++)
            {
                listaCombo.Items.Add(i);
            }
        }

        private void Feladatok(object sender, SelectionChangedEventArgs e)
        {
            int selectedNumber = (int)listaCombo.SelectedItem;




           
        }
        private void f1()
        {
            var max = Lakossag.MaxBy(l => l.Netto);
            

        }
    }
}