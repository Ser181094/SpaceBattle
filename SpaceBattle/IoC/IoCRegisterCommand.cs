using SpaceBattle.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.IoC
{
    public class IoCRegisterCommand : ICommand
    {
        private readonly string key;
        private readonly Func<object[], object> strategy;

        public IoCRegisterCommand(string key, Func<object[], object> strategy)
        {
            this.key = key;
            this.strategy = strategy;
        }

        public void Execute()
        {
            if (ScopeBaseDependencyStrategy.CurrentScope.Value!.Dependencies.ContainsKey(key))
            {
                return;
            }
            else
            {
                if (!ScopeBaseDependencyStrategy.CurrentScope.Value!.Dependencies.TryAdd(key, strategy))
                {
                    throw new System.Exception("Не удалось зарегистрировать зависимость");
                }
            }            
        }
    }
}
