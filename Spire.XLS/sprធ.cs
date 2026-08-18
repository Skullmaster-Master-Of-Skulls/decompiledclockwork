using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002F3 RID: 755
[spr\u2593(TBIFFRecord.BookBool)]
[CLSCompliant(false)]
internal class sprធ : BiffRecordRaw
{
	// Token: 0x06002EB9 RID: 11961 RVA: 0x001A2020 File Offset: 0x001A1020
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
		return this.ᜀ;
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x001A2064 File Offset: 0x001A1064
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

	// Token: 0x06002EBB RID: 11963 RVA: 0x001A20A8 File Offset: 0x001A10A8
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

	// Token: 0x06002EBC RID: 11964 RVA: 0x001A20E4 File Offset: 0x001A10E4
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

	// Token: 0x06002EBD RID: 11965 RVA: 0x001A2120 File Offset: 0x001A1120
	public sprធ()
	{
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x001A2134 File Offset: 0x001A1134
	public sprធ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x001A214C File Offset: 0x001A114C
	public sprធ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x001A2160 File Offset: 0x001A1160
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

	// Token: 0x06002EC1 RID: 11969 RVA: 0x001A21A8 File Offset: 0x001A11A8
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
		A_0.WriteUInt16(A_1, this.ᜀ);
		this.m_iLength = 2;
	}

	// Token: 0x04001501 RID: 5377
	[spr\u2429(0, 2)]
	private new ushort ᜀ;
}
