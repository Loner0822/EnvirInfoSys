using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublishSys
{
    public class Tools
    {
        #region 删除文件夹
        //plMsg 是显示进度条的 panel
        static public void DeleteFolder(string dir, Panel plMsg)
        {
            if (plMsg != null)
            {
                plMsg.Refresh();
            }
            if (!Directory.Exists(dir))
            {
                return;
            }
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                // /Q/F 选项表示不必用户确认，直接执行 del 命令
                p.StandardInput.WriteLine("del \"" + dir + "\\*.*\" /Q/F&exit");
                //string strOuput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();

                if (Directory.GetFiles(dir).Length + Directory.GetDirectories(dir).Length < 1)
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir);
                    }
                    //删除文件夹之后，需暂停一会儿，否则，文件夹状态还是存在的
                    //Thread.Sleep(200);
                }

                if (Directory.Exists(dir))
                {
                    foreach (string ditm in Directory.GetDirectories(dir))
                    {
                        DeleteFolder(ditm, plMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 拷贝文件夹
        //times 是递归调用的次数，初次调用设置为 0
        static public void CopyDirectory(string srcdir, string desdir, Panel plMsg, int times = 0)
        {
            if (plMsg != null)
            {
                plMsg.Refresh();
            }
            string folderName = "";
            int k = 0;
            if (times > 0)
            {
                k = srcdir.LastIndexOf("\\");
                folderName = srcdir.Substring(k + 1);
            }
            string desfolderdir = desdir + "\\" + folderName;
            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + "\\" + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);
            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))
                {
                    k = file.IndexOf(srcdir);
                    string tmp = file.Substring(k + srcdir.Length);
                    string connDir = desdir + tmp;
                    if (!Directory.Exists(connDir))
                    {
                        Directory.CreateDirectory(connDir);
                    }
                    CopyDirectory(file, desfolderdir, plMsg, 1);
                }
                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);
                    srcfileName = desfolderdir + "\\" + srcfileName;
                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }
                    File.Copy(file, srcfileName, true);
                }
            }//foreach 
        }
        #endregion
    }
}
