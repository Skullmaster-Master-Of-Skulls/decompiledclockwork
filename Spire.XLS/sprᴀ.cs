using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004AC RID: 1196
[spr\u2593(TBIFFRecord.ChartSerParent)]
[CLSCompliant(false)]
internal class sprᴀ : BiffRecordRaw
{
	// Token: 0x06004A09 RID: 18953 RVA: 0x002CD178 File Offset: 0x002CC178
	public ushort ᜁ()
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

	// Token: 0x06004A0A RID: 18954 RVA: 0x002CD1BC File Offset: 0x002CC1BC
	public void ᜀ(ushort A_0)
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

	// Token: 0x06004A0B RID: 18955 RVA: 0x002CD200 File Offset: 0x002CC200
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

	// Token: 0x06004A0C RID: 18956 RVA: 0x002CD23C File Offset: 0x002CC23C
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
		return 2;
	}

	// Token: 0x06004A0D RID: 18957 RVA: 0x002CD278 File Offset: 0x002CC278
	public sprᴀ()
	{
	}

	// Token: 0x06004A0E RID: 18958 RVA: 0x002CD28C File Offset: 0x002CC28C
	public sprᴀ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004A0F RID: 18959 RVA: 0x002CD2A4 File Offset: 0x002CC2A4
	public sprᴀ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004A10 RID: 18960 RVA: 0x002CD2B8 File Offset: 0x002CC2B8
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

	// Token: 0x06004A11 RID: 18961 RVA: 0x002CD300 File Offset: 0x002CC300
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x04002195 RID: 8597
	public new const int ᜀ = 2;

	// Token: 0x04002196 RID: 8598
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
