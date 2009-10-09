using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Finance.Web
{
    [Serializable()]
    public class DataNotAvailable : Exception
    {
        public string PotentialReason { private set; get; }
        public DataNotAvailable() : base() 
        {
            PotentialReason = "Unknown";
        }
        
        public DataNotAvailable(string message) : base(message) 
        {
            PotentialReason = message;
        }
        
        public DataNotAvailable(string message, Exception inner) : base(message,inner) 
        {
            PotentialReason = message;
        }
        
        protected DataNotAvailable(SerializationInfo info, StreamingContext context) : base(info,context) { }
        
        public override string ToString()
        {
            return PotentialReason + " " + base.ToString();
        }
    }
}
