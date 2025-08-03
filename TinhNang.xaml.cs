using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace QuanLyChanNuoi
{
    public partial class TinhNang : Window
    {
        private const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QLCN;Integrated Security=True;TrustServerCertificate=True";

        public enum PanelHienThi
        {
            H,
            X,
            CSSX,
            CSKN,
            ToChuc,
            CaNhan,
            dkcNuoi,
            tochucChNhan,
            giayChNhan,
            hoCnuoiNhole,
            coSoCheBien,
            CSChanNuoi,
            dichBenh,
            vungDichBenh,
            chiCucThuY,
            daiLyThuoc,
            khuTamGiu,
            coSoGietMo,
        }

        public PanelHienThi PanelCanHienThi { get; set; }

        public TinhNang()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            H.Visibility = Visibility.Collapsed;
            X.Visibility = Visibility.Collapsed;
            CSSX.Visibility = Visibility.Collapsed;

            switch (PanelCanHienThi)
            {
                case PanelHienThi.H:
                    H.Visibility = Visibility.Visible;
                    break;
                case PanelHienThi.X:
                    X.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(HComboBox);
                    break;
                case PanelHienThi.CSSX:
                    CSSX.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(HCSSX);
                    if (HCSSX.SelectedValue != null)
                    {
                        string idHuyen = HCSSX.SelectedValue.ToString();
                        LoadXaComboBox(XCSSX, idHuyen);
                    }
                    HCSSX.SelectionChanged += (sender, e) => H_SelectionChanged(HCSSX, XCSSX, sender, e);
                    break;
                case PanelHienThi.CSKN:
                    CSKN.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hcskn);
                    if (hcskn.SelectedValue != null)
                    {
                        string idHuyen = hcskn.SelectedValue.ToString();
                        LoadXaComboBox(xcskn, idHuyen);
                    }
                    hcskn.SelectionChanged += (sender, e) => H_SelectionChanged(hcskn, xcskn, sender, e);
                    break;
                case PanelHienThi.ToChuc:
                    ToChuc.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(htc);
                    LoadCoSoChanNuoi(cscntc);
                    if (htc.SelectedValue != null)
                    {
                        string idHuyen = htc.SelectedValue.ToString();
                        LoadXaComboBox(xtc, idHuyen);
                    }
                    htc.SelectionChanged += (sender, e) => H_SelectionChanged(htc, xtc, sender, e);
                    break;
                case PanelHienThi.CaNhan:
                    CaNhan.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hcn);
                    LoadCoSoChanNuoi(cscncn);
                    if (hcn.SelectedValue != null)
                    {
                        string idHuyen = hcn.SelectedValue.ToString();
                        LoadXaComboBox(xcn, idHuyen);
                    }
                    hcn.SelectionChanged += (sender, e) => H_SelectionChanged(hcn, xcn, sender, e);
                    break;
                case PanelHienThi.dkcNuoi:
                    dkcNuoi.Visibility = Visibility.Visible;
                    break;
                case PanelHienThi.tochucChNhan:
                    tochucChNhan.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(htccnhan);
                    if (htccnhan.SelectedValue != null)
                    {
                        string idHuyen = htccnhan.SelectedValue.ToString();
                        LoadXaComboBox(xtccnhan, idHuyen);
                    }
                    htccnhan.SelectionChanged += (sender, e) => H_SelectionChanged(htccnhan, xtccnhan, sender, e);
                    break;
                case PanelHienThi.giayChNhan:
                    giayChNhan.Visibility = Visibility.Visible;
                    LoadToChucChungNhanComboBox(tcgcn);
                    break;
                case PanelHienThi.hoCnuoiNhole:
                    hoCnuoiNhole.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hhcn);
                    if (hhcn.SelectedValue != null)
                    {
                        string idHuyen = hhcn.SelectedValue.ToString();
                        LoadXaComboBox(xhcn, idHuyen);
                    }
                    hhcn.SelectionChanged += (sender, e) => H_SelectionChanged(hhcn, xhcn, sender, e);
                    break; ;
                case PanelHienThi.coSoCheBien:
                    coSoCheBien.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hcb);
                    if (hcb.SelectedValue != null)
                    {
                        string idHuyen = hcb.SelectedValue.ToString();
                        LoadXaComboBox(xcbien, idHuyen);
                    }
                    hcb.SelectionChanged += (sender, e) => H_SelectionChanged(hcb, xcbien, sender, e);
                    break;
                case PanelHienThi.CSChanNuoi:
                    CSChanNuoi.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(huyencNuoi_cb);
                    LoadDieuKienChanNuoi();
                    if (huyencNuoi_cb.SelectedValue != null)
                    {
                        string idHuyen = huyencNuoi_cb.SelectedValue.ToString();
                        LoadXaComboBox(xacNuoi_cb, idHuyen);
                    }
                    huyencNuoi_cb.SelectionChanged += (sender, e) => H_SelectionChanged(huyencNuoi_cb, xacNuoi_cb, sender, e);
                    break;
                case PanelHienThi.dichBenh:
                    dichBenh.Visibility = Visibility.Visible;
                    break;
                case PanelHienThi.vungDichBenh:
                    vungDichBenh.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hvd);
                    LoadDichBenhComboBox(dbvd);
                    if (hvd.SelectedValue != null)
                    {
                        string idHuyen = hvd.SelectedValue.ToString();
                        LoadXaComboBox(xvd, idHuyen);
                    }
                    hvd.SelectionChanged += (sender, e) => H_SelectionChanged(hvd, xvd, sender, e);
                    break;
                case PanelHienThi.chiCucThuY:
                    chiCucThuY.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hccty);
                    if (hccty.SelectedValue != null)
                    {
                        string idHuyen = hccty.SelectedValue.ToString();
                        LoadXaComboBox(xccty, idHuyen);
                    }
                    hccty.SelectionChanged += (sender, e) => H_SelectionChanged(hccty, xccty, sender, e);
                    break;
                case PanelHienThi.daiLyThuoc:
                    daiLyThuoc.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hdly);
                    if (hdly.SelectedValue != null)
                    {
                        string idHuyen = hdly.SelectedValue.ToString();
                        LoadXaComboBox(xdly, idHuyen);
                    }
                    hdly.SelectionChanged += (sender, e) => H_SelectionChanged(hdly, xdly, sender, e);
                    break;
                case PanelHienThi.khuTamGiu:
                    khuTamGiu.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(htgiu);
                    if (htgiu.SelectedValue != null)
                    {
                        string idHuyen = htgiu.SelectedValue.ToString();
                        LoadXaComboBox(xtgiu, idHuyen);
                    }
                    htgiu.SelectionChanged += (sender, e) => H_SelectionChanged(htgiu, xtgiu, sender, e);
                    break;
                case PanelHienThi.coSoGietMo:
                    coSoGietMo.Visibility = Visibility.Visible;
                    LoadHuyenComboBox(hgm);
                    if (hgm.SelectedValue != null)
                    {
                        string idHuyen = hgm.SelectedValue.ToString();
                        LoadXaComboBox(xgm, idHuyen);
                    }
                    hgm.SelectionChanged += (sender, e) => H_SelectionChanged(hgm, xgm, sender, e);
                    break;
            }
        }

        private void ah2_Click(object sender, RoutedEventArgs e)
        {
            string idHuyen = aidh.Text.Trim();
            string tenHuyen = ath.Text.Trim();

            if (string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(tenHuyen))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(1) FROM huyen WHERE id_Huyen = @idHuyen OR tenHuyen = @tenHuyen";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        checkCommand.Parameters.AddWithValue("@tenHuyen", tenHuyen);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID huyện hoặc tên huyện đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO huyen (id_Huyen, tenHuyen) VALUES (@idHuyen, @tenHuyen)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@tenHuyen", tenHuyen);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm huyện thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearHuyenIF();
                        }
                        else
                        {
                            MessageBox.Show("Thêm huyện không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearHuyenIF()
        {
            aidh.Text = "";
            ath.Text = "";
        }

        public class Huyen
        {
            public string id_Huyen { get; set; }
            public string tenHuyen { get; set; }
        }

        public class Xa
        {
            public string id_Xa { get; set; }
            public string tenXa { get; set; }
        }

        public class ToChucChungNhan
        {
            public string id_TCChNhan { get; set; }
            public string tenTCChNhan { get; set; }
        }
        public class DichBenh
        {
            public string id_dichBenh { get; set; }
            public string tendichBenh { get; set; }
        }
        private void LoadDichBenhComboBox(ComboBox cb)
        {
            try
            {
                List<DichBenh> danhSachDichBenh = new List<DichBenh>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT id_dichBenh, tendichBenh FROM dichBenh";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DichBenh dichBenh = new DichBenh
                            {
                                id_dichBenh = reader["id_dichBenh"].ToString(),
                                tendichBenh = reader["tendichBenh"].ToString()
                            };
                            danhSachDichBenh.Add(dichBenh);
                        }
                    }
                }

                cb.ItemsSource = danhSachDichBenh;
                cb.DisplayMemberPath = "tendichBenh";
                cb.SelectedValuePath = "id_dichBenh";

                if (danhSachDichBenh.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu dịch bệnh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadToChucChungNhanComboBox(ComboBox cb)
        {
            try
            {
                List<ToChucChungNhan> danhSachToChuc = new List<ToChucChungNhan>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT id_TCChNhan, tenTCChNhan FROM tochucChNhan";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ToChucChungNhan tc = new ToChucChungNhan
                            {
                                id_TCChNhan = reader["id_TCChNhan"].ToString(),
                                tenTCChNhan = reader["tenTCChNhan"].ToString()
                            };
                            danhSachToChuc.Add(tc);
                        }
                    }
                }

                cb.ItemsSource = danhSachToChuc;
                cb.DisplayMemberPath = "tenTCChNhan";   // Hiển thị tên
                cb.SelectedValuePath = "id_TCChNhan";  // Giá trị lưu trữ là ID

                if (danhSachToChuc.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tổ chức chứng nhận: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Hàm load huyện cho ComboBox
        private void LoadHuyenComboBox(ComboBox cb)
        {
            try
            {
                List<Huyen> danhSachHuyen = new List<Huyen>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT id_Huyen, tenHuyen FROM huyen";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Huyen huyen = new Huyen
                            {
                                id_Huyen = reader["id_Huyen"].ToString(),
                                tenHuyen = reader["tenHuyen"].ToString()
                            };
                            danhSachHuyen.Add(huyen);
                        }
                    }
                }

                cb.ItemsSource = danhSachHuyen;
                cb.DisplayMemberPath = "tenHuyen";
                cb.SelectedValuePath = "id_Huyen";

                if (danhSachHuyen.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu huyện: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadXaComboBox(ComboBox cb, string idHuyen)
        {
            try
            {
                List<Xa> danhSachXa = new List<Xa>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT id_Xa, tenXa FROM xa WHERE id_Huyen = @idHuyen";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idHuyen", idHuyen);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            danhSachXa.Add(new Xa
                            {
                                id_Xa = reader["id_Xa"].ToString(),
                                tenXa = reader["tenXa"].ToString()
                            });
                        }
                    }
                }

                cb.ItemsSource = danhSachXa;
                cb.DisplayMemberPath = "tenXa";
                cb.SelectedValuePath = "id_Xa";

                if (danhSachXa.Count > 0)
                    cb.SelectedIndex = 0;
                else
                    cb.SelectedIndex = -1; // Không có xã nào
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu xã: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void H_SelectionChanged(ComboBox comboBoxHuyen, ComboBox comboBoxXa, object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxHuyen.SelectedValue != null)
            {
                string idHuyen = comboBoxHuyen.SelectedValue.ToString();
                LoadXaComboBox(comboBoxXa, idHuyen); // Cập nhật lại ComboBox xã khi thay đổi huyện
            }
        }


        // Hàm thêm xã
        private void ax2_Click(object sender, RoutedEventArgs e)
        {
            if (HComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn huyện từ danh sách.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string idHuyen = HComboBox.SelectedValue.ToString();
            string idXa = aidx.Text.Trim();
            string tenXa = atx.Text.Trim();

            if (string.IsNullOrEmpty(idXa) || string.IsNullOrEmpty(tenXa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID xã hoặc tên xã đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM xa WHERE id_Xa = @idXa OR tenXa = @tenXa";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idXa", idXa);
                        checkCommand.Parameters.AddWithValue("@tenXa", tenXa);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID xã hoặc tên xã đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm xã mới
                    string insertQuery = "INSERT INTO xa (id_Xa, id_Huyen, tenXa) VALUES (@idXa, @idHuyen, @tenXa)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@tenXa", tenXa);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm xã thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearXaIF();
                        }
                        else
                        {
                            MessageBox.Show("Thêm xã không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void LoadDieuKienChanNuoi()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT id_dkcNuoi, tendkCnuoi FROM dkcNuoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Gán vào ComboBox
                        dkcNuoi_cb.ItemsSource = dt.DefaultView;
                        dkcNuoi_cb.DisplayMemberPath = "tendkCnuoi"; // Hiển thị tên
                        dkcNuoi_cb.SelectedValuePath = "id_dkcNuoi"; // Lấy ID khi chọn
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


        // Hàm clear input sau khi thêm
        private void ClearXaIF()
        {
            aidx.Text = "";
            atx.Text = "";
        }
        private void acssx2_Click(object sender, RoutedEventArgs e)
        {
            if (HCSSX.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn huyện từ danh sách.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (XCSSX.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn xã từ danh sách.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string idHuyen = HCSSX.SelectedValue.ToString();
            string idXa = XCSSX.SelectedValue.ToString();
            string idCSD = idcssx.Text.Trim();
            string tenCSD = tcssx.Text.Trim();
            string diachiCSD = dccssx.Text.Trim();
            string sdtCSD = sdtcssx.Text.Trim();
            string emailCSD = ecssx.Text.Trim();

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(idCSD) || string.IsNullOrEmpty(tenCSD) || string.IsNullOrEmpty(diachiCSD) || string.IsNullOrEmpty(sdtCSD) || string.IsNullOrEmpty(emailCSD))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID cơ sở sản xuất đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM SXsphamXlycThai WHERE id_sXuat = @idCSD";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idCSD", idCSD);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID cơ sở đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm cơ sở sản xuất mới
                    string insertQuery = "INSERT INTO SXsphamXlycThai (id_sXuat, tensXuat, id_Huyen, id_Xa, diachisXuat, sdtsXuat, emailsXuat) VALUES (@idCSD, @tenCSD, @idHuyen, @idXa, @diachiCSD, @sdtCSD, @emailCSD)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idCSD", idCSD);
                        insertCommand.Parameters.AddWithValue("@tenCSD", tenCSD);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diachiCSD", diachiCSD);
                        insertCommand.Parameters.AddWithValue("@sdtCSD", sdtCSD);
                        insertCommand.Parameters.AddWithValue("@emailCSD", emailCSD);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cơ sở sản xuất thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearCSSXInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cơ sở sản xuất không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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


        private void ClearCSSXInputFields()
        {
            // Xóa nội dung các trường nhập liệu
            idcssx.Clear();
            tcssx.Clear();
            dccssx.Clear();
            sdtcssx.Clear();
            ecssx.Clear();
        }

        private void acskn2_Click(object sender, RoutedEventArgs e)
        {
            if (hcskn.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn huyện từ danh sách.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (xcskn.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn xã từ danh sách.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string idHuyen = hcskn.SelectedValue.ToString();
            string idXa = xcskn.SelectedValue.ToString();
            string idKN = idcskn.Text.Trim();
            string tenKN = tcskn.Text.Trim();
            string diachiKN = dccskn.Text.Trim();
            string sdtKN = sdtcskn.Text.Trim();
            string emailKN = ecskn.Text.Trim();

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(idKN) || string.IsNullOrEmpty(tenKN) || string.IsNullOrEmpty(diachiKN) || string.IsNullOrEmpty(sdtKN) || string.IsNullOrEmpty(emailKN))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID cơ sở khảo nghiệm đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM KNsphamXlycThai WHERE id_kNghiem = @idKN";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idKN", idKN);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID cơ sở khảo nghiệm đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm cơ sở khảo nghiệm mới
                    string insertQuery = "INSERT INTO KNsphamXlycThai (id_kNghiem, tenkNghiem, id_Huyen, id_Xa, diachikNghiem, sdtkNghiem, emailkNghiem) VALUES (@idKN, @tenKN, @idHuyen, @idXa, @diachiKN, @sdtKN, @emailKN)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idKN", idKN);
                        insertCommand.Parameters.AddWithValue("@tenKN", tenKN);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diachiKN", diachiKN);
                        insertCommand.Parameters.AddWithValue("@sdtKN", sdtKN);
                        insertCommand.Parameters.AddWithValue("@emailKN", emailKN);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cơ sở khảo nghiệm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearCSKNInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cơ sở khảo nghiệm không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearCSKNInputFields()
        {
            // Xóa nội dung các trường nhập liệu
            idcskn.Clear();
            tcskn.Clear();
            dccskn.Clear();
            sdtcskn.Clear();
            ecskn.Clear();
        }

        private void adkcn2_Click(object sender, RoutedEventArgs e)
        {
            string idDKCN = iddkcn.Text.Trim();
            string tenDKCN = tdkcn.Text.Trim();
            DateTime? ngayHL = nhldkcn.SelectedDate;
            DateTime? ngayHH = nhhdkcn.SelectedDate;

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(idDKCN) || string.IsNullOrEmpty(tenDKCN) || ngayHL == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin. (Ngày hiệu lực bắt buộc)", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID điều kiện chăn nuôi đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM dkcNuoi WHERE id_dkcNuoi = @idDKCN";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idDKCN", idDKCN);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID điều kiện chăn nuôi đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm điều kiện chăn nuôi mới
                    string insertQuery = "INSERT INTO dkcNuoi (id_dkcNuoi, tendkCnuoi, ngaycapdkCnuoi, ngayhethandkCnuoi) VALUES (@idDKCN, @tenDKCN, @ngayHL, @ngayHH)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idDKCN", idDKCN);
                        insertCommand.Parameters.AddWithValue("@tenDKCN", tenDKCN);
                        insertCommand.Parameters.AddWithValue("@ngayHL", ngayHL.Value);

                        if (ngayHH != null)
                        {
                            insertCommand.Parameters.AddWithValue("@ngayHH", ngayHH.Value);
                        }
                        else
                        {
                            insertCommand.Parameters.AddWithValue("@ngayHH", DBNull.Value);
                        }

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm điều kiện chăn nuôi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearDKCNInputFields();
                        }
                        else
                        {
                            MessageBox.Show("Thêm điều kiện chăn nuôi không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearDKCNInputFields()
        {
            iddkcn.Clear();
            tdkcn.Clear();
            nhldkcn.SelectedDate = null;
            nhhdkcn.SelectedDate = null;
        }
        private void xacNhan_cNuoi_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các ComboBox khóa ngoại đã chọn chưa
            if (dkcNuoi_cb.SelectedItem == null || huyencNuoi_cb.SelectedItem == null || xacNuoi_cb.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Huyện, Xã và Điều kiện chăn nuôi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy dữ liệu người dùng nhập
            string idCN = idcNuoi.Text.Trim();
            string tenCN = tencNuoi.Text.Trim();
            string idDKCN = dkcNuoi_cb.SelectedValue.ToString();
            string idHuyen = huyencNuoi_cb.SelectedValue.ToString();
            string idXa = xacNuoi_cb.SelectedValue.ToString();
            string diaChi = diachicNuoi.Text.Trim();
            string sdt = sdtcNuoi.Text.Trim();
            string email = emailcNuoi.Text.Trim();

            // Kiểm tra ô trống
            if (string.IsNullOrEmpty(idCN) || string.IsNullOrEmpty(tenCN) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID cơ sở đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM CSChanNuoi WHERE id_cNuoi = @idCN";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idCN", idCN);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID cơ sở chăn nuôi đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm cơ sở chăn nuôi mới
                    string insertQuery = @"INSERT INTO CSChanNuoi 
                (id_cNuoi, tencNuoi, id_dkcNuoi, id_Huyen, id_Xa, diachicNuoi, sdtcNuoi, emailcNuoi) 
                VALUES 
                (@idCN, @tenCN, @idDKCN, @idHuyen, @idXa, @diaChi, @sdt, @email)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idCN", idCN);
                        insertCommand.Parameters.AddWithValue("@tenCN", tenCN);
                        insertCommand.Parameters.AddWithValue("@idDKCN", idDKCN);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);
                        insertCommand.Parameters.AddWithValue("@email", email);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cơ sở chăn nuôi thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearCoSoChanNuoiIF();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cơ sở chăn nuôi không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
        private void ClearCoSoChanNuoiIF()
        {
            idcNuoi.Clear();
            tencNuoi.Clear();
            dkcNuoi_cb.SelectedIndex = -1;
            huyencNuoi_cb.SelectedIndex = -1;
            xacNuoi_cb.SelectedIndex = -1;
            diachicNuoi.Clear();
            sdtcNuoi.Clear();
            emailcNuoi.Clear();
        }

        private void LoadCoSoChanNuoi(ComboBox cscNuoi_cb)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT id_cNuoi, tencNuoi FROM CSChanNuoi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Gán dữ liệu vào ComboBox (ví dụ tên là cscNuoi_cb)
                        cscNuoi_cb.ItemsSource = dt.DefaultView;
                        cscNuoi_cb.DisplayMemberPath = "tencNuoi";
                        cscNuoi_cb.SelectedValuePath = "id_cNuoi";
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
        private void atc2_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các ComboBox phải chọn đầy đủ
            if (htc.SelectedItem == null || xtc.SelectedItem == null || cscntc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ huyện, xã và cơ sở chăn nuôi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy dữ liệu từ input
            string idHuyen = htc.SelectedValue.ToString();
            string idXa = xtc.SelectedValue.ToString();
            string idcNuoi = cscntc.SelectedValue.ToString();
            string idToChuc = idtc.Text.Trim();
            string tenToChuc = ttc.Text.Trim();
            string tenNguoiDaiDien = tndd.Text.Trim();
            string diaChiNguoiDaiDien = dcndd.Text.Trim();
            string sdtToChuc = sdttc.Text.Trim();
            string emailToChuc = etc.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idToChuc) || string.IsNullOrEmpty(tenToChuc) || string.IsNullOrEmpty(tenNguoiDaiDien)
                || string.IsNullOrEmpty(diaChiNguoiDaiDien) || string.IsNullOrEmpty(sdtToChuc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID tổ chức đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM ToChuc WHERE id_tChuc = @idToChuc";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idToChuc", idToChuc);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID tổ chức đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm tổ chức mới
                    string insertQuery = @"INSERT INTO ToChuc (id_cNuoi, id_tChuc, tentChuc, id_Huyen, id_Xa, nguoidaidien, email, sdttChuc, diachiNguoidaidien)
                                   VALUES (@idcNuoi, @idToChuc, @tenToChuc, @idHuyen, @idXa, @nguoiDaiDien, @Email, @sdtToChuc, @diaChiNguoiDaiDien)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idcNuoi", idcNuoi);
                        insertCommand.Parameters.AddWithValue("@idToChuc", idToChuc);
                        insertCommand.Parameters.AddWithValue("@tenToChuc", tenToChuc);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@nguoiDaiDien", tenNguoiDaiDien);
                        insertCommand.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(emailToChuc) ? (object)DBNull.Value : emailToChuc);
                        insertCommand.Parameters.AddWithValue("@sdtToChuc", sdtToChuc);
                        insertCommand.Parameters.AddWithValue("@diaChiNguoiDaiDien", diaChiNguoiDaiDien);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm tổ chức thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearToChucForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm tổ chức không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
        private void ClearToChucForm()
        {
            idtc.Clear();
            ttc.Clear();
            tndd.Clear();
            dcndd.Clear();
            sdttc.Clear();
            etc.Clear();

            htc.SelectedIndex = -1;
            xtc.SelectedIndex = -1;
            cscntc.SelectedIndex = -1;
        }
        private void acn2_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các ComboBox phải chọn đầy đủ
            if (hcn.SelectedItem == null || xcn.SelectedItem == null || cscncn.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ huyện, xã và cơ sở chăn nuôi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy dữ liệu từ input
            string idHuyen = hcn.SelectedValue.ToString();
            string idXa = xcn.SelectedValue.ToString();
            string idcNuoi = cscncn.SelectedValue.ToString();
            string idCaNhan = cccdcn.Text.Trim();
            string tenCaNhan = tcn.Text.Trim();
            string diaChiCaNhan = dccn.Text.Trim();
            string sdtCaNhan = sdtcn.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idCaNhan) || string.IsNullOrEmpty(tenCaNhan)
                || string.IsNullOrEmpty(diaChiCaNhan) || string.IsNullOrEmpty(sdtCaNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra CCCD (id_cNhan) đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM CaNhan WHERE id_cNhan = @idCaNhan";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idCaNhan", idCaNhan);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("CCCD đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm cá nhân mới
                    string insertQuery = @"INSERT INTO CaNhan (id_cNuoi, id_Huyen, id_Xa, id_cNhan, diachicNhan, tencNhan, sdtcNhan)
                                   VALUES (@idcNuoi, @idHuyen, @idXa, @idCaNhan, @diaChiCaNhan, @tenCaNhan, @sdtCaNhan)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idcNuoi", idcNuoi);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@idCaNhan", idCaNhan);
                        insertCommand.Parameters.AddWithValue("@diaChiCaNhan", diaChiCaNhan);
                        insertCommand.Parameters.AddWithValue("@tenCaNhan", tenCaNhan);
                        insertCommand.Parameters.AddWithValue("@sdtCaNhan", sdtCaNhan);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cá nhân thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearCaNhanForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cá nhân không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearCaNhanForm()
        {
            cccdcn.Clear();
            tcn.Clear();
            dccn.Clear();
            sdtcn.Clear();

            hcn.SelectedIndex = -1;
            xcn.SelectedIndex = -1;
            cscncn.SelectedIndex = -1;
        }
        private void atccnhan2_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra ComboBox phải chọn đầy đủ
            if (htccnhan.SelectedItem == null || xtccnhan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ huyện và xã.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Lấy dữ liệu từ input
            string idHuyen = htccnhan.SelectedValue.ToString();
            string idXa = xtccnhan.SelectedValue.ToString();
            string idTCChNhan = idtccnhan.Text.Trim();
            string tenTCChNhan = ttccnhan.Text.Trim();
            string diaChiTCChNhan = dctccnhan.Text.Trim();
            string sdtTCChNhan = sdttccnhan.Text.Trim();
            string emailTCChNhan = etccnhan.Text.Trim();

            // Kiểm tra các ô bắt buộc
            if (string.IsNullOrEmpty(idTCChNhan) || string.IsNullOrEmpty(tenTCChNhan) ||
                string.IsNullOrEmpty(diaChiTCChNhan) || string.IsNullOrEmpty(sdtTCChNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM tochucChNhan WHERE id_TCChNhan = @idTCChNhan";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idTCChNhan", idTCChNhan);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID tổ chức chứng nhận đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm mới
                    string insertQuery = @"INSERT INTO tochucChNhan (id_TCChNhan, tenTCChNhan, id_Huyen, id_Xa, diachiTCChNhan, sdtTCChNhan, emailTCChNhan)
                                   VALUES (@idTCChNhan, @tenTCChNhan, @idHuyen, @idXa, @diaChiTCChNhan, @sdtTCChNhan, @EmailTCChNhan)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idTCChNhan", idTCChNhan);
                        insertCommand.Parameters.AddWithValue("@tenTCChNhan", tenTCChNhan);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChiTCChNhan", diaChiTCChNhan);
                        insertCommand.Parameters.AddWithValue("@sdtTCChNhan", sdtTCChNhan);
                        insertCommand.Parameters.AddWithValue("@EmailTCChNhan", string.IsNullOrEmpty(emailTCChNhan) ? (object)DBNull.Value : emailTCChNhan);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm tổ chức chứng nhận thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearToChucChNhanForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm tổ chức chứng nhận không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearToChucChNhanForm()
        {
            idtccnhan.Clear();
            ttccnhan.Clear();
            dctccnhan.Clear();
            sdttccnhan.Clear();
            etccnhan.Clear();

            htccnhan.SelectedIndex = -1;
            xtccnhan.SelectedIndex = -1;
        }
        private void agcn2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idGiayChungNhan = idgcn.Text.Trim();
            string tenGiayChungNhan = tgcn.Text.Trim();
            string idToChucChungNhan = tcgcn.SelectedValue?.ToString();  // Lấy ID tổ chức
            DateTime? ngayChungNhan = ngcn.SelectedDate;
            DateTime? ngayHetHan = nhhgcn.SelectedDate;

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idGiayChungNhan) || string.IsNullOrEmpty(tenGiayChungNhan) || string.IsNullOrEmpty(idToChucChungNhan) || ngayChungNhan == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (ID, Tên, Tổ chức, Ngày chứng nhận).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID giấy chứng nhận đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM giayChNhan WHERE id_ChNhan = @idGiayChungNhan";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idGiayChungNhan", idGiayChungNhan);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID giấy chứng nhận đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm giấy chứng nhận mới
                    string insertQuery = @"INSERT INTO giayChNhan (id_ChNhan, id_TCChNhan, tenChNhan, ngayChNhan, ngayHetHan)
                                   VALUES (@idChNhan, @idToChuc, @tenChNhan, @ngayChNhan, @ngayHetHan)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idChNhan", idGiayChungNhan);
                        insertCommand.Parameters.AddWithValue("@idToChuc", idToChucChungNhan);
                        insertCommand.Parameters.AddWithValue("@tenChNhan", tenGiayChungNhan);
                        insertCommand.Parameters.AddWithValue("@ngayChNhan", ngayChungNhan.Value);

                        // Nếu ngày hết hạn không chọn thì để NULL trong SQL
                        if (ngayHetHan.HasValue)
                        {
                            insertCommand.Parameters.AddWithValue("@ngayHetHan", ngayHetHan.Value);
                        }
                        else
                        {
                            insertCommand.Parameters.AddWithValue("@ngayHetHan", DBNull.Value);
                        }

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm giấy chứng nhận thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearGiayChungNhanForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm giấy chứng nhận không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearGiayChungNhanForm()
        {
            idgcn.Text = "";
            tgcn.Text = "";
            tcgcn.SelectedIndex = -1;
            ngcn.SelectedDate = null;
            nhhgcn.SelectedDate = null;
        }
        private void ahcn2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idHoChanNuoi = idhcnnl.Text.Trim();
            string tenHoChanNuoi = thcn.Text.Trim();
            string tenChuHo = tchcn.Text.Trim();
            string sdtChuHo = sdthcn.Text.Trim();
            string idHuyen = hhcn.SelectedValue?.ToString();
            string idXa = xhcn.SelectedValue?.ToString();
            string thongKe = tkhcn.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idHoChanNuoi) || string.IsNullOrEmpty(tenHoChanNuoi) || string.IsNullOrEmpty(tenChuHo)
                || string.IsNullOrEmpty(sdtChuHo) || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (ID, Tên hộ, Tên chủ hộ, SĐT, Huyện, Xã).",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID hộ chăn nuôi đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM hoCnuoiNhole WHERE id_hoCnuoi = @idHoChanNuoi";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idHoChanNuoi", idHoChanNuoi);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID hộ chăn nuôi nhỏ lẻ đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm hộ chăn nuôi mới
                    string insertQuery = @"INSERT INTO hoCnuoiNhole (id_hoCnuoi, tenChuHo, sdtChuHo, id_Huyen, id_Xa, thongke)
                                   VALUES (@idHoChanNuoi, @tenChuHo, @sdtChuHo, @idHuyen, @idXa, @thongke)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idHoChanNuoi", idHoChanNuoi);
                        insertCommand.Parameters.AddWithValue("@tenChuHo", tenChuHo);
                        insertCommand.Parameters.AddWithValue("@sdtChuHo", sdtChuHo);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@thongke", string.IsNullOrEmpty(thongKe) ? (object)DBNull.Value : thongKe);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm hộ chăn nuôi nhỏ lẻ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearHoChanNuoiForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm hộ chăn nuôi nhỏ lẻ không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearHoChanNuoiForm()
        {
            idhcnnl.Text = "";
            thcn.Text = "";
            tchcn.Text = "";
            sdthcn.Text = "";
            hhcn.SelectedIndex = -1;
            xhcn.SelectedIndex = -1;
            tkhcn.Text = "";
        }
        private void acb2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idCoSoCheBien = idcscb.Text.Trim();
            string tenCoSoCheBien = tcscbien.Text.Trim();
            string idHuyen = hcb.SelectedValue?.ToString();
            string idXa = xcbien.SelectedValue?.ToString();
            string diaChi = dccbien.Text.Trim();
            string sdt = sdtcbien.Text.Trim();
            string email = emailcbien.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idCoSoCheBien) || string.IsNullOrEmpty(tenCoSoCheBien) || string.IsNullOrEmpty(idHuyen)
                || string.IsNullOrEmpty(idXa) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID cơ sở chế biến đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM csCheBien WHERE id_cBien = @idCoSoCheBien";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idCoSoCheBien", idCoSoCheBien);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID cơ sở chế biến đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm cơ sở chế biến mới
                    string insertQuery = @"INSERT INTO csCheBien (id_cBien, tencBien, id_Huyen, id_Xa, diachicBien, sdtcBien, emailcBien)
                                   VALUES (@idCoSoCheBien, @tenCoSoCheBien, @idHuyen, @idXa, @diaChi, @sdt, @email)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idCoSoCheBien", idCoSoCheBien);
                        insertCommand.Parameters.AddWithValue("@tenCoSoCheBien", tenCoSoCheBien);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);
                        insertCommand.Parameters.AddWithValue("@email", email);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cơ sở chế biến thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearCoSoCheBienForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cơ sở chế biến không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearCoSoCheBienForm()
        {
            idcscb.Text = "";
            tcscbien.Text = "";
            hcb.SelectedIndex = -1;
            xcbien.SelectedIndex = -1;
            dccbien.Text = "";
            sdtcbien.Text = "";
            emailcbien.Text = "";
        }
        private void adb2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idDichBenh = iddb.Text.Trim();
            string tenDichBenh = tdb.Text.Trim();
            string moTa = mtdb.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idDichBenh) || string.IsNullOrEmpty(tenDichBenh) || string.IsNullOrEmpty(moTa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (ID, Tên dịch bệnh, Mô tả).",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID dịch bệnh đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM dichBenh WHERE id_dichBenh = @idDichBenh";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idDichBenh", idDichBenh);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID dịch bệnh đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm dịch bệnh mới
                    string insertQuery = @"INSERT INTO dichBenh (id_dichBenh, tendichBenh, mota)
                                   VALUES (@idDichBenh, @tenDichBenh, @moTa)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idDichBenh", idDichBenh);
                        insertCommand.Parameters.AddWithValue("@tenDichBenh", tenDichBenh);
                        insertCommand.Parameters.AddWithValue("@moTa", moTa);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm dịch bệnh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearDichBenhForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm dịch bệnh không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearDichBenhForm()
        {
            iddb.Text = "";
            tdb.Text = "";
            mtdb.Text = "";
        }
        private void avd2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idVungDich = idvd.Text.Trim();
            string tenVungDich = tvd.Text.Trim();
            string idHuyen = hvd.SelectedValue?.ToString();
            string idXa = xvd.SelectedValue?.ToString();
            string idDichBenh = dbvd.SelectedValue?.ToString();
            DateTime? ngayKhoiPhat = nkpvd.SelectedDate;

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idVungDich) || string.IsNullOrEmpty(tenVungDich)
                || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa)
                || string.IsNullOrEmpty(idDichBenh) || ngayKhoiPhat == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID vùng dịch đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM VungdichBenh WHERE id_Vungdich = @idVungDich";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idVungDich", idVungDich);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID vùng dịch đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm vùng dịch bệnh mới
                    string insertQuery = @"INSERT INTO VungdichBenh 
                                    (id_Vungdich, tenVungdich, id_Huyen, id_Xa, id_dichBenh, ngaykhoiphat)
                                   VALUES 
                                    (@idVungDich, @tenVungDich, @idHuyen, @idXa, @idDichBenh, @ngayKhoiPhat)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idVungDich", idVungDich);
                        insertCommand.Parameters.AddWithValue("@tenVungDich", tenVungDich);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@idDichBenh", idDichBenh);
                        insertCommand.Parameters.AddWithValue("@ngayKhoiPhat", ngayKhoiPhat.Value);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm vùng dịch bệnh thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearVungDichBenhForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm vùng dịch bệnh không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
        private void ClearVungDichBenhForm()
        {
            idvd.Text = "";
            tvd.Text = "";
            hvd.SelectedIndex = -1;
            xvd.SelectedIndex = -1;
            dbvd.SelectedIndex = -1;
            nkpvd.SelectedDate = null;
        }
        private void accty2_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ input
            string idChiCuc = idccty.Text.Trim();
            string tenChiCuc = tccty.Text.Trim();
            string idHuyen = hccty.SelectedValue?.ToString();
            string idXa = xccty.SelectedValue?.ToString();
            string diaChi = dcccty.Text.Trim();
            string sdt = sdtccty.Text.Trim();
            string email = eccty.Text.Trim();

            // Kiểm tra các ô không được để trống
            if (string.IsNullOrEmpty(idChiCuc) || string.IsNullOrEmpty(tenChiCuc)
                || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa)
                || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Kiểm tra ID chi cục đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM cCucTY WHERE id_cCucTY = @idChiCuc";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idChiCuc", idChiCuc);

                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID chi cục thú y đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Thêm chi cục thú y mới
                    string insertQuery = @"INSERT INTO cCucTY 
                                    (id_cCucTY, tencCucTY, id_Huyen, id_Xa, diachicCucTY, sdtcCucTY, emailcCucTY)
                                   VALUES 
                                    (@idChiCuc, @tenChiCuc, @idHuyen, @idXa, @diaChi, @sdt, @email)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idChiCuc", idChiCuc);
                        insertCommand.Parameters.AddWithValue("@tenChiCuc", tenChiCuc);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);
                        insertCommand.Parameters.AddWithValue("@email", email);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm chi cục thú y thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearChiCucThuYForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm chi cục thú y không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
        private void ClearChiCucThuYForm()
        {
            idccty.Text = "";
            tccty.Text = "";
            hccty.SelectedIndex = -1;
            xccty.SelectedIndex = -1;
            dcccty.Text = "";
            sdtccty.Text = "";
            eccty.Text = "";
        }
        private void adl2_Click(object sender, RoutedEventArgs e)
        {
            string idDaiLy = iddly.Text.Trim();
            string tenDaiLy = tdly.Text.Trim();
            string idHuyen = hdly.SelectedValue?.ToString();
            string idXa = xdly.SelectedValue?.ToString();
            string diaChi = dcdly.Text.Trim();
            string sdt = sdtdly.Text.Trim();
            string email = edly.Text.Trim();

            if (string.IsNullOrEmpty(idDaiLy) || string.IsNullOrEmpty(tenDaiLy)
                || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa)
                || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(1) FROM dLythuoc WHERE id_dLy = @idDaiLy";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idDaiLy", idDaiLy);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID đại lý đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO dLythuoc 
                        (id_dLy, tendLy, id_Huyen, id_Xa, diachidLY, sdtdLy, emaildLy)
                        VALUES 
                        (@idDaiLy, @tenDaiLy, @idHuyen, @idXa, @diaChi, @sdt, @email)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idDaiLy", idDaiLy);
                        insertCommand.Parameters.AddWithValue("@tenDaiLy", tenDaiLy);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);
                        insertCommand.Parameters.AddWithValue("@email", email);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm đại lý thuốc thú y thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearDaiLyThuocForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm đại lý không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearDaiLyThuocForm()
        {
            iddly.Text = "";
            tdly.Text = "";
            hdly.SelectedIndex = -1;
            xdly.SelectedIndex = -1;
            dcdly.Text = "";
            sdtdly.Text = "";
            edly.Text = "";
        }
        private void atg_Click(object sender, RoutedEventArgs e)
        {
            string idKhu = idtgiu.Text.Trim();
            string tenKhu = ttgiu.Text.Trim();
            string idHuyen = htgiu.SelectedValue?.ToString();
            string idXa = xtgiu.SelectedValue?.ToString();
            string diaChi = dctgiu.Text.Trim();
            string sdt = sdttgiu.Text.Trim();

            if (string.IsNullOrEmpty(idKhu) || string.IsNullOrEmpty(tenKhu)
                || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa)
                || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(1) FROM khuTGiu WHERE id_tGiu = @idKhu";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idKhu", idKhu);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID khu tạm giữ đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO khuTGiu 
                        (id_tGiu, id_Huyen, id_Xa, diachitGiu, stttGiu)
                        VALUES 
                        (@idKhu, @idHuyen, @idXa, @diaChi, @sdt)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idKhu", idKhu);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm khu tạm giữ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearKhuTamGiuForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm khu tạm giữ không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearKhuTamGiuForm()
        {
            idtgiu.Text = "";
            ttgiu.Text = "";
            htgiu.SelectedIndex = -1;
            xtgiu.SelectedIndex = -1;
            dctgiu.Text = "";
            sdttgiu.Text = "";
        }
        private void agm2_Click(object sender, RoutedEventArgs e)
        {
            string idGietMo = idgm.Text.Trim();
            string tenGietMo = tgm.Text.Trim();
            string idHuyen = hgm.SelectedValue?.ToString();
            string idXa = xgm.SelectedValue?.ToString();
            string diaChi = dcgm.Text.Trim();
            string sdt = sdtgm.Text.Trim();

            if (string.IsNullOrEmpty(idGietMo) || string.IsNullOrEmpty(tenGietMo)
                || string.IsNullOrEmpty(idHuyen) || string.IsNullOrEmpty(idXa)
                || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(1) FROM CsGietMo WHERE id_GietMo = @idGietMo";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@idGietMo", idGietMo);
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("ID cơ sở giết mổ đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    string insertQuery = @"INSERT INTO CsGietMo 
                        (id_GietMo, tenGietMo, id_Huyen, id_Xa, diachiGietMo, sdtGietMo)
                        VALUES 
                        (@idGietMo, @tenGietMo, @idHuyen, @idXa, @diaChi, @sdt)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@idGietMo", idGietMo);
                        insertCommand.Parameters.AddWithValue("@tenGietMo", tenGietMo);
                        insertCommand.Parameters.AddWithValue("@idHuyen", idHuyen);
                        insertCommand.Parameters.AddWithValue("@idXa", idXa);
                        insertCommand.Parameters.AddWithValue("@diaChi", diaChi);
                        insertCommand.Parameters.AddWithValue("@sdt", sdt);

                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã thêm cơ sở giết mổ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            ClearGietMoForm();
                        }
                        else
                        {
                            MessageBox.Show("Thêm cơ sở giết mổ không thành công.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearGietMoForm()
        {
            idgm.Text = "";
            tgm.Text = "";
            hgm.SelectedIndex = -1;
            xgm.SelectedIndex = -1;
            dcgm.Text = "";
            sdtgm.Text = "";
        }

    }
}