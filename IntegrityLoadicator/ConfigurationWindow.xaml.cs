using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Xml;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Xml.Linq;


namespace ZebecLoadMaster
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        private SqlConnection conn;
        private SqlCommand command;
        private SqlDataReader reader;
        string sql = "";
        string connectionstring = "";
        string connectionstring1 = "";
        private XmlDocument doc;
        string pwdCheck;
        string password;
        string user;
        private string PATH = System.IO.Directory.GetCurrentDirectory() + @"\Settings\StabilityConfig.xml";
        public ConfigurationWindow()
        {
            InitializeComponent();
           
            instances();
        }
        public void instances()
        {
            try
            {
                btnCreate.IsEnabled = false;
                RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
                {
                    //try
                    //{
                    //    RegistryKey rk = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                    //    if (rk != null)
                    //    {
                    //        String[] instances = (String[])rk.GetValue("InstalledInstances");
                    //        //String[] instances = (String[])rk.GetValueNames();
                    //        if (instances.Length > 0)
                    //        {
                    //            foreach (String element in instances)
                    //            {
                    //                if (element == "MSSQLSERVER")

                    //                     MainWindow._servername= System.Environment.MachineName;
                    //                else
                    //                 MainWindow._servername = System.Environment.MachineName + @"\" + element; //For Other System
                    //                   // MainWindow._servername = System.Environment.MachineName;   //For Sangita System
                    //            }
                    //        }
                    //        rk.Close();
                    //    }
                    //}
                    //catch
                    //{

                    //}
                    try
                    {
                        RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Microsoft SQL Server\Instance Names\SQL");
                        if (key != null)
                        {
                            String[] instances = (String[])key.GetValueNames();

                            if (instances.Length > 0)
                            {
                                foreach (String element in instances)
                                {
                                    // MessageBox.Show(element);
                                    if (element == "MSSQLSERVER")
                                        //if (element == "SQLEXPRESS")
                                        listBoxSQLInstances.Items.Add(System.Environment.MachineName);
                                    else
                                        listBoxSQLInstances.Items.Add(System.Environment.MachineName + @"\" + element);
                                }
                            }
                            key.Close();
                        }
                        hklm.Close();
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //XmlDocument doc = new XmlDocument();
                        //XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                        //doc.AppendChild(declaration);
                        //XmlComment comment = doc.CreateComment("This is an XML Generated File");
                        //doc.AppendChild(comment);
                        //XmlElement root = doc.CreateElement("Settings");
                        //doc.AppendChild(root);
                        //XmlElement ServerName = doc.CreateElement("ServerName");
                        //ServerName.InnerText = MainWindow._servername;
                        //root.AppendChild(ServerName);
                        //doc.Save(PATH);
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        XmlDocument doc = new XmlDocument();
                        //if (!System.IO.File.Exists(PATH))
                        //{
                        //Create neccessary nodes
                        XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                        doc.AppendChild(declaration);
                        XmlComment comment = doc.CreateComment("This is an XML Generated File");
                        doc.AppendChild(comment);
                        XmlElement root = doc.CreateElement("Settings");
                        doc.AppendChild(root);
                        XmlElement ServerName = doc.CreateElement("ServerName");
                        ServerName.InnerText = MainWindow._servername;
                        root.AppendChild(ServerName);

                        XmlElement ModbusType = doc.CreateElement("MODBUSTYPE");
                        ModbusType.InnerText = "0";
                        root.AppendChild(ModbusType);

                        XmlElement ComPort = doc.CreateElement("PORT");
                        ComPort.InnerText = "COM3";
                        root.AppendChild(ComPort);

                        XmlElement Computers = doc.CreateElement("Computers");
                        root.AppendChild(Computers);

                        //XmlElement Computers = doc.CreateElement("Computers");
                        XmlElement Computer = doc.CreateElement("Computer");
                        Computers.AppendChild(Computer);

                        XmlAttribute ComputerName = doc.CreateAttribute("ComputerName");
                        ComputerName.InnerText = "Zebec";
                        Computer.Attributes.Append(ComputerName);

                        XmlAttribute DataBaseName = doc.CreateAttribute("DataBaseName");
                        DataBaseName.InnerText = "SanmarSitar_Stability";
                        Computer.Attributes.Append(DataBaseName);


                        XmlAttribute ComputerIPAddress = doc.CreateAttribute("ComputerIPAddress");
                        ComputerIPAddress.InnerText = "192.168.0.1";
                        Computer.Attributes.Append(ComputerIPAddress);

                        XmlAttribute QueuePathName = doc.CreateAttribute("QueuePathName");
                        QueuePathName.InnerText = @".\Private$\{0}";
                        Computer.Attributes.Append(QueuePathName);


                        XmlAttribute IsReplicationAllowed = doc.CreateAttribute("IsReplicationAllowed");
                        IsReplicationAllowed.InnerText = "TRUE";
                        Computer.Attributes.Append(IsReplicationAllowed);

                        XmlAttribute TimeInterval = doc.CreateAttribute("TimeInterval");
                        TimeInterval.InnerText = "00:00:10";
                        Computer.Attributes.Append(TimeInterval);


                        XmlAttribute MulticastAddress = doc.CreateAttribute("MulticastAddress");
                        MulticastAddress.InnerText = "234.1.1.1:8888";
                        Computer.Attributes.Append(MulticastAddress);

                        //XmlElement Computer2 = doc.CreateElement("Computer");
                        //Computers.AppendChild(Computer2);

                        //XmlAttribute ComputerName2 = doc.CreateAttribute("ComputerName");
                        //ComputerName2.InnerText = "Zebec1";
                        //Computer2.Attributes.Append(ComputerName2);

                        //XmlAttribute DataBaseName2 = doc.CreateAttribute("DataBaseName");
                        //DataBaseName2.InnerText = "SanmarSitar_Stability";
                        //Computer2.Attributes.Append(DataBaseName2);


                        //XmlAttribute ComputerIPAddress2 = doc.CreateAttribute("ComputerIPAddress");
                        //ComputerIPAddress2.InnerText = "192.168.0.2";
                        //Computer2.Attributes.Append(ComputerIPAddress2);

                        //XmlAttribute QueuePathName2 = doc.CreateAttribute("QueuePathName");
                        //QueuePathName2.InnerText = @".\Private$\{0}";
                        //Computer2.Attributes.Append(QueuePathName2);


                        //XmlAttribute IsReplicationAllowed2 = doc.CreateAttribute("IsReplicationAllowed");
                        //IsReplicationAllowed2.InnerText = "FALSE";
                        //Computer2.Attributes.Append(IsReplicationAllowed2);



                        //XmlAttribute TimeInterval2 = doc.CreateAttribute("TimeInterval");
                        //TimeInterval2.InnerText = "00:00:10";
                        //Computer2.Attributes.Append(TimeInterval2);


                        //XmlAttribute MulticastAddress2 = doc.CreateAttribute("MulticastAddress");
                        //MulticastAddress2.InnerText = "234.1.1.1:8888";
                        //Computer2.Attributes.Append(MulticastAddress2);




                        doc.Save(PATH);

                        //MessageBox.Show("All Computers Added to Configuration String");
                    }
                    catch
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup Files(*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == true)
            {
                textBoxRestorePath.Text = dlg.FileName;
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                if (textBoxServerName.Text == "")
                {
                    MessageBox.Show("Please Enter Server Name");
                }
                else if (textBoxRestorePath.Text == "")
                {
                    MessageBox.Show("Please Select Database Path");
                }
                else
                {
                    conn = new SqlConnection(connectionstring1);
                    conn.Open();
                    //sql= "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + cmbDatabase.Text + "') DROP DATABASE " + cmbDatabase.Text + " RESTORE DATABASE " + cmbDatabase.Text + " FROM DISK = '" + txtRestoreFileLocation.Text + "'";
                    sql = "USE Master;";
                    sql += "Alter Database SanmarSitar_Stability Set OFFLINE WITH ROLLBACK IMMEDIATE;";
                    sql += "Restore Database SanmarSitar_Stability FROM Disk = '" + textBoxRestorePath.Text + "' WITH REPLACE;";
                    command = new SqlCommand(sql, conn);
                    command.CommandTimeout = 1000;
                    command.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    MessageBox.Show("Database Successfully Restored");
                    Mouse.OverrideCursor = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
            }

        }

        private void btnConnectionString_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxServerName.Text == "")
                {
                    MessageBox.Show("Please Enter Server Name");
                }
                else if (textBoxUser.Text == "")
                {
                    MessageBox.Show("Please Enter User");
                }
                else if (textBoxPassword.Text == "")
                {
                    MessageBox.Show("Please Enter Password");
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    //if (!System.IO.File.Exists(PATH))
                    //{
                    //Create neccessary nodes
                    XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    doc.AppendChild(declaration);
                    XmlComment comment = doc.CreateComment("This is an XML Generated File");
                    doc.AppendChild(comment);

                    XmlElement root = doc.CreateElement("Settings");
                    doc.AppendChild(root);

                    XmlElement ServerName = doc.CreateElement("ServerName");
                    ServerName.InnerText = textBoxServerName.Text;
                    root.AppendChild(ServerName);

                    //XmlElement User = doc.CreateElement("User");
                    //User.InnerText = textBoxUser.Text;
                    //root.AppendChild(User);

                    //XmlElement Password = doc.CreateElement("Password");
                    //Password.InnerText = textBoxPassword.Text;
                    //root.AppendChild(Password);

                    doc.Save(PATH);

                    MessageBox.Show("Configuration String Created");
                }
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                password = textBoxPassword.Text;
                user = textBoxUser.Text;
                connectionstring1 = "Data Source=" + listBoxSQLInstances.SelectedItem.ToString() + ";Initial Catalog=master;User ID=" + user + ";Password=" + password;
                
                // connectionstring = "Data Source=localhost;Integrated Security=True;";
                // connectionstring = "Data Source=" + listBoxSQLInstances.SelectedItem.ToString() + ";Initial Catalog=SanmarSitar_Stability;User ID=SanmarSitar_Stability;Password=stabilityEtisalat";
                conn = new SqlConnection(connectionstring1);
                conn.Open();
                sql = "EXEC sp_databases";
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();

                listBoxSqlDatabases.Items.Clear();


                while (reader.Read())
                {

                    listBoxSqlDatabases.Items.Add(reader[0].ToString());
                }

                conn.Close();
                conn.Dispose();
                reader.Dispose();
                Mouse.OverrideCursor = null;
                btnCreate.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Mouse.OverrideCursor = null;
            }

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                conn = new SqlConnection(connectionstring1);
                conn.Open();
                sql = "";
                //sql= "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + cmbDatabase.Text + "') DROP DATABASE " + cmbDatabase.Text + " RESTORE DATABASE " + cmbDatabase.Text + " FROM DISK = '" + txtRestoreFileLocation.Text + "'";
                sql = "USE Master;";
                //sql += "Alter Database Stability Set OFFLINE WITH ROLLBACK IMMEDIATE;";
                sql += "Create database SanmarSitar_Stability";
                command = new SqlCommand(sql, conn);
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Mouse.OverrideCursor = null;
            }
        }

        private void listBoxSQLInstances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                textBoxServerName.Text = listBoxSQLInstances.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Please select Instance");
            }

        }

        private void btnMasterLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn = new SqlConnection(connectionstring1);
                conn.Open();
                //sql= "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'" + cmbDatabase.Text + "') DROP DATABASE " + cmbDatabase.Text + " RESTORE DATABASE " + cmbDatabase.Text + " FROM DISK = '" + txtRestoreFileLocation.Text + "'";
                sql = "USE Master;";
                sql += "IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'SanmarSitar_Stability')";
                sql += "DROP LOGIN [SanmarSitar_Stability];";
                sql += "CREATE LOGIN [SanmarSitar_Stability] WITH PASSWORD=N'stabilitySalat', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;";
                sql += "EXEC sys.sp_addsrvrolemember @loginame = N'SanmarSitar_Stability', @rolename = N'sysadmin';";
                sql += "USE Master;";
                sql += "EXEC master.dbo.sp_configure 'show advanced options', 1";
                sql += "RECONFIGURE WITH OVERRIDE;";
                sql += "EXEC master.dbo.sp_configure 'xp_cmdshell', 1";
                sql += "RECONFIGURE WITH OVERRIDE";
                command = new SqlCommand(sql, conn);
                command.CommandTimeout = 180;
                command.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                MessageBox.Show("Master Login Created Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveToXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                //if (!System.IO.File.Exists(PATH))
                //{
                //Create neccessary nodes
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                doc.AppendChild(declaration);
                XmlComment comment = doc.CreateComment("This is an XML Generated File");
                doc.AppendChild(comment);
                XmlElement root = doc.CreateElement("Settings");
                doc.AppendChild(root);

                XmlElement ModbusType = doc.CreateElement("MODBUSTYPE");
                ModbusType.InnerText = "0";
                root.AppendChild(ModbusType);

                XmlElement ComPort = doc.CreateElement("PORT");
                ComPort.InnerText = txtComPort.Text;
                root.AppendChild(ComPort);

                XmlElement ServerName = doc.CreateElement("ServerName");
                ServerName.InnerText = txtServerName.Text;
                root.AppendChild(ServerName);


                XmlElement Computers = doc.CreateElement("Computers");
                root.AppendChild(Computers);

                //XmlElement Computers = doc.CreateElement("Computers");
                XmlElement Computer = doc.CreateElement("Computer");
                Computers.AppendChild(Computer);

                XmlAttribute ComputerName = doc.CreateAttribute("ComputerName");
                ComputerName.InnerText = textBox4.Text;
                Computer.Attributes.Append(ComputerName);

                XmlAttribute DataBaseName = doc.CreateAttribute("DataBaseName");
                DataBaseName.InnerText = "SanmarSitar_Stability";
                Computer.Attributes.Append(DataBaseName);


                XmlAttribute ComputerIPAddress = doc.CreateAttribute("ComputerIPAddress");
                ComputerIPAddress.InnerText = textBox3.Text;
                Computer.Attributes.Append(ComputerIPAddress);

                XmlAttribute QueuePathName = doc.CreateAttribute("QueuePathName");
                QueuePathName.InnerText = @".\Private$\{0}";
                Computer.Attributes.Append(QueuePathName);


                XmlAttribute IsReplicationAllowed = doc.CreateAttribute("IsReplicationAllowed");
                IsReplicationAllowed.InnerText = textBox5.Text;
                Computer.Attributes.Append(IsReplicationAllowed);



                XmlAttribute TimeInterval = doc.CreateAttribute("TimeInterval");
                TimeInterval.InnerText = textBox6.Text;
                Computer.Attributes.Append(TimeInterval);


                XmlAttribute MulticastAddress = doc.CreateAttribute("MulticastAddress");
                MulticastAddress.InnerText = "234.1.1.1:8888";
                Computer.Attributes.Append(MulticastAddress);

                XmlElement Computer2 = doc.CreateElement("Computer");
                Computers.AppendChild(Computer2);


                doc.Save(PATH);

                MessageBox.Show("All Computers Added to Configuration String");
            }
            catch
            {
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var xml1 = XDocument.Load(PATH);

                XmlDocument xml = new XmlDocument();
                // You'll need to put the correct path to your xml file here
                xml.Load(PATH);
                XmlNode typeNode = xml.SelectSingleNode("Settings/MODBUSTYPE");
                // Get its value
               

                XmlNode node1 = xml.SelectSingleNode("Settings/ServerName");
                txtServerName.Text = node1.InnerText;
                XmlNodeList detailNodes = xml.SelectNodes("Settings/Computers");
                foreach (XmlNode detail in detailNodes)
                {
                    string test = detail.InnerXml.ToString();
                    //textBox4.Text = detail.SelectSingleNode("Computer/@ComputerName").InnerText;
                    //textBox6.Text = detail.SelectSingleNode("Computer/@ComputerIPAddress").InnerText;
                    //textBox8.Text = detail.SelectSingleNode("Computer/@IsReplicationAllowed").InnerText;
                    //textBox9.Text = detail.SelectSingleNode("Computer/@TimeInterval").InnerText;

                }

                foreach (XElement element in xml1.Descendants("Settings"))
                {
                    int i = 1;
                    foreach (XElement childEllement in element.Descendants("Computers"))
                    {
                        foreach (var childEl in childEllement.Descendants("Computer"))
                        {
                            if (i == 1)
                            {
                                var ComputerName = childEl.Attributes("ComputerName");

                                foreach (var xAttribute in ComputerName)
                                {
                                    textBox4.Text = xAttribute.Value;
                                }

                                var ComputerIPAddress = childEl.Attributes("ComputerIPAddress");

                                foreach (var xAttribute in ComputerIPAddress)
                                {
                                    textBox3.Text = xAttribute.Value;
                                }
                                var IsReplicationAllowed = childEl.Attributes("IsReplicationAllowed");

                                foreach (var xAttribute in IsReplicationAllowed)
                                {
                                    textBox5.Text = xAttribute.Value;
                                }
                                var TimeInterval = childEl.Attributes("TimeInterval");

                                foreach (var xAttribute in TimeInterval)
                                {
                                    textBox6.Text = xAttribute.Value;
                                }
                                //string text = childEl.FirstNode().("ComputerName") ;
                                //Console.WriteLine(childEl);
                                //Console.WriteLine(i);
                            }
                            if (i == 2)
                            {
                                var ComputerName = childEl.Attributes("ComputerName");

                  

                                var ComputerIPAddress = childEl.Attributes("ComputerIPAddress");

                              
                                var TimeInterval = childEl.Attributes("TimeInterval");

                              
                                //string text = childEl.FirstNode().("ComputerName") ;
                                //Console.WriteLine(childEl);
                                //Console.WriteLine(i);
                            }
                            if (i == 3)
                            {
                                var ComputerName = childEl.Attributes("ComputerName");

  

                                var ComputerIPAddress = childEl.Attributes("ComputerIPAddress");

                            
                                var IsReplicationAllowed = childEl.Attributes("IsReplicationAllowed");

                                var TimeInterval = childEl.Attributes("TimeInterval");

                                //string text = childEl.FirstNode().("ComputerName") ;
                                //Console.WriteLine(childEl);
                                //Console.WriteLine(i);
                            }
                            if (i == 4)
                            {
                                var ComputerName = childEl.Attributes("ComputerName");

                   
                                var ComputerIPAddress = childEl.Attributes("ComputerIPAddress");

                        
                                var IsReplicationAllowed = childEl.Attributes("IsReplicationAllowed");

                                var TimeInterval = childEl.Attributes("TimeInterval");

                            
                                //string text = childEl.FirstNode().("ComputerName") ;
                                //Console.WriteLine(childEl);
                                //Console.WriteLine(i);
                            }
                            if (i == 5)
                            {
                                var ComputerName = childEl.Attributes("ComputerName");

                     

                                var ComputerIPAddress = childEl.Attributes("ComputerIPAddress");

                          
                                var IsReplicationAllowed = childEl.Attributes("IsReplicationAllowed");

                        
                                var TimeInterval = childEl.Attributes("TimeInterval");

                           
                                //string text = childEl.FirstNode().("ComputerName") ;
                                //Console.WriteLine(childEl);
                                //Console.WriteLine(i);
                            }
                            i++;
                        }
                    }
                }
            }

            catch
            {
            }
        }
    }
}
