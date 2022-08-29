using System.Data;
using MySql.Data.MySqlClient;

public class UserRepository : ICrud<User>
{
    public User Delete(int id)
    {
        User u = null;
        try
        {
            using(MySqlCommand cmd = this.GetConnection().CreateCommand())
            {
                this.GetConnection().Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM usuarios WHERE id=@val";
                cmd.Parameters.AddWithValue("@val", id);
                MySqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read()) u = this.CreateUser(dr);
                this.GetConnection().Close();
                return u;
            }

        } catch(MySqlException ex)
        {
            System.Console.WriteLine(ex.StackTrace);
            System.Console.WriteLine("Se retornara null");
            return u;
        }
    }

    public User GetById(int id)
    {
        User u = null;
        try
        {
            using(MySqlConnection con = this.GetConnection())
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM usuarios";
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                    u = this.CreateUser(reader);
                return u;
            }            
        } catch(MySqlException ex)
        {
            System.Console.WriteLine(ex.StackTrace +$"\nEl usuario es nulo");
            return u;
        }
    }

    public List<User> Listing()
    {
        List<User> users = new();
        try
        {
            using (MySqlConnection con = this.GetConnection())
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM usuarios";
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    User u = CreateUser(reader);
                    users.Add(u);                
                }
                con.Close();
            }
        return users;            
        }
        catch (MySqlException ex )
        {
            System.Console.WriteLine(ex.Message + $"\nSe retorna una lista vacia");
            return users;
        }

    }

    public bool Save(User t)
    {
        string sql = "";
        if(t.Id == 0) sql = "INSERT INTO usuarios (username, password, email) VALUES(@val1, @val2, @val3)";
        else sql = "UPDATE usuarios SET username=@val1, password=@val2, email=@val3 WHERE id=@val4 ";
        try
        {
            using(MySqlConnection con = this.GetConnection())
            {
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@val1", t.UserName);
                cmd.Parameters.AddWithValue("@val2", t.Password);
                cmd.Parameters.AddWithValue("@val3", t.Email);
                if(t.Id > 0) cmd.Parameters.AddWithValue("@val4", t.Id);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            };

        } catch(MySqlException ex)
        {
            System.Console.WriteLine(ex.StackTrace);
            return false;
        }
    }

    private MySqlConnection GetConnection()
    {
        return DBConnection.Connection;        
    }
    private User CreateUser(MySqlDataReader reader)
    {
        User u = new();
        u.Id = reader.GetInt32("id");
        u.UserName = reader.GetString("username");
        u.Password = reader.GetString("password");
        u.Email = reader.GetString("email");
        return u;
    }
}