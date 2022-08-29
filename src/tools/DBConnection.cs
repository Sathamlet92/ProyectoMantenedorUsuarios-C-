using MySql.Data.MySqlClient;

public class DBConnection
{
    private static string stringConnect = "-OMITTEDs-";
    private static MySqlConnection conn;

    public static MySqlConnection Connection
    {
        get
        {
            if(conn == null)
                conn = new(stringConnect);
            return conn;
        }
    }
}