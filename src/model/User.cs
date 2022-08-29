public class User
{
    private String userName;
    private Int32 id;
    private String password;
    private String email;

    public User(string pUserName, int pId, string pPassword, string pEmail)
    {
        this.userName = pUserName;
        this.id = pId;
        this.password = pPassword;
        this.email = pEmail;
    }
    public User(){}

    public String UserName { get => userName; set => userName = value; }
    public Int32 Id { get => id; set => id = value; }
    public String Password { get => password; set => password = value; }
    public String Email { get => email; set => email = value; }

    public override string ToString()
    {
        return this.id + " " + this.userName + " " + this.Email;
    }
}