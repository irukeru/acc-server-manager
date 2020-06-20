using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ACC_Server_Manager_for_Ankarada_Icmece
{
    public partial class Form1 : Form
    {
        private String CurrentConfiguratonPath = "\\cfg\\current\\configuration.txt";

        private String documentsDirectoryPath = "";
        private String accConfigurationDirectoryPath = "";

        private Settings accServerSettings = new Settings();

        public Form1()
        {

            documentsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            accConfigurationDirectoryPath = documentsDirectoryPath + "\\ACCServerManager";

            if (!Directory.Exists(accConfigurationDirectoryPath))
            {

                DialogResult dialogResult = MessageBox.Show("Before start ACC Server Manager, you need to set ACC Server Directory Path !",
                                            "Important Question",
                                            MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    Environment.Exit(0);
                }

                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        accServerSettings.AccServerPath = fbd.SelectedPath;
                    } else
                    {
                        Environment.Exit(0);
                    }
                }

                Directory.CreateDirectory(accConfigurationDirectoryPath);

                this.saveSettingsToFile(accServerSettings, accConfigurationDirectoryPath);
            } else
            {
                this.accServerSettings = this.getSettingsFromFile(accConfigurationDirectoryPath);
            }

            InitializeComponent();
        }

        private void saveServerConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            String serverName = this.serverNameTextBox.Text;

            String serverPath = accConfigurationDirectoryPath + "\\" + serverName;

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            serverPath = serverPath + "\\" + serverName + ".json";

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(serverPath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            } catch (Exception ex)
            {
                Console.Out.WriteLine(ex);
            }

        }

        private void applyServerConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(this.getInterfaceGeneralConfiguration());
            File.WriteAllText(this.accServerSettings.AccServerPath + this.CurrentConfiguratonPath, jsonString);
        }

        private void saveSettingsToFile(Settings settingsObject, String directoryPath)
        {
            string jsonString = JsonConvert.SerializeObject(settingsObject);
            File.WriteAllText(directoryPath + "\\accServerManagerSettings.json", jsonString);
        }

        private Settings getSettingsFromFile(String directoryPath)
        {
            String jsonString = File.ReadAllText(directoryPath + "\\accServerManagerSettings.json");

            return JsonConvert.DeserializeObject<Settings>(jsonString);
        }

        private GeneralConfiguration getInterfaceGeneralConfiguration()
        {
            GeneralConfiguration generalConfigurationObj = new GeneralConfiguration();

            generalConfigurationObj.udpPort = Int32.Parse(this.udpPortTextBox.Text);
            generalConfigurationObj.tcpPort = Int32.Parse(this.tcpPortTextBox.Text);
            generalConfigurationObj.maxConnections = Int32.Parse(this.maxConnectionsTextBox.Text);
            generalConfigurationObj.registerToLobby = this.registerToLobbyCheckBox.Checked ? 1 : 0;

            return generalConfigurationObj;
        }

    }
}
