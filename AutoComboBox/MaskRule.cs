using System;
using System.Collections.Generic;

namespace AutoComboBox
{
	// Token: 0x02000033 RID: 51
	public class MaskRule
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00010058 File Offset: 0x0000F058
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0001006F File Offset: 0x0000F06F
		public string Mask { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00010078 File Offset: 0x0000F078
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0001008F File Offset: 0x0000F08F
		public string MaskGroup { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00010098 File Offset: 0x0000F098
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000100AF File Offset: 0x0000F0AF
		public char[] MaskChars { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000100B8 File Offset: 0x0000F0B8
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000100CF File Offset: 0x0000F0CF
		public List<int> SpaceInserts { get; set; }

		// Token: 0x060001A8 RID: 424 RVA: 0x000100D8 File Offset: 0x0000F0D8
		public MaskRule()
		{
			this.MaskChars = new char[0];
			this.SpaceInserts = new List<int>();
			this.Mask = "";
			this.MaskGroup = "";
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00010114 File Offset: 0x0000F114
		public static List<MaskRule> MaskRulesFromString(string rule)
		{
			List<MaskRule> list = new List<MaskRule>();
			string[] array = rule.Split(new char[]
			{
				';'
			});
			foreach (string s in array)
			{
				MaskRule maskRule = MaskRule.MaskRuleFromString(s);
				if (maskRule != null)
				{
					list.Add(maskRule);
				}
			}
			return list;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00010184 File Offset: 0x0000F184
		public static MaskRule MaskRuleFromString(string s)
		{
			int num = s.IndexOf(':');
			MaskRule result;
			if (num > 0)
			{
				string text = s.Substring(num + 1);
				MaskRule maskRule = new MaskRule
				{
					MaskGroup = s.Substring(0, num),
					Mask = text,
					MaskChars = text.ToCharArray(),
					SpaceInserts = MaskRule.SpaceInsertsFromMask(text)
				};
				result = maskRule;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000101F8 File Offset: 0x0000F1F8
		public static List<int> SpaceInsertsFromMask(string mask)
		{
			List<int> list = new List<int>();
			int i = -1;
			while (i < mask.Length)
			{
				i = mask.IndexOf(' ', i + 1);
				if (i < 0)
				{
					break;
				}
				list.Add(i);
			}
			return list;
		}
	}
}
