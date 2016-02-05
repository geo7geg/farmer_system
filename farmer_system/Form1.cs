using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Management;
using System.Net;
using System.Net.Sockets;
using Quartz;
using Quartz.Impl;
using System.Globalization;

namespace farmer_system
{
    public partial class Form1 : Form
    {
        string connectionString = WebApplication1.Class1.sqlstring;

        public Form1()
        {
            InitializeComponent();
          
            run_bg();

            timer1.Interval = 15000;
            timer1.Enabled = true;
            timer1.Tick += new System.EventHandler(OnTimerEvent);

            //timer2.Interval = 10000;
            //timer2.Enabled = true;
            //timer2.Tick += new System.EventHandler(OnTimerEvent1);

            listView1.View = View.List;
            listView1.View = View.Details;
            listView1.Columns.Add("Κωδικός", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Όνομα", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Επώνυμο", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Μονάδες", 115, HorizontalAlignment.Left);
            listView1.Columns[0].TextAlign = HorizontalAlignment.Center;
            listView1.Columns[1].TextAlign = HorizontalAlignment.Center;
            listView1.Columns[2].TextAlign = HorizontalAlignment.Center;
            listView1.Columns[3].TextAlign = HorizontalAlignment.Center;

            ///////////////////////////////////-CHECK SD FILES-/////////////////////////////////

            WebApplication1.Class1.txt_checkwrite("192.168.1.66");
            //WebApplication1.Class1.arduino_txtcheck_all();

            /////////////////////////////////////-ENCRYPTION-//////////////////////////////////

            //string test = File.ReadLines(@"\\192.168.1.65\arduino\www\farmer.txt", Encoding.Default).First().Trim();
            //string test = File.ReadLines(@"D:farmer.txt", Encoding.Default).First().Trim();
            //string test = "A73A.2F3B/1;33/0#2,2,3#2916911";
            //string test = ":;7;.A;43/231:4/0#21,11,26#2190;935";
            //char[] array = test.ToCharArray();
            //string key = "12";
            //char[] array1 = key.ToCharArray();
            //char[] array2 = WebApplication1.Class1.XORENC(array, array1);
            //string decrypt = new string(array2);
            //MessageBox.Show(decrypt);

            //string test = "9848-B870,10000,3 1/27/16 10:51:55";
            //string decrypted = WebApplication1.Class1.XORENC(test);
            //MessageBox.Show(decrypted);
            //email.Text = decrypted;
            //////////////////////////////////////-SCHEDULE-///////////////////////////////////

            // construct a scheduler factory
            //ISchedulerFactory schedFact = new StdSchedulerFactory();

            //// get a scheduler
            //IScheduler sched = schedFact.GetScheduler();
            //sched.Start();

            //// define the job and tie it to our HelloJob class
            //IJobDetail job = JobBuilder.Create<HelloJob>()
            //    .WithIdentity("myJob", "group1")
            //    .Build();

            //// Trigger the job to run now, and then every 40 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //  .WithIdentity("myTrigger", "group1")
            //  .StartAt(DateBuilder.DateOf(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year))
            //  .WithSimpleSchedule(x => x
            //      .WithIntervalInSeconds(30)
            //      .RepeatForever())
            //  .Build();

            //sched.ScheduleJob(job, trigger);

            //////////////////////////////////////////////////////////////////////////////////

            //Connect("192.168.1.65", 80);

            //WebApplication1.Class1.encrypt("aa" , @"C:\Users\George\Desktop\encrypt.txt");
            //string decrypt_words = WebApplication1.Class1.decrypt();
            //MessageBox.Show("Round Trip: " + decrypt_words);

            //string text = "A class is the most powerful data type in C#";
            //File.WriteAllText(@"\\192.168.1.65\arduino\www\format.txt", text);

            //StreamWriter file2 = new StreamWriter(@"\\192.168.1.65\arduino\www\format1.txt", true);
            //file2.WriteLine("Hello");
            //file2.Close();

            //MessageBox.Show(File.ReadLines(@"\\192.168.1.65\arduino\www\format.txt").First().Trim());

            //WebApplication1.Class1.USBSerialNumber usb = new WebApplication1.Class1.USBSerialNumber();
            //string serial = usb.getSerialNumberFromDriveLetter(@"D:");
            //MessageBox.Show(serial);

            //var usbDevices = GetUSBDevices();
            //foreach (var usbDevice in usbDevices)
            //{
            //    MessageBox.Show(usbDevice.DeviceID + "---------" + usbDevice.Description + "-------" + usbDevice.PnpDeviceID);
            //}
        }

        //public class HelloJob : IJob
        //{
        //    public void Execute(IJobExecutionContext context)
        //    {

        //    }
        //}

        //public Guid ToLittleEndian(this Guid javaGuid)
        //{
        //    byte[] net = new byte[16];
        //    byte[] java = javaGuid.ToByteArray();
        //    for (int i = 8; i < 16; i++)
        //    {
        //        net[i] = java[i];
        //    }
        //    net[3] = java[0];
        //    net[2] = java[1];
        //    net[1] = java[2];
        //    net[0] = java[3];
        //    net[5] = java[4];
        //    net[4] = java[5];
        //    net[6] = java[6];
        //    net[7] = java[7];
        //    return new Guid(net);
        //}

        private void OnTimerEvent(object sender, EventArgs e)
        {
            run_bg();
        }

        private void OnTimerEvent1(object sender, EventArgs e)
        {
            run_bg2();
        }

        public void run_bg()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = false;
            bw.WorkerReportsProgress = false;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
                timer1.Stop();
                timer1.Start();
            }
        }

        public void run_bg2()
        {
            BackgroundWorker bw2 = new BackgroundWorker();
            bw2.WorkerSupportsCancellation = false;
            bw2.WorkerReportsProgress = false;
            bw2.DoWork += new DoWorkEventHandler(bw2_DoWork);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            if (bw2.IsBusy != true)
            {
                bw2.RunWorkerAsync();
            }
        }

