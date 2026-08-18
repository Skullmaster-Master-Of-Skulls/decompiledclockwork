using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000017 RID: 23
	internal class ISBN : BarcodeCommon, IBarcode
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000D554 File Offset: 0x0000B754
		public ISBN(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000D564 File Offset: 0x0000B764
		private string Encode_ISBN_Bookland()
		{
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EBOOKLANDISBN-1: Numeric Data Only");
			}
			string a = "UNKNOWN";
			if (this.Raw_Data.Length == 10 || this.Raw_Data.Length == 9)
			{
				if (this.Raw_Data.Length == 10)
				{
					this.Raw_Data = this.Raw_Data.Remove(9, 1);
				}
				this.Raw_Data = "978" + this.Raw_Data;
				a = "ISBN";
			}
			else if (this.Raw_Data.Length == 12 && this.Raw_Data.StartsWith("978"))
			{
				a = "BOOKLAND-NOCHECKDIGIT";
			}
			else if (this.Raw_Data.Length == 13 && this.Raw_Data.StartsWith("978"))
			{
				a = "BOOKLAND-CHECKDIGIT";
				this.Raw_Data = this.Raw_Data.Remove(12, 1);
			}
			if (a == "UNKNOWN")
			{
				base.Error("EBOOKLANDISBN-2: Invalid input.  Must start with 978 and be length must be 9, 10, 12, 13 characters.");
			}
			return new EAN13(this.Raw_Data).Encoded_Value;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000D67A File Offset: 0x0000B87A
		public string Encoded_Value
		{
			get
			{
				return this.Encode_ISBN_Bookland();
			}
		}
	}
}
