using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003FA RID: 1018
[spr\u2593(TBIFFRecord.Number)]
[CLSCompliant(false)]
internal class spr\u19FF : spr\u22C6, spr\u2230, spr\u1929
{
	// Token: 0x06003D46 RID: 15686 RVA: 0x00222A6C File Offset: 0x00221A6C
	public double ᜅ()
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

	// Token: 0x06003D47 RID: 15687 RVA: 0x00222AB0 File Offset: 0x00221AB0
	public new void ᜀ(double A_0)
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

	// Token: 0x06003D48 RID: 15688 RVA: 0x00222AF4 File Offset: 0x00221AF4
	public virtual int ᜂ()
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
		return 14;
	}

	// Token: 0x06003D49 RID: 15689 RVA: 0x00222B34 File Offset: 0x00221B34
	public virtual int ᜃ()
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
		return 14;
	}

	// Token: 0x06003D4A RID: 15690 RVA: 0x00222B74 File Offset: 0x00221B74
	public virtual int ᜄ()
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
		return 14;
	}

	// Token: 0x06003D4B RID: 15691 RVA: 0x00222BB4 File Offset: 0x00221BB4
	public new double ᜁ()
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

	// Token: 0x06003D4D RID: 15693 RVA: 0x00222C0C File Offset: 0x00221C0C
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
		this.ᜁ = A_0.ReadDouble(A_1);
	}

	// Token: 0x06003D4E RID: 15694 RVA: 0x00222C54 File Offset: 0x00221C54
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
		A_0.WriteDouble(A_1, this.ᜁ);
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x00222C9C File Offset: 0x00221C9C
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 14;
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (A_0 != ExcelVersion.Version97to2003)
					{
						num2 = 2;
						continue;
					}
					return num;
				case 1:
					return num;
				case 2:
					num += 4;
					num2 = 1;
					continue;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x00222D14 File Offset: 0x00221D14
	public new static double ᜂ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				A_1 += 10;
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1 += 4;
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_6A;
				case 2:
					if (A_2 != ExcelVersion.Version97to2003)
					{
						num = 0;
						continue;
					}
					goto IL_6C;
				}
				break;
			}
		}
		IL_6A:
		IL_6C:
		return A_0.ReadDouble(A_1);
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x00222D94 File Offset: 0x00221D94
	object spr\u1929.ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x00222DDC File Offset: 0x00221DDC
	void spr\u1929.ᜀ(object A_0)
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
		this.ᜁ = (double)A_0;
	}

	// Token: 0x04001A77 RID: 6775
	private new const int ᜀ = 14;

	// Token: 0x04001A78 RID: 6776
	[spr\u2429(6, 8, TFieldType.Float)]
	private new double ᜁ;
}
