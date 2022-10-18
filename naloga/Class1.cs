using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;



namespace naloga
{
    public class Service
    {
       

        /// <summary>
        /// function find all people in database
        /// </summary>
        /// <returns></returns>
        public static List<Person> FindAllPeople()
        {
            baza baza = new baza();
            List<Person> people = new List<Person>();
            var command = new SQLiteCommand("SELECT * FROM people", baza.connection);
            baza.OpenConnection();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet data = new DataSet();
            adapter.Fill(data);

            foreach(DataRow row in data.Tables[0].Rows)
            {
                Person person = new Person();
                person.Id = (int)int.Parse(row["Id"].ToString());
                person.FirstName = (string)row["FirstName"];
                person.LastName = (string)row["LastName"];
                person.VatNumber = (int)int.Parse(row["VatNumber"].ToString());
                person.Adress = (string)row["Adress"];
                person.City = (string)row["City"];
                person.PostCode = (int)int.Parse(row["PostCode"].ToString());

                people.Add(person);
            }
            return people;
        }
        
       

        /// <summary>
        /// function add person into database
        /// </summary>
        /// <param name="person"></param>
        public static void AddPerson(Person person)
        {
            baza baza = new baza();
            var command = new SQLiteCommand("INSERT INTO people (FirstName, LastName, VatNumber, Adress, City, PostCode) VALUES (@FirstName, @LastName, @VatNumber, @Adress, @City, @PostCode)", baza.connection);
            baza.OpenConnection();

            command.Parameters.AddWithValue("@FirstName", person.FirstName);
            command.Parameters.AddWithValue("@LastName", person.LastName);
            command.Parameters.AddWithValue("@VatNumber", person.VatNumber);
            command.Parameters.AddWithValue("@Adress", person.Adress);
            command.Parameters.AddWithValue("@City", person.City);
            command.Parameters.AddWithValue("@PostCode", person.PostCode);

            command.ExecuteNonQuery();
            baza.CloseConnection();
        }



        /// <summary>
        /// function update person in database
        /// </summary>
        /// <param name="person"></param>
        public static void UpdatePerson(Person person)
        {
            baza baza = new baza();
            var command = new SQLiteCommand($"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', Adress = '{person.Adress}', City = '{person.City}', PostCode = {person.PostCode} WHERE VatNumber == {person.VatNumber}", baza.connection);
            baza.OpenConnection();
            command.ExecuteNonQuery();
            baza.CloseConnection();
        }

        /// <summary>
        /// function delete person from database
        /// </summary>
        /// <param name="vatNumber"></param>
        public static void DeletePerson(int vatNumber)
        {
            baza baza = new baza();
            var command = new SQLiteCommand($"DELETE FROM people WHERE VatNumber = {vatNumber}", baza.connection);
            baza.OpenConnection();
            command.ExecuteNonQuery();
            baza.CloseConnection();
        }




        /// <summary>
        /// function check if vat number has right lenght and if it is unquie
        /// </summary>
        /// <param name="vatNumber"></param>
        /// <returns></returns>
        public static bool CheckVatNumber(int vatNumber)
        {
            List<int> vatNumbers = new List<int>();
            List<Person> people = new List<Person>();
            people = FindAllPeople();
            foreach(Person person in people)
            {
                vatNumbers.Add(person.VatNumber);
            }
            if (vatNumber.ToString().Length == 8 && vatNumbers.Contains(vatNumber) == false)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// function chack if adress has house number
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public static bool CheckAdress( string adress)
        {
            string[] strings = adress.Split(' ');
            return strings.Last().Any(x => char.IsDigit(x));
        }

        /// <summary>
        /// function check if post code has lenght 4
        /// </summary>
        /// <param name="postCode"></param>
        /// <returns></returns>
        public static bool CheckPostCode(int postCode)
        {
            return postCode.ToString().Length == 4;
        }
    }
}
