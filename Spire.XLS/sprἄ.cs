using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000453 RID: 1107
internal class sprἄ : sprἏ
{
	// Token: 0x060042BB RID: 17083 RVA: 0x00255D9C File Offset: 0x00254D9C
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 7;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					goto IL_9C;
				}
				break;
			case 3:
				goto IL_84;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			int length = A_0.Length;
			num = 0;
			continue;
			IL_6F:
			if (length != 0)
			{
				goto IL_B6;
			}
			num = 3;
		}
		IL_84:
		throw new ArgumentException(RecordTableEnumerator.b("渼䬾㍀⩂⭄⁆楈⡊ⱌⅎ㽐㱒⅔睖㭘㹚絜㩞ౠ።ᅤṦ䝨", a_), RecordTableEnumerator.b("嬼倾㍀⹂⑄㍆", a_));
		IL_9C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("嬼倾㍀⹂⑄㍆", a_));
		IL_B6:
		this.ᜁ = A_0[A_1].ToString();
		return A_1 + 1;
	}

	// Token: 0x060042BC RID: 17084 RVA: 0x00255E78 File Offset: 0x00254E78
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

	// Token: 0x060042BD RID: 17085 RVA: 0x00255EBC File Offset: 0x00254EBC
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

	// Token: 0x060042BE RID: 17086 RVA: 0x00255F00 File Offset: 0x00254F00
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
		return TokenType.Unknown;
	}
}
