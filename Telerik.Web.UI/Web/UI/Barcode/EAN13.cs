using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009D7 RID: 2519
	internal class EAN13 : Product1D
	{
		// Token: 0x06006071 RID: 24689 RVA: 0x0012A534 File Offset: 0x00128734
		[Description("Initializes a new instance of EAN13 type.")]
		public EAN13()
		{
			this.Parity = new Dictionary<char, string>();
			this.Parity.Add('0', "111111");
			this.Parity.Add('1', "110100");
			this.Parity.Add('2', "110010");
			this.Parity.Add('3', "110001");
			this.Parity.Add('4', "101100");
			this.Parity.Add('5', "100110");
			this.Parity.Add('6', "100011");
			this.Parity.Add('7', "101010");
			this.Parity.Add('8', "101001");
			this.Parity.Add('9', "100101");
			this.Encoding = new Dictionary<string, string>();
			this.Encoding.Add("00", "0100111");
			this.Encoding.Add("01", "0110011");
			this.Encoding.Add("02", "0011011");
			this.Encoding.Add("03", "0100001");
			this.Encoding.Add("04", "0011101");
			this.Encoding.Add("05", "0111001");
			this.Encoding.Add("06", "0000101");
			this.Encoding.Add("07", "0010001");
			this.Encoding.Add("08", "0001001");
			this.Encoding.Add("09", "0010111");
			this.Encoding.Add("10", "0001101");
			this.Encoding.Add("11", "0011001");
			this.Encoding.Add("12", "0010011");
			this.Encoding.Add("13", "0111101");
			this.Encoding.Add("14", "0100011");
			this.Encoding.Add("15", "0110001");
			this.Encoding.Add("16", "0101111");
			this.Encoding.Add("17", "0111011");
			this.Encoding.Add("18", "0110111");
			this.Encoding.Add("19", "0001011");
			this.Encoding.Add("20", "1110010");
			this.Encoding.Add("21", "1100110");
			this.Encoding.Add("22", "1101100");
			this.Encoding.Add("23", "1000010");
			this.Encoding.Add("24", "1011100");
			this.Encoding.Add("25", "1001110");
			this.Encoding.Add("26", "1010000");
			this.Encoding.Add("27", "1000100");
			this.Encoding.Add("28", "1001000");
			this.Encoding.Add("29", "1110100");
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x0012A887 File Offset: 0x00128A87
		// (set) Token: 0x06006073 RID: 24691 RVA: 0x0012A88F File Offset: 0x00128A8F
		public Dictionary<char, string> Parity
		{
			get
			{
				return this.parity;
			}
			set
			{
				this.parity = value;
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x0012A898 File Offset: 0x00128A98
		// (set) Token: 0x06006075 RID: 24693 RVA: 0x0012A8A0 File Offset: 0x00128AA0
		public Dictionary<string, string> Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x0012A8A9 File Offset: 0x00128AA9
		public string GetHeadText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(0, 1);
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x0012A8C4 File Offset: 0x00128AC4
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			this.SetTextboxValues(value);
			StringBuilder stringBuilder = new StringBuilder();
			string text = this.parity[value[0]];
			string text2 = value.Substring(1, 6);
			for (int i = 0; i < text2.Length; i++)
			{
				string key = text[i].ToString() + text2[i].ToString();
				stringBuilder.Append(this.encoding[key]);
			}
			string text3 = value.Substring(7, 6);
			for (int j = 0; j < text3.Length; j++)
			{
				string key2 = EAN13.Right + text3[j];
				if (this.encoding.ContainsKey(key2))
				{
					stringBuilder.Append(this.encoding[key2]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x0012A9B4 File Offset: 0x00128BB4
		protected virtual string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 13;
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

		// Token: 0x06006079 RID: 24697 RVA: 0x0012AA3F File Offset: 0x00128C3F
		protected virtual void SetTextboxValues(string value)
		{
			base.LeadingTextboxText = this.GetHeadText(value);
			base.LeftTextboxText = this.GetLeftText(value);
			base.RightTextboxText = this.GetRightText(value);
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x0012AA68 File Offset: 0x00128C68
		protected virtual string GetLeftText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(1, 6);
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x0012AA80 File Offset: 0x00128C80
		protected virtual string GetRightText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(7, 6);
		}

		// Token: 0x0400176D RID: 5997
		public static readonly string Prefix = "101";

		// Token: 0x0400176E RID: 5998
		public static readonly string Suffix = "101";

		// Token: 0x0400176F RID: 5999
		public static readonly string Center = "01010";

		// Token: 0x04001770 RID: 6000
		public static readonly string Right = "2";

		// Token: 0x04001771 RID: 6001
		private Dictionary<char, string> parity;

		// Token: 0x04001772 RID: 6002
		private Dictionary<string, string> encoding;
	}
}
