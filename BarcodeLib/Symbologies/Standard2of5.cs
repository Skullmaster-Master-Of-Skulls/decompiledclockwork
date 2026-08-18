using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001B RID: 27
	internal class Standard2of5 : BarcodeCommon, IBarcode
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public Standard2of5(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000DC88 File Offset: 0x0000BE88
		private string Encode_Standard2of5()
		{
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("ES25-1: Numeric Data Only");
			}
			string str = "11011010";
			foreach (char c in this.Raw_Data)
			{
				str += this.S25_Code[int.Parse(c.ToString())];
			}
			return str + "1101011";
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Standard2of5();
			}
		}

		// Token: 0x04000074 RID: 116
		private string[] S25_Code = new string[]
		{
			"11101010101110",
			"10111010101110",
			"11101110101010",
			"10101110101110",
			"11101011101010",
			"10111011101010",
			"10101011101110",
			"10101110111010",
			"11101010111010",
			"10111010111010"
		};
	}
}
