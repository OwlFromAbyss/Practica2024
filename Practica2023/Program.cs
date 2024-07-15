using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Practica2024
{
    internal class Program
    {
        static void Main()
        {
            //подключение для добавления контактов
            SQLiteConnection connection = new SQLiteConnection("Data Source = Practica2024DB.db;version=3");
            //команда для SQLite
            SQLiteCommand cmd;


            string commandNumber;
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("~~ГЛАВНЫЙ ЭКРАН~~");
                Console.ResetColor();

                //показ всех доступных команд
                Commands();

                commandNumber = Console.ReadLine();

                switch (commandNumber)
                {

                    //case для добавления контакта
                    case "1":

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("~~ДОБАВЛЕНИЕ КОНТАКТА~~");
                        Console.ResetColor();
                        bool flagFirstCase = true;

                        while (flagFirstCase)
                        {
                            Console.WriteLine();

                            string correctName = NameEnter();
                            string correctSurname = SurnameEnter();
                            string correctNumber = "+7" + NumberEnter();
                            //команда для базы данных
                            string query = $"INSERT INTO Contacts (FirstName, LastName, Number) VALUES ('"+ correctName +"','"+ correctSurname +"','"+ correctNumber +"')";

                            //создание соединения с базой данных
                            connection.Open();
                            //исполнение команды к БД
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            //Закрытие соединения
                            connection.Close();

                            flagFirstCase = false;

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("Контакт был успешно записан\nНажмите любую кнопку для продолжения...");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine();
                        }
                        break;

                    //case для отображения всех номеров
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("~~ПРОСМОТР ВСЕХ КОНТАКТОВ~~");
                        Console.ResetColor();
                        Console.WriteLine();

                        ShowContacts();

                        /*bool deleteNumberFlag = true;
                        while (deleteNumberFlag)
                        {
                            Console.WriteLine("Желаете удалить контакт?");
                            switch (Console.ReadLine().ToLower())
                            {
                                case "да":
                                    Console.WriteLine();
                                    Console.Write("Введите номер контакта который хотите удалить: ");
                                    string deleteThisNumber = Console.ReadLine();

                                    readConnection.Open();
                                    string deleteQuery = $"DELETE FROM Контакты WHERE ID = {deleteThisNumber}";
                                    SQLiteCommand cmdDelete = new SQLiteCommand(deleteQuery, readConnection);
                                    cmdDelete.ExecuteNonQuery();
                                    readConnection.Close();

                                    break;

                                case "нет":
                                    Console.Clear();
                                    deleteNumberFlag = false;
                                    break;

                                default:
                                    Console.WriteLine("Были введены неверные символы");
                                    break;

                            }
                        }*/
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("Нажмите любую кнопку чтобы очистить консоль и вернуться на главный экран...");
                        Console.ResetColor();
                        Console.ReadKey();

                        break;

                    //case поиска по номеру телефона
                    case "3":
                        bool searchFlag = true;
                        while (searchFlag)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("~~ПОИСК ПО НОМЕРУ ТЕЛЕФОНА~~");
                            Console.ResetColor();
                            Console.WriteLine();
                            Console.Write("Введите цифры номера: ");

                            SearchContacts();

                            searchFlag = false;

                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("Нажмите любую кнопку чтобы очистить консоль и вернуться на главный экран...");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;

                    //case для завершения программы
                    case "4":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nЗавершение программы");
                        Console.ResetColor();
                        flag = false;
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Такой команды не существует, попробуйте еще раз\n");
                        Console.Write("Нажмите любую кнопку чтобы очистить консоль и вернуться на главный экран");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }

        }

        //функция вывода доступных команд
        private static void Commands()
        {

            Console.WriteLine("1 - Добавить контакт");
            Console.WriteLine("2 - Просмотр всех имеющихся контактов");
            Console.WriteLine("3 - Поиск контакта");
            Console.WriteLine("4 - Выход из приложения\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Выберите необходимое действие: ");
            Console.ResetColor();
        }

        //функция для ввода имени
        private static string NameEnter()
        {
            bool flagName = true;
            string name;


            while (flagName)
            {
                Console.Write("Введите имя без цифр и специальных знаков: ");
                name = Console.ReadLine();


                if (!name.All(char.IsLetter))
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Были введены недопустимые символы\n");
                    Console.ResetColor();

                }
                else if (name == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Имя не введено, введите имя");
                    Console.ResetColor();
                    Console.WriteLine();

                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Имя успешно записано");
                    Console.ResetColor();
                    flagName = false;
                    Console.WriteLine();

                    return name;
                }
            }
            return "";

        }

        //функция для ввода фамилии
        private static string SurnameEnter()
        {
            bool flagSurname = true;
            string surname;


            while (flagSurname)
            {
                Console.Write("Введите фамилию или '-' если хотите оставить контакт без фамилии: ");
                surname = Console.ReadLine();

                if (surname == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Данный вариант недоступен");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else if (surname == "-")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Поле оставлено пустым");
                    Console.ResetColor();
                    flagSurname = false;
                    Console.WriteLine();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Фамилия успешно записана");
                    Console.ResetColor();
                    flagSurname = false;
                    Console.WriteLine();
                    return surname;

                }
            }
            return "";
        }

        //функция для ввода номера
        private static string NumberEnter()
        {
            bool flagNumber = true;
            string number;
            while (flagNumber)
            {
                Console.Write("Введите номер телефона: +7");
                number = Console.ReadLine();


                if (number.Length != 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неправильное количество символов, попробуйте еще раз");
                    Console.ResetColor();
                }
                else
                {
                    if (number.All(char.IsDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Номер успешно записан");
                        Console.ResetColor();
                        flagNumber = false;

                        return number;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Был введен недопустимый символ(-ы), попробуйте еще раз");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            return "";
        }

        //функция отображения всех контактов 
        static void ShowContacts()
        {
            //строка подключения к БД
            SQLiteConnection readConnection = new SQLiteConnection("Data Source = Practica2024DB.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
            //экземпляр класс, представляющий собой применение команды к БД
            SQLiteCommand cmdRead;

            //команды к БД
            string readQuery = "SELECT * FROM Contacts ORDER BY ID";
            //выполнение команды
            cmdRead = new SQLiteCommand(readQuery, readConnection);

            //открытие соединения к БД
            readConnection.Open();

            //экземпляр класса, который считывает данные из БД
            SQLiteDataReader reader = cmdRead.ExecuteReader();

            //список куда запишутся имя, фамилия, номер человека
            List<string[]> contactsData = new List<string[]>();

            //добавление строк в список, пока считываются данные из БД
            while (reader.Read())
            {
                contactsData.Add(new string[3]);
                contactsData[contactsData.Count - 1][0] = reader[1].ToString();
                contactsData[contactsData.Count - 1][1] = reader[2].ToString();
                contactsData[contactsData.Count - 1][2] = reader[3].ToString();
            }

            //вывод контактов в консоль
            for (int i = 0; i < contactsData.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + contactsData[i][0] + " " + contactsData[i][1] + " \nНомер: " + contactsData[i][2] + "\n");
            }
            //закрытие ридера и соединения с БД
            reader.Close();
            readConnection.Close();
        }

        //функция поиска контакта по цифрам из номера телефона
        static void SearchContacts()
        {
            string search = Console.ReadLine();

            //если не было введено цифр номера или было введено слишком много
            if (search.Length == 0 || search.Length > 10)
            {
                Console.WriteLine("Введено недопустимое количество символов");
            }
            else
            {
                //подключение к БД
                SQLiteConnection readConnection = new SQLiteConnection("Data Source = Practica2024DB.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
                SQLiteCommand cmdRead;

                //выбор всего в таблице в порядке ID
                string readQuery = "SELECT * FROM Contacts ORDER BY ID";
                cmdRead = new SQLiteCommand(readQuery, readConnection);

                readConnection.Open();
                SQLiteDataReader reader = cmdRead.ExecuteReader();

                List<string[]> contactsData = new List<string[]>();

                while (reader.Read())
                {
                    //контакты будут добавлены в список только если содержат введенные пользователем цифры
                    if (reader[3].ToString().Contains(search))
                    {

                        contactsData.Add(new string[4]);
                        contactsData[contactsData.Count - 1][0] = reader[0].ToString();
                        contactsData[contactsData.Count - 1][1] = reader[1].ToString();
                        contactsData[contactsData.Count - 1][2] = reader[2].ToString();
                        contactsData[contactsData.Count - 1][3] = reader[3].ToString();
                    }

                }
                Console.WriteLine();
                //если контакты не были найдены
                if (contactsData.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Контакт(-ы) не были найдены...");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                //иначе выводятся контакты
                else
                {

                    for (int i = 0; i < contactsData.Count; i++)
                    {

                        Console.WriteLine("ID контакта - " + contactsData[i][0] + ") " + contactsData[i][1] + " " + contactsData[i][2] + " \nНомер: " + contactsData[i][3] + "\n");
                    }
                }



                reader.Close();
                readConnection.Close();
            }
        }
    }
}