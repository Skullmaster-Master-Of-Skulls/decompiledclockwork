using System;
using System.Collections.Generic;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x02000319 RID: 793
internal class spr\u173A : Style, spr\u2179
{
	// Token: 0x06002B27 RID: 11047 RVA: 0x002A5948 File Offset: 0x002A4948
	public ParagraphFormat ᜅ()
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

	// Token: 0x06002B28 RID: 11048 RVA: 0x002A598C File Offset: 0x002A498C
	public ListFormat ᜉ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_48:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24;
			case 1:
				this.ᜁ = new ListFormat(base.Document, this);
				num = 2;
				continue;
			case 2:
				goto IL_6C;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		IL_40:
		if (this.ᜁ == null)
		{
			goto IL_48;
		}
		IL_6C:
		return this.ᜁ;
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x002A5A18 File Offset: 0x002A4A18
	public spr\u2021 ᜊ()
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
		return this.ᜂ;
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x002A5A5C File Offset: 0x002A4A5C
	public spr\u20C7 ᜈ()
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
		return this.ᜃ;
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x002A5AA0 File Offset: 0x002A4AA0
	public sprᦣ ᜃ()
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

	// Token: 0x06002B2C RID: 11052 RVA: 0x002A5AE4 File Offset: 0x002A4AE4
	internal new spr\u173A ᜆ()
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
		return base.BaseStyle as spr\u173A;
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x002A5B2C File Offset: 0x002A4B2C
	public virtual StyleType ᜄ()
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
		return StyleType.TableStyle;
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x002A5B68 File Offset: 0x002A4B68
	internal Dictionary<ConditionalFormattingCode, sprῊ> ᜀ()
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

	// Token: 0x06002B2F RID: 11055 RVA: 0x002A5BAC File Offset: 0x002A4BAC
	internal spr\u173A(IDocument A_0) : base((Document)A_0)
	{
		this.ᜀ = new ParagraphFormat(base.Document);
		this.ᜀ.ᜀ(this);
		this.ᜂ = new spr\u2021(base.Document);
		this.ᜂ.ᜀ(this);
		this.ᜃ = new spr\u20C7(base.Document);
		this.ᜃ.ᜀ(this);
		this.ᜄ = new sprᦣ(base.Document);
		this.ᜄ.ᜀ(this);
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x002A5C44 File Offset: 0x002A4C44
	internal sprῊ ᜀ(ConditionalFormattingCode A_0)
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
		sprῊ sprῊ = new sprῊ(A_0, base.Document);
		this.ᜅ.Add(A_0, sprῊ);
		return sprῊ;
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x002A5C9C File Offset: 0x002A4C9C
	public virtual void ᜀ(string A_0)
	{
		for (;;)
		{
			for (;;)
			{
				base.ApplyBaseStyle(A_0);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.ApplyBase(this.ᜆ().ᜅ());
						this.ᜂ.ApplyBase(this.ᜆ().ᜊ());
						this.ᜃ.ApplyBase(this.ᜆ().ᜈ());
						this.ᜄ.ApplyBase(this.ᜆ().ᜃ());
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						if (this.ᜆ() == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x002A5D74 File Offset: 0x002A4D74
	public virtual IStyle ᜇ()
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
		return (spr\u173A)this.CloneImpl();
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x002A5DBC File Offset: 0x002A4DBC
	protected virtual object ᜂ()
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
		spr\u173A spr_u173A = (spr\u173A)base.CloneImpl();
		spr_u173A.ᜀ = new ParagraphFormat(base.Document);
		spr_u173A.ᜀ.ImportContainer(this.ᜅ());
		spr_u173A.ᜀ.ᜀ(spr_u173A);
		spr_u173A.ᜂ = new spr\u2021(base.Document);
		spr_u173A.ᜂ.ImportContainer(this.ᜊ());
		spr_u173A.ᜂ.ᜀ(this);
		spr_u173A.ᜃ = new spr\u20C7(base.Document);
		spr_u173A.ᜃ.ImportContainer(this.ᜈ());
		spr_u173A.ᜃ.ᜀ(this);
		spr_u173A.ᜄ = new sprᦣ(base.Document);
		spr_u173A.ᜄ.ImportContainer(this.ᜃ());
		spr_u173A.ᜄ.ᜀ(this);
		return spr_u173A;
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x002A5EBC File Offset: 0x002A4EBC
	internal virtual void ᜁ()
	{
		for (;;)
		{
			base.Close();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.Close();
					this.ᜀ = null;
					num = 9;
					continue;
				case 1:
					this.ᜃ.Close();
					this.ᜃ = null;
					num = 11;
					continue;
				case 2:
					if (this.ᜄ != null)
					{
						num = 10;
						continue;
					}
					return;
				case 3:
					if (this.ᜀ != null)
					{
						num = 0;
						continue;
					}
					goto IL_BE;
				case 4:
					if (this.ᜃ != null)
					{
						num = 1;
						continue;
					}
					goto IL_126;
				case 5:
					goto IL_82;
				case 6:
					if (this.ᜂ != null)
					{
						num = 7;
						continue;
					}
					goto IL_82;
				case 7:
					goto IL_146;
				case 8:
					return;
				case 9:
					goto IL_BE;
				case 10:
					this.ᜄ.Close();
					this.ᜄ = null;
					num = 8;
					continue;
				case 11:
					if (true)
					{
					}
					goto IL_126;
				}
				break;
				IL_82:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_146:
					this.ᜂ.Close();
					this.ᜂ = null;
					num = 5;
					continue;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_BE:
				num = 6;
				continue;
				IL_126:
				num = 2;
			}
		}
	}

	// Token: 0x0400252E RID: 9518
	private new ParagraphFormat ᜀ;

	// Token: 0x0400252F RID: 9519
	private new ListFormat ᜁ;

	// Token: 0x04002530 RID: 9520
	private new spr\u2021 ᜂ;

	// Token: 0x04002531 RID: 9521
	private spr\u20C7 ᜃ;

	// Token: 0x04002532 RID: 9522
	private sprᦣ ᜄ;

	// Token: 0x04002533 RID: 9523
	private Dictionary<ConditionalFormattingCode, sprῊ> ᜅ = new Dictionary<ConditionalFormattingCode, sprῊ>();
}
