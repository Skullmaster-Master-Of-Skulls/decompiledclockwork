using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200047D RID: 1149
[spr\u2593(TBIFFRecord.OleSize)]
[CLSCompliant(false)]
internal class spr\u21CC : BiffRecordRaw
{
	// Token: 0x06004652 RID: 18002 RVA: 0x002AB538 File Offset: 0x002AA538
	public ushort ᜅ()
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

	// Token: 0x06004653 RID: 18003 RVA: 0x002AB57C File Offset: 0x002AA57C
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
		return this.ᜂ;
	}

	// Token: 0x06004654 RID: 18004 RVA: 0x002AB5C0 File Offset: 0x002AA5C0
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

	// Token: 0x06004655 RID: 18005 RVA: 0x002AB604 File Offset: 0x002AA604
	public new ushort ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x002AB648 File Offset: 0x002AA648
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004657 RID: 18007 RVA: 0x002AB68C File Offset: 0x002AA68C
	public byte ᜆ()
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
		return this.ᜄ;
	}

	// Token: 0x06004658 RID: 18008 RVA: 0x002AB6D0 File Offset: 0x002AA6D0
	public void ᜁ(byte A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004659 RID: 18009 RVA: 0x002AB714 File Offset: 0x002AA714
	public byte ᜁ()
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
		return this.ᜅ;
	}

	// Token: 0x0600465A RID: 18010 RVA: 0x002AB758 File Offset: 0x002AA758
	public void ᜀ(byte A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0600465B RID: 18011 RVA: 0x002AB79C File Offset: 0x002AA79C
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
		return 8;
	}

	// Token: 0x0600465C RID: 18012 RVA: 0x002AB7D8 File Offset: 0x002AA7D8
	public virtual int ᜄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return 8;
	}

	// Token: 0x0600465D RID: 18013 RVA: 0x002AB814 File Offset: 0x002AA814
	public spr\u21CC()
	{
	}

	// Token: 0x0600465E RID: 18014 RVA: 0x002AB828 File Offset: 0x002AA828
	public spr\u21CC(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600465F RID: 18015 RVA: 0x002AB840 File Offset: 0x002AA840
	public spr\u21CC(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004660 RID: 18016 RVA: 0x002AB854 File Offset: 0x002AA854
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
		this.ᜃ = A_0.ReadUInt16(A_1 + 4);
		this.ᜄ = A_0.ReadByte(A_1 + 6);
		this.ᜅ = A_0.ReadByte(A_1 + 7);
	}

	// Token: 0x06004661 RID: 18017 RVA: 0x002AB8D8 File Offset: 0x002AA8D8
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
		A_0.WriteUInt16(A_1 + 4, this.ᜃ);
		A_0.WriteByte(A_1 + 6, this.ᜄ);
		A_0.WriteByte(A_1 + 7, this.ᜅ);
		this.m_iLength = 8;
	}

	// Token: 0x06004662 RID: 18018 RVA: 0x002AB964 File Offset: 0x002AA964
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
		return 8;
	}

	// Token: 0x04002019 RID: 8217
	private new const int ᜀ = 8;

	// Token: 0x0400201A RID: 8218
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x0400201B RID: 8219
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x0400201C RID: 8220
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x0400201D RID: 8221
	[spr\u2429(6, 1)]
	private byte ᜄ;

	// Token: 0x0400201E RID: 8222
	[spr\u2429(7, 1)]
	private byte ᜅ;
}
