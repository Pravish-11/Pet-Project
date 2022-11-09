using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class MenuHelper
    {

        
        internal double TotalPricePerMenu(List<MenuList> menuList, string Id, int Quantity)
        {

            //int price = 0;
            var price = 0.0;

            foreach (var menu in menuList)
            {

                if (menu.Id == Id)
                {
                    price = menu.Price * Quantity;
                    break;
                }
            }

            return price;
        }
    }
}




