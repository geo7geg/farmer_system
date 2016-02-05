using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net;

namespace farmer_system
{
    public partial class Form2 : Form
    {
        string connectionString = WebApplication1.Class1.sqlstring;

        public Form2()
        {
            InitializeComponent();
            ip.Mask = @"###\.###\.###\.###";
            ip.ValidatingType = typeof(System.Net.IPAddress);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (name.Text != "" && ip.Text != "")
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlDataReader reader;

                command.CommandText = "SELECT * FROM arduino where name=@p3";
                command.Prepare();

                command.Parameters.AddWithValue("@p3", name.Text);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("Το Όνομα Σταθμού " + name.Text + " είναι ήδη καταχωρημένο. Επιλέξτε διαφορετικό όνομα");
                }
                else
                {
                    reader.Close();

                    command.Parameters.Clear();

                    command.CommandText = "SELECT * FROM arduino where ip=@p4";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p4", ip.Text);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("H IP " + ip.Text + " είναι ήδη καταχωρημένη. Επιλέξτε διαφορετική ΙΡ");
                    }
                    else
                    {
                        IPAddress ipAddress;
                        if (IPAddress.TryParse(ip.Text.Replace(" ", ""), out ipAddress))
                        {
                            reader.Close();

                            command.Parameters.Clear();

                            command.CommandText = "INSERT into arduino (name,ip) values (@p1,@p2)";
                            command.Prepare();
                            command.Parameters.AddWithValue("@p1", name.Text);
                            command.Parameters.AddWithValue("@p2", ipAddress.ToString());

                            command.ExecuteNonQuery();

                            connection.Close();

                            MessageBox.Show("Επιτυχής Εισαγωγή Σταθμού");

                            this.Close();

                            if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
                            {
                                (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).run_bg();
                            }
                        }
                        else
                        {
                            MessageBox.Show("H IP " + ip.Text + " δεν είναι έγκυρη. Επιλέξτε διαφορετική ΙΡ");
                        }
                    }

                    
                }

                connection.Close();
            }            
            else
            {
                MessageBox.Show("Συμπληρώστε τα όλα τα πεδία");
            }
        }
    }
}
