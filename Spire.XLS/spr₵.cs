using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004E3 RID: 1251
[spr\u2593(TBIFFRecord.HideObj)]
[CLSCompliant(false)]
internal class spr\u20B5 : BiffRecordRaw
{
	// Token: 0x06004CBF RID: 19647 RVA: 0x002EE3B8 File Offset: 0x002ED3B8
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

	// Token: 0x06004CC0 RID: 19648 RVA: 0x002EE3FC File Offset: 0x002ED3FC
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

	// Token: 0x06004CC1 RID: 19649 RVA: 0x002EE440 File Offset: 0x002ED440
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

	// Token: 0x06004CC2 RID: 19650 RVA: 0x002EE47C File Offset: 0x002ED47C
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

	// Token: 0x06004CC3 RID: 19651 RVA: 0x002EE4B8 File Offset: 0x002ED4B8
	public spr\u20B5()
	{
	}

	// Token: 0x06004CC4 RID: 19652 RVA: 0x002EE4CC File Offset: 0x002ED4CC
	public spr\u20B5(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004CC5 RID: 19653 RVA: 0x002EE4E4 File Offset: 0x002ED4E4
	public spr\u20B5(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004CC6 RID: 19654 RVA: 0x002EE4F8 File Offset: 0x002ED4F8
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

	// Token: 0x06004CC7 RID: 19655 RVA: 0x002EE540 File Offset: 0x002ED540
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

	// Token: 0x06004CC8 RID: 19656 RVA: 0x002EE590 File Offset: 0x002ED590
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

	// Token: 0x040022F6 RID: 8950
	private new const int ᜀ = 2;

	// Token: 0x040022F7 RID: 8951
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
