using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000F8 RID: 248
	public class Break : ParagraphBase, spr\u2297
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000410C4 File Offset: 0x000400C4
		internal spr᪒ HtmlToDocLayoutInfo
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
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00041108 File Offset: 0x00040108
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.Break;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00041148 File Offset: 0x00040148
		public BreakType BreakType
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
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0004118C File Offset: 0x0004018C
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x000411D0 File Offset: 0x000401D0
		internal TextRange TextRange
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00041214 File Offset: 0x00040214
		internal override int EndPos
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
				return base.EndPos + this.ᜁ.Text.Length;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00041268 File Offset: 0x00040268
		public Break(IDocument doc) : this(doc, BreakType.LineBreak)
		{
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00041280 File Offset: 0x00040280
		public Break(IDocument doc, BreakType breakType) : base((Document)doc)
		{
			this.ᜀ = breakType;
			this.ᜁ = new TextRange(doc);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000412B8 File Offset: 0x000402B8
		internal override void Attach(Paragraph paragraph, int itemPos)
		{
			for (;;)
			{
				IL_1C:
				base.Attach(paragraph, itemPos);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (base.OwnerParagraph != null)
						{
							num = 4;
							continue;
						}
						goto IL_90;
					case 1:
						goto IL_70;
					case 2:
						if (this.ᜀ == BreakType.LineBreak)
						{
							num = 3;
							continue;
						}
						goto IL_90;
					case 3:
						base.OwnerParagraph.ᜀ(this, 0, this.ᜁ.Text);
						num = 1;
						continue;
					case 4:
						num = 2;
						continue;
					}
					goto IL_1C;
				}
				IL_90:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_A6;
				}
				IL_70:
				goto IL_90;
			}
			IL_A6:
			if (false)
			{
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00041374 File Offset: 0x00040374
		internal override void Detach()
		{
			for (;;)
			{
				IL_1C:
				base.Detach();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ == BreakType.LineBreak)
						{
							num = 4;
							continue;
						}
						goto IL_97;
					case 1:
						if (base.OwnerParagraph != null)
						{
							num = 3;
							continue;
						}
						goto IL_97;
					case 2:
						goto IL_6F;
					case 3:
						if (true)
						{
						}
						num = 0;
						continue;
					case 4:
						base.OwnerParagraph.ᜀ(this, this.ᜁ.Text.Length, string.Empty);
						num = 2;
						continue;
					}
					goto IL_1C;
				}
				IL_97:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_AD;
				}
				IL_6F:
				goto IL_97;
			}
			IL_AD:
			if (false)
			{
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00041434 File Offset: 0x00040434
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("౷͹౻᭽", a_), ParagraphItemType.Break);
			writer.WriteValue(ClipboardData.b("㩷ࡹ᥻ώ횁ﶃ", a_), this.BreakType);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000414BC File Offset: 0x000404BC
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.ReadXmlAttributes(reader);
			this.ᜀ = (BreakType)reader.ReadEnum(ClipboardData.b("ㅲݴቶᡸၺ⥼پ", a_), typeof(BreakType));
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00041530 File Offset: 0x00040530
		protected override void InitXDLSHolder()
		{
			int a_ = 7;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("ᥬ੮॰ݲ塴նᡸᕺ᩼᩾", a_), this.ᜁ);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00041594 File Offset: 0x00040594
		protected override void CreateLayoutInfo()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			switch (this.ᜀ)
			{
			case BreakType.PageBreak:
				this.ᜀ = new sprℐ(ChildrenLayoutDirection.Vertical, false);
				this.ᜀ.ᜅ(true);
				return;
			case BreakType.ColumnBreak:
				this.ᜀ = new sprℐ(ChildrenLayoutDirection.Vertical, false);
				this.ᜀ.ᜅ(true);
				return;
			case BreakType.LineBreak:
				this.ᜀ = new sprℐ(ChildrenLayoutDirection.Vertical, false);
				this.ᜀ.ᜃ(true);
				return;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00041638 File Offset: 0x00040638
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				SizeF result;
				for (;;)
				{
					result = default(SizeF);
					Paragraph paragraph = base.OwnerParagraph;
					int num = 22;
					for (;;)
					{
						ParagraphItemCollection paragraphItemCollection;
						int num2;
						Break @break;
						int num3;
						TextRange textRange;
						switch (num)
						{
						case 0:
							paragraphItemCollection = paragraph.\u1712();
							num = 43;
							continue;
						case 1:
							num = 39;
							continue;
						case 2:
							num = 40;
							continue;
						case 3:
							paragraph = (base.Owner.Owner.Owner as Paragraph);
							num = 4;
							continue;
						case 4:
							goto IL_541;
						case 5:
							goto IL_25F;
						case 6:
							if (paragraphItemCollection[num2] is TextRange)
							{
								num = 10;
								continue;
							}
							num2--;
							num = 27;
							continue;
						case 7:
							num = 23;
							continue;
						case 8:
							if (@break != null)
							{
								num = 1;
								continue;
							}
							goto IL_3EC;
						case 9:
							if (!(paragraphItemCollection[num3 - 1] is Break))
							{
								num = 45;
								continue;
							}
							goto IL_2DC;
						case 10:
							IL_368:
							textRange = (paragraph.Items[num2] as TextRange);
							num = 19;
							continue;
						case 11:
							result = dc.ᜀ(textRange, ClipboardData.b("䥦", a_));
							num = 46;
							continue;
						case 12:
							goto IL_594;
						case 13:
							return result;
						case 14:
							goto IL_2B9;
						case 15:
							goto IL_284;
						case 16:
							num = 9;
							continue;
						case 17:
							if (this.BreakType == BreakType.LineBreak)
							{
								num = 2;
								continue;
							}
							goto IL_2B9;
						case 18:
							if (paragraph.ᜋ)
							{
								num = 0;
								continue;
							}
							goto IL_1D2;
						case 19:
							goto IL_152;
						case 20:
							if (textRange != null)
							{
								num = 11;
								continue;
							}
							result.Height = dc.ᜁ(ClipboardData.b("䝦", a_), this.TextRange.CharacterFormat.Font, null).Height;
							num = 12;
							continue;
						case 21:
							goto IL_152;
						case 22:
							if (base.Owner is spr\u1AD2)
							{
								num = 3;
								continue;
							}
							goto IL_541;
						case 23:
							if (paragraphItemCollection.Count == 1)
							{
								num = 44;
								continue;
							}
							return result;
						case 24:
							if (num3 != 0)
							{
								num = 16;
								continue;
							}
							goto IL_2DC;
						case 25:
							if (paragraphItemCollection.Count == num3 + 1)
							{
								num = 15;
								continue;
							}
							goto IL_594;
						case 26:
							num = 24;
							continue;
						case 27:
							goto IL_25F;
						case 28:
							goto IL_503;
						case 29:
							if (!(paragraphItemCollection[num3 + 1] is spr\u248F))
							{
								num = 14;
								continue;
							}
							goto IL_2DC;
						case 30:
							if (num2 < 0)
							{
								num = 21;
								continue;
							}
							num = 6;
							continue;
						case 31:
							num = 25;
							continue;
						case 32:
							if (this != null)
							{
								num = 42;
								continue;
							}
							return result;
						case 33:
							num = 35;
							continue;
						case 34:
							num = 17;
							continue;
						case 35:
							if (!(paragraphItemCollection[num3 - 1] is Break))
							{
								num = 31;
								continue;
							}
							goto IL_284;
						case 36:
							if (this != null)
							{
								num = 34;
								continue;
							}
							goto IL_2B9;
						case 37:
							if (num3 > 0)
							{
								num = 33;
								continue;
							}
							goto IL_594;
						case 38:
							if (this.BreakType == BreakType.LineBreak)
							{
								num = 7;
								continue;
							}
							return result;
						case 39:
							if (@break.BreakType != BreakType.LineBreak)
							{
								num = 47;
								continue;
							}
							goto IL_503;
						case 40:
							if (num3 < paragraphItemCollection.Count - 1)
							{
								if (true)
								{
								}
								num = 26;
								continue;
							}
							goto IL_2B9;
						case 41:
							if (paragraphItemCollection.Count == num3 + 1)
							{
								num = 28;
								continue;
							}
							goto IL_594;
						case 42:
							num = 38;
							continue;
						case 43:
							goto IL_1D2;
						case 44:
							goto IL_2DC;
						case 45:
							num = 29;
							continue;
						case 46:
							goto IL_594;
						case 47:
							goto IL_3EC;
						}
						break;
						IL_152:
						num = 20;
						continue;
						IL_1D2:
						textRange = null;
						num3 = paragraphItemCollection.IndexOf(this);
						num = 37;
						continue;
						IL_25F:
						num = 30;
						continue;
						IL_284:
						@break = (paragraphItemCollection[num3 - 1] as Break);
						num = 8;
						continue;
						IL_2B9:
						num = 32;
						continue;
						IL_2DC:
						result.Height = dc.ᜁ(ClipboardData.b("䝦", a_), this.TextRange.CharacterFormat.Font, null).Height;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_368;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						IL_3EC:
						num = 41;
						continue;
						IL_503:
						num2 = num3;
						num = 5;
						continue;
						IL_541:
						paragraphItemCollection = paragraph.Items;
						num = 18;
						continue;
						IL_594:
						num = 36;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x04000DA0 RID: 3488
		private string \u25D8\u009E\u00A0\u0094;

		// Token: 0x04000DA1 RID: 3489
		private new BreakType ᜀ;

		// Token: 0x04000DA2 RID: 3490
		private string[] \u25D8\u0098\u0080\u0097;

		// Token: 0x04000DA3 RID: 3491
		private new TextRange ᜁ;

		// Token: 0x04000DA4 RID: 3492
		internal spr᪒ ᜂ = new spr᪒();
	}
}
