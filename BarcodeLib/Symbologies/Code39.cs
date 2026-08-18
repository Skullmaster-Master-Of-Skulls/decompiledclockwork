using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200000E RID: 14
	internal class Code39 : BarcodeCommon, IBarcode
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public Code39(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006DD9 File Offset: 0x00004FD9
		public Code39(string input, bool AllowExtended)
		{
			this.Raw_Data = input;
			this._AllowExtended = AllowExtended;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006E05 File Offset: 0x00005005
		public Code39(string input, bool AllowExtended, bool EnableChecksum)
		{
			this.Raw_Data = input;
			this._AllowExtended = AllowExtended;
			this._EnableChecksum = EnableChecksum;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006E38 File Offset: 0x00005038
		private string Encode_Code39()
		{
			this.init_Code39();
			this.init_ExtendedCode39();
			string text = this.Raw_Data.Replace("*", "");
			string text2 = "*" + text + (this._EnableChecksum ? this.getChecksumChar(text).ToString() : string.Empty) + "*";
			if (this._AllowExtended)
			{
				this.InsertExtendedCharsIfNeeded(ref text2);
			}
			string text3 = "";
			foreach (char c in text2)
			{
				try
				{
					text3 += this.C39_Code[c].ToString();
					text3 += "0";
				}
				catch
				{
					if (this._AllowExtended)
					{
						base.Error("EC39-1: Invalid data.");
					}
					else
					{
						base.Error("EC39-1: Invalid data. (Try using Extended Code39)");
					}
				}
			}
			text3 = text3.Substring(0, text3.Length - 1);
			this.C39_Code.Clear();
			return text3;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006F4C File Offset: 0x0000514C
		private void init_Code39()
		{
			this.C39_Code.Clear();
			this.C39_Code.Add('0', "101001101101");
			this.C39_Code.Add('1', "110100101011");
			this.C39_Code.Add('2', "101100101011");
			this.C39_Code.Add('3', "110110010101");
			this.C39_Code.Add('4', "101001101011");
			this.C39_Code.Add('5', "110100110101");
			this.C39_Code.Add('6', "101100110101");
			this.C39_Code.Add('7', "101001011011");
			this.C39_Code.Add('8', "110100101101");
			this.C39_Code.Add('9', "101100101101");
			this.C39_Code.Add('A', "110101001011");
			this.C39_Code.Add('B', "101101001011");
			this.C39_Code.Add('C', "110110100101");
			this.C39_Code.Add('D', "101011001011");
			this.C39_Code.Add('E', "110101100101");
			this.C39_Code.Add('F', "101101100101");
			this.C39_Code.Add('G', "101010011011");
			this.C39_Code.Add('H', "110101001101");
			this.C39_Code.Add('I', "101101001101");
			this.C39_Code.Add('J', "101011001101");
			this.C39_Code.Add('K', "110101010011");
			this.C39_Code.Add('L', "101101010011");
			this.C39_Code.Add('M', "110110101001");
			this.C39_Code.Add('N', "101011010011");
			this.C39_Code.Add('O', "110101101001");
			this.C39_Code.Add('P', "101101101001");
			this.C39_Code.Add('Q', "101010110011");
			this.C39_Code.Add('R', "110101011001");
			this.C39_Code.Add('S', "101101011001");
			this.C39_Code.Add('T', "101011011001");
			this.C39_Code.Add('U', "110010101011");
			this.C39_Code.Add('V', "100110101011");
			this.C39_Code.Add('W', "110011010101");
			this.C39_Code.Add('X', "100101101011");
			this.C39_Code.Add('Y', "110010110101");
			this.C39_Code.Add('Z', "100110110101");
			this.C39_Code.Add('-', "100101011011");
			this.C39_Code.Add('.', "110010101101");
			this.C39_Code.Add(' ', "100110101101");
			this.C39_Code.Add('$', "100100100101");
			this.C39_Code.Add('/', "100100101001");
			this.C39_Code.Add('+', "100101001001");
			this.C39_Code.Add('%', "101001001001");
			this.C39_Code.Add('*', "100101101101");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00007358 File Offset: 0x00005558
		private void init_ExtendedCode39()
		{
			this.ExtC39_Translation.Clear();
			this.ExtC39_Translation.Add(Convert.ToChar(0).ToString(), "%U");
			this.ExtC39_Translation.Add(Convert.ToChar(1).ToString(), "$A");
			this.ExtC39_Translation.Add(Convert.ToChar(2).ToString(), "$B");
			this.ExtC39_Translation.Add(Convert.ToChar(3).ToString(), "$C");
			this.ExtC39_Translation.Add(Convert.ToChar(4).ToString(), "$D");
			this.ExtC39_Translation.Add(Convert.ToChar(5).ToString(), "$E");
			this.ExtC39_Translation.Add(Convert.ToChar(6).ToString(), "$F");
			this.ExtC39_Translation.Add(Convert.ToChar(7).ToString(), "$G");
			this.ExtC39_Translation.Add(Convert.ToChar(8).ToString(), "$H");
			this.ExtC39_Translation.Add(Convert.ToChar(9).ToString(), "$I");
			this.ExtC39_Translation.Add(Convert.ToChar(10).ToString(), "$J");
			this.ExtC39_Translation.Add(Convert.ToChar(11).ToString(), "$K");
			this.ExtC39_Translation.Add(Convert.ToChar(12).ToString(), "$L");
			this.ExtC39_Translation.Add(Convert.ToChar(13).ToString(), "$M");
			this.ExtC39_Translation.Add(Convert.ToChar(14).ToString(), "$N");
			this.ExtC39_Translation.Add(Convert.ToChar(15).ToString(), "$O");
			this.ExtC39_Translation.Add(Convert.ToChar(16).ToString(), "$P");
			this.ExtC39_Translation.Add(Convert.ToChar(17).ToString(), "$Q");
			this.ExtC39_Translation.Add(Convert.ToChar(18).ToString(), "$R");
			this.ExtC39_Translation.Add(Convert.ToChar(19).ToString(), "$S");
			this.ExtC39_Translation.Add(Convert.ToChar(20).ToString(), "$T");
			this.ExtC39_Translation.Add(Convert.ToChar(21).ToString(), "$U");
			this.ExtC39_Translation.Add(Convert.ToChar(22).ToString(), "$V");
			this.ExtC39_Translation.Add(Convert.ToChar(23).ToString(), "$W");
			this.ExtC39_Translation.Add(Convert.ToChar(24).ToString(), "$X");
			this.ExtC39_Translation.Add(Convert.ToChar(25).ToString(), "$Y");
			this.ExtC39_Translation.Add(Convert.ToChar(26).ToString(), "$Z");
			this.ExtC39_Translation.Add(Convert.ToChar(27).ToString(), "%A");
			this.ExtC39_Translation.Add(Convert.ToChar(28).ToString(), "%B");
			this.ExtC39_Translation.Add(Convert.ToChar(29).ToString(), "%C");
			this.ExtC39_Translation.Add(Convert.ToChar(30).ToString(), "%D");
			this.ExtC39_Translation.Add(Convert.ToChar(31).ToString(), "%E");
			this.ExtC39_Translation.Add("!", "/A");
			this.ExtC39_Translation.Add("\"", "/B");
			this.ExtC39_Translation.Add("#", "/C");
			this.ExtC39_Translation.Add("$", "/D");
			this.ExtC39_Translation.Add("%", "/E");
			this.ExtC39_Translation.Add("&", "/F");
			this.ExtC39_Translation.Add("'", "/G");
			this.ExtC39_Translation.Add("(", "/H");
			this.ExtC39_Translation.Add(")", "/I");
			this.ExtC39_Translation.Add("*", "/J");
			this.ExtC39_Translation.Add("+", "/K");
			this.ExtC39_Translation.Add(",", "/L");
			this.ExtC39_Translation.Add("/", "/O");
			this.ExtC39_Translation.Add(":", "/Z");
			this.ExtC39_Translation.Add(";", "%F");
			this.ExtC39_Translation.Add("<", "%G");
			this.ExtC39_Translation.Add("=", "%H");
			this.ExtC39_Translation.Add(">", "%I");
			this.ExtC39_Translation.Add("?", "%J");
			this.ExtC39_Translation.Add("[", "%K");
			this.ExtC39_Translation.Add("\\", "%L");
			this.ExtC39_Translation.Add("]", "%M");
			this.ExtC39_Translation.Add("^", "%N");
			this.ExtC39_Translation.Add("_", "%O");
			this.ExtC39_Translation.Add("{", "%P");
			this.ExtC39_Translation.Add("|", "%Q");
			this.ExtC39_Translation.Add("}", "%R");
			this.ExtC39_Translation.Add("~", "%S");
			this.ExtC39_Translation.Add("`", "%W");
			this.ExtC39_Translation.Add("@", "%V");
			this.ExtC39_Translation.Add("a", "+A");
			this.ExtC39_Translation.Add("b", "+B");
			this.ExtC39_Translation.Add("c", "+C");
			this.ExtC39_Translation.Add("d", "+D");
			this.ExtC39_Translation.Add("e", "+E");
			this.ExtC39_Translation.Add("f", "+F");
			this.ExtC39_Translation.Add("g", "+G");
			this.ExtC39_Translation.Add("h", "+H");
			this.ExtC39_Translation.Add("i", "+I");
			this.ExtC39_Translation.Add("j", "+J");
			this.ExtC39_Translation.Add("k", "+K");
			this.ExtC39_Translation.Add("l", "+L");
			this.ExtC39_Translation.Add("m", "+M");
			this.ExtC39_Translation.Add("n", "+N");
			this.ExtC39_Translation.Add("o", "+O");
			this.ExtC39_Translation.Add("p", "+P");
			this.ExtC39_Translation.Add("q", "+Q");
			this.ExtC39_Translation.Add("r", "+R");
			this.ExtC39_Translation.Add("s", "+S");
			this.ExtC39_Translation.Add("t", "+T");
			this.ExtC39_Translation.Add("u", "+U");
			this.ExtC39_Translation.Add("v", "+V");
			this.ExtC39_Translation.Add("w", "+W");
			this.ExtC39_Translation.Add("x", "+X");
			this.ExtC39_Translation.Add("y", "+Y");
			this.ExtC39_Translation.Add("z", "+Z");
			this.ExtC39_Translation.Add(Convert.ToChar(127).ToString(), "%T");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00007C00 File Offset: 0x00005E00
		private void InsertExtendedCharsIfNeeded(ref string FormattedData)
		{
			string text = "";
			foreach (char c in FormattedData)
			{
				try
				{
					this.C39_Code[c].ToString();
					text += c.ToString();
				}
				catch
				{
					object obj = this.ExtC39_Translation[c.ToString()];
					text += obj.ToString();
				}
			}
			FormattedData = text;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00007C90 File Offset: 0x00005E90
		private char getChecksumChar(string strNoAstr)
		{
			string text = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";
			this.InsertExtendedCharsIfNeeded(ref strNoAstr);
			int num = 0;
			for (int i = 0; i < strNoAstr.Length; i++)
			{
				num += text.IndexOf(strNoAstr[i].ToString());
			}
			return text[num % 43];
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Code39();
			}
		}

		// Token: 0x04000059 RID: 89
		private Hashtable C39_Code = new Hashtable();

		// Token: 0x0400005A RID: 90
		private Hashtable ExtC39_Translation = new Hashtable();

		// Token: 0x0400005B RID: 91
		private bool _AllowExtended;

		// Token: 0x0400005C RID: 92
		private bool _EnableChecksum;
	}
}
