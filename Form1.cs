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
        private String ConfiguratonPath = "\\cfg\\configuration.json";
        private String SettingsPath = "\\cfg\\settings.json";
        private String EventPath = "\\cfg\\event.json";

        private String documentsDirectoryPath = "";
        private String accConfigurationDirectoryPath = "";

        private ACCServerManagerSettings accServerSettings = new ACCServerManagerSettings();
        private GeneralConfiguration generalConfiguration = new GeneralConfiguration();
        private Settings settings = new Settings();
        private Event events = new Event();

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

            this.generalConfiguration = this.LoadGeneralConfigurationObject();
            this.LoadGeneralConfiguration(this.generalConfiguration);

            this.settings = this.LoadSettingsObject();
            this.LoadSettings(this.settings);

            this.events = this.LoadEventObject();
            this.LoadEvent(this.events);
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
            if (this.trackMedalsRequirementComboBox.Text.Length == 0)
            {
                MessageBox.Show("Please select Track Medals Requirement Box");
                return;
            }

            if (Int32.Parse(this.preRaceWaitingTimeSecondsTextBox.Text) < 30)
            {
                MessageBox.Show("Pre Race Waiting Time Seconds can not be less then 30 seconds !");
                return;
            }

            // save configurations
            string jsonString = JsonConvert.SerializeObject(this.getInterfaceGeneralConfiguration());
            File.WriteAllText(this.accServerSettings.AccServerPath + this.ConfiguratonPath, jsonString);

            // save settings
            string settingsJsonString = JsonConvert.SerializeObject(this.getInterfaceSettings());
            File.WriteAllText(this.accServerSettings.AccServerPath + this.SettingsPath, settingsJsonString);

            // save Event
            string eventJsonString = JsonConvert.SerializeObject(this.getInterfaceEvent());
            File.WriteAllText(this.accServerSettings.AccServerPath + this.EventPath, eventJsonString);
        }

        private void saveSettingsToFile(ACCServerManagerSettings settingsObject, String directoryPath)
        {
            string jsonString = JsonConvert.SerializeObject(settingsObject);
            File.WriteAllText(directoryPath + "\\accServerManagerSettings.json", jsonString);
        }

        private ACCServerManagerSettings getSettingsFromFile(String directoryPath)
        {
            String jsonString = File.ReadAllText(directoryPath + "\\accServerManagerSettings.json");

            return JsonConvert.DeserializeObject<ACCServerManagerSettings>(jsonString);
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

        private GeneralConfiguration LoadGeneralConfigurationObject()
        {
            String jsonString = File.ReadAllText(this.accServerSettings.AccServerPath + this.ConfiguratonPath);
            return JsonConvert.DeserializeObject<GeneralConfiguration>(jsonString);
        }

        private Settings LoadSettingsObject()
        {
            String jsonString = File.ReadAllText(this.accServerSettings.AccServerPath + this.SettingsPath);
            return JsonConvert.DeserializeObject<Settings>(jsonString);
        }

        private Event LoadEventObject()
        {
            String jsonString = File.ReadAllText(this.accServerSettings.AccServerPath + this.EventPath);
            return JsonConvert.DeserializeObject<Event>(jsonString);
        }

        private void LoadGeneralConfiguration(GeneralConfiguration configuration)
        {
            this.udpPortTextBox.Text = Convert.ToString(configuration.udpPort);
            this.tcpPortTextBox.Text = Convert.ToString(configuration.tcpPort);
            this.maxConnectionsTextBox.Text = Convert.ToString(configuration.maxConnections);

        }

        private Settings getInterfaceSettings()
        {
            Settings settingsObj = new Settings();

            settingsObj.serverName = this.serverNameTextBox.Text;
            settingsObj.adminPassword = this.adminPasswordTextBox.Text;
            settingsObj.password = this.passwordTextBox.Text;
            settingsObj.spectatorPassword = this.spectatorPasswordTextBox.Text;
            settingsObj.trackMedalsRequirement = Int32.Parse(this.trackMedalsRequirementComboBox.Text);
            settingsObj.safetyRatingRequirement = Int32.Parse(this.safetyRatingRequirementTextBox.Text);
            settingsObj.racecraftRatingRequirement = Int32.Parse(this.racecraftRatingRequirementTextBox.Text);
            settingsObj.maxCarSlots = Int32.Parse(this.maximumCarSlotsTextBox.Text);

            return settingsObj;
        }

        private Event getInterfaceEvent()
        {
            Event eventObj = new Event();

            eventObj.track = this.trackComboBox.Text;
            eventObj.preRaceWaitingTimeSeconds = Int32.Parse(this.preRaceWaitingTimeSecondsTextBox.Text);

            return eventObj;
        }

        private void LoadSettings(Settings settings)
        {
            this.serverNameTextBox.Text = settings.serverName;
            this.adminPasswordTextBox.Text = settings.adminPassword;
            this.passwordTextBox.Text = settings.password;
            this.spectatorPasswordTextBox.Text = settings.spectatorPassword;
            this.trackMedalsRequirementComboBox.Text = Convert.ToString(settings.trackMedalsRequirement);
            this.safetyRatingRequirementTextBox.Text = Convert.ToString(settings.safetyRatingRequirement);
            this.racecraftRatingRequirementTextBox.Text = Convert.ToString(settings.racecraftRatingRequirement);
            this.maximumCarSlotsTextBox.Text = Convert.ToString(settings.maxCarSlots);
        }

        private void LoadEvent(Event events)
        {
            this.trackComboBox.Text = events.track;
            this.preRaceWaitingTimeSecondsTextBox.Text = Convert.ToString(events.preRaceWaitingTimeSeconds);
            this.sessionOverTimeSecondsTextBox.Text = Convert.ToString(events.sessionOverTimeSeconds);
            this.ambientTempTextBox.Text = Convert.ToString(events.ambientTemp);
            this.cloudLevelTextBox.Text = Convert.ToString(events.cloudLevel);
            this.rainTextBox.Text = Convert.ToString(events.rain);
            this.weatherRandomnessTextBox.Text = Convert.ToString(events.weatherRandomness);
        }
    }
}