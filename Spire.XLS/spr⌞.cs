using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000430 RID: 1072
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartAxisLineFormat)]
internal class spr\u231E : BiffRecordRaw
{
	// Token: 0x060040D5 RID: 16597 RVA: 0x00245328 File Offset: 0x00244328
	public AxisLineIdentifierType ᜀ()
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
		return (AxisLineIdentifierType)this.ᜁ;
	}

	// Token: 0x060040D6 RID: 16598 RVA: 0x0024536C File Offset: 0x0024436C
	public void ᜀ(AxisLineIdentifierType A_0)
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

	// Token: 0x060040D7 RID: 16599 RVA: 0x002453B0 File Offset: 0x002443B0
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
		return 2;
	}

	// Token: 0x060040D8 RID: 16600 RVA: 0x002453EC File Offset: 0x002443EC
	public virtual int ᜁ()
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
		return 2;
	}

	// Token: 0x060040D9 RID: 16601 RVA: 0x00245428 File Offset: 0x00244428
	public spr\u231E()
	{
	}

	// Token: 0x060040DA RID: 16602 RVA: 0x0024543C File Offset: 0x0024443C
	public spr\u231E(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060040DB RID: 16603 RVA: 0x00245454 File Offset: 0x00244454
	public spr\u231E(int A_0) : base(A_0)
	{
	}

	// Token: 0x060040DC RID: 16604 RVA: 0x00245468 File Offset: 0x00244468
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060040DD RID: 16605 RVA: 0x002454B0 File Offset: 0x002444B0
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
		this.ᜁ &= 3;
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x060040DE RID: 16606 RVA: 0x00245514 File Offset: 0x00244514
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

	// Token: 0x04001CEE RID: 7406
	public new const int ᜀ = 2;

	// Token: 0x04001CEF RID: 7407
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
