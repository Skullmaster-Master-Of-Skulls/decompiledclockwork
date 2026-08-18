using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009F5 RID: 2549
	internal class AlphaNumeric : Symbology2D
	{
		// Token: 0x06006106 RID: 24838 RVA: 0x00146AF0 File Offset: 0x00144CF0
		public AlphaNumeric()
		{
			base.Encoding = new Dictionary<char, string>();
			base.Encoding.Add('0', "0");
			base.Encoding.Add('1', "1");
			base.Encoding.Add('2', "2");
			base.Encoding.Add('3', "3");
			base.Encoding.Add('4', "4");
			base.Encoding.Add('5', "5");
			base.Encoding.Add('6', "6");
			base.Encoding.Add('7', "7");
			base.Encoding.Add('8', "8");
			base.Encoding.Add('9', "9");
			base.Encoding.Add('A', "10");
			base.Encoding.Add('B', "11");
			base.Encoding.Add('C', "12");
			base.Encoding.Add('D', "13");
			base.Encoding.Add('E', "14");
			base.Encoding.Add('F', "15");
			base.Encoding.Add('G', "16");
			base.Encoding.Add('H', "17");
			base.Encoding.Add('I', "18");
			base.Encoding.Add('J', "19");
			base.Encoding.Add('K', "20");
			base.Encoding.Add('L', "21");
			base.Encoding.Add('M', "22");
			base.Encoding.Add('N', "23");
			base.Encoding.Add('O', "24");
			base.Encoding.Add('P', "25");
			base.Encoding.Add('Q', "26");
			base.Encoding.Add('R', "27");
			base.Encoding.Add('S', "28");
			base.Encoding.Add('T', "29");
			base.Encoding.Add('U', "30");
			base.Encoding.Add('V', "31");
			base.Encoding.Add('W', "32");
			base.Encoding.Add('X', "33");
			base.Encoding.Add('Y', "34");
			base.Encoding.Add('Z', "35");
			base.Encoding.Add(' ', "36");
			base.Encoding.Add('$', "37");
			base.Encoding.Add('%', "38");
			base.Encoding.Add('*', "39");
			base.Encoding.Add('+', "40");
			base.Encoding.Add('-', "41");
			base.Encoding.Add('.', "42");
			base.Encoding.Add('/', "43");
			base.Encoding.Add(':', "44");
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x00146E38 File Offset: 0x00145038
		public string ValidateData(string valueToValidate)
		{
			valueToValidate = valueToValidate.ToUpper();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in valueToValidate)
			{
				if (base.Encoding.ContainsKey(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x00146E8C File Offset: 0x0014508C
		public Dictionary<int, string> EncodeData(string dataToEncode)
		{
			bool flag = dataToEncode.Length % 2 != 0;
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			List<int> list = new List<int>();
			Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
			for (int i = 0; i < dataToEncode.Length; i++)
			{
				char key = dataToEncode[i];
				dictionary.Add(i, base.Encoding[key]);
			}
			for (int j = 0; j < dictionary.Count; j += 2)
			{
				int num = int.Parse(dictionary[j]);
				if (j + 1 <= dictionary.Count - 1)
				{
					int num2 = int.Parse(dictionary[j + 1]);
					int item = 45 * num + num2;
					list.Add(item);
				}
				else
				{
					list.Add(num);
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				string text = Convert.ToString(list[k], 2);
				if (flag && k == list.Count - 1)
				{
					dictionary2.Add(k, text.PadLeft(6, '0'));
				}
				else
				{
					dictionary2.Add(k, text.PadLeft(11, '0'));
				}
			}
			return dictionary2;
		}
	}
}
