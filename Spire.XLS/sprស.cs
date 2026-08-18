using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000539 RID: 1337
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.DSF)]
internal class sprស : BiffRecordRaw
{
	// Token: 0x06005175 RID: 20853 RVA: 0x0032E970 File Offset: 0x0032D970
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

	// Token: 0x06005176 RID: 20854 RVA: 0x0032E9B4 File Offset: 0x0032D9B4
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

	// Token: 0x06005177 RID: 20855 RVA: 0x0032E9F8 File Offset: 0x0032D9F8
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

	// Token: 0x06005178 RID: 20856 RVA: 0x0032EA34 File Offset: 0x0032DA34
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

	// Token: 0x06005179 RID: 20857 RVA: 0x0032EA70 File Offset: 0x0032DA70
	public sprស()
	{
	}

	// Token: 0x0600517A RID: 20858 RVA: 0x0032EA84 File Offset: 0x0032DA84
	public sprស(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600517B RID: 20859 RVA: 0x0032EA9C File Offset: 0x0032DA9C
	public sprស(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600517C RID: 20860 RVA: 0x0032EAB0 File Offset: 0x0032DAB0
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

	// Token: 0x0600517D RID: 20861 RVA: 0x0032EAF8 File Offset: 0x0032DAF8
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

	// Token: 0x0600517E RID: 20862 RVA: 0x0032EB48 File Offset: 0x0032DB48
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

	// Token: 0x04002459 RID: 9305
	private new const int ᜀ = 2;

	// Token: 0x0400245A RID: 9306
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
