using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000477 RID: 1143
[spr\u2593(TBIFFRecord.PrintGridlines)]
[CLSCompliant(false)]
internal class spr\u2260 : BiffRecordRaw
{
	// Token: 0x060045FE RID: 17918 RVA: 0x002A9D04 File Offset: 0x002A8D04
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

	// Token: 0x060045FF RID: 17919 RVA: 0x002A9D48 File Offset: 0x002A8D48
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

	// Token: 0x06004600 RID: 17920 RVA: 0x002A9D8C File Offset: 0x002A8D8C
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

	// Token: 0x06004601 RID: 17921 RVA: 0x002A9DC8 File Offset: 0x002A8DC8
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

	// Token: 0x06004602 RID: 17922 RVA: 0x002A9E04 File Offset: 0x002A8E04
	public spr\u2260()
	{
	}

	// Token: 0x06004603 RID: 17923 RVA: 0x002A9E18 File Offset: 0x002A8E18
	public spr\u2260(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004604 RID: 17924 RVA: 0x002A9E30 File Offset: 0x002A8E30
	public spr\u2260(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004605 RID: 17925 RVA: 0x002A9E44 File Offset: 0x002A8E44
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

	// Token: 0x06004606 RID: 17926 RVA: 0x002A9E8C File Offset: 0x002A8E8C
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

	// Token: 0x06004607 RID: 17927 RVA: 0x002A9EDC File Offset: 0x002A8EDC
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

	// Token: 0x04001FEB RID: 8171
	private new const int ᜀ = 2;

	// Token: 0x04001FEC RID: 8172
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
