using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Management;

namespace WebApplication1
{
    public class Class1
    {
        public static byte[] key = Encoding.Default.GetBytes("Σ•~@2®1W’ƒ§9h―8‚ώµδΘZ`U³Sf>");
        public static byte[] vector = Encoding.Default.GetBytes("¨©ι™³ώRώ");

        public static void arduino_txtcheck_all()
        {
            string com = "Select * from arduino";
            SqlDataAdapter adpt5 = new SqlDataAdapter(com, sqlstring);
            DataSet myDataSet5 = new DataSet();
            adpt5.Fill(myDataSet5, "arduino");
            DataTable myDataTable5 = myDataSet5.Tables[0];
            DataRow tempRow5 = null;

            foreach (DataRow tempRow5_Variable in myDataTable5.Rows)
            {
                tempRow5 = tempRow5_Variable;

                try
                {
                    txt_checkwrite(tempRow5["ip"].ToString());
                }
                catch
                {

                }
            }
        }

        public static void txt_checkwrite(string ip)
        {
            SqlConnection connection = new SqlConnection(sqlstring);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlDataReader reader;
            
            int check1 = 0;
            int check2 = 0;
            int check3 = 0;
            int check4 = 0;
            foreach (var line in File.ReadLines(@"\\" + ip + @"\PiShare\events.txt",Encoding.Default).Reverse())
            {
                command.Parameters.Clear();

                string[] words = line.Split(',');

                command.CommandText = "SELECT * FROM events where uuid=@p1 AND time=@p2";

                //command.Parameters.AddWithValue("@p2", words[2].ToString());
                //command.Parameters.Add("@p2", SqlDbType.NVarChar, 50).Value = words[2].ToString();

                command.Parameters.Add("@p1", SqlDbType.Int);
                command.Parameters["@p1"].Value = Convert.ToInt32(words[0].ToString());

                //command.Parameters.Add("@p2", SqlDbType.NVarChar , 50);
                //command.Parameters["@p2"].Value = words[2];

                //command.Parameters.AddWithValue("@p2", words[2].ToString());

                command.Parameters.Add("@p2", SqlDbType.DateTime);
                command.Parameters["@p2"].Value = Convert.ToDateTime(words[2].ToString());

                command.Prepare();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    break;
                }
                else
                {
                    reader.Close();
                    command.Parameters.Clear();

                    command.CommandText = "SELECT * FROM farmer where uuid=@p1";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p1", words[0]);
                    reader = command.ExecuteReader();

                    string farmer_name = "";
                    string farmer_surname = "";
                    if (reader.Read())
                    {
                        farmer_name = reader["name"].ToString();
                        farmer_surname = reader["surname"].ToString();
                    }

                    reader.Close();

                    command.Parameters.Clear();

                    command.CommandText = "SELECT * FROM arduino where ip=@p1";
                    command.Prepare();

                    command.Parameters.AddWithValue("@p1", ip);
                    reader = command.ExecuteReader();

                    string station_name = "";
                   
                    if (reader.Read())
                    {
                        station_name = reader["name"].ToString();
                    }

                    reader.Close();

                    command.Parameters.Clear();

                    command.CommandText = "INSERT into events (uuid,usb,time,action,connector,arduino,units,units_remainder) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)";
                    command.Prepare();
                    command.Parameters.AddWithValue("@p1", words[0]);
                    command.Parameters.AddWithValue("@p2", words[1]);
                    command.Parameters.AddWithValue("@p3", words[2]);
                    command.Parameters.AddWithValue("@p4", words[3]);
                    command.Parameters.AddWithValue("@p5", words[4]);
                    command.Parameters.AddWithValue("@p6", station_name);
                    command.Parameters.AddWithValue("@p7", words[5]);
                    command.Parameters.AddWithValue("@p8", words[6]);
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();

                    command.CommandText = "SELECT * FROM events where uuid=@p1 AND time>@p2";
                    command.Prepare();

                    command.Parameters.Add("@p1", SqlDbType.Int);
                    command.Parameters["@p1"].Value = Convert.ToInt32(words[0].ToString());

                    command.Parameters.Add("@p2", SqlDbType.DateTime);
                    command.Parameters["@p2"].Value = Convert.ToDateTime(words[2].ToString());

                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {

                    }
                    else
                    {
                        reader.Close();
                        command.Parameters.Clear();

                        command.CommandText = "UPDATE farmer SET units=@p1 WHERE uuid=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", words[6]);
                        command.Parameters.AddWithValue("@p2", words[0]);
                        command.ExecuteNonQuery();
                    }

                    reader.Close();

                    command.Parameters.Clear();

                    if (words[4] == "1" && check1 == 0)
                    {
                        command.CommandText = "UPDATE arduino SET connector1=@p1 WHERE ip=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", farmer_name + " " + farmer_surname);
                        command.Parameters.AddWithValue("@p2", ip);
                        command.ExecuteNonQuery();

                        check1 = 1;
                    }else if (words[4] == "2" && check2 == 0)
                    {
                        command.CommandText = "UPDATE arduino SET connector2=@p1 WHERE ip=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", farmer_name + " " + farmer_surname);
                        command.Parameters.AddWithValue("@p2", ip);
                        command.ExecuteNonQuery();

