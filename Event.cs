using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC_Server_Manager_for_Ankarada_Icmece
{
    class Event
    {
        private String _track;
        private int _preRaceWaitingTimeSeconds;
        private int _sessionOverTimeSeconds;
        private int _ambientTemp;
        private double _cloudLevel;
        private double _rain;
        private int _weatherRandomness;

        public Event() { }

        public String track
        {
            get { return _track; }
            set { _track = value; }
        }

        public int preRaceWaitingTimeSeconds
        {
            get { return _preRaceWaitingTimeSeconds; }
            set { _preRaceWaitingTimeSeconds = value; }
        }

        public int sessionOverTimeSeconds
        {
            get { return _sessionOverTimeSeconds; }
            set { _sessionOverTimeSeconds = value; }
        }

        public int ambientTemp
        {
            get { return _ambientTemp; }
            set { _ambientTemp = value; }
        }

        public double cloudLevel
        {
            get { return _cloudLevel; }
            set { _cloudLevel = value; }
        }

        public double rain
        {
            get { return _rain; }
            set { _rain = value; }
        }

        public int weatherRandomness
        {
            get { return _weatherRandomness; }
            set { _weatherRandomness = value; }
        }
    }
}
