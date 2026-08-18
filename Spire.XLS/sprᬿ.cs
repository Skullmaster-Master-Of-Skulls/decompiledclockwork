using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003F4 RID: 1012
[spr\u2593(TBIFFRecord.HCenter)]
[spr\u2593(TBIFFRecord.VCenter)]
[CLSCompliant(false)]
internal class spr\u1B3F : BiffRecordRaw
{
	// Token: 0x06003CE6 RID: 15590 RVA: 0x00220614 File Offset: 0x0021F614
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
		return this.ᜀ;
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x00220658 File Offset: 0x0021F658
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
		this.ᜀ = A_0;
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x0022069C File Offset: 0x0021F69C
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

	// Token: 0x06003CE9 RID: 15593 RVA: 0x002206D8 File Offset: 0x0021F6D8
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

	// Token: 0x06003CEA RID: 15594 RVA: 0x00220714 File Offset: 0x0021F714
	public spr\u1B3F()
	{
	}

	// Token: 0x06003CEB RID: 15595 RVA: 0x00220728 File Offset: 0x0021F728
	public spr\u1B3F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003CEC RID: 15596 RVA: 0x00220740 File Offset: 0x0021F740
	public spr\u1B3F(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003CED RID: 15597 RVA: 0x00220754 File Offset: 0x0021F754
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
		this.ᜀ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x0022079C File Offset: 0x0021F79C
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
		this.m_iLength = this.GetStoreSize(A_2);
		A_0.WriteUInt16(A_1, this.ᜀ);
	}

	// Token: 0x04001A45 RID: 6725
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
