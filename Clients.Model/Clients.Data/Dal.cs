using System.Data.SqlClient;
using Dapper;


public class Dal
{
    //משנים לפי שם הקובץ שלכם
    private const string DB_NAME = "DB_User";
    //משנים לפי שם הפרויקט שלכם שבו נמצא קובץ מסד הנתונים
    private const string DATA_PROJECT_NAME = "Clients.Data";

    private static string GetConnectionString()
    {
        string dbName = DB_NAME;
        if (!dbName.Contains(".mdf")) dbName += ".mdf";

        //בניית מחרוזת הקישור
        string curAttachDbFilename = $@"{Directory.GetParent(Environment.CurrentDirectory)}\{DATA_PROJECT_NAME}\{dbName}";

        string cString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={curAttachDbFilename};Integrated Security=True";

        return cString;
    }

    public static async Task<IEnumerable<T>> QuerySql<T>(string sql, object parameters = null)
    {
        string _connectionString = GetConnectionString();
        var connection = new SqlConnection(_connectionString);

        try
        {
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(sql, parameters);
            return result;
        }
        catch (Exception ex)
        {
            //חשוב להשאיר כאן נקודת עצירה - כך אם תהיה שגיאה בשליפה, יהיה קל יותר לזהות אותה
            Console.WriteLine(ex.Message);
            return null;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public static async Task<IEnumerable<T>> QuerySql<T>(
    string sql,
    Type[] types,
    Func<object[], T> map,
    object parameters = null,
    string splitOn = null)
    {
        string _connectionString = GetConnectionString();
        await using var connection = new SqlConnection(_connectionString);

        try
        {
            await connection.OpenAsync();

            // Use Dapper to query with dynamic mapping for arbitrary types
            var result = await connection.QueryAsync(
                sql,
                types,
                map,
                parameters,
                splitOn: splitOn);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }


    public static async Task ExecuteSql(string sql, object parameters)
    {
        string _connectionString = GetConnectionString();
        var connection = new SqlConnection(_connectionString);

        try
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, parameters);
        }
        catch (Exception ex)
        {
            //חשוב להשאיר כאן נקודת עצירה - כך אם תהיה שגיאה בשמירה, יהיה קל יותר לזהות אותה
            Console.WriteLine(ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}