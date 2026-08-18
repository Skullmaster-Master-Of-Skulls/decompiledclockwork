using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003DC RID: 988
internal class spr\u262F : sprᭅ
{
	// Token: 0x06003BD8 RID: 15320 RVA: 0x00216AB8 File Offset: 0x00215AB8
	public override int ᜀ(string A_0, int A_1, int A_2, int A_3)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 7;
			double num3;
			for (;;)
			{
				string s;
				switch (num)
				{
				case 0:
				{
					int length;
					if (A_2 > length - 1)
					{
						num = 6;
						continue;
					}
					int num2 = base.ᜀ(spr\u262F.ᜀ, A_0, A_2, false);
					num = 4;
					continue;
				}
				case 1:
					goto IL_119;
				case 2:
					goto IL_17C;
				case 3:
					goto IL_138;
				case 4:
				{
					int num2;
					if (num2 < 0)
					{
						num = 2;
						continue;
					}
					string text = spr\u262F.ᜀ[num2];
					A_2 += text.Length;
					this.ᜂ = num2 + spr\u262F.CompareOperation.Equal;
					s = A_0.Substring(A_2, A_3 - A_2);
					num = 1;
					continue;
				}
				case 5:
					goto IL_E1;
				case 6:
					goto IL_1F2;
				case 8:
				{
					int length;
					if (length == 0)
					{
						num = 5;
						continue;
					}
					num = 10;
					continue;
				}
				case 9:
					goto IL_81;
				case 10:
					if (A_2 >= 0)
					{
						num = 11;
						continue;
					}
					goto IL_90;
				case 11:
					num = 0;
					continue;
				}
				if (A_0 != null)
				{
					int length = A_0.Length;
					num = 8;
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
					num = 9;
					continue;
				}
				IL_119:
				if (!double.TryParse(s, NumberStyles.Any, null, out num3))
				{
					return A_1;
				}
				num = 3;
			}
			IL_81:
			throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
			IL_90:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_), RecordTableEnumerator.b("िⱁ⁃⍅ぇ橉⁋⭍⍏⅑瑓≕し㭙㉛繝偟䉡ୣᑥ䡧൩ṫ୭ᅯٱᅳѵ塷๹ᑻώꊁ뺏", a_));
			IL_E1:
			throw new ArgumentException(RecordTableEnumerator.b("ጿ㙁㙃⽅♇ⵉ汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟ݡॣᙥᱧ፩䉫", a_), RecordTableEnumerator.b("☿ⵁ㙃⭅⥇㹉", a_));
			IL_138:
			this.ᜁ = num3;
			return A_3 + 1;
			IL_17C:
			if (true)
			{
			}
			return A_1;
			IL_1F2:
			goto IL_90;
		}
		}
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x00216CC0 File Offset: 0x00215CC0
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

	// Token: 0x06003BDA RID: 15322 RVA: 0x00216D00 File Offset: 0x00215D00
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

	// Token: 0x06003BDB RID: 15323 RVA: 0x00216D40 File Offset: 0x00215D40
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
		return TokenType.Condition;
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x00216D80 File Offset: 0x00215D80
	public new bool ᜀ(double A_0)
	{
		int a_ = 6;
		for (;;)
		{
			spr\u262F.CompareOperation compareOperation = this.ᜂ;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_B3;
				case 2:
					switch (compareOperation)
					{
					case spr\u262F.CompareOperation.Equal:
						goto IL_6D;
					case spr\u262F.CompareOperation.NotEqual:
						goto IL_9B;
					case spr\u262F.CompareOperation.GreaterEqual:
						goto IL_60;
					case spr\u262F.CompareOperation.LessEqual:
						goto IL_B5;
					case spr\u262F.CompareOperation.Less:
						goto IL_56;
					case spr\u262F.CompareOperation.Greater:
						goto IL_C2;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_56:
		return A_0 < this.ᜁ;
		IL_60:
		return A_0 >= this.ᜁ;
		IL_6D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9B:
			return A_0 != this.ᜁ;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			return A_0 == this.ᜁ;
		}
		IL_B3:
		throw new ArgumentException(RecordTableEnumerator.b("椻倽⬿ⵁ㍃⡅桇⥉⍋⍍⁏㍑♓㍕硗㕙ⱛ㭝቟͡ၣཥݧѩ䉫", a_));
		IL_B5:
		return A_0 <= this.ᜁ;
		IL_C2:
		return A_0 > this.ᜁ;
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x00216E78 File Offset: 0x00215E78
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u262F()
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u262F.ᜀ = new string[]
		{
			RecordTableEnumerator.b("筅", a_),
			RecordTableEnumerator.b("穅癇", a_),
			RecordTableEnumerator.b("硅畇", a_),
			RecordTableEnumerator.b("穅畇", a_),
			RecordTableEnumerator.b("穅", a_),
			RecordTableEnumerator.b("硅", a_)
		};
	}

	// Token: 0x040019FC RID: 6652
	private new static readonly string[] ᜀ;

	// Token: 0x040019FD RID: 6653
	private new double ᜁ;

	// Token: 0x040019FE RID: 6654
	private new spr\u262F.CompareOperation ᜂ;

	// Token: 0x020003DD RID: 989
	private enum CompareOperation
	{
		// Token: 0x04001A00 RID: 6656
		None,
		// Token: 0x04001A01 RID: 6657
		Equal,
		// Token: 0x04001A02 RID: 6658
		NotEqual,
		// Token: 0x04001A03 RID: 6659
		GreaterEqual,
		// Token: 0x04001A04 RID: 6660
		LessEqual,
		// Token: 0x04001A05 RID: 6661
		Less,
		// Token: 0x04001A06 RID: 6662
		Greater
	}
}
