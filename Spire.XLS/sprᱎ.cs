using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200039F RID: 927
[spr\u2593(TBIFFRecord.DefaultColWidth)]
[CLSCompliant(false)]
internal class sprᱎ : BiffRecordRaw
{
	// Token: 0x06003866 RID: 14438 RVA: 0x001F7CEC File Offset: 0x001F6CEC
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

	// Token: 0x06003867 RID: 14439 RVA: 0x001F7D30 File Offset: 0x001F6D30
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

	// Token: 0x06003868 RID: 14440 RVA: 0x001F7D74 File Offset: 0x001F6D74
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

	// Token: 0x06003869 RID: 14441 RVA: 0x001F7DB0 File Offset: 0x001F6DB0
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

	// Token: 0x0600386A RID: 14442 RVA: 0x001F7DEC File Offset: 0x001F6DEC
	public sprᱎ()
	{
	}

	// Token: 0x0600386B RID: 14443 RVA: 0x001F7E08 File Offset: 0x001F6E08
	public sprᱎ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600386C RID: 14444 RVA: 0x001F7E24 File Offset: 0x001F6E24
	public sprᱎ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600386D RID: 14445 RVA: 0x001F7E40 File Offset: 0x001F6E40
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

	// Token: 0x0600386E RID: 14446 RVA: 0x001F7E88 File Offset: 0x001F6E88
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

	// Token: 0x0600386F RID: 14447 RVA: 0x001F7ED8 File Offset: 0x001F6ED8
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

	// Token: 0x040018D7 RID: 6359
	private new const int ᜀ = 2;

	// Token: 0x040018D8 RID: 6360
	[spr\u2429(0, 2)]
	private ushort ᜁ = 8;
}
