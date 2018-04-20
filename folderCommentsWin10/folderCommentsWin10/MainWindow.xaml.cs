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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;



namespace folderCommentsWin10
{

    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string path = PathName.Text + "\\Desktop.ini";
            string pathf = PathName.Text;

            try
            {

                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    string myMessage = "[.ShellClassInfo]\r\nInfoTip = " + commentBox.Text + "\r\n";
                    Byte[] info = new UTF8Encoding(true).GetBytes(myMessage);
                    File.SetAttributes(path, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Archive);
                    fs.Write(info, 0, info.Length);
                    File.SetAttributes(pathf, FileAttributes.ReadOnly | FileAttributes.System);
                    //Can't figure out how to make the Windows explorer refresh these changes instantly. 
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                    MessageBox.Show("Done!");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                PathName.Text = files[0];
            }
        }
    }
}
