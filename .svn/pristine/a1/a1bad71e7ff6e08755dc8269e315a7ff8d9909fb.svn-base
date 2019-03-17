using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PublishSys
{
    public class IniOperator
    {
        public string FileName;
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public IniOperator()
        {
        }

        //初始化，获取文件名
        public IniOperator(string AFileName)
        {
            FileInfo fileinfo = new FileInfo(AFileName);
            if (!fileinfo.Exists)
            {
                StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
            }
            FileName = fileinfo.FullName;
        }

        //-------写字符串-----------
        public bool WriteString(string Section, string Ident, string Value)
        {
            return WritePrivateProfileString(Section, Ident, Value, FileName);
        }

        //--------读字符串------------
        public string ReadString(string Section, string Ident, string Default)
        {
            byte[] Buffer = new byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }

        //-----------写整数---------
        public void WriteInteger(string Section, string Ident, int Value)
        {
            WriteString(Section, Ident, Value.ToString());
        }

        //-------------读整数---------
        public int ReadInteger(string Section, string Ident, int Default)
        {
            string intStr = ReadString(Section, Ident, Convert.ToString(Default));
            try
            {
                return Convert.ToInt32(intStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Default;
            }
        }

        //-----------写 bool 值-----------
        public void WriteBool(string Section, string Ident, bool Value)
        {
            WriteString(Section, Ident, Convert.ToString(Value));
        }

        //------------读 bool 值 -------------
        public bool ReadBool(string Section, string Ident, bool Default)
        {
            try
            {
                return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Default;
        }

        //---------------获取所有键--------
        public void ReadSection(string Section, StringCollection Ident)
        {
            Byte[] Buffer = new Byte[16384];
            int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(Buffer, bufLen, Ident);
        }

        private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if (Buffer[i] == 0)
                    {
                        i = start;
                    }
                    else
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }

        //-------------获取所有节点名-----------
        public void ReadSections(StringCollection SectionList)
        {
            byte[] Buffer = new byte[65535];
            int bufLen = 0;
            bufLen = GetPrivateProfileString(null, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
            GetStringsFromBuffer(Buffer, bufLen, SectionList);
        }

        //-------------获取指定节点下的所有值-----------
        public void ReadSectionValue(string Section, NameValueCollection Values)
        {
            StringCollection KeyList = new StringCollection();
            ReadSection(Section, KeyList);
            Values.Clear();
            foreach (string key in KeyList)
            {
                Values.Add(key, ReadString(Section, key, "none"));
            }
        }

        //----------清除节点-----------
        public void EraseSection(string Section)
        {
            WritePrivateProfileString(Section, null, null, FileName);
        }

        //-------------删除键--------------
        public void DeleteKey(string Section, string Ident)
        {
            WritePrivateProfileString(Section, Ident, null, FileName);
        }

        //ini文件有变化时执行此文件
        public void UpdateFile()
        {
            WritePrivateProfileString(null, null, null, FileName);
        }

        //判断键是否存在
        public bool ValueExists(string Section, string Ident)
        {
            StringCollection Idents = new StringCollection();
            ReadSection(Section, Idents);
            return Idents.IndexOf(Ident) > -1;
        }

        //----析构方法----
        ~IniOperator()
        {
            UpdateFile();
        }
    }
}
