using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004B1 RID: 1201
internal class sprᴔ : sprἏ
{
	// Token: 0x06004A3C RID: 19004 RVA: 0x002CDDB0 File Offset: 0x002CCDB0
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 8;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_71;
				case 1:
					if (string.Compare(A_0, A_1, RecordTableEnumerator.b("礽┿ⱁ⅃㑅⥇♉", a_), 0, RecordTableEnumerator.b("礽┿ⱁ⅃㑅⥇♉", a_).Length, StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						num = 8;
						continue;
					}
					return A_1;
				case 2:
					num = 9;
					continue;
				case 3:
					goto IL_D9;
				case 4:
					return A_1;
				case 5:
					goto IL_B4;
				case 6:
					goto IL_6C;
				case 7:
				{
					int length;
					if (length == 0)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				}
				case 8:
					A_1 += RecordTableEnumerator.b("礽┿ⱁ⅃㑅⥇♉", a_).Length;
					num = 4;
					continue;
				case 9:
				{
					int length;
					if (A_1 > length - 1)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 6;
				}
				else
				{
					int length = A_0.Length;
					num = 7;
				}
			}
			IL_6C:
			throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
			IL_D9:
			throw new ArgumentException(RecordTableEnumerator.b("洽㐿ぁⵃ⡅⽇橉⽋⽍㹏㱑㭓≕硗㡙㥛繝՟ཡᑣብᅧ", a_), RecordTableEnumerator.b("堽⼿ぁ⥃❅㱇", a_));
		}
		}
		IL_71:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_), RecordTableEnumerator.b("眽⸿♁⅃㹅桇♉⥋㵍⍏牑⁓㹕㥗㑙籛湝䁟ൡᙣ䙥ཧᡩ५཭ѯ᝱ٳ噵౷ቹᵻၽꁿﺉꂍ", a_));
		IL_B4:
		goto IL_71;
	}

	// Token: 0x06004A3D RID: 19005 RVA: 0x002CDF6C File Offset: 0x002CCF6C
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 14;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9C:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3D;
		}
		string text;
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				goto IL_92;
			case 1:
				return text;
			case 2:
				num = 0;
				continue;
			case 3:
				if (text.Contains(RecordTableEnumerator.b("瑃桅硇穉籋繍", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_A6;
			}
			goto IL_3D;
		}
		IL_92:
		if (text.Length < 12)
		{
			goto IL_9C;
		}
		IL_A6:
		return A_0.ToString(A_2);
		IL_3D:
		if (true)
		{
		}
		text = ((decimal)A_0).ToString();
		num = 3;
		goto IL_27;
	}

	// Token: 0x06004A3E RID: 19006 RVA: 0x002CE028 File Offset: 0x002CD028
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
		return A_0;
	}

	// Token: 0x06004A3F RID: 19007 RVA: 0x002CE064 File Offset: 0x002CD064
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
		return TokenType.General;
	}

	// Token: 0x040021A2 RID: 8610
	private new const string ᜀ = "General";
}
