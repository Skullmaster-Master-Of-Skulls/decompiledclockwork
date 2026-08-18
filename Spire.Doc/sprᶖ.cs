using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

// Token: 0x02000439 RID: 1081
internal class sprᶖ : Field
{
	// Token: 0x06003BCE RID: 15310 RVA: 0x00374290 File Offset: 0x00373290
	public virtual DocumentObjectType ᜁ()
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
		return DocumentObjectType.EmbededField;
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x003742D0 File Offset: 0x003732D0
	internal new int ᜀ()
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

	// Token: 0x06003BD0 RID: 15312 RVA: 0x00374314 File Offset: 0x00373314
	internal new void ᜀ(int A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x00374358 File Offset: 0x00373358
	internal new bool ᜄ()
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

	// Token: 0x06003BD2 RID: 15314 RVA: 0x0037439C File Offset: 0x0037339C
	internal new void ᜀ(bool A_0)
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

	// Token: 0x06003BD3 RID: 15315 RVA: 0x003743E0 File Offset: 0x003733E0
	internal sprᶖ(IDocument A_0) : base(A_0)
	{
		this.m_paraItemType = ParagraphItemType.EmbedField;
	}

	// Token: 0x06003BD4 RID: 15316 RVA: 0x003743FC File Offset: 0x003733FC
	protected virtual void ᜂ()
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
		base.InitXDLSHolder();
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x00374440 File Offset: 0x00373440
	protected virtual void ᜀ(IXDLSAttributeReader A_0)
	{
		int a_ = 19;
		for (;;)
		{
			base.ReadXmlAttributes(A_0);
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.HasAttribute(ClipboardData.b("⩸ེቼൾ힆소ﺖ", a_)))
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					this.ᜀ = A_0.ReadInt(ClipboardData.b("⩸ེቼൾ힆소ﺖ", a_));
					this.ᜁ = A_0.ReadBoolean(ClipboardData.b("㙸᝺᡼䵾캀ﾊ", a_));
					goto IL_92;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						goto IL_B3;
					}
					break;
				}
				break;
				IL_92:
				num = 2;
			}
		}
		IL_B3:
		if (false)
		{
		}
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x00374508 File Offset: 0x00373508
	protected virtual void ᜀ(IXDLSAttributeWriter A_0)
	{
		int a_ = 11;
		for (;;)
		{
			base.WriteXmlAttributes(A_0);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.WriteValue(ClipboardData.b("≰ݲᩴնᡸᱺ᡼⽾즄歷ﺐﶒ", a_), this.ᜀ);
					A_0.WriteValue(ClipboardData.b("㹰ὲၴ䕶㙸᥺᝼᩾", a_), this.ᜁ);
					goto IL_7D;
				case 1:
					if (this.ᜀ > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D;
					default:
						goto IL_A6;
					}
					break;
				}
				break;
				IL_7D:
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_A6:
		if (false)
		{
		}
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x003745C4 File Offset: 0x003735C4
	protected virtual object ᜃ()
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
		return (sprᶖ)base.CloneImpl();
	}

	// Token: 0x04002BBD RID: 11197
	protected internal new int ᜀ;

	// Token: 0x04002BBE RID: 11198
	protected internal new bool ᜁ;
}
