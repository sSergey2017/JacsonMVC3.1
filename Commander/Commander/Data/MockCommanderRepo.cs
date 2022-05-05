﻿using Commander.Models;
using System.Collections.Generic;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command { Id = 0, HowTo = "Boil", Line="Water", Platform = "Pan" },
                new Command { Id = 1, HowTo = "Boil1", Line="Water1", Platform = "Pan" },
                new Command { Id = 2, HowTo = "Boil1", Line="Water1", Platform = "Pan" }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil", Line="Water", Platform = "Pan" };
        }
    }
}
