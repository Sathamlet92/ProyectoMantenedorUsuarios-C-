

internal class Program
{
    private static void Main(string[] args)
    {
        int option = 0;
        Dictionary<string, int> options = new();
        options.Add("Crear", 1);
        options.Add("Update", 2);
        options.Add("Listar", 3);
        options.Add("Borrar", 4);
        options.Add("Salir", 5);
        string val = null;
        ICrud<User> repo = new UserRepository();

        do
        {
            foreach (KeyValuePair<string, int> values in options)
            {
                Console.Write(values.Key + " #" + values.Value + " | ");
            }
            Console.WriteLine("Mantenedo de usuarios");
            Console.WriteLine("Elige una opcion para continuar");
            try
            {
                val = Console.ReadLine();
                System.Console.WriteLine("\n");
                if (int.TryParse(val, out option))
                {
                    switch (option)
                    {
                        case 1:
                        case 2:
                            User u = CreateUser(option);
                            if(u.UserName.Length > 0 && u.Email.Length > 0 && u.Password.Length > 0 && repo.Save(u) )
                            {
                                System.Console.WriteLine("Usuario creado satisfactoriamente\n");
                            } else System.Console.WriteLine("Error al intentar agregar usuario\n");
                            break;
                        case 3:
                            repo.Listing().ForEach(p => System.Console.WriteLine(p));
                            System.Console.WriteLine("\n");
                            break;
                        case 4:
                            System.Console.Write("Igresa el Id que quieres eliminar: ");
                            int delVal = Convert.ToInt32(Console.ReadLine());
                            repo.Delete(delVal);
                            break;
                        case 5:
                            option = 5;
                            System.Console.WriteLine("Saliendo del mantendor de usuario...");
                            Thread.Sleep(1500);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Vuelvelo a intentar");
                    Thread.Sleep(2000);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Debes ingresar un numero de la opcion\n");
            } catch (FormatException ex)
            {
                Console.WriteLine("Debes ingresar un un ID valido\n");
            }

        } while (option != 5);
    }
    private static User CreateUser(int opcion)
    {
        User u = new();
        try
        {                
            Console.Write("Ingresa el nombre de usuario: ");
            u.UserName = Convert.ToString(Console.ReadLine());
            Console.Write("Ingresa la contraseña: ");
            u.Password = Console.ReadLine();
            System.Console.Write("Ingresa el correo electronico: ");
            u.Email = Console.ReadLine();
            if(opcion == 2)
            {
                System.Console.WriteLine("Ingresa el ID que quieres modificar");
                u.Id = Convert.ToInt32(Console.ReadLine());

            } 

        } catch(FormatException ex)
        {
            System.Console.WriteLine("Debes ingresar un id valido: " + ex.Message);
        }
        return u;
    }
}