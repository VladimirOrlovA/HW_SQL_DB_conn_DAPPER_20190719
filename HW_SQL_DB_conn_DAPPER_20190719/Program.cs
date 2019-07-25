using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data.OleDb;

using Dapper;
using System.Data;

namespace HW_SQL_DB_conn_DAPPER_20190719
{
    class Program
    {
        private static readonly string conn_str = "Server = ASUS_P52F\\SQLEXPRESS; Database = InternetShop; user Id = OVA; Password=123";

        // для ознакомления, ДЗ ниже
        private static void Dapper_sel()
        {
            Console.WriteLine("DAPPER");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                List<ProductCategory> res = db.Query<ProductCategory>("SELECT id, category_name FROM ProductCategory", commandType: CommandType.Text).ToList();
                foreach (var item in res)
                {
                    Console.WriteLine($"{item.id} - {item.category_name}");
                }
            }
        }

        class ProductCategory
        {
            public int id { get; set; }
            public string category_name { get; set; }
        }

        private static void getProductCat(int? category_id)
        {
            Console.WriteLine("DAPPER");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                List<ProdCatProducts> res = db.Query<ProdCatProducts>("p_get_pc_p", new { category_id = category_id }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var item in res)
                {
                    Console.WriteLine($"{item.category_name} - {item.product_name}- {item.product_model}- {item.cost}");
                }
            }
        }

        class ProdCatProducts
        {
            public int category_id { get; set; }
            public string category_name { get; set; }
            public string product_name { get; set; }
            public string product_model { get; set; }
            public string cost { get; set; }
        }

        // ---------------------------------------------------------------
        // ------------------HW_SQL_DB_conn_DAPPER_20190719---------------
        // ---------------------------------------------------------------

        //TASK 1
        private static void selectCustomerByGender(string gender)
        {
            Console.WriteLine("DAPPER - for SELECT");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                List<GenderCustomer> res = db.Query<GenderCustomer>("p_select_by_gender", new { gender = gender }, commandType: CommandType.StoredProcedure).ToList();
                foreach (var item in res)
                {
                    Console.WriteLine($"{item.customer_id}  \t {item.first_name}  \t {item.last_name}");
                }
            }
        }

        class GenderCustomer
        {
            public int customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
        }


        //TASK 2
        private static void insertToTableCustomer
            (
            string first_name,
            string last_name,
            string email,
            string password,
            string address,
            long phone,
            string gender,
            DateTime birthdate,
            DateTime reg_date,
            int bonus_percent
            )
        {
            Console.WriteLine("DAPPER - for INSERT");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                db.Query<InsertCustomer>("p_insert_to_customer", new
                {
                    first_name = first_name,
                    last_name = last_name,
                    email = email,
                    password = password,
                    address = address,
                    phone = phone,
                    gender = gender,
                    birthdate = birthdate,
                    reg_date = reg_date,
                    bonus_percent = bonus_percent

                }, commandType: CommandType.StoredProcedure);

            }
        }

        class InsertCustomer
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string address { get; set; }
            public long phone { get; set; }
            public string gender { get; set; }
            public DateTime birthdate { get; set; }
            public DateTime reg_date { get; set; }
            public int bonus_percent { get; set; }
        }

        //TASK 3
        private static void deleteFromCustomerById (int customer_id)
        {
            Console.WriteLine("DAPPER - for DELETE");
            using (IDbConnection db = new SqlConnection(conn_str))
            {
                db.Query<InsertCustomer>("p_delete_from_Customer_by_id", new
                {
                    customer_id = customer_id
                }, commandType: CommandType.StoredProcedure);

            }
        }

        class DeleteCustomer
        {
            public int customer_id { get; set; }
        }

        static void Main(string[] args)
        {
            //Dapper_sel();
            //getProductCat(2);
            
            //selectCustomerByGender("f");
            //insertToTableCustomer("Николай", "Смирнов", "smirnov@mail.ru", "qwerty1", "Сейфулина, 510 - 35", 87019548651, "m", new DateTime(1985, 03, 08), new DateTime(2019, 07, 25), 10);
            //deleteFromCustomerById(22);
   
            Console.ReadKey();
        }
    }
}
