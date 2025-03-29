
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomashneZadanie
{
    internal static class Program
    {
        private static string? Name;
        private static int cntTasks;// = 1;
        private static int lenghtTasks;
        private static Dictionary<int, string> ISINS = new Dictionary<int, string> { };
        private static Dictionary<string, string> allowedCommands = new Dictionary<string, string>
        {
            {"/start","     To set or change name"},
            {"/info","      To show info about version"},
            {"/help","      To show info about program"},
            {"/echo","      To echo"},
            {"/addtask","   To add your ISIN to list"},
            {"/showtasks"," To show your ISIN list"},
            {"/removetask","To delete ISIN from list"},
            {"/exit","      To close the program"}
        };
        public static void Main(string[] args)
        {
            try
            {
                CntTasksSet();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}. Установлено значение по умолчанию - 5");
                cntTasks = 5;
            }

            try
            {
                LenghtTasksSet();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}. Установлено значение по умолчанию - 12");
                lenghtTasks = 12;
            }

            ShowMenu();
            string? Command;
            while (true)
            {
                Console.WriteLine(string.IsNullOrEmpty(Name) ? "Введите команду:" : $"Введите команду, {Name}:");
                Command = Console.ReadLine();

                try
                {
                    if (string.IsNullOrWhiteSpace(Command) || !allowedCommands.ContainsKey(Command))
                    {
                        throw new CommandException();
                    }

                    if (Command == "/exit")
                        return;

                    CommandExecute(Command);
                }
                catch (CommandException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }
        public static void CommandExecute(string Command)
        {
            switch (Command)
            {
                case "/start":
                    Name = null;
                    try
                    {
                        NameInput();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        Name = null;
                    }
                    break;

                case "/help":
                    Help();
                    break;

                case "/info":
                    Info();
                    break;

                case "/exit":
                    Exit();
                    break;

                case "/addtask":
                    try
                    {
                        Addtask();
                    }
                    catch (DuplicateTaskException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (TaskLengthLimitException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    catch (TaskCountLimitException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;

                case "/showtasks":
                    Showtasks();
                    break;

                case "/removetask":
                    try
                    {
                        Removetask();
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                    break;
                //Removetask();
                //break;

                case "/echo":
                    {
                        if (!string.IsNullOrEmpty(Name))
                        {
                            string? echoText = null;
                            while (string.IsNullOrEmpty(echoText))
                            {
                                Console.WriteLine("Enter text to echo:");
                                echoText = Console.ReadLine();
                            }
                            try
                            {
                                int result = ParseAndValidateInt(echoText, 10, 100);
                                Echo(echoText);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                            break;
                        }
                        else
                        {
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

            if (ISINS.ContainsValue(value))
            {
                throw new DuplicateTaskException(value);
            }
            else
            {
                int newId = GetNextAvailableId();
                ISINS[newId] = value;
                Console.WriteLine($"Элемент  {value}  добавлен в список с ID {newId}.");
            }

        }
        static int GetNextAvailableId()
        {
            int Cnt = 1;
            while (ISINS.ContainsKey(Cnt)) Cnt++;
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
            Console.WriteLine("Введите максимальное количество ISIN для отслеживания:");
            int value = ParseAndValidateInt(Console.ReadLine(), 0, 12);
            cntTasks = value;
            Console.WriteLine($"Вы ввели: {cntTasks}");

        }

        public static void LenghtTasksSet()
        {
            Console.WriteLine("Введите максимальную длину ISIN:");
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
            Console.WriteLine($"Write security ISIN  {Name}");
            Console.WriteLine("");

            string? AddISIN = Console.ReadLine();

            if (string.IsNullOrEmpty(AddISIN))
            {
                throw new ArgumentException($"Введено пустое значение.");
            }
            if (AddISIN.Length > lenghtTasks)
            {
                throw new TaskLengthLimitException(lenghtTasks, AddISIN);
            }
            if (ISINS.Count == cntTasks)
            {
                throw new TaskCountLimitException(cntTasks, AddISIN);
            }
            if (string.IsNullOrWhiteSpace(AddISIN))
            {
                throw new ArgumentException("Введено недопустимое выражение");
            }
            AddItem(AddISIN);
            Console.WriteLine("");
            //if (ISINS.Count < cntTasks)
            //{
            //    Console.WriteLine($"Повторить добавление? Если да - нажми Y и ENTER  {Name}");
            //    Console.WriteLine("");
            //    if (Console.ReadLine() == "Y")
            //        Addtask();
            //}
        }
        public static void Showtasks()
        {
            if (ISINS.Count == 0)
            {
                Console.WriteLine("Список пуст");
                Console.WriteLine("");
            }
            else
            {
                //Console.WriteLine("Список ISIN: " + string.Join(", ", ISIN));
                foreach (var item in ISINS)
                {
                    Console.WriteLine($"{item.Key}. {item.Value}");
                }
            }
            //if (ISINS.Count < 5)
            //{
            //    Console.WriteLine($"Если надо добавить бумагу - нажми Y и ENTER  {Name}");
            //    Console.WriteLine("");

            //    if (Console.ReadLine() == "Y")
            //        Addtask();
            //}
        }
        public static void Removetask()
        {
            if (ISINS.Count == 0)
            {
                Console.WriteLine("Список пустой");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Введите номер бумаги из списка для удаления");
                foreach (var item in ISINS)
                {
                    Console.WriteLine($"{item.Key}. {item.Value}");
                }
                Console.WriteLine("");

                int IdISIN;
                IdISIN = ParseAndValidateInt((Console.ReadLine()), 0, cntTasks);
                ISINS.Remove(IdISIN);
                Console.WriteLine($"Удален элемент: {IdISIN}");

                //Console.WriteLine($"Повторить удаление?Если да - нажми Y и ENTER  {Name}");
                //Console.WriteLine("");
                //if (Console.ReadLine() == "Y")
                //    Removetask();
            }
        }
        public static int ParseAndValidateInt(string? str, int min, int max)
        {
            if (int.TryParse(str, out int value))
            {
                if (value > min && value < max)
                {
                    return value;
                }
            }
            throw new ArgumentException("Ошибка");

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
