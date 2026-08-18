using System;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x0200028D RID: 653
internal class sprῊ : Style
{
	// Token: 0x0600229C RID: 8860 RVA: 0x00238A70 File Offset: 0x00237A70
	internal ParagraphFormat ᜀ()
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

	// Token: 0x0600229D RID: 8861 RVA: 0x00238AB4 File Offset: 0x00237AB4
	internal spr\u2021 ᜈ()
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

	// Token: 0x0600229E RID: 8862 RVA: 0x00238AF8 File Offset: 0x00237AF8
	internal spr\u20C7 ᜅ()
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

	// Token: 0x0600229F RID: 8863 RVA: 0x00238B3C File Offset: 0x00237B3C
	internal new sprᦣ ᜂ()
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

	// Token: 0x060022A0 RID: 8864 RVA: 0x00238B80 File Offset: 0x00237B80
	internal ConditionalFormattingCode ᜇ()
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

	// Token: 0x060022A1 RID: 8865 RVA: 0x00238BC4 File Offset: 0x00237BC4
	public virtual StyleType ᜃ()
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

	// Token: 0x060022A2 RID: 8866 RVA: 0x00238C00 File Offset: 0x00237C00
	internal sprῊ(ConditionalFormattingCode A_0, IDocument A_1) : base((Document)A_1)
	{
		this.ᜄ = A_0;
		this.ᜀ = new ParagraphFormat(base.Document);
		this.ᜀ.ᜀ(this);
		this.ᜁ = new spr\u2021(base.Document);
		this.ᜁ.ᜀ(this);
		this.ᜂ = new spr\u20C7(base.Document);
		this.ᜂ.ᜀ(this);
		this.ᜃ = new sprᦣ(base.Document);
		this.ᜃ.ᜀ(this);
	}

	// Token: 0x060022A3 RID: 8867 RVA: 0x00238C94 File Offset: 0x00237C94
	public virtual IStyle ᜄ()
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
		return (sprῊ)this.CloneImpl();
	}

	// Token: 0x060022A4 RID: 8868 RVA: 0x00238CDC File Offset: 0x00237CDC
	protected virtual object ᜉ()
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
		sprῊ sprῊ = (sprῊ)base.CloneImpl();
		sprῊ.ᜀ = new ParagraphFormat(base.Document);
		sprῊ.ᜀ.ImportContainer(this.ᜀ());
		sprῊ.ᜀ.ᜀ(sprῊ);
		sprῊ.ᜁ = new spr\u2021(base.Document);
		sprῊ.ᜁ.ImportContainer(this.ᜈ());
		sprῊ.ᜁ.ᜀ(this);
		sprῊ.ᜂ = new spr\u20C7(base.Document);
		sprῊ.ᜂ.ImportContainer(this.ᜅ());
		sprῊ.ᜂ.ᜀ(this);
		sprῊ.ᜃ = new sprᦣ(base.Document);
		sprῊ.ᜃ.ImportContainer(this.ᜂ());
		sprῊ.ᜃ.ᜀ(this);
		return sprῊ;
	}

	// Token: 0x060022A5 RID: 8869 RVA: 0x00238DDC File Offset: 0x00237DDC
	internal virtual void ᜁ()
	{
		for (;;)
		{
			for (;;)
			{
				base.Close();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ != null)
						{
							num = 5;
							continue;
						}
						goto IL_A5;
					case 1:
						if (this.ᜃ != null)
						{
							num = 6;
							continue;
						}
						return;
					case 2:
						goto IL_12C;
					case 3:
						goto IL_82;
					case 4:
						if (this.ᜂ != null)
						{
							num = 8;
							continue;
						}
						goto IL_12C;
					case 5:
						this.ᜀ.Close();
						this.ᜀ = null;
						num = 10;
						continue;
					case 6:
						this.ᜃ.Close();
						this.ᜃ = null;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 7:
						this.ᜁ.Close();
						this.ᜁ = null;
						num = 3;
						continue;
					case 8:
						this.ᜂ.Close();
						this.ᜂ = null;
						if (true)
						{
						}
						num = 2;
						continue;
					case 9:
						if (this.ᜁ != null)
						{
							num = 7;
							continue;
						}
						goto IL_82;
					case 10:
						goto IL_A5;
					case 11:
						return;
					}
					break;
					IL_82:
					num = 4;
					continue;
					IL_A5:
					num = 9;
					continue;
					IL_12C:
					num = 1;
				}
			}
		}
	}

	// Token: 0x04002117 RID: 8471
	private new ParagraphFormat ᜀ;

	// Token: 0x04002118 RID: 8472
	private new spr\u2021 ᜁ;

	// Token: 0x04002119 RID: 8473
	private new spr\u20C7 ᜂ;

	// Token: 0x0400211A RID: 8474
	private sprᦣ ᜃ;

	// Token: 0x0400211B RID: 8475
	private ConditionalFormattingCode ᜄ;
}
