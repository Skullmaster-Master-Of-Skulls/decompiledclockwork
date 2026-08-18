using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003D4 RID: 980
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ExternCount)]
internal class sprṘ : BiffRecordRaw
{
	// Token: 0x06003B8D RID: 15245 RVA: 0x002156AC File Offset: 0x002146AC
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

	// Token: 0x06003B8E RID: 15246 RVA: 0x002156F0 File Offset: 0x002146F0
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

	// Token: 0x06003B8F RID: 15247 RVA: 0x00215734 File Offset: 0x00214734
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

	// Token: 0x06003B90 RID: 15248 RVA: 0x00215770 File Offset: 0x00214770
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

	// Token: 0x06003B91 RID: 15249 RVA: 0x002157AC File Offset: 0x002147AC
	public sprṘ()
	{
	}

	// Token: 0x06003B92 RID: 15250 RVA: 0x002157C0 File Offset: 0x002147C0
	public sprṘ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x002157D8 File Offset: 0x002147D8
	public sprṘ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003B94 RID: 15252 RVA: 0x002157EC File Offset: 0x002147EC
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

	// Token: 0x06003B95 RID: 15253 RVA: 0x00215834 File Offset: 0x00214834
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

	// Token: 0x06003B96 RID: 15254 RVA: 0x00215884 File Offset: 0x00214884
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

	// Token: 0x040019DB RID: 6619
	private new const int ᜀ = 2;

	// Token: 0x040019DC RID: 6620
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
