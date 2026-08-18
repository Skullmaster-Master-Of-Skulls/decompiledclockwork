using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009CD RID: 2509
	internal class Code128C : Code128
	{
		// Token: 0x0600603A RID: 24634 RVA: 0x0012674C File Offset: 0x0012494C
		[Description("Initializes a new instance of Code128C type.")]
		public Code128C()
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
			this.charset.Add('õ');
			this.charset.Add('ô');
			this.charset.Add('÷');
			this.charset.Add('ü');
			this.charset.Add('ý');
			this.charset.Add('þ');
			this.charset.Add('ÿ');
			this.codeset = new List<string>();
			this.codeset.Add("00");
			this.codeset.Add("01");
			this.codeset.Add("02");
			this.codeset.Add("03");
			this.codeset.Add("04");
			this.codeset.Add("05");
			this.codeset.Add("06");
			this.codeset.Add("07");
			this.codeset.Add("08");
			this.codeset.Add("09");
			this.codeset.Add("10");
			this.codeset.Add("11");
			this.codeset.Add("12");
			this.codeset.Add("13");
			this.codeset.Add("14");
			this.codeset.Add("15");
			this.codeset.Add("16");
			this.codeset.Add("17");
			this.codeset.Add("18");
			this.codeset.Add("19");
			this.codeset.Add("20");
			this.codeset.Add("21");
			this.codeset.Add("22");
			this.codeset.Add("23");
			this.codeset.Add("24");
			this.codeset.Add("25");
			this.codeset.Add("26");
			this.codeset.Add("27");
			this.codeset.Add("28");
			this.codeset.Add("29");
			this.codeset.Add("30");
			this.codeset.Add("31");
			this.codeset.Add("32");
			this.codeset.Add("33");
			this.codeset.Add("34");
			this.codeset.Add("35");
			this.codeset.Add("36");
			this.codeset.Add("37");
			this.codeset.Add("38");
			this.codeset.Add("39");
			this.codeset.Add("40");
			this.codeset.Add("41");
			this.codeset.Add("42");
			this.codeset.Add("43");
			this.codeset.Add("44");
			this.codeset.Add("45");
			this.codeset.Add("46");
			this.codeset.Add("47");
			this.codeset.Add("48");
			this.codeset.Add("49");
			this.codeset.Add("50");
			this.codeset.Add("51");
			this.codeset.Add("52");
			this.codeset.Add("53");
			this.codeset.Add("54");
			this.codeset.Add("55");
			this.codeset.Add("56");
			this.codeset.Add("57");
			this.codeset.Add("58");
			this.codeset.Add("59");
			this.codeset.Add("60");
			this.codeset.Add("61");
			this.codeset.Add("62");
			this.codeset.Add("63");
			this.codeset.Add("64");
			this.codeset.Add("65");
			this.codeset.Add("66");
			this.codeset.Add("67");
			this.codeset.Add("68");
			this.codeset.Add("69");
			this.codeset.Add("70");
			this.codeset.Add("71");
			this.codeset.Add("72");
			this.codeset.Add("73");
			this.codeset.Add("74");
			this.codeset.Add("75");
			this.codeset.Add("76");
			this.codeset.Add("77");
			this.codeset.Add("78");
			this.codeset.Add("79");
			this.codeset.Add("80");
			this.codeset.Add("81");
			this.codeset.Add("82");
			this.codeset.Add("83");
			this.codeset.Add("84");
			this.codeset.Add("85");
			this.codeset.Add("86");
			this.codeset.Add("87");
			this.codeset.Add("88");
			this.codeset.Add("89");
			this.codeset.Add("90");
			this.codeset.Add("91");
			this.codeset.Add("92");
			this.codeset.Add("93");
			this.codeset.Add("94");
			this.codeset.Add("95");
			this.codeset.Add("96");
			this.codeset.Add("97");
			this.codeset.Add("98");
			this.codeset.Add("99");
			this.codeset.Add("õ");
			this.codeset.Add("ô");
			this.codeset.Add("÷");
			this.codeset.Add("ü");
			this.codeset.Add("ý");
			this.codeset.Add("þ");
			this.codeset.Add("ÿ");
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x00126F18 File Offset: 0x00125118
		internal int GetSwitch(string value, int start, int final)
		{
			int i;
			int num2;
			for (i = start; i < final; i = num2)
			{
				int num;
				if (char.IsDigit(value[i]))
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
				num2 = i + num;
				if (num2 > final)
				{
					return i;
				}
				string item = value.Substring(i, num);
				if (!this.codeset.Contains(item))
				{
					return i;
				}
			}
			return i;
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x00126F68 File Offset: 0x00125168
		internal int[] GetIndices(string value, int start, int final)
		{
			List<int> list = new List<int>();
			if (start > 0)
			{
				list.Add(Code128C.Switch);
			}
			else
			{
				list.Add(Code128C.Prefix);
			}
			int num2;
			for (int i = start; i < final; i = num2)
			{
				int num;
				if (char.IsDigit(value[i]))
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
				num2 = i + num;
				if (num2 <= final)
				{
					string item = value.Substring(i, num);
					if (this.codeset.Contains(item))
					{
						list.Add(this.codeset.IndexOf(item));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x00126FEF File Offset: 0x001251EF
		protected override int[] GetIndices(string value)
		{
			return this.GetIndices(value, 0, value.Length);
		}

		// Token: 0x0400174B RID: 5963
		private static readonly int Switch = 99;

		// Token: 0x0400174C RID: 5964
		private static readonly int Prefix = 105;

		// Token: 0x0400174D RID: 5965
		private List<char> charset;

		// Token: 0x0400174E RID: 5966
		private List<string> codeset;
	}
}
