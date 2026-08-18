using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Utils.Text;

// Token: 0x0200038A RID: 906
internal class sprᩁ
{
	// Token: 0x06003278 RID: 12920 RVA: 0x002E7290 File Offset: 0x002E6290
	public sprᩁ(string A_0)
	{
		int a_ = 8;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ᩭᕯੱs", a_));
		}
		this.ᜆ = A_0;
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x002E72CC File Offset: 0x002E62CC
	private int ᜀ()
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
		return this.ᜆ.Length;
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x002E7314 File Offset: 0x002E6314
	public int ᜂ()
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
		return this.ᜇ;
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x002E7358 File Offset: 0x002E6358
	public void ᜀ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜇ = A_0;
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x002E739C File Offset: 0x002E639C
	public string ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = this.ᜇ;
				bool flag = false;
				int num2 = 40;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (this.ᜆ[num3] == '\n')
						{
							num2 = 28;
							continue;
						}
						goto IL_21F;
					}
					case 1:
						num2 = 30;
						continue;
					case 2:
					{
						Separator separator;
						if (separator != Separator.InitialQuotePunctuation)
						{
							num2 = 3;
							continue;
						}
						goto IL_248;
					}
					case 3:
						flag = false;
						num2 = 17;
						continue;
					case 4:
					{
						char c;
						if (c == '\r')
						{
							num2 = 1;
							continue;
						}
						goto IL_21F;
					}
					case 5:
						if (num4 < this.ᜀ())
						{
							num2 = 34;
							continue;
						}
						goto IL_3F6;
					case 6:
						goto IL_572;
					case 7:
					{
						if (num >= this.ᜀ())
						{
							num2 = 14;
							continue;
						}
						char c = this.ᜆ[num];
						char c2 = c;
						num2 = 27;
						continue;
					}
					case 8:
						num2 = 11;
						continue;
					case 9:
						if (true)
						{
						}
						if (!flag)
						{
							num2 = 33;
							continue;
						}
						goto IL_1F0;
					case 10:
						goto IL_398;
					case 11:
						goto IL_146;
					case 12:
						if (!flag)
						{
							num2 = 21;
							continue;
						}
						goto IL_146;
					case 13:
						goto IL_146;
					case 14:
						num2 = 32;
						continue;
					case 15:
						if (num > this.ᜇ)
						{
							num2 = 24;
							continue;
						}
						goto IL_1F0;
					case 16:
					{
						Separator separator2;
						switch (separator2)
						{
						case Separator.Separator:
							goto IL_455;
						case Separator.LineBreakSeparator:
							goto IL_146;
						case Separator.InitialQuotePunctuation:
							num2 = 31;
							continue;
						case Separator.G7FFLetter:
							num2 = 15;
							continue;
						default:
							num2 = 8;
							continue;
						}
						break;
					}
					case 17:
						goto IL_248;
					case 18:
					{
						char a_;
						if (sprᩁ.ᜁ(a_) == Separator.Separator)
						{
							num2 = 20;
							continue;
						}
						goto IL_3F6;
					}
					case 19:
						num2 = 10;
						continue;
					case 20:
						num++;
						num2 = 29;
						continue;
					case 21:
						goto IL_F3;
					case 22:
						goto IL_344;
					case 23:
						goto IL_1C7;
					case 24:
						num2 = 9;
						continue;
					case 25:
						num++;
						num2 = 22;
						continue;
					case 26:
						num2 = 0;
						continue;
					case 27:
					{
						char c2;
						switch (c2)
						{
						case '\t':
							goto IL_47E;
						case '\n':
						case '\r':
						{
							int num3 = num + 1;
							num2 = 4;
							continue;
						}
						case '\v':
						case '\f':
							goto IL_398;
						default:
							num2 = 39;
							continue;
						}
						break;
					}
					case 28:
						num++;
						num2 = 37;
						continue;
					case 29:
						goto IL_54C;
					case 30:
					{
						int num3;
						if (num3 < this.ᜀ())
						{
							num2 = 26;
							continue;
						}
						goto IL_21F;
					}
					case 31:
						if (num > this.ᜇ)
						{
							num2 = 38;
							continue;
						}
						flag = true;
						num2 = 13;
						continue;
					case 32:
						if (num > this.ᜇ)
						{
							num2 = 6;
							continue;
						}
						goto IL_577;
					case 33:
						goto IL_4CA;
					case 34:
					{
						char a_ = this.ᜆ[num4];
						num2 = 18;
						continue;
					}
					case 35:
					{
						char c2;
						if (c2 != ' ')
						{
							num2 = 19;
							continue;
						}
						goto IL_47E;
					}
					case 36:
						if (num == this.ᜇ)
						{
							num2 = 25;
							continue;
						}
						goto IL_41F;
					case 37:
						goto IL_141;
					case 38:
						num2 = 12;
						continue;
					case 39:
						num2 = 35;
						continue;
					case 40:
						goto IL_1C7;
					}
					break;
					IL_146:
					num2 = 2;
					continue;
					IL_1C7:
					num2 = 7;
					continue;
					IL_1F0:
					num4 = num + 1;
					num2 = 5;
					continue;
					IL_248:
					num++;
					num2 = 23;
					continue;
					IL_398:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_577;
					default:
					{
						if (false)
						{
						}
						char c;
						Separator separator = sprᩁ.ᜁ(c);
						Separator separator2 = separator;
						num2 = 16;
						continue;
					}
					}
					IL_47E:
					num2 = 36;
				}
			}
			IL_F3:
			string result = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result;
			IL_141:
			IL_21F:
			string result2 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ + 1);
			this.ᜇ = num + 1;
			return result2;
			IL_344:
			goto IL_41F;
			IL_3F6:
			num++;
			string result3 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result3;
			IL_41F:
			string result4 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result4;
			IL_455:
			num++;
			string result5 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result5;
			IL_4CA:
			string result6 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result6;
			IL_54C:
			goto IL_3F6;
			IL_572:
			string result7 = this.ᜆ.Substring(this.ᜇ, num - this.ᜇ);
			this.ᜇ = num;
			return result7;
			IL_577:
			return null;
		}
		}
	}

	// Token: 0x0600327D RID: 12925 RVA: 0x002E7924 File Offset: 0x002E6924
	public string ᜃ()
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
		int num = this.ᜇ;
		string result = this.ᜁ();
		this.ᜇ = num;
		return result;
	}

	// Token: 0x0600327E RID: 12926 RVA: 0x002E7978 File Offset: 0x002E6978
	internal static Separator ᜁ(char A_0)
	{
		int num = 2;
		for (;;)
		{
			UnicodeCategory unicodeCategory;
			switch (num)
			{
			case 0:
				return Separator.None;
			case 1:
				num = 4;
				continue;
			case 3:
				goto IL_B9;
			case 4:
				if (true)
				{
				}
				if (unicodeCategory == UnicodeCategory.OpenPunctuation)
				{
					num = 7;
					continue;
				}
				return Separator.Separator;
			case 5:
				num = 6;
				continue;
			case 6:
				if (A_0 < 'ࠀ')
				{
					num = 0;
					continue;
				}
				return Separator.G7FFLetter;
			case 7:
				goto IL_5F;
			}
			if (char.IsLetterOrDigit(A_0))
			{
				num = 5;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				unicodeCategory = char.GetUnicodeCategory(A_0);
				num = 3;
				continue;
			}
			IL_B9:
			if (unicodeCategory == UnicodeCategory.InitialQuotePunctuation)
			{
				break;
			}
			num = 1;
		}
		IL_5F:
		return Separator.InitialQuotePunctuation;
	}

	// Token: 0x0600327F RID: 12927 RVA: 0x002E7A54 File Offset: 0x002E6A54
	internal static bool ᜀ(char A_0)
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
		return sprᩁ.ᜁ(A_0) != Separator.None;
	}

	// Token: 0x06003280 RID: 12928 RVA: 0x002E7A9C File Offset: 0x002E6A9C
	// Note: this type is marked as 'beforefieldinit'.
	static sprᩁ()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᩁ.ᜄ = new char[]
		{
			' ',
			'\t'
		};
		sprᩁ.ᜅ = new Regex(ClipboardData.b("❸⁺嵼⍾\ude82꺄ꎆ", a_), RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}

	// Token: 0x040027E8 RID: 10216
	public const char ᜀ = ' ';

	// Token: 0x040027E9 RID: 10217
	public const char ᜁ = '\t';

	// Token: 0x040027EA RID: 10218
	private const RegexOptions ᜂ = RegexOptions.IgnoreCase | RegexOptions.Compiled;

	// Token: 0x040027EB RID: 10219
	private const string ᜃ = "^[ \\t]+$";

	// Token: 0x040027EC RID: 10220
	public static readonly char[] ᜄ;

	// Token: 0x040027ED RID: 10221
	private static Regex ᜅ;

	// Token: 0x040027EE RID: 10222
	private string ᜆ;

	// Token: 0x040027EF RID: 10223
	private int ᜇ;
}
