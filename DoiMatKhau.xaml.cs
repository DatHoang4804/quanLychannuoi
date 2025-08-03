using System.Windows;
using Microsoft.Data.SqlClient;

namespace QuanLyChanNuoi
{
    public partial class DoiMatKhau : Window
    {
        private readonly string _userID;
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

        public DoiMatKhau(string userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void BtnLuu2_Click(object sender, RoutedEventArgs e)
        {
            string matKhauCu = mkcu1.Text.Trim();
            string matKhauMoi = mkmoi1.Password.Trim();
            string xacNhanMK = mkmoi2.Password.Trim();

            // Kiểm tra trống
            if (string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi) || string.IsNullOrWhiteSpace(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra mật khẩu mới trùng khớp
            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu mới và cũ không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Kiểm tra mật khẩu cũ đúng không
                string sqlCheck = "SELECT COUNT(*) FROM taikhoan WHERE id = @id AND matkhau = @matkhaucu";
                using (var cmdCheck = new SqlCommand(sqlCheck, connection))
                {
                    cmdCheck.Parameters.AddWithValue("@id", _userID);
                    cmdCheck.Parameters.AddWithValue("@matkhaucu", matKhauCu);

                    int count = (int)cmdCheck.ExecuteScalar();
                    if (count == 0)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                // Cập nhật mật khẩu mới
                string sqlUpdate = "UPDATE taikhoan SET matkhau = @matkhaumoi WHERE id = @id";
                using (var cmdUpdate = new SqlCommand(sqlUpdate, connection))
                {
                    cmdUpdate.Parameters.AddWithValue("@matkhaumoi", matKhauMoi);
                    cmdUpdate.Parameters.AddWithValue("@id", _userID);

                    int rows = cmdUpdate.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true; // báo hiệu thành công về TrangChu
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
