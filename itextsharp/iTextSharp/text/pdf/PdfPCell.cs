using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf.events;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C6 RID: 198
	public class PdfPCell : Rectangle
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x00021CE0 File Offset: 0x00020CE0
		public PdfPCell() : base(0f, 0f, 0f, 0f)
		{
			this.borderWidth = 0.5f;
			this.border = 15;
			this.column.SetLeading(0f, 1f);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00021D7C File Offset: 0x00020D7C
		public PdfPCell(Phrase phrase) : base(0f, 0f, 0f, 0f)
		{
			this.borderWidth = 0.5f;
			this.border = 15;
			ColumnText columnText = this.column;
			this.phrase = phrase;
			columnText.AddText(phrase);
			this.column.SetLeading(0f, 1f);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00021E2D File Offset: 0x00020E2D
		public PdfPCell(Image image) : this(image, false)
		{
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00021E38 File Offset: 0x00020E38
		public PdfPCell(Image image, bool fit) : base(0f, 0f, 0f, 0f)
		{
			this.borderWidth = 0.5f;
			this.border = 15;
			if (fit)
			{
				this.image = image;
				this.column.SetLeading(0f, 1f);
				this.Padding = this.borderWidth / 2f;
				return;
			}
			this.column.AddText(this.phrase = new Phrase(new Chunk(image, 0f, 0f)));
			this.column.SetLeading(0f, 1f);
			this.Padding = 0f;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00021F3A File Offset: 0x00020F3A
		public PdfPCell(PdfPTable table) : this(table, null)
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00021F44 File Offset: 0x00020F44
		public PdfPCell(PdfPTable table, PdfPCell style) : base(0f, 0f, 0f, 0f)
		{
			this.borderWidth = 0.5f;
			this.border = 15;
			this.column.SetLeading(0f, 1f);
			this.table = table;
			table.WidthPercentage = 100f;
			table.ExtendLastRow = true;
			this.column.AddElement(table);
			if (style != null)
			{
				this.CloneNonPositionParameters(style);
				this.verticalAlignment = style.verticalAlignment;
				this.paddingLeft = style.paddingLeft;
				this.paddingRight = style.paddingRight;
				this.paddingTop = style.paddingTop;
				this.paddingBottom = style.paddingBottom;
				this.colspan = style.colspan;
				this.rowspan = style.rowspan;
				this.cellEvent = style.cellEvent;
				this.useDescender = style.useDescender;
				this.useBorderPadding = style.useBorderPadding;
				this.rotation = style.rotation;
				return;
			}
			this.Padding = 0f;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000220A4 File Offset: 0x000210A4
		public PdfPCell(PdfPCell cell) : base(cell.llx, cell.lly, cell.urx, cell.ury)
		{
			this.CloneNonPositionParameters(cell);
			this.verticalAlignment = cell.verticalAlignment;
			this.paddingLeft = cell.paddingLeft;
			this.paddingRight = cell.paddingRight;
			this.paddingTop = cell.paddingTop;
			this.paddingBottom = cell.paddingBottom;
			this.phrase = cell.phrase;
			this.fixedHeight = cell.fixedHeight;
			this.minimumHeight = cell.minimumHeight;
			this.noWrap = cell.noWrap;
			this.colspan = cell.colspan;
			this.rowspan = cell.rowspan;
			if (cell.table != null)
			{
				this.table = new PdfPTable(cell.table);
			}
			this.image = Image.GetInstance(cell.image);
			this.cellEvent = cell.cellEvent;
			this.useDescender = cell.useDescender;
			this.column = ColumnText.Duplicate(cell.column);
			this.useBorderPadding = cell.useBorderPadding;
			this.rotation = cell.rotation;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00022212 File Offset: 0x00021212
		public void AddElement(IElement element)
		{
			if (this.table != null)
			{
				this.table = null;
				this.column.SetText(null);
			}
			this.column.AddElement(element);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0002223B File Offset: 0x0002123B
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00022244 File Offset: 0x00021244
		public Phrase Phrase
		{
			get
			{
				return this.phrase;
			}
			set
			{
				this.table = null;
				this.image = null;
				ColumnText columnText = this.column;
				this.phrase = value;
				columnText.SetText(value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00022274 File Offset: 0x00021274
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00022281 File Offset: 0x00021281
		public int HorizontalAlignment
		{
			get
			{
				return this.column.Alignment;
			}
			set
			{
				this.column.Alignment = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0002228F File Offset: 0x0002128F
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00022297 File Offset: 0x00021297
		public int VerticalAlignment
		{
			get
			{
				return this.verticalAlignment;
			}
			set
			{
				this.verticalAlignment = value;
				if (this.table != null)
				{
					this.table.ExtendLastRow = (this.verticalAlignment == 4);
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000222BC File Offset: 0x000212BC
		public float EffectivePaddingLeft
		{
			get
			{
				if (this.UseBorderPadding)
				{
					float num = this.BorderWidthLeft / (this.UseVariableBorders ? 1f : 2f);
					return this.paddingLeft + num;
				}
				return this.paddingLeft;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000222FC File Offset: 0x000212FC
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x00022304 File Offset: 0x00021304
		public float PaddingLeft
		{
			get
			{
				return this.paddingLeft;
			}
			set
			{
				this.paddingLeft = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00022310 File Offset: 0x00021310
		public float EffectivePaddingRight
		{
			get
			{
				if (this.UseBorderPadding)
				{
					float num = this.BorderWidthRight / (this.UseVariableBorders ? 1f : 2f);
					return this.paddingRight + num;
				}
				return this.paddingRight;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00022350 File Offset: 0x00021350
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00022358 File Offset: 0x00021358
		public float PaddingRight
		{
			get
			{
				return this.paddingRight;
			}
			set
			{
				this.paddingRight = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00022364 File Offset: 0x00021364
		public float EffectivePaddingTop
		{
			get
			{
				if (this.UseBorderPadding)
				{
					float num = this.BorderWidthTop / (this.UseVariableBorders ? 1f : 2f);
					return this.paddingTop + num;
				}
				return this.paddingTop;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000223A4 File Offset: 0x000213A4
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000223AC File Offset: 0x000213AC
		public float PaddingTop
		{
			get
			{
				return this.paddingTop;
			}
			set
			{
				this.paddingTop = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x000223B8 File Offset: 0x000213B8
		public float EffectivePaddingBottom
		{
			get
			{
				if (this.UseBorderPadding)
				{
					float num = this.BorderWidthBottom / (this.UseVariableBorders ? 1f : 2f);
					return this.paddingBottom + num;
				}
				return this.paddingBottom;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x000223F8 File Offset: 0x000213F8
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00022400 File Offset: 0x00021400
		public float PaddingBottom
		{
			get
			{
				return this.paddingBottom;
			}
			set
			{
				this.paddingBottom = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00022409 File Offset: 0x00021409
		public float Padding
		{
			set
			{
				this.paddingBottom = value;
				this.paddingTop = value;
				this.paddingLeft = value;
				this.paddingRight = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00022430 File Offset: 0x00021430
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00022427 File Offset: 0x00021427
		public bool UseBorderPadding
		{
			get
			{
				return this.useBorderPadding;
			}
			set
			{
				this.useBorderPadding = value;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00022438 File Offset: 0x00021438
		public void SetLeading(float fixedLeading, float multipliedLeading)
		{
			this.column.SetLeading(fixedLeading, multipliedLeading);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00022447 File Offset: 0x00021447
		public float Leading
		{
			get
			{
				return this.column.Leading;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00022454 File Offset: 0x00021454
		public float MultipliedLeading
		{
			get
			{
				return this.column.MultipliedLeading;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00022461 File Offset: 0x00021461
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0002246E File Offset: 0x0002146E
		public float Indent
		{
			get
			{
				return this.column.Indent;
			}
			set
			{
				this.column.Indent = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0002247C File Offset: 0x0002147C
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00022489 File Offset: 0x00021489
		public float ExtraParagraphSpace
		{
			get
			{
				return this.column.ExtraParagraphSpace;
			}
			set
			{
				this.column.ExtraParagraphSpace = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00022497 File Offset: 0x00021497
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0002249F File Offset: 0x0002149F
		public float FixedHeight
		{
			get
			{
				return this.fixedHeight;
			}
			set
			{
				this.fixedHeight = value;
				this.minimumHeight = 0f;
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000224B3 File Offset: 0x000214B3
		public bool HasFixedHeight()
		{
			return this.FixedHeight > 0f;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000224CB File Offset: 0x000214CB
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x000224C2 File Offset: 0x000214C2
		public bool NoWrap
		{
			get
			{
				return this.noWrap;
			}
			set
			{
				this.noWrap = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000224D3 File Offset: 0x000214D3
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x000224DC File Offset: 0x000214DC
		public PdfPTable Table
		{
			get
			{
				return this.table;
			}
			set
			{
				this.table = value;
				this.column.SetText(null);
				this.image = null;
				if (this.table != null)
				{
					this.table.ExtendLastRow = (this.verticalAlignment == 4);
					this.column.AddElement(this.table);
					this.table.WidthPercentage = 100f;
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00022540 File Offset: 0x00021540
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00022548 File Offset: 0x00021548
		public float MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
			set
			{
				this.minimumHeight = value;
				this.fixedHeight = 0f;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0002255C File Offset: 0x0002155C
		public bool HasMinimumHeight()
		{
			return this.MinimumHeight > 0f;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0002256B File Offset: 0x0002156B
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00022573 File Offset: 0x00021573
		public int Colspan
		{
			get
			{
				return this.colspan;
			}
			set
			{
				this.colspan = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0002257C File Offset: 0x0002157C
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00022584 File Offset: 0x00021584
		public int Rowspan
		{
			get
			{
				return this.rowspan;
			}
			set
			{
				this.rowspan = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0002258D File Offset: 0x0002158D
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0002259A File Offset: 0x0002159A
		public float FollowingIndent
		{
			get
			{
				return this.column.FollowingIndent;
			}
			set
			{
				this.column.FollowingIndent = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x000225A8 File Offset: 0x000215A8
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x000225B5 File Offset: 0x000215B5
		public float RightIndent
		{
			get
			{
				return this.column.RightIndent;
			}
			set
			{
				this.column.RightIndent = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x000225C3 File Offset: 0x000215C3
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x000225D0 File Offset: 0x000215D0
		public float SpaceCharRatio
		{
			get
			{
				return this.column.SpaceCharRatio;
			}
			set
			{
				this.column.SpaceCharRatio = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x000225DE File Offset: 0x000215DE
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x000225EB File Offset: 0x000215EB
		public int RunDirection
		{
			get
			{
				return this.column.RunDirection;
			}
			set
			{
				this.column.RunDirection = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000225F9 File Offset: 0x000215F9
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00022601 File Offset: 0x00021601
		public Image Image
		{
			get
			{
				return this.image;
			}
			set
			{
				this.column.SetText(null);
				this.table = null;
				this.image = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0002261D File Offset: 0x0002161D
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00022628 File Offset: 0x00021628
		public IPdfPCellEvent CellEvent
		{
			get
			{
				return this.cellEvent;
			}
			set
			{
				if (value == null)
				{
					this.cellEvent = null;
					return;
				}
				if (this.cellEvent == null)
				{
					this.cellEvent = value;
					return;
				}
				if (this.cellEvent is PdfPCellEventForwarder)
				{
					((PdfPCellEventForwarder)this.cellEvent).AddCellEvent(value);
					return;
				}
				PdfPCellEventForwarder pdfPCellEventForwarder = new PdfPCellEventForwarder();
				pdfPCellEventForwarder.AddCellEvent(this.cellEvent);
				pdfPCellEventForwarder.AddCellEvent(value);
				this.cellEvent = pdfPCellEventForwarder;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0002268F File Offset: 0x0002168F
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0002269C File Offset: 0x0002169C
		public int ArabicOptions
		{
			get
			{
				return this.column.ArabicOptions;
			}
			set
			{
				this.column.ArabicOptions = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000226AA File Offset: 0x000216AA
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x000226B7 File Offset: 0x000216B7
		public bool UseAscender
		{
			get
			{
				return this.column.UseAscender;
			}
			set
			{
				this.column.UseAscender = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000226C5 File Offset: 0x000216C5
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x000226CD File Offset: 0x000216CD
		public bool UseDescender
		{
			get
			{
				return this.useDescender;
			}
			set
			{
				this.useDescender = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000226D6 File Offset: 0x000216D6
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x000226DE File Offset: 0x000216DE
		public ColumnText Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000226E7 File Offset: 0x000216E7
		public List<IElement> CompositeElements
		{
			get
			{
				return this.column.compositeElements;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00022732 File Offset: 0x00021732
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x000226F4 File Offset: 0x000216F4
		public new int Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				int num = value % 360;
				if (num < 0)
				{
					num += 360;
				}
				if (num % 90 != 0)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("rotation.must.be.a.multiple.of.90"));
				}
				this.rotation = num;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0002273C File Offset: 0x0002173C
		internal void ConsumeHeight(float height)
		{
			float num = this.Right - this.EffectivePaddingRight;
			float num2 = this.Left + this.EffectivePaddingLeft;
			float num3 = height - this.EffectivePaddingTop - this.EffectivePaddingBottom;
			if (this.Rotation != 90 && this.Rotation != 270)
			{
				this.column.SetSimpleColumn(num2, num3 + 0.001f, num, 0f);
			}
			else
			{
				this.column.SetSimpleColumn(0f, num2, num3 + 0.001f, num);
			}
			try
			{
				this.column.Go(true);
			}
			catch (DocumentException)
			{
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000227E4 File Offset: 0x000217E4
		public float GetMaxHeight()
		{
			bool flag = this.Rotation == 90 || this.Rotation == 270;
			Image image = this.Image;
			if (image != null)
			{
				image.ScalePercent(100f);
				float num = flag ? image.ScaledHeight : image.ScaledWidth;
				float num2 = (this.Right - this.EffectivePaddingRight - this.EffectivePaddingLeft - this.Left) / num;
				image.ScalePercent(num2 * 100f);
				float num3 = flag ? image.ScaledWidth : image.ScaledHeight;
				this.Bottom = this.Top - this.EffectivePaddingTop - this.EffectivePaddingBottom - num3;
			}
			else if (flag && this.HasFixedHeight())
			{
				this.Bottom = this.Top - this.FixedHeight;
			}
			else
			{
				ColumnText columnText = ColumnText.Duplicate(this.Column);
				float right;
				float num4;
				float left;
				float bottom;
				if (flag)
				{
					right = 20000f;
					num4 = this.Right - this.EffectivePaddingRight;
					left = 0f;
					bottom = this.Left + this.EffectivePaddingLeft;
				}
				else
				{
					right = (this.NoWrap ? 20000f : (this.Right - this.EffectivePaddingRight));
					num4 = this.Top - this.EffectivePaddingTop;
					left = this.Left + this.EffectivePaddingLeft;
					bottom = (this.HasFixedHeight() ? (num4 + this.EffectivePaddingBottom - this.FixedHeight) : -1.0737418E+09f);
				}
				PdfPRow.SetColumn(columnText, left, bottom, right, num4);
				columnText.Go(true);
				if (flag)
				{
					this.Bottom = this.Top - this.EffectivePaddingTop - this.EffectivePaddingBottom - columnText.FilledWidth;
				}
				else
				{
					float num5 = columnText.YLine;
					if (this.UseDescender)
					{
						num5 += columnText.Descender;
					}
					this.Bottom = num5 - this.EffectivePaddingBottom;
				}
			}
			float height = base.Height;
			if (this.HasFixedHeight())
			{
				height = this.FixedHeight;
			}
			else if (this.HasMinimumHeight() && height < this.MinimumHeight)
			{
				height = this.MinimumHeight;
			}
			return height;
		}

		// Token: 0x04000366 RID: 870
		private ColumnText column = new ColumnText(null);

		// Token: 0x04000367 RID: 871
		private int verticalAlignment = 4;

		// Token: 0x04000368 RID: 872
		private float paddingLeft = 2f;

		// Token: 0x04000369 RID: 873
		private float paddingRight = 2f;

		// Token: 0x0400036A RID: 874
		private float paddingTop = 2f;

		// Token: 0x0400036B RID: 875
		private float paddingBottom = 2f;

		// Token: 0x0400036C RID: 876
		private float fixedHeight;

		// Token: 0x0400036D RID: 877
		private bool noWrap;

		// Token: 0x0400036E RID: 878
		private PdfPTable table;

		// Token: 0x0400036F RID: 879
		private float minimumHeight;

		// Token: 0x04000370 RID: 880
		private int colspan = 1;

		// Token: 0x04000371 RID: 881
		private int rowspan = 1;

		// Token: 0x04000372 RID: 882
		private Image image;

		// Token: 0x04000373 RID: 883
		private IPdfPCellEvent cellEvent;

		// Token: 0x04000374 RID: 884
		private bool useDescender;

		// Token: 0x04000375 RID: 885
		private bool useBorderPadding;

		// Token: 0x04000376 RID: 886
		protected Phrase phrase;

		// Token: 0x04000377 RID: 887
		private new int rotation;
	}
}
