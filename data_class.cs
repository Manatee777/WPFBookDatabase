using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;  //pouziti dynamickych kolekcí


//trida pro pristup k sql skrze linqTosql

namespace MBooks
{
    class data_class : ObservableCollection<Table>
    {

        //ihned v konstruktoru je treba pridat vsechny tabulky(tably) do kolekce

      

        public data_class(book_linqDataContext context)
        {

       

            foreach (Table zaznam in context.Tables)  //pro všechny nové tabulky v linsqTosql 
            {
             this.Add(zaznam);   // Collection<Table>.Add nová tabulka 
            }
        }

       
    }
}
