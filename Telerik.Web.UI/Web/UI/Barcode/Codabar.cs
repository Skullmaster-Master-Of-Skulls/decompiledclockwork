using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009C8 RID: 2504
	internal class Codabar : Symbology1D
	{
		// Token: 0x0600601C RID: 24604 RVA: 0x00124AAC File Offset: 0x00122CAC
		public Codabar()
		{
			this.prefixes = new List<char>();
			this.prefixes.Add('A');
			this.prefixes.Add('B');
			this.prefixes.Add('C');
			this.prefixes.Add('D');
			this.suffixes = new List<char>();
			this.suffixes.Add('A');
			this.suffixes.Add('B');
			this.suffixes.Add('C');
			this.suffixes.Add('D');
			this.encoding = new Dictionary<char, string>();
			this.encoding.Add('0', "101010011");
			this.encoding.Add('1', "101011001");
			this.encoding.Add('2', "101001011");
			this.encoding.Add('3', "110010101");
			this.encoding.Add('4', "101101001");
			this.encoding.Add('5', "110101001");
			this.encoding.Add('6', "100101011");
			this.encoding.Add('7', "100101101");
			this.encoding.Add('8', "100110101");
			this.encoding.Add('9', "110100101");
			this.encoding.Add('-', "101001101");
			this.encoding.Add('$', "101100101");
			this.encoding.Add(':', "1101011011");
			this.encoding.Add('/', "1101101011");
			this.encoding.Add('.', "1101101101");
			this.encoding.Add('+', "101100110011");
			this.encoding.Add('A', "1011001001");
			this.encoding.Add('B', "1010010011");
			this.encoding.Add('C', "1001001011");
			this.encoding.Add('D', "1010011001");
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x00124CB0 File Offset: 0x00122EB0
		internal override string GetEncoding(string value)
		{
			value = this.ValidateValue(value);
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			char item = value[0];
			if (!this.prefixes.Contains(item))
			{
				value = this.prefixes[0] + value;
			}
			char item2 = value[value.Length - 1];
			if (!this.suffixes.Contains(item2))
			{
				value += this.suffixes[0];
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Symbology1D.GapChar);
				}
				stringBuilder.Append(this.encoding[value[i]]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x00124D84 File Offset: 0x00122F84
		internal string ValidateValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (this.encoding.ContainsKey(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400173C RID: 5948
		private List<char> prefixes;

		// Token: 0x0400173D RID: 5949
		private List<char> suffixes;

		// Token: 0x0400173E RID: 5950
		private Dictionary<char, string> encoding;
	}
}
