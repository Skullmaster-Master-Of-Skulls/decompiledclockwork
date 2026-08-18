using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004FE RID: 1278
[spr\u2593(TBIFFRecord.RefreshAll)]
[CLSCompliant(false)]
internal class spr\u2371 : BiffRecordRaw
{
	// Token: 0x06004E02 RID: 19970 RVA: 0x002F8C1C File Offset: 0x002F7C1C
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

	// Token: 0x06004E03 RID: 19971 RVA: 0x002F8C60 File Offset: 0x002F7C60
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

	// Token: 0x06004E04 RID: 19972 RVA: 0x002F8CA4 File Offset: 0x002F7CA4
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

	// Token: 0x06004E05 RID: 19973 RVA: 0x002F8CE0 File Offset: 0x002F7CE0
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

	// Token: 0x06004E06 RID: 19974 RVA: 0x002F8D1C File Offset: 0x002F7D1C
	public spr\u2371()
	{
	}

	// Token: 0x06004E07 RID: 19975 RVA: 0x002F8D30 File Offset: 0x002F7D30
	public spr\u2371(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E08 RID: 19976 RVA: 0x002F8D48 File Offset: 0x002F7D48
	public spr\u2371(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E09 RID: 19977 RVA: 0x002F8D5C File Offset: 0x002F7D5C
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

	// Token: 0x06004E0A RID: 19978 RVA: 0x002F8DA4 File Offset: 0x002F7DA4
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

	// Token: 0x06004E0B RID: 19979 RVA: 0x002F8DF4 File Offset: 0x002F7DF4
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

	// Token: 0x04002349 RID: 9033
	private new const int ᜀ = 2;

	// Token: 0x0400234A RID: 9034
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
