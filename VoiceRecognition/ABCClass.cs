using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace VoiceRecognition
{
    class ABCClass
    {
        private string server, user, pass, database, strConn;
        MySqlConnection conn;
        public ABCClass()
        {
            initDBConfig("127.0.0.1","root","","speechrec");
        }

        public ABCClass(string server, string user, string pass, string database)
        {
            initDBConfig(server, user, pass, database);
        }

         public void initDBConfig(string server, string user, string pass, string database)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.database = database;

            strConn = "server="+server+";uid="+user+";pwd="+pass+";database="+database+";";
        }

         public void startConn()
         {
             try
             {
                 conn = new  MySqlConnection();
                 conn.ConnectionString = strConn;
                 conn.Open();
                 MessageBox.Show("Se Armo Coneccion");
             }
             catch (MySqlException ex)
             {
                 MessageBox.Show("No se Armo Coneccion");
             }
            // return this.conn;
         }

         public List<string> getEmpleados(string sql)
         {
             List <string> emp = new List<string>();
             //startConn();
             //conn.Open();
             MySqlCommand comm = new MySqlCommand(sql, conn);
             MySqlDataReader read = comm.ExecuteReader();

             while(read.Read()){
                 emp.Add(read.GetString(0));
             }
             read.Close();

             return emp;
         }

    }
}
