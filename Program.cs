using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using JiebaNet;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using static EiOuDonkey.Program;

namespace EiOuDonkey
{
    public static class Program
    {
        public const string Red = "\x1B[31m";
        public const string Yellow = "\x1B[33m";
        public const string Green = "\x1B[32m";
        public const string RESET = "\x1B[0m";
        public static bool change_all = false;
        public static bool use_color = false;
        public static bool ingore_overwrite = false;
        public static bool force_use_lolcat = false;
        public static bool remind_overwrite = false;
        public static bool output_all_infos = false;
        public static Random random = new Random();

        public static int Main(string[] Args)
        {

            string input = "";
            var poscut = new PosSegmenter();

            foreach (string t in Args)
            {
                if (t != "" && (t[0] == '-' || t[0] == '/'))
                    switch (t[1])
                    {
                        case 'A':
                        case 'a':
                            change_all = true;
                            break;
                        case 'C':
                        case 'c':
                            use_color = true;
                            break;
                        case 'F':
                        case 'f':
                            ingore_overwrite = true;
                            force_use_lolcat = true;
                            break;
                        case 'I':
                        case 'i':
                            output_all_infos = true;
                            break;
                        default:
                            Console.WriteLine($"{Yellow}Warning : Can not recognise input arg:-{t[1]}{RESET}");
                            break;
                    }
                else
                {
                    if (input != "")
                        remind_overwrite = true;
                    input = t;
                }


            }

            if (input == "")
            {
                Console.WriteLine($"{Red}Error:No text input. Abort.{RESET}");
                return -1;
            }
            if (remind_overwrite && !ingore_overwrite)
                Console.WriteLine($"{Yellow}Warning : The text has been overwrite. Use \" to input space or use -f to ingore this warning {RESET}");

            input.Replace("，", "");
            input.Replace("。", "");
            input.Replace(",", "");
            input.Replace(".", "");

            var result = poscut.Cut(input);
            string output = "";

            foreach (var a in result)
            {
                if (random.Next(0, 100) > 50 && (a.Flag == "r" || a.Flag == "n" || a.Flag == "ns" || a.Flag == "nt" || a.Flag == "nz" || a.Flag == "nl" || a.Flag == "ng" || a.Flag == "nr1" || a.Flag == "nr2" || a.Flag == "t") || change_all)
                {
                    if (random.Next(0, 100) < 50)
                        output += $"{a.Word},欸～,";
                    else
                        output += $"{a.Word},奥～,";
                }
                else if (!a.Flag.Contains("w"))
                    output += $"{a.Word}";

            }

            if (output[output.Length - 1] == ',')
                output += "OK啦";
            else
                output += ",OK啦";

            if (use_color)
                return LolcatOutput.Output(output);
            return DefaultOutput.Output(output);
        }

    }
    public class DefaultOutput
    {
        public static int Output(string s)
        {
            Console.WriteLine(s);
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
                    Console.WriteLine($"{Red}Error:lolcat is not found , directly ouput.{RESET}");
                    use_color = false;
                }
            }
            else
            {
                Console.WriteLine($"{Red}lolcat(-c) can only be used on linux!{RESET}\n{Green}Alert:Add -f to force use lolcat whatever the operating system is , if you are sure lolcat is installed.{RESET}");
                return -1;
            }
            return 0;
        }
    }
}

