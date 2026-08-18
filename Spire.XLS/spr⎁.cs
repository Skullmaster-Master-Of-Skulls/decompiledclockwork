using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000347 RID: 839
internal class spr\u2381 : sprἏ
{
	// Token: 0x0600331E RID: 13086 RVA: 0x001D47CC File Offset: 0x001D37CC
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 14;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_70;
			case 1:
				if (A_0[A_1] == '\\')
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
			case 2:
				this.ᜁ = A_0[A_1 + 1].ToString();
				A_1 += 2;
				num = 10;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_196;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 4:
				if (A_0[A_1] == '[')
				{
					num = 6;
					continue;
				}
				return A_1;
			case 5:
				goto IL_12F;
			case 6:
				num = 9;
				continue;
			case 7:
				goto IL_EF;
			case 8:
			{
				int length;
				if (length == 0)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
			}
			case 9:
				if (A_0[A_1 + 2] == '$')
				{
					num = 11;
					continue;
				}
				return A_1;
			case 10:
				goto IL_C7;
			case 11:
				this.ᜁ = A_0[A_1 + 1].ToString();
				A_1 = A_0.IndexOf(']', A_1 + 3) + 1;
				num = 5;
				continue;
			}
			if (A_0 == null)
			{
				num = 0;
			}
			else
			{
				int length = A_0.Length;
				num = 8;
			}
		}
		IL_70:
		throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
		IL_C7:
		return A_1;
		IL_EF:
		goto IL_196;
		IL_12F:
		return A_1;
		IL_196:
		throw new ArgumentException(RecordTableEnumerator.b("ᝃ㉅㩇⍉≋⥍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ͥէᩩᡫ᝭幯", a_), RecordTableEnumerator.b("≃⥅㩇❉ⵋ㩍", a_));
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x001D4994 File Offset: 0x001D3994
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
		return this.ᜁ;
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x001D49D8 File Offset: 0x001D39D8
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
		return this.ᜁ;
	}

	// Token: 0x06003321 RID: 13089 RVA: 0x001D4A1C File Offset: 0x001D3A1C
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
		return TokenType.Character;
	}

	// Token: 0x0400164D RID: 5709
	private new const char ᜀ = '\\';
}
