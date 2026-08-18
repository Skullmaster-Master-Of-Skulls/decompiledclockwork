using System;
using System.IO;

// Token: 0x0200025B RID: 603
internal class spr\u22A5
{
	// Token: 0x06001E50 RID: 7760 RVA: 0x001E219C File Offset: 0x001E119C
	internal Stream ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x001E21E0 File Offset: 0x001E11E0
	internal string ᜂ()
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

	// Token: 0x06001E52 RID: 7762 RVA: 0x001E2224 File Offset: 0x001E1224
	internal void ᜀ(string A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x001E2268 File Offset: 0x001E1268
	public spr\u22A5(Stream A_0)
	{
		if (A_0 == null)
		{
			this.ᜀ = new MemoryStream();
			return;
		}
		byte[] buffer = new byte[A_0.Length];
		A_0.Position = 0L;
		A_0.Read(buffer, 0, (int)A_0.Length);
		this.ᜀ = new MemoryStream(buffer);
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x001E22C0 File Offset: 0x001E12C0
	internal spr\u22A5 ᜀ()
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
		spr\u22A5 spr_u22A = new spr\u22A5(this.ᜀ);
		spr_u22A.ᜀ(this.ᜁ);
		return spr_u22A;
	}

	// Token: 0x04001FAF RID: 8111
	protected Stream ᜀ;

	// Token: 0x04001FB0 RID: 8112
	protected string ᜁ;
}
