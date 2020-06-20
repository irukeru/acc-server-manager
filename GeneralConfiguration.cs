using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Server_Manager_for_Ankarada_Icmece
{
    class GeneralConfiguration
    {
        private int _udpPort;
        private int _tcpPort;
        private int _maxConnections;
        private bool _registerToLobby;
        private int _configVersion = 1;

        public GeneralConfiguration() { }

        public GeneralConfiguration(int udpPort)
        {
            this._udpPort = udpPort;
        }

        public GeneralConfiguration(int udpPort, int tcpPort)
        {
            this._udpPort = udpPort;
            this._tcpPort = tcpPort;
        }

        public GeneralConfiguration(int udpPort, int tcpPort, int maxConnections)
        {
            this._udpPort = udpPort;
            this._tcpPort = tcpPort;
            this._maxConnections = maxConnections;
        }

        public GeneralConfiguration(int udpPort, int tcpPort, int maxConnections, bool registerToLobby)
        {
            this._udpPort = udpPort;
            this._tcpPort = tcpPort;
            this._maxConnections = maxConnections;
            this._registerToLobby = registerToLobby;
        }

        public int udpPort
        {
            get { return _udpPort; }
            set { _udpPort = value; }
        }

        public int tcpPort
        {
            get { return _tcpPort; }
            set { _tcpPort = value; }
        }

        public int maxConnections
        {
            get { return _maxConnections; }
            set { _maxConnections = value; }
        }

        public int registerToLobby
        {
            get { return _registerToLobby ? 1 : 0; }
            set { _registerToLobby = (value == 1); }
        }

        public int configVersion
        {
            get { return _configVersion; }
            set { _configVersion = value ; }
        }
    }
}
