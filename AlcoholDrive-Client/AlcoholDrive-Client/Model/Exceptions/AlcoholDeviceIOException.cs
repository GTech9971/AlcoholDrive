using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model.Exceptions {
    public class AlcoholDeviceIOException : IOException {

        public AlcoholDeviceIOException() : base() { }

        public AlcoholDeviceIOException(string message) : base(message) { }

        public AlcoholDeviceIOException(string manufacturer, string product)
            : base($"デバイスへのIOに失敗 Manufacturer:{manufacturer} Product:{product}") {
        }
    }
}
