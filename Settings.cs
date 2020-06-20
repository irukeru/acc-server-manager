using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Server_Manager_for_Ankarada_Icmece
{
    class Settings
    {
        private String _serverName;
        private String _adminPassword;
        private String _password;
        private String _spectatorPassword;
        private int _trackMedalsRequirement;
        private int _safetyRatingRequirement;
        private int _racecraftRatingRequirement;
        private int _maxCarSlots;
        private int _configVersion = 1;

        public Settings() { }

        public String serverName
        {
            get { return _serverName; }
            set { _serverName = value; }
        }

        public String adminPassword
        {
            get { return _adminPassword; }
            set { _adminPassword = value; }
        }

        public String password
        {
            get { return _password; }
            set { _password = value; }
        }

        public String spectatorPassword
        {
            get { return _spectatorPassword; }
            set { _spectatorPassword = value; }
        }

        public int trackMedalsRequirement
        {
            get { return _trackMedalsRequirement; }
            set { _trackMedalsRequirement = value; }
        }

        public int safetyRatingRequirement
        {
            get { return _safetyRatingRequirement; }
            set { _safetyRatingRequirement = value; }
        }

        public int racecraftRatingRequirement
        {
            get { return _racecraftRatingRequirement; }
            set { _racecraftRatingRequirement = value; }
        }

        public int maxCarSlots
        {
            get { return _maxCarSlots; }
            set { _maxCarSlots = value; }
        }

        public int configVersion
        {
            get { return _configVersion; }
            set { _configVersion = value; }
        }
    }
}