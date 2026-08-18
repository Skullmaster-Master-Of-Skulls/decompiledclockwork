using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;

// Token: 0x020001C1 RID: 449
internal class spr\u23B0
{
	// Token: 0x06001305 RID: 4869 RVA: 0x00138AB8 File Offset: 0x00137AB8
	private spr\u23B0()
	{
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x00138ACC File Offset: 0x00137ACC
	internal static Color ᜁ(string A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			Match match;
			string value2;
			string value3;
			string value4;
			int green;
			int red;
			int alpha;
			StringBuilder stringBuilder;
			for (;;)
			{
				match = spr\u23B0.ᜈ.Match(A_0);
				string value = match.Groups[2].Value;
				value2 = match.Groups[8].Value;
				value3 = match.Groups[6].Value;
				value4 = match.Groups[10].Value;
				string value5 = match.Groups[12].Value;
				int num = 22;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_643;
					case 1:
						num = 41;
						continue;
					case 2:
					{
						string value6;
						if ((value6 = match.Groups[4].Value) != null)
						{
							num = 21;
							continue;
						}
						goto IL_5C9;
					}
					case 3:
						goto IL_506;
					case 4:
						num = 31;
						continue;
					case 5:
					{
						string value6;
						if (!(value6 == ClipboardData.b("१๩࡫", a_)))
						{
							num = 29;
							continue;
						}
						green = 3;
						num = 44;
						continue;
					}
					case 6:
					{
						string value7;
						if (!(value7 == ClipboardData.b("ѧͩɫ୭㽯q㉳ήᑷᙹ", a_)))
						{
							num = 7;
							continue;
						}
						red = 241;
						num = 16;
						continue;
					}
					case 7:
						num = 17;
						continue;
					case 8:
					{
						string value6;
						if (!(value6 == ClipboardData.b("ѧͩ୫٭ѯ᝱ᩳ", a_)))
						{
							num = 30;
							continue;
						}
						green = 2;
						num = 0;
						continue;
					}
					case 9:
					{
						string value6;
						if (!(value6 == ClipboardData.b("౧୩ṫխᕯᱱ", a_)))
						{
							num = 25;
							continue;
						}
						green = 1;
						num = 15;
						continue;
					}
					case 10:
						if (spr\u1CC6.ᜋ(value3))
						{
							num = 37;
							continue;
						}
						num = 42;
						continue;
					case 11:
						if (value2 == ClipboardData.b("ὧͩɫ੭Ὧձ⁳፵w๹", a_))
						{
							num = 36;
							continue;
						}
						num = 23;
						continue;
					case 12:
						goto IL_5F8;
					case 13:
						num = 6;
						continue;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_423;
						default:
							if (false)
							{
							}
							goto IL_356;
						}
						break;
					case 15:
						goto IL_3DE;
					case 16:
						goto IL_356;
					case 17:
					{
						string value7;
						if (!(value7 == ClipboardData.b("ѧͩɫ୭", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_423;
					}
					case 18:
						goto IL_1A2;
					case 19:
					{
						string value7;
						if (!(value7 == ClipboardData.b("๧ͩkɭ", a_)))
						{
							num = 13;
							continue;
						}
						red = 240;
						num = 24;
						continue;
					}
					case 20:
						if (spr\u1CC6.ᜋ(value5))
						{
							num = 33;
							continue;
						}
						goto IL_676;
					case 21:
						num = 9;
						continue;
					case 22:
						if (spr\u1CC6.ᜋ(value))
						{
							num = 32;
							continue;
						}
						num = 35;
						continue;
					case 23:
						if (value2 == ClipboardData.b("ᱧɩիᵭ", a_))
						{
							num = 18;
							continue;
						}
						goto IL_391;
					case 24:
						goto IL_356;
					case 25:
						num = 8;
						continue;
					case 26:
						goto IL_20B;
					case 27:
						goto IL_5D5;
					case 28:
						goto IL_5D5;
					case 29:
						num = 40;
						continue;
					case 30:
						num = 5;
						continue;
					case 31:
					{
						string value7;
						if (!(value7 == ClipboardData.b("᭧ɩ൫੭Ὧձ", a_)))
						{
							num = 1;
							continue;
						}
						red = 243;
						num = 45;
						continue;
					}
					case 32:
						alpha = 239;
						num = 46;
						continue;
					case 33:
					{
						stringBuilder = new StringBuilder(6);
						int num2 = 0;
						num = 27;
						continue;
					}
					case 34:
					{
						int num2;
						if (num2 >= value5.Length)
						{
							num = 12;
							continue;
						}
						char value8 = value5[num2];
						stringBuilder.Append(value8);
						stringBuilder.Append(value8);
						num2++;
						num = 28;
						continue;
					}
					case 35:
						if (spr\u1CC6.ᜋ(value2))
						{
							num = 39;
							continue;
						}
						num = 10;
						continue;
					case 36:
						goto IL_57D;
					case 37:
						goto IL_66B;
					case 38:
						if (value2 == ClipboardData.b("ὧͩɫ੭Ὧձ", a_))
						{
							num = 3;
							continue;
						}
						num = 11;
						continue;
					case 39:
						num = 38;
						continue;
					case 40:
						goto IL_2EE;
					case 41:
						goto IL_351;
					case 42:
						if (spr\u1CC6.ᜋ(value4))
						{
							num = 26;
							continue;
						}
						if (true)
						{
						}
						num = 20;
						continue;
					case 43:
						num = 19;
						continue;
					case 44:
						goto IL_591;
					case 45:
						goto IL_356;
					case 46:
					{
						string value7;
						if ((value7 = match.Groups[3].Value) != null)
						{
							num = 43;
							continue;
						}
						goto IL_45E;
					}
					}
					break;
					IL_356:
					num = 2;
					continue;
					IL_423:
					red = 242;
					num = 14;
					continue;
					IL_5D5:
					num = 34;
				}
			}
			IL_1A2:
			return Color.Empty;
			IL_20B:
			return spr\u25D1.ᜄ(value4);
			IL_266:
			int blue = sprᜌ.ᜆ(match.Groups[5].Value);
			return Color.FromArgb(alpha, red, green, blue);
			IL_2EE:
			goto IL_5C9;
			IL_351:
			goto IL_45E;
			IL_391:
			return sprᲜ.ᜀ(value2);
			IL_3DE:
			goto IL_266;
			IL_45E:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
			IL_506:
			return Color.FromArgb(239, 17, 0, 0);
			IL_57D:
			return Color.Black;
			IL_591:
			goto IL_266;
			IL_5C9:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
			IL_5F8:
			return spr\u25D1.ᜄ(stringBuilder.ToString());
			IL_643:
			goto IL_266;
			IL_66B:
			return spr\u25D1.ᜄ(value3);
			IL_676:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
		}
		}
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x0013915C File Offset: 0x0013815C
	internal static string ᜁ(Color A_0)
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
		return spr\u23B0.ᜀ(A_0, true, false);
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x001391A0 File Offset: 0x001381A0
	internal static string ᜀ(Color A_0, bool A_1, bool A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 29;
			StringBuilder stringBuilder;
			string text2;
			for (;;)
			{
				byte g;
				switch (num)
				{
				case 0:
					if (A_0.A == 255)
					{
						num = 36;
						continue;
					}
					goto IL_52F;
				case 1:
				{
					byte r;
					if (r == 17)
					{
						num = 38;
						continue;
					}
					goto IL_17F;
				}
				case 2:
					goto IL_3F9;
				case 3:
					switch (g)
					{
					case 1:
						stringBuilder.Append(ClipboardData.b("ᝲᑴնቸṺ፼", a_));
						num = 34;
						continue;
					case 2:
						stringBuilder.Append(ClipboardData.b("ὲᱴၶᅸེ᡼ᅾ", a_));
						num = 11;
						continue;
					case 3:
						stringBuilder.Append(ClipboardData.b("ቲᅴ፶", a_));
						num = 5;
						continue;
					default:
						num = 26;
						continue;
					}
					break;
				case 4:
				{
					string text;
					if (text != "")
					{
						num = 21;
						continue;
					}
					num = 33;
					continue;
				}
				case 5:
					goto IL_1C3;
				case 6:
					goto IL_2E4;
				case 7:
				{
					if (A_0.R >= 240)
					{
						num = 32;
						continue;
					}
					byte r = A_0.R;
					num = 35;
					continue;
				}
				case 8:
					goto IL_E0;
				case 9:
					num = 0;
					continue;
				case 10:
					goto IL_3F9;
				case 11:
					goto IL_1C3;
				case 12:
					num = 24;
					continue;
				case 13:
				{
					if (A_0.A == 239)
					{
						num = 15;
						continue;
					}
					string text = sprᲜ.ᜀ(A_0);
					num = 4;
					continue;
				}
				case 14:
					num = 37;
					continue;
				case 15:
					stringBuilder = new StringBuilder();
					num = 7;
					continue;
				case 16:
					goto IL_1F0;
				case 17:
					goto IL_358;
				case 18:
					if (A_2)
					{
						num = 27;
						continue;
					}
					return text2;
				case 19:
					if (spr\u23B0.ᜀ((int)A_0.R))
					{
						num = 14;
						continue;
					}
					goto IL_52F;
				case 20:
					goto IL_3F9;
				case 21:
				{
					string text;
					return text;
				}
				case 22:
				{
					byte r2;
					switch (r2)
					{
					case 240:
						stringBuilder.Append(ClipboardData.b("ᕲᱴ᭶ᕸ孺", a_));
						num = 20;
						continue;
					case 241:
						stringBuilder.Append(ClipboardData.b("ὲᱴ᥶ᱸ㑺ོ㥾Ꞇ", a_));
						num = 10;
						continue;
					case 242:
						stringBuilder.Append(ClipboardData.b("ὲᱴ᥶ᱸ孺", a_));
						num = 2;
						continue;
					case 243:
						stringBuilder.Append(ClipboardData.b("rᵴᙶᵸᑺ੼彾", a_));
						num = 30;
						continue;
					case 244:
						goto IL_434;
					case 245:
					case 246:
						goto IL_35D;
					case 247:
						goto IL_2B9;
					default:
						num = 28;
						continue;
					}
					break;
				}
				case 23:
					goto IL_17A;
				case 24:
					if (spr\u23B0.ᜀ((int)A_0.B))
					{
						num = 6;
						continue;
					}
					goto IL_52F;
				case 25:
					num = 1;
					continue;
				case 26:
					num = 31;
					continue;
				case 27:
					goto IL_582;
				case 28:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45E;
					default:
						if (false)
						{
						}
						num = 23;
						continue;
					}
					break;
				case 30:
					goto IL_3F9;
				case 31:
					goto IL_259;
				case 32:
				{
					byte r2 = A_0.R;
					num = 22;
					continue;
				}
				case 33:
					if (A_1)
					{
						num = 9;
						continue;
					}
					goto IL_52F;
				case 34:
					goto IL_1C3;
				case 35:
				{
					byte r;
					if (r != 1)
					{
						num = 25;
						continue;
					}
					goto IL_45C;
				}
				case 36:
					if (true)
					{
					}
					num = 19;
					continue;
				case 37:
					if (spr\u23B0.ᜀ((int)A_0.G))
					{
						num = 12;
						continue;
					}
					goto IL_52F;
				case 38:
					stringBuilder.Append(ClipboardData.b("Ѳᱴ᥶ᵸᑺ੼", a_));
					num = 17;
					continue;
				}
				if (A_0.IsEmpty)
				{
					num = 8;
					continue;
				}
				goto IL_45E;
				IL_1C3:
				stringBuilder.AppendFormat(ClipboardData.b("孲๴䝶Ѹ剺", a_), A_0.B);
				num = 16;
				continue;
				IL_3F9:
				g = A_0.G;
				num = 3;
				continue;
				IL_45E:
				num = 13;
				continue;
				IL_52F:
				text2 = string.Format(ClipboardData.b("偲๴䝶Ѹz䱼ɾ婢놂", a_), spr\u23B0.ᜂ((int)A_0.R), spr\u23B0.ᜂ((int)A_0.G), spr\u23B0.ᜂ((int)A_0.B));
				num = 18;
			}
			IL_E0:
			return ClipboardData.b("ݲᵴṶ੸", a_);
			IL_E5:
			return stringBuilder.ToString();
			IL_17A:
			goto IL_35D;
			IL_17F:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
			IL_1F0:
			goto IL_E5;
			IL_259:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
			IL_2B9:
			return null;
			IL_2E4:
			return string.Format(ClipboardData.b("偲๴䝶Ѹz䱼ɾ婢놂", a_), spr\u23B0.ᜁ((int)A_0.R), spr\u23B0.ᜁ((int)A_0.G), spr\u23B0.ᜁ((int)A_0.B));
			IL_358:
			goto IL_E5;
			IL_35D:
			throw new InvalidOperationException(spr\u23B0.ᜀ(A_0));
			IL_434:
			return null;
			IL_45C:
			return null;
			IL_582:
			return text2.ToUpper();
		}
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0013976C File Offset: 0x0013876C
	private static string ᜀ(Color A_0)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return string.Format(ClipboardData.b("㡬Ůᩰᵲᩴv᝸孺ṼၾꞆﶈﶌ놐ﮔ쒠톢삤쎦覨쾪\ud8ac\uddae\ud8b0\uddb2튴鞶풺쾼\udbbe賀迂ꋆ뇈믊ꋌ뷎ꗐꋘꃜ", a_), A_0.ToString());
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x001397D0 File Offset: 0x001387D0
	private static string ᜀ(string A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("❱ᩳᵵᙷᕹ୻ၽꁿ겋揄뚕ﶗﾛ햟첡킣쎥\udaa7쾩좫躭풯잱욳\udfb5횷\uddb9鲻꾿냁ꃃ诅蓇ꗋꏍꃏ뷑ꛓꋕﳛꗝ탟鿡쫣", a_), A_0);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x00139828 File Offset: 0x00138828
	private static string ᜂ(int A_0)
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
		return sprᜌ.ᜃ(A_0);
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x0013986C File Offset: 0x0013886C
	private static string ᜁ(int A_0)
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
		return sprᜌ.ᜅ(A_0 % 16);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x001398B0 File Offset: 0x001388B0
	private static bool ᜀ(int A_0)
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
		return A_0 / 16 == A_0 % 16;
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x001398F8 File Offset: 0x001388F8
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u23B0()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u23B0.ᜈ = new Regex(ClipboardData.b("⩳幵偷剹❻ώ굿\ud983궅ꆇꪉꒋ햍뾑쮕뎗뎙삛뚝袟ﺡ삣趥膧薫螭첯骱蚵閷莹\uddbb鎽ꚿ鿁뿃뗇냋觑뗓ﯕꋗ蟙컟죡췣髥샧짩쓫뗭샯\udff1췳韵헷鳹ꇻ藽㛿缁ⴃ⠅∇⌉瀋☍㌏㨑伓☕㔗⌙紛㌝䘟缡弣ᔥ唧̩ȫЭ᤯ᬱဳ", a_), RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}

	// Token: 0x0400189B RID: 6299
	private const int ᜀ = 2;

	// Token: 0x0400189C RID: 6300
	private const int ᜁ = 3;

	// Token: 0x0400189D RID: 6301
	private const int ᜂ = 4;

	// Token: 0x0400189E RID: 6302
	private const int ᜃ = 5;

	// Token: 0x0400189F RID: 6303
	private const int ᜄ = 6;

	// Token: 0x040018A0 RID: 6304
	private const int ᜅ = 8;

	// Token: 0x040018A1 RID: 6305
	private const int ᜆ = 10;

	// Token: 0x040018A2 RID: 6306
	private const int ᜇ = 12;

	// Token: 0x040018A3 RID: 6307
	private static readonly Regex ᜈ;
}
