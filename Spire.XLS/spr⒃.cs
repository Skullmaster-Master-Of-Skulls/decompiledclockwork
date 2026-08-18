using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003D5 RID: 981
[spr\u2593(TBIFFRecord.Codepage)]
[CLSCompliant(false)]
internal class spr\u2483 : BiffRecordRaw
{
	// Token: 0x06003B97 RID: 15255 RVA: 0x002158C0 File Offset: 0x002148C0
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

	// Token: 0x06003B98 RID: 15256 RVA: 0x00215904 File Offset: 0x00214904
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

	// Token: 0x06003B99 RID: 15257 RVA: 0x00215948 File Offset: 0x00214948
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

	// Token: 0x06003B9A RID: 15258 RVA: 0x00215984 File Offset: 0x00214984
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

	// Token: 0x06003B9B RID: 15259 RVA: 0x002159C0 File Offset: 0x002149C0
	public spr\u2483()
	{
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x002159E0 File Offset: 0x002149E0
	public spr\u2483(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x00215A00 File Offset: 0x00214A00
	public spr\u2483(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x00215A20 File Offset: 0x00214A20
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

	// Token: 0x06003B9F RID: 15263 RVA: 0x00215A68 File Offset: 0x00214A68
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

	// Token: 0x06003BA0 RID: 15264 RVA: 0x00215AB8 File Offset: 0x00214AB8
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

	// Token: 0x040019DD RID: 6621
	private new const int ᜀ = 2;

	// Token: 0x040019DE RID: 6622
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1200;
}
