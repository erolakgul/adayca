using adayca.ui.BaseInterface;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Services
{
    public class CustomVariables : ICustomVariables
    {
        private Guid _guid;

        public CustomVariables()
        {
            _guid = Guid.NewGuid();
        }
        public Guid GenerateGuid()
        {
            return _guid;
        }

        public void SetNlogVariable(string key, string value)
        {
            LogManager.Configuration.Variables[key] = value;
        }
    }
}
