using MySql.Data.MySqlClient;

namespace ProductsProjectAPI.Controllers
{
    public class Global
    {
        public static MySqlConnection getInstance() 
        {
            return new MySqlConnection("server=fintech.chc48k60w3sf.af-south-1.rds.amazonaws.com;uid=admin;pwd=huambo#1995;database=fintech");
        }
    }
}
