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

namespace MBooks
{
    /// <summary>
    /// Interaction logic for Edit_window.xaml
    /// </summary>
    public partial class Edit_window : Window
    {

        private static book_linqDataContext _datact = new book_linqDataContext();
        private data_class _zastupce_tridy_dat;

        public int id;

        public Edit_window()
        {
           
            InitializeComponent();


           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int default_id = Convert.ToInt32(label_helpid.Content);
            foreach (Table update in _datact.Tables)
            {
                if (update.Id == default_id)
                {
                    _datact.Tables.DeleteOnSubmit(update);
                    _zastupce_tridy_dat.Remove(update);
                    
                    _datact.SubmitChanges();
                   

                    Table novy = new Table();
                    novy.Nazev = tb_nazev.Text;
                    novy.Recenze = tb_kom.Text;
                    novy.Zanr = tb_zanr.Text;
                    novy.Hodnocení = Convert.ToInt32(hodnoceni_combo.SelectionBoxItemStringFormat); //nutno konverzovat
                    novy.Autor = tb_autor.Text;
                    novy.Obrazek = label_image.Content.ToString();


 
                   
                    _datact.Tables.InsertOnSubmit(novy);
                    _zastupce_tridy_dat.Add(novy);
                    _datact.SubmitChanges();
                    

                    



                   

                    Application.Current.Windows[0].Close(); 
                        
                }
            }
            

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



            }
        }

        private void tb_nazev_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow stare = new MainWindow();
            stare.Show();
          
        }

        private void update_button_Loaded(object sender, RoutedEventArgs e)
        {

            _zastupce_tridy_dat = new data_class(_datact);
          
        }

        private void tb_kom_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void hodnoceni_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
