using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009FD RID: 2557
	internal class Numeric : Symbology2D
	{
		// Token: 0x0600610F RID: 24847 RVA: 0x0016CDF4 File Offset: 0x0016AFF4
		public Numeric()
		{
			base.CharSet = new List<char>();
			base.CharSet.Add('0');
			base.CharSet.Add('1');
			base.CharSet.Add('2');
			base.CharSet.Add('3');
			base.CharSet.Add('4');
			base.CharSet.Add('5');
			base.CharSet.Add('6');
			base.CharSet.Add('7');
			base.CharSet.Add('8');
			base.CharSet.Add('9');
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0016CE94 File Offset: 0x0016B094
		public string ValidateData(string valueToValidate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in valueToValidate)
			{
				if (base.CharSet.Contains(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x0016CEE0 File Offset: 0x0016B0E0
		public Dictionary<int, string> EncodeData(string dataToEncode)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			this.EncodeSectionData(dictionary, dataToEncode);
			return dictionary;
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x0016CEFC File Offset: 0x0016B0FC
		private void EncodeSectionData(Dictionary<int, string> binaryResult, string rawData)
		{
			if (rawData.Length >= 3)
			{
				string s = rawData.Substring(0, 3);
				rawData = rawData.Substring(3);
				int value = int.Parse(s);
				string text = Convert.ToString(value, 2);
				text = text.PadLeft(10, '0');
				binaryResult.Add(binaryResult.Count, text);
				this.EncodeSectionData(binaryResult, rawData);
				return;
			}
			if (rawData.Length == 2)
			{
				string s = rawData;
				int value = int.Parse(s);
				string text = Convert.ToString(value, 2);
				text = text.PadLeft(7, '0');
				binaryResult.Add(binaryResult.Count, text);
				return;
			}
			if (rawData.Length == 1)
			{
				string s = rawData;
				int value = int.Parse(s);
				string text = Convert.ToString(value, 2);
				text = text.PadLeft(4, '0');
				binaryResult.Add(binaryResult.Count, text);
				return;
			}
			if (binaryResult.Count == 0)
			{
				string text = Convert.ToString(0, 2);
				text = text.PadLeft(4, '0');
				binaryResult.Add(binaryResult.Count, text);
			}
		}
	}
}
