using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text.Json;
using System.Xml.Linq;
using Restaurant.Models;


internal class Program
{
     static void Main(string[] args)
    {

        Connection connects = new Connection();


        




        string menuJsonData = File.ReadAllText("C:\\Users\\pgeerowa\\source\\repos\\Restaurant\\Restaurant\\Menu_List.json");

        // deserialize json into list
        var menu = JsonSerializer.Deserialize<Root>(menuJsonData);


        //check if menu list is null 
        if (menu != null)
        {
            foreach(var item in menu.Menu_List)
            {
                //String interpolation - Inject object into string
                Console.WriteLine($"{item.Id} {item.Name} {item.Price}");
            }
        }




        // User input - to get firstname and lastname
        Console.WriteLine("First enter your full name and Choose an Id option from the following list: ");

        Console.Write("Enter your FirstName:");
        string firstname = Console.ReadLine();
        Console.Write("Enter your LastName:");
        string lastname = Console.ReadLine();
        Console.WriteLine("Hello" + "  " + firstname + " " + lastname + " " +"Welcome!   Now choose your order id:");

        // connection to save info into database
       // connects.create_Customer(firstname, lastname);



        List<SelectedMenu> MenuList = new List<SelectedMenu>();


        var orderMore = true;

        while (orderMore)
        {
            Console.WriteLine("Enter your food id:");
            int order = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many you want to order:");
            int quantity = Convert.ToInt32(Console.ReadLine());


            var menuName = string.Empty;
            foreach (var currentMenu in menu.Menu_List)
            {

                if (currentMenu.Id == order.ToString())
                {
                  menuName = currentMenu.Name;
                }
            }

            



            var SelectedItem = new SelectedMenu(order.ToString(), menuName, quantity);


            MenuList.Add(SelectedItem);


            Console.WriteLine("do you want to order more food? Type Yes or Type X to exit");
            var uservalue = Console.ReadLine();     

            if (uservalue != null && uservalue.Trim().Equals("X", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("ok your order will be ready.");
                break;
            }
           

           


        }



        //instantiate object 
        MenuHelper menuhelper = new MenuHelper();

        Console.WriteLine("Here is your Order:");
        var total = 0.0;
        
        
      foreach (var order in MenuList )
       {
            Console.WriteLine("FoodId:" + order.Id + " " + "Quantity:" + order.Quantity);

            // To calculate total price 
            var x = menuhelper.TotalPricePerMenu(menu.Menu_List, order.Id, order.Quantity);

            total = total + x;
            Console.WriteLine(order.Id + "  " + order.Name + " " + order.Quantity + "  " + x   );

          

        };

        Console.WriteLine();


        //Total Price of orders.
        Console.WriteLine("Total amount to be paid:" + total);




        var menulistObj = Newtonsoft.Json.JsonConvert.SerializeObject(menu);
        connects.InsertOrderDetails(((int)total), firstname, lastname, menulistObj);

        Console.WriteLine(menulistObj);



        Console.WriteLine();
        Console.WriteLine("Here is your Receipts:!");
        Console.WriteLine();
        Console.WriteLine("Customer name:" + " " + string.Concat(firstname, " ", lastname));


        //ConsoleDataFormatter.PrintLine();

        foreach (var userinput in MenuList)
        {
            var x = menuhelper.TotalPricePerMenu(menu.Menu_List, userinput.Id, userinput.Quantity);
            //total = total + x;

            //call save method with (firstname, lastname, menu list selected, total price)
            Console.WriteLine(" Your Food Id is:" + " " + userinput.Id + " " + " and Food name " + userinput.Name + " " + "with Quantity:" + " " +userinput.Quantity + " "+ ". your cost:" + x);
            

        }


        Console.WriteLine("Total amount to be paid:" + total);




        Console.Read();






    
    
    }


   

}




