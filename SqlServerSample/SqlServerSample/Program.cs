using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

/*
 * Program is about to connect multiple databases 
 * and display the contents of those databases when sql statment is processed
 * @author: ST
 * 
 */

namespace SqlServerSample
{

    class SqlServerConnection
    {
      SqlConnection connection;
      String sql;
      SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
      //Constructor -- Initialises the Connection String
        public SqlServerConnection(String databaseName, String sql)
            {
            builder.DataSource = "techlauncher.icognition.cloud"; 
            builder.UserID = "techlaunchersql";              
            builder.Password = "techlaunchersql";      
            builder.InitialCatalog = databaseName;
            this.sql = sql;
            this.connection = new SqlConnection(builder.ConnectionString);
            }
       /*
        * Process SQL query
        * @return String Output of the SQL statement 
        */
        public String query_sql(String sql)
        {
        String output ="";
        try{
            using (SqlConnection connection = this.connection)
            {
            this.connection.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            command = new SqlCommand(sql,connection);
            dataReader = command.ExecuteReader();
            while(dataReader.Read())
                {
                output = output + " " + dataReader.GetValue(0);
            }
            
            dataReader.Close();
            command.Dispose();
            }
        }
        catch (SqlException e)
        {
                Console.WriteLine(e.ToString());
        }
            return output;    
        }
        //closing the connection
        ~SqlServerConnection()
            {
            connection.Close();
        }              
    }
    
    class Program
    {
        static void Main(string[] args)
        {
           
                String sql = "SELECT * from dbo.TSACCESSCO";
                String result_alpha,result_bravo,result_charlie;

                Console.WriteLine("Establsihing Connection with database Alpha");
                Console.WriteLine("Output from Alpha: ");
                SqlServerConnection alpha = new SqlServerConnection("Alpha", sql);
                result_alpha = alpha.query_sql(sql);
                Console.WriteLine(result_alpha);
               
                Console.WriteLine('\n');
                Console.WriteLine("Establsihing Connection with database Bravo");
                Console.WriteLine("Output from Bravo: ");
                SqlServerConnection bravo = new SqlServerConnection("Bravo", sql);
                result_bravo =  bravo.query_sql(sql);
                Console.WriteLine(result_bravo);

                 Console.WriteLine('\n');
                Console.WriteLine("Establsihing Connection with database Charlie");
                Console.WriteLine("Output from Charlie: ");
                SqlServerConnection charlie = new SqlServerConnection("Charlie", sql);
                result_charlie = charlie.query_sql(sql);
                Console.WriteLine(result_charlie);
                
                Console.WriteLine('\n');
                Console.WriteLine("Displaying the data from all 3 database: ");
                Console.WriteLine(result_alpha +" "+result_bravo+" "+result_charlie);
       
            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);

        }

   
    }
}
