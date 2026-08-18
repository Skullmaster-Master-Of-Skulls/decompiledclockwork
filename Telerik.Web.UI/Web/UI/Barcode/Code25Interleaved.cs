using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CF RID: 2511
	internal class Code25Interleaved : Code25
	{
		// Token: 0x06006043 RID: 24643 RVA: 0x00127160 File Offset: 0x00125360
		public Code25Interleaved()
		{
			this.encoding = new Dictionary<string, string>();
			this.encoding.Add("00", "10101100110010");
			this.encoding.Add("01", "10010110110100");
			this.encoding.Add("02", "10100110110100");
			this.encoding.Add("03", "10010011011010");
			this.encoding.Add("04", "10101100110100");
			this.encoding.Add("05", "10010110011010");
			this.encoding.Add("06", "10100110011010");
			this.encoding.Add("07", "10101101100100");
			this.encoding.Add("08", "10010110110010");
			this.encoding.Add("09", "10100110110010");
			this.encoding.Add("10", "11010100100110");
			this.encoding.Add("11", "11001010101100");
			this.encoding.Add("12", "11010010101100");
			this.encoding.Add("13", "11001001010110");
			this.encoding.Add("14", "11010100101100");
			this.encoding.Add("15", "11001010010110");
			this.encoding.Add("16", "11010010010110");
			this.encoding.Add("17", "11010101001100");
			this.encoding.Add("18", "11001010100110");
			this.encoding.Add("19", "11010010100110");
			this.encoding.Add("20", "10110100100110");
			this.encoding.Add("21", "10011010101100");
			this.encoding.Add("22", "10110010101100");
			this.encoding.Add("23", "10011001010110");
			this.encoding.Add("24", "10110100101100");
			this.encoding.Add("25", "10011010010110");
			this.encoding.Add("26", "10110010010110");
			this.encoding.Add("27", "10110101001100");
			this.encoding.Add("28", "10011010100110");
			this.encoding.Add("29", "10110010100110");
			this.encoding.Add("30", "11011010010010");
			this.encoding.Add("31", "11001101010100");
			this.encoding.Add("32", "11011001010100");
			this.encoding.Add("33", "11001100101010");
			this.encoding.Add("34", "11011010010100");
			this.encoding.Add("35", "11001101001010");
			this.encoding.Add("36", "11011001001010");
			this.encoding.Add("37", "11011010100100");
			this.encoding.Add("38", "11001101010010");
			this.encoding.Add("39", "11011001010010");
			this.encoding.Add("40", "10101100100110");
			this.encoding.Add("41", "10010110101100");
			this.encoding.Add("42", "10100110101100");
			this.encoding.Add("43", "10010011010110");
			this.encoding.Add("44", "10101100101100");
			this.encoding.Add("45", "10010110010110");
			this.encoding.Add("46", "10100110010110");
			this.encoding.Add("47", "10101101001100");
			this.encoding.Add("48", "10010110100110");
			this.encoding.Add("49", "10100110100110");
			this.encoding.Add("50", "11010110010010");
			this.encoding.Add("51", "11001011010100");
			this.encoding.Add("52", "11010011010100");
			this.encoding.Add("53", "11001001101010");
			this.encoding.Add("54", "11010110010100");
			this.encoding.Add("55", "11001011001010");
			this.encoding.Add("56", "11010011001010");
			this.encoding.Add("57", "11010110100100");
			this.encoding.Add("58", "11001011010010");
			this.encoding.Add("59", "11010011010010");
			this.encoding.Add("60", "10110110010010");
			this.encoding.Add("61", "10011011010100");
			this.encoding.Add("62", "10110011010100");
			this.encoding.Add("63", "10011001101010");
			this.encoding.Add("64", "10110110010100");
			this.encoding.Add("65", "10011011001010");
			this.encoding.Add("66", "10110011001010");
			this.encoding.Add("67", "10110110100100");
			this.encoding.Add("68", "10011011010010");
			this.encoding.Add("69", "10110011010010");
			this.encoding.Add("70", "10101001100110");
			this.encoding.Add("71", "10010101101100");
			this.encoding.Add("72", "10100101101100");
			this.encoding.Add("73", "10010010110110");
			this.encoding.Add("74", "10101001101100");
			this.encoding.Add("75", "10010100110110");
			this.encoding.Add("76", "10100100110110");
			this.encoding.Add("77", "10101011001100");
			this.encoding.Add("78", "10010101100110");
			this.encoding.Add("79", "10100101100110");
			this.encoding.Add("80", "11010100110010");
			this.encoding.Add("81", "11001010110100");
			this.encoding.Add("82", "11010010110100");
			this.encoding.Add("83", "11001001011010");
			this.encoding.Add("84", "11010100110100");
			this.encoding.Add("85", "11001010011010");
			this.encoding.Add("86", "11010010011010");
			this.encoding.Add("87", "11010101100100");
			this.encoding.Add("88", "11001010110010");
			this.encoding.Add("89", "11010010110010");
			this.encoding.Add("90", "10110100110010");
			this.encoding.Add("91", "10011010110100");
			this.encoding.Add("92", "10110010110100");
			this.encoding.Add("93", "10011001011010");
			this.encoding.Add("94", "10110100110100");
			this.encoding.Add("95", "10011010011010");
			this.encoding.Add("96", "10110010011010");
			this.encoding.Add("97", "10110101100100");
			this.encoding.Add("98", "10011010110010");
			this.encoding.Add("99", "10110010110010");
			this.encoding.Add("[", "1010");
			this.encoding.Add("]", "1101");
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x00127A00 File Offset: 0x00125C00
		internal override string GetEncoding(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			value = base.ValidateValue(value);
			base.CheckSum = base.GetChecksum(value).ToString();
			if (base.CalculateCheckSum)
			{
				value += base.CheckSum;
			}
			if (value.Length % 2 != 0)
			{
				value = this.padding + value;
			}
			if (!value.StartsWith(this.prefix))
			{
				value = this.prefix + value;
			}
			if (!value.EndsWith(this.suffix))
			{
				value += this.suffix;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (int i = 0; i < value.Length; i += num)
			{
				if (char.IsDigit(value[i]))
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
				string key = value.Substring(i, num);
				stringBuilder.Append(this.encoding[key]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001750 RID: 5968
		private string prefix = "[";

		// Token: 0x04001751 RID: 5969
		private string suffix = "]";

		// Token: 0x04001752 RID: 5970
		private string padding = "0";

		// Token: 0x04001753 RID: 5971
		private Dictionary<string, string> encoding;
	}
}
