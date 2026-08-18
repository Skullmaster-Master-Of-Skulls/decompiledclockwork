using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D8 RID: 2520
	internal class EAN8 : Product1D
	{
		// Token: 0x0600607D RID: 24701 RVA: 0x0012AAC4 File Offset: 0x00128CC4
		[Description("Initializes a new instance of EAN8 type.")]
		public EAN8()
		{
			this.encoding = new Dictionary<string, string>();
			this.encoding.Add("00", "0001101");
			this.encoding.Add("01", "0011001");
			this.encoding.Add("02", "0010011");
			this.encoding.Add("03", "0111101");
			this.encoding.Add("04", "0100011");
			this.encoding.Add("05", "0110001");
			this.encoding.Add("06", "0101111");
			this.encoding.Add("07", "0111011");
			this.encoding.Add("08", "0110111");
			this.encoding.Add("09", "0001011");
			this.encoding.Add("10", "1110010");
			this.encoding.Add("11", "1100110");
			this.encoding.Add("12", "1101100");
			this.encoding.Add("13", "1000010");
			this.encoding.Add("14", "1011100");
			this.encoding.Add("15", "1001110");
			this.encoding.Add("16", "1010000");
			this.encoding.Add("17", "1000100");
			this.encoding.Add("18", "1001000");
			this.encoding.Add("19", "1110100");
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x0012AC88 File Offset: 0x00128E88
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			this.SetTextboxValues(value);
			StringBuilder stringBuilder = new StringBuilder();
			string text = value.Substring(0, 4);
			for (int i = 0; i < text.Length; i++)
			{
				string key = EAN8.Left + text[i];
				stringBuilder.Append(this.encoding[key]);
			}
			string text2 = value.Substring(4, 4);
			for (int j = 0; j < text2.Length; j++)
			{
				string key2 = EAN8.Right + text2[j];
				stringBuilder.Append(this.encoding[key2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x0012AD44 File Offset: 0x00128F44
		private string ValidateValue(string value)
		{
			int num = 8;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			if (value.Length >= num)
			{
				return new StringBuilder(base.GetSymbols(stringBuilder.ToString().Substring(0, num - 1), num)).ToString();
			}
			if (value.Length < num)
			{
				stringBuilder = new StringBuilder(base.GetSymbols(stringBuilder.ToString(), num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x0012ADCE File Offset: 0x00128FCE
		private string GetSymbols(string value)
		{
			return base.GetSymbols(value, 8);
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x0012ADD8 File Offset: 0x00128FD8
		private string GetLeftText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(0, 4);
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x0012ADF0 File Offset: 0x00128FF0
		private string GetRightText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(4, 4);
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x0012AE08 File Offset: 0x00129008
		private void SetTextboxValues(string value)
		{
			base.LeftTextboxText = this.GetLeftText(value);
			base.RightTextboxText = this.GetRightText(value);
		}

		// Token: 0x04001773 RID: 6003
		public static readonly string Left = "0";

		// Token: 0x04001774 RID: 6004
		public static readonly string Right = "1";

		// Token: 0x04001775 RID: 6005
		public static readonly string Prefix = "101";

		// Token: 0x04001776 RID: 6006
		public static readonly string Suffix = "101";

		// Token: 0x04001777 RID: 6007
		public static readonly string Center = "01010";

		// Token: 0x04001778 RID: 6008
		private Dictionary<string, string> encoding;
	}
}
