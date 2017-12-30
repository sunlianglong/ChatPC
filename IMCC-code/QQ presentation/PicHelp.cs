using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QQ_presentation
{
    class PicHelp
    {
        public static void UploadPic(string ip_address, string file, string table)
        {
            CreatLocalHeader(ip_address, file);
            BitmapImage bitmapImage;
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = System.IO.File.OpenRead(@file);
            bitmapImage.EndInit();
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);//very important, it should be set to the start of the stream
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);

            using (MySqlConnection conn = new MySqlConnection(GlobeClass.sqlconnet))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Insert into " + table + " values(@ip,@img)";
                cmd.Parameters.Add("@ip", MySql.Data.MySqlClient.MySqlDbType.VarString);
                cmd.Parameters.Add("@img", MySql.Data.MySqlClient.MySqlDbType.Blob);
                cmd.Parameters[0].Value = ip_address;
                cmd.Parameters[1].Value = imageData;
                cmd.Connection = conn;
                conn.Open();
                int affectedrows = cmd.ExecuteNonQuery();
                cmd.Dispose();//此处可以不用调用,  
                conn.Close();// 离开 using 块, connection 会自行关闭 
                MessageBox.Show("图片上传成功！");
                conn.Close();

            }
        }
        public static void DeletePic(string ip_address, string file, string table)
        {
            using (MySqlConnection conn = new MySqlConnection(GlobeClass.sqlconnet))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Delete from " + table + " where ip=@ip_address";
                cmd.Parameters.Add("@ip", MySql.Data.MySqlClient.MySqlDbType.VarString);
                cmd.Parameters[0].Value = ip_address;
                cmd.Connection = conn;
                conn.Open();
                int affectedrows = cmd.ExecuteNonQuery();
                cmd.Dispose();//此处可以不用调用,  
                conn.Close();// 离开 using 块, connection 会自行关闭 
            }
        }
        public static void ReloadPic(string ip_address, string file, string table)
        {
            CreatLocalHeader(ip_address, file);
            BitmapImage bitmapImage;
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = System.IO.File.OpenRead(@file);
            bitmapImage.EndInit();
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);//very important, it should be set to the start of the stream
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);

            using (MySqlConnection conn = new MySqlConnection(GlobeClass.sqlconnet))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update " + table + " set img=@img where ip='"+ip_address+"'";
                cmd.Parameters.Add("@img", MySql.Data.MySqlClient.MySqlDbType.Blob);
                cmd.Parameters[0].Value = imageData;
                cmd.Connection = conn;
                conn.Open();
                int affectedrows = cmd.ExecuteNonQuery();
                cmd.Dispose();//此处可以不用调用,  
                conn.Close();// 离开 using 块, connection 会自行关闭 
                MessageBox.Show("图片修改成功！");

            }
        }
        public static void saveForWhile(string ip_address, string file, string table)
        {
            CreatLocalHeader(ip_address, file);
            BitmapImage bitmapImage;
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = System.IO.File.OpenRead(@file);
            bitmapImage.EndInit();
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);//very important, it should be set to the start of the stream
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);

            using (MySqlConnection conn = new MySqlConnection(GlobeClass.sqlconnet))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update "+table+" set img=@img where ip=@ip_address";
                cmd.Parameters.Add("@ip", MySql.Data.MySqlClient.MySqlDbType.VarString);
                cmd.Parameters.Add("@img", MySql.Data.MySqlClient.MySqlDbType.Blob);
                cmd.Parameters[0].Value = ip_address;
                cmd.Parameters[1].Value = imageData;
                cmd.Connection = conn;
                conn.Open();
                int affectedrows = cmd.ExecuteNonQuery();
                cmd.Dispose();//此处可以不用调用,  
                conn.Close();// 离开 using 块, connection 会自行关闭 
                MessageBox.Show("图片修改成功！");

            }
        }
        public static Bitmap DownloadPic(string ip_address, string table)
        {
            string sql = "select img from " + table + " where ip='" + ip_address + "'";
            using (MySqlConnection conn = new MySqlConnection(GlobeClass.sqlconnet))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Read();
                    byte[] img1 = (byte[])reader["img"];
                    MemoryStream ms = new MemoryStream(img1);
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    Bitmap bmp = new Bitmap(ms);
                    return bmp;
                }
                else
                {
                    Bitmap bmp = null;
                    return bmp;
                }
                conn.Close();
            }
        }
        private static void CreatLocalHeader(string ip_address, string file)
        {
            string spath = "D:\\Program Files (x86)\\IMCC\\User\\" + ip_address + "\\header";
            if (Directory.Exists(spath))
            {

            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(spath);
                directoryInfo.Create();
            }
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            int rand = GlobeClass.GetRandomSeed();
            string rand_name = rand + ".png";
            cfa.AppSettings.Settings["HD_NUM"].Value = rand_name;
            string file2 = spath + "\\" + cfa.AppSettings.Settings["HD_NUM"].Value;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
            FileInfo f1 = new FileInfo(file);
            f1.CopyTo(file2, true);
            MessageBox.Show("缓存成功！");
        }

        public static ImageSource ChangeBitmapToImageSource(Bitmap bitmap)

        {

            //Bitmap bitmap = icon.ToBitmap();

            IntPtr hBitmap = bitmap.GetHbitmap();


            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(

                hBitmap,

                IntPtr.Zero,

                Int32Rect.Empty,

                BitmapSizeOptions.FromEmptyOptions());


            return wpfBitmap;

        }

    }
}
