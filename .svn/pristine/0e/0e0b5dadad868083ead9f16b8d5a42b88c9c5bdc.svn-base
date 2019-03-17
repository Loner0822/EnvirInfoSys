using System.Text;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System;
using System.Diagnostics;

namespace PublishSys
{
    public class Webservice
    {
        public static string InvokeWebservice(string url, string methodname, object[] args)
        {
            try
            {
                string @namespace = "zbxh";
                System.Net.WebClient wc = new System.Net.WebClient();
                System.IO.Stream stream = wc.OpenRead(url + "?WSDL");
                System.Web.Services.Description.ServiceDescription sd = System.Web.Services.Description.ServiceDescription.Read(stream);
                string classname = sd.Services[0].Name;
                System.Web.Services.Description.ServiceDescriptionImporter sdi = new System.Web.Services.Description.ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace(@namespace);
                System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);

                Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();
                System.CodeDom.Compiler.ICodeCompiler icc = csc.CreateCompiler();

                System.CodeDom.Compiler.CompilerParameters cplist = new System.CodeDom.Compiler.CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                System.CodeDom.Compiler.CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return (string)mi.Invoke(obj, args);
            }
            catch
            {
                return "error";
            }
        }
        public static bool Download(string url, string localfile)
        {
            bool flag = false;
            long startPosition = 0; // 上次下载的文件起始位置
            FileStream writeStream; // 写入本地文件流对象

            long remoteFileLength = GetHttpLength(url);// 取得远程文件长度
            SendMessage(remoteFileLength.ToString(), 4000);
            if (remoteFileLength == 0)
            {
                return false;
            }
            if (File.Exists(localfile))
            {
                writeStream = File.OpenWrite(localfile);
                startPosition = writeStream.Length;
                if (startPosition >= remoteFileLength)
                {
                    return true;
                }
                else
                {
                    writeStream.Seek(startPosition, SeekOrigin.Current);
                }
            }
            else
            {
                writeStream = new FileStream(localfile, FileMode.Create);
                startPosition = 0;
            }


            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);// 打开网络连接
                if (startPosition > 0)
                {
                    myRequest.AddRange((int)startPosition);
                }

                Stream readStream = myRequest.GetResponse().GetResponseStream();
                byte[] btArray = new byte[512];
                int contentSize = readStream.Read(btArray, 0, btArray.Length);

                long currPostion = startPosition;

                while (contentSize > 0)
                {
                    currPostion += contentSize;//下载进度
                    writeStream.Write(btArray, 0, contentSize);// 写入本地文件
                    SendMessage(currPostion.ToString(), 5000);
                    contentSize = readStream.Read(btArray, 0, btArray.Length);
                }
                writeStream.Close();
                readStream.Close();
                flag = true;
            }
            catch (Exception)
            {
                writeStream.Close();
                flag = false;
            }

            return flag;
        }
        
        private static long GetHttpLength(string url)
        {
            long length = 0;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();

                if (rsp.StatusCode == HttpStatusCode.OK)
                {
                    length = rsp.ContentLength;
                }
                rsp.Close();
                return length;
            }
            catch
            {
                return length;
            }

        }
        private static void SendMessage(string strText, int data)
        {

            IntPtr hwndRecvWindow = ImportFromDLL.FindWindow(null, "Form2");
            IntPtr hwndSendWindow = Process.GetCurrentProcess().Handle;

            ImportFromDLL.COPYDATASTRUCT copydata = new ImportFromDLL.COPYDATASTRUCT();
            copydata.cbData = data;
            copydata.lpData = strText;//内容  

            //发送消息
            ImportFromDLL.SendMessage(hwndRecvWindow, ImportFromDLL.WM_COPYDATA, hwndSendWindow, ref copydata);

            return;
        }
    }

}
