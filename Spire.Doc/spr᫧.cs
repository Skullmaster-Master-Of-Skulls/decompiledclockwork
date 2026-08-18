using System;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;

// Token: 0x02000431 RID: 1073
internal class spr\u1AE7 : BodyRegion, spr\u2215
{
	// Token: 0x06003B84 RID: 15236 RVA: 0x00372BDC File Offset: 0x00371BDC
	public spr៧ ᜆ()
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

	// Token: 0x06003B85 RID: 15237 RVA: 0x00372C20 File Offset: 0x00371C20
	public spr\u1803 ᜈ()
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

	// Token: 0x06003B86 RID: 15238 RVA: 0x00372C64 File Offset: 0x00371C64
	public CharacterFormat ᜏ()
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

	// Token: 0x06003B87 RID: 15239 RVA: 0x00372CA8 File Offset: 0x00371CA8
	internal spr\u1AE7(Document A_0) : base(A_0)
	{
		this.ᜁ = new spr\u1803();
		this.ᜀ = new spr៧(A_0, this);
		this.ᜂ = new CharacterFormat(A_0);
		this.ᜃ = this.ᜀ.ᜇ();
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x00372CF4 File Offset: 0x00371CF4
	internal spr\u1AE7 ᜄ()
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
		return (spr\u1AE7)this.CloneImpl();
	}

