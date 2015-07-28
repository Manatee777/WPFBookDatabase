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
using System.ComponentModel; //pro listbox sort description

namespace MBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //nacteni linqtosql data contextu
        private static book_linqDataContext _datact = new book_linqDataContext();
        // zastupce tridy, ktera preklada vsechny tably z linqTables do kolekce, ktera se nasledne binduje s xaml prvky
        private data_class _zastupce_tridy_dat;

       
       

        public MainWindow()
        {
            InitializeComponent();
        }






        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            //otevreni noveho okna 
            Window1 nove = new Window1();
            nove.Show();
            //zavreni urrent okna
            Application.Current.Windows[0].Close(); 
        }


        //udalost po loadingu okna aneb nacteni observable kolekce lisboxem
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            //inicialiazce instance datove tridy, ktera ziskava v parametru linqtosql context, jeho tables ulozi do sve kolekce
            _zastupce_tridy_dat = new data_class(_datact);   
            //listbox ziska jako itemsource instance dataclass
            this.listbox1.ItemsSource = _zastupce_tridy_dat;
        }



        //nakonec nepouzity button pro zobrazení dat > nahrazem kliknutím na položku
        private void button_show_Click(object sender, RoutedEventArgs e)
        {
            Table prochazec = (Table)listbox1.SelectedItem;  
            label_nazev.Content = prochazec.Nazev;
            label_recenze.Content = new TextBlock() { Text = prochazec.Recenze, TextWrapping = TextWrapping.Wrap };
            label_hodnoceni.Content = prochazec.Hodnocení;
            label_zanr.Content = prochazec.Zanr;
            label_autor.Content = prochazec.Autor;
            label_id.Content = prochazec.Id;

            BitmapImage bitmapa = new BitmapImage();
            bitmapa.BeginInit();
            bitmapa.UriSource = new Uri(prochazec.Obrazek);
            bitmapa.EndInit();
            image1.Source = bitmapa;

            //barvení rectanglu
            switch (prochazec.Hodnocení)
            {
                case 5:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.IndianRed);
                    break;
                case 1:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.DarkSlateGray);
                    break;

                case 2:
                case 3:
                case 4:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.RosyBrown);
                    break;
            }
        }



        
        //šipkov buttony pro projzdeni obsahu
        private void button_right_Click(object sender, RoutedEventArgs e)
        {
            //pokud je index listboxu mensi nez pocet polozek - 1 ( kvuli indexaci od 0)
            if (listbox1.SelectedIndex < listbox1.Items.Count - 1)
            {
                button_left.IsEnabled = true;
                //prechod na dalsi polozku
                listbox1.SelectedIndex += 1;
                //pretypovani selected itemu na Table 
                Table sipka = (Table)listbox1.SelectedItem;
                //klasicky pruchod zaznamem a zobrazeni v labelech
                label_nazev.Content = sipka.Nazev;
                label_recenze.Content = sipka.Recenze;
                label_hodnoceni.Content = sipka.Hodnocení;
                label_zanr.Content = sipka.Zanr;
                label_autor.Content = sipka.Autor;
                //inicilizace bitmpaky
                BitmapImage bitmapa = new BitmapImage();
                bitmapa.BeginInit();
                bitmapa.UriSource = new Uri(sipka.Obrazek);
                bitmapa.EndInit();
                image1.Source = bitmapa;

            }
            //zamrznuti buttonu 
            else button_right.IsEnabled = false;
        }

        // -//-
        private void button_left_Click(object sender, RoutedEventArgs e)
        {

            if (listbox1.SelectedIndex > 1)
            {
                button_right.IsEnabled = true;
                listbox1.SelectedIndex -= 1;

                Table sipka = (Table)listbox1.SelectedItem;
                label_nazev.Content = sipka.Nazev;
                label_recenze.Content = sipka.Recenze;
                label_hodnoceni.Content = sipka.Hodnocení;
                label_zanr.Content = sipka.Zanr;
                label_autor.Content = sipka.Autor;

                BitmapImage bitmapa = new BitmapImage();
                bitmapa.BeginInit();
                bitmapa.UriSource = new Uri(sipka.Obrazek);
                bitmapa.EndInit();
                image1.Source = bitmapa;
                
            }

            else { 
                listbox1.SelectedIndex = 0;
                button_left.IsEnabled = false;
            
            }
        }



        
        //sort funkce comboboxu
        private void button_serad_Click(object sender, RoutedEventArgs e)
        {
            //ziskani stringformatu z xaml comboboxu
            string format = tridici_combobox.SelectionBoxItemStringFormat;

            switch (format)
            {

                case "Hodnocení":
                    //nejprve je treba vycistit vyber, jinak zustane predchozi vyber v pameti
                    listbox1.Items.SortDescriptions.Clear();
                    //sort description klesajici
                    listbox1.Items.SortDescriptions.Add(new SortDescription(tridici_combobox.SelectionBoxItemStringFormat, ListSortDirection.Descending));

                    break;

                case "Nazev":
                    listbox1.Items.SortDescriptions.Clear();//nutno zadat, jinak se do stringu ulozi na "pevno" prvni itemstrinformat
                    listbox1.Items.SortDescriptions.Add(new SortDescription(tridici_combobox.SelectionBoxItemStringFormat, ListSortDirection.Ascending));

                    break;
            }
        }




        //Po kliku na item
        private void ukaz_item_listboxu(object sender, RoutedEventArgs e) //event nadefinovat v xaml pod item container style jako handler
        {
            ListBoxItem item = e.Source as ListBoxItem;  //nejprve je potreba oznacit item listboxu jako zdroj pro event
            Table prochazec_data_ct = item.DataContext as Table;  //misto pretypovani listboxu se musi pretypovat kazdy item.datacontext zvlast na table
            //pretypování listboxu na Table

            label_nazev.Content = prochazec_data_ct.Nazev;
            label_recenze.Content = new TextBlock() { Text = prochazec_data_ct.Recenze, TextWrapping = TextWrapping.Wrap };
            label_hodnoceni.Content = prochazec_data_ct.Hodnocení;
            label_zanr.Content = prochazec_data_ct.Zanr;
            label_autor.Content = prochazec_data_ct.Autor;
            label_id.Content = prochazec_data_ct.Id;


            BitmapImage bitmapa = new BitmapImage();
            bitmapa.BeginInit();
            bitmapa.UriSource = new Uri(prochazec_data_ct.Obrazek);
            bitmapa.EndInit();
            image1.Source = bitmapa;


            switch (prochazec_data_ct.Hodnocení)
            {
                case 5:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.IndianRed);
                    break;
                case 1:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.DarkSlateGray);
                    break;

                case 2:
                case 3:
                case 4:
                    rectangle_rank.Fill = new SolidColorBrush(System.Windows.Media.Colors.RosyBrown);
                    break;
            }
        }
         


        //otevreni edit okna a zobrazeni stavajiciho zaznamu
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Edit_window edit_okno = new Edit_window();
            edit_okno.label_helpid.Content = label_id.Content;
            edit_okno.tb_nazev.Text = label_nazev.Content.ToString();
            edit_okno.tb_autor.Text = label_autor.Content.ToString();
            edit_okno.tb_kom.Text = label_recenze.ToString();
            edit_okno.tb_zanr.Text = label_zanr.Content.ToString();
            edit_okno.hodnoceni_combo.SelectedIndex = Convert.ToInt32(label_hodnoceni.Content) - 1; //-1 protoze index comboboxu zacina od 0
            edit_okno.label_image.Content = image1.Source;
            edit_okno.Show();
            Application.Current.Windows[0].Close(); 
            
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (listbox1.SelectedItem != null)
            {
                _datact.Tables.DeleteOnSubmit((Table)listbox1.SelectedItem); //smaze pretypovany item
                _zastupce_tridy_dat.Remove((Table)listbox1.SelectedItem); //smazat i z kolekceobsahujici _datact.tably
                _datact.SubmitChanges();

                label_autor.Content = null;
                label_hodnoceni.Content = null;
                label_id.Content = null;
                label_nazev.Content = null;
                label_zanr.Content = null;
                label_recenze.Content = null;
                image1.Source = null;
            }

        }



        // zbytecnost 
        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tridici_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

  




        }






        }




        



    

