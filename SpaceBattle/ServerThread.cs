﻿using SpaceBattle.Interfaces;
using System.Collections.Concurrent;

namespace SpaceBattle
{
    public class ServerThread
    {
        public BlockingCollection<ICommand> _q;
        private Thread _t;
        private bool _stop = false;
        public Action _behaviour;
        public Action _actionAfterStop = () => { };
        public CommandState commandState;

        public ServerThread(BlockingCollection<ICommand> q)
        {
            _q = q;
            commandState = new CommandState();

            _behaviour = () =>
            {
                var cmd = _q.Take();
                try
                {
                    commandState.Execute(cmd);
                }
                catch (Exception e)
                {
                    IoC.IoC.Resolve<ICommand>("ExceptionHandler", cmd, e).Execute();
                }
            };

            _t = new Thread(() => {
                while (!_stop)
                {
                    _behaviour();
                }
                _actionAfterStop();
            });
        }

        public void Start()
        {
            _t.Start();
        }

        public void Stop()
        {
            _stop = true;
        }

        public void Join()
        {
            _t.Join();
        }

        public void SetBehaviour(Action newBeh)
        {
            _behaviour = newBeh;
        }

        public void SetActionAfterStop(Action action)
        {
            action.Invoke();
        }

    }
}
