using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;

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
                Console.WriteLine();
                Console.ResetColor();

                //показ всех доступных команд
                ShowCommands();

                commandNumber = Console.ReadLine();

                switch (commandNumber)
                {

                    //case для добавления контакта
                    case "1":

                        Console.Clear();
                        bool flagFirstCase = true;

                        while (flagFirstCase)
                        {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("~~ДОБАВЛЕНИЕ КОНТАКТА~~");
                        Console.ResetColor();
                        Console.WriteLine();    

                            
                            try
                            {
                                string correctName = EnterName();
                                string correctSurname = EnterSurname();
                                string correctNumber = EnterNumber();

                                //команда для базы данных
                                string query = $"INSERT INTO Contacts (FirstName, LastName, Number) VALUES ('" + correctName + "','" + correctSurname + "','" + correctNumber + "')";

                                //создание соединения с базой данных
                                connection.Open();
                                //исполнение команды к БД
                                cmd = new SQLiteCommand(query, connection);
                                cmd.ExecuteNonQuery();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Номер успешно записан");
                                
                                Console.WriteLine();
                                
                                Console.Write("Контакт был успешно записан\nНажмите любую кнопку для продолжения...");
                                Console.ResetColor();

                                //Закрытие соединения
                                connection.Close();

                                flagFirstCase = false;
                            }
                            catch (Exception) {
                                Console.WriteLine();
                                Console.ForegroundColor= ConsoleColor.Red;
                                Console.Write("Контакт с таким номером телефона был найден в базе данных\n");
                                Console.ResetColor();
                                Console.Write("Нажмите любую кнопку...");
                            }
                            

                            Console.ReadKey();
                            Console.Clear();
                            
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
                            

                            SearchContacts();
                            ChangeContact();
                            DeleteContact();

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
        private static void ShowCommands()
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
        private static string EnterName()
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
        private static string EnterSurname()
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
        private static string EnterNumber()
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
                        
                        flagNumber = false;

                        return "+7" + number;
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
            string readQuery = "SELECT * FROM Contacts ORDER BY FirstName";
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
                contactsData[contactsData.Count - 1][0] = reader[0].ToString();
                contactsData[contactsData.Count - 1][1] = reader[1].ToString();
                contactsData[contactsData.Count - 1][2] = reader[2].ToString();
            }

            if (contactsData.Count == 0)
            {

                Console.WriteLine("База данных пуста");

                Console.WriteLine();
            }
            else
            {

                for (int i = 0; i < contactsData.Count; i++)
                {
                    Console.WriteLine(i + 1 + ") " + contactsData[i][0] + " " + contactsData[i][1] + " \nНомер: " + contactsData[i][2] + "\n");
                }
            }
            //вывод контактов в консоль
            //закрытие ридера и соединения с БД
            reader.Close();
            readConnection.Close();
        }

        //функция поиска контакта по цифрам из номера телефона
        static void SearchContacts()
        {
            bool searchContactsFlag = true;
            while (searchContactsFlag) {
                Console.Write("Введите цифры номера: ");
                string search = Console.ReadLine();

                //если не было введено цифр номера или было введено слишком много
                if (search.Length == 0 || search.Length > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введено недопустимое количество символов");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    //подключение к БД
                    SQLiteConnection readConnection = new SQLiteConnection("Data Source = Practica2024DB.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
                    SQLiteCommand cmdRead;

                    //выбор всего в таблице в порядке имени
                    string readQuery = "SELECT * FROM Contacts ORDER BY FirstName";
                    cmdRead = new SQLiteCommand(readQuery, readConnection);

                    readConnection.Open();
                    SQLiteDataReader reader = cmdRead.ExecuteReader();

                    List<string[]> contactsData = new List<string[]>();

                    while (reader.Read())
                    {
                        //контакты будут добавлены в список только если содержат введенные пользователем цифры
                        if (reader[2].ToString().Contains(search))
                        {

                            contactsData.Add(new string[3]);
                            contactsData[contactsData.Count - 1][0] = reader[0].ToString();
                            contactsData[contactsData.Count - 1][1] = reader[1].ToString();
                            contactsData[contactsData.Count - 1][2] = reader[2].ToString();

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
                        searchContactsFlag = false;
                    }
                    //иначе выводятся контакты
                    else
                    {

                        for (int i = 0; i < contactsData.Count; i++)
                        {

                            Console.WriteLine(i + 1 + ") " + contactsData[i][0] + " " + contactsData[i][1] + " \nНомер: " + contactsData[i][2] + "\n");
                        }
                        searchContactsFlag = false;
                        reader.Close();
                        readConnection.Close();



                    }


                }
            }
        }

        //функция изменения имени/фамилии контакта
        private static void ChangeContact()
        {
            SQLiteConnection readConnection = new SQLiteConnection("Data Source = Practica2024DB.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");

            string changeContactData;
            bool changeDataFlag = true;
            bool changeContactFlag = true;
            bool newNameIsNotNull = true;
            string word;


            while (changeDataFlag)
            {
                Console.Write("Желаете изменить данные контакта? (да/нет): ");
                readConnection.Open();

                switch (changeContactData = Console.ReadLine().ToLower())
                {
                    case "да":
                        Console.WriteLine();


                        while (changeContactFlag)
                        {
                            Console.Write("Введите номер телефона, контакт которого хотите изменить: +7");

                            string contactNumber = Console.ReadLine();
                            Console.WriteLine();

                            if (contactNumber.Length == 10 && contactNumber.All(char.IsDigit))
                            {
                                Console.Write("Введите, что хотите изменить (имя/фамилия): ");
                                switch (word = Console.ReadLine().ToLower())
                                {

                                    case "имя":

                                        while (newNameIsNotNull)
                                        {
                                            Console.WriteLine();
                                            Console.Write("Введите новое имя контакта: ");
                                            string nameNew = Console.ReadLine();
                                            if (nameNew == "")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Имя не может быть оставлено пустым, попробуйте еще раз");
                                                Console.ResetColor();
                                                Console.WriteLine();
                                            }

                                            else if (!nameNew.All(char.IsLetter))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Были введены недопустимые символы, попробуйте еще раз");
                                                Console.ResetColor();
                                                Console.WriteLine();
                                            }

                                            else
                                            {

                                                string updateNameQuery = "UPDATE Contacts SET FirstName='" + nameNew + "'  WHERE Number='" + "+7" + contactNumber + "'";

                                                SQLiteCommand updateNameCmd = new SQLiteCommand(updateNameQuery, readConnection);
                                                updateNameCmd.ExecuteNonQuery();

                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Имя было успешно изменено");
                                                Console.ResetColor();
                                                Console.WriteLine();

                                                changeContactFlag = false;
                                                newNameIsNotNull = false;
                                            }
                                        }
                                        break;

                                    case "фамилия":
                                        Console.WriteLine();
                                        Console.Write("Введите новую фамилию контакта: ");

                                        string surnameNew = Console.ReadLine();
                                        string updateSurnameQuery = "UPDATE Contacts SET LastName='" + surnameNew + "'  WHERE Number='" + "+7" + contactNumber + "'";


                                        SQLiteCommand updateSurnameCmd = new SQLiteCommand(updateSurnameQuery, readConnection);
                                        updateSurnameCmd.ExecuteNonQuery();


                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Фамилия успешно изменена");
                                        Console.ResetColor();
                                        Console.WriteLine();
                                        changeContactFlag = false;
                                        changeDataFlag = false;
                                        break;

                                    default:
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Недопустимый запрос");
                                        Console.ResetColor();
                                        Console.WriteLine();
                                        break;
                                }


                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Были введены недопустимые символы или недопустимое количество символов, попробуйте еще раз");
                                Console.ResetColor();

                            }
                        }
                        break;

                    case "нет":
                        changeDataFlag = false;
                        break;

                    default:

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Введен неправильный запрос");
                        Console.ResetColor();
                        break;
                }
                readConnection.Close();
            }
        }

        //функция удаления контакта
        private static void DeleteContact()
        {
            SQLiteConnection readConnection = new SQLiteConnection("Data Source = Practica2024DB.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
            bool deleteDataFlag = true;

            while (deleteDataFlag)
            {

                readConnection.Open();
                Console.WriteLine();
                Console.Write("Желаете удалить контакт? (да/нет): ");
                string collapse;
                switch (collapse = Console.ReadLine().ToLower())
                {
                    case "да":
                        Console.WriteLine();
                        Console.Write("Введите номер контакта, который хотите удалить: +7");
                        string deleteNumber = Console.ReadLine();

                        if (deleteNumber.Length == 10 && deleteNumber.All(char.IsDigit))
                        {
                            string updateNameQuery = "DELETE FROM Contacts WHERE Number='" + "+7" + deleteNumber + "'";
                            SQLiteCommand updateNameCmd = new SQLiteCommand(updateNameQuery, readConnection);
                            updateNameCmd.ExecuteNonQuery();
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Контакт был успешно удален");
                            Console.ResetColor();
                            deleteDataFlag = false;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Введены недопустимые символы или не все цифры");
                            Console.ResetColor();
                        }
                        break;


                    case "нет":
                        deleteDataFlag = false;
                        break;


                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Такой команды нет");
                        Console.ResetColor();
                        break;
                }
                readConnection.Close();
            }
        }
    }
}