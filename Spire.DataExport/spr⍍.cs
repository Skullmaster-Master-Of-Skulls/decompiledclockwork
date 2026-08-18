using System;

// Token: 0x02000141 RID: 321
internal class spr\u234D : sprᠺ
{
	// Token: 0x060007D7 RID: 2007 RVA: 0x0004ED84 File Offset: 0x0004DD84
	public spr\u234D(ushort A_0, ushort A_1, ushort A_2, spr\u1DCA A_3) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜁ = A_3;
		this.ᜁ.ᜀ = A_2;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x0004EDB4 File Offset: 0x0004DDB4
	public override void ᜀ(sprḗ A_0)
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
		sprᮌ.ᜀ(61456, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0);
		byte[] a_ = spr\u1DCA.ᜀ(this.ᜁ);
		A_0.ᜁ(a_, base.ᜅ());
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0004EE28 File Offset: 0x0004DE28
	public override void ᜀ(byte[] A_0, ref int A_1)
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
		sprᮌ.ᜀ(61456, base.ᜆ(), base.ᜄ(), base.ᜅ(), A_0, ref A_1);
		byte[] sourceArray = spr\u1DCA.ᜀ(this.ᜁ);
		Array.Copy(sourceArray, 0, A_0, A_1, base.ᜅ());
		A_1 += base.ᜅ();
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0004EEA8 File Offset: 0x0004DEA8
	public override int ᜀ()
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
		return sizeof(spr\u1DCA) + sizeof(spr\u1CC5);
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0004EEF0 File Offset: 0x0004DEF0
	public ushort ᜁ()
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

	// Token: 0x060007DC RID: 2012 RVA: 0x0004EF34 File Offset: 0x0004DF34
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
		this.ᜀ = A_0;
	}

	// Token: 0x04000625 RID: 1573
	private new ushort ᜀ;

	// Token: 0x04000626 RID: 1574
	private spr\u1DCA ᜁ;
}
