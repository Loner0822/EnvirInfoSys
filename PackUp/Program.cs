using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PackUp
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        static int Main(string[] args)
        {
            Console.Title = "Packup";
            IntPtr intptr = FindWindow("ConsoleWindowClass", "Packup");

            if (intptr != IntPtr.Zero)
            {
                ShowWindow(intptr, 0);//隐藏这个窗口
            }

            string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
            IniOperator inip = new IniOperator(WorkPath + "RegInfo.ini");
            string AppName = inip.ReadString("Public", "AppName", "");
            AppName = AppName.Replace("\0", "");
            string UnitName = inip.ReadString("Public", "UnitName", "");
            UnitName = UnitName.Replace("\0", "");
            string VerNum = inip.ReadString("版本号", "VerNum", "");
            VerNum = VerNum.Replace("\0", "");

            inip = new IniOperator(WorkPath + "PackUp.ini");
            string my_app_name = inip.ReadString("packup", "my_app_name", "");
            string my_app_version = inip.ReadString("packup", "my_app_version", "");
            string my_app_publisher = inip.ReadString("packup", "my_app_publisher", "");
            string my_app_exe_name = inip.ReadString("packup", "my_app_exe_name", "");
            string my_app_id = inip.ReadString("packup", "my_app_id", "");
            string source_exe_path = inip.ReadString("packup", "source_exe_path", "");
            string source_path = inip.ReadString("packup", "source_path", "");
            string registry_subkey = inip.ReadString("packup", "registry_subkey", "");

            string str = File.ReadAllText(WorkPath + "区域环境信息化系统Raw.iss", Encoding.GetEncoding("GB2312"));
            str = str.Replace("MY_APP_NAME", my_app_publisher + my_app_name);
            str = str.Replace("MY_APP_VERSION", my_app_version);
            str = str.Replace("MY_APP_PUBLISHER", my_app_publisher);
            str = str.Replace("MY_APP_EXE_NAME", my_app_exe_name);
            str = str.Replace("APP_ID", my_app_id);
            str = str.Replace("SOURCE_EXE_PATH", source_exe_path);
            str = str.Replace("SOURCE_PATH", source_path);
            str = str.Replace("REGISTRY_SUBKEY", registry_subkey);
            str = str.Replace("\0", "");

            File.WriteAllText(WorkPath + "区域环境信息化系统.iss", str, Encoding.GetEncoding("GB2312"));
            return 0;
        }
    }
}
