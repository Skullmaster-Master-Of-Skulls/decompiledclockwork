using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x020004A3 RID: 1187
[sprᵴ(MsoRecords.msofbtDg)]
[CLSCompliant(false)]
internal class spr\u2608 : spr\u1D3B
{
	// Token: 0x06004963 RID: 18787 RVA: 0x002C995C File Offset: 0x002C895C
	public spr\u2608(spr\u1D3B A_0) : base(A_0)
	{
		base.ᜈ(1);
	}

	// Token: 0x06004964 RID: 18788 RVA: 0x002C9978 File Offset: 0x002C8978
	public spr\u2608(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004965 RID: 18789 RVA: 0x002C9990 File Offset: 0x002C8990
	public uint ᜁ()
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

	// Token: 0x06004966 RID: 18790 RVA: 0x002C99D4 File Offset: 0x002C89D4
	public new void ᜀ(uint A_0)
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

	// Token: 0x06004967 RID: 18791 RVA: 0x002C9A18 File Offset: 0x002C8A18
	public new int ᜀ()
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

	// Token: 0x06004968 RID: 18792 RVA: 0x002C9A5C File Offset: 0x002C8A5C
	public new void ᜀ(int A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004969 RID: 18793 RVA: 0x002C9AA0 File Offset: 0x002C8AA0
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
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
		spr\u1D3B.ᜀ(A_0, this.ᜂ);
		spr\u1D3B.ᜀ(A_0, this.ᜃ);
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x002C9AFC File Offset: 0x002C8AFC
	public override void ᜀ(Stream A_0)
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
		this.ᜂ = spr\u1D3B.ᜃ(A_0);
		this.ᜃ = spr\u1D3B.ᜄ(A_0);
	}

	// Token: 0x0600496B RID: 18795 RVA: 0x002C9B50 File Offset: 0x002C8B50
	public override int ᜁ(ExcelVersion A_0)
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

	// Token: 0x04002151 RID: 8529
	private new const int ᜀ = 1;

	// Token: 0x04002152 RID: 8530
	private new const int ᜁ = 8;

	// Token: 0x04002153 RID: 8531
	[spr\u2429(0, 4)]
	private new uint ᜂ;

	// Token: 0x04002154 RID: 8532
	[spr\u2429(4, 4)]
	private new int ᜃ;
}
