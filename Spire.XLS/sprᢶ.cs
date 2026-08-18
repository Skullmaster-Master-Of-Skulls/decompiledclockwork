using System;
using System.Globalization;
using System.Threading;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002F9 RID: 761
internal class sprᢶ : sprἏ
{
	// Token: 0x06002EFE RID: 12030 RVA: 0x001A3DA4 File Offset: 0x001A2DA4
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 12;
		int num = 10;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				goto IL_D2;
			case 1:
				if (string.Compare(A_0, A_1, RecordTableEnumerator.b("́ृ楅ᡇ݉", a_), 0, sprᢶ.ᜅ, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					num = 5;
					continue;
				}
				return A_1;
			case 2:
				goto IL_BE;
			case 3:
				num = 8;
				continue;
			case 4:
				goto IL_E3;
			case 5:
				this.ᜁ = RecordTableEnumerator.b("́ृ楅ᡇ݉", a_);
				A_1 += sprᢶ.ᜅ;
				num = 9;
				continue;
			case 6:
				goto IL_76;
			case 7:
				if (A_1 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_7B;
			case 8:
				if (A_1 > length - 1)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			case 9:
				return A_1;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				}
				if (false)
				{
				}
				break;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			length = A_0.Length;
			num = 0;
			continue;
			IL_D2:
			if (length == 0)
			{
				num = 4;
			}
			else
			{
				num = 7;
			}
		}
		IL_76:
		throw new ArgumentNullException(RecordTableEnumerator.b("⑁⭃㑅╇⭉㡋", a_));
		IL_7B:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁ൃ⡅ⱇ⽉㑋", a_), RecordTableEnumerator.b("ᑁ╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟๡ţᕥ᭧䩩ᡫ٭ᅯᱱ味䙵塷᭹ቻ᩽ꁿﺉﲍ낏ﲓ몙솟첡蒣삥잧\ud8a9솫쾭쒯銱\ud8b3펵횷\uddb9좻횽", a_));
		IL_BE:
		goto IL_7B;
		IL_E3:
		throw new ArgumentException(RecordTableEnumerator.b("ᅁぃ㑅ⅇ⑉⭋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ţ୥ᡧṩᕫ䁭", a_), RecordTableEnumerator.b("⑁⭃㑅╇⭉㡋", a_));
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x001A3F58 File Offset: 0x001A2F58
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			int hour = DateTime.FromOADate(A_0).Hour;
			if (hour <= 12)
			{
				return A_2.DateTimeFormat.AMDesignator;
			}
			break;
		}
		}
		return A_2.DateTimeFormat.PMDesignator;
	}

	// Token: 0x06002F00 RID: 12032 RVA: 0x001A3FC4 File Offset: 0x001A2FC4
	public override string ᜀ(string A_0, bool A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002F01 RID: 12033 RVA: 0x001A4004 File Offset: 0x001A3004
	internal new static string ᜀ(string A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				int num3;
				bool flag;
				int num4;
				bool flag2;
				CultureInfo currentCulture;
				switch (num)
				{
				case 0:
					num = 11;
					continue;
				case 1:
					if (num2 == 0)
					{
						num = 4;
						continue;
					}
					goto IL_9F;
				case 2:
					if (true)
					{
					}
					if (num3 == 0)
					{
						num = 10;
						continue;
					}
					goto IL_9F;
				case 3:
					goto IL_6D;
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_13E;
				case 6:
					if (flag)
					{
						num = 12;
						continue;
					}
					return A_0;
				case 8:
					if (A_0.Contains(RecordTableEnumerator.b("䠻䨽", a_)))
					{
						num = 0;
						continue;
					}
					return A_0;
				case 9:
					flag2 = (num4 != 0);
					goto IL_147;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_0;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 11:
					if (currentCulture.DateTimeFormat.ShortTimePattern.Contains(RecordTableEnumerator.b("䠻䨽", a_)))
					{
						num = 5;
						continue;
					}
					return A_0;
				case 12:
					num = 8;
					continue;
				case 13:
					flag2 = true;
					goto IL_147;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				currentCulture = Thread.CurrentThread.CurrentCulture;
				num2 = new sprᩆ().ᜀ(A_0, 0);
				num3 = new sprᡥ().ᜀ(A_0, num2);
				num4 = new spr\u173F().ᜀ(A_0, num3);
				num = 1;
				continue;
				IL_9F:
				num = 13;
				continue;
				IL_147:
				flag = flag2;
				num = 6;
			}
			IL_6D:
			throw new ArgumentNullException(A_0);
			IL_13E:
			return A_0.Replace(RecordTableEnumerator.b("䠻䨽", a_), RecordTableEnumerator.b("紻猽漿ቁृ", a_));
		}
		}
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x001A4208 File Offset: 0x001A3208
	internal override TokenType ᜀ()
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
		return TokenType.AmPm;
	}

	// Token: 0x06002F03 RID: 12035 RVA: 0x001A4248 File Offset: 0x001A3248
	// Note: this type is marked as 'beforefieldinit'.
	static sprᢶ()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprᢶ.ᜅ = RecordTableEnumerator.b("簼爾湀ፂࡄ", a_).Length;
	}

	// Token: 0x04001519 RID: 5401
	private new const string ᜀ = "tt";

	// Token: 0x0400151A RID: 5402
	private new const string ᜁ = "AM/PM";

	// Token: 0x0400151B RID: 5403
	private new const int ᜂ = 12;

	// Token: 0x0400151C RID: 5404
	private new const string ᜃ = "AM";

	// Token: 0x0400151D RID: 5405
	private const string ᜄ = "PM";

	// Token: 0x0400151E RID: 5406
	private static readonly int ᜅ;
}
