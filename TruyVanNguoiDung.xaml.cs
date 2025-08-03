using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace QuanLyChanNuoi
{
    public partial class TruyVanNguoiDung : Window
    {
        private string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

        public TruyVanNguoiDung()
        {
            InitializeComponent();
            LoadTruyVanNguoiDung();
        }

        private void LoadTruyVanNguoiDung()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                    SELECT 
                        id AS [ID], 
                        matkhau AS [Mật khẩu],
                        tendaydu AS [Tên đầy đủ],
                        email AS [Email],
                        role AS [Quyền]
                    FROM taikhoan";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridNguoiDung.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn: " + ex.Message);
            }
        }

        private void kqtc_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Commit edit changes to the DataGrid
                dataGridNguoiDung.RowEditEnding -= kqtc_RowEditEnding; 
                dataGridNguoiDung.CommitEdit(DataGridEditingUnit.Row, true);
                dataGridNguoiDung.RowEditEnding += kqtc_RowEditEnding; 

                // Make sure the row is a DataRowView before processing
                if (e.Row.Item is DataRowView row)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open(); // Open the connection

                            // Prepare the update query
                            string query = @"
                            UPDATE taikhoan
                            SET matkhau = @matkhau,
                                tendaydu = @tendaydu,
                                email = @email,
                                role = @role
                            WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                // Add parameters for the query
                                cmd.Parameters.AddWithValue("@id", row["ID"]);
                                cmd.Parameters.AddWithValue("@matkhau", row["Mật khẩu"]);
                                cmd.Parameters.AddWithValue("@tendaydu", row["Tên đầy đủ"]);
                                cmd.Parameters.AddWithValue("@email", row["Email"]);
                                cmd.Parameters.AddWithValue("@role", row["Quyền"]);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cập nhật thông tin người dùng thành công!");
                                }
                                else
                                {
                                    MessageBox.Show("Cập nhật không thành công. Vui lòng thử lại.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật thông tin người dùng: " + ex.Message);
                    }
                }
            }
        }
    }
}
