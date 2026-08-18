using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003C5 RID: 965
[spr\u2593(TBIFFRecord.SelectionInfo)]
[CLSCompliant(false)]
internal class sprᲓ : BiffRecordRaw
{
	// Token: 0x06003AB6 RID: 15030 RVA: 0x0020F828 File Offset: 0x0020E828
	public sprᲓ()
	{
	}

	// Token: 0x06003AB7 RID: 15031 RVA: 0x0020F83C File Offset: 0x0020E83C
	public sprᲓ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003AB8 RID: 15032 RVA: 0x0020F854 File Offset: 0x0020E854
	public sprᲓ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003AB9 RID: 15033 RVA: 0x0020F868 File Offset: 0x0020E868
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

	// Token: 0x06003ABA RID: 15034 RVA: 0x0020F8AC File Offset: 0x0020E8AC
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

	// Token: 0x06003ABB RID: 15035 RVA: 0x0020F8F0 File Offset: 0x0020E8F0
	public ushort ᜁ()
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

	// Token: 0x06003ABC RID: 15036 RVA: 0x0020F934 File Offset: 0x0020E934
	public void ᜁ(ushort A_0)
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

	// Token: 0x06003ABD RID: 15037 RVA: 0x0020F978 File Offset: 0x0020E978
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
		return 26;
	}

	// Token: 0x06003ABE RID: 15038 RVA: 0x0020F9B8 File Offset: 0x0020E9B8
	public virtual int ᜂ()
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
		return 26;
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x0020F9F8 File Offset: 0x0020E9F8
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
		this.ᜂ = A_0.ReadUInt16(A_1 + 2);
	}

	// Token: 0x06003AC0 RID: 15040 RVA: 0x0020FA50 File Offset: 0x0020EA50
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
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
		A_0.WriteByte(25, 0);
		this.m_iLength = 26;
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x0020FAB8 File Offset: 0x0020EAB8
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
		return 26;
	}

	// Token: 0x0400199A RID: 6554
	private new const int ᜀ = 26;

	// Token: 0x0400199B RID: 6555
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x0400199C RID: 6556
	[spr\u2429(2, 2)]
	private ushort ᜂ;
}
