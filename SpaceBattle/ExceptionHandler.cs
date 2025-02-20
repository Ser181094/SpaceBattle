using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class ExceptionHandler
    {
        private static IDictionary<Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>> _store = new Dictionary<Type, IDictionary<Type, Func<ICommand, Exception, ICommand>>>();
        
        private static readonly Func<ICommand, Exception, ICommand> _defaultHandler =
            (command, exception) => new WriteToLogCommand(command, exception);
        public static Func<ICommand, Exception, ICommand> DefaultHandler { get; set; } = _defaultHandler;

        public static ICommand Handle(ICommand c, Exception e)
        {
            Type ct = c.GetType();
            Type et = e.GetType();

            if (!_store.TryGetValue(ct, out var eStore))
            {
                return DefaultHandler(c, e);
            }

            if (!eStore.TryGetValue(et, out var handler))
            {
                return DefaultHandler(c, e);
            }            

            return handler(c, e);
        }

        public static void RegisterHandler(Type? ct, Type? et, Func<ICommand, Exception, ICommand> h)
        {
            _store.Add(ct, new Dictionary<Type, Func<ICommand, Exception, ICommand>>() {{ et, h }});
            
        }

        public static void ClearStore()
        {
            _store.Clear();
        }
    }

}
