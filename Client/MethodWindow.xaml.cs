using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MethodWindow.xaml
    /// </summary>
    public partial class MethodWindow : Window
    {
        private static int timeout = 100;
        private bool checkClick_1 = false;
        private bool checkClick_2 = false;
        public static double target1;
        public static double target2;
        public static double target3;
        public static double target4;
        public MethodWindow()
        {
            InitializeComponent();
            checkClick_1 = false;
            checkClick_2 = false;
            GetMarks();
            MethodCount();
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Ex1_1_2.Text) || !string.IsNullOrEmpty(Ex1_1_3.Text) ||
                !string.IsNullOrEmpty(Ex1_2_3.Text) || !string.IsNullOrEmpty(Ex1_1_4.Text) ||
                !string.IsNullOrEmpty(Ex1_2_4.Text) || !string.IsNullOrEmpty(Ex1_3_4.Text))
            {
                checkClick_1 = true;

                Exp1_2_1.Content = 20 - int.Parse(Ex1_1_2.Text);
                Exp1_3_1.Content = 20 - int.Parse(Ex1_1_3.Text);
                Exp1_3_2.Content = 20 - int.Parse(Ex1_2_3.Text);
                Exp1_4_1.Content = 20 - int.Parse(Ex1_1_4.Text);
                Exp1_4_2.Content = 20 - int.Parse(Ex1_2_4.Text);
                Exp1_4_3.Content = 20 - int.Parse(Ex1_3_4.Text);

                AddInfoLabel_1.Content = "Матрица заполнена!";
            }
            else
                AddInfoLabel_1.Content = "В матрицу введены не все оценки!";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Ex2_1_2.Text) || !string.IsNullOrEmpty(Ex2_1_3.Text) ||
                !string.IsNullOrEmpty(Ex2_2_3.Text) || !string.IsNullOrEmpty(Ex2_1_4.Text) ||
                !string.IsNullOrEmpty(Ex2_2_4.Text) || !string.IsNullOrEmpty(Ex2_3_4.Text))
            {
                checkClick_2 = true;

                Exp2_2_1.Content = 20 - int.Parse(Ex2_1_2.Text);
                Exp2_3_1.Content = 20 - int.Parse(Ex2_1_3.Text);
                Exp2_3_2.Content = 20 - int.Parse(Ex2_2_3.Text);
                Exp2_4_1.Content = 20 - int.Parse(Ex2_1_4.Text);
                Exp2_4_2.Content = 20 - int.Parse(Ex2_2_4.Text);
                Exp2_4_3.Content = 20 - int.Parse(Ex2_3_4.Text);

                AddInfoLabel_2.Content = "Матрица заполнена!";
            }
            else
                AddInfoLabel_2.Content = "В матрицу введены не все оценки!";
        }
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (checkClick_1)
            {
                ClientObject.SendRequestToServer("EXPERT MARKS");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer("1");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex1_1_2.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex1_1_3.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex1_1_4.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex1_2_3.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex1_2_4.Text);
                System.Threading.Thread.Sleep(timeout);
                AddInfoLabel_1.Content = ClientObject.SendRequestToServer(Ex1_3_4.Text);
            }
            else
                AddInfoLabel_1.Content = "Матрица не была изменена!";


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (checkClick_2)
            {
                ClientObject.SendRequestToServer("EXPERT MARKS");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer("2");
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex2_1_2.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex2_1_3.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex2_1_4.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex2_2_3.Text);
                System.Threading.Thread.Sleep(timeout);
                ClientObject.SendRequestToServer(Ex2_2_4.Text);
                System.Threading.Thread.Sleep(timeout);
                AddInfoLabel_2.Content = ClientObject.SendRequestToServer(Ex2_3_4.Text);
            }
            else
                AddInfoLabel_2.Content = "Матрица не была изменена!";

        }
                
        private void GetMarks()
        {
            ClientObject.SendRequestToServer("GET MARKS");
            System.Threading.Thread.Sleep(timeout);
            
            DataTable dataTable = ClientObject.SendSelectRequestToServer("1");
            Ex1_1_2.Text = dataTable.Rows[0][1].ToString();
            Exp1_2_1.Content = 20 - int.Parse(Ex1_1_2.Text);
            Ex1_1_3.Text = dataTable.Rows[0][2].ToString();
            Exp1_3_1.Content = 20 - int.Parse(Ex1_1_3.Text);
            Ex1_1_4.Text = dataTable.Rows[0][3].ToString();
            Exp1_4_1.Content = 20 - int.Parse(Ex1_1_4.Text);
            Ex1_2_3.Text = dataTable.Rows[0][4].ToString();
            Exp1_3_2.Content = 20 - int.Parse(Ex1_2_3.Text);
            Ex1_2_4.Text = dataTable.Rows[0][5].ToString();
            Exp1_4_2.Content = 20 - int.Parse(Ex1_2_4.Text);
            Ex1_3_4.Text = dataTable.Rows[0][6].ToString();
            Exp1_4_3.Content = 20 - int.Parse(Ex1_3_4.Text);
            System.Threading.Thread.Sleep(timeout);

            ClientObject.SendRequestToServer("GET MARKS");
            dataTable = ClientObject.SendSelectRequestToServer("2");
            Ex2_1_2.Text = dataTable.Rows[0][1].ToString();
            Exp2_2_1.Content = 20 - int.Parse(Ex2_1_2.Text);
            Ex2_1_3.Text = dataTable.Rows[0][2].ToString();
            Exp2_3_1.Content = 20 - int.Parse(Ex2_1_3.Text);
            Ex2_1_4.Text = dataTable.Rows[0][4].ToString();
            Exp2_4_1.Content = 20 - int.Parse(Ex2_1_4.Text);
            Ex2_2_3.Text = dataTable.Rows[0][3].ToString();
            Exp2_3_2.Content = 20 - int.Parse(Ex2_2_3.Text);
            Ex2_2_4.Text = dataTable.Rows[0][5].ToString();
            Exp2_4_2.Content = 20 - int.Parse(Ex2_2_4.Text);
            Ex2_3_4.Text = dataTable.Rows[0][6].ToString();
            Exp2_4_3.Content = 20 - int.Parse(Ex2_3_4.Text);
            System.Threading.Thread.Sleep(timeout);

            
        }

        private void MethodCount()
        {
            int N = 12;
            float [,] Sum = new float[2, 4] {{0,0,0,0 }, { 0, 0, 0, 0 } };
            float [,] Normal=new float[2, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }};
            float[] TargetWeight = new float[4] { 0,0,0,0 };

            Exp1_2_1.Content = 20 - int.Parse(Ex1_1_2.Text);
            Exp1_3_1.Content = 20 - int.Parse(Ex1_1_3.Text);
            Exp1_3_2.Content = 20 - int.Parse(Ex1_2_3.Text);
            Exp1_4_1.Content = 20 - int.Parse(Ex1_1_4.Text);
            Exp1_4_2.Content = 20 - int.Parse(Ex1_2_4.Text);
            Exp1_4_3.Content = 20 - int.Parse(Ex1_3_4.Text);

            Sum[0, 0] = int.Parse(Ex1_1_2.Text) + int.Parse(Ex1_1_3.Text) + int.Parse(Ex1_1_4.Text);
            Sum[0, 1] = 20 - int.Parse(Ex1_1_2.Text) + int.Parse(Ex1_2_3.Text) + int.Parse(Ex1_2_4.Text);
            Sum[0, 2] = 40 - (int.Parse(Ex1_1_3.Text) + int.Parse(Ex1_2_3.Text)) + int.Parse(Ex1_3_4.Text);
            Sum[0, 3] = 60 - (int.Parse(Ex1_1_4.Text) + int.Parse(Ex1_2_4.Text) + int.Parse(Ex1_3_4.Text));

            Sum[1, 0] = int.Parse(Ex2_1_2.Text) + int.Parse(Ex2_1_3.Text) + int.Parse(Ex2_1_4.Text);
            Sum[1, 1] = 20 - int.Parse(Ex2_1_2.Text) + int.Parse(Ex2_2_3.Text) + int.Parse(Ex2_2_4.Text);
            Sum[1, 2] = 40 - (int.Parse(Ex2_1_3.Text) + int.Parse(Ex2_2_3.Text)) + int.Parse(Ex2_3_4.Text);
            Sum[1, 3] = 60 - (int.Parse(Ex2_1_4.Text) + int.Parse(Ex2_2_4.Text) + int.Parse(Ex2_3_4.Text));
                        
            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Normal[i, j] = Sum[i, j] / (N*20);
                }
            }
           
                

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TargetWeight[j] += Normal[i, j];                    
                }
            }
            Target_1.Content = Math.Round(TargetWeight[0],3);
            Target_2.Content = Math.Round(TargetWeight[1],3);
            Target_3.Content = Math.Round(TargetWeight[2],3);
            Target_4.Content = Math.Round(TargetWeight[3],3);

            target1 = Math.Round(TargetWeight[0], 3);
            target2 = Math.Round(TargetWeight[1], 3);
            target3 = Math.Round(TargetWeight[2], 3);
            target4 = Math.Round(TargetWeight[3], 3);
                     
            
            
            float temp;
            for (int i = 0; i < TargetWeight.Length - 1; i++)
            {
                for (int j = i + 1; j < TargetWeight.Length; j++)
                {
                    if (TargetWeight[i] < TargetWeight[j])
                    {
                        temp = TargetWeight[i];
                        TargetWeight[i] = TargetWeight[j];
                        TargetWeight[j] = temp;
                    }
                }
            }
                        
            if (Math.Round(TargetWeight[0], 3) == target1)
            {
                InfoLabel.Content = "Согласно экпертным оценкам, они рекомендуют автомобили марки Шкода";
            }
            else if (Math.Round(TargetWeight[0], 3) == target2)
            {
                InfoLabel.Content = "Согласно экпертным оценкам, они рекомендуют автомобили марки Фольксваген";
            }
            else if (Math.Round(TargetWeight[0], 3) == target3)
            {
                InfoLabel.Content = "Согласно экпертным оценкам, они рекомендуют автомобили марки Рено";
            }
            else if (Math.Round(TargetWeight[0], 3) == target4)
            {
                InfoLabel.Content = "Согласно экпертным оценкам, они рекомендуют автомобили марки Ауди";
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {          
            MethodCount();
        }

        private void diagrammButton_Click(object sender, RoutedEventArgs e)
        {
            BasicColumn basicColumn = new BasicColumn();
            basicColumn.Show();
        }       
    }
}
