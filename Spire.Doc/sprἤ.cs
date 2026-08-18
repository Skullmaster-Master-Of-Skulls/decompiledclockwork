using System;
using System.IO;

// Token: 0x020003BF RID: 959
internal class sprἬ : spr\u25B1
{
	// Token: 0x06003626 RID: 13862 RVA: 0x0032DC80 File Offset: 0x0032CC80
	internal byte[] ᜁ()
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

	// Token: 0x06003627 RID: 13863 RVA: 0x0032DCC4 File Offset: 0x0032CCC4
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003628 RID: 13864 RVA: 0x0032DD08 File Offset: 0x0032CD08
	internal sprἬ(int A_0, bool A_1, int A_2) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
	}

	// Token: 0x06003629 RID: 13865 RVA: 0x0032DD24 File Offset: 0x0032CD24
	internal void ᜁ(Stream A_0)
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
		this.ᜁ = new byte[this.ᜀ];
		A_0.Read(this.ᜁ, 0, this.ᜀ);
	}

	// Token: 0x0600362A RID: 13866 RVA: 0x0032DD84 File Offset: 0x0032CD84
	internal override void ᜀ(Stream A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int num = base.ᜂ();
		num |= (base.ᜄ() ? 16384 : 0);
		num |= 32768;
		spr\u23F8.ᜀ(A_0, (short)num);
		spr\u23F8.ᜁ(A_0, this.ᜁ.Length);
	}

	// Token: 0x0600362B RID: 13867 RVA: 0x0032DDFC File Offset: 0x0032CDFC
	internal void ᜂ(Stream A_0)
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
		A_0.Write(this.ᜁ, 0, this.ᜁ.Length);
	}

	// Token: 0x0600362C RID: 13868 RVA: 0x0032DE4C File Offset: 0x0032CE4C
	internal override spr\u25B1 ᜀ()
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
		sprἬ sprἬ = new sprἬ(base.ᜂ(), base.ᜄ(), this.ᜀ);
		sprἬ.ᜁ = new byte[this.ᜀ];
		this.ᜁ.CopyTo(sprἬ.ᜁ, 0);
		return sprἬ;
	}

	// Token: 0x04002979 RID: 10617
	private new int ᜀ;

	// Token: 0x0400297A RID: 10618
	private new byte[] ᜁ;
}
