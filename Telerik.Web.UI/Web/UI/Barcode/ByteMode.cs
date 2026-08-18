using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009F6 RID: 2550
	internal class ByteMode : Symbology2D
	{
		// Token: 0x06006109 RID: 24841 RVA: 0x00146FAC File Offset: 0x001451AC
		internal ByteMode()
		{
			base.Encoding = new Dictionary<char, string>();
			base.Encoding.Add('\r', "13");
			base.Encoding.Add('\n', "10");
			base.Encoding.Add(' ', "32");
			base.Encoding.Add('!', "33");
			base.Encoding.Add('"', "34");
			base.Encoding.Add('#', "35");
			base.Encoding.Add('$', "36");
			base.Encoding.Add('%', "37");
			base.Encoding.Add('&', "38");
			base.Encoding.Add('\'', "39");
			base.Encoding.Add('(', "40");
			base.Encoding.Add(')', "41");
			base.Encoding.Add('*', "42");
			base.Encoding.Add('+', "43");
			base.Encoding.Add(',', "44");
			base.Encoding.Add('-', "45");
			base.Encoding.Add('.', "46");
			base.Encoding.Add('/', "47");
			base.Encoding.Add('0', "48");
			base.Encoding.Add('1', "49");
			base.Encoding.Add('2', "50");
			base.Encoding.Add('3', "51");
			base.Encoding.Add('4', "52");
			base.Encoding.Add('5', "53");
			base.Encoding.Add('6', "54");
			base.Encoding.Add('7', "55");
			base.Encoding.Add('8', "56");
			base.Encoding.Add('9', "57");
			base.Encoding.Add(':', "58");
			base.Encoding.Add(';', "59");
			base.Encoding.Add('<', "60");
			base.Encoding.Add('=', "61");
			base.Encoding.Add('>', "62");
			base.Encoding.Add('?', "63");
			base.Encoding.Add('@', "64");
			base.Encoding.Add('A', "65");
			base.Encoding.Add('B', "66");
			base.Encoding.Add('C', "67");
			base.Encoding.Add('D', "68");
			base.Encoding.Add('E', "69");
			base.Encoding.Add('F', "70");
			base.Encoding.Add('G', "71");
			base.Encoding.Add('H', "72");
			base.Encoding.Add('I', "73");
			base.Encoding.Add('J', "74");
			base.Encoding.Add('K', "75");
			base.Encoding.Add('L', "76");
			base.Encoding.Add('M', "77");
			base.Encoding.Add('N', "78");
			base.Encoding.Add('O', "79");
			base.Encoding.Add('P', "80");
			base.Encoding.Add('Q', "81");
			base.Encoding.Add('R', "82");
			base.Encoding.Add('S', "83");
			base.Encoding.Add('T', "84");
			base.Encoding.Add('U', "85");
			base.Encoding.Add('V', "86");
			base.Encoding.Add('W', "87");
			base.Encoding.Add('X', "88");
			base.Encoding.Add('Y', "89");
			base.Encoding.Add('Z', "90");
			base.Encoding.Add('[', "91");
			base.Encoding.Add('¥', "92");
			base.Encoding.Add(']', "93");
			base.Encoding.Add('^', "94");
			base.Encoding.Add('_', "95");
			base.Encoding.Add('`', "96");
			base.Encoding.Add('a', "97");
			base.Encoding.Add('b', "98");
			base.Encoding.Add('c', "99");
			base.Encoding.Add('d', "100");
			base.Encoding.Add('e', "101");
			base.Encoding.Add('f', "102");
			base.Encoding.Add('g', "103");
			base.Encoding.Add('h', "104");
			base.Encoding.Add('i', "105");
			base.Encoding.Add('j', "106");
			base.Encoding.Add('k', "107");
			base.Encoding.Add('l', "108");
			base.Encoding.Add('m', "109");
			base.Encoding.Add('n', "110");
			base.Encoding.Add('o', "111");
			base.Encoding.Add('p', "112");
			base.Encoding.Add('q', "113");
			base.Encoding.Add('r', "114");
			base.Encoding.Add('s', "115");
			base.Encoding.Add('t', "116");
			base.Encoding.Add('u', "117");
			base.Encoding.Add('v', "118");
			base.Encoding.Add('w', "119");
			base.Encoding.Add('x', "120");
			base.Encoding.Add('y', "121");
			base.Encoding.Add('z', "122");
			base.Encoding.Add('{', "123");
			base.Encoding.Add('|', "124");
			base.Encoding.Add('}', "125");
			base.Encoding.Add('¯', "126");
			base.Encoding.Add('｡', "161");
			base.Encoding.Add('｢', "162");
			base.Encoding.Add('｣', "163");
			base.Encoding.Add('､', "164");
			base.Encoding.Add('･', "165");
			base.Encoding.Add('ｦ', "166");
			base.Encoding.Add('ｧ', "167");
			base.Encoding.Add('ｨ', "168");
			base.Encoding.Add('ｩ', "169");
			base.Encoding.Add('ｪ', "170");
			base.Encoding.Add('ｫ', "171");
			base.Encoding.Add('ｬ', "172");
			base.Encoding.Add('ｭ', "173");
			base.Encoding.Add('ｮ', "174");
			base.Encoding.Add('ｯ', "175");
			base.Encoding.Add('ｰ', "176");
			base.Encoding.Add('ｱ', "177");
			base.Encoding.Add('ｲ', "178");
			base.Encoding.Add('ｳ', "179");
			base.Encoding.Add('ｴ', "180");
			base.Encoding.Add('ｵ', "181");
			base.Encoding.Add('ｶ', "182");
			base.Encoding.Add('ｷ', "183");
			base.Encoding.Add('ｸ', "184");
			base.Encoding.Add('ｹ', "185");
			base.Encoding.Add('ｺ', "186");
			base.Encoding.Add('ｻ', "187");
			base.Encoding.Add('ｼ', "188");
			base.Encoding.Add('ｽ', "189");
			base.Encoding.Add('ｾ', "190");
			base.Encoding.Add('ｿ', "191");
			base.Encoding.Add('ﾀ', "192");
			base.Encoding.Add('ﾁ', "193");
			base.Encoding.Add('ﾂ', "194");
			base.Encoding.Add('ﾃ', "195");
			base.Encoding.Add('ﾄ', "196");
			base.Encoding.Add('ﾅ', "197");
			base.Encoding.Add('ﾆ', "198");
			base.Encoding.Add('ﾇ', "199");
			base.Encoding.Add('ﾈ', "200");
			base.Encoding.Add('ﾉ', "201");
			base.Encoding.Add('ﾊ', "202");
			base.Encoding.Add('ﾋ', "203");
			base.Encoding.Add('ﾌ', "204");
			base.Encoding.Add('ﾍ', "205");
			base.Encoding.Add('ﾎ', "206");
			base.Encoding.Add('ﾏ', "207");
			base.Encoding.Add('ﾐ', "208");
			base.Encoding.Add('ﾑ', "209");
			base.Encoding.Add('ﾒ', "210");
			base.Encoding.Add('ﾓ', "211");
			base.Encoding.Add('ﾔ', "212");
			base.Encoding.Add('ﾕ', "213");
			base.Encoding.Add('ﾖ', "214");
			base.Encoding.Add('ﾗ', "215");
			base.Encoding.Add('ﾘ', "216");
			base.Encoding.Add('ﾙ', "217");
			base.Encoding.Add('ﾚ', "218");
			base.Encoding.Add('ﾛ', "219");
			base.Encoding.Add('ﾜ', "220");
			base.Encoding.Add('ﾝ', "221");
			base.Encoding.Add('ﾞ', "222");
			base.Encoding.Add('ﾟ', "223");
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x00147BD0 File Offset: 0x00145DD0
		public string ValidateData(string valueToValidate)
		{
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

		// Token: 0x0600610B RID: 24843 RVA: 0x00147C1C File Offset: 0x00145E1C
		public Dictionary<int, string> EncodeData(string dataToEncode)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < dataToEncode.Length; i++)
			{
				int value = int.Parse(base.Encoding[dataToEncode[i]]);
				dictionary.Add(i, Convert.ToString(value, 2).PadLeft(8, '0'));
			}
			return dictionary;
		}
	}
}
