using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x0200001A RID: 26
	internal class Postnet : BarcodeCommon, IBarcode
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public Postnet(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		private string Encode_Postnet()
		{
			this.Raw_Data = this.Raw_Data.Replace("-", "");
			int i = this.Raw_Data.Length;
			switch (i)
			{
			case 5:
			case 6:
			case 9:
			case 11:
				break;
			default:
				base.Error("EPOSTNET-2: Invalid data length. (5, 6, 9, or 11 digits only)");
				break;
			}
			string text = "1";
			int num = 0;
			foreach (char c in this.Raw_Data)
			{
				try
				{
					int num2 = Convert.ToInt32(c.ToString());
					text += this.POSTNET_Code[num2];
					num += num2;
				}
				catch (Exception ex)
				{
					base.Error("EPOSTNET-2: Invalid data. (Numeric only) --> " + ex.Message);
				}
			}
			int num3 = num % 10;
			int num4 = 10 - ((num3 == 0) ? 10 : num3);
			text += this.POSTNET_Code[num4];
			text += "1";
			return text;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000DC08 File Offset: 0x0000BE08
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Postnet();
			}
		}

		// Token: 0x04000073 RID: 115
		private string[] POSTNET_Code = new string[]
		{
			"11000",
			"00011",
			"00101",
			"00110",
			"01001",
			"01010",
			"01100",
			"10001",
			"10010",
			"10100"
		};
	}
}
