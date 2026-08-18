using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CE RID: 2510
	internal abstract class Code25 : Symbology1D
	{
		// Token: 0x0600603F RID: 24639 RVA: 0x00127010 File Offset: 0x00125210
		public Code25()
		{
			this.charset = new List<char>();
			this.charset.Add('0');
			this.charset.Add('1');
			this.charset.Add('2');
			this.charset.Add('3');
			this.charset.Add('4');
			this.charset.Add('5');
			this.charset.Add('6');
			this.charset.Add('7');
			this.charset.Add('8');
			this.charset.Add('9');
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x001270B0 File Offset: 0x001252B0
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
			return stringBuilder.ToString();
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x001270F2 File Offset: 0x001252F2
		public char GetChecksum(string value)
		{
			return this.GetChecksum(value, 3, 1, 10);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x00127100 File Offset: 0x00125300
		private char GetChecksum(string value, int first, int second, int modulo)
		{
			int num = 0;
			int num2 = first;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				int num3 = this.charset.IndexOf(value[i]);
				num += num3 * num2;
				if (num2 == first)
				{
					num2 = second;
				}
				else
				{
					num2 = first;
				}
			}
			num %= modulo;
			if (num != 0)
			{
				num = modulo - num;
			}
			return this.charset[num];
		}

		// Token: 0x0400174F RID: 5967
		private List<char> charset;
	}
}
