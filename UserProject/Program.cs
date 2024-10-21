namespace UserProject
{
    using System;
    using Microsoft.Data.SqlClient;

    class Program
    {
        static string connectionString = "Server=DESKTOP-9PK656A\\SQLEXPRESS;Database=UserProject;Trusted_Connection=True;TrustServerCertificate=True";

        static void Main(string[] args)
        {
            InsertUsers();
            GetUserWithSettings(2);
            DeleteUser(3);
        }

        static void InsertUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertUser = "INSERT INTO Users (Name) VALUES (@Name); SELECT SCOPE_IDENTITY();";
                string insertSettings = "INSERT INTO UserSettings (UserId, Theme, Notifications) VALUES (@UserId, @Theme, @Notifications);";

                for (int i = 1; i <= 3; i++)
                {
                    using (var userCommand = new SqlCommand(insertUser, connection))
                    {
                        userCommand.Parameters.AddWithValue("@Name", $"User {i}");
                        int userId = Convert.ToInt32(userCommand.ExecuteScalar());

                        using (var settingsCommand = new SqlCommand(insertSettings, connection))
                        {
                            settingsCommand.Parameters.AddWithValue("@UserId", userId);
                            settingsCommand.Parameters.AddWithValue("@Theme", "Dark");
                            settingsCommand.Parameters.AddWithValue("@Notifications", i % 2 == 0);
                            settingsCommand.ExecuteNonQuery();
                        }
                    }
                }
                Console.WriteLine("Users and their settings inserted.");
            }
        }

        static void GetUserWithSettings(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                SELECT u.Id, u.Name, s.Theme, s.Notifications
                FROM Users u
                JOIN UserSettings s ON u.Id = s.UserId
                WHERE u.Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Settings = new UserSettings
                                {
                                    Theme = reader.GetString(2),
                                    Notifications = reader.GetBoolean(3)
                                }
                            };
                            Console.WriteLine($"Name: {user.Name}, Theme: {user.Settings.Theme}, Notifications: {user.Settings.Notifications}");
                        }
                    }
                }
            }
        }

        static void DeleteUser(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Users WHERE Id = @Id";

                using (var command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);
                    int affectedRows = command.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {affectedRows} user(s).");
                }
            }
        }
    }

}
