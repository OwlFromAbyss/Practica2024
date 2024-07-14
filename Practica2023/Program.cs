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
            SQLiteConnection connection = new SQLiteConnection("Data Source = ПрактикаБД.db;version=3");
            SQLiteConnection readConnection = new SQLiteConnection("Data Source = ПрактикаБД.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
            SQLiteCommand cmd;
            

            string commandNumber;
            bool flag = true;
            while (flag)
            {
                
                Commands();

                commandNumber = Console.ReadLine();

                switch (commandNumber)
                {
                    case "1":

                        Console.Clear();
                        bool flagFirstCase = true;
                        while (flagFirstCase)
                        {
                            Console.WriteLine();

                            string correctName = NameEnter();
                            string correctSurname = SurnameEnter();
                            string correctNumber = "+7" + NumberEnter();
                            string query = "INSERT INTO Контакты (Имя, Фамилия, Телефон) VALUES ('" + correctName + "','" + correctSurname + "','" + correctNumber + "')";

                            connection.Open();
                            cmd = new SQLiteCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            flagFirstCase = false;

                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Контакт был успешно записан");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                        break;

                    case "2":
                        Console.WriteLine();
                        
                        ShowContacts();

                        bool deleteNumberFlag = true;
                        while (deleteNumberFlag) {
                        Console.WriteLine("Желаете удалить контакт?");
                        switch(Console.ReadLine().ToLower())
                        {
                            case "да":
                                Console.WriteLine();
                                Console.Write("Введите номер контакта который хотите удалить: ");
                                string deleteThisNumber = Console.ReadLine();

                                readConnection.Open();
                                string deleteQuery = $"DELETE FROM Контакты WHERE ID = {deleteThisNumber}";
                                SQLiteCommand cmdDelete = new SQLiteCommand(deleteQuery, readConnection);

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
                        }
                        
                        Console.WriteLine();

                        break;


                    case "3":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nЗавершение программы");
                        Console.ResetColor();
                        flag = false;
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Такой команды не существует, попробуйте еще раз\n");
                        
                        Console.ResetColor();
                        break;
                }
            }

        }

        private static void Commands()
        {

            Console.WriteLine("1 - Добавить контакт");
            Console.WriteLine("2 - Просмотр всех имеющихся контактов и удаление выбранного");
            Console.WriteLine("3 - Выход из приложения\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Выберите необходимое действие: ");
            Console.ResetColor();
        }
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
                    Console.WriteLine("Неправильное количество цифр, попробуйте еще раз");
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

        static void ShowContacts()
        {
            SQLiteConnection readConnection = new SQLiteConnection("Data Source = ПрактикаБД.db;AttachDbFilename=|DataDirectory|\\ПрактикаБД.db;version=3");
            SQLiteCommand cmdRead;
            string readQuery = "SELECT * FROM Контакты ORDER BY ID";
            cmdRead = new SQLiteCommand(readQuery, readConnection);

            readConnection.Open();
            SQLiteDataReader reader = cmdRead.ExecuteReader();

            List<string[]> contactsData = new List<string[]>();

            while (reader.Read())
            {
                contactsData.Add(new string[3]);
                contactsData[contactsData.Count - 1][0] = reader[1].ToString();
                contactsData[contactsData.Count - 1][1] = reader[2].ToString();
                contactsData[contactsData.Count - 1][2] = reader[3].ToString();

            }

            for (int i = 0; i < contactsData.Count; i++)
                Console.WriteLine(i + 1 + ") " + contactsData[i][0] + " " + contactsData[i][1] + " \nНомер: " + contactsData[i][2] + "\n");

            reader.Close();
            readConnection.Close();
        }

    }
}
