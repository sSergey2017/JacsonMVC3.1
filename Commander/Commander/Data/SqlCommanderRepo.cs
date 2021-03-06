using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Add(cmd);
            
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
           return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == id);

        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            // throw new NotImplementedException();
        }
    }
}