        public void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            fillarduino1();
        }

        public void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            WebApplication1.Class1.arduino_txtcheck_all();
        }

        public static Boolean Connect(string host, int port)
        {
            try
            {
                IPAddress[] IPs = Dns.GetHostAddresses(host);

                Socket s = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                IAsyncResult result = s.BeginConnect(IPs[0], port, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(1000, true);

                if (success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //try
                //{
                //    s.Connect(IPs[0], port);
                //    //MessageBox.Show("Connected");
                //    return true;
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.ToString());// something went wrong
                //    return false;
                //}
            }
            catch (Exception)
            {
                return false;
            }
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_DiskDrive"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }
    

        class USBDeviceInfo
        {
            public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
            {
                this.DeviceID = deviceID;
                this.PnpDeviceID = pnpDeviceID;
                this.Description = description;
            }
            public string DeviceID { get; private set; }
            public string PnpDeviceID { get; private set; }
            public string Description { get; private set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (units.Text != "" && name.Text != "" && surname.Text != "" && mobilephone.Text != "" && afm.Text != "" && doy.Text != "" && usb.Text != "")
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                
                command.CommandText = "INSERT into farmer (units,usb,name,surname,MobilePhone,HomePhone,HomeStreet,email,afm,doy) values (@p2,@p11,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)";
                command.Prepare();
                command.Parameters.AddWithValue("@p2", units.Text);
                command.Parameters.AddWithValue("@p3", name.Text);
                command.Parameters.AddWithValue("@p4", surname.Text);
                command.Parameters.AddWithValue("@p5", mobilephone.Text);
                command.Parameters.AddWithValue("@p6", homephone.Text);
                command.Parameters.AddWithValue("@p7", homestreet.Text);
                command.Parameters.AddWithValue("@p8", email.Text);
                command.Parameters.AddWithValue("@p9", afm.Text);
                command.Parameters.AddWithValue("@p10", doy.Text);
                command.Parameters.AddWithValue("@p11", usb.Text);

                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Επιτυχής Εισαγωγή Αγρότη");

                foreach (Control control in panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox txt = (TextBox)control;
                        txt.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Συμπληρώστε τα απαραίτητα πεδία με τον αστερίσκο");
            }
        }

        private void farmerimputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = "";
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM farmer", connection);

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Αγρότη";
            dataGridView1.Columns[1].HeaderText = "Μονάδες";
            dataGridView1.Columns[2].HeaderText = "UUID USB";
            dataGridView1.Columns[3].HeaderText = "Όνομα";
            dataGridView1.Columns[4].HeaderText = "Επίθετο";
            dataGridView1.Columns[5].HeaderText = "Κινητό Τηλέφωνο";
            dataGridView1.Columns[6].HeaderText = "Τηλέφωνο Οικίας";
            dataGridView1.Columns[7].HeaderText = "Διεύθυνση";
            dataGridView1.Columns[8].HeaderText = "Email";
            dataGridView1.Columns[9].HeaderText = "ΑΦΜ";
            dataGridView1.Columns[10].HeaderText = "ΔΟΥ";
            
            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM events", connection);

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Γεγονότος";
            dataGridView1.Columns[1].HeaderText = "Κωδικός Αγρότη";
            dataGridView1.Columns[2].HeaderText = "Ημερομηνία";
            dataGridView1.Columns[3].HeaderText = "Ενέργεια";
            dataGridView1.Columns[4].HeaderText = "Μονάδες";
            dataGridView1.Columns[5].HeaderText = "Υπόλοιπο Μονάδων";
            
            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = true;
        }

        public void fillFarmer()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();

            string com = "Select * from farmer ORDER BY surname DESC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, connection);
            DataSet myDataSet = new DataSet();
            adpt.Fill(myDataSet, "farmer");
            DataTable myDataTable = myDataSet.Tables[0];
            DataRow tempRow = null;

            listView1.Items.Clear();

            foreach (DataRow tempRow_Variable in myDataTable.Rows)
            {
                tempRow = tempRow_Variable;

                ListViewItem lvi = new ListViewItem(tempRow["uuid"].ToString());
                lvi.SubItems.Add(tempRow["name"].ToString());
                lvi.SubItems.Add(tempRow["surname"].ToString());
                lvi.SubItems.Add(tempRow["units"].ToString());
   
                lvi.UseItemStyleForSubItems = false;

                lvi.ForeColor = Color.WhiteSmoke;
                lvi.SubItems[1].ForeColor = Color.WhiteSmoke;
                lvi.SubItems[2].ForeColor = Color.WhiteSmoke;
                lvi.SubItems[3].ForeColor = Color.WhiteSmoke;

                listView1.Items.Add(lvi);
            }

        }

        public void fillarduino()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();

            string com = "Select * from arduino ORDER BY id ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, connection);
            DataSet myDataSet = new DataSet();
            adpt.Fill(myDataSet, "arduino");
            DataTable myDataTable = myDataSet.Tables[0];
            DataRow tempRow = null;

            if (listView2.InvokeRequired)
                listView2.Invoke(new MethodInvoker(delegate
                {
                    listView2.Items.Clear();

                }));
            else
                listView2.Items.Clear();
           
            foreach (DataRow tempRow_Variable in myDataTable.Rows)
            {
                tempRow = tempRow_Variable;

                ListViewItem lvi = new ListViewItem(tempRow["name"].ToString());

                if (Connect(tempRow["ip"].ToString(), 80) == true)
                {
                    lvi.SubItems.Add("CONNECTED");
                    lvi.SubItems[1].ForeColor = Color.ForestGreen;
                }
                else
                {
                    lvi.SubItems.Add("DISCONNECTED");
                    lvi.SubItems[1].ForeColor = Color.DarkRed;
                }

                lvi.SubItems.Add(tempRow["ip"].ToString());
                lvi.SubItems[2].ForeColor = Color.DarkBlue;

                lvi.UseItemStyleForSubItems = false;

                lvi.ForeColor = Color.DarkBlue;

                if (listView2.InvokeRequired)
                    listView2.Invoke(new MethodInvoker(delegate
                    {
                        listView2.Items.Add(lvi);

                    }));
                else
                    listView2.Items.Add(lvi);
            }
            connection.Close();
        }

        public void fillarduino1()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();

            string com = "Select * from arduino ORDER BY id ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, connection);
            DataSet myDataSet = new DataSet();
            adpt.Fill(myDataSet, "arduino");
            DataTable dataTable = myDataSet.Tables[0];

            DataTable dtCloned = dataTable.Clone();
            dtCloned.Columns[0].DataType = typeof(String);
            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (Connect(dtCloned.Rows[i][2].ToString(), 22) == true)// or port=80
                {
                    dtCloned.Rows[i][0] = "CONNECTED";
                }
                else
                {
                    dtCloned.Rows[i][0] = "DISCONNECTED";
                }
            }


            if (dataGridView2.InvokeRequired)
                dataGridView2.Invoke(new MethodInvoker(delegate
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = dtCloned;

                }));
            else
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = dtCloned;

            connection.Close();

            if (dataGridView2.InvokeRequired)
                dataGridView2.Invoke(new MethodInvoker(delegate
                {
                    dataGridView2.Columns[1].DisplayIndex = 0;
                    dataGridView2.Columns[1].HeaderText = "Arduino";
                    dataGridView2.Columns[0].HeaderText = "Κατάσταση";
                    dataGridView2.Columns[2].HeaderText = "ΙΡ";
                    dataGridView2.Columns[3].HeaderText = "1";
                    dataGridView2.Columns[4].HeaderText = "2";
                    dataGridView2.Columns[5].HeaderText = "3";
                    dataGridView2.Columns[6].HeaderText = "4";
                }));
            else
                dataGridView2.Columns[1].DisplayIndex = 0;
                dataGridView2.Columns[1].HeaderText = "Arduino";
                dataGridView2.Columns[0].HeaderText = "Κατάσταση";
                dataGridView2.Columns[2].HeaderText = "ΙΡ";
                dataGridView2.Columns[3].HeaderText = "1";
                dataGridView2.Columns[4].HeaderText = "2";
                dataGridView2.Columns[5].HeaderText = "3";
                dataGridView2.Columns[6].HeaderText = "4";

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value.ToString() == "CONNECTED")
                {
                    dataGridView2.Rows[i].Cells[0].Style.ForeColor = Color.ForestGreen;
                }
                else
                {
                    dataGridView2.Rows[i].Cells[0].Style.ForeColor = Color.DarkRed;
                }
            }
        }

        public void checkarduino()
        {
            if (listView2.InvokeRequired)
                listView2.Invoke(new MethodInvoker(delegate
                {
                    for (int i = 0; i < listView2.Items.Count; i++)
                    {
                        if (Connect(listView2.Items[i].SubItems[2].Text, 80) == true)
                        {
                            if (listView2.InvokeRequired)
                                listView2.Invoke(new MethodInvoker(delegate
                                {
                                    listView2.Items[i].SubItems[1].Text = "CONNECTED";
                                    listView2.Items[i].SubItems[1].ForeColor = Color.ForestGreen;
                                }));
                            else
                                listView2.Items[i].SubItems[1].Text = "CONNECTED";
                                listView2.Items[i].SubItems[1].ForeColor = Color.ForestGreen;
        }
                        else
                        {
                            if (listView2.InvokeRequired)
                                listView2.Invoke(new MethodInvoker(delegate
                                {
                                    listView2.Items[i].SubItems[1].Text = "DISCONNECTED";
                                    listView2.Items[i].SubItems[1].ForeColor = Color.DarkRed;
                                }));
                            else
                                listView2.Items[i].SubItems[1].Text = "DISCONNECTED";
                                listView2.Items[i].SubItems[1].ForeColor = Color.DarkRed;
                        }
                    }
                }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "UPDATE farmer SET units= units + @p1 WHERE uuid=@p2";
                command.Prepare();
                command.Parameters.AddWithValue("@p1", Convert.ToInt32(numericUpDown1.Text));
                command.Parameters.AddWithValue("@p2", listView1.SelectedItems[0].Text);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Η Προσθήκη ολοκληρώθηκε");

                fillFarmer();
            }
            else
            {
                MessageBox.Show("Επιλέξτε έναν αγρότη");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WebApplication1.Class1.USBSerialNumber usb = new WebApplication1.Class1.USBSerialNumber();
                    string path = folderBrowserDialog1.SelectedPath;
                    string serial = usb.getSerialNumberFromDriveLetter(path.Remove(path.Length - 1));

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlDataReader reader;

                    command.CommandText = "SELECT * FROM farmer where uuid=@p3";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p3", listView1.SelectedItems[0].Text);
                    reader = command.ExecuteReader();

                    string farmer_serial = "";
                    int units = 0;
                    if (reader.Read())
                    {
                        units = Convert.ToInt32(reader["units"]);
                        farmer_serial = reader["usb"].ToString();
                    }

                    reader.Close();

                    if (serial == farmer_serial)
                    {
                        command.Parameters.Clear();

                        if (units - Convert.ToInt32(numericUpDown1.Text) >= 0)
                        {
                            //string key = "12";
                            command.Parameters.Clear();

                            command.CommandText = "UPDATE farmer SET units= units - @p1 WHERE uuid=@p2";
                            command.Prepare();
                            command.Parameters.AddWithValue("@p1", Convert.ToInt32(numericUpDown1.Text));
                            command.Parameters.AddWithValue("@p2", listView1.SelectedItems[0].Text);
                            command.ExecuteNonQuery();

                            command.Parameters.Clear();

                            command.CommandText = "SELECT * FROM farmer where uuid=@p3";
                            command.Prepare();

                            command.Parameters.AddWithValue("@p3", listView1.SelectedItems[0].Text);
                            reader = command.ExecuteReader();

                            string text = "";
                            string text1 = "";
                            int units1 = 0;
                            int uuid = 0;
                            try
                            {
                                string test = File.ReadLines(folderBrowserDialog1.SelectedPath + "units.txt", Encoding.Default).First().Trim();
                                
                                if (reader.Read())
                                {
                                    if (test != "")
                                    {
                                        //char[] array3 = test.ToCharArray();
                                       
                                        //char[] array4 = key.ToCharArray();
                                        string test1 = WebApplication1.Class1.XORENC(test);
                                        //string test1 = new string(array5);

                                        text = test1.Substring(0, test1.IndexOf(","));
                                        text1 = test1.Substring(test1.IndexOf(" "));

                                        text = text + "," + reader["units"].ToString() + "," + reader["uuid"].ToString() + text1;
                                        //DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") 
                                        units1 = Convert.ToInt32(reader["units"]);
                                        uuid = Convert.ToInt32(reader["uuid"]);
                                    }
                                    else
                                    {
                                        text = "NA," + reader["units"].ToString() + "," + reader["uuid"].ToString() + " ";

                                        units = Convert.ToInt32(reader["units"]);
                                        uuid = Convert.ToInt32(reader["uuid"]);
                                    }
                                }

                                reader.Close();

                                //char[] array = text.ToCharArray();
                                //key = "12";
                                //char[] array1 = key.ToCharArray();
                                //char[] array2 = WebApplication1.Class1.XORENC(test);
                                string encrypt = WebApplication1.Class1.XORENC(text);

                                File.WriteAllText(folderBrowserDialog1.SelectedPath + "units.txt", encrypt, Encoding.Default);

                                fillFarmer();

                                command.Parameters.Clear();

                                command.CommandText = "INSERT into fill (uuid,date,adding,units) values (@p1,@p2,@p3,@p4)";
                                command.Prepare();
                                command.Parameters.AddWithValue("@p1", uuid);
                                command.Parameters.AddWithValue("@p2", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                                command.Parameters.AddWithValue("@p3", "-" + numericUpDown1.Text);
                                command.Parameters.AddWithValue("@p4", units1);
                                command.ExecuteNonQuery();

                                MessageBox.Show("Η Προσθήκη Μονάδων στο USB ολοκληρώθηκε");
                                connection.Close();
                            }
                            catch (Exception)
                            {
                                if (reader.Read())
                                {
                                    text = "NA," + reader["units"].ToString() + "," + reader["uuid"].ToString() + " ";

                                    //char[] array = text.ToCharArray();
                                    //key = "12";
                                    //char[] array1 = key.ToCharArray();
                                    //char[] array2 = WebApplication1.Class1.XORENC(array, array1);
                                    string encrypt = WebApplication1.Class1.XORENC(text);

                                    units1 = Convert.ToInt32(reader["units"]);
                                    uuid = Convert.ToInt32(reader["uuid"]);

                                    reader.Close();

                                    File.WriteAllText(folderBrowserDialog1.SelectedPath + "units.txt", encrypt, Encoding.Default);

                                    fillFarmer();

                                    command.Parameters.Clear();

                                    command.CommandText = "INSERT into fill (uuid,date,adding,units) values (@p1,@p2,@p3,@p4)";
                                    command.Prepare();
                                    command.Parameters.AddWithValue("@p1", uuid);
                                    command.Parameters.AddWithValue("@p2", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                                    command.Parameters.AddWithValue("@p3", "-" + numericUpDown1.Text);
                                    command.Parameters.AddWithValue("@p4", units1);
                                    command.ExecuteNonQuery();

                                    MessageBox.Show("Η Προσθήκη Μονάδων στο USB ολοκληρώθηκε");
                                    connection.Close();
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Οι Μονάδες δεν επιτρέπεται να έχουν αρνητικό πρόσημο");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Το Serial Number του αγρότη και του Usb δεν ταιριάζουν");
                    }

                }
            }
            else
            {
                MessageBox.Show("Επιλέξτε έναν αγρότη");
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            fillFarmer();
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WebApplication1.Class1.USBSerialNumber usb = new WebApplication1.Class1.USBSerialNumber();
                    string path = folderBrowserDialog1.SelectedPath;
                    string serial = usb.getSerialNumberFromDriveLetter(path.Remove(path.Length - 1));

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlDataReader reader;

                    command.CommandText = "SELECT * FROM farmer where uuid=@p3";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p3", listView1.SelectedItems[0].Text);
                    reader = command.ExecuteReader();

                    string farmer_serial = "";
                    if (reader.Read())
                    {
                        farmer_serial = reader["usb"].ToString();
                    }

                    reader.Close();

                    if (serial == farmer_serial)
                    {
                        //string key = "12";

                        command.Parameters.Clear();

                        command.CommandText = "UPDATE farmer SET units= units + @p1 WHERE uuid=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", Convert.ToInt32(numericUpDown1.Text));
                        command.Parameters.AddWithValue("@p2", listView1.SelectedItems[0].Text);
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();

                        command.CommandText = "SELECT * FROM farmer where uuid=@p3";
                        command.Prepare();

                        command.Parameters.AddWithValue("@p3", listView1.SelectedItems[0].Text);
                        reader = command.ExecuteReader();

                        string test = "";
                        string text = "";
                        string text1 = "";
                        int units = 0;
                        int uuid = 0;
                        try
                        {
                            test = File.ReadLines(folderBrowserDialog1.SelectedPath + "units.txt", Encoding.Default).First().Trim();
                            //MessageBox.Show(test);
                            if (reader.Read())
                            {
                                if (test != "")
                                {
                                    //char[] array3 = test.ToCharArray();
                                    //char[] array4 = key.ToCharArray();
                                    //char[] array5 = WebApplication1.Class1.XORENC(test);
                                    string test1 = WebApplication1.Class1.XORENC(test);

                                    text = test1.Substring(0, test1.IndexOf(","));
                                    text1 = test1.Substring(test1.IndexOf(" "));

                                    text = text + "," + reader["units"].ToString() + "," + reader["uuid"].ToString() + text1;
                                    //DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") 
                                    units = Convert.ToInt32(reader["units"]);
                                    uuid = Convert.ToInt32(reader["uuid"]);
                                }
                                else
                                {
                                    text = "NA," + reader["units"].ToString() + "," + reader["uuid"].ToString() + " ";

                                    units = Convert.ToInt32(reader["units"]);
                                    uuid = Convert.ToInt32(reader["uuid"]);
                                }
                            }

                            reader.Close();
                           
                            //char[] array = text.ToCharArray();
                            //key = "12";
                            //char[] array1 = key.ToCharArray();
                            //char[] array2 = WebApplication1.Class1.XORENC(array, array1);
                            string encrypt = WebApplication1.Class1.XORENC(text);

                            File.WriteAllText(folderBrowserDialog1.SelectedPath + "units.txt", encrypt, Encoding.Default);
                            

                            fillFarmer();

                            command.Parameters.Clear();

                            command.CommandText = "INSERT into fill (uuid,date,adding,units) values (@p1,@p2,@p3,@p4)";
                            command.Prepare();
                            command.Parameters.AddWithValue("@p1", uuid);
                            command.Parameters.AddWithValue("@p2", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                            command.Parameters.AddWithValue("@p3", "+" + numericUpDown1.Text);
                            command.Parameters.AddWithValue("@p4", units);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Η Προσθήκη Μονάδων στο USB ολοκληρώθηκε");
                            connection.Close();
                    }
                        catch (Exception)
                    {
                        if (reader.Read())
                        {
                            text = "NA," + reader["units"].ToString() + "," + reader["uuid"].ToString() + " ";
                                //char[] array = text.ToCharArray();
                                //key = "12";
                                //char[] array1 = key.ToCharArray();
                                //char[] array2 = WebApplication1.Class1.XORENC(text);
                                string encrypt = WebApplication1.Class1.XORENC(text);

                                units = Convert.ToInt32(reader["units"]);
                                uuid = Convert.ToInt32(reader["uuid"]);

                                reader.Close();

                                File.WriteAllText(folderBrowserDialog1.SelectedPath + "units.txt", encrypt, Encoding.Default);

                                fillFarmer();

                                command.Parameters.Clear();

                                command.CommandText = "INSERT into fill (uuid,date,adding,units) values (@p1,@p2,@p3,@p4)";
                                command.Prepare();
                                command.Parameters.AddWithValue("@p1", uuid);
                                command.Parameters.AddWithValue("@p2", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                                command.Parameters.AddWithValue("@p3", "+" + numericUpDown1.Text);
                                command.Parameters.AddWithValue("@p4", units);
                                command.ExecuteNonQuery();

                                MessageBox.Show("Η Προσθήκη Μονάδων στο USB ολοκληρώθηκε");
                                connection.Close();
                            }

                        }
                }
                    else
                    {
                        MessageBox.Show("Το Serial Number του αγρότη και του Usb δεν ταιριάζουν");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Επιλέξτε έναν αγρότη");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows[0].Cells.Count > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "DELETE FROM arduino WHERE name=@p1";
                command.Prepare();
                command.Parameters.AddWithValue("@p1", dataGridView2.SelectedRows[0].Cells[1].Value.ToString());
                command.ExecuteNonQuery();

                connection.Close();

                run_bg();
                timer1.Stop();
                timer1.Start();

                MessageBox.Show("Ο Σταθμός διεγράφη");
            }
            else
            {
                MessageBox.Show("Επιλέξτε ένα σταθμό");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            run_bg();
            
            timer1.Stop();
            timer1.Start();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM farmer", connection);

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Αγρότη";
            dataGridView1.Columns[1].HeaderText = "Μονάδες";
            dataGridView1.Columns[2].HeaderText = "GUID USB";
            dataGridView1.Columns[3].HeaderText = "Όνομα";
            dataGridView1.Columns[4].HeaderText = "Επίθετο";
            dataGridView1.Columns[5].HeaderText = "Κινητό Τηλέφωνο";
            dataGridView1.Columns[6].HeaderText = "Τηλέφωνο Οικίας";
            dataGridView1.Columns[7].HeaderText = "Διεύθυνση";
            dataGridView1.Columns[8].HeaderText = "Email";
            dataGridView1.Columns[9].HeaderText = "ΑΦΜ";
            dataGridView1.Columns[10].HeaderText = "ΔΟΥ";

            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;

            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM events ORDER BY event_id DESC", connection);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader reader;

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            DataTable dtCloned = dataTable.Clone();
            dtCloned.Columns[1].DataType = typeof(String);
            
            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                command.CommandText = "SELECT * FROM farmer where uuid='" + dtCloned.Rows[i].ItemArray[1].ToString() + "'";
                command.Prepare();
                reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    dtCloned.Rows[i][1] = Convert.ToString(reader["name"].ToString() + " " + reader["surname"].ToString());  
                }

                reader.Close();

                command.CommandText = "SELECT * FROM action where type='" + dtCloned.Rows[i].ItemArray[4].ToString() + "'";
                command.Prepare();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dtCloned.Rows[i][4] = Convert.ToString(reader["action"].ToString());
                }

                reader.Close();
            }

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dtCloned;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Γεγονότος";
            dataGridView1.Columns[1].HeaderText = "Όνομα Αγρότη";
            dataGridView1.Columns[2].HeaderText = "UUID USB";
            dataGridView1.Columns[3].HeaderText = "Ημερομηνία";
            dataGridView1.Columns[4].HeaderText = "Ενέργεια";
            dataGridView1.Columns[5].HeaderText = "Ρευματοδότης";
            dataGridView1.Columns[6].HeaderText = "Σταθμός";
            dataGridView1.Columns[7].HeaderText = "Μονάδες";
            dataGridView1.Columns[8].HeaderText = "Υπόλοιπο Μονάδων";

            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";

            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.ReadOnly = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM events ORDER BY event_id DESC", connection);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader reader;

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            DataTable dtCloned = dataTable.Clone();
            dtCloned.Columns[1].DataType = typeof(String);
            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                command.CommandText = "SELECT * FROM farmer where uuid='" + dtCloned.Rows[i].ItemArray[1].ToString() + "'";
                command.Prepare();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dtCloned.Rows[i][1] = Convert.ToString(reader["name"].ToString() + " " + reader["surname"].ToString());
                }

                reader.Close();

                command.CommandText = "SELECT * FROM action where type='" + dtCloned.Rows[i].ItemArray[4].ToString() + "'";
                command.Prepare();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dtCloned.Rows[i][4] = Convert.ToString(reader["action"].ToString());
                }

                reader.Close();
            }

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dtCloned;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Γεγονότος";
            dataGridView1.Columns[1].HeaderText = "Όνομα Αγρότη";
            dataGridView1.Columns[2].HeaderText = "UUID USB";
            dataGridView1.Columns[3].HeaderText = "Ημερομηνία";
            dataGridView1.Columns[4].HeaderText = "Ενέργεια";
            dataGridView1.Columns[5].HeaderText = "Ρευματοδότης";
            dataGridView1.Columns[6].HeaderText = "Σταθμός";
            dataGridView1.Columns[7].HeaderText = "Μονάδες";
            dataGridView1.Columns[8].HeaderText = "Υπόλοιπο Μονάδων";

            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";

            string word = toolStripTextBox1.Text;
            char[] letters = word.ToCharArray();
            char[] letters1 = word.ToCharArray();


            for (int i = 0; i <= letters.Length - 1; i++)
            {
                switch (letters[i])
                {
                    case 'a':
                        letters1[i] = 'α';
                        break;
                    case 'b':
                        letters1[i] = 'β';
                        break;
                    case 'c':
                        letters1[i] = 'ψ';
                        break;
                    case 'd':
                        letters1[i] = 'δ';
                        break;
                    case 'e':
                        letters1[i] = 'ε';
                        break;
                    case 'f':
                        letters1[i] = 'φ';
                        break;
                    case 'g':
                        letters1[i] = 'γ';
                        break;
                    case 'h':
                        letters1[i] = 'η';
                        break;
                    case 'i':
                        letters1[i] = 'ι';
                        break;
                    case 'j':
                        letters1[i] = 'ξ';
                        break;
                    case 'k':
                        letters1[i] = 'κ';
                        break;
                    case 'l':
                        letters1[i] = 'λ';
                        break;
                    case 'm':
                        letters1[i] = 'μ';
                        break;
                    case 'n':
                        letters1[i] = 'ν';
                        break;
                    case 'o':
                        letters1[i] = 'ο';
                        break;
                    case 'p':
                        letters1[i] = 'π';
                        break;
                    case 'q':
                        letters1[i] = 'κ';
                        break;
                    case 'r':
                        letters1[i] = 'ρ';
                        break;
                    case 's':
                        letters1[i] = 'σ';
                        break;
                    case 't':
                        letters1[i] = 'τ';
                        break;
                    case 'u':
                        letters1[i] = 'θ';
                        break;
                    case 'v':
                        letters1[i] = 'β';
                        break;
                    case 'w':
                        letters1[i] = 'ω';
                        break;
                    case 'x':
                        letters1[i] = 'χ';
                        break;
                    case 'y':
                        letters1[i] = 'υ';
                        break;
                    case 'z':
                        letters1[i] = 'ζ';
                        break;
                    case 'A':
                        letters1[i] = 'α';
                        break;
                    case 'B':
                        letters1[i] = 'β';
                        break;
                    case 'C':
                        letters1[i] = 'ψ';
                        break;
                    case 'D':
                        letters1[i] = 'δ';
                        break;
                    case 'E':
                        letters1[i] = 'ε';
                        break;
                    case 'F':
                        letters1[i] = 'φ';
                        break;
                    case 'G':
                        letters1[i] = 'γ';
                        break;
                    case 'H':
                        letters1[i] = 'η';
                        break;
                    case 'I':
                        letters1[i] = 'ι';
                        break;
                    case 'J':
                        letters1[i] = 'ξ';
                        break;
                    case 'K':
                        letters1[i] = 'κ';
                        break;
                    case 'L':
                        letters1[i] = 'λ';
                        break;
                    case 'M':
                        letters1[i] = 'μ';
                        break;
                    case 'N':
                        letters1[i] = 'ν';
                        break;
                    case 'O':
                        letters1[i] = 'ο';
                        break;
                    case 'P':
                        letters1[i] = 'π';
                        break;
                    case 'Q':
                        letters1[i] = 'κ';
                        break;
                    case 'R':
                        letters1[i] = 'ρ';
                        break;
                    case 'S':
                        letters1[i] = 'σ';
                        break;
                    case 'T':
                        letters1[i] = 'τ';
                        break;
                    case 'U':
                        letters1[i] = 'θ';
                        break;
                    case 'V':
                        letters1[i] = 'β';
                        break;
                    case 'W':
                        letters1[i] = 'ω';
                        break;
                    case 'X':
                        letters1[i] = 'χ';
                        break;
                    case 'Y':
                        letters1[i] = 'υ';
                        break;
                    case 'Z':
                        letters1[i] = 'ζ';
                        break;
                    case 'ά':
                        letters1[i] = 'α';
                        break;
                    case 'έ':
                        letters1[i] = 'ε';
                        break;
                    case 'ή':
                        letters1[i] = 'η';
                        break;
                    case 'ύ':
                        letters1[i] = 'υ';
                        break;
                    case 'ί':
                        letters1[i] = 'ι';
                        break;
                    case 'ό':
                        letters1[i] = 'ο';
                        break;
                    case 'ώ':
                        letters1[i] = 'ω';
                        break;
                    case 'Ά':
                        letters1[i] = 'α';
                        break;
                    case 'Έ':
                        letters1[i] = 'ε';
                        break;
                    case 'Ή':
                        letters1[i] = 'η';
                        break;
                    case 'Ύ':
                        letters1[i] = 'υ';
                        break;
                    case 'Ί':
                        letters1[i] = 'ι';
                        break;
                    case 'Ό':
                        letters1[i] = 'ο';
                        break;
                    case 'Ώ':
                        letters1[i] = 'ω';
                        break;
                }
            }
            word = string.Join("", letters1);

            DataRow[] foundRows = dtCloned.Select("uuid LIKE '%" + toolStripTextBox1.Text + "%' OR uuid LIKE '%" + word + "%' OR usb LIKE '%" + toolStripTextBox1.Text + "%'");

            DataTable dt = new DataTable();
            if (foundRows.Length != 0)
            {
                dt = foundRows.CopyToDataTable();

                DataRow tempRow = null;

                int sum = 0;

                foreach (DataRow tempRow_Variable in dt.Rows)
                {
                    tempRow = tempRow_Variable;
                    sum += Convert.ToInt32(tempRow[7]);
                }

                DataRow row = (DataRow)dt.NewRow();

                row[7] = sum;

                dt.Rows.Add(row);

                dataGridView1.DataSource = dt;
                panel1.Visible = true;
                panel2.Visible = false;
                dataGridView1.Visible = true;
                dataGridView1.ReadOnly = true;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
                MessageBox.Show("Δεν βρέθηκε αποτέλεσμα");
            }

            connection.Close();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                connection.Open();
                SqlCommand command1 = new SqlCommand("SELECT * FROM events ORDER BY event_id DESC", connection);
                SqlCommand command = connection.CreateCommand();
                SqlDataReader reader;

                DataTable dataTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command1);

                da.Fill(dataTable);

                DataTable dtCloned = dataTable.Clone();
                dtCloned.Columns[1].DataType = typeof(String);
                foreach (DataRow row in dataTable.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    command.CommandText = "SELECT * FROM farmer where uuid='" + dtCloned.Rows[i].ItemArray[1].ToString() + "'";
                    command.Prepare();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        dtCloned.Rows[i][1] = Convert.ToString(reader["name"].ToString() + " " + reader["surname"].ToString());
                    }

                    reader.Close();

                    command.CommandText = "SELECT * FROM action where type='" + dtCloned.Rows[i].ItemArray[4].ToString() + "'";
                    command.Prepare();
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        dtCloned.Rows[i][4] = Convert.ToString(reader["action"].ToString());
                    }

                    reader.Close();
                }

                dataGridView1.DataSource = null;

                dataGridView1.DataSource = dtCloned;

                dataGridView1.Columns[0].HeaderText = "Κωδικός Γεγονότος";
                dataGridView1.Columns[1].HeaderText = "Όνομα Αγρότη";
                dataGridView1.Columns[2].HeaderText = "UUID USB";
                dataGridView1.Columns[3].HeaderText = "Ημερομηνία";
                dataGridView1.Columns[4].HeaderText = "Ενέργεια";
                dataGridView1.Columns[5].HeaderText = "Ρευματοδότης";
                dataGridView1.Columns[6].HeaderText = "Σταθμός";
                dataGridView1.Columns[7].HeaderText = "Μονάδες";
                dataGridView1.Columns[8].HeaderText = "Υπόλοιπο Μονάδων";

                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";

                string word = toolStripTextBox1.Text;
                char[] letters = word.ToCharArray();
                char[] letters1 = word.ToCharArray();


                for (int i = 0; i <= letters.Length - 1; i++)
                {
                    switch (letters[i])
                    {
                        case 'a':
                            letters1[i] = 'α';
                            break;
                        case 'b':
                            letters1[i] = 'β';
                            break;
                        case 'c':
                            letters1[i] = 'ψ';
                            break;
                        case 'd':
                            letters1[i] = 'δ';
                            break;
                        case 'e':
                            letters1[i] = 'ε';
                            break;
                        case 'f':
                            letters1[i] = 'φ';
                            break;
                        case 'g':
                            letters1[i] = 'γ';
                            break;
                        case 'h':
                            letters1[i] = 'η';
                            break;
                        case 'i':
                            letters1[i] = 'ι';
                            break;
                        case 'j':
                            letters1[i] = 'ξ';
                            break;
                        case 'k':
                            letters1[i] = 'κ';
                            break;
                        case 'l':
                            letters1[i] = 'λ';
                            break;
                        case 'm':
                            letters1[i] = 'μ';
                            break;
                        case 'n':
                            letters1[i] = 'ν';
                            break;
                        case 'o':
                            letters1[i] = 'ο';
                            break;
                        case 'p':
                            letters1[i] = 'π';
                            break;
                        case 'q':
                            letters1[i] = 'κ';
                            break;
                        case 'r':
                            letters1[i] = 'ρ';
                            break;
                        case 's':
                            letters1[i] = 'σ';
                            break;
                        case 't':
                            letters1[i] = 'τ';
                            break;
                        case 'u':
                            letters1[i] = 'θ';
                            break;
                        case 'v':
                            letters1[i] = 'β';
                            break;
                        case 'w':
                            letters1[i] = 'ω';
                            break;
                        case 'x':
                            letters1[i] = 'χ';
                            break;
                        case 'y':
                            letters1[i] = 'υ';
                            break;
                        case 'z':
                            letters1[i] = 'ζ';
                            break;
                        case 'A':
                            letters1[i] = 'α';
                            break;
                        case 'B':
                            letters1[i] = 'β';
                            break;
                        case 'C':
                            letters1[i] = 'ψ';
                            break;
                        case 'D':
                            letters1[i] = 'δ';
                            break;
                        case 'E':
                            letters1[i] = 'ε';
                            break;
                        case 'F':
                            letters1[i] = 'φ';
                            break;
                        case 'G':
                            letters1[i] = 'γ';
                            break;
                        case 'H':
                            letters1[i] = 'η';
                            break;
                        case 'I':
                            letters1[i] = 'ι';
                            break;
                        case 'J':
                            letters1[i] = 'ξ';
                            break;
                        case 'K':
                            letters1[i] = 'κ';
                            break;
                        case 'L':
                            letters1[i] = 'λ';
                            break;
                        case 'M':
                            letters1[i] = 'μ';
                            break;
                        case 'N':
                            letters1[i] = 'ν';
                            break;
                        case 'O':
                            letters1[i] = 'ο';
                            break;
                        case 'P':
                            letters1[i] = 'π';
                            break;
                        case 'Q':
                            letters1[i] = 'κ';
                            break;
                        case 'R':
                            letters1[i] = 'ρ';
                            break;
                        case 'S':
                            letters1[i] = 'σ';
                            break;
                        case 'T':
                            letters1[i] = 'τ';
                            break;
                        case 'U':
                            letters1[i] = 'θ';
                            break;
                        case 'V':
                            letters1[i] = 'β';
                            break;
                        case 'W':
                            letters1[i] = 'ω';
                            break;
                        case 'X':
                            letters1[i] = 'χ';
                            break;
                        case 'Y':
                            letters1[i] = 'υ';
                            break;
                        case 'Z':
                            letters1[i] = 'ζ';
                            break;
                        case 'ά':
                            letters1[i] = 'α';
                            break;
                        case 'έ':
                            letters1[i] = 'ε';
                            break;
                        case 'ή':
                            letters1[i] = 'η';
                            break;
                        case 'ύ':
                            letters1[i] = 'υ';
                            break;
                        case 'ί':
                            letters1[i] = 'ι';
                            break;
                        case 'ό':
                            letters1[i] = 'ο';
                            break;
                        case 'ώ':
                            letters1[i] = 'ω';
                            break;
                        case 'Ά':
                            letters1[i] = 'α';
                            break;
                        case 'Έ':
                            letters1[i] = 'ε';
                            break;
                        case 'Ή':
                            letters1[i] = 'η';
                            break;
                        case 'Ύ':
                            letters1[i] = 'υ';
                            break;
                        case 'Ί':
                            letters1[i] = 'ι';
                            break;
                        case 'Ό':
                            letters1[i] = 'ο';
                            break;
                        case 'Ώ':
                            letters1[i] = 'ω';
                            break;
                    }
                }
                word = string.Join("", letters1);

                DataRow[] foundRows = dtCloned.Select("uuid LIKE '%" + toolStripTextBox1.Text + "%' OR uuid LIKE '%" + word + "%' OR usb LIKE '%" + toolStripTextBox1.Text + "%'");

                DataTable dt = new DataTable();
                if (foundRows.Length != 0)
                {
                    dt = foundRows.CopyToDataTable();

                    DataRow tempRow = null;

                    int sum = 0;

                    foreach (DataRow tempRow_Variable in dt.Rows)
                    {
                        tempRow = tempRow_Variable;
                        sum += Convert.ToInt32(tempRow[7]);
                    }

                    DataRow row = (DataRow)dt.NewRow();

                    row[7] = sum;

                    dt.Rows.Add(row);

                    dataGridView1.DataSource = dt;
                    panel1.Visible = true;
                    panel2.Visible = false;
                    dataGridView1.Visible = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    MessageBox.Show("Δεν βρέθηκε αποτέλεσμα");
                }
                
                connection.Close();
            }
        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value.ToString() == "CONNECTED")
                {
                    dataGridView2.Rows[i].Cells[0].Style.ForeColor = Color.ForestGreen;
                }
                else
                {
                    dataGridView2.Rows[i].Cells[0].Style.ForeColor = Color.DarkRed;
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void usb_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                WebApplication1.Class1.USBSerialNumber usb1 = new WebApplication1.Class1.USBSerialNumber();
                string path = folderBrowserDialog1.SelectedPath;
                string serial = usb1.getSerialNumberFromDriveLetter(path.Remove(path.Length - 1));

                if (serial != null)
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    SqlDataReader reader;

                    command.CommandText = "SELECT * FROM farmer where usb=@p3";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p3", serial);
                    reader = command.ExecuteReader();

                    File.Create(path + "farmer.txt").Close();

                    if (reader.Read())
                    {
                        MessageBox.Show("Το συγκεκριμένο Usb χρησιμοποιείται από άλλο αγρότη");
                        File.Delete(path + "farmer.txt");
                    }
                    else
                    {
                        usb.Text = serial;
                    }

                    reader.Close();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Η διαδρομή που επιλέξατε δεν είναι σωστή");
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();

            int currentrow = e.RowIndex;

            command.CommandText = "UPDATE farmer SET usb=@p1, name=@p2, surname=@p3, MobilePhone=@p4, HomePhone=@p5, HomeStreet=@p6, email=@p7, afm=@p8, doy=@p9 WHERE uuid=@p10";
            command.Prepare();
            command.Parameters.AddWithValue("@p1", dataGridView1.Rows[currentrow].Cells[2].Value.ToString());
            command.Parameters.AddWithValue("@p2", dataGridView1.Rows[currentrow].Cells[3].Value.ToString());
            command.Parameters.AddWithValue("@p3", dataGridView1.Rows[currentrow].Cells[4].Value.ToString());
            command.Parameters.AddWithValue("@p4", dataGridView1.Rows[currentrow].Cells[5].Value.ToString());
            command.Parameters.AddWithValue("@p5", dataGridView1.Rows[currentrow].Cells[6].Value.ToString());
            command.Parameters.AddWithValue("@p6", dataGridView1.Rows[currentrow].Cells[7].Value.ToString());
            command.Parameters.AddWithValue("@p7", dataGridView1.Rows[currentrow].Cells[8].Value.ToString());
            command.Parameters.AddWithValue("@p8", dataGridView1.Rows[currentrow].Cells[9].Value.ToString());
            command.Parameters.AddWithValue("@p9", dataGridView1.Rows[currentrow].Cells[10].Value.ToString());
            command.Parameters.AddWithValue("@p10", dataGridView1.Rows[currentrow].Cells[0].Value.ToString());
            command.ExecuteNonQuery();

            connection.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM fill", connection);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader reader;

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command1);

            da.Fill(dataTable);

            DataTable dtCloned = dataTable.Clone();
            dtCloned.Columns[1].DataType = typeof(String);
            
            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                command.CommandText = "SELECT * FROM farmer where uuid='" + dtCloned.Rows[i].ItemArray[1].ToString() + "'";
                command.Prepare();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    dtCloned.Rows[i][1] = Convert.ToString(reader["name"].ToString() + " " + reader["surname"].ToString());
                }

                reader.Close();

                //dtCloned.Rows[i][2] = Convert.ToDateTime(dtCloned.Rows[i].ItemArray[2]).ToString("dd-MM-yyyy HH:mm:ss");
            }

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dtCloned;

            dataGridView1.Columns[0].HeaderText = "Κωδικός Γεμίσματος";
            dataGridView1.Columns[1].HeaderText = "Όνομα Αγρότη";
            dataGridView1.Columns[2].HeaderText = "Ημερομηνία";
            dataGridView1.Columns[3].HeaderText = "Προστιθέμενες Μονάδες";
            dataGridView1.Columns[4].HeaderText = "Συνολικές Μονάδες";

            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";

            dataGridView1.ReadOnly = true;

            panel1.Visible = true;
            panel2.Visible = false;
            dataGridView1.Visible = true;
        }
    }
}
