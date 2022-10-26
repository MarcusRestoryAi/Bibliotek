using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Bibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initera variabeler med Koppling Information
            string server = "localhost";
            string database = "göteborghandel";
            string user = "root"; 
            string pass = "SokrateS13"; //ANGE EGET LÖSENORD

            string connectionString = $"SERVER={server};DATABASE={database};UID={user};PASSWORD={pass};";

            //Skapa en Koppling till vår MySQL Databas
            MySqlConnection conn = new MySqlConnection(connectionString);

            //Menyval
            ConsoleKeyInfo input;

            do //Do While Loop
            {
                //Rensa consolen
                Console.Clear();

                //Skriv ut Meny
                Console.WriteLine("Välkommen till DB meny");
                Console.WriteLine("----------------------");
                Console.WriteLine("1. Skriv in ett nytt namn");
                Console.WriteLine("2. Hämta alla namn");
                Console.WriteLine("3. Avsluta!");

                //Hämta användarens menyval
                input = Console.ReadKey();

                //SwitchCase
                switch (input.KeyChar.ToString())
                {
                    //Varje Case är ett möjligt värde
                    case "1":
                        //Detta skall ske om värdet är 1
                        //Anropa funktion för att skriva in ett nytt namn till DB
                        Console.Clear();
                        InsertNameToDB(conn);
                        break;
                    case "2":
                        //Detta skall ske om värdet är 2
                        //Anropa funktion för att hämta namn från DB
                        Console.Clear();
                        SelectNameFromDB(conn);
                        break;
                    //Default är ett Catch-all händelse
                    default:
                        //Hantering vid fel val
                        break;
                }
            } while (input.KeyChar.ToString() != "3");

            

            
        }

        static void InsertNameToDB(MySqlConnection conn)
        {
            //Be användaren om ett namn input
            Console.Write("Skriv in ett namn:");
            string name = Console.ReadLine();

            //Skapa vår SQL Insert query
            string insertQuery = $"insert into person(name) values ('{name}')";

            //Öppna Connection
            conn.Open();

            //Skapa Commando till Databas
            MySqlCommand cmd = new MySqlCommand(insertQuery, conn);

            //Exekvera Command
            cmd.ExecuteReader();

            //Stänga koppling till DB
            conn.Close();

            //Meddelande till användaren
            Console.WriteLine("Name inserted successfully!");
            Console.WriteLine("(Press any key to continue...)");
            Console.ReadKey();
        }

        static void SelectNameFromDB(MySqlConnection conn)
        {
            //Skriv vår SQL Query
            string query = "select * from person";

            //Öppna våran connection
            conn.Open();

            //Skapa en MySqlCommand obj
            MySqlCommand cmd = new MySqlCommand(query, conn);

            //Execute command och spara resultatet i SqlReader
            MySqlDataReader reader = cmd.ExecuteReader();

            //WhileLoop för att gå igenom Reader Objektet
            while (reader.Read())
            {
                //Hämta värden från Reader
                int id = (int) reader["id"];
                string name = (string) reader["name"];

                //Skriv ut namn och nummer till Konsol
                Console.WriteLine($"{id}. {name}");
            }

            //Stänga connection
            conn.Close();

            //Meddelande till användaren
            Console.WriteLine("Names fetched successfully!");
            Console.WriteLine("(Press any key to continue...)");
            Console.ReadKey();
        }
    }
}