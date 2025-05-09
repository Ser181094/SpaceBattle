﻿using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class InterpretationCommand : ICommand
    {
        private readonly IMessage _message;

        public InterpretationCommand(IMessage message) => _message = message;

        public void Execute()
        {
            var cmd = IoC.IoC.Resolve<ICommand>("CreateCommand", _message);
            cmd.Execute();
        }
    }
}
