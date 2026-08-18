using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020005A4 RID: 1444
[spr\u2593(TBIFFRecord.Country)]
[CLSCompliant(false)]
internal class spr\u2338 : BiffRecordRaw
{
	// Token: 0x060057A1 RID: 22433 RVA: 0x0037B744 File Offset: 0x0037A744
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

	// Token: 0x060057A2 RID: 22434 RVA: 0x0037B788 File Offset: 0x0037A788
	public void ᜁ(ushort A_0)
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

	// Token: 0x060057A3 RID: 22435 RVA: 0x0037B7CC File Offset: 0x0037A7CC
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
		return this.ᜂ;
	}

	// Token: 0x060057A4 RID: 22436 RVA: 0x0037B810 File Offset: 0x0037A810
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

	// Token: 0x060057A5 RID: 22437 RVA: 0x0037B854 File Offset: 0x0037A854
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
		return 4;
	}

	// Token: 0x060057A6 RID: 22438 RVA: 0x0037B890 File Offset: 0x0037A890
	public virtual int ᜁ()
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
		return 4;
	}

	// Token: 0x060057A7 RID: 22439 RVA: 0x0037B8CC File Offset: 0x0037A8CC
	public spr\u2338()
	{
	}

	// Token: 0x060057A8 RID: 22440 RVA: 0x0037B8F0 File Offset: 0x0037A8F0
	public spr\u2338(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060057A9 RID: 22441 RVA: 0x0037B914 File Offset: 0x0037A914
	public spr\u2338(int A_0) : base(A_0)
	{
	}

	// Token: 0x060057AA RID: 22442 RVA: 0x0037B938 File Offset: 0x0037A938
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
	}

	// Token: 0x060057AB RID: 22443 RVA: 0x0037B990 File Offset: 0x0037A990
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
		this.m_iLength = 4;
	}

	// Token: 0x060057AC RID: 22444 RVA: 0x0037B9F0 File Offset: 0x0037A9F0
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
		return 4;
	}

	// Token: 0x040029AB RID: 10667
	private new const int ᜀ = 4;

	// Token: 0x040029AC RID: 10668
	[spr\u2429(0, 2)]
	private ushort ᜁ = 1;

	// Token: 0x040029AD RID: 10669
	[spr\u2429(2, 2)]
	private ushort ᜂ = 1;
}
