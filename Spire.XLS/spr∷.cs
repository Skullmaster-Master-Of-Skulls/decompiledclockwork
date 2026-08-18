using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000260 RID: 608
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.StreamId)]
internal class spr\u2237 : BiffRecordRaw
{
	// Token: 0x0600248B RID: 9355 RVA: 0x00154C28 File Offset: 0x00153C28
	public spr\u2237()
	{
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x00154C3C File Offset: 0x00153C3C
	public spr\u2237(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x00154C54 File Offset: 0x00153C54
	public spr\u2237(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x00154C68 File Offset: 0x00153C68
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

	// Token: 0x0600248F RID: 9359 RVA: 0x00154CAC File Offset: 0x00153CAC
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

	// Token: 0x06002490 RID: 9360 RVA: 0x00154CF0 File Offset: 0x00153CF0
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

	// Token: 0x06002491 RID: 9361 RVA: 0x00154D38 File Offset: 0x00153D38
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

	// Token: 0x06002492 RID: 9362 RVA: 0x00154D88 File Offset: 0x00153D88
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

	// Token: 0x04001280 RID: 4736
	private new const int ᜀ = 2;

	// Token: 0x04001281 RID: 4737
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
