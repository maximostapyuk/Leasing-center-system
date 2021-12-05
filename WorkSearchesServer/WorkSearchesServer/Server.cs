using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WorkSearchesServer
{
    class Server
    {
        static private int userCounter = 0;
        static private int status = 0;
        static private string command = "";      

        static public void Run()
        {                  
            SqlCommander.ConnectToDatabase();

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(WorkSQL.GetIP()), WorkSQL.GetPort());
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен и ожидает подключения...");
                Console.WriteLine("Параметры запуска: Порт: "+ WorkSQL.GetPort());
                Console.WriteLine("IP адрес:"+ WorkSQL.GetIP());
                
                Employee employee = new Employee();
                Car car = new Car();
                Client client = new Client();
                LogIn logIn = new LogIn();
                Expert expert = new Expert();
                Clear(employee, car, client, logIn, expert);


                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    if (builder.ToString() == "Клиент подключён")
                    {
                        userCounter++;
                        WriteStatistic();
                    }
                    if (status == 0 && ((builder.ToString() == "GET CLIENT INFO") ||
                                        (builder.ToString() == "SELECT OPERATIONS") ||
                                        (builder.ToString() == "ADD OPERATION") ||
                                        (builder.ToString() == "RED EMPLOYEE ACCESS")||
                                        (builder.ToString() == "DELETE EMPLOYEE")||
                                        (builder.ToString() == "GET MARKS") ||
                                        (builder.ToString() == "EXPERT MARKS")||
                                        (builder.ToString() == "LOG IN") ||
                                        (builder.ToString() == "REGISTRATION") || 
                                        (builder.ToString() == "SELECT CLIENT") ||
                                        (builder.ToString() == "ADD CLIENT")    ||
                                        (builder.ToString() == "DELETE CLIENT") || 
                                        (builder.ToString() == "UPDATE CLIENT") ||
                                        (builder.ToString() == "SELECT CAR") ||
                                        (builder.ToString() == "ADD CAR") ||
                                        (builder.ToString() == "DELETE CAR") ||
                                        (builder.ToString() == "UPDATE CAR")))
                    {
                        command = builder.ToString();
                        ++status;
                    }
                    else
                    {
                        if (status == 1)
                        {
                            switch (command)
                            {
                                case "GET CLIENT INFO":
                                    {
                                        client.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.GetClientInfo(client);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "SELECT OPERATIONS":
                                    {
                                        WorkSQL.info = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectOperations(WorkSQL.info);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "ADD OPERATION":
                                    {
                                        if (client.ID == "")
                                        {
                                            client.ID = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Login == "")
                                            {
                                                client.Login = builder.ToString();
                                            }
                                            else
                                            {
                                                car.VIN = builder.ToString();

                                                string clientFIO = SqlCommander.GetClientFio(client.ID);
                                                string emplFIO = SqlCommander.GetEmplFio(client.Login);
                                                string VIN = SqlCommander.GetCarBrandName(car.VIN);

                                                string response = SqlCommander.AddOperation(clientFIO, emplFIO, VIN);
                                                data = Encoding.Unicode.GetBytes(response);
                                                handler.Send(data);
                                                Clear(employee, car, client, logIn, expert);
                                            }
                                        }
                                    }
                                    break;
                                case "DELETE EMPLOYEE":
                                    {
                                        employee.ID = builder.ToString();
                                        string response = SqlCommander.DelEmployee(employee);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "RED EMPLOYEE ACCESS":
                                    {
                                        employee.ID = builder.ToString();
                                        string response = SqlCommander.ChangeEmplAccess(employee.ID);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "GET MARKS":
                                    {
                                        expert.ExpertNum = builder.ToString();
                                        DataTable dataTable = SqlCommander.GetMarks(expert);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "EXPERT MARKS":
                                    {
                                        if (expert.ExpertNum == "")
                                        {
                                            expert.ExpertNum = builder.ToString();
                                        }
                                        else
                                        {
                                            if (expert.Mark1_2 == "")
                                            {
                                                expert.Mark1_2 = builder.ToString();
                                            }
                                            else
                                            {
                                                if (expert.Mark1_3 == "")
                                                {
                                                    expert.Mark1_3 = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (expert.Mark1_4 == "")
                                                    {
                                                        expert.Mark1_4 = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (expert.Mark2_3 == "")
                                                        {
                                                            expert.Mark2_3 = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (expert.Mark2_4 == "")
                                                            {
                                                                expert.Mark2_4 = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                expert.Mark3_4 = builder.ToString();
                                                                string response = SqlCommander.ExpertMark(expert);
                                                                data = Encoding.Unicode.GetBytes(response);
                                                                handler.Send(data);
                                                                Clear(employee, car, client, logIn, expert);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "LOG IN":
                                    {
                                        if (employee.Login == "")
                                        {
                                            employee.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            employee.Password = builder.ToString();
                                            string response = SqlCommander.LogIn(employee);
                                            data = Encoding.Unicode.GetBytes(response);
                                            handler.Send(data);
                                            Clear(employee,car,client,logIn,expert);
                                        }
                                    }break;
                                case "REGISTRATION":
                                    {
                                        if (employee.Login == "")
                                        {
                                            employee.Login = builder.ToString();
                                        }
                                        else
                                        {
                                            if (employee.Password == "")
                                            {
                                                employee.Password = builder.ToString();
                                            }
                                            else
                                            {
                                                if (employee.Surname == "")
                                                {
                                                    employee.Surname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (employee.Name == "")
                                                    {
                                                        employee.Name = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (employee.Thirdname == "")
                                                        {
                                                            employee.Thirdname = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (employee.Email == "")
                                                            {
                                                                employee.Email = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (employee.Adress == "")
                                                                {
                                                                    employee.Adress = builder.ToString();
                                                                }
                                                                else
                                                                {
                                                                    if (employee.PhoneCode == "")
                                                                    {
                                                                        employee.PhoneCode = builder.ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (employee.Phone == "")
                                                                        {
                                                                            employee.Phone = builder.ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (employee.Gender == "")
                                                                            {
                                                                                employee.Gender = builder.ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (employee.Age == "")
                                                                                {
                                                                                    employee.Age = builder.ToString();
                                                                                }
                                                                                else
                                                                                {
                                                                                    employee.Access = builder.ToString();
                                                                                    string response = SqlCommander.Registration(employee);
                                                                                    data = Encoding.Unicode.GetBytes(response);
                                                                                    handler.Send(data);
                                                                                    Clear(employee, car, client, logIn, expert);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    } break;
                                case "SELECT CLIENT":
                                    {
                                        client.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectClient(client);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "ADD CLIENT":
                                    {
                                        if (client.Name == "")
                                        {
                                            client.Name = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Surname == "")
                                            {
                                                client.Surname = builder.ToString();
                                            }
                                            else
                                            {
                                                if (client.Thirdname == "")
                                                {
                                                    client.Thirdname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (client.Gender == "")
                                                    {
                                                        client.Gender = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (client.Email == "")
                                                        {
                                                            client.Email = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (client.PhoneCode == "")
                                                            {
                                                                client.PhoneCode = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (client.Phone == "")
                                                                {
                                                                    client.Phone = builder.ToString();
                                                                }
                                                                else
                                                                {
                                                                    if (client.Age == "")
                                                                    {
                                                                        client.Age = builder.ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        client.Adress = builder.ToString();
                                                                        string response = SqlCommander.AddClient(client);
                                                                        data = Encoding.Unicode.GetBytes(response);
                                                                        handler.Send(data);
                                                                        Clear(employee, car, client, logIn, expert);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "DELETE CLIENT":
                                    {
                                        client.ID = builder.ToString();
                                        string response = SqlCommander.DelClient(client);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "UPDATE CLIENT":
                                    {
                                        if (client.ID == "")
                                        {
                                            client.ID = builder.ToString();
                                        }
                                        else
                                        {
                                            if (client.Name == "")
                                            {
                                                client.Name = builder.ToString();
                                            }
                                            else
                                            {
                                                if (client.Surname == "")
                                                {
                                                    client.Surname = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (client.Thirdname == "")
                                                    {
                                                        client.Thirdname = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (client.Gender == "")
                                                        {
                                                            client.Gender = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (client.Email == "")
                                                            {
                                                                client.Email = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (client.Phone == "")
                                                                {
                                                                    client.Phone = builder.ToString();
                                                                }
                                                                else
                                                                {
                                                                    if (client.PhoneCode == "")
                                                                    {
                                                                        client.PhoneCode = builder.ToString();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (client.Age == "")
                                                                        {
                                                                            client.Age = builder.ToString();
                                                                        }
                                                                        else
                                                                        {
                                                                            client.Adress = builder.ToString();
                                                                            string response = SqlCommander.ChangeClient(client);
                                                                            data = Encoding.Unicode.GetBytes(response);
                                                                            handler.Send(data);
                                                                            Clear(employee, car, client, logIn, expert);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "SELECT CAR":
                                    {
                                        car.Search = builder.ToString();
                                        DataTable dataTable = SqlCommander.SelectCar(car);
                                        byte[] responseData = GetBinaryFormatData(dataTable);
                                        handler.Send(responseData);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "ADD CAR":
                                    {
                                        if (car.GearBox == "")
                                        {
                                            car.GearBox = builder.ToString();
                                        }
                                        else
                                        {
                                            if (car.Currency == "")
                                            {
                                                car.Currency = builder.ToString();
                                            }
                                            else
                                            {
                                                if (car.Speed == "")
                                                {
                                                    car.Speed = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (car.Price == "")
                                                    {
                                                        car.Price = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (car.Description == "")
                                                        {
                                                             car.Description = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (car.Brand == "")
                                                            {
                                                                car.Brand = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (car.VIN == "")
                                                                {
                                                                    car.VIN = builder.ToString();
                                                                }
                                                                else
                                                                {
                                                                    car.Name = builder.ToString();
                                                                    string response = SqlCommander.AddCar(car);
                                                                    data = Encoding.Unicode.GetBytes(response);
                                                                    handler.Send(data);
                                                                    Clear(employee, car, client, logIn, expert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "DELETE CAR":
                                    {
                                        car.VIN = builder.ToString();
                                        string response = SqlCommander.DelCar(car.VIN);
                                        data = Encoding.Unicode.GetBytes(response);
                                        handler.Send(data);
                                        Clear(employee, car, client, logIn, expert);
                                    }
                                    break;
                                case "UPDATE CAR":
                                    {
                                        if (car.VIN == "")
                                        {
                                            car.VIN = builder.ToString();
                                        }
                                        else
                                        {
                                            if (car.GearBox == "")
                                            {
                                                car.GearBox = builder.ToString();
                                            }
                                            else
                                            {
                                                if (car.Currency == "")
                                                {
                                                    car.Currency = builder.ToString();
                                                }
                                                else
                                                {
                                                    if (car.Speed == "")
                                                    {
                                                        car.Speed = builder.ToString();
                                                    }
                                                    else
                                                    {
                                                        if (car.Price == "")
                                                        {
                                                            car.Price = builder.ToString();
                                                        }
                                                        else
                                                        {
                                                            if (car.Name == "")
                                                            {
                                                                car.Name = builder.ToString();
                                                            }
                                                            else
                                                            {
                                                                if (car.Brand == "")
                                                                {
                                                                    car.Brand = builder.ToString();
                                                                }
                                                                else
                                                                {
                                                                    car.Description = builder.ToString();
                                                                    string response = SqlCommander.ChangeCar(car);
                                                                    data = Encoding.Unicode.GetBytes(response);
                                                                    handler.Send(data);
                                                                    Clear(employee, car, client, logIn, expert);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(builder.ToString());
                            string message = "Ваше сообщение доставлено";
                            data = Encoding.Unicode.GetBytes(message);
                            handler.Send(data);
                        }
                    }
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Serialize DataTable for Select
        static byte[] GetBinaryFormatData(DataTable dt)
        {
            BinaryFormatter bFormat = new BinaryFormatter();
            byte[] outList = null;
            dt.RemotingFormat = SerializationFormat.Binary;
            using (MemoryStream ms = new MemoryStream())
            {
                bFormat.Serialize(ms, dt);
                outList = ms.ToArray();
            }
            return outList;
        }
        static public void WriteStatistic()
        {
            Console.WriteLine("\r\n За время работы сервера количество подключенных клиентов: " + userCounter + ".\r\n");
        }
        static private void Clear(Employee employee, Car car, Client client, LogIn logIn, Expert expert)
        {
            car.Clean();
            client.Clean();
            employee.Clean();
            logIn.Clean();
            expert.Clean();
            
            status = 0;
            command = "";         
        }
    }
}
