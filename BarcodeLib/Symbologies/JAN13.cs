using System;

namespace BarcodeLib.Symbologies
{
	// Token: 0x02000018 RID: 24
	internal class JAN13 : BarcodeCommon, IBarcode
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000D554 File Offset: 0x0000B754
		public JAN13(string input)
		{
			this.Raw_Data = input;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000D684 File Offset: 0x0000B884
		private string Encode_JAN13()
		{
			if (!this.Raw_Data.StartsWith("49"))
			{
				base.Error("EJAN13-1: Invalid Country Code for JAN13 (49 required)");
			}
			if (!BarcodeCommon.CheckNumericOnly(this.Raw_Data))
			{
				base.Error("EJAN13-2: Numeric Data Only");
			}
			return new EAN13(this.Raw_Data).Encoded_Value;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000D6D6 File Offset: 0x0000B8D6
		public string Encoded_Value
		{
			get
			{
				return this.Encode_JAN13();
			}
		}
	}
}
