using System;
using System.IO;
using Spire.Doc.Core.Escher;

// Token: 0x02000426 RID: 1062
internal class spr\u224B : spr\u23F8
{
	// Token: 0x06003B19 RID: 15129 RVA: 0x0036FA3C File Offset: 0x0036EA3C
	public void ᜁ(Stream A_0)
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
		uint num = spr\u23F8.ᜃ(A_0);
		this.ᜀ = (num & 15U);
		this.ᜁ = (num & 65520U) >> 4;
		this.ᜂ = (MSOFBT)((num & 4294901760U) >> 16);
		this.ᜃ = spr\u23F8.ᜃ(A_0);
	}

	// Token: 0x06003B1A RID: 15130 RVA: 0x0036FAB4 File Offset: 0x0036EAB4
	public void ᜀ(Stream A_0)
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
		uint num = this.ᜀ;
		num += this.ᜁ << 4;
		num = (uint)((int)num + ((uint)this.ᜂ << 16));
		spr\u23F8.ᜀ(A_0, num);
		spr\u23F8.ᜀ(A_0, this.ᜃ);
	}

	// Token: 0x06003B1B RID: 15131 RVA: 0x0036FB20 File Offset: 0x0036EB20
	public uint ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x06003B1C RID: 15132 RVA: 0x0036FB64 File Offset: 0x0036EB64
	public void ᜂ(uint A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06003B1D RID: 15133 RVA: 0x0036FBA8 File Offset: 0x0036EBA8
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
		return this.ᜁ;
	}

	// Token: 0x06003B1E RID: 15134 RVA: 0x0036FBEC File Offset: 0x0036EBEC
	public void ᜁ(uint A_0)
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

	// Token: 0x06003B1F RID: 15135 RVA: 0x0036FC30 File Offset: 0x0036EC30
	internal MSOFBT ᜂ()
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

	// Token: 0x06003B20 RID: 15136 RVA: 0x0036FC74 File Offset: 0x0036EC74
	internal void ᜀ(MSOFBT A_0)
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

	// Token: 0x06003B21 RID: 15137 RVA: 0x0036FCB8 File Offset: 0x0036ECB8
	public new uint ᜇ()
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

	// Token: 0x06003B22 RID: 15138 RVA: 0x0036FCFC File Offset: 0x0036ECFC
	public void ᜀ(uint A_0)
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

	// Token: 0x04002B80 RID: 11136
	private new uint ᜀ;

	// Token: 0x04002B81 RID: 11137
	private new uint ᜁ;

	// Token: 0x04002B82 RID: 11138
	private new MSOFBT ᜂ;

	// Token: 0x04002B83 RID: 11139
	private new uint ᜃ;
}
