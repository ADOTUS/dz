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
            : base("Пустая команда не обрабатывается, попробуйте снова.") { }
        public CommandException(string Command)
            : base($"Не верная команда '{Command}', попробуйте снова.") { }
    }

}
