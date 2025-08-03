using System;
using System.Data;
using System.Windows;
using Microsoft.Data.SqlClient; 

namespace QuanLyChanNuoi
{
    public partial class LichSuDangNhap : Window
    {
        private string userID;

        public LichSuDangNhap(string userID)
        {
            InitializeComponent();
            this.userID = userID;
            LoadLichSuDangNhap();
        }

        private void LoadLichSuDangNhap()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT thoiGianDangNhap AS [Thời Gian Đăng Nhập], 
                               thoiGianDangXuat AS [Thời Gian Đăng Xuất]
                        FROM LogDangNhap
                        WHERE id_TaiKhoan = @idTaiKhoan
                        ORDER BY thoiGianDangNhap DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTaiKhoan", userID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridLichSu.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử: " + ex.Message);
            }
        }
    }
}
