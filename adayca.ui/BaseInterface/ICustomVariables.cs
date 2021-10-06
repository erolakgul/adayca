using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.BaseInterface
{
    public interface ICustomVariables
    {
        Guid GenerateGuid();
        void SetNlogVariable(string key, string value);
    }
}
