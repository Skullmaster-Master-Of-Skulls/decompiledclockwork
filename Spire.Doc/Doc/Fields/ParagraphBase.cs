using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x020000A1 RID: 161
	public abstract class ParagraphBase : DocumentBase, IParagraphBase
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00014E0C File Offset: 0x00013E0C
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00014E50 File Offset: 0x00013E50
		internal bool SkipDocxItem
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
				return this.ᜂ;
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
				this.ᜂ = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00014E94 File Offset: 0x00013E94
		public Paragraph OwnerParagraph
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
				return base.Owner as Paragraph;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00014EDC File Offset: 0x00013EDC
		public bool IsInsertRevision
		{
			get
			{
				while (this.m_charFormat != null)
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
						if (true)
						{
						}
						return this.m_charFormat.IsInsertRevision;
					}
				}
				return false;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00014F30 File Offset: 0x00013F30
		internal void \u1713(bool A_0)
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
			this.ParaItemCharFormat.IsInsertRevision = A_0;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00014F78 File Offset: 0x00013F78
		public bool IsDeleteRevision
		{
			get
			{
				while (this.m_charFormat != null)
				{
					if (true)
					{
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
						return this.m_charFormat.IsDeleteRevision;
					}
				}
				return false;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00014FCC File Offset: 0x00013FCC
		internal void \u1712(bool A_0)
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
			this.ParaItemCharFormat.IsDeleteRevision = A_0;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00015014 File Offset: 0x00014014
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00015068 File Offset: 0x00014068
		internal bool IsChangedCFormat
		{
			get
			{
				while (this.m_charFormat != null)
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
						return this.m_charFormat.IsChangedFormat;
					}
				}
				return false;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_48:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (true)
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
						return;
					case 2:
						this.m_charFormat.IsChangedFormat = value;
						num = 0;
						continue;
					}
					break;
				}
				if (this.m_charFormat != null)
				{
					goto IL_48;
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000150E8 File Offset: 0x000140E8
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0001512C File Offset: 0x0001412C
		internal int StartPos
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00015170 File Offset: 0x00014170
		internal virtual int EndPos
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
				return this.StartPos;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000151B4 File Offset: 0x000141B4
		internal bool ItemDetached
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
				return base.OwnerBase == null;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000151F8 File Offset: 0x000141F8
		internal CharacterFormat ParaItemCharFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						num = 4;
						continue;
					case 2:
						this.m_charFormat = (base.Owner.Owner.Owner as Paragraph).BreakCharacterFormat;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_56;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_B1;
					case 4:
						if (base.Owner is spr\u1AD2)
						{
							num = 2;
							continue;
						}
						this.m_charFormat = this.OwnerParagraph.BreakCharacterFormat;
						goto IL_56;
					case 5:
						goto IL_5E;
					}
					if (this.m_charFormat == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_56:
					num = 5;
				}
				IL_5E:
				IL_B1:
				return this.m_charFormat;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000152E8 File Offset: 0x000142E8
		protected ParagraphBase(Document doc) : base(doc, null)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00015300 File Offset: 0x00014300
		internal virtual void Attach(Paragraph owner, int itemPos)
		{
			int a_ = 11;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ItemDetached)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					num = 8;
					continue;
				case 1:
					goto IL_B0;
				case 2:
					goto IL_D8;
				case 4:
					this.ParaItemCharFormat.ApplyBase(this.OwnerParagraph.BreakCharacterFormat.BaseFormat);
					num = 2;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_103;
					default:
						goto IL_63;
					}
					break;
				case 6:
					if (owner != this.OwnerParagraph)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
				case 7:
					goto IL_48;
				case 8:
					goto IL_103;
				}
				if (owner == null)
				{
					num = 7;
					continue;
				}
				num = 6;
				continue;
				IL_103:
				if (this.OwnerParagraph.BreakCharacterFormat.BaseFormat == null)
				{
					goto IL_128;
				}
				num = 4;
			}
			IL_48:
			throw new ArgumentNullException(ClipboardData.b("ṰѲ᭴ቶ୸", a_));
			IL_63:
			if (false)
			{
			}
			throw new InvalidOperationException();
			IL_B0:
			throw new InvalidOperationException();
			IL_D8:
			IL_128:
			this.StartPos = itemPos;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001543C File Offset: 0x0001443C
		internal virtual void Detach()
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (!base.Document.IsClosing)
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 2:
					goto IL_83;
				case 3:
					num = 0;
					continue;
				}
				IL_28:
				if (this.ItemDetached)
				{
					num = 3;
					continue;
				}
				return;
				goto IL_28;
			}
			IL_83:
			throw new InvalidOperationException();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000154D0 File Offset: 0x000144D0
		internal void ឨ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_40:
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
				case 1:
					this.m_charFormat.AcceptChanges();
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				break;
			}
			if (this.m_charFormat != null)
			{
				goto IL_40;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00015550 File Offset: 0x00014550
		internal void ឪ()
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
					this.m_charFormat.RemoveChanges();
					num = 2;
					continue;
				case 2:
					return;
				}
				goto IL_40;
			}
			IL_24:
			if (true)
			{
			}
			IL_40:
			if (this.m_charFormat != null)
			{
				goto IL_48;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000155D0 File Offset: 0x000145D0
		internal bool ឭ()
		{
			for (;;)
			{
				IL_00:
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_7A;
					case 2:
						num = 5;
						continue;
					case 3:
						if (!this.IsChangedCFormat)
						{
							goto IL_99;
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
							num = 1;
							continue;
						}
						break;
					case 5:
						if (!this.IsDeleteRevision)
						{
							num = 0;
							continue;
						}
						return true;
					}
					if (this.IsInsertRevision)
					{
						return true;
					}
					num = 2;
				}
			}
			return true;
			IL_7A:
			return true;
			IL_99:
			if (true)
			{
			}
			return false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00015680 File Offset: 0x00014680
		internal CharacterFormat ឬ()
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
			return this.m_charFormat;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000156C4 File Offset: 0x000146C4
		internal virtual void Close()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_40:
				if (true)
				{
				}
				num = 2;
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
					return;
				case 2:
					this.m_charFormat.Close();
					this.m_charFormat = null;
					num = 0;
					continue;
				}
				break;
			}
			if (this.m_charFormat != null)
			{
				goto IL_40;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001574C File Offset: 0x0001474C
		protected override object CloneImpl()
		{
			ParagraphBase paragraphBase;
			for (;;)
			{
				for (;;)
				{
					paragraphBase = (ParagraphBase)base.CloneImpl();
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return paragraphBase;
						case 1:
							if (this.m_charFormat != null)
							{
								num = 2;
								continue;
							}
							return paragraphBase;
						case 2:
							paragraphBase.m_charFormat = new CharacterFormat(base.Document);
							paragraphBase.m_charFormat.ᜃ(this.m_charFormat);
							paragraphBase.m_charFormat.ImportContainer(this.m_charFormat);
							paragraphBase.m_charFormat.ᜀ(paragraphBase);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (true)
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
			return paragraphBase;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00015810 File Offset: 0x00014810
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			sprᯉ sprᯉ;
			for (;;)
			{
				if (true)
				{
				}
				base.CloneRelationsTo(doc, nextOwner);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_16E;
					case 2:
						if (doc.ImportStyles)
						{
							num = 11;
							continue;
						}
						return;
					case 3:
						if (doc.ImportOption != ImportOptions.UseDestinationStyles)
						{
							num = 12;
							continue;
						}
						goto IL_DF;
					case 4:
						goto IL_B4;
					case 5:
					{
						IStyle style = doc.Styles.FindByName(sprᯉ.Name, StyleType.CharacterStyle);
						num = 17;
						continue;
					}
					case 6:
						num = 13;
						continue;
					case 7:
						goto IL_DF;
					case 8:
						num = 2;
						continue;
					case 9:
						if (doc.ImportOption == ImportOptions.UseDestinationStyles)
						{
							num = 8;
							continue;
						}
						return;
					case 10:
						if (sprᯉ != null)
						{
							num = 5;
							continue;
						}
						return;
					case 11:
						sprᯉ = (base.Document.Styles.FindByName(this.m_charFormat.CharStyleName, StyleType.CharacterStyle) as sprᯉ);
						num = 10;
						continue;
					case 12:
						this.ᜀ(doc);
						num = 7;
						continue;
					case 13:
						if (!string.IsNullOrEmpty(this.m_charFormat.CharStyleName))
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
								num = 9;
								continue;
							}
						}
						num = 0;
						continue;
					case 14:
						if (doc.CurClonedSection != null)
						{
							num = 16;
							continue;
						}
						return;
					case 15:
						if (this.m_charFormat != null)
						{
							num = 6;
							continue;
						}
						return;
					case 16:
					{
						IStyle style;
						sprᯉ = (sprᯉ)sprᯉ.ᜀ(doc, style);
						this.m_charFormat.CharStyleName = sprᯉ.Name;
						num = 1;
						continue;
					}
					case 17:
					{
						IStyle style;
						if (style == null)
						{
							num = 4;
							continue;
						}
						num = 14;
						continue;
					}
					}
					break;
					IL_DF:
					num = 15;
				}
			}
			IL_B4:
			sprᯉ.ᜁ(doc);
			return;
			IL_16E:;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00015A3C File Offset: 0x00014A3C
		private new void ᜀ(Document A_0)
		{
			int a_ = 13;
			int num = 1;
			Paragraph paragraph;
			for (;;)
			{
				ParagraphStyle paragraphStyle;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						goto IL_EF;
					}
					break;
				case 2:
					if (A_0.ImportOption == ImportOptions.KeepSourceFormatting)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					goto IL_11B;
				case 4:
					this.m_charFormat.ᜅ(paragraphStyle.CharacterFormat);
					num = 0;
					continue;
				case 5:
					paragraph = new Paragraph(A_0);
					num = 3;
					continue;
				case 6:
					if (paragraph == null)
					{
						num = 5;
						continue;
					}
					goto IL_89;
				case 7:
					goto IL_4A;
				}
				if (A_0.ImportOption == ImportOptions.MergeFormatting)
				{
					num = 7;
					continue;
				}
				paragraphStyle = (A_0.Styles.FindByName(ClipboardData.b("㵲ᩴնᑸ᩺ᅼ", a_), StyleType.ParagraphStyle) as ParagraphStyle);
				num = 2;
				continue;
				IL_4A:
				paragraph = A_0.LastParagraph;
				num = 6;
			}
			IL_89:
			this.m_charFormat.ᜂ(paragraph.BreakCharacterFormat);
			return;
			IL_EF:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
			IL_11B:
			goto IL_89;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00015B6C File Offset: 0x00014B6C
		public DocumentObject GetAncestor(DocumentObjectType objectType)
		{
			DocumentObject owner;
			for (;;)
			{
				owner = base.Owner;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (owner.DocumentObjectType == objectType)
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
								num = 2;
								continue;
							}
						}
						num = 4;
						continue;
					case 1:
						goto IL_5E;
					case 2:
						goto IL_A0;
					case 3:
						goto IL_5C;
					case 4:
						if (owner.Owner == null)
						{
							num = 3;
							continue;
						}
						owner = owner.Owner;
						num = 5;
						continue;
					case 5:
						goto IL_5E;
					}
					break;
					IL_5E:
					num = 0;
				}
			}
			IL_5C:
			return null;
			IL_A0:
			if (true)
			{
			}
			return owner;
		}

		// Token: 0x040009A0 RID: 2464
		private new int ᜀ;

		// Token: 0x040009A1 RID: 2465
		internal bool ᜁ;

		// Token: 0x040009A2 RID: 2466
		private float \u2460\u0082\u008D\u00A2;

		// Token: 0x040009A3 RID: 2467
		private bool[] \u2593\u00AF\u009F\u0093;

		// Token: 0x040009A4 RID: 2468
		protected CharacterFormat m_charFormat;

		// Token: 0x040009A5 RID: 2469
		private string \u2593\u0086\u0088ª;

		// Token: 0x040009A6 RID: 2470
		private bool ᜂ;
	}
}
