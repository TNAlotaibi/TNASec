using System;
using System.Text;
using System.Windows.Forms;

namespace TNASec
{
    public partial class Form1 : Form
    {

        static Random rndm = new Random();
        static int rndmNumber = 0;
        static string HexLetter = "ABCDEF";
        static char[] ArrayChars = { 'ሐ', 'አ', 'ម', 'ఎ', 'л', '噸', '我', 'ए', 'फ', 'Ф', 'ؤ', 'ء', 'ئ', 'Q', '+', '!', '@', '~', '#', '$', '%', '^', '&', '*', '(', ')', '{', '}', '_', '-', '>', '<', '이', '자', '형', 'ó', 'ʻ', 'ü' };
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = EncryptString(textBox1.Text);
        }

        static byte[] ReturnByteFromHex(string hex)
        {
            byte[] Text = new byte[hex.Length / 2];
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return Text;
        }
        public static string ConvertStringToASCII(string Text)
        {
            byte[] AsciiNumber = Encoding.ASCII.GetBytes(Text);
            string StringStore = null;
            foreach (int value in AsciiNumber)
            {
                StringStore += ConvertASCIIToHex(value);
            }
            return StringStore;
        }
    
        public static string ConvertASCIIToHex(int ASCIINumber)
        {
        DoAgain:
            int newNum = rndm.Next(0, 10);
            if (newNum == rndmNumber) goto DoAgain;
            rndmNumber = newNum;
            string Hex = string.Format("{0:X}", ASCIINumber);
            if (IsDigit(Hex[0]) && IsDigit(Hex[1]))
            {
                return newNum + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + Hex[0].ToString() + ArrayChars[newNum] + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + Hex[1].ToString();
            }
            return newNum + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + Hex[0].ToString() + Converter(Hex[1].ToString(), true) + ArrayChars[newNum] + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString() + ArrayChars[rndm.Next(0, ArrayChars.Length)].ToString();
        }
        public static string Decrypt(string Hex)
        {
            if (Hex == "Error") return Hex + " in decrypt :)";
            string text = string.Empty, result = string.Empty;
            try
            {
                foreach (char chr in Hex)
                {
                    if (IsDigit(chr))
                    {
                        text += chr;
                    }
                    else if (IsDigit(Hex[Hex.IndexOf(chr) - 1]) && HexLetter.Contains(chr.ToString()))
                    {
                        text += chr;
                    }
                }
                for (int z = 0; z <= text.Length - 3; z += 3)
                {
                    result += Encoding.ASCII.GetString(ReturnByteFromHex(text[z + 1].ToString() + text[z + 2].ToString()));
                }
                return result;
            }
            catch
            {
                return "Error in decrypt :)";
            }
        }
        static string Converter(string text,bool HexLetter)
        {
            try
            {
                if (HexLetter)
                {   
                    return text.Replace("A", "हजार番目の手紙").Replace("B", "ब番目の手紙").Replace("C", "သုံး番目の手紙").Replace("E", "ສີ່番目の手紙").Replace("F", "lètki番目の手紙");
                }
                else
                {
                    for (var index = 0; index < ArrayChars.Length - 1; index++)
                    {
                        if (text.Contains(ArrayChars[index].ToString()))
                        {
                            return text.Replace("हजार番目の手紙", "A").Replace("ब番目の手紙", "B").Replace("သုံး番目の手紙", "C").Replace("ສີ່番目の手紙", "E").Replace("lètki番目の手紙", "F");
                        }
                    }
                    return "Error";
                }  
            }
            catch
            {
                return "Error";
            }
    
        }
        static bool IsDigit(char chr)
        {
            return Char.IsDigit(chr);
        }
        string EncryptString(string text)
        {
            string result = ConvertStringToASCII(text);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Decrypt(Converter(textBox1.Text, false));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
            button3.Text = "Copied";
        }

    }
}
