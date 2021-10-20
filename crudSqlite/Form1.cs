using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace crudSqlite
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            dgvProduct.DataSource = Connection.query("SELECT * FROM producto");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtId.Text.Length != 0)
            {
                string sql = "SELECT Nombre, Precio FROM Producto WHERE id_producto=@id";
                SQLiteCommand command = new SQLiteCommand(sql, Connection.objDB());
                command.Parameters.Add(new SQLiteParameter("@id", int.Parse(txtId.Text)));
                SQLiteDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    string query = "UPDATE Producto SET Nombre=@name, Precio=@price WHERE id_producto=@id";
                    command = new SQLiteCommand(query, Connection.objDB());
                    command.Parameters.Add(new SQLiteParameter("@id", int.Parse(txtId.Text)));
                    command.Parameters.Add(new SQLiteParameter("@name", txtName.Text));
                    command.Parameters.Add(new SQLiteParameter("@price", float.Parse(txtPrice.Text)));
                    if (command.ExecuteNonQuery() > 0)
                    {
                        getData();
                        MessageBox.Show("Producto Actualizado");
                        cleanData();
                    }
                    else MessageBox.Show("Error al registrar");
                }
            }
            else
            {
                string sql = "INSERT INTO Producto(Nombre, Precio) VALUES(@name, @price)";
                SQLiteCommand command = new SQLiteCommand(sql, Connection.objDB());
                command.Parameters.Add(new SQLiteParameter("@name", txtName.Text));
                command.Parameters.Add(new SQLiteParameter("@price", float.Parse(txtPrice.Text)));
                if (command.ExecuteNonQuery() > 0)
                {
                    getData();
                    cleanData();
                }
                else MessageBox.Show("Error al registrar");
            }
            

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Ingrese un el id del producto a eliminar");
            }
            else
            {
                string sql = "DELETE FROM Producto WHERE id_producto=@id";
                SQLiteCommand command = new SQLiteCommand(sql, Connection.objDB());
                command.Parameters.Add(new SQLiteParameter("@id", int.Parse(txtId.Text)));
                if (command.ExecuteNonQuery() > 0)
                {
                    getData();
                    cleanData();
                }
                else MessageBox.Show("No se puede eliminar");
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Ingrese un el id del producto a buscar");
            }
            else
            {
                string sql = "SELECT Nombre, Precio FROM Producto WHERE id_producto=@id";
                SQLiteCommand command = new SQLiteCommand(sql, Connection.objDB());
                command.Parameters.Add(new SQLiteParameter("@id", int.Parse(txtId.Text)));
                SQLiteDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    txtName.Text = dataReader[0].ToString();
                    txtPrice.Text = dataReader[1].ToString();
                }
                else MessageBox.Show("No se encontro el producto");
                
            }
        }
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = dgvProduct.SelectedRows[0].Cells[1].Value.ToString();
                txtPrice.Text = dgvProduct.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("error", ex.ToString());
            }
            
        }

        public void cleanData()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
        }

    }
}