	// Token: 0x06003B89 RID: 15241 RVA: 0x00372D3C File Offset: 0x00371D3C
	protected virtual object ᜋ()
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
		return (spr\u1AE7)base.CloneImpl();
	}

	// Token: 0x06003B8A RID: 15242 RVA: 0x00372D84 File Offset: 0x00371D84
	internal virtual BodyRegion ᜃ()
	{
		for (;;)
		{
			IL_00:
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.OwnerTextBody.Owner is TextBox)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					num = 1;
					continue;
				case 1:
					if (base.OwnerTextBody.Owner is Section)
					{
						num = 3;
						continue;
					}
					goto IL_166;
				case 2:
					goto IL_52;
				case 3:
					goto IL_10F;
				case 4:
					num = 0;
					continue;
				case 5:
					if (base.Owner is TableCell)
					{
						num = 9;
						continue;
					}
					num = 7;
					continue;
				case 6:
					goto IL_9A;
				case 7:
					if (!(base.Owner is Body))
					{
						goto IL_166;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 9:
					goto IL_BF;
				}
				if (base.NextSibling != null)
				{
					num = 2;
				}
				else
				{
					num = 5;
				}
			}
		}
		IL_52:
		return base.NextSibling as BodyRegion;
		IL_9A:
		return (base.OwnerTextBody.Owner as TextBox).ᜄ();
		IL_BF:
		return (base.Owner as TableCell).ᜋ();
		IL_10F:
		return base.GetNextInSection(base.OwnerTextBody.Owner as Section);
		IL_166:
		return null;
	}

	// Token: 0x06003B8B RID: 15243 RVA: 0x00372EF8 File Offset: 0x00371EF8
	internal virtual bool ᜌ()
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (this.ᜂ == null)
			{
				return false;
			}
			break;
		}
		return this.ᜂ.IsDeleteRevision;
	}

	// Token: 0x06003B8C RID: 15244 RVA: 0x00372F4C File Offset: 0x00371F4C
	internal virtual void ᜂ(bool A_0)
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

	// Token: 0x06003B8D RID: 15245 RVA: 0x00372F88 File Offset: 0x00371F88
	internal virtual void ᜃ(bool A_0)
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

	// Token: 0x06003B8E RID: 15246 RVA: 0x00372FC4 File Offset: 0x00371FC4
	internal virtual void ᜀ(bool A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ.IsDeleteRevision = A_0;
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				return;
			}
			if (this.ᜂ == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06003B8F RID: 15247 RVA: 0x00373044 File Offset: 0x00372044
	internal virtual void ᜄ(bool A_0)
	{
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ.IsInsertRevision = A_0;
				num = 2;
				continue;
			case 2:
				return;
			}
			if (this.ᜂ == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06003B90 RID: 15248 RVA: 0x003730C4 File Offset: 0x003720C4
	internal virtual bool ᜇ()
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
		return false;
	}

	// Token: 0x06003B91 RID: 15249 RVA: 0x00373100 File Offset: 0x00372100
	public virtual int ᜀ(Regex A_0, string A_1)
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
		return 1;
	}

	// Token: 0x06003B92 RID: 15250 RVA: 0x0037313C File Offset: 0x0037213C
	public virtual int ᜀ(string A_0, string A_1, bool A_2, bool A_3)
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
		return 0;
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x00373178 File Offset: 0x00372178
	public virtual int ᜀ(Regex A_0, TextSelection A_1)
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
		return 0;
	}

	// Token: 0x06003B94 RID: 15252 RVA: 0x003731B4 File Offset: 0x003721B4
	public virtual int ᜀ(Regex A_0, TextSelection A_1, bool A_2)
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
		return 0;
	}

	// Token: 0x06003B95 RID: 15253 RVA: 0x003731F0 File Offset: 0x003721F0
	public new int ᜀ(string A_0, TextSelection A_1, bool A_2, bool A_3)
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
		return 0;
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x0037322C File Offset: 0x0037222C
	public new int ᜀ(string A_0, TextSelection A_1, bool A_2, bool A_3, bool A_4)
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
		return 0;
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x00373268 File Offset: 0x00372268
	internal int ᜁ(string A_0, string A_1, bool A_2, bool A_3)
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
		return 0;
	}

	// Token: 0x06003B98 RID: 15256 RVA: 0x003732A4 File Offset: 0x003722A4
	internal int ᜁ(Regex A_0, string A_1)
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
		return 0;
	}

	// Token: 0x06003B99 RID: 15257 RVA: 0x003732E0 File Offset: 0x003722E0
	protected virtual void ᜅ()
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

	// Token: 0x06003B9A RID: 15258 RVA: 0x0037331C File Offset: 0x0037231C
	internal virtual void ᜊ()
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
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x00373358 File Offset: 0x00372358
	internal virtual void ᜉ()
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

	// Token: 0x06003B9C RID: 15260 RVA: 0x00373394 File Offset: 0x00372394
	internal virtual void \u1712()
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
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x003733D0 File Offset: 0x003723D0
	internal virtual void ᜎ()
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
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x0037340C File Offset: 0x0037240C
	internal virtual bool ᜑ()
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

	// Token: 0x06003B9F RID: 15263 RVA: 0x00373448 File Offset: 0x00372448
	internal virtual bool \u170D()
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
		return false;
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x00373484 File Offset: 0x00372484
	public virtual TextSelection ᜀ(Regex A_0)
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
		return null;
	}

	// Token: 0x06003BA1 RID: 15265 RVA: 0x003734C0 File Offset: 0x003724C0
	public new TextSelection ᜀ(string A_0, bool A_1, bool A_2)
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
		return null;
	}

	// Token: 0x06003BA2 RID: 15266 RVA: 0x003734FC File Offset: 0x003724FC
	internal virtual void ᜁ(bool A_0)
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

	// Token: 0x06003BA3 RID: 15267 RVA: 0x00373538 File Offset: 0x00372538
	internal virtual spr\u226E ᜁ(Regex A_0)
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
		return null;
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x00373574 File Offset: 0x00372574
	internal virtual void ᜀ()
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

	// Token: 0x06003BA5 RID: 15269 RVA: 0x003735B0 File Offset: 0x003725B0
	internal virtual bool ᜐ()
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
		return false;
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x003735EC File Offset: 0x003725EC
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
		return DocumentObjectType.StructureDocumentTag;
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x00373628 File Offset: 0x00372628
	public DocumentObjectCollection ᜂ()
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
		return this.ᜃ;
	}

	// Token: 0x06003BA8 RID: 15272 RVA: 0x0037366C File Offset: 0x0037266C
	internal new Table ᜀ(float A_0)
	{
		switch (0)
		{
		default:
		{
			Table table;
			for (;;)
			{
				if (true)
				{
				}
				table = new Table(base.Document);
				table.ResetCells(1, 1);
				TableCell tableCell = table.Rows[0].Cells[0];
				tableCell.Width = A_0;
				table.TableFormat.Paddings.All = 0f;
				table.TableFormat.Borders.BorderType = BorderStyle.None;
				table.Rows[0].RowFormat.Borders.BorderType = BorderStyle.None;
				tableCell.CellFormat.Borders.BorderType = BorderStyle.None;
				table.IsSDTTable = true;
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_CC;
					case 1:
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CC;
						}
						if (false)
						{
						}
						if (num >= this.ᜆ().ᜂ().Items.Count)
						{
							num2 = 2;
							continue;
						}
						BodyRegion bodyRegion = this.ᜆ().ᜂ().Items[num];
						tableCell.Items.Add(bodyRegion.Clone());
						num++;
						num2 = 3;
						continue;
					}
					case 2:
						goto IL_122;
					case 3:
						goto IL_CC;
					}
					break;
					IL_CC:
					num2 = 1;
				}
			}
			IL_122:
			table.ᜀ(base.Owner);
			return table;
		}
		}
	}

	// Token: 0x04002BAD RID: 11181
	private new spr៧ ᜀ;

	// Token: 0x04002BAE RID: 11182
	private spr\u1803 ᜁ;

	// Token: 0x04002BAF RID: 11183
	private CharacterFormat ᜂ;

	// Token: 0x04002BB0 RID: 11184
	private DocumentObjectCollection ᜃ;
}
