using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003AA RID: 938
internal class spr\u19EE : sprᭅ
{
	// Token: 0x060038E1 RID: 14561 RVA: 0x001FB3B4 File Offset: 0x001FA3B4
	public override int ᜀ(string A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 13;
		int num = 9;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				return A_1;
			case 1:
				goto IL_A6;
			case 2:
				num = 12;
				continue;
			case 3:
				goto IL_122;
			case 4:
				if (A_2 >= 0)
				{
					num = 2;
					continue;
				}
				goto IL_127;
			case 5:
				if (length == 0)
				{
					num = 15;
					continue;
				}
				num = 4;
				continue;
			case 6:
				if (A_2 != A_3)
				{
					num = 0;
					continue;
				}
				A_2++;
				num = 3;
				continue;
			case 7:
				goto IL_67;
			case 8:
				if (true)
				{
				}
				this.ᜆ = this.ᜀ(A_0, A_2, A_3);
				num = 13;
				continue;
			case 10:
				goto IL_239;
			case 11:
				return A_1;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
					if (false)
					{
					}
					if (A_2 > length - 1)
					{
						num = 10;
						continue;
					}
					this.ᜆ = this.ᜁ(A_0, A_2);
					num = 1;
					continue;
				}
				break;
			case 13:
				if (this.ᜆ < 0)
				{
					num = 11;
					continue;
				}
				this.ᜆ += 7;
				A_2 = A_3 + 1;
				num = 14;
				continue;
			case 14:
				goto IL_88;
			case 15:
				goto IL_10E;
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			length = A_0.Length;
			num = 5;
			continue;
			IL_A6:
			if (this.ᜆ < 0)
			{
				num = 8;
			}
			else
			{
				string text = spr\u19EE.ᜅ[this.ᜆ];
				A_2 += text.Length;
				this.ᜁ = text;
				num = 6;
			}
		}
		IL_67:
		throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌", a_));
		IL_88:
		return A_2;
		IL_10E:
		throw new ArgumentException(RecordTableEnumerator.b("၂ㅄ㕆⁈╊⩌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢d੦ᥨὪᑬ䅮", a_), RecordTableEnumerator.b("╂⩄㕆⑈⩊㥌", a_));
		IL_122:
		return A_2;
		IL_127:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂⭄⍆ⱈ㍊", a_), RecordTableEnumerator.b("ੂ⭄⍆ⱈ㍊浌⍎㑐⁒♔睖ⵘ㍚㱜ㅞ䅠卢䕤ࡦ᭨䭪੬ᵮᑰቲŴቶ୸孺ॼ᝾ꖄ꾎﶐ﮔ뎜", a_));
		IL_239:
		goto IL_127;
	}

	// Token: 0x060038E2 RID: 14562 RVA: 0x001FB600 File Offset: 0x001FA600
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
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
		return string.Empty;
	}

	// Token: 0x060038E3 RID: 14563 RVA: 0x001FB640 File Offset: 0x001FA640
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
		return string.Empty;
	}

	// Token: 0x060038E4 RID: 14564 RVA: 0x001FB680 File Offset: 0x001FA680
	internal override TokenType ᜀ()
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
		return TokenType.Color;
	}

	// Token: 0x060038E5 RID: 14565 RVA: 0x001FB6C0 File Offset: 0x001FA6C0
	private new int ᜁ(string A_0, int A_1)
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
		return base.ᜀ(spr\u19EE.ᜅ, A_0, A_1, true);
	}

	// Token: 0x060038E6 RID: 14566 RVA: 0x001FB70C File Offset: 0x001FA70C
	private new int ᜀ(string A_0, int A_1, int A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 17;
					continue;
				case 1:
				{
					bool flag;
					if (flag)
					{
						num = 14;
						continue;
					}
					return -1;
				}
				case 2:
				{
					int length;
					if (A_1 > length - 1)
					{
						num = 18;
						continue;
					}
					num = 8;
					continue;
				}
				case 3:
					goto IL_2D9;
				case 4:
				{
					int length2;
					int num2 = A_1 + length2;
					string s = A_0.Substring(num2, A_2 - num2);
					double num3;
					bool flag = double.TryParse(s, NumberStyles.Integer, null, out num3);
					num = 1;
					continue;
				}
				case 5:
				{
					int length2;
					if (string.Compare(A_0, A_1, RecordTableEnumerator.b("ɀⱂ⥄⡆㭈", a_), 0, length2, StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						num = 4;
						continue;
					}
					return -1;
				}
				case 6:
					goto IL_16D;
				case 8:
					if (A_2 >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_CC;
				case 9:
				{
					int length;
					if (length == 0)
					{
						num = 6;
						continue;
					}
					num = 12;
					continue;
				}
				case 10:
					goto IL_131;
				case 11:
					goto IL_88;
				case 12:
					if (A_1 >= 0)
					{
						num = 19;
						continue;
					}
					goto IL_296;
				case 13:
					num = 16;
					continue;
				case 14:
				{
					double num3;
					int num4 = (int)num3;
					num = 15;
					continue;
				}
				case 15:
				{
					int num4;
					if (num4 >= 1)
					{
						num = 13;
						continue;
					}
					goto IL_208;
				}
				case 16:
				{
					int num4;
					if (num4 > 56)
					{
						num = 10;
						continue;
					}
					return num4;
				}
				case 17:
				{
					int length;
					if (A_2 > length - 1)
					{
						num = 3;
						continue;
					}
					int length2 = RecordTableEnumerator.b("ɀⱂ⥄⡆㭈", a_).Length;
					num = 5;
					continue;
				}
				case 18:
					goto IL_280;
				case 19:
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 11;
				}
				else
				{
					if (true)
					{
					}
					int length = A_0.Length;
					num = 9;
				}
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_CC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⑀ⵂ⅄ๆ❈⽊⡌㝎", a_), RecordTableEnumerator.b("рⵂ⅄ๆ❈⽊⡌㝎煐㽒ご⑖⩘筚⥜㝞`ൢ䕤坦䥨੪ͬ୮兰ᑲݴቶᡸེ᡼ൾꆀꮊﾐﾖ래", a_));
			IL_131:
			goto IL_208;
			IL_16D:
			throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㝄⹆❈ⱊ浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠٢ࡤᝦᵨቪ䍬", a_), RecordTableEnumerator.b("❀ⱂ㝄⩆⡈㽊", a_));
			IL_208:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				int num4;
				return num4;
			}
			default:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ɀⱂ⥄⡆㭈歊⑌ⅎ㕐㙒ⵔ", a_));
			}
			IL_280:
			IL_296:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ⵂ⅄≆ㅈ", a_), RecordTableEnumerator.b("ࡀⵂ⅄≆ㅈ歊⅌⩎≐⁒畔⍖ㅘ㩚㍜罞兠䍢੤ᕦ䥨౪Ὤ੮ၰݲၴն奸ེᕼṾꎂ歷뾐", a_));
			IL_2D9:
			goto IL_CC;
		}
		}
	}

	// Token: 0x060038E7 RID: 14567 RVA: 0x001FB9F8 File Offset: 0x001FA9F8
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u19EE()
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
		spr\u19EE.ᜄ = new Regex(RecordTableEnumerator.b("昼簾⹀⽂⩄㕆楈၊経扎桐๒織੖", a_));
		spr\u19EE.ᜅ = new string[]
		{
			RecordTableEnumerator.b("缼匾⁀⁂⹄", a_),
			RecordTableEnumerator.b("樼圾⡀㝂⁄", a_),
			RecordTableEnumerator.b("漼娾╀", a_),
			RecordTableEnumerator.b("稼䴾⑀♂⭄", a_),
			RecordTableEnumerator.b("缼匾㑀♂", a_),
			RecordTableEnumerator.b("搼娾ⵀ⽂⩄う", a_),
			RecordTableEnumerator.b("瀼帾♀♂⭄㍆⡈", a_),
			RecordTableEnumerator.b("縼䘾⁀ⵂ", a_)
		};
	}

	// Token: 0x04001903 RID: 6403
	private new const string ᜀ = "Color";

	// Token: 0x04001904 RID: 6404
	private new const int ᜁ = 1;

	// Token: 0x04001905 RID: 6405
	private new const int ᜂ = 56;

	// Token: 0x04001906 RID: 6406
	private new const int ᜃ = 7;

	// Token: 0x04001907 RID: 6407
	private static readonly Regex ᜄ;

	// Token: 0x04001908 RID: 6408
	private static readonly string[] ᜅ;

	// Token: 0x04001909 RID: 6409
	private int ᜆ = -1;
}
