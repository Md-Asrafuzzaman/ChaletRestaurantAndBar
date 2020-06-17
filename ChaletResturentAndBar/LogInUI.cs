using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChaletResturentAndBar
{
    public partial class LogInUI : Form
    {
        public LogInUI()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            /*// SQL Connection...................
            string connectionString = @"Server=LAPTOP-MO6NVULF; Database=ChaletDB; Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //SQL Command........................
            string commandString = @"SELECT * FROM UserTable WHERE User Name='" + userNameTextBox.Text + "' AND Password='" + passwordTextBox.Text + "'";
            SqlCommand sqlCommand = new SqlCommand(commandString, sqlConnection);

            //Connection Open
            sqlConnection.Open();

            //Show
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                OrderUI orderUI = new OrderUI();
                orderUI.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Wrong Informatiomn");
            sqlConnection.Close();*/
            /*string userName = "Admin";
            string password = "123456";*/
            if (userNameTextBox.Text == "Admin" || userNameTextBox.Text == "admin" && passwordTextBox.Text == "123456")
            {
                OrderUI orderUI = new OrderUI();
                orderUI.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Informatiomn");
               

            }
               

        }

    }
}
