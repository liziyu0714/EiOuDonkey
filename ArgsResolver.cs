using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Policy;
using JiebaNet;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using static EiOuDonkey.Program;
using static EiOuDonkey.TextColors;

namespace EiOuDonkey
{
    public class ArgsResolver
    {
        public static string ResolveArg(string[] args)
        {
            string input = "";
            foreach (string t in args)
            {
                if (t != "" && (t[0] == '-' || t[0] == '/') && t[1] != '-')
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
                            Console.WriteLine($"{Yellow}Warning: Can not recognise input arg: -{t[1]}{RESET}");
                            break;
                    }
                else if (t != "" && t[0] == '-' && t[1] == '-')
                {
                    switch (t[2..])
                    {
                        case "all":
                            change_all = true;
                            break;
                        case "use-color":
                            use_color = true;
                            break;
                        case "force-all":
                            ingore_overwrite = force_use_lolcat = true;
                            break;
                        case "force-lolcat":
                            force_use_lolcat = true;
                            break;
                        case "force-ingore-overwrite":
                            ingore_overwrite = true;
                            break;
                        case "output-all-infos":
                            output_all_infos = true;
                            break;
                    }
                }
                else
                {
                    if (input != "")
                        remind_overwrite = true;
                    input = t;
                }
            }
            if (input == "")
                throw new Exception($"{Red}Error: No text input. Abort.{RESET}");
            if (remind_overwrite && !ingore_overwrite)
                Console.WriteLine($"{Yellow}Warning: The text has been overwrite. Use \" to input space or use -f to ingore this warning {RESET}");
            return input;
        }
        
    }
}