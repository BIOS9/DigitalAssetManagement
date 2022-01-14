using System;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DAMLib.Database.MySql.Configuration
{
    public class MySqlOptionsValidation : IValidateOptions<MySqlOptions>
    {
        public ValidateOptionsResult Validate(string name, MySqlOptions options)
        {
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(options.ConnectionString);
                conn.Open();
            }
            catch (ArgumentException ex)
            {
                return ValidateOptionsResult.Fail($"Invalid MySql connection string. {ex.Message}");
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        return ValidateOptionsResult.Fail($"Failed to connect to MySql DB host. {ex.Message}");
                    case 0: // Access denied (Check DB name,username,password)
                        return ValidateOptionsResult.Fail($"Failed to connect to MySql DB. Access denied. {ex.Message}");
                    default:
                        return ValidateOptionsResult.Fail($"Failed to connect to MySql DB. {ex.Message}");
                }
            }
            finally
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ValidateOptionsResult.Success;
        }
    }
}