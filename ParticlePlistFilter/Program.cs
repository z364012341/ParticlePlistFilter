using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CE.iPhone.PList;
using System.IO;
using System.Security.AccessControl;

namespace ParticlePlistFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            String workPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            DirectoryInfo dir = new DirectoryInfo(workPath);
            //path为某个目录，如： “D:\Program Files”
            FileInfo[] inf = dir.GetFiles();
            foreach (FileInfo finf in inf)
            {
                try
                {
                    if (finf.Extension.Equals(".plist"))
                    {//如果扩展名为“.xml”
                        PListRoot root = PListRoot.Load(finf.ToString());
                        PListDict dic = (PListDict)root.Root;
                        if (dic.ContainsKey("textureFileName"))
                        {
                            //PListString name = (PListString)dic["textureFileName"];
                            //if (name != "")
                            //{
                            //String deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                            String dicPath = Path.Combine(workPath, "ParticlePlistFilter");
                            if (!Directory.Exists(dicPath))
                            {
                                Directory.CreateDirectory(dicPath);
                                //DirectoryInfo dirinfo = new DirectoryInfo(dicPath);
                                //System.Security.AccessControl.DirectorySecurity dirSecurity = dirinfo.GetAccessControl();
                                //dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                                //dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                                //dirinfo.SetAccessControl(dirSecurity);
                            }
                            //System.IO.File.Copy(dicPath, Path.Combine(workPath, finf.ToString()), true);
                            //System.IO.File.Copy(dicPath, Path.Combine(workPath, (PListString)dic["textureFileName"]), true);
                            RunCMD("xcopy /y " + Path.Combine(workPath, finf.ToString()) + " " + dicPath + " /i");
                            RunCMD("xcopy /y " + Path.Combine(workPath, (PListString)dic["textureFileName"]) + " " + dicPath + " /i");
                            // }
                        }

                    }
                }
                catch
                {
                    System.Console.WriteLine("!!!");
                }

                //读取文件的完整目录和文件名
            }
        }

        public static void RunCMD(String str)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.Start();//启动程序
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str);
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
        }
    }
}
