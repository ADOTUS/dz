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
        //public TaskCountLimitException()    : base($"Не верное значение. Рекомендуемый диапазон от 1 до 5") { }
        public TaskCountLimitException(int min,int max) : base($"Доступный числовой диапазон от {min} до {max}")
        { }
        public TaskCountLimitException(int cntTasks) : base($"Такого номерра нет в списке. Количество добавленных задач - {cntTasks}")
        { }
        public TaskCountLimitException(int cntTasks, string addTask) : base($"Максимальное количество добавленных задач - {cntTasks}, элемент {addTask} в список не добавлен")
        { }
    }
}

