using System;
using System.IO;
using Spire.Doc;

// Token: 0x020001A4 RID: 420
internal class sprᩉ : spr\u2192
{
	// Token: 0x06001039 RID: 4153 RVA: 0x000F8980 File Offset: 0x000F7980
	internal byte[] ᜉ()
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

	// Token: 0x0600103A RID: 4154 RVA: 0x000F89C4 File Offset: 0x000F79C4
	internal void ᜀ(byte[] A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x000F8A08 File Offset: 0x000F7A08
	internal sprᩉ(Document A_0) : base(A_0)
	{
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x000F8A1C File Offset: 0x000F7A1C
	protected override void ᜁ(Stream A_0)
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
		this.ᜀ = new byte[base.\u1717().ᜇ()];
		A_0.Read(this.ᜀ, 0, base.\u1717().ᜇ());
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x000F8A88 File Offset: 0x000F7A88
	protected override void ᜀ(Stream A_0)
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
		A_0.Write(this.ᜀ, 0, this.ᜀ.Length);
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x000F8AD8 File Offset: 0x000F7AD8
	internal virtual spr\u2192 ᜈ()
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
		sprᩉ sprᩉ = new sprᩉ(this.ᜁ);
		sprᩉ.ᜀ = new byte[this.ᜀ.Length];
		this.ᜀ.CopyTo(sprᩉ.ᜀ, 0);
		sprᩉ.ᜀ(base.\u1717().ᜆ());
		sprᩉ.ᜁ = this.ᜁ;
		return sprᩉ;
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x000F8B64 File Offset: 0x000F7B64
	internal override void \u170D()
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
		base.\u170D();
		this.ᜀ = null;
	}

	// Token: 0x040017A8 RID: 6056
	private new byte[] ᜀ;
}
