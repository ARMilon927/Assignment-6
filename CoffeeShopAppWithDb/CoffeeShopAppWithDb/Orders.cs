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
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }
        private SqlDataReader reader;
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        private SqlConnection sqlConnection;
        private string commandString;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlDataAdapter;
        object customerId;
        object itemId;
        object price;
        private int totalPrice;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidCustomer())
            {
                InsertOrder();
            }
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ShowOrder();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ValidCustomer())
            {
                UpdateOrder();
            }
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
            SearchOrder();
        }
        private void orderDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.orderDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                customerNameTextBox.Text = row.Cells[1].Value.ToString();
                itemNameTextBox.Text = row.Cells[2].Value.ToString();
                quantityTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        private bool ValidCustomer()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Customers.Id AS CustomerID, Items.Id AS ItemID, Items.Price FROM Customers,Items WHERE Customers.Name = '" +
                            customerNameTextBox.Text + "' AND Items.Name = '" + itemNameTextBox.Text + "' ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                customerId = dataTable.Rows[0][0];
                itemId = dataTable.Rows[0][1];
                price = dataTable.Rows[0][2];
                sqlConnection.Close();
                return true;
            }
            MessageBox.Show("Customer does not exist");
            return false;
        }
        private void InsertOrder()
        {
            try
            {
                int customerID = Convert.ToInt32(customerId);
                int itemID = Convert.ToInt32(itemId);
                totalPrice = Convert.ToInt32(price) * Convert.ToInt32(quantityTextBox.Text);
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Orders (CustomerId, ItemId, Quantity, TotalPrice) Values (" + customerID + ", " + itemID + ", '" + quantityTextBox.Text + "', " + totalPrice + ")";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Order is Saved");
                }
                else
                {
                    MessageBox.Show("Order is not Saved");
                }
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private void ShowOrder()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT Orders.Id, Customers.Name AS CustomerName, Items.Name AS ItemName, Orders.Quantity, Orders.TotalPrice FROM Orders,Customers,Items WHERE Customers.Id = Orders.CustomerId AND Items.Id = Orders.ItemId";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                orderDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
        
            
        private void UpdateOrder()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE Orders SET CustomerId = " + customerId + ", ItemId = " + itemId + ", Quantity = " + quantityTextBox.Text + ", TotalPrice = " + totalPrice + " WHERE Id = " + idLabel.Text + "";
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
                ShowOrder();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        
        private void DeleteItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Orders WHERE Id = " + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (rowAffected > 0)
            {
                MessageBox.Show("Order is deleted successfully");
                ShowOrder();
            }
        }
       
        private void SearchOrder()
        {
            if (String.IsNullOrEmpty(customerNameTextBox.Text))
                customerNameTextBox.Text = null;
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
                itemNameTextBox.Text = null;
            if (String.IsNullOrEmpty(quantityTextBox.Text))
                quantityTextBox.Text = "0";
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM OrderInformations WHERE CustomerName = '" + customerNameTextBox.Text + "' OR ItemName = '" + itemNameTextBox.Text + "' OR Quantity = "+quantityTextBox.Text+" ";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                orderDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
    }
}
