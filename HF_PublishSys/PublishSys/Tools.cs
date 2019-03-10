using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PublishSys
{
	public class Tools
	{
		public static void DeleteFolder(string dir, Panel plMsg)
		{
			plMsg?.Refresh();
			if (Directory.Exists(dir))
			{
				try
				{
					Process process = new Process();
					process.StartInfo.FileName = "cmd.exe";
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardInput = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.CreateNoWindow = true;
					process.Start();
					process.StandardInput.WriteLine("del \"" + dir + "\\*.*\" /Q/F&exit");
					process.WaitForExit();
					process.Close();
					if (Directory.GetFiles(dir).Length + Directory.GetDirectories(dir).Length < 1 && Directory.Exists(dir))
					{
						Directory.Delete(dir);
					}
					if (Directory.Exists(dir))
					{
						string[] directories = Directory.GetDirectories(dir);
						foreach (string dir2 in directories)
						{
							DeleteFolder(dir2, plMsg);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		public static void CopyDirectory(string srcdir, string desdir, Panel plMsg, int times = 0)
		{
			plMsg?.Refresh();
			string str = "";
			int num = 0;
			if (times > 0)
			{
				num = srcdir.LastIndexOf("\\");
				str = srcdir.Substring(num + 1);
			}
			string text = desdir + "\\" + str;
			if (desdir.LastIndexOf("\\") == desdir.Length - 1)
			{
				text = desdir + "\\" + str;
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(srcdir);
			string[] array = fileSystemEntries;
			foreach (string text2 in array)
			{
				if (Directory.Exists(text2))
				{
					num = text2.IndexOf(srcdir);
					string str2 = text2.Substring(num + srcdir.Length);
					string path = desdir + str2;
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					CopyDirectory(text2, text, plMsg, 1);
				}
				else
				{
					string str3 = text2.Substring(text2.LastIndexOf("\\") + 1);
					str3 = text + "\\" + str3;
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					File.Copy(text2, str3, overwrite: true);
				}
			}
		}
	}
}
