using System;
using System.Collections;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200000B RID: 11
	internal class Codabar : BarcodeCommon, IBarcode
	{
		// Token: 0x06000060 RID: 96 RVA: 0x0000406F File Offset: 0x0000226F
		public Codabar(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000408C File Offset: 0x0000228C
		private string Encode_Codabar()
		{
			if (this.Raw_Data.Length < 2)
			{
				base.Error("ECODABAR-1: Data format invalid. (Invalid length)");
			}
			string text = this.Raw_Data[0].ToString().ToUpper().Trim();
			if (!(text == "A") && !(text == "B") && !(text == "C") && !(text == "D"))
			{
				base.Error("ECODABAR-2: Data format invalid. (Invalid START character)");
			}
			text = this.Raw_Data[this.Raw_Data.Trim().Length - 1].ToString().ToUpper().Trim();
			if (!(text == "A") && !(text == "B") && !(text == "C") && !(text == "D"))
			{
				base.Error("ECODABAR-3: Data format invalid. (Invalid STOP character)");
			}
			this.init_Codabar();
			string text2 = this.Raw_Data;
			foreach (object obj in this.Codabar_Code.Keys)
			{
				char oldChar = (char)obj;
				if (!BarcodeCommon.CheckNumericOnly(oldChar.ToString()))
				{
					text2 = text2.Replace(oldChar, '1');
				}
			}
			if (!BarcodeCommon.CheckNumericOnly(text2))
			{
				base.Error("ECODABAR-4: Data contains invalid  characters.");
			}
			string text3 = "";
			foreach (char c in this.Raw_Data)
			{
				text3 += this.Codabar_Code[c].ToString();
				text3 += "0";
			}
			text3 = text3.Remove(text3.Length - 1);
			this.Codabar_Code.Clear();
			this.Raw_Data = this.Raw_Data.Trim().Substring(1, base.RawData.Trim().Length - 2);
			return text3;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000042A4 File Offset: 0x000024A4
		private void init_Codabar()
		{
			this.Codabar_Code.Clear();
			this.Codabar_Code.Add('0', "101010011");
			this.Codabar_Code.Add('1', "101011001");
			this.Codabar_Code.Add('2', "101001011");
			this.Codabar_Code.Add('3', "110010101");
			this.Codabar_Code.Add('4', "101101001");
			this.Codabar_Code.Add('5', "110101001");
			this.Codabar_Code.Add('6', "100101011");
			this.Codabar_Code.Add('7', "100101101");
			this.Codabar_Code.Add('8', "100110101");
			this.Codabar_Code.Add('9', "110100101");
			this.Codabar_Code.Add('-', "101001101");
			this.Codabar_Code.Add('$', "101100101");
			this.Codabar_Code.Add(':', "1101011011");
			this.Codabar_Code.Add('/', "1101101011");
			this.Codabar_Code.Add('.', "1101101101");
			this.Codabar_Code.Add('+', "101100110011");
			this.Codabar_Code.Add('A', "1011001001");
			this.Codabar_Code.Add('B', "1010010011");
			this.Codabar_Code.Add('C', "1001001011");
			this.Codabar_Code.Add('D', "1010011001");
			this.Codabar_Code.Add('a', "1011001001");
			this.Codabar_Code.Add('b', "1010010011");
			this.Codabar_Code.Add('c', "1001001011");
			this.Codabar_Code.Add('d', "1010011001");
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000044E4 File Offset: 0x000026E4
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Codabar();
			}
		}

		// Token: 0x04000052 RID: 82
		private Hashtable Codabar_Code = new Hashtable();
	}
}
