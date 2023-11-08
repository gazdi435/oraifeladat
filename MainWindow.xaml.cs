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
using System.IO;

namespace OrganizationFeladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> szervezetek = new List<Organization>();

        void Betoltes(string fileName)
        {
            File.ReadAllLines(fileName).Skip(1).ToList().ForEach(x => szervezetek.Add(new Organization(x.Split(";"))));
        }
        public MainWindow()
        {
            InitializeComponent();
            Betoltes("organizations100.csv");
            dgAdatok.ItemsSource = szervezetek;

            cbOrszag.ItemsSource = szervezetek.Select(x=>x.Country).OrderBy(x=>x).Distinct().ToList();
            cbOrszag.SelectedIndex = 0;
            cbAlapitas.ItemsSource = szervezetek.Select(x => x.Founded).OrderBy(x => x).Distinct().ToList();
            cbAlapitas.SelectedIndex = 0;
            cbSzures.SelectedIndex = 3;


        }

        private void cbOrszag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(cbSzures.SelectedItem);
            if (cbSzures.SelectedItem == "Csak az ország szerint")
            {
                dgAdatok.ItemsSource = szervezetek.Where(x => x.Country == cbOrszag.SelectedItem).ToList();
            }
            else if (cbSzures.SelectedItem == "Csak az év szerint")
            {
                dgAdatok.ItemsSource = szervezetek.Where(x => x.Founded == Convert.ToInt32(cbAlapitas.SelectedItem)).ToList();
            }
            else if(cbSzures.SelectedItem == "Mindkettő szerint")
            {
                dgAdatok.ItemsSource = szervezetek.Where(x => x.Country == cbOrszag.SelectedItem && x.Founded==Convert.ToInt32(cbAlapitas.SelectedItem)).ToList();
            }
            else
            {
                dgAdatok.ItemsSource = szervezetek;
            }
        }

        private void cbAlapitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
