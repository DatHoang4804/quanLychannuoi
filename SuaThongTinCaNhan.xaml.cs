using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
namespace QuanLyChanNuoi
{
    /// <summary>
    /// Interaction logic for SuaThongTinCaNhan.xaml
    /// </summary>
    public partial class SuaThongTinCaNhan : Window
    {
        private string _userID;
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

        public SuaThongTinCaNhan(string userID, string userName, string userEmail)
        {
            InitializeComponent();
            _userID = userID;
            txtHoTen.Text = userName;
            txtEmail.Text = userEmail;
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            string newHoTen = txtHoTen.Text.Trim();
            string newEmail = txtEmail.Text.Trim();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE taikhoan SET tendaydu = @tendaydu, email = @email WHERE id = @id";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tendaydu", newHoTen);
                    command.Parameters.AddWithValue("@email", newEmail);
                    command.Parameters.AddWithValue("@id", _userID);

                    int rows = command.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật thành công! Hãy đăng xuất để hiển thị dữ liệu mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
