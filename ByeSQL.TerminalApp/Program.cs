using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ByeSQL.TerminalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ByeSQL = new ByeSQLTester();

            string[] queries = new string[] {
                @"
                CREATE TABLE users (
                    id INT(11) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(30) NOT NULL,
                    email VARCHAR(50),
                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                )",
                "INSERT INTO users (name, email, created_at) VALUES ('danny', 'danny@test.com', '2021-01-01 00:01:10');",
                "SELECT name, created_at, COUNT(*) FROM users WHERE email = 'danny@test.com';"
            };

            for(int i = 0; i < queries.Length; i++)
            {
                ByeSQL.Parse(queries[i]);
            }
        }
    }
}
