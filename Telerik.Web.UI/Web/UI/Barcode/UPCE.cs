using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009DA RID: 2522
	internal class UPCE : Product1D
	{
		// Token: 0x0600608B RID: 24715 RVA: 0x0012B18C File Offset: 0x0012938C
		[Description("Initializes a new instance of UPCE type.")]
		public UPCE()
		{
			this.parity = new Dictionary<string, string>();
			this.parity.Add("00", "000111");
			this.parity.Add("01", "001011");
			this.parity.Add("02", "001101");
			this.parity.Add("03", "001110");
			this.parity.Add("04", "010011");
			this.parity.Add("05", "011001");
			this.parity.Add("06", "011100");
			this.parity.Add("07", "010101");
			this.parity.Add("08", "010110");
			this.parity.Add("09", "011010");
			this.parity.Add("10", "111000");
			this.parity.Add("11", "110100");
			this.parity.Add("12", "110010");
			this.parity.Add("13", "110001");
			this.parity.Add("14", "101100");
			this.parity.Add("15", "100110");
			this.parity.Add("16", "100011");
			this.parity.Add("17", "101010");
			this.parity.Add("18", "101001");
			this.parity.Add("19", "100101");
			this.encoding = new Dictionary<string, string>();
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

		// Token: 0x0600608C RID: 24716 RVA: 0x0012B500 File Offset: 0x00129700
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			this.SetTextboxValues(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = value.Substring(1, 6);
			string key = value.Substring(0, 1) + value.Substring(value.Length - 1, 1);
			string text2 = this.parity[key];
			for (int i = 0; i < text.Length; i++)
			{
				string key2 = text2[i].ToString() + text[i].ToString();
				stringBuilder.Append(this.encoding[key2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x0012B5BC File Offset: 0x001297BC
		protected string ValidateValueUPCE(string value)
		{
			int num = 12;
			StringBuilder stringBuilder = new StringBuilder(value);
			if (stringBuilder.Length >= num)
			{
				stringBuilder = new StringBuilder(stringBuilder.ToString().Substring(0, num - 1));
			}
			else
			{
				stringBuilder = new StringBuilder(this.GetSymbols(value));
			}
			stringBuilder = new StringBuilder(base.GetSymbols(stringBuilder.ToString(), 12));
			if (!stringBuilder.ToString().StartsWith("0") && !stringBuilder.ToString().StartsWith("1"))
			{
				stringBuilder[0] = '0';
			}
			string text = stringBuilder.ToString().Substring(1, 5);
			string text2 = stringBuilder.ToString().Substring(6, 5);
			if (text.EndsWith("000") || text.EndsWith("100") || text.EndsWith("200"))
			{
				if (!text2.StartsWith("00"))
				{
					stringBuilder[6] = '0';
					stringBuilder[7] = '0';
				}
			}
			else if (text.EndsWith("00"))
			{
				if (!text2.StartsWith("000"))
				{
					stringBuilder[6] = '0';
					stringBuilder[7] = '0';
					stringBuilder[8] = '0';
				}
			}
			else if (text.EndsWith("0"))
			{
				if (!text2.StartsWith("0000"))
				{
					stringBuilder[6] = '0';
					stringBuilder[7] = '0';
					stringBuilder[8] = '0';
					stringBuilder[9] = '0';
				}
			}
			else if (text2.CompareTo("00005") < 0 || text2.CompareTo("00009") > 0)
			{
				stringBuilder[6] = '0';
				stringBuilder[7] = '0';
				stringBuilder[8] = '0';
				stringBuilder[9] = '0';
				stringBuilder[10] = '5';
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0012B77C File Offset: 0x0012997C
		protected string GetSymbols(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return "00000000";
			}
			if (value.Length < 12)
			{
				value = base.GetSymbols(value, 12);
			}
			string text = value.Substring(1, 5);
			value.Substring(6, 5);
			if (text.EndsWith("000") || text.EndsWith("100") || text.EndsWith("200"))
			{
				return string.Format("{0}{1}{2}{3}", new object[]
				{
					value.Substring(0, 3),
					value.Substring(8, 3),
					value.Substring(3, 1),
					value.Substring(11, 1)
				});
			}
			if (text.EndsWith("00"))
			{
				return string.Format("{0}{1}3{2}", value.Substring(0, 4), value.Substring(9, 2), value.Substring(11, 1));
			}
			if (text.EndsWith("0"))
			{
				return string.Format("{0}{1}4{2}", value.Substring(0, 5), value.Substring(10, 1), value.Substring(11, 1));
			}
			return string.Format("{0}{1}", value.Substring(0, 6), value.Substring(10, 2));
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x0012B8A4 File Offset: 0x00129AA4
		protected void SetTextboxValues(string value)
		{
			base.LeadingTextboxText = this.GetHeadText(value);
			base.LeftTextboxText = this.GetLeftText(value);
			base.EndTextboxText = this.GetTailText(value);
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x0012B8CD File Offset: 0x00129ACD
		protected string GetHeadText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(0, 1);
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x0012B8E5 File Offset: 0x00129AE5
		protected string GetTailText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(7, 1);
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x0012B8FD File Offset: 0x00129AFD
		protected string GetLeftText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return value.Substring(1, 6);
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x0012B918 File Offset: 0x00129B18
		protected string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (char.IsDigit(c))
				{
					stringBuilder.Append(c);
				}
			}
			value = stringBuilder.ToString();
			if (value.Length == 12 || value.Length == 11)
			{
				value = this.ValidateValueUPCE(value);
				value = this.GetSymbols(value);
				return value;
			}
			if (value.Length == 6)
			{
				string value2;
				if (value[5] == '0' || value[5] == '1' || value[5] == '2')
				{
					value2 = string.Concat(new object[]
					{
						"0",
						value[0],
						value[1],
						value[5],
						"0000",
						value[2],
						value[3],
						value[4]
					});
				}
				else if (value[5] == '3')
				{
					value2 = string.Concat(new object[]
					{
						"0",
						value[0],
						value[1],
						value[2],
						"00000",
						value[3],
						value[4]
					});
				}
				else if (value[5] == '4')
				{
					value2 = string.Concat(new object[]
					{
						"0",
						value[0],
						value[1],
						value[2],
						value[3],
						"00000",
						value[4]
					});
				}
				else
				{
					value2 = string.Concat(new object[]
					{
						"0",
						value[0],
						value[1],
						value[2],
						value[3],
						value[4],
						"0000",
						value[5]
					});
				}
				return "0" + value + base.GetChecksum(value2);
			}
			if (value.Length == 7)
			{
				if (value[0] == '0' || value[0] == '1')
				{
					value = value.Substring(0, 7) + base.GetChecksum(value);
				}
				else
				{
					value = "0" + value.Substring(1, 6) + base.GetChecksum("0" + value.Substring(1, 6));
				}
				return value;
			}
			if (value.Length != 8)
			{
				return this.GetSymbols(value);
			}
			value = value.Substring(0, value.Length - 1) + base.GetChecksum(value);
			if (value[0] != '0' || value[0] == '1')
			{
				return new StringBuilder(value[0].ToString() + value.Substring(1)).ToString();
			}
			return value;
		}

		// Token: 0x0400177B RID: 6011
		public static readonly string Prefix = "101";

		// Token: 0x0400177C RID: 6012
		public static readonly string Suffix = "010101";

		// Token: 0x0400177D RID: 6013
		private Dictionary<string, string> parity;

		// Token: 0x0400177E RID: 6014
		private Dictionary<string, string> encoding;
	}
}
