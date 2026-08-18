using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000502 RID: 1282
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PivotViewSource)]
internal class spr\u17F8 : BiffRecordRaw
{
	// Token: 0x06004E44 RID: 20036 RVA: 0x002F9F88 File Offset: 0x002F8F88
	public spr\u17F8()
	{
	}

	// Token: 0x06004E45 RID: 20037 RVA: 0x002F9F9C File Offset: 0x002F8F9C
	public spr\u17F8(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E46 RID: 20038 RVA: 0x002F9FB4 File Offset: 0x002F8FB4
	public spr\u17F8(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E47 RID: 20039 RVA: 0x002F9FC8 File Offset: 0x002F8FC8
	public spr\u17F8.DataSourceTypes ᜀ()
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
		return (spr\u17F8.DataSourceTypes)this.ᜁ;
	}

	// Token: 0x06004E48 RID: 20040 RVA: 0x002FA00C File Offset: 0x002F900C
	public void ᜀ(spr\u17F8.DataSourceTypes A_0)
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

	// Token: 0x06004E49 RID: 20041 RVA: 0x002FA050 File Offset: 0x002F9050
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

	// Token: 0x06004E4A RID: 20042 RVA: 0x002FA098 File Offset: 0x002F9098
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

	// Token: 0x06004E4B RID: 20043 RVA: 0x002FA0E8 File Offset: 0x002F90E8
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

	// Token: 0x04002362 RID: 9058
	private new const int ᜀ = 2;

	// Token: 0x04002363 RID: 9059
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x02000503 RID: 1283
	public enum DataSourceTypes
	{
		// Token: 0x04002365 RID: 9061
		MSExcelOrDB = 1,
		// Token: 0x04002366 RID: 9062
		External,
		// Token: 0x04002367 RID: 9063
		ConsolidationRanges = 4,
		// Token: 0x04002368 RID: 9064
		PivotTable = 8,
		// Token: 0x04002369 RID: 9065
		ScenarioManager = 16
	}
}
