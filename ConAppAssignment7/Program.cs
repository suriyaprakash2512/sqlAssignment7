using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7Assignment
{
    internal class Program
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataAdapter sda;
        public static DataSet ds;
        public static string constr = "server=DESKTOP-G2EN09F;database=LibraryDB;trusted_connection=true;";
        static void Main(string[] args)
        {
            con = new SqlConnection(constr);

            string c;
            try
            {
                do
                {
                    Console.WriteLine("Choose the operation \n 1.Display  Books \n 2.Add Books \n 3.Update Quantity \n");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            {
                                cmd = new SqlCommand("select * from Books", con);
                                sda = new SqlDataAdapter(cmd);
                                ds = new DataSet();
                                con.Open();
                                sda.Fill(ds, "Books");
                                con.Close();
                                Console.WriteLine("Book ID \t Book Name \t\t Author \t\tGenre \t\tQuantity");
                                foreach (DataRow row in ds.Tables["Books"].Rows)
                                {
                                    Console.Write(row["BookId"] + "\t \t");
                                    Console.Write(row["Title"] + "\t \t");
                                    Console.Write(row["Author"] + "\t \t");
                                    Console.Write(row["Genre"] + "\t");
                                    Console.Write(row["Quantity"]);
                                    Console.WriteLine("\n");
                                }
                                break;
                            }
                        case 2:
                            {
                                cmd = new SqlCommand("select * from Books", con);
                                sda = new SqlDataAdapter(cmd);
                                ds = new DataSet();
                                con.Open();
                                sda.Fill(ds, "Books");
                                DataTable dt = ds.Tables["Books"];
                                DataRow dr = dt.NewRow();
                                Console.WriteLine("Enter Book Id");
                                dr["BookId"] = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter Book Name");
                                dr["Title"] = Console.ReadLine();
                                Console.WriteLine("Enter Author Name");
                                dr["Author"] = Console.ReadLine();
                                Console.WriteLine("Enter Genre");
                                dr["Genre"] = Console.ReadLine();
                                Console.WriteLine("Enter Quantity");
                                dr["Quantity"] = int.Parse(Console.ReadLine());
                                dt.Rows.Add(dr);
                                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                                sda.Update(ds, "Books");
                                Console.WriteLine("New Book Inserted!!!");
                                con.Close();
                                break;
                            }
                        case 3:
                            {
                                cmd = new SqlCommand("select * from Books", con);
                                sda = new SqlDataAdapter(cmd);
                                ds = new DataSet();
                                con.Open();
                                sda.Fill(ds, "Books");
                                Console.WriteLine("Enter Book Id to Update the Customer");
                                int bid = int.Parse(Console.ReadLine());
                                DataRow dr = null;
                                foreach (DataRow row in ds.Tables["Books"].Rows)
                                {
                                    if ((int)row["BookId"] == bid)
                                    {
                                        dr = row;
                                        break;
                                    }
                                }
                                if (dr == null)
                                {
                                    Console.WriteLine($"No such Book Id {bid} exists");
                                }
                                else
                                {

                                    Console.WriteLine("Enter New Quantity");
                                    dr["Quantity"] = int.Parse(Console.ReadLine());
                                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                                    sda.Update(ds, "Books");

                                    Console.WriteLine("Book Quantity Updated!!!");
                                    con.Close();
                                }

                                break;
                            }
                    }
                    Console.WriteLine("Do You Wanna Continue Press y/n");
                    c = Console.ReadLine();

                } while (c == "y");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error!!!" + ex.Message);
            }

        }
    }
}