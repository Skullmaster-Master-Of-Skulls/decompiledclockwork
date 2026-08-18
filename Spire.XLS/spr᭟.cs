using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000361 RID: 865
[spr\u2593(TBIFFRecord.PivotBoolean)]
[CLSCompliant(false)]
internal class spr\u1B5F : BiffRecordRaw, spr\u1929
{
	// Token: 0x060034FF RID: 13567 RVA: 0x001E584C File Offset: 0x001E484C
	public spr\u1B5F()
	{
	}

	// Token: 0x06003500 RID: 13568 RVA: 0x001E5860 File Offset: 0x001E4860
	public spr\u1B5F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003501 RID: 13569 RVA: 0x001E5878 File Offset: 0x001E4878
	public spr\u1B5F(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003502 RID: 13570 RVA: 0x001E588C File Offset: 0x001E488C
	public new bool ᜃ()
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
		return this.ᜁ == 1;
	}

	// Token: 0x06003503 RID: 13571 RVA: 0x001E58D0 File Offset: 0x001E48D0
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
		this.ᜁ = (A_0 ? 1 : 0);
	}

	// Token: 0x06003504 RID: 13572 RVA: 0x001E5920 File Offset: 0x001E4920
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

	// Token: 0x06003505 RID: 13573 RVA: 0x001E595C File Offset: 0x001E495C
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

	// Token: 0x06003506 RID: 13574 RVA: 0x001E5998 File Offset: 0x001E4998
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

	// Token: 0x06003507 RID: 13575 RVA: 0x001E59E0 File Offset: 0x001E49E0
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

	// Token: 0x06003508 RID: 13576 RVA: 0x001E5A30 File Offset: 0x001E4A30
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

	// Token: 0x06003509 RID: 13577 RVA: 0x001E5A6C File Offset: 0x001E4A6C
	object spr\u1929.ᜁ()
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
		return this.ᜃ();
	}

	// Token: 0x0600350A RID: 13578 RVA: 0x001E5AB4 File Offset: 0x001E4AB4
	void spr\u1929.ᜀ(object A_0)
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
		this.ᜀ((bool)A_0);
	}

	// Token: 0x04001724 RID: 5924
	private new const int ᜀ = 2;

	// Token: 0x04001725 RID: 5925
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
