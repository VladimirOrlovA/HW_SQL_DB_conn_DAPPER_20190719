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

        static void Main(string[] args)
        {
            Dapper_sel();
            getProductCat(2);

            Console.ReadKey();
        }
    }
}
