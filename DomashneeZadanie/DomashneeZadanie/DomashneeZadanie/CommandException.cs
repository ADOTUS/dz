using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    class CommandException : Exception
    {
        public CommandException()
            : base($"Не верная команда, попробуйте снова.") { }
    }
}
