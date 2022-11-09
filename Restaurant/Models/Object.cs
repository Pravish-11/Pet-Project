using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public  class SelectedMenu

    {


        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

         
 



        public SelectedMenu(string Id, string Name, int Quantity)
        {
            this.Id = Id;
            this.Name = Name;   
            this.Quantity = Quantity;
           
           

        }




    }
}
