using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000431 RID: 1073
[spr\u2593(TBIFFRecord.BoolErr)]
[CLSCompliant(false)]
internal class spr\u249B : spr\u22C6, spr\u1929
{
	// Token: 0x060040DF RID: 16607 RVA: 0x00245550 File Offset: 0x00244550
	public byte ᜄ()
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

	// Token: 0x060040E0 RID: 16608 RVA: 0x00245594 File Offset: 0x00244594
	public new void ᜀ(byte A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060040E1 RID: 16609 RVA: 0x002455D8 File Offset: 0x002445D8
	public new bool ᜂ()
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
		return this.ᜂ == 1;
	}

	// Token: 0x060040E2 RID: 16610 RVA: 0x0024561C File Offset: 0x0024461C
	public new void ᜀ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜂ = (A_0 ? 1 : 0);
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x00245668 File Offset: 0x00244668
	public virtual int ᜀ()
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
		return 8;
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x002456A4 File Offset: 0x002446A4
	public virtual int ᜁ()
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
		return 8;
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x002456F4 File Offset: 0x002446F4
	protected override void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.ᜁ = A_0.ReadByte(A_1);
		this.ᜂ = A_0.ReadByte(A_1 + 1);
	}

	// Token: 0x060040E7 RID: 16615 RVA: 0x0024574C File Offset: 0x0024474C
	protected override void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteByte(A_1, this.ᜁ);
		A_0.WriteByte(A_1 + 1, this.ᜂ);
	}

	// Token: 0x060040E8 RID: 16616 RVA: 0x002457A4 File Offset: 0x002447A4
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			num = 8;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_0 != ExcelVersion.Version97to2003)
						{
							num2 = 1;
							continue;
						}
						return num;
					}
					break;
				case 1:
					num += 4;
					num2 = 2;
					continue;
				case 2:
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x0024581C File Offset: 0x0024481C
	public new static int ᜂ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			A_1 += 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_60;
				case 1:
					A_1 += 4;
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_2 != ExcelVersion.Version97to2003)
						{
							num = 1;
							continue;
						}
						goto IL_62;
					}
					break;
				}
				break;
			}
		}
		IL_60:
		IL_62:
		return (int)A_0.ReadInt16(A_1);
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x0024589C File Offset: 0x0024489C
	public new object ᜃ()
	{
		if (true)
		{
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
			if (!this.ᜂ())
			{
				return this.ᜄ() != 0;
			}
			break;
		}
		return this.ᜄ();
	}

	// Token: 0x060040EB RID: 16619 RVA: 0x00245900 File Offset: 0x00244900
	public new void ᜀ(object A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 is bool)
			{
				this.ᜀ(false);
				this.ᜀ((byte)A_0);
				return;
			}
			break;
		}
		this.ᜀ(true);
		this.ᜀ((byte)A_0);
	}

	// Token: 0x04001CF0 RID: 7408
	private new const int ᜀ = 8;

	// Token: 0x04001CF1 RID: 7409
	[spr\u2429(6, 1)]
	private new byte ᜁ;

	// Token: 0x04001CF2 RID: 7410
	[spr\u2429(7, 1)]
	private new byte ᜂ;
}
