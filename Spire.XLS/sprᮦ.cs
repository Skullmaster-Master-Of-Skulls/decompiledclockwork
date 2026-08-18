using System;
using System.Globalization;
using Spire.Xls.Core.FormatParser.FormatTokens;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000346 RID: 838
internal class spr\u1BA6 : sprἏ
{
	// Token: 0x06003319 RID: 13081 RVA: 0x001D4578 File Offset: 0x001D3578
	public override int ᜀ(string A_0, int A_1)
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			int length;
			switch (num)
			{
			case 1:
				num = 7;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_43;
				default:
				{
					if (false)
					{
					}
					if (length == 0)
					{
						num = 5;
						continue;
					}
					bool flag = A_1 + 1 >= length;
					num = 8;
					continue;
				}
				}
				break;
			case 3:
				this.ᜁ = A_0[A_1 + 1].ToString();
				A_1 += 2;
				num = 6;
				continue;
			case 4:
				goto IL_4B;
			case 5:
				goto IL_112;
			case 6:
				goto IL_CA;
			case 7:
			{
				if (true)
				{
				}
				bool flag;
				if (!flag)
				{
					num = 3;
					continue;
				}
				return A_1;
			}
			case 8:
				if (A_0[A_1] == '_')
				{
					num = 1;
					continue;
				}
				return A_1;
			}
			goto IL_3D;
			IL_43:
			num = 4;
			continue;
			IL_3D:
			if (A_0 == null)
			{
				goto IL_43;
			}
			length = A_0.Length;
			num = 2;
		}
		IL_4B:
		throw new ArgumentNullException(RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃ", a_));
		IL_CA:
		return A_1;
		IL_112:
		throw new ArgumentException(RecordTableEnumerator.b("椹䠻䰽⤿ⱁ⍃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙㥛㍝ၟᙡᵣ䡥", a_), RecordTableEnumerator.b("尹医䰽ⴿ⍁ぃ", a_));
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x001D46D0 File Offset: 0x001D36D0
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
		return this.ᜀ(string.Empty, A_1);
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x001D4718 File Offset: 0x001D3718
	public override string ᜀ(string A_0, bool A_1)
	{
		int a_ = 11;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (!A_1)
			{
				return RecordTableEnumerator.b("慀", a_);
			}
			break;
		}
		if (true)
		{
		}
		return this.ᜁ;
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x001D4778 File Offset: 0x001D3778
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
		return TokenType.ReservedPlace;
	}

	// Token: 0x0400164B RID: 5707
	private new const char ᜀ = '_';

	// Token: 0x0400164C RID: 5708
	private new const string ᜁ = " ";
}
