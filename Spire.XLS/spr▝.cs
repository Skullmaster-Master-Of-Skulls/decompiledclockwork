using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000345 RID: 837
internal class spr\u259D : sprἏ
{
	// Token: 0x06003312 RID: 13074 RVA: 0x001D4218 File Offset: 0x001D3218
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 19;
		int num = 8;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				if (A_1 >= 0)
				{
					num = 0;
					continue;
				}
				goto IL_58;
			case 2:
				goto IL_9B;
			case 3:
				goto IL_BD;
			case 4:
				if (this.ᜂ < 0)
				{
					num = 6;
					continue;
				}
				goto IL_16D;
			case 5:
				if (true)
				{
				}
				if (A_1 > length - 1)
				{
					num = 2;
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
					this.ᜂ = base.ᜀ(spr\u259D.ᜁ, A_0, A_1, false);
					num = 4;
					continue;
				}
				break;
			case 6:
				return A_1;
			case 7:
				goto IL_56;
			case 9:
				if (length == 0)
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			}
			IL_4B:
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			length = A_0.Length;
			num = 9;
			continue;
			goto IL_4B;
		}
		IL_56:
		throw new ArgumentNullException(RecordTableEnumerator.b("⽈⑊㽌≎ぐ❒", a_));
		IL_58:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁈╊⥌⩎⥐", a_), RecordTableEnumerator.b("H╊⥌⩎⥐獒㥔㉖⩘⡚絜⭞ॠɢ୤䝦奨䭪౬Ůᕰ卲ቴնᱸ᩺ॼ᩾ꎂ권ﶒ떚", a_));
		IL_9B:
		goto IL_58;
		IL_BD:
		throw new ArgumentException(RecordTableEnumerator.b("ᩈ㽊㽌♎㽐㑒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ孴", a_), RecordTableEnumerator.b("⽈⑊㽌≎ぐ❒", a_));
		IL_16D:
		return A_1 + spr\u259D.ᜁ[this.ᜂ].Length;
	}

	// Token: 0x06003313 RID: 13075 RVA: 0x001D43A8 File Offset: 0x001D33A8
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
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
					break;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				if (A_0 >= 0.0)
				{
					num = 3;
					continue;
				}
				goto IL_90;
			case 3:
				goto IL_8E;
			}
			IL_29:
			if (this.ᜂ == 0)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			goto IL_90;
			goto IL_29;
		}
		IL_8E:
		return spr\u259D.ᜁ[0];
		IL_90:
		return RecordTableEnumerator.b("猵", a_);
	}

	// Token: 0x06003314 RID: 13076 RVA: 0x001D4454 File Offset: 0x001D3454
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

	// Token: 0x06003315 RID: 13077 RVA: 0x001D4494 File Offset: 0x001D3494
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
		return TokenType.Scientific;
	}

	// Token: 0x06003317 RID: 13079 RVA: 0x001D44F0 File Offset: 0x001D34F0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u259D()
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u259D.ᜁ = new string[]
		{
			RecordTableEnumerator.b("Ɇ扈", a_),
			RecordTableEnumerator.b("Ɇ摈", a_)
		};
	}

	// Token: 0x04001648 RID: 5704
	private new const string ᜀ = "E";

	// Token: 0x04001649 RID: 5705
	private new static readonly string[] ᜁ;

	// Token: 0x0400164A RID: 5706
	private new int ᜂ = -1;
}
