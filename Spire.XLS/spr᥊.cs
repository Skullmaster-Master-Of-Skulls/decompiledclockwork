using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020004FA RID: 1274
[spr\u2593(TBIFFRecord.XCT)]
[CLSCompliant(false)]
internal class spr᥊ : BiffRecordRaw
{
	// Token: 0x06004DD1 RID: 19921 RVA: 0x002F7E04 File Offset: 0x002F6E04
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

	// Token: 0x06004DD2 RID: 19922 RVA: 0x002F7E48 File Offset: 0x002F6E48
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

	// Token: 0x06004DD3 RID: 19923 RVA: 0x002F7E8C File Offset: 0x002F6E8C
	public ushort ᜀ()
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

	// Token: 0x06004DD4 RID: 19924 RVA: 0x002F7ED0 File Offset: 0x002F6ED0
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

	// Token: 0x06004DD5 RID: 19925 RVA: 0x002F7F14 File Offset: 0x002F6F14
	public virtual int ᜃ()
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

	// Token: 0x06004DD6 RID: 19926 RVA: 0x002F7F50 File Offset: 0x002F6F50
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

	// Token: 0x06004DD7 RID: 19927 RVA: 0x002F7F8C File Offset: 0x002F6F8C
	public spr᥊()
	{
	}

	// Token: 0x06004DD8 RID: 19928 RVA: 0x002F7FA0 File Offset: 0x002F6FA0
	public spr᥊(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004DD9 RID: 19929 RVA: 0x002F7FB8 File Offset: 0x002F6FB8
	public spr᥊(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004DDA RID: 19930 RVA: 0x002F7FCC File Offset: 0x002F6FCC
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

	// Token: 0x06004DDB RID: 19931 RVA: 0x002F8024 File Offset: 0x002F7024
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
		this.m_iLength = 4;
		A_0.WriteUInt16(A_1, this.ᜁ);
		A_0.WriteUInt16(A_1 + 2, this.ᜂ);
	}

	// Token: 0x04002336 RID: 9014
	private new const int ᜀ = 4;

	// Token: 0x04002337 RID: 9015
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04002338 RID: 9016
	[spr\u2429(2, 2)]
	private ushort ᜂ;
}
