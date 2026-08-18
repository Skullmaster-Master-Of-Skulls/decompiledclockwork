using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000010 RID: 16
	internal class Pharmacode : BarcodeCommon, IBarcode
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000088FC File Offset: 0x00006AFC
		public Pharmacode(string input)
		{
			this.Raw_Data = input;
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EPHARM-1: Data contains invalid  characters (non-numeric).");
				return;
			}
			if (this.Raw_Data.Length > 6)
			{
				base.Error("EPHARM-2: Data too long (invalid data input length).");
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000896C File Offset: 0x00006B6C
		private string Encode_Pharmacode()
		{
			int num;
			if (!int.TryParse(this.Raw_Data, out num))
			{
				base.Error("EPHARM-3: Input is unparseable.");
			}
			else if (num < 3 || num > 131070)
			{
				base.Error("EPHARM-4: Data contains invalid  characters (invalid numeric range).");
			}
			int num2 = 0;
			for (int i = 15; i >= 0; i--)
			{
				if (Math.Pow(2.0, (double)i) < (double)(num / 2))
				{
					num2 = i;
					break;
				}
			}
			double num3 = Math.Pow(2.0, (double)(num2 + 1)) - 2.0;
			string[] array = new string[num2 + 1];
			int num4 = 0;
			for (int j = num2; j >= 0; j--)
			{
				double num5 = Math.Pow(2.0, (double)j);
				if ((double)num - num3 > num5)
				{
					array[num4++] = this._thickBar;
					num3 += num5;
				}
				else
				{
					array[num4++] = this._thinBar;
				}
			}
			string text = string.Empty;
			foreach (string str in array)
			{
				if (text != string.Empty)
				{
					text += this._gap;
				}
				text += str;
			}
			return text;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00008AA2 File Offset: 0x00006CA2
		public string Encoded_Value
		{
			get
			{
				return this.Encode_Pharmacode();
			}
		}

		// Token: 0x0400005E RID: 94
		private string _thinBar = "1";

		// Token: 0x0400005F RID: 95
		private string _gap = "00";

		// Token: 0x04000060 RID: 96
		private string _thickBar = "111";
	}
}
