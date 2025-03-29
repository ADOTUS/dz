using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    class TaskLengthLimitException : Exception
    {
        public TaskLengthLimitException(int lenghtTasks, string AddISIN) : base($"Максимальное количество символов в ISIN - {lenghtTasks}. Вы ввели - {AddISIN}")
        { }
    }
}
