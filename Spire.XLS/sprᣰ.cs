using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000426 RID: 1062
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Precision)]
internal class sprᣰ : BiffRecordRaw
{
	// Token: 0x06004065 RID: 16485 RVA: 0x00242ED8 File Offset: 0x00241ED8
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

	// Token: 0x06004066 RID: 16486 RVA: 0x00242F1C File Offset: 0x00241F1C
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

	// Token: 0x06004067 RID: 16487 RVA: 0x00242F60 File Offset: 0x00241F60
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

	// Token: 0x06004068 RID: 16488 RVA: 0x00242F9C File Offset: 0x00241F9C
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

	// Token: 0x06004069 RID: 16489 RVA: 0x00242FD8 File Offset: 0x00241FD8
	public sprᣰ()
	{
	}

	// Token: 0x0600406A RID: 16490 RVA: 0x00242FF4 File Offset: 0x00241FF4
	public sprᣰ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x0600406B RID: 16491 RVA: 0x00243010 File Offset: 0x00242010
	public sprᣰ(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600406C RID: 16492 RVA: 0x0024302C File Offset: 0x0024202C
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

	// Token: 0x0600406D RID: 16493 RVA: 0x00243074 File Offset: 0x00242074
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

	// Token: 0x0600406E RID: 16494 RVA: 0x002430C4 File Offset: 0x002420C4
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

	// Token: 0x04001CD4 RID: 7380
	private new const int ᜀ = 2;

	// Token: 0x04001CD5 RID: 7381
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1;
}
