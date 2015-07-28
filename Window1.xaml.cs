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
using System.Collections.ObjectModel; 


namespace MBooks
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private static book_linqDataContext _datact = new book_linqDataContext();
        private data_class objekt_data2;

       

        private string transport = "";

      
        public Window1()
        {
            InitializeComponent();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Table nova_kniha = new Table();
            nova_kniha.Nazev = tb_nazev.Text;
            nova_kniha.Zanr = tb_zanr.Text;
            nova_kniha.Hodnocení = Convert.ToInt32(hodnoceni_combo.SelectionBoxItemStringFormat); //nutno konverzovat
            nova_kniha.Autor = tb_autor.Text;
            nova_kniha.Obrazek = transport;



          
            _datact.Tables.InsertOnSubmit(nova_kniha); //pridani mezi tables v linq
            objekt_data2.Add(nova_kniha); //pridani do kolekce
           

            _datact.SubmitChanges();


           

            Application.Current.Windows[0].Close(); 

          //  Button abstract_button = new Button();
         //   abstract_button.Click += new RoutedEventHandler(updater);


           //MainWindow nove = new MainWindow();

           // objekt_data2 = new data_class(_datact); 
           // nove.listbox1.ItemsSource = objekt_data2;
          
            
        }

      //  private void updater(object sender, RoutedEventArgs e)
    //    {
          //  MainWindow nove = new MainWindow();
         //   nove.InvalidateVisual();
           
      //  }

      


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            objekt_data2 = new data_class(_datact);
        }




        private void tb_nazev_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog otevrit = new Microsoft.Win32.OpenFileDialog();

            otevrit.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            Nullable<bool> result = otevrit.ShowDialog();

            if (result == true)
            {
                string JmenoObrazku = otevrit.FileName;
                label_image.Content = JmenoObrazku;
               
               
               
                transport = JmenoObrazku;
               
                
                
                

            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow stare = new MainWindow();
            stare.Show();
        }
    }
}