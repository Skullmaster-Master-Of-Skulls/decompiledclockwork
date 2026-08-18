using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000447 RID: 1095
[spr\u2593(TBIFFRecord.ParsedExpression)]
[CLSCompliant(false)]
internal class spr\u1B03 : BiffRecordRaw
{
	// Token: 0x060041F2 RID: 16882 RVA: 0x002502E0 File Offset: 0x0024F2E0
	public spr\u1B03()
	{
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x002502F4 File Offset: 0x0024F2F4
	public spr\u1B03(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060041F4 RID: 16884 RVA: 0x0025030C File Offset: 0x0024F30C
	public spr\u1B03(int A_0) : base(A_0)
	{
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x00250320 File Offset: 0x0024F320
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

	// Token: 0x060041F6 RID: 16886 RVA: 0x00250364 File Offset: 0x0024F364
	public ushort ᜂ()
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

	// Token: 0x060041F7 RID: 16887 RVA: 0x002503A8 File Offset: 0x0024F3A8
	public void ᜀ(ushort A_0)
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

	// Token: 0x060041F8 RID: 16888 RVA: 0x002503EC File Offset: 0x0024F3EC
	public byte[] ᜀ()
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

	// Token: 0x060041F9 RID: 16889 RVA: 0x00250430 File Offset: 0x0024F430
	public void ᜀ(byte[] A_0)
	{
		int a_ = 19;
		while (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㽈⩊⅌㩎㑐", a_));
			}
		}
		this.ᜃ = A_0;
		this.ᜁ = (ushort)this.ᜃ.Length;
	}

	// Token: 0x060041FA RID: 16890 RVA: 0x002504A4 File Offset: 0x0024F4A4
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
		this.ᜃ = new byte[(int)this.ᜁ];
		A_0.ReadArray(A_1 + 4, this.ᜃ);
	}

	// Token: 0x060041FB RID: 16891 RVA: 0x0025051C File Offset: 0x0024F51C
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
		A_0.WriteBytes(A_1 + 4, this.ᜃ);
		this.m_iLength += this.ᜃ.Length + 4;
	}

	// Token: 0x060041FC RID: 16892 RVA: 0x0025059C File Offset: 0x0024F59C
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
		return this.ᜃ.Length + 4;
	}

	// Token: 0x04001D31 RID: 7473
	private new const int ᜀ = 4;

	// Token: 0x04001D32 RID: 7474
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001D33 RID: 7475
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x04001D34 RID: 7476
	private new byte[] ᜃ;
}
