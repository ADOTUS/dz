using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//++ 1 Добавить глобальный try catch
//  Добавьте try catch в метод Main
//  catch должен отлавливать все виды исключений и выводить в консоль сообщение “Произошла непредвиденная ошибка: “ с информацией об исключении (Type, Message, StackTrace, InnerException)
//++++2 Добавить ограничение на максимальное количество задач
//  При старте приложения выводите текст «Введите максимально допустимое количество задач»
//  Ожидайте ввод из консоли. Это должно быть число от 1 до 100, иначе нужно выбросить исключение ArgumentException с сообщением.
//  В методе Main добавьте отдельный catch для типа ArgumentException и в нем выводите в консоль только сообщение из исключения.
//  Создайте свой тип исключения TaskCountLimitException, который в конструкторе должен принимать только int taskCountLimit, а сообщение должно быть вида $“Превышено максимальное количество задач равное {taskCountLimit}“ https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
//++++ 3 Добавьте проверку на максимально допустимое количество задач в обработчик команды /addtask. Если количество превышено, то нужно выбросить исключение TaskCountLimitException.
//  В методе Main добавьте отдельный catch для типа TaskCountLimitException и в нем выводите в консоль только сообщение из исключения.
//  Попадание в catch не должно останавливать работу приложения
//  Добавить ограничение на максимальную длину задачи
//  При старте приложения выводите текст «Введите максимально допустимую длину задачи»
//  Ожидайте ввод из консоли. Это должно быть число от 1 до 100, иначе нужно выбросить исключение ArgumentException с сообщением.
//  Создайте свой тип исключения TaskLengthLimitException, который в конструкторе должен принимать int taskLength, int taskLengthLimit, а сообщение должно быть вида $“Длина задачи ‘{taskLength}’ превышает максимально допустимое значение {taskLengthLimit}“.
//  Добавьте проверку на максимально допустимую длину задачи в обработчик команды /addtask. Если длина превышена, то нужно выбросить исключение TaskLengthLimitException.
//  В методе Main добавьте отдельный catch для типа TaskLengthLimitException и в нем выводите в консоль только сообщение из исключения.
//  Попадание в catch не должно останавливать работу приложения
//+++4 Добавить проверку на дубликаты задач
//  Создайте свой тип исключения DuplicateTaskException, который в конструкторе должен принимать string task, а сообщение должно быть вида $“Задача ‘{task}’ уже существует“.
//  Добавьте проверку на дубликаты задач в обработчик команды /addtask. Если пользователь пытается добавить уже существующую задачу., то нужно выбросить исключение DuplicateTaskException.
//  В методе Main добавьте отдельный catch для типа DuplicateTaskExceptionи в нем выводите в консоль только сообщение из исключения.
//+ 5 Добавить метод int ParseAndValidateInt(string? str, int min, int max), который приводит полученную строку к int и проверяет, что оно находится в диапазоне min и max. В противном случае выбрасывать ArgumentException с сообщением. Добавить использование этого метода в приложение.
//++ 6 Добавить метод void ValidateString(string? str), который проверяет, что строка не равна null
// , не равна пустой строке и имеет какие-то символы кроме проблема. В противном случае
// выбрасывать ArgumentException с сообщением. Добавить использование этого метода в приложение.
//+ 7 Вынести обработчики команд в отдельные методы
namespace _4zadanie
{
    internal static class Program
    {
        private static string? Name;

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
                catch (CommandException)
                {
                    Console.WriteLine("Не верная команда, попробуйте снова.");
                }
                finally
                {
                    Command = null;
                }
            }
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

                case "/exit":
                    Exit();
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
                            //string echoText = null;
                            //while (string.IsNullOrEmpty(echoText))
                            //{
                            //    Console.WriteLine("Enter text to echo:");
                            //    echoText = Console.ReadLine();
                            //}
                            //Echo(echoText);
                            //Command = null;
                            //break;
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
            try
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
            catch (DuplicateTaskException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return;
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
            try
            {
                ValidateString(Name);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
                Name = null;
                return;
            }
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
            try
            {
                if (string.IsNullOrEmpty(AddISIN))
                {
                    throw new ArgumentException("Введено недопустимое значение.");
                }

                if (AddISIN.Length > 3)
                {
                    throw new TaskLengthLimitException();
                }

            }
            catch (TaskLengthLimitException)
            {
                Console.WriteLine($"Максимальное количество символов в ISIN - 3");
                return;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return;
            }
            try
            {
                if (ISINS.Count == 5)
                {
                    throw new TaskCountLimitException();
                }
            }
            catch (TaskCountLimitException)
            {
                Console.WriteLine($"Максимальное количество добавленных ISIN - 5, элемент {AddISIN} в список не добавлен");
                return;
            }
            try
            {
                ValidateString(AddISIN);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"{ex.Message}");
                return;
            }
            AddItem(AddISIN);
            Console.WriteLine("");
            if (ISINS.Count < 5)
            {
                Console.WriteLine($"Повторить добавление? Если да - нажми Y и ENTER  {Name}");
                Console.WriteLine("");
                if (Console.ReadLine() == "Y")
                    Addtask();
            }
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
            if (ISINS.Count < 5)
            {
                Console.WriteLine($"Если надо добавить бумагу - нажми Y и ENTER  {Name}");
                Console.WriteLine("");

                if (Console.ReadLine() == "Y")
                    Addtask();
            }
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

                if (int.TryParse(Console.ReadLine(), out int IdISIN))
                {
                    if (ISINS.TryGetValue(IdISIN, out string? ISIN))
                    {
                        Console.WriteLine($"Удалено из списка: {IdISIN}. {ISIN}");
                        Console.WriteLine("");
                        ISINS.Remove(IdISIN);
                    }
                    else
                    {
                        Console.WriteLine("В списке нет бумаги с таким ID.");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Ожидается числовое значение, ошибка.");
                    Console.WriteLine("");
                }
                Console.WriteLine($"Повторить удаление?Если да - нажми Y и ENTER  {Name}");
                Console.WriteLine("");
                if (Console.ReadLine() == "Y")
                    Removetask();
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
            throw new ArgumentException("Введено не число или число не входит в заданный диапазон");
        }
        public static string ValidateString(string? str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            throw new ArgumentException("Введено недопустимое выражение");
        }
        class CommandException : Exception
        {
            public CommandException() : base()
            { }
            public CommandException(string message) : base(message)
            { }
        }
        class TaskCountLimitException : Exception
        {
            public TaskCountLimitException() : base()
            { }
            public TaskCountLimitException(string message) : base(message)
            { }
        }
        class TaskLengthLimitException : Exception
        {
            public TaskLengthLimitException() : base()
            { }
            public TaskLengthLimitException(string message) : base(message)
            { }
        }
        class DuplicateTaskException : Exception
        {
            public DuplicateTaskException(string ISIN)
                : base($"Бумага ‘{ISIN}’ уже существует в списке") { }
        }
        class LenghtTaskException : Exception
        {
            public LenghtTaskException(string ISIN)
                : base($"Наименование ‘{ISIN}’ не соответствует требованиям (до 12 символов)") { }
        }

    }

}
