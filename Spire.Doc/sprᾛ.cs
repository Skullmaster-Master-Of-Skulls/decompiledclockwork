using System;
using System.Drawing;
using Spire.CompoundFile.Doc;

// Token: 0x0200039F RID: 927
internal class spr\u1F9B : sprố
{
	// Token: 0x0600344F RID: 13391 RVA: 0x003012F0 File Offset: 0x003002F0
	internal spr\u1F9B(sprᩍ A_0)
	{
		int a_ = 6;
		this.ᜇ = SizeF.Empty;
		this.ᜈ = RectangleF.Empty;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("Ὣ٭ᅯɱᅳ", a_));
		}
		this.ᜉ = A_0;
		this.ᜀ = (float)A_0.\u177D();
	}

	// Token: 0x06003450 RID: 13392 RVA: 0x00301350 File Offset: 0x00300350
	internal spr\u1F9B(sprᩍ A_0, SizeF A_1) : this(A_0)
	{
		this.ᜇ = A_1;
	}

	// Token: 0x06003451 RID: 13393 RVA: 0x0030136C File Offset: 0x0030036C
	internal spr\u1F9B(sprᩍ A_0, SizeF A_1, float A_2) : this(A_0, A_1)
	{
		if (A_2 > 0f)
		{
			this.ᜀ = A_2;
		}
	}

	// Token: 0x06003452 RID: 13394 RVA: 0x00301394 File Offset: 0x00300394
	internal spr\u1F9B(SizeF A_0, float A_1)
	{
		this.ᜇ = SizeF.Empty;
		this.ᜈ = RectangleF.Empty;
		base..ctor();
		this.ᜇ = A_0;
		if (A_1 > 0f)
		{
			this.ᜀ = A_1;
		}
	}

	// Token: 0x06003453 RID: 13395 RVA: 0x003013D8 File Offset: 0x003003D8
	internal spr\u1F9B(sprᩍ A_0, SizeF A_1, float A_2, byte[] A_3, bool A_4, bool A_5, bool A_6, bool A_7) : this(A_0, A_1, A_2)
	{
		this.ᜁ = A_3;
		this.ᜄ = A_4;
		this.ᜂ = A_5;
		this.ᜅ = A_6;
		this.ᜃ = A_7;
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x00301418 File Offset: 0x00300418
	internal spr\u1F9B \u170D()
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
		return (spr\u1F9B)base.MemberwiseClone();
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x00301460 File Offset: 0x00300460
	internal float ᜅ()
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

	// Token: 0x06003456 RID: 13398 RVA: 0x003014A4 File Offset: 0x003004A4
	public RectangleF ᜌ()
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
		return this.ᜈ;
	}

	// Token: 0x06003457 RID: 13399 RVA: 0x003014E8 File Offset: 0x003004E8
	public void ᜀ(RectangleF A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06003458 RID: 13400 RVA: 0x0030152C File Offset: 0x0030052C
	internal sprᩍ ᜁ()
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
		return this.ᜉ;
	}

	// Token: 0x06003459 RID: 13401 RVA: 0x00301570 File Offset: 0x00300570
	internal SizeF ᜇ()
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
		return this.ᜇ;
	}

	// Token: 0x0600345A RID: 13402 RVA: 0x003015B4 File Offset: 0x003005B4
	internal void ᜀ(SizeF A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x0600345B RID: 13403 RVA: 0x003015F8 File Offset: 0x003005F8
	internal bool ᜊ()
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
		return this.ᜆ;
	}

	// Token: 0x0600345C RID: 13404 RVA: 0x0030163C File Offset: 0x0030063C
	internal bool ᜋ()
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
		return this.ᜅ;
	}

	// Token: 0x0600345D RID: 13405 RVA: 0x00301680 File Offset: 0x00300680
	internal bool ᜆ()
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
		return this.ᜄ;
	}

	// Token: 0x0600345E RID: 13406 RVA: 0x003016C4 File Offset: 0x003006C4
	internal bool ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x0600345F RID: 13407 RVA: 0x00301708 File Offset: 0x00300708
	internal bool ᜈ()
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

	// Token: 0x06003460 RID: 13408 RVA: 0x0030174C File Offset: 0x0030074C
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
		return this.ᜁ;
	}

	// Token: 0x06003461 RID: 13409 RVA: 0x00301790 File Offset: 0x00300790
	internal sprᲨ ᜃ()
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
		return this.ᜊ;
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x003017D4 File Offset: 0x003007D4
	internal void ᜀ(sprᲨ A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x00301818 File Offset: 0x00300818
	internal bool ᜀ()
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
		return false;
	}

	// Token: 0x06003464 RID: 13412 RVA: 0x00301854 File Offset: 0x00300854
	internal bool ᜂ()
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
		return false;
	}

	// Token: 0x04002850 RID: 10320
	private float ᜀ;

	// Token: 0x04002851 RID: 10321
	private byte[] ᜁ;

	// Token: 0x04002852 RID: 10322
	private bool ᜂ;

	// Token: 0x04002853 RID: 10323
	private bool ᜃ;

	// Token: 0x04002854 RID: 10324
	private bool ᜄ;

	// Token: 0x04002855 RID: 10325
	private bool ᜅ;

	// Token: 0x04002856 RID: 10326
	private bool ᜆ;

	// Token: 0x04002857 RID: 10327
	private SizeF ᜇ;

	// Token: 0x04002858 RID: 10328
	private RectangleF ᜈ;

	// Token: 0x04002859 RID: 10329
	private sprᩍ ᜉ;

	// Token: 0x0400285A RID: 10330
	private sprᲨ ᜊ;
}
