using System;

namespace VatsimEF.Models
{
    public abstract class VatsimClient
    {
        public string Cid { get; set; }        
        public string Callsign { get; set; }
        public string Realname { get; set; }
        public string Clienttype { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Server { get; set; }
        public string Protrevision { get; set; }
        public string TimeLastAtisReceived { get; set; }
        public string TimeLogon { get; set; }
    }
}
