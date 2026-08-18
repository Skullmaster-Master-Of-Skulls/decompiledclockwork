using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004AF RID: 1199
[spr\u2593(TBIFFRecord.ChartAxisDisplayUnits)]
[CLSCompliant(false)]
internal class spr\u1AB1 : BiffRecordRaw
{
	// Token: 0x06004A25 RID: 18981 RVA: 0x002CD7BC File Offset: 0x002CC7BC
	public spr\u1AB1()
	{
	}

	// Token: 0x06004A26 RID: 18982 RVA: 0x002CD7D0 File Offset: 0x002CC7D0
	public spr\u1AB1(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004A27 RID: 18983 RVA: 0x002CD7E8 File Offset: 0x002CC7E8
	public spr\u1AB1(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004A28 RID: 18984 RVA: 0x002CD7FC File Offset: 0x002CC7FC
	public ChartDisplayUnitType ᜁ()
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
		return (ChartDisplayUnitType)this.ᜁ;
	}

	// Token: 0x06004A29 RID: 18985 RVA: 0x002CD840 File Offset: 0x002CC840
	public void ᜀ(ChartDisplayUnitType A_0)
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

	// Token: 0x06004A2A RID: 18986 RVA: 0x002CD884 File Offset: 0x002CC884
	public double ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x06004A2B RID: 18987 RVA: 0x002CD8C8 File Offset: 0x002CC8C8
	public void ᜀ(double A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004A2C RID: 18988 RVA: 0x002CD90C File Offset: 0x002CC90C
	public bool ᜅ()
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
		return this.ᜃ == 3;
	}

	// Token: 0x06004A2D RID: 18989 RVA: 0x002CD950 File Offset: 0x002CC950
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
		this.ᜃ = (A_0 ? 3 : 1);
	}

	// Token: 0x06004A2E RID: 18990 RVA: 0x002CD99C File Offset: 0x002CC99C
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
		return this.ᜄ;
	}

	// Token: 0x06004A2F RID: 18991 RVA: 0x002CD9E0 File Offset: 0x002CC9E0
	public void ᜀ(byte A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004A30 RID: 18992 RVA: 0x002CDA24 File Offset: 0x002CCA24
	public virtual int ᜃ()
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
		return 16;
	}

	// Token: 0x06004A31 RID: 18993 RVA: 0x002CDA64 File Offset: 0x002CCA64
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
		return 16;
	}

	// Token: 0x06004A32 RID: 18994 RVA: 0x002CDAA4 File Offset: 0x002CCAA4
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		A_1 += 4;
		this.ᜁ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜂ = A_0.ReadDouble(A_1);
		A_1 += 8;
		this.ᜃ = A_0.ReadByte(A_1);
		A_1++;
		this.ᜄ = A_0.ReadByte(A_1);
	}

	// Token: 0x06004A33 RID: 18995 RVA: 0x002CDB28 File Offset: 0x002CCB28
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteDouble(A_1, this.ᜂ);
		A_1 += 8;
		A_0.WriteByte(A_1, this.ᜃ);
		A_1++;
		A_0.WriteByte(A_1, this.ᜄ);
		this.m_iLength = 16;
	}

	// Token: 0x06004A34 RID: 18996 RVA: 0x002CDBD0 File Offset: 0x002CCBD0
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
		return 16;
	}

	// Token: 0x0400219C RID: 8604
	public new const int ᜀ = 16;

	// Token: 0x0400219D RID: 8605
	[spr\u2429(4, 2)]
	private ushort ᜁ;

	// Token: 0x0400219E RID: 8606
	[spr\u2429(6, 8, TFieldType.Float)]
	private double ᜂ;

	// Token: 0x0400219F RID: 8607
	[spr\u2429(14, 1)]
	private new byte ᜃ;

	// Token: 0x040021A0 RID: 8608
	[spr\u2429(15, 1)]
	private byte ᜄ;
}
