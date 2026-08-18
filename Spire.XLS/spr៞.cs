using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200053B RID: 1339
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.DateWindow1904)]
internal class spr\u17DE : BiffRecordRaw
{
	// Token: 0x0600518F RID: 20879 RVA: 0x0032F028 File Offset: 0x0032E028
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

	// Token: 0x06005190 RID: 20880 RVA: 0x0032F06C File Offset: 0x0032E06C
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

	// Token: 0x06005191 RID: 20881 RVA: 0x0032F0B0 File Offset: 0x0032E0B0
	public new bool ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x06005192 RID: 20882 RVA: 0x0032F0F4 File Offset: 0x0032E0F4
	public void ᜀ(bool A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06005193 RID: 20883 RVA: 0x0032F138 File Offset: 0x0032E138
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
		return 2;
	}

	// Token: 0x06005194 RID: 20884 RVA: 0x0032F174 File Offset: 0x0032E174
	public virtual int ᜀ()
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

	// Token: 0x06005195 RID: 20885 RVA: 0x0032F1B0 File Offset: 0x0032E1B0
	public spr\u17DE()
	{
	}

	// Token: 0x06005196 RID: 20886 RVA: 0x0032F1C4 File Offset: 0x0032E1C4
	public spr\u17DE(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005197 RID: 20887 RVA: 0x0032F1DC File Offset: 0x0032E1DC
	public spr\u17DE(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005198 RID: 20888 RVA: 0x0032F1F0 File Offset: 0x0032E1F0
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
		this.ᜁ = A_0.ReadBit(A_1, 0);
	}

	// Token: 0x06005199 RID: 20889 RVA: 0x0032F248 File Offset: 0x0032E248
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
		A_0.WriteBit(A_1, this.ᜁ, 0);
		this.m_iLength = 2;
	}

	// Token: 0x04002461 RID: 9313
	[spr\u2429(0, 2)]
	private new ushort ᜀ;

	// Token: 0x04002462 RID: 9314
	[spr\u2429(0, 0, TFieldType.Bit)]
	private bool ᜁ;
}
