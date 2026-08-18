using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000517 RID: 1303
internal class sprṒ : sprἏ
{
	// Token: 0x06004F39 RID: 20281 RVA: 0x002FF4E4 File Offset: 0x002FE4E4
	public sprṒ()
	{
		int a_ = 1;
		base..ctor();
		this.ᜁ = RecordTableEnumerator.b("శ", a_);
	}

	// Token: 0x06004F3A RID: 20282 RVA: 0x002FF514 File Offset: 0x002FE514
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 16;
		int num = 2;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
			{
				int length;
				if (A_1 > length - 1)
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				int length2 = this.ᜁ.Length;
				int num2 = string.Compare(A_0, A_1, this.ᜁ, 0, length2);
				num = 4;
				continue;
			}
			case 1:
			{
				int length;
				if (length == 0)
				{
					num = 6;
					continue;
				}
				num = 7;
				continue;
			}
			case 3:
				num = 0;
				continue;
			case 4:
			{
				int num2;
				if (num2 == 0)
				{
					num = 9;
					continue;
				}
				return A_1;
			}
			case 5:
				goto IL_50;
			case 6:
				goto IL_E3;
			case 7:
				if (A_1 >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_5F;
			case 8:
				return A_1;
			case 9:
			{
				int length2;
				A_1 += length2;
				num = 8;
				continue;
			}
			case 10:
				goto IL_A5;
			}
			while (A_0 != null)
			{
				int length = A_0.Length;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					goto IL_13;
				}
			}
			num = 5;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("⁅❇㡉⅋⽍⑏", a_));
		IL_5F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_), RecordTableEnumerator.b("ཅ♇⹉⥋㙍灏㹑ㅓ╕⭗穙⡛㙝şౡ䑣噥䡧୩ɫ੭偯ᕱٳ፵᥷๹᥻౽ꁿꪉﺏﺕ뚗", a_));
		IL_A5:
		goto IL_5F;
		IL_E3:
		throw new ArgumentException(RecordTableEnumerator.b("ᕅ㱇㡉╋⁍㝏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥൧ݩᱫᩭ९山", a_), RecordTableEnumerator.b("⁅❇㡉⅋⽍⑏", a_));
	}

	// Token: 0x06004F3B RID: 20283 RVA: 0x002FF6B0 File Offset: 0x002FE6B0
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
		throw new NotSupportedException();
	}

	// Token: 0x06004F3C RID: 20284 RVA: 0x002FF6F0 File Offset: 0x002FE6F0
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

	// Token: 0x06004F3D RID: 20285 RVA: 0x002FF730 File Offset: 0x002FE730
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
		return TokenType.Section;
	}

	// Token: 0x040023BE RID: 9150
	private new const string ᜀ = ";";
}
