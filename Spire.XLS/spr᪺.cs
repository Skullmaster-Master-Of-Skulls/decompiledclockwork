using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003D8 RID: 984
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartEndDispUnit)]
internal class spr\u1ABA : BiffRecordRaw
{
	// Token: 0x06003BB2 RID: 15282 RVA: 0x00216008 File Offset: 0x00215008
	public spr\u1ABA()
	{
	}

	// Token: 0x06003BB3 RID: 15283 RVA: 0x0021601C File Offset: 0x0021501C
	public spr\u1ABA(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x00216034 File Offset: 0x00215034
	public spr\u1ABA(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x00216048 File Offset: 0x00215048
	public bool ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x06003BB6 RID: 15286 RVA: 0x0021608C File Offset: 0x0021508C
	public void ᜀ(bool A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003BB7 RID: 15287 RVA: 0x002160D0 File Offset: 0x002150D0
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
		return 6;
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x0021610C File Offset: 0x0021510C
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

	// Token: 0x06003BB9 RID: 15289 RVA: 0x0021614C File Offset: 0x0021514C
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
		this.ᜂ = A_0.ReadBit(A_1, 4);
	}

	// Token: 0x06003BBA RID: 15290 RVA: 0x0021619C File Offset: 0x0021519C
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
		int offset = A_1;
		A_0.WriteUInt32(A_1, 0U);
		A_1 += 4;
		A_0.WriteUInt32(A_1, 0U);
		A_1 += 4;
		A_0.WriteBit(offset, this.ᜂ, 4);
	}

	// Token: 0x06003BBB RID: 15291 RVA: 0x00216224 File Offset: 0x00215224
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

	// Token: 0x040019EB RID: 6635
	public new const int ᜀ = 6;

	// Token: 0x040019EC RID: 6636
	public const int ᜁ = 12;

	// Token: 0x040019ED RID: 6637
	[spr\u2429(4, 4, TFieldType.Bit)]
	private bool ᜂ;
}
