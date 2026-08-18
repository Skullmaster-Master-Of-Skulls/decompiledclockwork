using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000276 RID: 630
internal abstract class sprἏ : ICloneable
{
	// Token: 0x06002625 RID: 9765 RVA: 0x0015E344 File Offset: 0x0015D344
	public sprἏ()
	{
	}

	// Token: 0x06002626 RID: 9766
	public abstract int ᜀ(string A_0, int A_1);

	// Token: 0x06002627 RID: 9767 RVA: 0x0015E358 File Offset: 0x0015D358
	protected int ᜀ(Regex A_0, string A_1, int A_2)
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
		Match match;
		return this.ᜀ(A_0, A_1, A_2, out match);
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x0015E3A0 File Offset: 0x0015D3A0
	protected int ᜀ(Regex A_0, string A_1, int A_2, out Match A_3)
	{
		int a_ = 11;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜁ(A_3.Value);
				A_2 += this.ᜁ.Length;
				num = 10;
				continue;
			case 2:
				goto IL_209;
			case 3:
				if (A_3.Index == A_2)
				{
					num = 1;
					continue;
				}
				return A_2;
			case 4:
			{
				int length;
				if (length == 0)
				{
					num = 2;
					continue;
				}
				num = 8;
				continue;
			}
			case 5:
			{
				int length;
				if (A_2 > length - 1)
				{
					num = 11;
					continue;
				}
				A_3 = A_0.Match(A_1, A_2);
				num = 6;
				continue;
			}
			case 6:
				if (A_3.Success)
				{
					num = 9;
					continue;
				}
				return A_2;
			case 7:
			{
				if (A_1 == null)
				{
					num = 14;
					continue;
				}
				int length = A_1.Length;
				num = 4;
				continue;
			}
			case 8:
				if (A_2 >= 0)
				{
					num = 12;
					continue;
				}
				goto IL_8B;
			case 9:
				goto IL_F5;
			case 10:
				goto IL_187;
			case 11:
				goto IL_1E1;
			case 12:
				if (true)
				{
				}
				num = 5;
				continue;
			case 13:
				goto IL_86;
			case 14:
				goto IL_F0;
			}
			if (A_0 != null)
			{
				num = 7;
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
				num = 13;
				continue;
			}
			IL_F5:
			num = 3;
		}
		IL_86:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂≄≆ㅈ", a_));
		IL_8B:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ⵂ⅄≆ㅈ", a_), RecordTableEnumerator.b("ࡀⵂ⅄≆ㅈ歊⅌⩎≐⁒畔⍖ㅘ㩚㍜罞兠䍢੤ᕦ䥨౪Ὤ੮ၰݲၴն奸ེᕼṾꎂ歷뾐", a_));
		IL_F0:
		throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
		IL_187:
		return A_2;
		IL_1E1:
		goto IL_8B;
		IL_209:
		throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄⹆❈ⱊ浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠٢ࡤᝦᵨቪ䍬", a_), RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x0015E5BC File Offset: 0x0015D5BC
	public virtual string ᜁ(ref double A_0)
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
		return this.ᜀ(ref A_0, false, null, null);
	}

	// Token: 0x0600262A RID: 9770
	public abstract string ᜀ(string A_0, bool A_1);

	// Token: 0x0600262B RID: 9771 RVA: 0x0015E604 File Offset: 0x0015D604
	public virtual string ᜂ(string A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0015E648 File Offset: 0x0015D648
	public object ᜇ()
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
		return base.MemberwiseClone();
	}

	// Token: 0x0600262D RID: 9773
	public abstract string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3);

	// Token: 0x0600262E RID: 9774 RVA: 0x0015E68C File Offset: 0x0015D68C
	public string ᜈ()
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
		return this.ᜁ;
	}

	// Token: 0x0600262F RID: 9775 RVA: 0x0015E6D0 File Offset: 0x0015D6D0
	public void ᜁ(string A_0)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_EB;
			case 2:
				if (A_0.Length == 0)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
			case 3:
				goto IL_C8;
			case 4:
				if (this.ᜁ != A_0)
				{
					num = 6;
					continue;
				}
				return;
			case 5:
				goto IL_43;
			case 6:
				this.ᜁ = A_0;
				this.ᜃ();
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 5;
			}
			else
			{
				num = 2;
			}
		}
		IL_43:
		IL_92:
		throw new ArgumentNullException(RecordTableEnumerator.b("㍄♆╈㹊⡌", a_));
		IL_C8:
		return;
		IL_EB:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_92;
		default:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("ᙄ㍆㭈≊⍌⡎煐げ㑔㥖㝘㑚⥜罞͠٢䕤ɦѨ᭪ᥬ᙮彰", a_), RecordTableEnumerator.b("㍄♆╈㹊⡌", a_));
		}
	}

	// Token: 0x06002630 RID: 9776
	internal abstract TokenType ᜀ();

	// Token: 0x06002631 RID: 9777 RVA: 0x0015E7EC File Offset: 0x0015D7EC
	public int ᜀ(string[] A_0, string A_1, int A_2, bool A_3)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				int length;
				StringComparison stringComparison;
				StringComparison comparisonType;
				switch (num)
				{
				case 0:
					if (length == 0)
					{
						num = 7;
						continue;
					}
					num = 3;
					continue;
				case 2:
				{
					if (A_2 > length - 1)
					{
						num = 10;
						continue;
					}
					int num2 = 0;
					int num3 = A_0.Length;
					num = 12;
					continue;
				}
				case 3:
					if (A_2 >= 0)
					{
						num = 16;
						continue;
					}
					goto IL_209;
				case 4:
					stringComparison = StringComparison.CurrentCulture;
					goto IL_11A;
				case 5:
					if (!A_3)
					{
						goto IL_A2;
					}
					num = 11;
					continue;
				case 6:
					goto IL_7D;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A2;
					default:
						goto IL_112;
					}
					break;
				case 8:
					goto IL_153;
				case 9:
				{
					string text;
					if (string.Compare(A_1, A_2, text, 0, text.Length, comparisonType) == 0)
					{
						num = 14;
						continue;
					}
					int num2;
					num2++;
					num = 8;
					continue;
				}
				case 10:
					goto IL_1F3;
				case 11:
					stringComparison = StringComparison.CurrentCultureIgnoreCase;
					goto IL_11A;
				case 12:
					goto IL_153;
				case 13:
				{
					int num2;
					int num3;
					if (num2 >= num3)
					{
						if (true)
						{
						}
						num = 15;
						continue;
					}
					string text = A_0[num2];
					num = 5;
					continue;
				}
				case 14:
				{
					int num2;
					return num2;
				}
				case 15:
					return -1;
				case 16:
					num = 2;
					continue;
				case 17:
					num = 4;
					continue;
				}
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				length = A_1.Length;
				num = 0;
				continue;
				IL_A2:
				num = 17;
				continue;
				IL_11A:
				comparisonType = stringComparison;
				num = 9;
				continue;
				IL_153:
				num = 13;
			}
			IL_7D:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			IL_112:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("欷丹主圽⸿╁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㽙ㅛ⹝ᑟ᭡䩣", a_), RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			IL_1F3:
			IL_209:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_), RecordTableEnumerator.b("焷吹堻嬽㠿扁⡃⍅㭇㥉汋㩍㡏㍑㩓癕桗穙㍛ⱝ䁟աᙣͥ१ṩ५ᱭ偯ٱᱳ᝵ᙷ婹ࡻᙽꒃ늑ﶙ躟", a_));
		}
		}
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x0015EA28 File Offset: 0x0015DA28
	protected virtual void ᜃ()
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
	}

	// Token: 0x040012EA RID: 4842
	protected const RegexOptions ᜀ = RegexOptions.Compiled;

	// Token: 0x040012EB RID: 4843
	protected string ᜁ;
}
