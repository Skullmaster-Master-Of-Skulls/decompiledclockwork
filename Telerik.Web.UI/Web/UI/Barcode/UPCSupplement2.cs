using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DB RID: 2523
	internal class UPCSupplement2 : Symbology1D
	{
		// Token: 0x06006095 RID: 24725 RVA: 0x0012BCD8 File Offset: 0x00129ED8
		public UPCSupplement2()
		{
			this.parity = new List<string>();
			this.parity.Add("11");
			this.parity.Add("10");
			this.parity.Add("01");
			this.parity.Add("00");
			this.encoding = new Dictionary<string, string>();
			this.encoding.Add(string.Empty, "1011");
			this.encoding.Add("00", "0100111");
			this.encoding.Add("01", "0110011");
			this.encoding.Add("02", "0011011");
			this.encoding.Add("03", "0100001");
			this.encoding.Add("04", "0011101");
			this.encoding.Add("05", "0111001");
			this.encoding.Add("06", "0000101");
			this.encoding.Add("07", "0010001");
			this.encoding.Add("08", "0001001");
			this.encoding.Add("09", "0010111");
			this.encoding.Add("10", "0001101");
			this.encoding.Add("11", "0011001");
			this.encoding.Add("12", "0010011");
			this.encoding.Add("13", "0111101");
			this.encoding.Add("14", "0100011");
			this.encoding.Add("15", "0110001");
			this.encoding.Add("16", "0101111");
			this.encoding.Add("17", "0111011");
			this.encoding.Add("18", "0110111");
			this.encoding.Add("19", "0001011");
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x0012BF04 File Offset: 0x0012A104
		public string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			if (value.Length > 2)
			{
				stringBuilder = new StringBuilder(stringBuilder.ToString().Substring(0, 2));
			}
			else if (stringBuilder.Length < 2)
			{
				stringBuilder = new StringBuilder(this.GetSymbols(stringBuilder.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x0012BF80 File Offset: 0x0012A180
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			int num = Convert.ToInt32(value);
			num %= this.parity.Count;
			string text = this.parity[num];
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.encoding[string.Empty]);
			for (int i = 0; i < value.Length; i++)
			{
				string key = text[i].ToString() + value[i].ToString();
				stringBuilder.Append(this.encoding[key]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x0012C035 File Offset: 0x0012A235
		private string GetSymbols(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.PadLeft(2, this.padding);
		}

		// Token: 0x0400177F RID: 6015
		private readonly char padding = '0';

		// Token: 0x04001780 RID: 6016
		private readonly List<string> parity;

		// Token: 0x04001781 RID: 6017
		private readonly Dictionary<string, string> encoding;
	}
}
