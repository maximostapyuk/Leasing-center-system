using System;
using System.Data;
using System.Data.SqlClient;

namespace WorkSearchesServer
{
    static class SqlCommander
    {
        static private SqlConnection sqlConnection;

        static public void ConnectToDatabase()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = @"Data Source=HOME-PC\SQLEXPRESS;Initial Catalog=LeasingBase;Integrated Security=True";
            sqlConnection.Open();
        }

        static public DataTable SelectClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (client.Search == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Person]";
            }
            else
            {
                Console.WriteLine("Поиск в таблице Клиент: " + client.Search);
                sqlCommand.CommandText = "SELECT * FROM [Person] WHERE (surname='" + client.Search + "')";
            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Person");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Клиентов ");
            return dataTable;
        }

        static public DataTable SelectCar(Car car)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                if (car.Search == "@")
                {
                    sqlCommand.CommandText = "SELECT * FROM [Car]";
                }
                else
                {
                    Console.WriteLine("Поиск в таблице Автомобили: " + car.Search);
                    sqlCommand.CommandText = "SELECT * FROM [Car] WHERE(VIN='" + car.Search + "') OR (brand='" + car.Search + "') OR (name='" + car.Search + "')";
                }
                sqlCommand.Connection = sqlConnection;


                
            }
              catch(System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Car");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод Данных об автомобиле");
            return dataTable;
        }
        static public DataTable GetClientInfo(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT * FROM [Person] WHERE (ID='" + int.Parse(client.Search) + "')";
            
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Person");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Получение данных клиента по ID ");
            return dataTable;
        }
        //clientName, clientSurname, clientThirdname, clientGender, clientEmail, clientAdress, clientPhoneCode, clientPhone, clientAge
        static public string AddClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Person]( gender, name, surname, thirdname, email, address, phone_code, phone, age) " +
                "VALUES(@clGender, @clName, @clSurname, @clThirdname, @clEmail, @clAddress, @clPhoneCode, @clPhone,@clAge)";
            
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clAddress", client.Adress);
            sqlCommand.Parameters.AddWithValue("@clPhoneCode", client.PhoneCode);
            sqlCommand.Parameters.AddWithValue("@clPhone", int.Parse(client.Phone));
            sqlCommand.Parameters.AddWithValue("@clAge", int.Parse(client.Age));
                   
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавление Клиента ";
                Console.WriteLine(response + " (Возраст: " + client.Age + ") (Имя клиента: " + client.Name + ") (Фамилия:  " + client.Surname + ") (Отчество: " + client.Thirdname + ") " +
                    "(Пол: " + client.Gender + ") (Email: " + client.Email + ") (Адрес: " + client.Adress + ") (Код телефона: " + client.PhoneCode + ") (Телефон: " + client.Phone + ")");
                return response;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string response = "База Данных: Клиент не был добавлен, введены неверные данные!  ";
                Console.WriteLine(response);
                Console.WriteLine(ex.Message);
                return response;
            }
        }

        static public string AddCar(Car car)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Car](VIN, price, gearbox, description, price_currency, brand, name,max_speed)" +
                "values(@crVIN, @crPrice, @crGearBox, @crDescription, @crCurrency, @crBrand, @crName,@crSpeed)";
                        
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@crVin", car.VIN);
            sqlCommand.Parameters.AddWithValue("@crPrice", int.Parse(car.Price));
            sqlCommand.Parameters.AddWithValue("@crGearBox", car.GearBox);
            sqlCommand.Parameters.AddWithValue("@crDescription", car.Description);
            sqlCommand.Parameters.AddWithValue("@crCurrency", car.Currency);
            sqlCommand.Parameters.AddWithValue("@crBrand", car.Brand);
            sqlCommand.Parameters.AddWithValue("@crName", car.Name);
            sqlCommand.Parameters.AddWithValue("@crSpeed", int.Parse(car.Speed));
            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "База Данных: Добавлены данные об автомобиле ";
                Console.WriteLine(response + " (VIN: " + car.VIN + ") (Коробка передач: " + car.GearBox + ") (Стоимость:  " + car.Currency + ") (Название: " + car.Name + ")");
                return response;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "База Данных: Информация  об автомобиле не добавлена ";
                Console.WriteLine(response);
                return response;
            }
        }

        static public string DelEmployee(Employee employee)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Employees] WHERE ID = @id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@id", employee.ID);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Сотрудник не удален,такого сотрудника не существует ";
                Console.WriteLine(response + " " + employee.ID);
                return response;
            }
            else
            {
                string response = "Сотрудник удален ";
                Console.WriteLine(response + " " + employee.ID);
                return response;
            }
        }
        static public string DelClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Person] WHERE Id = @Id";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@Id", client.ID);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Клиент не удален ";
                Console.WriteLine(response + " " + client.ID);
                return response;
            }
            else
            {
                string response = "База Данных: Клиент удален ";
                Console.WriteLine(response + " " + client.ID);
                return response;
            }
        }

        static public string DelCar(string VIN)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "DELETE FROM [Car] WHERE VIN = @crVIN";

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@crVIN", VIN);
            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Информация об автомобиле не удалена ";
                Console.WriteLine(response + " " + VIN);
                return response;
            }
            else
            {
                string response = "База Данных: Информация об автомобиле удалена ";
                Console.WriteLine(response + " " + VIN);
                return response;
            }
        }
        static public DataTable GetMarks(Expert expert)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {                
                Console.WriteLine("Получение отметок " + expert.ExpertNum+ " эксперта. ");
                sqlCommand.CommandText = "SELECT * FROM [Expert] WHERE(expertNum='" + expert.ExpertNum + "')";
                
                sqlCommand.Connection = sqlConnection;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Expert");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("Получение отметок экспертов");
            return dataTable;
        }
        static public DataTable GetClients(string sqlInfo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            try
            {
                Console.WriteLine("Запрос данных о пользователях. ");
                if(sqlInfo == "All")
                {
                    sqlCommand.CommandText = "SELECT * FROM [Employees]";
                }
                else
                    sqlCommand.CommandText = "SELECT * FROM [Employees] Where access = '1'";

                sqlCommand.Connection = sqlConnection;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable("Employees");
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        static public string ChangeClient(Client client)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Person] SET gender = @clGender, name = @clName, surname = @clSurname, thirdname = @clThirdname," +
                "email = @clEmail, address = @clAddress, phone_code = @clPhoneCode, phone=@clPhone, age=@clAge Where ID = @clId";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clId", Convert.ToDecimal(client.ID));
            sqlCommand.Parameters.AddWithValue("@clGender", client.Gender);
            sqlCommand.Parameters.AddWithValue("@clName", client.Name);
            sqlCommand.Parameters.AddWithValue("@clSurname", client.Surname);
            sqlCommand.Parameters.AddWithValue("@clThirdname", client.Thirdname);
            sqlCommand.Parameters.AddWithValue("@clEmail", client.Email);
            sqlCommand.Parameters.AddWithValue("@clAddress", client.Adress);
            sqlCommand.Parameters.AddWithValue("@clPhoneCode", client.PhoneCode);
            sqlCommand.Parameters.AddWithValue("@clPhone", Convert.ToDecimal(client.Phone));
            sqlCommand.Parameters.AddWithValue("@clAge", Convert.ToDecimal(client.Age));

            try
            {
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    string response = "База Данных: Данные о клиенте не изменены ";
                    Console.WriteLine(response );

                    return response;
                }
                else
                {
                    string response = "База Данных: Данные клиента изменены ";
                    Console.WriteLine(response + " (Id: " + client.ID + ") (Имя клиента: " + client.Name + ") (Фамилия:  " + client.Surname + ") (Отчество: " + client.Thirdname + ") " +
                        "(Пол: " + client.Gender + ") (Email: " + client.Email + ") (Адрес: " + client.Adress + ") (Код телефона: " + client.PhoneCode + ") (Телефон: " + client.Phone + ") (Возраст: " + client.Age + ")");

                    return response;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string response = "rtgfhghg";
                Console.WriteLine(ex.Message);
                return response;
            }
            
        }

        static public string ChangeCar(Car car)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Car] SET brand = @crBrand, name = @crName, " +
                "description = @crDescription, gearbox = @crGearBox, price_currency = @crCurrency, price = @crPrice, max_speed=@crSpeed Where VIN = @crVin";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@crVin", car.VIN);
            sqlCommand.Parameters.AddWithValue("@crPrice", int.Parse(car.Price));
            sqlCommand.Parameters.AddWithValue("@crGearBox", car.GearBox);
            sqlCommand.Parameters.AddWithValue("@crDescription", car.Description);
            sqlCommand.Parameters.AddWithValue("@crCurrency", car.Currency);
            sqlCommand.Parameters.AddWithValue("@crBrand", car.Brand);
            sqlCommand.Parameters.AddWithValue("@crName", car.Name);
            sqlCommand.Parameters.AddWithValue("@crSpeed", int.Parse(car.Speed));

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "База Данных: Данные об автомобиле не изменены ";
                Console.WriteLine(response);
                return response;
            }
            else
            {
                string response = "База Данных: Данные об автомобиле изменены ";
                Console.WriteLine(response + " (VIN: " + car.VIN + ") (Тип коробки передач: " + car.GearBox + ") (Валюта:  " + car.Currency + ") (Скорость: " + car.Speed + ")");
                return response;
            }
        }

        static public string Registration(Employee employee)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Employees]( login, password, surname, name," +
                "thirdname, email, address, phone_code, phone,gender, age, access) " +
                "VALUES(@empLogin, @empPassword, @empSurname, @empName, @empThirdname, @empEmail, " +
                "@empAddress, @empPhoneCode, @empPhone, @empGender, @empAge, @empAccess)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@empLogin", employee.Login);
            sqlCommand.Parameters.AddWithValue("@empPassword", employee.Password);
            sqlCommand.Parameters.AddWithValue("@empSurname", employee.Surname);
            sqlCommand.Parameters.AddWithValue("@empName", employee.Name);
            sqlCommand.Parameters.AddWithValue("@empThirdname", employee.Thirdname);
            sqlCommand.Parameters.AddWithValue("@empEmail", employee.Email);
            sqlCommand.Parameters.AddWithValue("@empAddress", employee.Adress);
            sqlCommand.Parameters.AddWithValue("@empPhoneCode", employee.PhoneCode);
            sqlCommand.Parameters.AddWithValue("@empPhone", int.Parse(employee.Phone));
            sqlCommand.Parameters.AddWithValue("@empGender", employee.Gender);
            sqlCommand.Parameters.AddWithValue("@empAge", int.Parse(employee.Age));
            sqlCommand.Parameters.AddWithValue("@empAccess", int.Parse(employee.Access));

            try
            {
                sqlCommand.ExecuteNonQuery();
                string response = "Регистрация прошла успешно!";
                Console.WriteLine(response);
                return response;

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string response = "Регистрация закончилась ошибкой!  ";
                Console.WriteLine(response);
                Console.WriteLine(ex.Message);
                return response;
            }
        }
        static public string LogIn(Employee employee)
        {
            Console.WriteLine("Запрос на подключение пользователя: Login: " + employee.Login + " Password: " + employee.Password);
            string response = "FALSE";
            SqlCommand sqlCommand = new SqlCommand();
            
            sqlCommand.CommandText = "SELECT * FROM [Employees] WHERE (login='" + employee.Login + "') AND (password='" + employee.Password + "') AND (access='1')";
            
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Employees");
            sqlDataAdapter.Fill(dataTable);
            if(dataTable.Rows.Count>0)
            {
                Console.WriteLine("Подключен пользователь! Login: " + employee.Login + " Password: " + employee.Password); 
                return "USER";
            }
            sqlCommand.CommandText = "SELECT * FROM [Employees] WHERE (login='" + employee.Login + "') AND (password='" + employee.Password + "') AND (access='2')";

            sqlCommand.Connection = sqlConnection;

            sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            dataTable = new DataTable("Employees");
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                Console.WriteLine("Подключен администратор! Login: " + employee.Login + " Password: " + employee.Password);
                return "ADMIN";
            }
            
            return response;
        }

        static public string ExpertMark(Expert expert)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Expert] SET mark1_2 = @exMark1_2, mark1_3 = @exMark1_3, " +
                "mark1_4 = @exMark1_4, mark2_3 = @exMark2_3, mark2_4 = @exMark2_4, mark3_4 = @exMark3_4 Where expertNum = @exNum";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@exNum", int.Parse(expert.ExpertNum));
            sqlCommand.Parameters.AddWithValue("@exMark1_2", int.Parse(expert.Mark1_2));
            sqlCommand.Parameters.AddWithValue("@exMark1_3", int.Parse(expert.Mark1_3));
            sqlCommand.Parameters.AddWithValue("@exMark1_4", int.Parse(expert.Mark1_4));
            sqlCommand.Parameters.AddWithValue("@exMark2_3", int.Parse(expert.Mark2_3));
            sqlCommand.Parameters.AddWithValue("@exMark2_4", int.Parse(expert.Mark2_4));
            sqlCommand.Parameters.AddWithValue("@exMark3_4", int.Parse(expert.Mark3_4));

            if (sqlCommand.ExecuteNonQuery() == 0)
            {
                string response = "Данные об оценках экспертов не изменены ";
                Console.WriteLine(response);
                return response;
            }
            else
            {
                string response = "Данные об оценках отредактированы ";
                Console.WriteLine(response);
                return response;
            }
        }
        static public string ChangeEmplAccess(string id)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "UPDATE [Employees] SET access = '2' Where ID = @clId and access='1'";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clId", Convert.ToDecimal(id));            

            try
            {
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    string response = " Данные о сотруднике не изменены, такого сотрудника не существует ";
                    Console.WriteLine("База Данных:"+response + "(Id: " + id + ")");

                    return response;
                }
                else
                {
                    string response = "Данные сотруднике изменены ";
                    Console.WriteLine("База Данных: "+response + " (Id: " + id + ")");

                    return response;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string response = "Сотрудника с таким ID не существует";
                Console.WriteLine(ex.Message);
                return response;
            }

        }
        static public string AddOperation(string clientId, string sqlInfo, string carVIN)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "INSERT into [Leasing](clientFIO, emplFIO, carBrandName) values(@clFIO, @empFIO, @crBrandName)";

            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue("@clFIO", clientId);
            sqlCommand.Parameters.AddWithValue("@empFIO", sqlInfo);
            sqlCommand.Parameters.AddWithValue("@crBrandName", carVIN);

            try
            {
                sqlCommand.ExecuteNonQuery();
                string response =  "Добавлена операция лизинга ";
                Console.WriteLine("База Данных: "+response + "(ФИО клиента: " + clientId + ") (ФИО сотрудника: " + sqlInfo + ") (VIN автомобиля:  " + carVIN + ")");
                return response;
            }
            catch (System.Data.SqlClient.SqlException)
            {
                string response = "Операция лизинга не добавлена";
                Console.WriteLine(response);
                return response;
            }
        }
        static public string GetClientFio(string clientID)
        {
            SqlCommand sqlCommand = new SqlCommand();
            
            sqlCommand.CommandText = "SELECT * FROM [Person] WHERE (ID='" + int.Parse(clientID) + "')";
            
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Person");
            sqlDataAdapter.Fill(dataTable);

            string FIO = dataTable.Rows[0][2].ToString() + " " + dataTable.Rows[0][3].ToString() + " " + dataTable.Rows[0][4].ToString();

            return FIO;
        }
        static public string GetEmplFio(string empLogin)
        {
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM [Employees] WHERE (login='" + empLogin + "')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Employees");
            sqlDataAdapter.Fill(dataTable);

            string FIO = dataTable.Rows[0][3].ToString() + " " + dataTable.Rows[0][4].ToString() + " " + dataTable.Rows[0][5].ToString();

            return FIO;
        }
        static public string GetCarBrandName(string VIN)
        {
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = "SELECT * FROM [Car] WHERE (VIN='" + VIN + "')";

            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Car");
            sqlDataAdapter.Fill(dataTable);

            string brand_name = dataTable.Rows[0][1].ToString() + " " + dataTable.Rows[0][2].ToString();

            return brand_name;
        }
        static public DataTable SelectOperations(string sqlInfo)
        {
            SqlCommand sqlCommand = new SqlCommand();
            if (sqlInfo == "@")
            {
                sqlCommand.CommandText = "SELECT * FROM [Leasing]";
            }            
            else
            {
                sqlCommand.CommandText = "SELECT * FROM [Leasing] Where operation_data Between '2021-"+ int.Parse(sqlInfo) +"- 01' And '2021-"+int.Parse(sqlInfo)+"-30'";

            }
            sqlCommand.Connection = sqlConnection;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable("Leasing");
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine("База Данных: Вывод операций лизинга ");
            return dataTable;
        }

        //Get the number of rows in table
        static public int GetCount(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT COUNT(*) FROM" + tableName;
            sqlCommand.Connection = sqlConnection;
            return (int)sqlCommand.ExecuteScalar();
        }

        static public int GetId(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SELECT MAX(Id) FROM " + tableName;
            sqlCommand.Connection = sqlConnection;
            object obj = sqlCommand.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                return 0;
            }
            else
            {
                int id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return id;
            }
        }
    }
}
