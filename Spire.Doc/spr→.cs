using System;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x02000173 RID: 371
internal abstract class spr\u2192 : spr\u23F8
{
	// Token: 0x06000CD2 RID: 3282 RVA: 0x000D5664 File Offset: 0x000D4664
	internal spr\u1D2F \u1717()
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

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000D56A8 File Offset: 0x000D46A8
	internal void ᜀ(spr\u1D2F A_0)
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

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000D56EC File Offset: 0x000D46EC
	internal spr\u2192(Document A_0)
	{
		this.ᜁ = A_0;
		this.ᜀ = new spr\u1D2F(A_0);
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x000D5714 File Offset: 0x000D4714
	internal spr\u2192(MSOFBT A_0, int A_1, Document A_2) : this(A_2)
	{
		this.ᜀ.ᜀ(A_0);
		this.ᜀ.ᜂ(A_1);
	}

	// Token: 0x06000CD6 RID: 3286
	protected abstract void ᜁ(Stream A_0);

	// Token: 0x06000CD7 RID: 3287
	protected abstract void ᜀ(Stream A_0);

	// Token: 0x06000CD8 RID: 3288
	internal new abstract spr\u2192 ᜃ();

	// Token: 0x06000CD9 RID: 3289 RVA: 0x000D5740 File Offset: 0x000D4740
	internal bool ᜀ(spr\u1D2F A_0, Stream A_1)
	{
		this.ᜀ = A_0;
		int num = (int)A_1.Position;
		this.ᜁ(A_1);
		int num2 = (int)A_1.Position - num;
		if (num2 == this.ᜀ.ᜇ())
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
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x000D57B0 File Offset: 0x000D47B0
	internal void ᜅ(Stream A_0)
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
		this.ᜀ(new spr\u1D2F(A_0, this.ᜁ), A_0);
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x000D5800 File Offset: 0x000D4800
	internal int ᜆ(Stream A_0)
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
		int num = Convert.ToInt32(A_0.Position);
		this.\u1717().ᜀ(A_0);
		int num2 = Convert.ToInt32(A_0.Position);
		this.ᜀ(A_0);
		int num3 = Convert.ToInt32(A_0.Position);
		this.\u1717().ᜀ(num3 - num2);
		A_0.Position = (long)num;
		this.\u1717().ᜀ(A_0);
		A_0.Position = (long)num3;
		return num3 - num;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x000D58A0 File Offset: 0x000D48A0
	internal new virtual void \u170D()
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
	}

	// Token: 0x04001457 RID: 5207
	private new spr\u1D2F ᜀ;

	// Token: 0x04001458 RID: 5208
	internal new Document ᜁ;
}
