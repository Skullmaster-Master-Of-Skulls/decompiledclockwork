using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000504 RID: 1284
[spr\u2593(TBIFFRecord.PivotName)]
[CLSCompliant(false)]
internal class spr\u1F5A : BiffRecordRaw
{
	// Token: 0x06004E4C RID: 20044 RVA: 0x002FA124 File Offset: 0x002F9124
	public spr\u1F5A()
	{
	}

	// Token: 0x06004E4D RID: 20045 RVA: 0x002FA138 File Offset: 0x002F9138
	public spr\u1F5A(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E4E RID: 20046 RVA: 0x002FA150 File Offset: 0x002F9150
	public spr\u1F5A(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E4F RID: 20047 RVA: 0x002FA164 File Offset: 0x002F9164
	public new ushort ᜃ()
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

	// Token: 0x06004E50 RID: 20048 RVA: 0x002FA1A8 File Offset: 0x002F91A8
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

	// Token: 0x06004E51 RID: 20049 RVA: 0x002FA1EC File Offset: 0x002F91EC
	public void ᜀ(bool A_0)
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

	// Token: 0x06004E52 RID: 20050 RVA: 0x002FA230 File Offset: 0x002F9230
	public ushort ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06004E53 RID: 20051 RVA: 0x002FA274 File Offset: 0x002F9274
	public void ᜁ(ushort A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004E54 RID: 20052 RVA: 0x002FA2B8 File Offset: 0x002F92B8
	public ushort ᜄ()
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

	// Token: 0x06004E55 RID: 20053 RVA: 0x002FA2FC File Offset: 0x002F92FC
	public void ᜂ(ushort A_0)
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

	// Token: 0x06004E56 RID: 20054 RVA: 0x002FA340 File Offset: 0x002F9340
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
		return this.ᜅ;
	}

	// Token: 0x06004E57 RID: 20055 RVA: 0x002FA384 File Offset: 0x002F9384
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
		this.ᜅ = A_0;
	}

	// Token: 0x06004E58 RID: 20056 RVA: 0x002FA3C8 File Offset: 0x002F93C8
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
		this.ᜂ = A_0.ReadBit(A_1, 1);
		this.ᜃ = A_0.ReadUInt16(A_1 + 2);
		this.ᜄ = A_0.ReadUInt16(A_1 + 4);
		this.ᜅ = A_0.ReadUInt16(A_1 + 6);
	}

	// Token: 0x06004E59 RID: 20057 RVA: 0x002FA44C File Offset: 0x002F944C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteBit(A_1, this.ᜂ, 1);
		A_0.WriteUInt16(A_1 + 2, this.ᜃ);
		A_0.WriteUInt16(A_1 + 4, this.ᜄ);
		A_0.WriteUInt16(A_1 + 6, this.ᜅ);
		this.m_iLength = 8;
	}

	// Token: 0x06004E5A RID: 20058 RVA: 0x002FA4D8 File Offset: 0x002F94D8
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
		return 8;
	}

	// Token: 0x0400236A RID: 9066
	private new const int ᜀ = 8;

	// Token: 0x0400236B RID: 9067
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x0400236C RID: 9068
	[spr\u2429(0, 1, TFieldType.Bit)]
	private bool ᜂ;

	// Token: 0x0400236D RID: 9069
	[spr\u2429(2, 2)]
	private new ushort ᜃ;

	// Token: 0x0400236E RID: 9070
	[spr\u2429(4, 2)]
	private ushort ᜄ;

	// Token: 0x0400236F RID: 9071
	[spr\u2429(6, 2)]
	private ushort ᜅ;
}
