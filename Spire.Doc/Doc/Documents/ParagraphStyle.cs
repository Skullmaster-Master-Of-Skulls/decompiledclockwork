using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x0200049D RID: 1181
	public class ParagraphStyle : Style, IParagraphStyle
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x003D4158 File Offset: 0x003D3158
		public ParagraphFormat ParagraphFormat
		{
			get
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
				return this.m_prFormat;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06004083 RID: 16515 RVA: 0x003D419C File Offset: 0x003D319C
		public new ParagraphStyle BaseStyle
		{
			get
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
				return base.BaseStyle as ParagraphStyle;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x003D41E4 File Offset: 0x003D31E4
		public override StyleType StyleType
		{
			get
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
				return StyleType.ParagraphStyle;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x003D4220 File Offset: 0x003D3220
		public ListFormat ListFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_76;
					case 2:
						this.m_listFormat = new ListFormat(base.Document, this);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (this.m_listFormat != null)
					{
						break;
					}
					num = 2;
				}
				IL_76:
				return this.m_listFormat;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x003D42AC File Offset: 0x003D32AC
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x003D42F0 File Offset: 0x003D32F0
		internal int ListIndex
		{
			get
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
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x003D4334 File Offset: 0x003D3334
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x003D4378 File Offset: 0x003D3378
		internal int ListLevel
		{
			get
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
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x003D43BC File Offset: 0x003D33BC
		public ParagraphStyle(IDocument doc) : base((Document)doc)
		{
			this.m_prFormat = new ParagraphFormat(base.Document);
			this.m_prFormat.ᜀ(this);
			if ((doc as Document).ᜃ)
			{
				(doc as Document).ᜃ = false;
				base.ApplyBaseStyle(BuiltinStyle.Normal);
				(doc as Document).ᜃ = true;
			}
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x003D4430 File Offset: 0x003D3430
		public override void ApplyBaseStyle(string styleName)
		{
			for (;;)
			{
				IL_14:
				if (true)
				{
				}
				base.ApplyBaseStyle(styleName);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4F:
					num = 2;
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
						goto IL_47;
					case 1:
						return;
					case 2:
						this.m_prFormat.ApplyBase(this.BaseStyle.ParagraphFormat);
						num = 1;
						continue;
					}
					goto IL_14;
				}
				IL_47:
				if (this.BaseStyle != null)
				{
					goto IL_4F;
				}
				break;
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x003D44C0 File Offset: 0x003D34C0
		public override IStyle Clone()
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
			return (ParagraphStyle)this.CloneImpl();
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x003D4508 File Offset: 0x003D3508
		protected override object CloneImpl()
		{
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				IL_1C:
				paragraphStyle = (ParagraphStyle)base.CloneImpl();
				paragraphStyle.m_prFormat = new ParagraphFormat(base.Document);
				paragraphStyle.m_prFormat.ImportContainer(this.ParagraphFormat);
				paragraphStyle.m_prFormat.ᜀ(paragraphStyle);
				paragraphStyle.m_listFormat = new ListFormat(base.Document, this);
				paragraphStyle.m_listFormat.ImportContainer(this.ListFormat);
				paragraphStyle.m_listFormat.ᜀ(paragraphStyle);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B4:
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_AC;
					case 1:
						paragraphStyle.ApplyBaseStyle(this.BaseStyle.Name);
						num = 2;
						continue;
					case 2:
						return paragraphStyle;
					}
					goto IL_1C;
				}
				IL_AC:
				if (this.BaseStyle != null)
				{
					goto IL_B4;
				}
				break;
			}
			return paragraphStyle;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x003D4600 File Offset: 0x003D3600
		internal void ᜀ(Document A_0)
		{
			int a_ = 11;
			int num = 16;
			for (;;)
			{
				spr\u177D spr_u177D;
				switch (num)
				{
				case 0:
					goto IL_B2;
				case 1:
				{
					ListStyle currentListStyle = this.ListFormat.CurrentListStyle;
					num = 5;
					continue;
				}
				case 2:
					if (A_0.ListOverrides.ᜀ(this.ListFormat.LFOStyleName) == null)
					{
						num = 7;
						continue;
					}
					return;
				case 3:
					this.ListFormat.CurrentListLevel.ParaStyleName = base.Name.Replace(ClipboardData.b("兰", a_), string.Empty);
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					num = 15;
					continue;
				case 5:
				{
					ListStyle currentListStyle;
					if (currentListStyle != null)
					{
						num = 4;
						continue;
					}
					goto IL_19F;
				}
				case 6:
					return;
				case 7:
					goto IL_B0;
				case 8:
					if (this.ListFormat.LFOStyleName == null)
					{
						num = 6;
						continue;
					}
					num = 2;
					continue;
				case 9:
					return;
				case 10:
					if (spr_u177D != null)
					{
						num = 13;
						continue;
					}
					return;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
					{
						if (false)
						{
						}
						ListStyle currentListStyle;
						A_0.ListStyles.Add((ListStyle)currentListStyle.Clone());
						num = 12;
						continue;
					}
					}
					break;
				case 12:
					goto IL_19F;
				case 13:
					A_0.ListOverrides.ᜀ((spr\u177D)spr_u177D.Clone());
					num = 9;
					continue;
				case 14:
				{
					ListStyle currentListStyle;
					if (currentListStyle != null)
					{
						num = 3;
						continue;
					}
					goto IL_B2;
				}
				case 15:
				{
					ListStyle currentListStyle;
					if (A_0.ListStyles.FindByName(currentListStyle.Name) == null)
					{
						num = 11;
						continue;
					}
					goto IL_19F;
				}
				}
				if (this.ListFormat.ListType != ListType.NoList)
				{
					num = 1;
					continue;
				}
				goto IL_B2;
				IL_B0:
				spr_u177D = base.Document.ListOverrides.ᜀ(this.ListFormat.LFOStyleName);
				num = 10;
				continue;
				IL_B2:
				num = 8;
				continue;
				IL_19F:
				num = 14;
			}
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x003D4848 File Offset: 0x003D3848
		internal override void Close()
		{
			for (;;)
			{
				IL_14:
				base.Close();
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4E:
					num = 2;
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
						goto IL_46;
					case 1:
						return;
					case 2:
						this.m_prFormat.Close();
						this.m_prFormat = null;
						num = 1;
						continue;
					}
					goto IL_14;
				}
				IL_46:
				if (this.m_prFormat != null)
				{
					goto IL_4E;
				}
				break;
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x003D48D4 File Offset: 0x003D38D4
		protected override void InitXDLSHolder()
		{
			int a_ = 8;
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
			base.XDLSHolder.AddElement(ClipboardData.b("ṭᅯqᕳᅵ੷᭹౻ᙽ굿", a_), this.m_prFormat);
		}

		// Token: 0x04002FFA RID: 12282
		protected ParagraphFormat m_prFormat;

		// Token: 0x04002FFB RID: 12283
		private long[] \u2460\u0088\u0089\u00A4;

		// Token: 0x04002FFC RID: 12284
		private int[] \u2460\u008E\u00AC\u00B0;

		// Token: 0x04002FFD RID: 12285
		protected ListFormat m_listFormat;

		// Token: 0x04002FFE RID: 12286
		private new int ᜀ = -1;

		// Token: 0x04002FFF RID: 12287
		private bool[] \u2609\u0095\u009E\u00A6;

		// Token: 0x04003000 RID: 12288
		private long \u2593\u008B\u008D\u00A1;

		// Token: 0x04003001 RID: 12289
		private new int ᜁ = -1;
	}
}
