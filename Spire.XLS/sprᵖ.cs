using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002BA RID: 698
[spr\u2593(TBIFFRecord.Delta)]
[CLSCompliant(false)]
internal class spr\u1D56 : BiffRecordRaw
{
	// Token: 0x06002A3C RID: 10812 RVA: 0x0017B640 File Offset: 0x0017A640
	public double ᜁ()
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

	// Token: 0x06002A3D RID: 10813 RVA: 0x0017B684 File Offset: 0x0017A684
	public void ᜀ(double A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x0017B6C8 File Offset: 0x0017A6C8
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

	// Token: 0x06002A3F RID: 10815 RVA: 0x0017B704 File Offset: 0x0017A704
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
		return 8;
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0017B740 File Offset: 0x0017A740
	public spr\u1D56()
	{
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x0017B764 File Offset: 0x0017A764
	public spr\u1D56(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x0017B788 File Offset: 0x0017A788
	public spr\u1D56(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0017B7AC File Offset: 0x0017A7AC
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
		this.ᜂ = A_0.ReadDouble(A_1);
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0017B7F4 File Offset: 0x0017A7F4
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
		this.m_iLength = 8;
		A_0.WriteDouble(A_1, this.ᜂ);
	}

	// Token: 0x0400140C RID: 5132
	public new const double ᜀ = 0.001;

	// Token: 0x0400140D RID: 5133
	private const int ᜁ = 8;

	// Token: 0x0400140E RID: 5134
	[spr\u2429(0, 8, TFieldType.Float)]
	private double ᜂ = 0.001;
}
