using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopAppWithDb
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        private SqlDataReader reader;
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ExistCustomer())
                MessageBox.Show("This name already exists");
            else
                InsertCustomer();
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ShowCustomer();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ExistCustomer())
                MessageBox.Show("This name already exists");
            else
                UpdateCustomer();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                DeleteItem();
            }
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchItem();
        }
        private void customerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customerDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerNameTextBox.Text = row.Cells[1].Value.ToString();
                contactTextBox.Text = row.Cells[2].Value.ToString();
                addressTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        private bool ExistCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + customerNameTextBox.Text + "' AND Id <>" + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        private void InsertCustomer()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Customers (Name, Contact, Address) Values ('" + customerNameTextBox.Text + "', " +
                                contactTextBox.Text + ", '" + addressTextBox.Text + "')";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Saved");
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private void ShowCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                customerDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
       
        private void UpdateCustomer()
        {
            try
            {

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"UPDATE Customers SET Name = '" + customerNameTextBox.Text + "', Contact = '" + contactTextBox.Text +
                                   "', Address = '"+addressTextBox.Text+"' WHERE Id = " + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int isExecuted = sqlCommand.ExecuteNonQuery();
            if (isExecuted > 0)
            {
                MessageBox.Show("Customer updated");
            }
            else
            {
                MessageBox.Show("Customer not updated");
            }
            sqlConnection.Close();
            ShowCustomer();
        }
       
        private void DeleteItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Customers WHERE Id = " + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (rowAffected > 0)
            {
                MessageBox.Show("Customer is deleted successfully");
                ShowCustomer();
            }
        }
        private void SearchItem()
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
                customerNameTextBox.Text = null;
            if (String.IsNullOrEmpty(addressTextBox.Text))
                addressTextBox.Text = null;
            if (String.IsNullOrEmpty(contactTextBox.Text))
                contactTextBox.Text = null;
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Customers WHERE Name = '" + customerNameTextBox.Text + "' OR Contact = '" + contactTextBox.Text + "' OR Address = '" + addressTextBox.Text + "'";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                customerDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
    }
}
