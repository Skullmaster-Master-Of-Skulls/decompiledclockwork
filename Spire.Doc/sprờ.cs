using System;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x02000434 RID: 1076
internal class sprờ : ParagraphBase, sprἤ
{
	// Token: 0x06003BB7 RID: 15287 RVA: 0x00373B88 File Offset: 0x00372B88
	public spr\u1AD2 ᜇ()
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

	// Token: 0x06003BB8 RID: 15288 RVA: 0x00373BCC File Offset: 0x00372BCC
	public new void ᜀ(spr\u1AD2 A_0)
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

	// Token: 0x06003BB9 RID: 15289 RVA: 0x00373C10 File Offset: 0x00372C10
	public new spr\u1803 ᜀ()
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

	// Token: 0x06003BBA RID: 15290 RVA: 0x00373C54 File Offset: 0x00372C54
	public new void ᜀ(spr\u1803 A_0)
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

	// Token: 0x06003BBB RID: 15291 RVA: 0x00373C98 File Offset: 0x00372C98
	public CharacterFormat ᜅ()
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
		return base.ParaItemCharFormat;
	}

	// Token: 0x06003BBC RID: 15292 RVA: 0x00373CDC File Offset: 0x00372CDC
	public virtual DocumentObjectType ᜂ()
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
		return DocumentObjectType.StructureDocumentTagInline;
	}

	// Token: 0x06003BBD RID: 15293 RVA: 0x00373D18 File Offset: 0x00372D18
	internal sprờ(Document A_0) : base(A_0)
	{
		this.ᜁ = new spr\u1803();
		this.ᜀ = new spr\u1AD2(A_0, this);
		this.ᜀ.ᜀ(this);
		this.m_charFormat = new CharacterFormat(A_0);
	}

	// Token: 0x06003BBE RID: 15294 RVA: 0x00373D5C File Offset: 0x00372D5C
	protected virtual void ᜄ()
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

	// Token: 0x06003BBF RID: 15295 RVA: 0x00373D98 File Offset: 0x00372D98
	internal sprờ ᜃ()
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
		return (sprờ)this.CloneImpl();
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x00373DE0 File Offset: 0x00372DE0
	protected virtual object ᜁ()
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
		return (sprờ)base.CloneImpl();
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x00373E28 File Offset: 0x00372E28
	internal void ᜆ()
	{
		for (;;)
		{
			IParagraphStyle style = base.OwnerParagraph.GetStyle();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					base.ParaItemCharFormat.ApplyBase(style.CharacterFormat);
					int num2 = 0;
					num = 2;
					continue;
				}
				case 1:
				{
					int num2;
					if (num2 >= this.ᜇ().ᜂ().Count)
					{
						num = 3;
						continue;
					}
					this.ᜇ().ᜂ()[num2].ParaItemCharFormat.ApplyBase(style.CharacterFormat);
					num2++;
					goto IL_91;
				}
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						if (false)
						{
						}
						goto IL_44;
					}
					break;
				case 3:
					return;
				case 4:
					if (style != null)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					goto IL_44;
				}
				break;
				IL_44:
				num = 1;
				continue;
				IL_91:
				num = 5;
			}
		}
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x00373F24 File Offset: 0x00372F24
	internal new void ᜀ(ParagraphItemCollection A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					if (this.ᜇ().ᜂ()[num] is sprờ)
					{
						num2 = 5;
						continue;
					}
					A_0.InnerList.Add(this.ᜇ().ᜂ()[num]);
					num2 = 6;
					continue;
				case 1:
					return;
				case 2:
					if (num >= this.ᜇ().ᜂ().Count)
					{
						num2 = 1;
						continue;
					}
					num2 = 0;
					continue;
				case 3:
					goto IL_3F;
				case 4:
					goto IL_C3;
				case 5:
					(this.ᜇ().ᜂ()[num] as sprờ).ᜀ(A_0);
					num2 = 3;
					continue;
				case 6:
					goto IL_3F;
				case 7:
					goto IL_C3;
				}
				break;
				IL_3F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_C3:
					num2 = 2;
					break;
				default:
					if (false)
					{
					}
					num++;
					num2 = 7;
					break;
				}
			}
		}
	}

	// Token: 0x04002BB7 RID: 11191
	private new spr\u1AD2 ᜀ;

	// Token: 0x04002BB8 RID: 11192
	private new spr\u1803 ᜁ;
}
