using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Policy;
using JiebaNet;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using static EiOuDonkey.Program;

namespace EiOuDonkey
{
    public class DefaultOutput
    {
        public static int Output(string s)
        {
            Console.WriteLine(s);
            return 0;
        }
    }

    public class AllInfosOutput
    {
        public static int Output(string s)
        {
            Type t = typeof(Program);
            System.Reflection.FieldInfo[] members = t.GetFields(); 
            foreach(System.Reflection.FieldInfo info in members)
            {
                Console.WriteLine($"Name: {info.Name} \t Type: {info.GetType()} \t Value: {info.GetValue(info)!.ToString()}");
            }
            return 0;
        }
    }

    public class LolcatOutput
    {
        public static bool Test_lolcat()
        {
            Process p = new Process();
            p.StartInfo.FileName = "bash";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("command -v lolcat");
            p.StandardInput.Close();
            if (p.ExitCode == 0)
                return true;
            else return false;

        }
        public static void Use_lolcat(string s)
        {
            Process p = new Process();
            p.StartInfo.FileName = "bash";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine($"echo \"{s}\" | lolcat");
            p.StandardInput.Close();
            p.WaitForExit();
        }

        public static int Output(string s)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix || force_use_lolcat)
            {
                if (Test_lolcat())
                    Use_lolcat(s);
                else
                {
                    Console.WriteLine($"{Red}Error: lolcat is not found , directly ouput.{RESET}");
                    use_color = false;
                }
            }
            else
            {
                Console.WriteLine($"{Red}lolcat(-c) can only be used on linux!{RESET}\n{Green}Alert: Add -f to force use lolcat whatever the operating system is , if you are sure lolcat is installed.{RESET}");
                return -1;
            }
            return 0;
        }
    }
}