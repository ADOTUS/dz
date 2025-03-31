using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//2. base($"Наименование ‘{ISIN}’ не соответствует требованиям (до 12 символов)") - максимальное количество символом может меняться. Не нужно его жестко приписывать в коде
//3. Не нужно добавлять try catch в каждую команду. Должны быть более глобальные try catch например один в методе Main, второй в CommandExecute
//4. Не catch для класса Exception
namespace DomashneZadanie
{
    internal static class Program
    {
        private static string? Name;
        private static int cntTasks;// = 1;
        private static int lenghtTasks;
        static bool sucscess = false;
        private static Dictionary<int, string> tasks = new Dictionary<int, string> { };
        private static Dictionary<string, string> allowedCommands = new Dictionary<string, string>
        {
            {"/start","     To set or change name"},
            {"/info","      To show info about version"},
            {"/help","      To show info about program"},
            {"/echo","      To echo"},
            {"/addtask","   To add your tasl to list"},
            {"/showtasks"," To show your tasks list"},
            {"/removetask","To delete task from list"},
            {"/exit","      To close the program"}
        };
        public static void Main(string[] args)
        {
            while (!sucscess)//sucscess == false
            {
                try

                {
                    CntTasksSet();
                    LenghtTasksSet();
                }
                catch (TaskCountLimitException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }

            ShowMenu();
            while (true)
            {
                try
                {
                    string? Command;

                    Console.WriteLine(string.IsNullOrEmpty(Name) ? "Введите команду:" : $"Введите команду, {Name}:");
                    Command = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(Command) || !allowedCommands.ContainsKey(Command))
                    {
                        if (Command == null)
                        { 
                            throw new CommandException();
                        }
                        throw new CommandException(Command);
                    }
                    if (Command == "/exit")
                    {

                        Exit();
                        return;
                    }

                    CommandExecute(Command);


                }
                catch (CommandException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Name = null;
                }
                catch (DuplicateTaskException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (TaskLengthLimitException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                } 
            }

        }
        public static void CommandInput()
        {

        }
        public static void CommandExecute(string Command)
        {

            switch (Command)
            {
                case "/start":
                    Name = null;

                    NameInput();

                    break;

                case "/help":
                    Help();
                    break;

                case "/info":
                    Info();
                    break;

                case "/addtask":

                    Addtask();
                    break;

                case "/showtasks":
                    Showtasks();
                    break;

                case "/removetask":

                    Removetask();
                    break;

                case "/echo":
                    {
                        if (!string.IsNullOrEmpty(Name))
                        {
                            string? echoText = null;
                            while (string.IsNullOrEmpty(echoText))
                            {
                                Console.WriteLine("Введите какой либо не пустой текст:");
                                echoText = Console.ReadLine();
                            }

                            Echo(echoText);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Безымянному страннику эхо не отвечает:");
                            break;
                        }
                    }
                default:
                    Console.WriteLine($"Unknown command {Name}. Try again, please.");
                    break;
            }

        }
        static void AddItem(string value)
        {

            if (tasks.ContainsValue(value))
            {
                throw new DuplicateTaskException(value);
            }
            else
            {
                int newId = GetNextAvailableId();
                tasks[newId] = value;
                Console.WriteLine($"Элемент  {value}  добавлен в список с ID {newId}.");
            }

        }
        static int GetNextAvailableId()
        {
            int Cnt = 1;
            while (tasks.ContainsKey(Cnt)) Cnt++;
            return Cnt;
        }
        public static void ShowMenu()
        {
            foreach (var menus in allowedCommands)
            {
                Console.WriteLine($"{menus.Key} {menus.Value}");
            }
            Console.WriteLine("");
        }
        public static void NameInput()
        {
            Console.WriteLine("Enter your Name:");
            Name = Console.ReadLine();
            ValidateString(Name);
        }
        public static void CntTasksSet()
        {
            Console.WriteLine("Введите максимальное количество задач для отслеживания:");
            int value = ParseAndValidateInt(Console.ReadLine(), 0, 5);
            cntTasks = value;
            Console.WriteLine($"Вы ввели: {cntTasks}");

        }

        public static void LenghtTasksSet()
        {
            Console.WriteLine("Введите максимальную длину задачи:");
            int value = ParseAndValidateInt(Console.ReadLine(), 0, 12);
            lenghtTasks = value;
            Console.WriteLine($"Вы ввели: {lenghtTasks}");
        }
        public static void Help()
        {
            foreach (var menus in allowedCommands)
            {
                Console.WriteLine($"{menus.Key} {menus.Value}");
            }
            Console.WriteLine("");
        }
        public static void Info()
        {
            Console.WriteLine($"App version 0.01. CreateDate is 24-02-2025  {Name}");
            Console.WriteLine("");
        }
        public static void Exit()
        {
            Console.WriteLine($"Buy Buy  {Name}");
        }
        public static void Echo(string text)
        {
            Console.WriteLine($"Echo: {text}");
            Console.WriteLine("");
        }
        public static void Addtask()
        {
            Console.WriteLine($"Напишите задачу  {Name}");
            Console.WriteLine("");

            string? addTask = Console.ReadLine();

            if (string.IsNullOrEmpty(addTask))
            {
                throw new ArgumentException($"Введено пустое значение.");
            }
            if (addTask.Length > lenghtTasks)
            {
                throw new TaskLengthLimitException(lenghtTasks, addTask);
            }
            if (tasks.Count == cntTasks)
            {
                throw new TaskCountLimitException(cntTasks, addTask);
            }
            if (string.IsNullOrWhiteSpace(addTask))
            {
                throw new ArgumentException("Введено недопустимое выражение");
            }
            AddItem(addTask);
            Console.WriteLine("");
            if (tasks.Count < cntTasks)
            {
                Console.WriteLine($"Повторить добавление? Если да - нажми Y и ENTER  {Name}");
                Console.WriteLine("");
                if (Console.ReadLine() == "Y")
                    Addtask();
            }
        }
        public static void Showtasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список пуст");
                Console.WriteLine("");
            }
            else
            {
                //Console.WriteLine("Список task: " + string.Join(", ", task));
                foreach (var item in tasks)
                {
                    Console.WriteLine($"{item.Key}. {item.Value}");
                }
            }
        }
        public static void Removetask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список пустой");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Введите номер задачи из списка для удаления");
                foreach (var item in tasks)
                {
                    Console.WriteLine($"{item.Key}. {item.Value}");
                }
                Console.WriteLine("");

                int idTask;
                idTask = ParseAndValidateInt((Console.ReadLine()), 0, cntTasks);
                tasks.Remove(idTask);
                Console.WriteLine($"Удален элемент: {idTask}");

            }
        }
        public static int ParseAndValidateInt(string? str, int min, int max)
        {

            if (int.TryParse(str, out int value) && value > min && value < max)
            {
                sucscess = true;
                return value;
            }
            else
            {
                sucscess = false;
                if (cntTasks != 0 && lenghtTasks!=0)
                {
                    throw new TaskCountLimitException(cntTasks);
                }
                else
                {
                    throw new TaskCountLimitException(min,max);
                }
            }
        }
        public static string ValidateString(string? str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            throw new ArgumentException("Введено недопустимое выражение");
        }
    }

}