using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200037A RID: 890
internal class spr\u2478 : sprἏ
{
	// Token: 0x06003649 RID: 13897 RVA: 0x001EB24C File Offset: 0x001EA24C
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 0:
				this.ᜁ = A_0[A_1 + 1].ToString();
				A_1 += 2;
				num = 3;
				continue;
			case 1:
				num = 11;
				continue;
			case 2:
				goto IL_CE;
			case 3:
				return A_1;
			case 5:
				if (A_1 >= 0)
				{
					num = 8;
					continue;
				}
				goto IL_123;
			case 6:
				goto IL_58;
			case 7:
				if (A_1 <= length - 1)
				{
					num = 9;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CE;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 8:
				if (true)
				{
				}
				num = 7;
				continue;
			case 9:
				if (A_0[A_1] == '*')
				{
					num = 1;
					continue;
				}
				return A_1;
			case 10:
				goto IL_BA;
			case 11:
				if (length > A_1 + 1)
				{
					num = 0;
					continue;
				}
				return A_1;
			case 12:
				goto IL_E9;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			length = A_0.Length;
			num = 2;
			continue;
			IL_CE:
			if (length == 0)
			{
				num = 12;
			}
			else
			{
				num = 5;
			}
		}
		IL_58:
		throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
		IL_BA:
		goto IL_123;
		IL_E9:
		throw new ArgumentException(RecordTableEnumerator.b("欷丹主圽⸿╁摃╅⥇⑉≋⅍⑏牑㙓㍕硗㽙ㅛ⹝ᑟ᭡䩣", a_), RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
		IL_123:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷吹堻嬽㠿", a_), RecordTableEnumerator.b("焷吹堻嬽㠿扁⡃⍅㭇㥉汋㩍㡏㍑㩓癕桗穙㍛ⱝ䁟աᙣͥ१ṩ५ᱭ偯ٱᱳ᝵ᙷ婹ࡻᙽꒃ揄벑", a_));
	}

	// Token: 0x0600364A RID: 13898 RVA: 0x001EB40C File Offset: 0x001EA40C
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

	// Token: 0x0600364B RID: 13899 RVA: 0x001EB44C File Offset: 0x001EA44C
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

	// Token: 0x0600364C RID: 13900 RVA: 0x001EB48C File Offset: 0x001EA48C
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
		return TokenType.Asterix;
	}

	// Token: 0x0400178D RID: 6029
	private new const char ᜀ = '*';
}
