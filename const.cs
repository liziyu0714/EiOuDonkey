using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Policy;
using JiebaNet;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using EiOuDonkey;
using static EiOuDonkey.Program;

namespace EiOuDonkey
{
    public static class TextColors
    {
        public const string Red = "\x1B[31m";
        public const string Yellow = "\x1B[33m";
        public const string Green = "\x1B[32m";
        public const string RESET = "\x1B[0m";
    }
}