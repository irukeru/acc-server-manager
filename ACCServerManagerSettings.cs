using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Server_Manager_for_Ankarada_Icmece
{
    class ACCServerManagerSettings
    {

        private String _AccServerPath;

        public ACCServerManagerSettings() { }

        public ACCServerManagerSettings(String AccServerPath) {
            this._AccServerPath = AccServerPath;
        }

        public string AccServerPath
        {
            get { return _AccServerPath; }
            set { _AccServerPath = value; }
        }
    }
}
