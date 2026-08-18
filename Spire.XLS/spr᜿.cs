using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003DB RID: 987
internal class spr\u173F : sprἏ
{
	// Token: 0x06003BD0 RID: 15312 RVA: 0x002167E8 File Offset: 0x002157E8
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
		return base.ᜀ(spr\u173F.ᜂ, A_0, A_1);
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x00216830 File Offset: 0x00215830
	public override string ᜀ(ref double A_0, bool A_1, CultureInfo A_2, sprᨠ A_3)
	{
		int a_ = 7;
		int num2;
		for (;;)
		{
			IL_2D:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8D:
				num = 6;
				break;
			case 1:
				goto IL_4D;
			default:
				goto IL_4D;
			}
			int millisecond;
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					num2++;
					num = 1;
					continue;
				case 1:
					goto IL_C2;
				case 2:
					if (millisecond >= 500)
					{
						num = 0;
						continue;
					}
					goto IL_8D;
				case 3:
					goto IL_B1;
				case 4:
					num = 2;
					continue;
				case 5:
					if (this.ᜃ)
					{
						num = 4;
						continue;
					}
					goto IL_8D;
				case 6:
					if (this.ᜁ.Length > 1)
					{
						num = 3;
						continue;
					}
					goto IL_FA;
				}
				goto IL_2D;
			}
			IL_C2:
			goto IL_8D;
			IL_4D:
			if (false)
			{
			}
			DateTime dateTime = DateTime.FromOADate(A_0);
			num2 = dateTime.Second;
			millisecond = dateTime.Millisecond;
			if (true)
			{
			}
			num = 5;
			goto IL_0B;
		}
		IL_B1:
		return num2.ToString(RecordTableEnumerator.b("഼༾", a_));
		IL_FA:
		return num2.ToString();
	}

	// Token: 0x06003BD2 RID: 15314 RVA: 0x00216940 File Offset: 0x00215940
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

	// Token: 0x06003BD3 RID: 15315 RVA: 0x00216980 File Offset: 0x00215980
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
		return TokenType.Second;
	}

	// Token: 0x06003BD4 RID: 15316 RVA: 0x002169BC File Offset: 0x002159BC
	public new bool ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x00216A00 File Offset: 0x00215A00
	public new void ᜀ(bool A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x00216A44 File Offset: 0x00215A44
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u173F()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u173F.ᜂ = new Regex(RecordTableEnumerator.b("挷䤹漻挽欿", a_), RegexOptions.Compiled);
	}

	// Token: 0x040019F8 RID: 6648
	private new const string ᜀ = "00";

	// Token: 0x040019F9 RID: 6649
	private new const int ᜁ = 500;

	// Token: 0x040019FA RID: 6650
	private new static readonly Regex ᜂ;

	// Token: 0x040019FB RID: 6651
	private new bool ᜃ = true;
}
