using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace QuanLyChanNuoi
{
    public partial class TrangChu : Window
    {
        private readonly string _userID;
        private int logDangNhapID;
        private string _userRole;
        private string _userName;
        private string _userEmail;
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

        public TrangChu(string userID, int logDangNhapID)
        {
            InitializeComponent();
            _userID = userID;
            this.logDangNhapID = logDangNhapID;
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var query = "SELECT role, tendaydu, email FROM taikhoan WHERE ID = @ID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", _userID);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _userRole = reader["role"].ToString();
                                _userName = reader["tendaydu"].ToString();
                                _userEmail = reader["email"].ToString();
                                txtUserName.Text = _userName;
                                if (_userRole == "QuanTri")
                                {
                                    txtUserRole.Text = "Quản trị viên";
                                }
                                else if (_userRole == "CanBoNghiepVu")
                                {
                                    txtUserRole.Text = "Cán bộ nghiệp vụ";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy thông tin người dùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                _userRole = string.Empty;
                                _userName = string.Empty;
                                _userEmail = string.Empty;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối hoặc truy vấn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                _userRole = string.Empty;
                _userName = string.Empty;
                _userEmail = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                _userRole = string.Empty;
                _userName = string.Empty;
                _userEmail = string.Empty;
            }
        }

        private void btnTrangChu_Click(object sender, RoutedEventArgs e)
        {
            TrangChuPanel.Visibility = Visibility.Visible;
            TraCuuPanel.Visibility = Visibility.Collapsed;
            CaNhanPanel.Visibility = Visibility.Collapsed;
        }

        private void btnTraCuu_Click(object sender, RoutedEventArgs e)
        {
            TrangChuPanel.Visibility = Visibility.Collapsed;
            TraCuuPanel.Visibility = Visibility.Visible;
            CaNhanPanel.Visibility = Visibility.Collapsed;
        }

        private void btnCaNhan_Click(object sender, RoutedEventArgs e)
        {
            TrangChuPanel.Visibility = Visibility.Collapsed;
            TraCuuPanel.Visibility = Visibility.Collapsed;
            CaNhanPanel.Visibility = Visibility.Visible;
        }

        // Nút đăng xuất
        private void btnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật thời gian đăng xuất
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE LogDangNhap SET thoiGianDangXuat = @thoiGianDangXuat WHERE id = @logID";
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddWithValue("@thoiGianDangXuat", DateTime.Now);
                    cmd.Parameters.AddWithValue("@logID", logDangNhapID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi ghi log đăng xuất: " + ex.Message);
            }

            // Quay lại trang đăng nhập
            new MainWindow().Show();
            Close();
        }

        private void ah1_Click(object sender, RoutedEventArgs e)
        {
            if (_userRole == "QuanTri")
            {
                TinhNang tinhNangWindow = new TinhNang();

                // Đặt Panel hiển thị trước khi ShowDialog()
                tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.H; // Hiển thị Panel H
                tinhNangWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chỉ có quản trị viên mới có thể sử dụng chức năng này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ax1_Click(object sender, RoutedEventArgs e)
        {
            if (_userRole == "QuanTri")
            {
                TinhNang tinhNangWindow = new TinhNang();

                // Đặt Panel hiển thị trước khi ShowDialog()
                tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.X; // Hiển thị Panel X
                tinhNangWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chỉ có quản trị viên mới có thể sử dụng chức năng này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void acssx1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.CSSX; // Hiển thị Panel CSSX
            tinhNangWindow.ShowDialog();
        }

        private void acskn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.CSKN; // Hiển thị Panel CSKN
            tinhNangWindow.ShowDialog();
        }

        private void atc1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.ToChuc;
            tinhNangWindow.ShowDialog();
        }

        private void acn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.CaNhan;
            tinhNangWindow.ShowDialog();
        }

        private void adkcn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.dkcNuoi;
            tinhNangWindow.ShowDialog();
        }

        private void atccn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.tochucChNhan;
            tinhNangWindow.ShowDialog();
        }

        private void agcn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.giayChNhan;
            tinhNangWindow.ShowDialog();
        }

        private void ahcnnl1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.hoCnuoiNhole;
            tinhNangWindow.ShowDialog();
        }

        private void acscb1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.coSoCheBien;
            tinhNangWindow.ShowDialog();
        }
        private void acscn1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.CSChanNuoi;
            tinhNangWindow.ShowDialog();
        }
        private void aldb1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.dichBenh;
            tinhNangWindow.ShowDialog();
        }

        private void avdb1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.vungDichBenh;
            tinhNangWindow.ShowDialog();
        }

        private void accty1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.chiCucThuY;
            tinhNangWindow.ShowDialog();
        }

        private void adlbt1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.daiLyThuoc;
            tinhNangWindow.ShowDialog();
        }

        private void acstg1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.khuTamGiu;
            tinhNangWindow.ShowDialog();
        }

        private void acsgm1_Click(object sender, RoutedEventArgs e)
        {
            TinhNang tinhNangWindow = new TinhNang();

            // Đặt Panel hiển thị trước khi ShowDialog()
            tinhNangWindow.PanelCanHienThi = TinhNang.PanelHienThi.coSoGietMo;
            tinhNangWindow.ShowDialog();
        }
        //Ham thuc hien tra cuu
        private void tc_Click(object sender, RoutedEventArgs e)
        {
            // Lay lua chon tu ComboBox
            ComboBoxItem selectedItem = tc.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Vui long chon mot muc de tra cuu!");
                return;
            }

            // Lay text cua muc da chon
            string selectedValue = selectedItem.Content.ToString();

            // Lay cau lenh SQL tuong ung
            string query = GetQueryBySelection(selectedValue);

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Chuc nang chua duoc ho tro!");
                return;
            }

            // Load du lieu len DataGrid
            LoadData(query);
        }

        // Ham map lua chon ComboBox sang cau lenh SQL
        private string GetQueryBySelection(string selection)
        {
            switch (selection)
            {
                case "Huyện":
                    return @"
            SELECT
                h.id_Huyen AS N'Mã huyện',
                h.tenHuyen AS N'Tên huyện'
            FROM huyen h
            ";
                case "Xã":
                    return @"
            SELECT
                x.id_Xa AS N'Mã xã',
                x.tenXa AS N'Tên xã',
                h.tenHuyen AS N'Tên huyện'
            FROM xa x
            JOIN huyen h ON x.id_Huyen = h.id_Huyen
            ";
                case "Cơ sở sản xuất sản phẩm xử lý chất thải":
                    return @"
            SELECT
                s.id_sXuat AS N'Mã cơ sở',
                s.tensXuat AS N'Tên cơ sở',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                s.diachisXuat AS N'Địa chỉ',
                s.sdtsXuat AS N'Số điện thoại',
                s.emailsXuat AS N'Email'
            FROM SXsphamXlycThai s
            JOIN huyen h ON s.id_Huyen = h.id_Huyen
            JOIN xa x ON s.id_Xa = x.id_Xa
            ";

                case "Cơ sở khảo nghiệm sản phẩm xử lý chất thải":
                    return @"
            SELECT
                k.id_kNghiem AS N'Mã cơ sở',
                k.tenkNghiem AS N'Tên cơ sở',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                k.diachikNghiem AS N'Địa chỉ',
                k.sdtkNghiem AS N'Số điện thoại',
                k.emailkNghiem AS N'Email'
            FROM KNsphamXlycThai k
            JOIN huyen h ON k.id_Huyen = h.id_Huyen
            JOIN xa x ON k.id_Xa = x.id_Xa
            ";

                case "Tổ chức":
                    return @"
            SELECT
                t.id_tChuc AS N'Mã tổ chức',
                t.tentChuc AS N'Tên tổ chức',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                t.nguoidaidien AS N'Người đại diện',
                t.email AS N'Email',
                t.sdttChuc AS N'Số điện thoại',
                t.diachiNguoidaidien AS N'Địa chỉ người đại diện',
                cn.tencNuoi AS N'Cơ sở chăn nuôi liên kết'
            FROM ToChuc t
            JOIN huyen h ON t.id_Huyen = h.id_Huyen
            JOIN xa x ON t.id_Xa = x.id_Xa
            LEFT JOIN CSChanNuoi cn ON t.id_cNuoi = cn.id_cNuoi
            ";

                case "Cá nhân":
                    return @"
            SELECT
                c.id_cNhan AS N'Căn cước công dân',
                c.tencNhan AS N'Tên cá nhân',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                c.diachicNhan AS N'Địa chỉ',
                c.sdtcNhan AS N'Số điện thoại',
                cn.tencNuoi AS N'Cơ sở chăn nuôi liên kết'
            FROM CaNhan c
            JOIN huyen h ON c.id_Huyen = h.id_Huyen
            JOIN xa x ON c.id_Xa = x.id_Xa
            LEFT JOIN CSChanNuoi cn ON c.id_cNuoi = cn.id_cNuoi
            ";

                case "Điều kiện chăn nuôi":
                    return @"
            SELECT
                dk.id_dkcNuoi AS N'Mã điều kiện',
                dk.tendkCnuoi AS N'Tên điều kiện',
                dk.ngaycapdkCnuoi AS N'Ngày cấp',
                dk.ngayhethandkCnuoi AS N'Ngày hết hạn'
            FROM dkcNuoi dk
            ";

                case "Tổ chức chứng nhận":
                    return @"
            SELECT
                tc.id_TCChNhan AS N'Mã tổ chức',
                tc.tenTCChNhan AS N'Tên tổ chức',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                tc.diachiTCChNhan AS N'Địa chỉ',
                tc.sdtTCChNhan AS N'Số điện thoại',
                tc.emailTCChNhan AS N'Email'
            FROM tochucChNhan tc
            JOIN huyen h ON tc.id_Huyen = h.id_Huyen
            JOIN xa x ON tc.id_Xa = x.id_Xa
            ";

                case "Giấy chứng nhận":
                    return @"
            SELECT
                gc.id_ChNhan AS N'Mã giấy chứng nhận',
                gc.id_TCChNhan AS N'Mã tổ chức chứng nhận',
                tc.tenTCChNhan AS N'Tên tổ chức chứng nhận',
                gc.tenChNhan AS N'Tên giấy chứng nhận',
                gc.ngayChNhan AS N'Ngày cấp',
                gc.ngayHetHan AS N'Ngày hết hạn'
            FROM giayChNhan gc
            JOIN tochucChNhan tc ON gc.id_TCChNhan = tc.id_TCChNhan
            ";

                case "Hộ chăn nuôi nhỏ lẻ":
                    return @"
            SELECT
                hc.id_hoCnuoi AS N'Mã hộ',
                hc.tenChuHo AS N'Tên chủ hộ',
                hc.sdtChuHo AS N'Số điện thoại',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                hc.thongke AS N'Thống kê'
            FROM hoCnuoiNhole hc
            JOIN huyen h ON hc.id_Huyen = h.id_Huyen
            JOIN xa x ON hc.id_Xa = x.id_Xa
            ";

                case "Cơ sở chăn nuôi":
                    return @"
            SELECT
                cs.id_cNuoi AS N'Mã cơ sở',
                cs.tencNuoi AS N'Tên cơ sở',
                dk.tendkCnuoi AS N'Điều kiện chăn nuôi',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                cs.diachicNuoi AS N'Địa chỉ',
                cs.sdtcNuoi AS N'Số điện thoại',
                cs.emailcNuoi AS N'Email'
            FROM CSChanNuoi cs
            JOIN huyen h ON cs.id_Huyen = h.id_Huyen
            JOIN xa x ON cs.id_Xa = x.id_Xa
            JOIN dkcNuoi dk ON cs.id_dkcNuoi = dk.id_dkcNuoi
            ";

                case "Cơ sở chế biến":
                    return @"
            SELECT
                cb.id_cBien AS N'Mã cơ sở',
                cb.tencBien AS N'Tên cơ sở',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                cb.diachicBien AS N'Địa chỉ',
                cb.sdtcBien AS N'Số điện thoại',
                cb.emailcBien AS N'Email'
            FROM csCheBien cb
            JOIN huyen h ON cb.id_Huyen = h.id_Huyen
            JOIN xa x ON cb.id_Xa = x.id_Xa
            ";

                case "Loại dịch bệnh":
                    return @"
            SELECT
                db.id_dichBenh AS N'Mã dịch bệnh',
                db.tendichBenh AS N'Tên dịch bệnh',
                db.mota AS N'Mô tả'
            FROM dichBenh db
            ";

                case "Vùng dịch bệnh":
                    return @"
            SELECT
                vd.id_Vungdich AS N'Mã vùng dịch',
                vd.tenVungdich AS N'Tên vùng dịch',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                db.tendichBenh AS N'Tên dịch bệnh',
                vd.ngaykhoiphat AS N'Ngày khởi phát',
                CASE vd.tinhtrang
                    WHEN 1 THEN N'Đã xử lý'
                    WHEN 0 THEN N'Chưa xử lý'
                    ELSE N'Không xác định'
                END AS N'Tình trạng'
            FROM VungdichBenh vd
            JOIN huyen h ON vd.id_Huyen = h.id_Huyen
            JOIN xa x ON vd.id_Xa = x.id_Xa
            JOIN dichBenh db ON vd.id_dichBenh = db.id_dichBenh
            ";

                case "Chi cục thú y":
                    return @"
            SELECT
                cty.id_cCucTY AS N'Mã chi cục',
                cty.tencCucTY AS N'Tên chi cục',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                cty.diachicCucTY AS N'Địa chỉ',
                cty.sdtcCucTY AS N'Số điện thoại',
                cty.emailcCucTY AS N'Email'
            FROM cCucTY cty
            JOIN huyen h ON cty.id_Huyen = h.id_Huyen
            JOIN xa x ON cty.id_Xa = x.id_Xa
            ";

                case "Đại lý bán thuốc thú y":
                    return @"
            SELECT
                dl.id_dLy AS N'Mã đại lý',
                dl.tendLy AS N'Tên đại lý',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                dl.diachidLY AS N'Địa chỉ',
                dl.sdtdLy AS N'Số điện thoại',
                dl.emaildLy AS N'Email'
            FROM dLythuoc dl
            JOIN huyen h ON dl.id_Huyen = h.id_Huyen
            JOIN xa x ON dl.id_Xa = x.id_Xa
            ";

                case "Cơ sở tạm giữ":
                    return @"
            SELECT
                tg.id_tGiu AS N'Mã cơ sở',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                tg.diachitGiu AS N'Địa chỉ',
                tg.stttGiu AS N'Số điện thoại'
            FROM khuTGiu tg
            JOIN huyen h ON tg.id_Huyen = h.id_Huyen
            JOIN xa x ON tg.id_Xa = x.id_Xa
            ";

                case "Cơ sở giết mổ":
                    return @"
            SELECT
                gm.id_GietMo AS N'Mã cơ sở',
                gm.tenGietMo AS N'Tên cơ sở',
                h.tenHuyen AS N'Huyện',
                x.tenXa AS N'Xã',
                gm.diachiGietMo AS N'Địa chỉ',
                gm.sdtGietMo AS N'Số điện thoại'
            FROM CsGietMo gm
            JOIN huyen h ON gm.id_Huyen = h.id_Huyen
            JOIN xa x ON gm.id_Xa = x.id_Xa
            ";

                default:
                    return null;
            }
        }

        // Ham load du lieu tu SQL va hien thi len DataGrid
        private void LoadData(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    kqtc.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi lay du lieu: " + ex.Message);
            }
        }

        // Sự kiện khi kết thúc chỉnh sửa một dòng trong DataGrid
        private void kqtc_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Kiểm tra xem hành động chỉnh sửa là "Commit" (người dùng thực sự muốn lưu)
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Bắt buộc: ép DataGrid commit dữ liệu mới từ ô người dùng vừa sửa vào DataRow
                kqtc.RowEditEnding -= kqtc_RowEditEnding;
                kqtc.CommitEdit(DataGridEditingUnit.Row, true);
                kqtc.RowEditEnding += kqtc_RowEditEnding;
                // Kiểm tra xem dòng hiện tại có phải là DataRowView không
                if (e.Row.Item is DataRowView row)
                {
                    try
                    {
                        // Mở kết nối đến SQL Server
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            con.Open(); // Mở kết nối

                            // Xác định loại dữ liệu đang chỉnh sửa dựa trên lựa chọn trong ComboBox
                            ComboBoxItem selectedItem = tc.SelectedItem as ComboBoxItem;
                            if (selectedItem == null)
                            {
                                MessageBox.Show("Vui lòng chọn mục cần cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            string selectedValue = selectedItem.Content.ToString();
                            string query = "";

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            if (selectedValue == "Huyện")
                            {
                                if (_userRole == "QuanTri")
                                {
                                    query = @"
                                UPDATE huyen
                                SET tenHuyen = @tenHuyen
                                WHERE id_Huyen = @id_Huyen";
                                    cmd.CommandText = query;
                                    cmd.Parameters.AddWithValue("@id_Huyen", row["Mã huyện"]); // Giả sử cột chứa mã huyện có tên là "Mã huyện"
                                    cmd.Parameters.AddWithValue("@tenHuyen", row["Tên huyện"]);
                                }
                                else
                                {
                                    MessageBox.Show("Chỉ có quản trị viên được quyền sửa xã và huyện!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                            }
                            if (selectedValue == "Xã")
                            {
                                if (_userRole == "QuanTri")
                                {
                                    query = @"
                                UPDATE xa
                                SET tenXa = @tenXa
                                WHERE id_Xa = @id_Xa";
                                    cmd.CommandText = query;
                                    cmd.Parameters.AddWithValue("@id_Xa", row["Mã xã"]);
                                    cmd.Parameters.AddWithValue("@tenXa", row["Tên xã"]);
                                }
                                else
                                {
                                    MessageBox.Show("Chỉ có quản trị viên được quyền sửa xã và huyện!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                            }
                            else if (selectedValue == "Cơ sở sản xuất sản phẩm xử lý chất thải")
                            {
                                query = @"
                                UPDATE SXsphamXlycThai
                                SET tensXuat = @ten,
                                    diachisXuat = @diachi,
                                    sdtsXuat = @sdt,
                                    emailsXuat = @email
                                WHERE id_sXuat = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên cơ sở"]);
                                cmd.Parameters.AddWithValue("@diachi", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdt", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@email", row["Email"]);
                            }
                            else if (selectedValue == "Cơ sở khảo nghiệm sản phẩm xử lý chất thải")
                            {
                                query = @"
                                UPDATE KNsphamXlycThai
                                SET tenkNghiem = @ten,
                                    diachikNghiem = @diachi,
                                    sdtkNghiem = @sdt,
                                    emailkNghiem = @email
                                WHERE id_kNghiem = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên cơ sở"]);
                                cmd.Parameters.AddWithValue("@diachi", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdt", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@email", row["Email"]);
                            }
                            else if (selectedValue == "Tổ chức")
                            {
                                query = @"
                                UPDATE ToChuc
                                SET tentChuc = @ten,
                                    nguoidaidien = @nguoidaidien,
                                    diachiNguoidaidien = @diachinguoidaidien,
                                    sdttChuc = @sdt,
                                    email = @email
                                WHERE id_tChuc = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã tổ chức"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên tổ chức"]);
                                cmd.Parameters.AddWithValue("@nguoidaidien", row["Người đại diện"]);
                                cmd.Parameters.AddWithValue("@diachinguoidaidien", row["Địa chỉ người đại diện"]);
                                cmd.Parameters.AddWithValue("@sdt", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@email", row["Email"]);
                            }
                            else if (selectedValue == "Cá nhân")
                            {
                                query = @"
                                UPDATE CaNhan
                                SET tencNhan = @ten,
                                    diachicNhan = @diachi,
                                    sdtcNhan = @sdt
                                WHERE id_cNhan = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Căn cước công dân"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên cá nhân"]);
                                cmd.Parameters.AddWithValue("@diachi", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdt", row["Số điện thoại"]);
                            }
                            else if (selectedValue == "Điều kiện chăn nuôi")
                            {
                                query = @"
                                UPDATE dkcNuoi
                                SET tendkCnuoi = @ten,
                                    ngaycapdkCnuoi = @ngaycap,
                                    ngayhethandkCnuoi = @ngayhethan
                                WHERE id_dkcNuoi = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã điều kiện"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên điều kiện"]);
                                cmd.Parameters.AddWithValue("@ngaycap", row["Ngày cấp"]);
                                cmd.Parameters.AddWithValue("@ngayhethan", row["Ngày hết hạn"]);
                            }
                            else if (selectedValue == "Tổ chức chứng nhận")
                            {
                                query = @"
                                UPDATE tochucChNhan
                                SET tenTCChNhan = @ten,
                                    diachiTCChNhan = @diachi,
                                    sdtTCChNhan = @sdt,
                                    emailTCChNhan = @email
                                WHERE id_TCChNhan = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã tổ chức"]);
                                cmd.Parameters.AddWithValue("@ten", row["Tên tổ chức"]);
                                cmd.Parameters.AddWithValue("@diachi", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdt", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@email", row["Email"]);
                            }
                            else if (selectedValue == "Giấy chứng nhận")
                            {
                                query = @"
                                UPDATE giayChNhan
                                SET id_TCChNhan = (SELECT id_TCChNhan FROM tochucChNhan WHERE tenTCChNhan = @tenTCChNhan),
                                    tenChNhan = @tenChNhan,
                                    ngayChNhan = @ngayChNhan,
                                    ngayHetHan = @ngayHetHan
                                WHERE id_ChNhan = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã giấy chứng nhận"]);
                                cmd.Parameters.AddWithValue("@tenTCChNhan", row["Tên tổ chức chứng nhận"]);
                                cmd.Parameters.AddWithValue("@tenChNhan", row["Tên giấy chứng nhận"]);
                                cmd.Parameters.AddWithValue("@ngayChNhan", row["Ngày cấp"]);
                                cmd.Parameters.AddWithValue("@ngayHetHan", row["Ngày hết hạn"]);
                            }
                            else if (selectedValue == "Hộ chăn nuôi nhỏ lẻ")
                            {
                                query = @"
                                UPDATE hoCnuoiNhole
                                SET tenChuHo = @tenChuHo,
                                    sdtChuHo = @sdtChuHo,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    thongke = @thongke
                                WHERE id_hoCnuoi = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã hộ"]);
                                cmd.Parameters.AddWithValue("@tenChuHo", row["Tên chủ hộ"]);
                                cmd.Parameters.AddWithValue("@sdtChuHo", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@thongke", row["Thống kê"]);
                            }
                            else if (selectedValue == "Cơ sở chăn nuôi")
                            {
                                query = @"
                                UPDATE CSChanNuoi
                                SET tencNuoi = @tencNuoi,
                                    id_dkcNuoi = (SELECT id_dkcNuoi FROM dkcNuoi WHERE tendkCnuoi = @tendkCnuoi),
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachicNuoi = @diachicNuoi,
                                    sdtcNuoi = @sdtcNuoi,
                                    emailcNuoi = @emailcNuoi
                                WHERE id_cNuoi = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@tencNuoi", row["Tên cơ sở"]);
                                cmd.Parameters.AddWithValue("@tendkCnuoi", row["Điều kiện chăn nuôi"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachicNuoi", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdtcNuoi", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@emailcNuoi", row["Email"]);
                            }
                            else if (selectedValue == "Cơ sở chế biến")
                            {
                                query = @"
                                UPDATE csCheBien
                                SET tencBien = @tencBien,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachicBien = @diachicBien,
                                    sdtcBien = @sdtcBien,
                                    emailcBien = @emailcBien
                                WHERE id_cBien = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@tencBien", row["Tên cơ sở"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachicBien", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdtcBien", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@emailcBien", row["Email"]);
                            }
                            else if (selectedValue == "Loại dịch bệnh")
                            {
                                query = @"
                                UPDATE dichBenh
                                SET tendichBenh = @tendichBenh,
                                    mota = @mota
                                WHERE id_dichBenh = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã dịch bệnh"]);
                                cmd.Parameters.AddWithValue("@tendichBenh", row["Tên dịch bệnh"]);
                                cmd.Parameters.AddWithValue("@mota", row["Mô tả"]);
                            }
                            else if (selectedValue == "Vùng dịch bệnh")
                            {
                                query = @"
                                UPDATE VungdichBenh
                                SET tenVungdich = @tenVungdich,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    id_dichBenh = (SELECT id_dichBenh FROM dichBenh WHERE tendichBenh = @tendichBenh),
                                    ngaykhoiphat = @ngaykhoiphat,
                                    tinhtrang = @tinhtrang
                                WHERE id_Vungdich = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã vùng dịch"]);
                                cmd.Parameters.AddWithValue("@tenVungdich", row["Tên vùng dịch"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@tendichBenh", row["Tên dịch bệnh"]);
                                cmd.Parameters.AddWithValue("@ngaykhoiphat", row["Ngày khởi phát"]);
                                cmd.Parameters.AddWithValue("@tinhtrang", row["Tình trạng"] is string && row["Tình trạng"].ToString() == "Đã xử lý" ? 1 : 0);
                            }
                            else if (selectedValue == "Chi cục thú y")
                            {
                                query = @"
                                UPDATE cCucTY
                                SET tencCucTY = @tencCucTY,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachicCucTY = @diachicCucTY,
                                    sdtcCucTY = @sdtcCucTY,
                                    emailcCucTY = @emailcCucTY
                                WHERE id_cCucTY = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã chi cục"]);
                                cmd.Parameters.AddWithValue("@tencCucTY", row["Tên chi cục"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachicCucTY", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdtcCucTY", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@emailcCucTY", row["Email"]);
                            }
                            else if (selectedValue == "Đại lý bán thuốc thú y")
                            {
                                query = @"
                                UPDATE dLythuoc
                                SET tendLy = @tendLy,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachidLY = @diachidLY,
                                    sdtdLy = @sdtdLy,
                                    emaildLy = @emaildLy
                                WHERE id_dLy = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã đại lý"]);
                                cmd.Parameters.AddWithValue("@tendLy", row["Tên đại lý"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachidLY", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdtdLy", row["Số điện thoại"]);
                                cmd.Parameters.AddWithValue("@emaildLy", row["Email"]);
                            }
                            else if (selectedValue == "Cơ sở tạm giữ")
                            {
                                query = @"
                                UPDATE khuTGiu
                                SET id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachitGiu = @diachitGiu,
                                    stttGiu = @stttGiu
                                WHERE id_tGiu = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachitGiu", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@stttGiu", row["Số điện thoại"]);
                            }
                            else if (selectedValue == "Cơ sở giết mổ")
                            {
                                query = @"
                                UPDATE CsGietMo
                                SET tenGietMo = @tenGietMo,
                                    id_Huyen = (SELECT id_Huyen FROM huyen WHERE tenHuyen = @tenHuyen),
                                    id_Xa = (SELECT id_Xa FROM xa WHERE tenXa = @tenXa),
                                    diachiGietMo = @diachiGietMo,
                                    sdtGietMo = @sdtGietMo
                                WHERE id_GietMo = @id";

                                cmd.CommandText = query;
                                cmd.Parameters.AddWithValue("@id", row["Mã cơ sở"]);
                                cmd.Parameters.AddWithValue("@tenGietMo", row["Tên cơ sở"]);
                                cmd.Parameters.AddWithValue("@tenHuyen", row["Huyện"]);
                                cmd.Parameters.AddWithValue("@tenXa", row["Xã"]);
                                cmd.Parameters.AddWithValue("@diachiGietMo", row["Địa chỉ"]);
                                cmd.Parameters.AddWithValue("@sdtGietMo", row["Số điện thoại"]);
                            }
                            else
                            {
                                MessageBox.Show("Chức năng cập nhật cho mục này chưa được hỗ trợ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }

                            // Thực thi câu lệnh UPDATE (cập nhật dữ liệu)
                            cmd.ExecuteNonQuery();
                        }
                        // Thông báo khi cập nhật thành công
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        // Thông báo khi có lỗi trong quá trình cập nhật
                        MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        //Bam nut mo cua so sua thong tin ca nhan
        private void SuaTT_Click(object sender, RoutedEventArgs e)
        {
            SuaThongTinCaNhan suaTT = new SuaThongTinCaNhan(_userID, _userName, _userEmail);
            bool? result = suaTT.ShowDialog();
        }

        //Doi mat khau
        private void doiMK_Click(object sender, RoutedEventArgs e)
        {
            var doiMK = new DoiMatKhau(_userID);
            bool? result = doiMK.ShowDialog();
        }
        //Lich su dang nhap tai khoan
        private void LsDN_Click(object sender, RoutedEventArgs e)
        {
            var lsDN = new LichSuDangNhap(_userID);
            lsDN.ShowDialog();
        }

        private void tvND_Click(object sender, RoutedEventArgs e)
        {
            if (_userRole == "QuanTri")
            {
                var tvND = new TruyVanNguoiDung();
                tvND.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chức năng này chỉ dùng cho quản trị viên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

    }
}



