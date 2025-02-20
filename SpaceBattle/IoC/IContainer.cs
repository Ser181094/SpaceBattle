using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.IoC
{
    public interface IContainer
    {
        T Resolve<T>(string key, params object[] args);
    }
}
