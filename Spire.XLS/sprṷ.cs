using System;
using System.Globalization;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000277 RID: 631
internal abstract class sprṷ : sprἏ
{
	// Token: 0x06002633 RID: 9779 RVA: 0x0015EA64 File Offset: 0x0015DA64
	public sprṷ()
	{
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x0015EA78 File Offset: 0x0015DA78
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 9;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D9;
			case 1:
				goto IL_B7;
			case 2:
			{
				A_1++;
				char c;
				this.ᜁ = c.ToString();
				num = 7;
				continue;
			}
			case 3:
				goto IL_50;
			case 4:
				num = 6;
				continue;
			case 5:
			{
				int length;
				if (length == 0)
				{
					num = 0;
					continue;
				}
				num = 10;
				continue;
			}
			case 6:
			{
				int length;
				if (A_1 > length - 1)
				{
					num = 1;
					continue;
				}
				char c = A_0[A_1];
				num = 9;
				continue;
			}
			case 7:
				return A_1;
			case 9:
			{
				char c;
				if (c == this.ᜁ())
				{
					num = 2;
					continue;
				}
				return A_1;
			}
			case 10:
				if (A_1 >= 0)
				{
					num = 4;
					continue;
				}
				goto IL_71;
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				int length = A_0.Length;
				num = 5;
			}
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_D9:
			throw new ArgumentException(RecordTableEnumerator.b("氾㕀ㅂⱄ⥆⹈歊⹌⹎㽐㵒㩔⍖祘㥚㡜罞Ѡ๢ᕤ፦ၨ", a_), RecordTableEnumerator.b("夾⹀ㅂ⡄♆㵈", a_));
		default:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("夾⹀ㅂ⡄♆㵈", a_));
		}
		IL_71:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾⽀❂⁄㽆", a_), RecordTableEnumerator.b("瘾⽀❂⁄㽆楈❊⡌㱎≐獒⅔㽖㡘㕚絜潞䅠ౢᝤ䝦๨ᥪ࡬๮հᙲݴ坶൸፺ᱼᅾꆀﾊꆎ", a_));
		IL_B7:
		goto IL_71;
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x0015EC14 File Offset: 0x0015DC14
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

	// Token: 0x06002636 RID: 9782 RVA: 0x0015EC58 File Offset: 0x0015DC58
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

	// Token: 0x06002637 RID: 9783
	public new abstract char ᜁ();
}
