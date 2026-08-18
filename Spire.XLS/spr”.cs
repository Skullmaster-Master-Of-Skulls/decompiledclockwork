using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004AE RID: 1198
[spr\u2593(TBIFFRecord.ChartAxisOffset)]
[CLSCompliant(false)]
internal class spr\u201D : BiffRecordRaw
{
	// Token: 0x06004A1B RID: 18971 RVA: 0x002CD560 File Offset: 0x002CC560
	public spr\u201D()
	{
	}

	// Token: 0x06004A1C RID: 18972 RVA: 0x002CD574 File Offset: 0x002CC574
	public spr\u201D(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004A1D RID: 18973 RVA: 0x002CD58C File Offset: 0x002CC58C
	public spr\u201D(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004A1E RID: 18974 RVA: 0x002CD5A0 File Offset: 0x002CC5A0
	public int ᜁ()
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
		return (int)this.ᜂ;
	}

	// Token: 0x06004A1F RID: 18975 RVA: 0x002CD5E4 File Offset: 0x002CC5E4
	public void ᜀ(int A_0)
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
		this.ᜂ = (ushort)A_0;
	}

	// Token: 0x06004A20 RID: 18976 RVA: 0x002CD628 File Offset: 0x002CC628
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
		return 10;
	}

	// Token: 0x06004A21 RID: 18977 RVA: 0x002CD668 File Offset: 0x002CC668
	public virtual int ᜀ()
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
		return 12;
	}

	// Token: 0x06004A22 RID: 18978 RVA: 0x002CD6A8 File Offset: 0x002CC6A8
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		A_1 += 4;
		this.ᜂ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06004A23 RID: 18979 RVA: 0x002CD6F8 File Offset: 0x002CC6F8
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, (ushort)base.TypeCode);
		A_1 += 2;
		A_0.WriteUInt16(A_1, 0);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, 2);
		A_1 += 2;
		A_0.WriteUInt32(A_1, 0U);
	}

	// Token: 0x06004A24 RID: 18980 RVA: 0x002CD77C File Offset: 0x002CC77C
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
		return 12;
	}

	// Token: 0x04002199 RID: 8601
	public new const int ᜀ = 10;

	// Token: 0x0400219A RID: 8602
	public const int ᜁ = 12;

	// Token: 0x0400219B RID: 8603
	[spr\u2429(4, 2)]
	private ushort ᜂ;
}
