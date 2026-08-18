using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x0200047C RID: 1148
[spr\u2593(TBIFFRecord.PasswordRev4)]
[CLSCompliant(false)]
internal class spr\u1938 : BiffRecordRaw
{
	// Token: 0x06004648 RID: 17992 RVA: 0x002AB324 File Offset: 0x002AA324
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

	// Token: 0x06004649 RID: 17993 RVA: 0x002AB368 File Offset: 0x002AA368
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

	// Token: 0x0600464A RID: 17994 RVA: 0x002AB3AC File Offset: 0x002AA3AC
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

	// Token: 0x0600464B RID: 17995 RVA: 0x002AB3E8 File Offset: 0x002AA3E8
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

	// Token: 0x0600464C RID: 17996 RVA: 0x002AB424 File Offset: 0x002AA424
	public spr\u1938()
	{
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x002AB438 File Offset: 0x002AA438
	public spr\u1938(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x002AB450 File Offset: 0x002AA450
	public spr\u1938(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x002AB464 File Offset: 0x002AA464
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

	// Token: 0x06004650 RID: 18000 RVA: 0x002AB4AC File Offset: 0x002AA4AC
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

	// Token: 0x06004651 RID: 18001 RVA: 0x002AB4FC File Offset: 0x002AA4FC
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

	// Token: 0x04002017 RID: 8215
	private new const int ᜀ = 2;

	// Token: 0x04002018 RID: 8216
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
