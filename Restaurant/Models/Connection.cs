using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Restaurant.Models
{

    class Connection
    {

        SqlConnection connection;
        string connectionString = null;

        


        public Connection()
        {
            
            

            connectionString = "Data Source=PBIO-2941;Initial Catalog=Restaurant;User ID=pgeerowal;Password=Tom@123";


            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //command = new SqlCommand(sql, connection);
                //dataReader = command.ExecuteReader();
                //while (dataReader.Read())
                //{
                //    Console.WriteLine(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
                //}
                //dataReader.Close();
                //command.Dispose();
                //connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
            }
        }



        public void create_Customer(string firstname, string lastname)
        {

            SqlCommand command;
            SqlDataReader dataReader;

            // take userinput(firstname/lastname) & save to db Customers 
           var  sql = $"Insert into Customers(firstname, lastname) values('{firstname}','{lastname}')";

            

            command = new SqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
            }
            dataReader.Close();
            command.Dispose();
            // connection.Close();




        }






        public Customer FetchCustomerByName(string firstname, string lastname)
        {
            Customer cust = new Customer();

            SqlDataReader dataReader;


            var sql = $"SELECT * FROM Customers where firstname='{firstname}' and lastname = '{lastname}'";


            
            //create command
            SqlCommand command = new SqlCommand(sql, connection);

            
            command.Prepare();
            command.ExecuteNonQuery();



            using ( dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        cust.Id = dataReader.GetInt32(0);
                        cust.firstname = dataReader.GetString(1);
                        cust.lastname = dataReader.GetString(2);

                    }


                }


                return cust;
            }
        }




        public void InsertOrderDetails(int total, string firstname, string lastname, string menulistobj)
        {
            
            Customer customer = FetchCustomerByName(firstname, lastname);

            if (customer.Id <= 0)
            {
                 create_Customer(firstname, lastname);
                customer = FetchCustomerByName(firstname, lastname);

            }

            
           


            var sql = $"insert into tbl_order(order_details,customer_id,total_price) values('{menulistobj}', '{customer.Id}',{total})";


            var command = new SqlCommand(sql, connection);


            command.ExecuteNonQuery();
        }




        

        public void DisplayReport()
        {
            var stsSelectAllOrder = $"SELECT ord.order_id,ord.order_details,ord.total_price,ord.date_time,cust.customer_id,cust.firstname,cust.lastname FROM tbl_order ord, Customer cust where cust.customer_id = ord.customer_id ";
            var command = new SqlCommand(stsSelectAllOrder, connection);
            command.Prepare();
            command.ExecuteNonQuery();
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.HasRows)
                {
                    //String.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", );
                    ConsoleDataFormatter.PrintRow("Order Id", "Order details", "total price", "Date/Time of order", "customer Id", "firstname", "lastname");



                    while (dataReader.Read())
                    {
                        //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}", dr.GetInt32(0),
                        //dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetInt32(4), dr.GetValue(5));
                        ConsoleDataFormatter.PrintRow(dataReader.GetInt32(0).ToString(), dataReader.GetString(1), dataReader.GetInt32(2).ToString(), dataReader.GetValue(3).ToString(), dataReader.GetInt32(4).ToString(), dataReader.GetString(5), dataReader.GetString(6));



                    }



                    ConsoleDataFormatter.PrintLine();



                }




                return;
            }



        }






    }
}

    



