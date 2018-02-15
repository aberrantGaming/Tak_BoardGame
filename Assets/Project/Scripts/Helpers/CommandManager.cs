using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class CommandManager : MonoBehaviour
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public bool HasPendingCommands {
            get {
                return _commands.Any(x => !x.IsCompleted);
            }
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void ProcessPendingCommands()
        {
            foreach (ICommand command in _commands.Where(x => !x.IsCompleted))
            {
                command.Execute();
            }
        }
    }
}