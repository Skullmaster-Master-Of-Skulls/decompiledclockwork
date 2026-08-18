using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000011 RID: 17
	internal class FIM : BarcodeCommon, IBarcode
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00008AAC File Offset: 0x00006CAC
		public FIM(string input)
		{
			input = input.Trim();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(input);
			if (num <= 3339451269U)
			{
				if (num <= 3289118412U)
				{
					if (num != 3238785555U)
					{
						if (num != 3289118412U)
						{
							goto IL_173;
						}
						if (!(input == "A"))
						{
							goto IL_173;
						}
					}
					else
					{
						if (!(input == "D"))
						{
							goto IL_173;
						}
						goto IL_164;
					}
				}
				else if (num != 3322673650U)
				{
					if (num != 3339451269U)
					{
						goto IL_173;
					}
					if (!(input == "B"))
					{
						goto IL_173;
					}
					goto IL_146;
				}
				else
				{
					if (!(input == "C"))
					{
						goto IL_173;
					}
					goto IL_155;
				}
			}
			else if (num <= 3826002220U)
			{
				if (num != 3775669363U)
				{
					if (num != 3826002220U)
					{
						goto IL_173;
					}
					if (!(input == "a"))
					{
						goto IL_173;
					}
				}
				else
				{
					if (!(input == "d"))
					{
						goto IL_173;
					}
					goto IL_164;
				}
			}
			else if (num != 3859557458U)
			{
				if (num != 3876335077U)
				{
					goto IL_173;
				}
				if (!(input == "b"))
				{
					goto IL_173;
				}
				goto IL_146;
			}
			else
			{
				if (!(input == "c"))
				{
					goto IL_173;
				}
				goto IL_155;
			}
			this.Raw_Data = this.FIM_Codes[0];
			return;
			IL_146:
			this.Raw_Data = this.FIM_Codes[1];
			return;
			IL_155:
			this.Raw_Data = this.FIM_Codes[2];
			return;
			IL_164:
			this.Raw_Data = this.FIM_Codes[3];
			return;
			IL_173:
			base.Error("EFIM-1: Could not determine encoding type. (Only pass in A, B, C, or D)");
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00008C38 File Offset: 0x00006E38
		public string Encode_FIM()
		{
			string text = "";
			string rawData = base.RawData;
			for (int i = 0; i < rawData.Length; i++)
			{
				text = text + rawData[i].ToString() + "0";
			}
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00008C8F File Offset: 0x00006E8F
		public string Encoded_Value
		{
			get
			{
				return this.Encode_FIM();
			}
		}

		// Token: 0x04000061 RID: 97
		private string[] FIM_Codes = new string[]
		{
			"110010011",
			"101101101",
			"110101011",
			"111010111"
		};

		// Token: 0x02000027 RID: 39
		public enum FIMTypes
		{
			// Token: 0x040000A4 RID: 164
			FIM_A,
			// Token: 0x040000A5 RID: 165
			FIM_B,
			// Token: 0x040000A6 RID: 166
			FIM_C,
			// Token: 0x040000A7 RID: 167
			FIM_D
		}
	}
}
