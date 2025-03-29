using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    class TaskCountLimitException : Exception
    {
        public TaskCountLimitException(int cntTasks, string AddISIN) : base($"Максимальное количество добавленных ISIN - {cntTasks}, элемент {AddISIN} в список не добавлен")
        { }
    }
}

