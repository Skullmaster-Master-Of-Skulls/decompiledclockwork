using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000433 RID: 1075
internal class spr\u25F3 : sprἏ
{
	// Token: 0x060040F4 RID: 16628 RVA: 0x00245B84 File Offset: 0x00244B84
	public override int ᜀ(string A_0, int A_1)
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
		return base.ᜀ(spr\u25F3.ᜀ, A_0, A_1);
	}

	// Token: 0x060040F5 RID: 16629 RVA: 0x00245BCC File Offset: 0x00244BCC
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		double num;
		for (;;)
		{
			num = A_0 * 24.0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					if (A_0 > 0.0)
					{
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					num2 = 2;
					continue;
				case 2:
					goto IL_8D;
				case 3:
					goto IL_70;
				}
				break;
			}
		}
		IL_70:
		double num3 = Math.Floor(num);
		goto IL_95;
		IL_8D:
		num3 = Math.Ceiling(num);
		IL_95:
		num = num3;
		return ((int)num).ToString();
	}

	// Token: 0x060040F6 RID: 16630 RVA: 0x00245C7C File Offset: 0x00244C7C
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

	// Token: 0x060040F7 RID: 16631 RVA: 0x00245CBC File Offset: 0x00244CBC
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
		return TokenType.Hour24;
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x00245CF8 File Offset: 0x00244CF8
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u25F3()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u25F3.ᜀ = new Regex(RecordTableEnumerator.b("昹朻攽⠿ੁ᥃浅ᑇᝉ", a_), RegexOptions.Compiled);
	}

	// Token: 0x04001CF9 RID: 7417
	private new static readonly Regex ᜀ;
}
