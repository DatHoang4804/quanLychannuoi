using System;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace QuanLyChanNuoi
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dk1_Click(object sender, RoutedEventArgs e)
        {
            DangNhapPanel.Visibility = Visibility.Collapsed;
            DangKyPanel.Visibility = Visibility.Visible;
        }

        private void dn2_Click(object sender, RoutedEventArgs e)
        {
            DangNhapPanel.Visibility = Visibility.Visible;
            DangKyPanel.Visibility = Visibility.Collapsed;
        }

        private void dn1_Click(object sender, RoutedEventArgs e)
        {
            string tenTaiKhoan = tkdn.Text.Trim();
            string matKhau = mkdn.Password.Trim();
            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ID FROM taikhoan WHERE id = @id AND matkhau = @matkhau";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", tenTaiKhoan);
                    command.Parameters.AddWithValue("@matkhau", matKhau);
                    string userID = command.ExecuteScalar()?.ToString();

                    if (userID != null)
                    {
                        // Ghi log đăng nhập và lấy ID log
                        string insertLogQuery = "INSERT INTO LogDangNhap (thoiGianDangNhap, id_TaiKhoan) OUTPUT INSERTED.id VALUES (@thoiGianDangNhap, @id_TaiKhoan)";
                        SqlCommand insertLogCmd = new SqlCommand(insertLogQuery, connection);
                        insertLogCmd.Parameters.AddWithValue("@thoiGianDangNhap", DateTime.Now);
                        insertLogCmd.Parameters.AddWithValue("@id_TaiKhoan", userID);
                        int logDangNhapID = (int)insertLogCmd.ExecuteScalar();  // Lấy ID log mới insert

                        // Mở trang chủ, truyền cả userID và logDangNhapID
                        TrangChu trangChuWindow = new TrangChu(userID, logDangNhapID);
                        trangChuWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dk2_Click(object sender, RoutedEventArgs e)
        {
            string tenDayDu = tdddk.Text.Trim();
            string tenTaiKhoan = tkdk.Text.Trim();
            string email = gdk.Text.Trim();
            string matKhau = mkdk1.Password.Trim();
            string nhapLaiMatKhau = mkdk2.Password.Trim();

            if (string.IsNullOrEmpty(tenDayDu) || string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(nhapLaiMatKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(1) FROM taikhoan WHERE id = @id OR email = @email";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@id", tenTaiKhoan);
                    checkCommand.Parameters.AddWithValue("@email", email);
                    int existingCount = (int)checkCommand.ExecuteScalar();
                    if (existingCount > 0)
                    {
                        MessageBox.Show("Tên tài khoản hoặc email đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string query = "INSERT INTO taikhoan (id, email, matkhau, tendaydu, role) VALUES (@id, @email, @matkhau, @tendaydu, @role)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", tenTaiKhoan);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@matkhau", matKhau);
                    command.Parameters.AddWithValue("@tendaydu", tenDayDu);
                    command.Parameters.AddWithValue("@role", "CanBoNghiepVu");
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearRegistrationForm();
                    }
                    else
                    {
                        MessageBox.Show("Đăng ký không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc truy vấn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearRegistrationForm()
        {
            tdddk.Text = "";
            tkdk.Text = "";
            gdk.Text = "";
            mkdk1.Password = "";
            mkdk2.Password = "";
        }
    }
}
