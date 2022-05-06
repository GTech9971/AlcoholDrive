using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public abstract class BaseService {

        protected readonly List<int> CommandList;

        public BaseService() {
            CommandList = new List<int>();
            InitCommandList();
        }

        protected abstract void InitCommandList();

        public bool ContainsCommand(int cmd) {
            return CommandList.Exists(c => c == cmd);
        }
    }
}
