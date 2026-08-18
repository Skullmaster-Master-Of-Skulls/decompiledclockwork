using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000003 RID: 3
internal class sprṔ
{
	// Token: 0x06000005 RID: 5 RVA: 0x00003534 File Offset: 0x00002534
	public static string ᜂ(long A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = sprṔ.ᜁ(A_0);
		int num2 = sprṔ.ᜀ(A_0);
		return sprṔ.ᜀ(num, num2, num, num2, true);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00003588 File Offset: 0x00002588
	public static long ᜁ(string A_0)
	{
		int a_ = 9;
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7F;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_87;
			case 2:
				goto IL_58;
			case 3:
				if (A_0.Length < 2)
				{
					goto IL_7F;
				}
				goto IL_9D;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 3;
			continue;
			IL_7F:
			num = 1;
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("儾⁀⹂⁄", a_));
		IL_87:
		throw new ArgumentException(RecordTableEnumerator.b("儾⁀⹂⁄杆㩈⍊≌㩎㵐㝒畔㩖㙘⥚㡜罞ᕠୢѤ०䥨奪䵬ᱮࡰṲ᝴ᡶᕸࡺ", a_));
		IL_9D:
		int a_2 = 0;
		int a_3 = 0;
		sprṔ.ᜀ(A_0, out a_2, out a_3);
		return sprṔ.ᜀ(a_3, a_2);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00003654 File Offset: 0x00002654
	public static void ᜀ(string A_0, out int A_1, out int A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 9;
			int num2;
			char c;
			int num4;
			int num5;
			int num6;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					goto IL_1AB;
				case 2:
					goto IL_174;
				case 3:
					goto IL_18A;
				case 4:
					num2 = num3;
					num = 12;
					continue;
				case 5:
					goto IL_208;
				case 6:
					if (char.IsLetter(c))
					{
						num = 0;
						continue;
					}
					num = 17;
					continue;
				case 7:
					if (num4 < 0)
					{
						num = 8;
						continue;
					}
					goto IL_95;
				case 8:
					num4 = num3;
					num = 10;
					continue;
				case 10:
					goto IL_95;
				case 11:
					num = 19;
					continue;
				case 12:
					goto IL_1B0;
				case 13:
					goto IL_90;
				case 14:
				{
					int length;
					if (num3 >= length)
					{
						num = 1;
						continue;
					}
					c = A_0[num3];
					if (true)
					{
					}
					num = 15;
					continue;
				}
				case 15:
					if (char.IsDigit(c))
					{
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B0;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 16:
					goto IL_1D6;
				case 17:
					if (c != '$')
					{
						num = 5;
						continue;
					}
					goto IL_1D6;
				case 18:
					goto IL_18A;
				case 19:
					if (num2 < 0)
					{
						num = 4;
						continue;
					}
					goto IL_1B0;
				case 20:
				{
					if (A_0.Length < 2)
					{
						num = 2;
						continue;
					}
					num4 = -1;
					num5 = 0;
					num2 = -1;
					num6 = 0;
					num3 = 0;
					int length = A_0.Length;
					num = 18;
					continue;
				}
				case 21:
					goto IL_1D6;
				}
				if (A_0 == null)
				{
					num = 13;
					continue;
				}
				num = 20;
				continue;
				IL_95:
				num5++;
				num = 16;
				continue;
				IL_18A:
				num = 14;
				continue;
				IL_1B0:
				num6++;
				num = 21;
				continue;
				IL_1D6:
				num3++;
				num = 3;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("倽ℿ⽁⅃", a_));
			IL_174:
			throw new ArgumentException(RecordTableEnumerator.b("倽ℿ⽁⅃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙せ㭝፟ᅡ䑣ብgཀྵɫ乭䉯剱ݳཱུᕷ᡹፻ች", a_));
			IL_1AB:
			string s = A_0.Substring(num2, num6);
			string a_2 = A_0.Substring(num4, num5);
			A_1 = int.Parse(s, NumberStyles.None, NumberFormatInfo.InvariantInfo);
			A_2 = sprṔ.ᜀ(a_2);
			return;
			IL_208:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倽ℿ⽁⅃", a_), RecordTableEnumerator.b("紽⠿⍁㙃❅⭇㹉⥋㱍灏", a_) + c + RecordTableEnumerator.b("ḽ㜿⍁㝃晅♇╉㡋湍㕏⩑⑓㍕㭗⹙㥛㩝也", a_));
		}
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000393C File Offset: 0x0000293C
	public static int ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				int length = A_0.Length;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (true)
						{
						}
						if (num2 >= length)
						{
							num3 = 3;
							continue;
						}
						char c = A_0[num2];
						num *= 26;
						num3 = 4;
						continue;
					}
					case 1:
						goto IL_7E;
					case 2:
						goto IL_7E;
					case 3:
						return num;
					case 4:
					{
						char c;
						num += (int)('\u0001' + ((c >= 'a') ? (c - 'a') : (c - 'A')));
						num2++;
						num3 = 1;
						continue;
					}
					}
					break;
					IL_7E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
						if (false)
						{
						}
						num3 = 0;
						break;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00003A14 File Offset: 0x00002A14
	public static string ᜀ(int A_0)
	{
		int a_ = 18;
		int num = 0;
		for (;;)
		{
			string text;
			switch (num)
			{
			case 1:
				goto IL_3E;
			case 2:
				if (A_0 < 0)
				{
					num = 4;
					continue;
				}
				goto IL_3E;
			case 3:
				goto IL_39;
			case 4:
				return text;
			}
			if (A_0 < 1)
			{
				num = 3;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return text;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				A_0--;
				text = string.Empty;
				num = 1;
				continue;
			}
			IL_3E:
			int num2 = A_0 % 26;
			A_0 = A_0 / 26 - 1;
			text = Convert.ToChar(65 + num2) + text;
			num = 2;
		}
		IL_39:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭇╉⁋㭍㵏㱑ᵓ㡕㱗㽙⑛", a_), RecordTableEnumerator.b("ṇ⭉⁋㭍㕏牑❓㹕㝗⽙せ㩝䁟aţ䙥੧ཀྵᡫᥭᕯ᝱ᩳ噵䥷呹", a_));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00003B00 File Offset: 0x00002B00
	public static string ᜂ(int A_0, int A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return sprṔ.ᜀ(A_0, A_1, false);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00003B44 File Offset: 0x00002B44
	public static string ᜀ(int A_0, int A_1, bool A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return sprṔ.ᜀ(A_0, A_1, A_2, false);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00003B88 File Offset: 0x00002B88
	public static string ᜀ(int A_0, int A_1, bool A_2, bool A_3)
	{
		int a_ = 10;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3D;
			case 1:
				if (!A_3)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				default:
					goto IL_D3;
				}
				break;
			case 3:
				goto IL_BB;
			case 4:
				goto IL_52;
			case 5:
				if (A_2)
				{
					num = 3;
					continue;
				}
				goto IL_3F;
			}
			if (A_1 < 1)
			{
				num = 0;
				continue;
			}
			num = 5;
			continue;
			IL_3F:
			num = 1;
		}
		IL_3D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ሿⵁ㍃晅ⅇ⑉⡋⭍⡏牑㵓╕硗ⵙ⹛ㅝ๟ա䩣䙥Ⅷṩ䱫൭ᅯᱱᩳ᥵౷婹ṻ᭽ꁿﮇꪉﲑ뒓ꞕ", a_));
		IL_52:
		if (true)
		{
		}
		return sprṔ.ᜀ(A_0) + A_1;
		IL_BB:
		return string.Format(RecordTableEnumerator.b("ሿ㥁瑃㭅େㅉ絋㍍", a_), A_1, A_0);
		IL_D3:
		if (false)
		{
		}
		return string.Concat(new object[]
		{
			'$',
			sprṔ.ᜀ(A_0),
			'$',
			A_1
		});
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00003CAC File Offset: 0x00002CAC
	public static string ᜀ(int A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 19;
		string text;
		for (;;)
		{
			IL_45:
			text = sprṔ.ᜂ(A_1, A_0);
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (A_0 == A_2)
						{
							num = 1;
							continue;
						}
						goto IL_7B;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_6D;
					case 3:
						return text;
					}
					goto IL_45;
				}
				IL_6D:
				if (A_1 != A_3)
				{
					goto IL_7B;
				}
				num = 3;
			}
		}
		return text;
		IL_7B:
		string str = sprṔ.ᜂ(A_3, A_2);
		return text + RecordTableEnumerator.b("獈", a_) + str;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00003D5C File Offset: 0x00002D5C
	public static string ᜀ(int A_0, int A_1, int A_2, int A_3, bool A_4)
	{
		int a_ = 15;
		string text;
		for (;;)
		{
			IL_3D:
			text = sprṔ.ᜀ(A_1, A_0, A_4);
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return text;
					case 1:
						goto IL_67;
					case 2:
						num = 1;
						continue;
					case 3:
						if (A_0 == A_2)
						{
							num = 2;
							continue;
						}
						goto IL_7D;
					}
					goto IL_3D;
				}
				IL_67:
				if (true)
				{
				}
				if (A_1 != A_3)
				{
					goto IL_7D;
				}
				num = 0;
			}
		}
		return text;
		IL_7D:
		string str = sprṔ.ᜀ(A_3, A_2, A_4);
		return text + RecordTableEnumerator.b("罄", a_) + str;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003E10 File Offset: 0x00002E10
	public static string ᜁ(int A_0, int A_1)
	{
		int a_ = 14;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				goto IL_8D;
			case 2:
				num = 3;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (A_1 < 1)
					{
						num = 1;
						continue;
					}
					goto IL_8F;
				}
				break;
			}
			if (A_0 < 1)
			{
				break;
			}
			num = 2;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("❃⥅⑇㽉⅋⁍灏㵑♓癕⩗㕙⭛繝य़ౡcͥၧ䩩իᵭ偯ᵱųɵ塷ᕹ᩻幽ꒉ", a_));
		IL_8D:
		goto IL_3F;
		IL_8F:
		string text = sprṔ.ᜀ(A_0);
		return string.Concat(new object[]
		{
			RecordTableEnumerator.b("恃", a_),
			text,
			RecordTableEnumerator.b("恃", a_),
			A_1
		});
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00003EF0 File Offset: 0x00002EF0
	public static long ᜀ(int A_0, int A_1)
	{
		int a_ = 0;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (A_0 < 0)
					{
						num = 2;
						continue;
					}
					goto IL_8F;
				}
				break;
			case 2:
				goto IL_8D;
			case 3:
				num = 0;
				continue;
			}
			if (A_1 < 0)
			{
				break;
			}
			if (true)
			{
			}
			num = 3;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䄵䨷唹刻夽怿ぁ⭃ㅅ桇╉㹋湍㍏㵑㡓⍕㕗㑙籛㝝๟١ţṥ", a_));
		IL_8D:
		goto IL_3F;
		IL_8F:
		return ((long)A_1 << 32) + (long)A_0;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00003F94 File Offset: 0x00002F94
	[DebuggerStepThrough]
	public static int ᜁ(long A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (int)((ulong)A_0 >> 32);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003FD8 File Offset: 0x00002FD8
	[DebuggerStepThrough]
	public static int ᜀ(long A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return (int)(A_0 & (long)((ulong)-1));
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00004018 File Offset: 0x00003018
	public static string ᜀ(ref string A_0)
	{
		int a_ = 0;
		int num = 9;
		string text;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				text = text.Substring(text.IndexOf('(') + 1);
				num = 13;
				continue;
			case 1:
				goto IL_61;
			case 2:
				goto IL_CD;
			case 3:
				if (text[0] == '\'')
				{
					num = 12;
					continue;
				}
				return text;
			case 4:
				if (text[length - 1] == '\'')
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				return text;
			case 5:
			{
				if (A_0.Length == 0)
				{
					num = 2;
					continue;
				}
				text = null;
				int num2 = A_0.IndexOf('!');
				num = 6;
				continue;
			}
			case 6:
			{
				int num2;
				if (num2 != -1)
				{
					num = 10;
					continue;
				}
				return text;
			}
			case 7:
				text = text.Substring(1, length - 2);
				num = 11;
				continue;
			case 8:
			{
				if (text.Contains(RecordTableEnumerator.b("ḵ", a_)))
				{
					num = 0;
					continue;
				}
				int num2;
				A_0 = A_0.Substring(num2 + 1, A_0.Length - num2 - 1);
				num = 14;
				continue;
			}
			case 10:
			{
				int num2;
				text = A_0.Substring(0, num2).Replace(RecordTableEnumerator.b("ᄵἷ", a_), RecordTableEnumerator.b("ᄵ", a_));
				num = 8;
				continue;
			}
			case 11:
				return text;
			case 12:
				num = 4;
				continue;
			case 13:
				goto IL_66;
			case 14:
				goto IL_66;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			num = 5;
			continue;
			IL_66:
			length = text.Length;
			num = 3;
		}
		IL_61:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_CD:
			throw new ArgumentException(RecordTableEnumerator.b("䐵夷吹嬻嬽฿⍁⥃⍅桇⥉ⵋ⁍灏㱑㭓≕硗㡙㥛繝՟ཡᑣብᅧ", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽฿⍁⥃⍅", a_));
		}
		return text;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00004244 File Offset: 0x00003244
	public static bool ᜁ(IList A_0)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				bool flag;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					goto IL_D2;
				case 2:
				{
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					IXLSRange ixlsrange = A_0[num2] as IXLSRange;
					num = 10;
					continue;
				}
				case 3:
					return flag;
				case 4:
					goto IL_104;
				case 5:
					goto IL_69;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D2;
					default:
						if (false)
						{
						}
						flag = false;
						num = 3;
						continue;
					}
					break;
				case 7:
					if (flag)
					{
						num = 1;
						continue;
					}
					return flag;
				case 8:
					goto IL_104;
				case 9:
					return flag;
				case 10:
				{
					IXLSRange ixlsrange;
					if (!ixlsrange.IsWrapText)
					{
						num = 6;
						continue;
					}
					num2++;
					num = 8;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				flag = true;
				count = A_0.Count;
				num2 = 0;
				num = 4;
				continue;
				IL_D2:
				num = 2;
				continue;
				IL_104:
				num = 7;
			}
			IL_69:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽ℿⱁ⍃⍅େ╉⁋⭍㍏♑㵓㥕㙗", a_));
		}
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000043A8 File Offset: 0x000033A8
	public static void ᜀ(IList A_0, bool A_1)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = A_0.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_47;
					case 2:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						goto IL_5D;
					case 3:
						goto IL_47;
					}
					goto IL_34;
					IL_47:
					num2 = 2;
					continue;
				}
				IL_5D:
				if (true)
				{
				}
				((IXLSRange)A_0[num]).IsWrapText = A_1;
				num++;
				num2 = 1;
			}
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00004448 File Offset: 0x00003448
	public static string ᜀ(IList A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int count = A_0.Count;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_56;
					case 1:
					{
						int num2;
						if (num2 >= count)
						{
							num = 7;
							continue;
						}
						IXLSRange ixlsrange = (IXLSRange)A_0[num2];
						num = 2;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							IXLSRange ixlsrange;
							string numberFormat;
							if (numberFormat != ixlsrange.NumberFormat)
							{
								num = 4;
								continue;
							}
							int num2;
							num2++;
							break;
						}
						}
						if (true)
						{
						}
						num = 3;
						continue;
					case 3:
						goto IL_C9;
					case 4:
						goto IL_C5;
					case 5:
					{
						if (count == 0)
						{
							num = 0;
							continue;
						}
						IXLSRange ixlsrange = (IXLSRange)A_0[0];
						string numberFormat = ixlsrange.NumberFormat;
						int num2 = 1;
						num = 6;
						continue;
					}
					case 6:
						goto IL_C9;
					case 7:
					{
						string numberFormat;
						return numberFormat;
					}
					}
					break;
					IL_C9:
					num = 1;
				}
			}
			IL_56:
			return null;
			IL_C5:
			return null;
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00004564 File Offset: 0x00003564
	public static string ᜀ(IList<CellRange> A_0)
	{
		switch (0)
		{
		default:
		{
			string cellStyleName;
			for (;;)
			{
				int count = A_0.Count;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_BC;
					case 1:
						goto IL_B8;
					case 2:
					{
						int num2;
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IXLSRange ixlsrange = A_0[num2];
						num = 4;
						continue;
					}
					case 3:
						goto IL_D8;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							IXLSRange ixlsrange;
							if (cellStyleName != ixlsrange.CellStyleName)
							{
								num = 1;
								continue;
							}
							int num2;
							num2++;
							break;
						}
						}
						num = 0;
						continue;
					case 5:
					{
						if (count == 0)
						{
							num = 7;
							continue;
						}
						IXLSRange ixlsrange = A_0[0];
						cellStyleName = ixlsrange.CellStyleName;
						int num2 = 1;
						num = 6;
						continue;
					}
					case 6:
						goto IL_BC;
					case 7:
						goto IL_56;
					}
					break;
					IL_BC:
					num = 2;
				}
			}
			IL_56:
			return null;
			IL_B8:
			return null;
			IL_D8:
			if (true)
			{
			}
			return cellStyleName;
		}
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00004678 File Offset: 0x00003678
	public static string ᜀ(IList<IXLSRange> A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				int count = A_0.Count;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5E;
					case 1:
					{
						string cellStyleName;
						return cellStyleName;
					}
					case 2:
						goto IL_C0;
					case 3:
					{
						int num2;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						IXLSRange ixlsrange = A_0[num2];
						num = 6;
						continue;
					}
					case 4:
						goto IL_C4;
					case 5:
						goto IL_C4;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							string cellStyleName;
							IXLSRange ixlsrange;
							if (cellStyleName != ixlsrange.CellStyleName)
							{
								num = 2;
								continue;
							}
							int num2;
							num2++;
							break;
						}
						}
						num = 5;
						continue;
					case 7:
					{
						if (count == 0)
						{
							num = 0;
							continue;
						}
						IXLSRange ixlsrange = A_0[0];
						string cellStyleName = ixlsrange.CellStyleName;
						int num2 = 1;
						num = 4;
						continue;
					}
					}
					break;
					IL_C4:
					num = 3;
				}
			}
			IL_5E:
			return null;
			IL_C0:
			return null;
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000478C File Offset: 0x0000378C
	public static int ᜀ(string A_0, IWorkbook A_1, out int A_2, out int A_3, out int A_4, out int A_5)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num;
			Match match;
			for (;;)
			{
				A_5 = (A_4 = (A_3 = (A_2 = -1)));
				string[] array = A_0.Split(new char[]
				{
					':'
				});
				num = array.Length;
				Regex regex = FormulaUtil.FullRowRangeRegex;
				match = regex.Match(A_0);
				int num2 = 13;
				for (;;)
				{
					long num4;
					switch (num2)
					{
					case 0:
						if (match.Index == 0)
						{
							num2 = 1;
							continue;
						}
						goto IL_107;
					case 1:
						num2 = 15;
						continue;
					case 2:
						if (num >= 1)
						{
							num2 = 4;
							continue;
						}
						goto IL_E3;
					case 3:
					{
						long num3;
						A_4 = sprṔ.ᜁ(num3);
						A_5 = sprṔ.ᜀ(num3);
						num2 = 7;
						continue;
					}
					case 4:
						num4 = sprṔ.ᜁ(array[0]);
						A_4 = (A_2 = sprṔ.ᜁ(num4));
						A_5 = (A_3 = sprṔ.ᜀ(num4));
						num2 = 8;
						continue;
					case 5:
					{
						long num3 = sprṔ.ᜁ(array[1]);
						num2 = 9;
						continue;
					}
					case 6:
						num2 = 12;
						continue;
					case 7:
						goto IL_1B7;
					case 8:
						goto IL_E3;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D2;
						default:
						{
							if (false)
							{
							}
							long num3;
							if (num4 != num3)
							{
								num2 = 3;
								continue;
							}
							return num;
						}
						}
						break;
					case 10:
						goto IL_3B9;
					case 11:
						num2 = 16;
						continue;
					case 12:
						if (match.Length == A_0.Length)
						{
							num2 = 21;
							continue;
						}
						goto IL_2A7;
					case 13:
						if (match.Success)
						{
							goto IL_D2;
						}
						goto IL_2A7;
					case 14:
						num2 = 0;
						continue;
					case 15:
						if (match.Length == A_0.Length)
						{
							num2 = 10;
							continue;
						}
						goto IL_107;
					case 16:
						if (match.Index == 0)
						{
							num2 = 6;
							continue;
						}
						goto IL_2A7;
					case 17:
						if (num == 2)
						{
							num2 = 5;
							continue;
						}
						num2 = 20;
						continue;
					case 18:
						if (match.Success)
						{
							if (true)
							{
							}
							num2 = 14;
							continue;
						}
						goto IL_107;
					case 19:
						goto IL_301;
					case 20:
						if (num > 2)
						{
							num2 = 19;
							continue;
						}
						return num;
					case 21:
						goto IL_1E5;
					}
					break;
					IL_D2:
					num2 = 11;
					continue;
					IL_E3:
					num2 = 17;
					continue;
					IL_107:
					num4 = -1L;
					num2 = 2;
					continue;
					IL_2A7:
					regex = FormulaUtil.FullColumnRangeRegex;
					match = regex.Match(A_0);
					num2 = 18;
				}
			}
			IL_1B7:
			return num;
			IL_1E5:
			A_3 = 1;
			A_5 = A_1.MaxColumnCount;
			string value = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("派⹀㑂瑄", a_)].Value);
			string value2 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("派⹀㑂睄", a_)].Value);
			A_2 = Convert.ToInt32(value);
			A_4 = Convert.ToInt32(value2);
			return num;
			IL_301:
			throw new ArgumentException();
			IL_3B9:
			string a_2 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("簾⹀⽂い⩆❈穊", a_)].Value);
			string a_3 = UtilityMethods.ᜀ(match.Groups[RecordTableEnumerator.b("簾⹀⽂い⩆❈祊", a_)].Value);
			A_3 = sprṔ.ᜀ(a_2);
			A_5 = sprṔ.ᜀ(a_3);
			A_2 = 1;
			A_4 = A_1.MaxRowCount;
			return num;
		}
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00004B64 File Offset: 0x00003B64
	public static Rectangle ᜀ(IXLSRange A_0, bool A_1)
	{
		int a_ = 2;
		Rectangle result;
		for (;;)
		{
			for (;;)
			{
				result = new Rectangle(-1, -1, -1, -1);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (A_1)
							{
								num = 3;
								continue;
							}
							return result;
						}
						break;
					case 1:
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						goto IL_9D;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_9B;
					}
					break;
				}
			}
		}
		return result;
		IL_9B:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨷嬹刻夽┿", a_));
		IL_9D:
		result.Y = A_0.Row;
		result.Height = A_0.LastRow - result.Y;
		result.X = A_0.Column;
		result.Width = A_0.LastColumn - result.X;
		return result;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00004C68 File Offset: 0x00003C68
	// Note: this type is marked as 'beforefieldinit'.
	static sprṔ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		sprṔ.\u1713 = new sprṔ.TCellType[]
		{
			sprṔ.TCellType.RK,
			sprṔ.TCellType.Number,
			sprṔ.TCellType.Formula
		};
	}

	// Token: 0x04000004 RID: 4
	private const char ᜀ = '$';

	// Token: 0x04000005 RID: 5
	public const string ᜁ = "mm/dd/yyyy";

	// Token: 0x04000006 RID: 6
	public const string ᜂ = "h:mm:ss";

	// Token: 0x04000007 RID: 7
	public const string ᜃ = "0.00";

	// Token: 0x04000008 RID: 8
	public const string ᜄ = "@";

	// Token: 0x04000009 RID: 9
	public const string ᜅ = "General";

	// Token: 0x0400000A RID: 10
	private const string ᜆ = "{{={0}}}";

	// Token: 0x0400000B RID: 11
	private const string ᜇ = "This method should be called for single cells only.";

	// Token: 0x0400000C RID: 12
	public const string ᜈ = "Normal";

	// Token: 0x0400000D RID: 13
	internal const int ᜉ = 15;

	// Token: 0x0400000E RID: 14
	private const bool ᜊ = false;

	// Token: 0x0400000F RID: 15
	private const char ᜋ = 'C';

	// Token: 0x04000010 RID: 16
	private const char ᜌ = 'R';

	// Token: 0x04000011 RID: 17
	private const char \u170D = '[';

	// Token: 0x04000012 RID: 18
	private const char ᜎ = ']';

	// Token: 0x04000013 RID: 19
	private const string ᜏ = "R{0}C{1}";

	// Token: 0x04000014 RID: 20
	private const long ᜐ = 31241376000000000L;

	// Token: 0x04000015 RID: 21
	private const int ᜑ = 61;

	// Token: 0x04000016 RID: 22
	private const int \u1712 = 32;

	// Token: 0x04000017 RID: 23
	private static readonly sprṔ.TCellType[] \u1713;

	// Token: 0x02000237 RID: 567
	public enum TCellType
	{
		// Token: 0x040011FB RID: 4603
		Number = 515,
		// Token: 0x040011FC RID: 4604
		RK = 638,
		// Token: 0x040011FD RID: 4605
		LabelSST = 253,
		// Token: 0x040011FE RID: 4606
		Blank = 513,
		// Token: 0x040011FF RID: 4607
		Formula = 6,
		// Token: 0x04001200 RID: 4608
		BoolErr = 517,
		// Token: 0x04001201 RID: 4609
		RString = 214,
		// Token: 0x04001202 RID: 4610
		Label = 516
	}
}
