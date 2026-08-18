using System;
using System.Drawing;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x02000238 RID: 568
internal class spr\u2379 : spr\u2192
{
	// Token: 0x06001B10 RID: 6928 RVA: 0x001C57F4 File Offset: 0x001C47F4
	internal spr\u2379(Document A_0) : base(MSOFBT.msofbtSpgr, 1, A_0)
	{
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x001C5824 File Offset: 0x001C4824
	protected override void ᜁ(Stream A_0)
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
		this.ᜀ = spr\u23F8.ᜁ(A_0);
		this.ᜁ = spr\u23F8.ᜁ(A_0);
		this.ᜂ = spr\u23F8.ᜁ(A_0);
		this.ᜃ = spr\u23F8.ᜁ(A_0);
		this.ᜄ = new Point(this.ᜀ, this.ᜁ);
		this.ᜅ = new Size(this.ᜂ - this.ᜀ, this.ᜃ - this.ᜁ);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x001C58CC File Offset: 0x001C48CC
	protected override void ᜀ(Stream A_0)
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
		spr\u23F8.ᜁ(A_0, this.ᜀ);
		spr\u23F8.ᜁ(A_0, this.ᜁ);
		spr\u23F8.ᜁ(A_0, this.ᜂ);
		spr\u23F8.ᜁ(A_0, this.ᜃ);
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x001C5938 File Offset: 0x001C4938
	internal virtual spr\u2192 ᜁ()
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
		spr\u2379 spr_u = (spr\u2379)base.MemberwiseClone();
		spr_u.ᜁ = this.ᜁ;
		return spr_u;
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x001C598C File Offset: 0x001C498C
	internal Point ᜀ()
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

	// Token: 0x06001B15 RID: 6933 RVA: 0x001C59D0 File Offset: 0x001C49D0
	internal void ᜀ(Point A_0)
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

	// Token: 0x06001B16 RID: 6934 RVA: 0x001C5A14 File Offset: 0x001C4A14
	internal Size ᜂ()
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

	// Token: 0x06001B17 RID: 6935 RVA: 0x001C5A58 File Offset: 0x001C4A58
	internal void ᜀ(Size A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x04001EA7 RID: 7847
	private new int ᜀ;

	// Token: 0x04001EA8 RID: 7848
	private new int ᜁ;

	// Token: 0x04001EA9 RID: 7849
	private new int ᜂ;

	// Token: 0x04001EAA RID: 7850
	private new int ᜃ;

	// Token: 0x04001EAB RID: 7851
	private new Point ᜄ = Point.Empty;

	// Token: 0x04001EAC RID: 7852
	private new Size ᜅ = Size.Empty;
}
