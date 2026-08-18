using System;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

// Token: 0x0200024E RID: 590
internal class spr\u1AB5
{
	// Token: 0x06001DBC RID: 7612 RVA: 0x001D6400 File Offset: 0x001D5400
	internal static bool ᜀ(Regex A_0)
	{
		int a_ = 5;
		string text = A_0.ToString();
		if (text.Length != 0)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_42;
				}
			}
			IL_42:
			if (false)
			{
			}
			return text == ClipboardData.b("䍪剬卮䱰⵲ॴ⭶⹸ݺⅼ୾ꢀꮂ몄몆궈톌\ud88e쾒뺖", a_);
		}
		return true;
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x001D646C File Offset: 0x001D546C
	internal static Regex ᜀ(string A_0, bool A_1, bool A_2)
	{
		int a_ = 3;
		for (;;)
		{
			IL_21:
			A_0 = Regex.Escape(A_0);
			for (;;)
			{
				IL_29:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A8;
					case 1:
						if (A_2)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_93;
					case 2:
						goto IL_93;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_29;
						default:
							if (false)
							{
							}
							A_0 = ClipboardData.b("䅨呪公剮⽰ི⥴⁶ո❺ॼ噾", a_) + A_0 + ClipboardData.b("䅨呪偬䭮൰⽲≴୶╸ེ呼", a_);
							num = 2;
							continue;
						}
						break;
					}
					goto IL_21;
					IL_93:
					num = 0;
				}
			}
		}
		IL_A8:
		return new Regex(A_0, A_1 ? RegexOptions.None : RegexOptions.IgnoreCase);
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x001D6534 File Offset: 0x001D5534
	internal static int ᜀ(Paragraph A_0, int A_1, out TextRange A_2)
	{
		int result;
		for (;;)
		{
			A_2 = null;
			result = 0;
			int num = 0;
			int count = A_0.Items.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 3;
					continue;
				case 1:
					return result;
				case 2:
					goto IL_97;
				case 3:
					if (A_2.StartPos + A_2.TextLength >= A_1)
					{
						if (true)
						{
						}
						num2 = 7;
						continue;
					}
					goto IL_49;
				case 4:
					return result;
				case 5:
					goto IL_97;
				case 6:
					if (A_2 != null)
					{
						num2 = 0;
						continue;
					}
					goto IL_49;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_49;
					default:
						if (false)
						{
						}
						result = num;
						num2 = 4;
						continue;
					}
					break;
				case 8:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					A_2 = (A_0[num] as TextRange);
					num2 = 6;
					continue;
				}
				break;
				IL_49:
				num++;
				num2 = 5;
				continue;
				IL_97:
				num2 = 8;
			}
		}
		return result;
	}

	// Token: 0x04001F76 RID: 8054
	internal const string ᜀ = "(?<=^|\\W|\\t)";

	// Token: 0x04001F77 RID: 8055
	internal const string ᜁ = "(?=$|\\W|\\t)";

	// Token: 0x04001F78 RID: 8056
	internal const string ᜂ = "(?<=^|\\W|\\t)(?=$|\\W|\\t)";
}
