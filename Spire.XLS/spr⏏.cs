using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x02000533 RID: 1331
[CLSCompliant(false)]
[sprᵴ(MsoRecords.msofbtChildAnchor)]
internal class spr\u23CF : spr\u1D3B
{
	// Token: 0x06005138 RID: 20792 RVA: 0x0032DA34 File Offset: 0x0032CA34
	public new int ᜃ()
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

	// Token: 0x06005139 RID: 20793 RVA: 0x0032DA78 File Offset: 0x0032CA78
	public void ᜁ(int A_0)
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

	// Token: 0x0600513A RID: 20794 RVA: 0x0032DABC File Offset: 0x0032CABC
	public int ᜁ()
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

	// Token: 0x0600513B RID: 20795 RVA: 0x0032DB00 File Offset: 0x0032CB00
	public new void ᜃ(int A_0)
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

	// Token: 0x0600513C RID: 20796 RVA: 0x0032DB44 File Offset: 0x0032CB44
	public new int ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x0600513D RID: 20797 RVA: 0x0032DB88 File Offset: 0x0032CB88
	public new void ᜀ(int A_0)
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

	// Token: 0x0600513E RID: 20798 RVA: 0x0032DBCC File Offset: 0x0032CBCC
	public new int ᜀ()
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

	// Token: 0x0600513F RID: 20799 RVA: 0x0032DC10 File Offset: 0x0032CC10
	public void ᜂ(int A_0)
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

	// Token: 0x06005140 RID: 20800 RVA: 0x0032DC54 File Offset: 0x0032CC54
	public spr\u23CF(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x06005141 RID: 20801 RVA: 0x0032DC68 File Offset: 0x0032CC68
	public spr\u23CF(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06005142 RID: 20802 RVA: 0x0032DC80 File Offset: 0x0032CC80
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
		this.m_iLength = 16;
		spr\u1D3B.ᜀ(A_0, this.ᜁ);
		spr\u1D3B.ᜀ(A_0, this.ᜂ);
		spr\u1D3B.ᜀ(A_0, this.ᜃ);
		spr\u1D3B.ᜀ(A_0, this.ᜄ);
	}

	// Token: 0x06005143 RID: 20803 RVA: 0x0032DCF4 File Offset: 0x0032CCF4
	public override void ᜀ(Stream A_0)
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
		this.ᜁ = spr\u1D3B.ᜄ(A_0);
		this.ᜂ = spr\u1D3B.ᜄ(A_0);
		this.ᜃ = spr\u1D3B.ᜄ(A_0);
		this.ᜄ = spr\u1D3B.ᜄ(A_0);
	}

	// Token: 0x04002449 RID: 9289
	private new const int ᜀ = 16;

	// Token: 0x0400244A RID: 9290
	[spr\u2429(0, 4, true)]
	private new int ᜁ;

	// Token: 0x0400244B RID: 9291
	[spr\u2429(4, 4, true)]
	private new int ᜂ;

	// Token: 0x0400244C RID: 9292
	[spr\u2429(8, 4, true)]
	private new int ᜃ;

	// Token: 0x0400244D RID: 9293
	[spr\u2429(12, 4, true)]
	private new int ᜄ;
}
