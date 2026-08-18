using System;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004AA RID: 1194
[spr\u2593(TBIFFRecord.Compatibility)]
internal class sprᬡ : BiffRecordRaw
{
	// Token: 0x060049C7 RID: 18887 RVA: 0x002CBC8C File Offset: 0x002CAC8C
	public sprᬡ()
	{
		this.ᜀ = new spr\u200E();
		this.ᜀ.ᜀ(2188);
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x002CBCBC File Offset: 0x002CACBC
	public uint ᜀ()
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

	// Token: 0x060049C9 RID: 18889 RVA: 0x002CBD00 File Offset: 0x002CAD00
	public void ᜀ(uint A_0)
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

	// Token: 0x060049CA RID: 18890 RVA: 0x002CBD44 File Offset: 0x002CAD44
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt32(A_1 + 12);
	}

	// Token: 0x060049CB RID: 18891 RVA: 0x002CBD90 File Offset: 0x002CAD90
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜀ.ᜂ());
		A_0.WriteUInt16(A_1 + 2, this.ᜀ.ᜀ());
		A_0.WriteInt64(A_1 + 4, 0L);
		A_0.WriteUInt32(A_1 + 12, this.ᜁ);
	}

	// Token: 0x060049CC RID: 18892 RVA: 0x002CBE0C File Offset: 0x002CAE0C
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
		return 16;
	}

	// Token: 0x04002176 RID: 8566
	private new spr\u200E ᜀ;

	// Token: 0x04002177 RID: 8567
	private uint ᜁ;
}
