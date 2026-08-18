using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200049E RID: 1182
[spr\u2593(TBIFFRecord.WindowProtect)]
[CLSCompliant(false)]
internal class spr\u2520 : BiffRecordRaw
{
	// Token: 0x0600490E RID: 18702 RVA: 0x002C72C0 File Offset: 0x002C62C0
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
		return this.ᜁ == 1;
	}

	// Token: 0x0600490F RID: 18703 RVA: 0x002C7304 File Offset: 0x002C6304
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

	// Token: 0x06004910 RID: 18704 RVA: 0x002C7354 File Offset: 0x002C6354
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

	// Token: 0x06004911 RID: 18705 RVA: 0x002C7390 File Offset: 0x002C6390
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

	// Token: 0x06004912 RID: 18706 RVA: 0x002C73CC File Offset: 0x002C63CC
	public spr\u2520()
	{
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x002C73E0 File Offset: 0x002C63E0
	public spr\u2520(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x002C73F8 File Offset: 0x002C63F8
	public spr\u2520(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004915 RID: 18709 RVA: 0x002C740C File Offset: 0x002C640C
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

	// Token: 0x06004916 RID: 18710 RVA: 0x002C7454 File Offset: 0x002C6454
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
		this.m_iLength = 2;
		A_0.WriteUInt16(A_1, this.ᜁ);
	}

	// Token: 0x04002125 RID: 8485
	private new const int ᜀ = 2;

	// Token: 0x04002126 RID: 8486
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
