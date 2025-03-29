using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    class LenghtTaskException : Exception
    {
        public LenghtTaskException(string ISIN)
            : base($"Наименование ‘{ISIN}’ не соответствует требованиям (до 12 символов)") { }
    }
}
