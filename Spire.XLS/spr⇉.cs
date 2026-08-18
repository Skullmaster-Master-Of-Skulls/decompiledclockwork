using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003A4 RID: 932
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartBegDispUnit)]
internal class spr\u21C9 : BiffRecordRaw
{
	// Token: 0x060038BD RID: 14525 RVA: 0x001FA6AC File Offset: 0x001F96AC
	public spr\u21C9()
	{
	}

	// Token: 0x060038BE RID: 14526 RVA: 0x001FA6C0 File Offset: 0x001F96C0
	public spr\u21C9(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060038BF RID: 14527 RVA: 0x001FA6D8 File Offset: 0x001F96D8
	public spr\u21C9(int A_0) : base(A_0)
	{
	}

	// Token: 0x060038C0 RID: 14528 RVA: 0x001FA6EC File Offset: 0x001F96EC
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
		return this.ᜁ;
	}

	// Token: 0x060038C1 RID: 14529 RVA: 0x001FA730 File Offset: 0x001F9730
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
		this.ᜁ = A_0;
	}

	// Token: 0x060038C2 RID: 14530 RVA: 0x001FA774 File Offset: 0x001F9774
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
		return 12;
	}

	// Token: 0x060038C3 RID: 14531 RVA: 0x001FA7B4 File Offset: 0x001F97B4
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

	// Token: 0x060038C4 RID: 14532 RVA: 0x001FA7F4 File Offset: 0x001F97F4
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
		this.ᜁ = A_0.ReadBit(A_1, 4);
	}

	// Token: 0x060038C5 RID: 14533 RVA: 0x001FA844 File Offset: 0x001F9844
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
		A_0.WriteBit(offset, this.ᜁ, 4);
	}

	// Token: 0x060038C6 RID: 14534 RVA: 0x001FA8CC File Offset: 0x001F98CC
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

	// Token: 0x040018ED RID: 6381
	public new const int ᜀ = 12;

	// Token: 0x040018EE RID: 6382
	[spr\u2429(4, 4, TFieldType.Bit)]
	private bool ᜁ;
}
