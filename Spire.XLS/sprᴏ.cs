using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200053E RID: 1342
[spr\u2593(TBIFFRecord.ChartIfmt)]
[CLSCompliant(false)]
internal class sprᴏ : BiffRecordRaw
{
	// Token: 0x060051B2 RID: 20914 RVA: 0x0032F838 File Offset: 0x0032E838
	public ushort ᜀ()
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

	// Token: 0x060051B3 RID: 20915 RVA: 0x0032F87C File Offset: 0x0032E87C
	public void ᜀ(ushort A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜁ = A_0;
				goto IL_48;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					goto IL_66;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 != this.ᜁ)
			{
				num = 1;
				continue;
			}
			return;
			IL_48:
			num = 2;
		}
		IL_66:
		if (false)
		{
		}
	}

	// Token: 0x060051B4 RID: 20916 RVA: 0x0032F8F8 File Offset: 0x0032E8F8
	public sprᴏ()
	{
	}

	// Token: 0x060051B5 RID: 20917 RVA: 0x0032F90C File Offset: 0x0032E90C
	public sprᴏ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051B6 RID: 20918 RVA: 0x0032F924 File Offset: 0x0032E924
	public sprᴏ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051B7 RID: 20919 RVA: 0x0032F938 File Offset: 0x0032E938
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060051B8 RID: 20920 RVA: 0x0032F980 File Offset: 0x0032E980
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x060051B9 RID: 20921 RVA: 0x0032F9D0 File Offset: 0x0032E9D0
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 2;
	}

	// Token: 0x04002469 RID: 9321
	private new const int ᜀ = 2;

	// Token: 0x0400246A RID: 9322
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
