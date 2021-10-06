using adayca.ui.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.BaseInterface
{
    public interface ICustomOptions<out T> : IOptions<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
        T GetValue(string section);
        T GetUrl(string param);
    }
}
