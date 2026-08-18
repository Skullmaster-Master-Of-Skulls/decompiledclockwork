using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200025F RID: 607
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.UnkMarker)]
internal class sprḥ : BiffRecordRaw
{
	// Token: 0x0600247E RID: 9342 RVA: 0x001548D0 File Offset: 0x001538D0
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

	// Token: 0x0600247F RID: 9343 RVA: 0x00154914 File Offset: 0x00153914
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

	// Token: 0x06002480 RID: 9344 RVA: 0x00154958 File Offset: 0x00153958
	public ushort ᜄ()
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

	// Token: 0x06002481 RID: 9345 RVA: 0x0015499C File Offset: 0x0015399C
	public void ᜂ(ushort A_0)
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

	// Token: 0x06002482 RID: 9346 RVA: 0x001549E0 File Offset: 0x001539E0
	public ushort ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x00154A24 File Offset: 0x00153A24
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

	// Token: 0x06002484 RID: 9348 RVA: 0x00154A68 File Offset: 0x00153A68
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
		return 6;
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x00154AA4 File Offset: 0x00153AA4
	public virtual int ᜁ()
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
		return 6;
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x00154AE0 File Offset: 0x00153AE0
	public sprḥ()
	{
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x00154AFC File Offset: 0x00153AFC
	public sprḥ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x00154B1C File Offset: 0x00153B1C
	public sprḥ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x00154B38 File Offset: 0x00153B38
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
		A_1 += 2;
		this.ᜂ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜃ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x00154BA4 File Offset: 0x00153BA4
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
		this.ᜁ = 0;
		this.ᜂ = 55;
		this.ᜃ = 0;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜂ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜃ);
	}

	// Token: 0x0400127C RID: 4732
	private new const ushort ᜀ = 55;

	// Token: 0x0400127D RID: 4733
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x0400127E RID: 4734
	[spr\u2429(2, 2)]
	private ushort ᜂ = 55;

	// Token: 0x0400127F RID: 4735
	[spr\u2429(4, 2)]
	private new ushort ᜃ;
}
