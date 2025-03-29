using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    class DuplicateTaskException : Exception
    {
        public DuplicateTaskException(string ISIN)
            : base($"Бумага ‘{ISIN}’ уже существует в списке") { }
    }
}
