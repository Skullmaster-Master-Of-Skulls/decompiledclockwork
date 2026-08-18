using System;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;

// Token: 0x020001B1 RID: 433
internal class spr៧ : DocumentBase
{
	// Token: 0x060010ED RID: 4333 RVA: 0x000FDB84 File Offset: 0x000FCB84
	internal ParagraphCollection ᜅ()
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

	// Token: 0x060010EE RID: 4334 RVA: 0x000FDBC8 File Offset: 0x000FCBC8
	internal new void ᜀ(ParagraphCollection A_0)
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

	// Token: 0x060010EF RID: 4335 RVA: 0x000FDC0C File Offset: 0x000FCC0C
	internal new TableCollection ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000FDC50 File Offset: 0x000FCC50
	internal new void ᜀ(TableCollection A_0)
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

	// Token: 0x060010F1 RID: 4337 RVA: 0x000FDC94 File Offset: 0x000FCC94
	internal Body ᜂ()
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

	// Token: 0x060010F2 RID: 4338 RVA: 0x000FDCD8 File Offset: 0x000FCCD8
	internal new void ᜀ(Body A_0)
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

	// Token: 0x060010F3 RID: 4339 RVA: 0x000FDD1C File Offset: 0x000FCD1C
	public virtual DocumentObjectType ᜃ()
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
		return DocumentObjectType.SDTBlockContent;
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000FDD5C File Offset: 0x000FCD5C
	public DocumentObjectCollection ᜇ()
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

	// Token: 0x060010F5 RID: 4341 RVA: 0x000FDDA0 File Offset: 0x000FCDA0
	internal spr៧(Document A_0, spr\u1AE7 A_1) : base(A_0, A_1)
	{
		this.ᜂ = new Body(A_0, this);
		this.ᜀ = new ParagraphCollection(this.ᜂ.Items);
		this.ᜁ = new TableCollection(this.ᜂ.Items);
		this.ᜄ = new BodyRegionCollection(A_0);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000FDDFC File Offset: 0x000FCDFC
	internal spr៧ ᜄ()
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
		return (spr៧)this.CloneImpl();
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000FDE44 File Offset: 0x000FCE44
	protected virtual object ᜁ()
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
		return (spr៧)base.CloneImpl();
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x000FDE8C File Offset: 0x000FCE8C
	protected virtual void ᜆ()
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
	}

	// Token: 0x040017DD RID: 6109
	private new ParagraphCollection ᜀ;

	// Token: 0x040017DE RID: 6110
	private TableCollection ᜁ;

	// Token: 0x040017DF RID: 6111
	private Body ᜂ;

	// Token: 0x040017E0 RID: 6112
	private spr\u1AE7 ᜃ;

	// Token: 0x040017E1 RID: 6113
	private new DocumentObjectCollection ᜄ;
}
