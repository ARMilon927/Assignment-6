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
    public partial class ItemUI : Form
    {
        string connectionString = @"Server=.\SILENTREVENGER; Database=CoffeeShop; Integrated Security=True";
        SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private string commandString;
        private SqlDataAdapter sqlDataAdapter;
        private SqlDataReader reader;
        public ItemUI()
        {
            InitializeComponent();
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ExistItem())
                MessageBox.Show("This name already exists");
            else
                InsertItem();
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ShowItem();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this item?", "Delete Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
            {
                DeleteItem();
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (ExistItem())
                MessageBox.Show("This name already exists");
            else
                UpdateItem();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchItem();
        }
        private void itemDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemDataGridView.Rows[e.RowIndex];
                idLabel.Text = row.Cells[0].Value.ToString();
                itemNameTextBox.Text = row.Cells[1].Value.ToString();
                priceTextBox.Text = row.Cells[2].Value.ToString();
            }
        }
       
        private bool ExistItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + itemNameTextBox.Text + "' AND Id <>" + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            sqlConnection.Close();
            return isExist;
        }
        private void InsertItem()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"INSERT INTO Items (Name, Price) Values ('" + itemNameTextBox.Text + "', " +
                                priceTextBox.Text + ")";
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
        private void ShowItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                itemDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
        private void DeleteItem()
        {
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"DELETE FROM Items WHERE Id = " + idLabel.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            int rowAffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            if (rowAffected > 0)
            {
                MessageBox.Show("Item is deleted successfully");
                ShowItem();
            }
        }
        private void UpdateItem()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                commandString = @"UPDATE Items SET Name = '" + itemNameTextBox.Text + "', Price = " + priceTextBox.Text +
                                " WHERE Id = " + idLabel.Text + "";
                sqlCommand = new SqlCommand(commandString, sqlConnection);
                sqlConnection.Open();
                int isExecuted = sqlCommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Item updated");
                }
                else
                {
                    MessageBox.Show("Item not updated");
                }
                sqlConnection.Close();
                ShowItem();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private void SearchItem()
        {
            if (String.IsNullOrEmpty(itemNameTextBox.Text))
                itemNameTextBox.Text = null;
            if (String.IsNullOrEmpty(priceTextBox.Text))
                priceTextBox.Text = "0";
            sqlConnection = new SqlConnection(connectionString);
            commandString = @"SELECT * FROM Items WHERE Name = '" + itemNameTextBox.Text + "' OR Price = " +
                                   priceTextBox.Text + "";
            sqlCommand = new SqlCommand(commandString, sqlConnection);
            sqlConnection.Open();
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                itemDataGridView.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("No Data Found");
            }
            sqlConnection.Close();
        }
    }
}
