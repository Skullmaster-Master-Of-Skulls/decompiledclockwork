using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000013 RID: 19
	internal class ITF14 : BarcodeCommon, IBarcode
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public ITF14(string input)
		{
			this.Raw_Data = input;
			this.CheckDigit();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009D34 File Offset: 0x00007F34
		private string Encode_ITF14()
		{
			if (this.Raw_Data.Length > 14 || this.Raw_Data.Length < 13)
			{
				base.Error("EITF14-1: Data length invalid. (Length must be 13 or 14)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EITF14-2: Numeric data only.");
			}
			string str = "1010";
			for (int i = 0; i < this.Raw_Data.Length; i += 2)
			{
				bool flag = true;
				string text = this.ITF14_Code[int.Parse(this.Raw_Data[i].ToString())];
				string text2 = this.ITF14_Code[int.Parse(this.Raw_Data[i + 1].ToString())];
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

		// Token: 0x06000091 RID: 145 RVA: 0x00009EB8 File Offset: 0x000080B8
		private void CheckDigit()
		{
			if (this.Raw_Data.Length == 13)
			{
				int num = 0;
				for (int i = 0; i <= this.Raw_Data.Length - 1; i++)
				{
					int num2 = int.Parse(this.Raw_Data.Substring(i, 1));
					num += num2 * ((i == 0 || i % 2 == 0) ? 3 : 1);
				}
				int num3 = num % 10;
				num3 = 10 - num3;
				if (num3 == 10)
				{
					num3 = 0;
				}
				this.Raw_Data += num3.ToString();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00009F3B File Offset: 0x0000813B
		public string Encoded_Value
		{
			get
			{
				return this.Encode_ITF14();
			}
		}

		// Token: 0x04000067 RID: 103
		private string[] ITF14_Code = new string[]
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