                        check2 = 1;
                    }
                    else if (words[4] == "3" && check3 == 0)
                    {
                        command.CommandText = "UPDATE arduino SET connector3=@p1 WHERE ip=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", farmer_name + " " + farmer_surname);
                        command.Parameters.AddWithValue("@p2", ip);
                        command.ExecuteNonQuery();

                        check3 = 1;
                    }
                    else if (words[4] == "4" && check4 == 0)
                    {
                        command.CommandText = "UPDATE arduino SET connector4=@p1 WHERE ip=@p2";
                        command.Prepare();
                        command.Parameters.AddWithValue("@p1", farmer_name + " " + farmer_surname);
                        command.Parameters.AddWithValue("@p2", ip);
                        command.ExecuteNonQuery();

                        check4 = 1;
                    }

                    Array.Clear(words, 0, words.Length);
                    
                    command.Parameters.Clear();
                    
                }

                reader.Close();
            }

            connection.Close();

            //using (StreamWriter writer = new StreamWriter(@"\\" + ip + @"\arduino\www\format.txt"))
            //{
            //    foreach (var line in lines)
            //    {
            //        char[] first = line.ToCharArray();

            //        if (!first[0].ToString().Equals("#"))
            //        {
            //            writer.WriteLine("#" + line);
            //        }
            //        else
            //        {
            //            writer.WriteLine(line);
            //        }
            //    }
            //}
        }

        public static string XORENC(string input)
        {
            char[] key = { 'K', 'C', 'Q' }; 
            char[] output = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            return new string(output);
        }

        //public static char[] XORENC(char[] word, char[] key)
        //{
        //    uint combine;
        //    int insize = word.Length;
        //    int keysize = key.Length;
        //    for (int x = 0; x < insize; x++)
        //    {
        //        for (int i = 0; i < keysize; i++)
        //        {
        //            uint textchar = (uint)word[x];
        //            uint keyCode = (uint)key[i];
        //            combine = textchar ^ keyCode;
        //            word[x] = (char)combine;
        //        }
        //    }
        //    return word;
        //}

        public static string txt_reading()
        {
            //string appPath = HttpRuntime.AppDomainAppPath + @"\sql.txt";
            string appPath = @"c:\Users\" + Environment.UserName + @"\Desktop\sql.txt";
            List<string> lines = new List<string>();

            using (StreamReader r = new StreamReader(appPath, Encoding.Default))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            string sqltext = "";
            foreach (string s in lines)
            {
                //string[] words = s;

                sqltext = s.Trim();

                //words[0] = "";
                //words[1] = "";
            }

            return sqltext;
        }

        public static string sqlstringtext()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\sql.txt";
            //string appPath = @"c:\Users\" + Environment.UserName + @"\Desktop\sql.txt";
            List<string> lines = new List<string>();

            using (StreamReader r = new StreamReader(appPath, Encoding.Default))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            string sqltext = "";
            foreach (string s in lines)
            {
                //string[] words = s;

                sqltext = s.Trim();

                //words[0] = "";
                //words[1] = "";
            }

            return sqltext;
        }

        public static string sqlstring = sqlstringtext();

        public class USBSerialNumber
        {

            string _serialNumber;
            string _driveLetter;

            public string getSerialNumberFromDriveLetter(string driveLetter)
            {
                this._driveLetter = driveLetter.ToUpper();

                if (!this._driveLetter.Contains(":"))
                {
                    this._driveLetter += ":";
                }

                matchDriveLetterWithSerial();

                return this._serialNumber;
            }

            private void matchDriveLetterWithSerial()
            {

                string[] diskArray;
                string driveNumber;
                string driveLetter;

                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
                foreach (ManagementObject dm in searcher1.Get())
                {
                    diskArray = null;
                    driveLetter = getValueInQuotes(dm["Dependent"].ToString());
                    diskArray = getValueInQuotes(dm["Antecedent"].ToString()).Split(',');
                    driveNumber = diskArray[0].Remove(0, 6).Trim();
                    if (driveLetter == this._driveLetter)
                    {
                        /* This is where we get the drive serial */
                        ManagementObjectSearcher disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                        foreach (ManagementObject disk in disks.Get())
                        {

                            if (disk["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveNumber) & disk["InterfaceType"].ToString() == "USB")
                            {
                                this._serialNumber = parseSerialFromDeviceID(disk["PNPDeviceID"].ToString());
                            }
                        }
                    }
                }
            }

            private string parseSerialFromDeviceID(string deviceId)
            {
                string[] splitDeviceId = deviceId.Split('\\');
                string[] serialArray;
                string serial;
                int arrayLen = splitDeviceId.Length - 1;

                serialArray = splitDeviceId[arrayLen].Split('&');
                serial = serialArray[0];

                return serial;
            }

            private string getValueInQuotes(string inValue)
            {
                string parsedValue = "";

                int posFoundStart = 0;
                int posFoundEnd = 0;

                posFoundStart = inValue.IndexOf("\"");
                posFoundEnd = inValue.IndexOf("\"", posFoundStart + 1);

                parsedValue = inValue.Substring(posFoundStart + 1, (posFoundEnd - posFoundStart) - 1);

                return parsedValue;
            }
        }

        public static void encrypt(string words, string path)
        {
            byte[] encrypted = EncryptStringToBytes_Aes(words, key, vector);

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine(words);
                file.Close();
            }

            using (System.IO.StreamReader file1 = new System.IO.StreamReader(@"C:\Users\George\Desktop\encrypt.txt", true))
            {
                byte[] key = Encoding.Default.GetBytes(File.ReadLines(@"C:\Users\George\Desktop\encrypt.txt").Skip(1).Take(1).First().Trim());
                byte[] vector = Encoding.Default.GetBytes(File.ReadLines(@"C:\Users\George\Desktop\encrypt.txt").Skip(2).Take(1).First().Trim());
            }
        }

        public static string EncryptOrDecryptXOR(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                // take next character from string
                char character = text[c];

                // cast to a uint
                uint charCode = (uint)character;

                // figure out which character to take from the key
                int keyPosition = c % key.Length; // use modulo to "wrap round"

                // take the key character
                char keyChar = key[keyPosition];

                // cast it to a uint also
                uint keyCode = (uint)keyChar;

                // perform XOR on the two character codes
                uint combinedCode = charCode ^ keyCode;

                // cast back to a char
                char combinedChar = (char)combinedCode;

                // add to the result
                result.Append(combinedChar);
            }

            return result.ToString();
        }

        public static string decrypt()
        {
            byte[] encrypted = Encoding.Default.GetBytes(File.ReadLines(@"C:\Users\George\Desktop\encrypt.txt", Encoding.Default).First().Trim());
            string roundtrip = DecryptStringFromBytes_Aes(encrypted, key, vector);

            return roundtrip;
        }

        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}