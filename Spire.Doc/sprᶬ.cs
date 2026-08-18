using System;
using Spire.Doc.Core;

// Token: 0x020002AC RID: 684
[CLSCompliant(false)]
internal class spr\u1DAC : sprᳱ
{
	// Token: 0x06002500 RID: 9472 RVA: 0x00255694 File Offset: 0x00254694
	public new spr\u1AB6 ᜊ()
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
		return (spr\u1AB6)this.ᜊ;
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x002556DC File Offset: 0x002546DC
	public spr\u1DAC(sprᬛ A_0) : base(A_0)
	{
		this.ᜋ = WordSubdocument.HeaderFooter;
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x002556F8 File Offset: 0x002546F8
	public override void ᜉ()
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
		base.ᜉ();
		this.ᜁ(this.ᜀ);
	}

	// Token: 0x06002503 RID: 9475 RVA: 0x00255748 File Offset: 0x00254748
	public new void ᜁ(int A_0)
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
		this.ᜊ().ᜁ(A_0 - 1);
		this.ᜀ(HeaderType.EvenHeader);
		base.ᜤ();
		base.ᝁ();
	}

	// Token: 0x06002504 RID: 9476 RVA: 0x002557A4 File Offset: 0x002547A4
	public new void ᜀ(HeaderType A_0)
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
		this.ᜋ();
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x002557EC File Offset: 0x002547EC
	public override void ᜀ(int A_0)
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
		this.ᜀ((HeaderType)A_0);
	}

	// Token: 0x06002506 RID: 9478 RVA: 0x00255830 File Offset: 0x00254830
	public override sprᨼ ᜇ()
	{
		int a_ = base.ᜄ(this.ᜊ().ᜎ(), 1);
		if (this.ᜅ.ᜃ().\u1712() != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return this.ᜅ.ᜃ().\u1712().ᜀ(this.ᜋ, a_);
			}
		}
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x002558B0 File Offset: 0x002548B0
	protected override void ᜋ()
	{
		if (this.ᜅ.ᜃ().\u1714() != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜎ();
				this.ᜈ = WordChunkType.Text;
				this.ᜄ.ᜃ().Position = (long)this.ᜊ().ᜀ((int)this.ᜁ);
				return;
			}
		}
		if (true)
		{
		}
		this.ᜈ = WordChunkType.DocumentEnd;
		this.ᜁ = HeaderType.InvalidValue;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x00255940 File Offset: 0x00254940
	protected override void ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				this.ᜊ = new spr\u1AB6(this.ᜅ.ᜄ());
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				if (this.ᜊ != null)
				{
					return;
				}
				if (true)
				{
				}
				num = 2;
				break;
			}
		}
	}
}
