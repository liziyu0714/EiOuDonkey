using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Policy;
using JiebaNet;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using EiOuDonkey;
using static EiOuDonkey.Program;
using static EiOuDonkey.TextColors;

namespace EiOuDonkey
{
    public static class Program
    {
        
        public static bool change_all = false;
        public static bool use_color = false;
        public static bool ingore_overwrite = false;
        public static bool force_use_lolcat = false;
        public static bool remind_overwrite = false;
        public static bool output_all_infos = false;
        public static Random random = new Random();

        public static string orignal_string = "";
        public static string rendered_string = "";
        public static string result_string = "";

        public static int Main(string[] Args)
        {
            string input = "";
            try
            {
                input = ArgsResolver.ResolveArg(Args);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{Red}Exception in ArgsResolver: {ex.Message} {RESET}");
                return -1;
            }

            orignal_string = input;

            input = input.Replace("，", "");
            input = input.Replace("。", "");
            input = input.Replace("；", "");
            input = input.Replace(",", "");
            input = input.Replace(".", "");

            rendered_string = input;

            var poscut = new PosSegmenter();
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

            result_string = output;

            if (use_color)
                return LolcatOutput.Output(output);
            if(output_all_infos)
                return AllInfosOutput.Output(output);
            return DefaultOutput.Output(output);
        }

    }
    
    

}

