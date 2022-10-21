using System;
using System.Collections.Generic;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TextEditor
{
    internal class AccountList
    {
        static string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string sFile = Path.Combine(sCurrentDirectory, @"..\..\login.txt");

        public static string Login { get; set; }
        public static string Type { get { return Users[Login].User_Type; } }
        public static Dictionary<string, User> Users { get; set; }

        // static string sFile = Path.Combine(sCurrentDirectory, @"..\..\abc.txt");

        public void Read()
        {
            Users = new Dictionary<string, User>();

            string[] lines = File.ReadAllLines(sFile);

            foreach (string line in lines)
            {
                string[] info = line.Split(',');
                User user = new User
                {
                    Name = info[0],
                    Password = info[1],
                    User_Type = info[2],
                    First_Name = info[3],
                    Last_Name = info[4],
                    Birth_Date = info[5]
                };

                Users.Add(user.Name, user);
            }
        }

        public void Close()
        {
            using (StreamWriter outputFile = new StreamWriter(sFile))
            {
                foreach (User item in Users.Values)
                {
                    string line = item.toString();

                    outputFile.WriteLine(line);
                }
            }
        }

        public void Add(User user)
        {
            try
            {
                Users.Add(user.Name, user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool Valid(string userName, string password)
        {
            if (Users[userName].Password == password)
            {
                return true;
            }

            return false;
        }
    }

    internal class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string User_Type { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Birth_Date { get; set; }

        public string toString()
        {
            return Name + ',' + Password + ',' + User_Type + ',' + First_Name + ',' + Last_Name + ',' + Birth_Date;
        }
    }
}
