using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200049F RID: 1183
[spr\u2593(TBIFFRecord.SQLDataTypeId)]
[CLSCompliant(false)]
internal class spr\u2503 : BiffRecordRaw
{
	// Token: 0x06004917 RID: 18711 RVA: 0x002C74A4 File Offset: 0x002C64A4
	public spr\u2503()
	{
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x002C74B8 File Offset: 0x002C64B8
	public spr\u2503(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x002C74D0 File Offset: 0x002C64D0
	public spr\u2503(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600491A RID: 18714 RVA: 0x002C74E4 File Offset: 0x002C64E4
	public spr\u2503.SQLDataType ᜀ()
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
		return (spr\u2503.SQLDataType)this.ᜁ;
	}

	// Token: 0x0600491B RID: 18715 RVA: 0x002C7528 File Offset: 0x002C6528
	public void ᜀ(spr\u2503.SQLDataType A_0)
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
		this.ᜁ = (ushort)A_0;
	}

	// Token: 0x0600491C RID: 18716 RVA: 0x002C756C File Offset: 0x002C656C
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

	// Token: 0x0600491D RID: 18717 RVA: 0x002C75B4 File Offset: 0x002C65B4
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

	// Token: 0x0600491E RID: 18718 RVA: 0x002C7604 File Offset: 0x002C6604
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

	// Token: 0x04002127 RID: 8487
	private new const int ᜀ = 2;

	// Token: 0x04002128 RID: 8488
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x020004A0 RID: 1184
	public enum SQLDataType
	{
		// Token: 0x0400212A RID: 8490
		SQL_UNKNOWN_TYPE,
		// Token: 0x0400212B RID: 8491
		SQL_CHAR,
		// Token: 0x0400212C RID: 8492
		SQL_NUMERIC,
		// Token: 0x0400212D RID: 8493
		SQL_DECIMAL,
		// Token: 0x0400212E RID: 8494
		SQL_INTEGER,
		// Token: 0x0400212F RID: 8495
		SQL_SMALLINT,
		// Token: 0x04002130 RID: 8496
		SQL_FLOAT,
		// Token: 0x04002131 RID: 8497
		SQL_REAL,
		// Token: 0x04002132 RID: 8498
		SQL_DOUBLE,
		// Token: 0x04002133 RID: 8499
		SQL_DATETIME,
		// Token: 0x04002134 RID: 8500
		SQL_VARCHAR = 12
	}
}
