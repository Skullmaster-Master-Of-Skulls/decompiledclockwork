using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000016 RID: 22
	internal class Interleaved2of5 : BarcodeCommon, IBarcode
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000D360 File Offset: 0x0000B560
		public Interleaved2of5(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		private string Encode_Interleaved2of5()
		{
			if (this.Raw_Data.Length % 2 != 0)
			{
				base.Error("EI25-1: Data length invalid.");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EI25-2: Numeric Data Only");
			}
			string str = "1010";
			for (int i = 0; i < this.Raw_Data.Length; i += 2)
			{
				bool flag = true;
				string text = this.I25_Code[int.Parse(this.Raw_Data[i].ToString())];
				string text2 = this.I25_Code[int.Parse(this.Raw_Data[i + 1].ToString())];
				string text3 = "";
				while (text.Length > 0)
				{
					text3 = text3 + text[0].ToString() + text2[0].ToString();
					text = text.Substring(1);
					text2 = text2.Substring(1);
				}
				foreach (char c in text3)
				{
					if (flag)
					{
						if (c == 'N')
						{
							str += "1";
						}
						else
						{
							str += "11";
						}
					}
					else if (c == 'N')
					{
						str += "0";
					}
					else
					{
						str += "00";
					}
					flag = !flag;
				}
			}
			return str + "1101";
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000D54C File Offset: 0x0000B74C
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Interleaved2of5();
			}
		}

		// Token: 0x04000070 RID: 112
		private string[] I25_Code = new string[]
		{
			"NNWWN",
			"WNNNW",
			"NWNNW",
			"WWNNN",
			"NNWNW",
			"WNWNN",
			"NWWNN",
			"NNNWW",
			"WNNWN",
			"NWNWN"
		};
	}
}
