using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003CD RID: 973
	internal class ToolStripItemInternalLayout
	{
		// Token: 0x06004302 RID: 17154 RVA: 0x0011C4F7 File Offset: 0x0011A6F7
		public ToolStripItemInternalLayout(ToolStripItem ownerItem)
		{
			if (ownerItem == null)
			{
				throw new ArgumentNullException("ownerItem");
			}
			this.ownerItem = ownerItem;
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x0011C51F File Offset: 0x0011A71F
		protected virtual ToolStripItem Owner
		{
			get
			{
				return this.ownerItem;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x0011C528 File Offset: 0x0011A728
		public virtual Rectangle ImageRectangle
		{
			get
			{
				Rectangle imageBounds = this.LayoutData.imageBounds;
				imageBounds.Intersect(this.layoutData.field);
				return imageBounds;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x0011C554 File Offset: 0x0011A754
		internal ButtonBaseAdapter.LayoutData LayoutData
		{
			get
			{
				this.EnsureLayout();
				return this.layoutData;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x0011C563 File Offset: 0x0011A763
		public Size PreferredImageSize
		{
			get
			{
				return this.Owner.PreferredImageSize;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x0011C570 File Offset: 0x0011A770
		protected virtual ToolStrip ParentInternal
		{
			get
			{
				if (this.ownerItem == null)
				{
					return null;
				}
				return this.ownerItem.ParentInternal;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x0011C588 File Offset: 0x0011A788
		public virtual Rectangle TextRectangle
		{
			get
			{
				Rectangle textBounds = this.LayoutData.textBounds;
				textBounds.Intersect(this.layoutData.field);
				return textBounds;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x0011C5B4 File Offset: 0x0011A7B4
		public virtual Rectangle ContentRectangle
		{
			get
			{
				return this.LayoutData.field;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x0011C5C1 File Offset: 0x0011A7C1
		public virtual TextFormatFlags TextFormat
		{
			get
			{
				if (this.currentLayoutOptions != null)
				{
					return this.currentLayoutOptions.gdiTextFormatFlags;
				}
				return this.CommonLayoutOptions().gdiTextFormatFlags;
			}
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x0011C5E4 File Offset: 0x0011A7E4
		internal static TextFormatFlags ContentAlignToTextFormat(ContentAlignment alignment, bool rightToLeft)
		{
			TextFormatFlags textFormatFlags = TextFormatFlags.Default;
			if (rightToLeft)
			{
				textFormatFlags |= TextFormatFlags.RightToLeft;
			}
			textFormatFlags |= ControlPaint.TranslateAlignmentForGDI(alignment);
			return textFormatFlags | ControlPaint.TranslateLineAlignmentForGDI(alignment);
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0011C614 File Offset: 0x0011A814
		protected virtual ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
		{
			ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = new ToolStripItemInternalLayout.ToolStripItemLayoutOptions();
			Rectangle client = new Rectangle(Point.Empty, this.ownerItem.Size);
			toolStripItemLayoutOptions.client = client;
			toolStripItemLayoutOptions.growBorderBy1PxWhenDefault = false;
			toolStripItemLayoutOptions.borderSize = 2;
			toolStripItemLayoutOptions.paddingSize = 0;
			toolStripItemLayoutOptions.maxFocus = true;
			toolStripItemLayoutOptions.focusOddEvenFixup = false;
			toolStripItemLayoutOptions.font = this.ownerItem.Font;
			toolStripItemLayoutOptions.text = (((this.Owner.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text) ? this.Owner.Text : string.Empty);
			toolStripItemLayoutOptions.imageSize = this.PreferredImageSize;
			toolStripItemLayoutOptions.checkSize = 0;
			toolStripItemLayoutOptions.checkPaddingSize = 0;
			toolStripItemLayoutOptions.checkAlign = ContentAlignment.TopLeft;
			toolStripItemLayoutOptions.imageAlign = this.Owner.ImageAlign;
			toolStripItemLayoutOptions.textAlign = this.Owner.TextAlign;
			toolStripItemLayoutOptions.hintTextUp = false;
			toolStripItemLayoutOptions.shadowedText = !this.ownerItem.Enabled;
			toolStripItemLayoutOptions.layoutRTL = (RightToLeft.Yes == this.Owner.RightToLeft);
			toolStripItemLayoutOptions.textImageRelation = this.Owner.TextImageRelation;
			toolStripItemLayoutOptions.textImageInset = 0;
			toolStripItemLayoutOptions.everettButtonCompat = false;
			toolStripItemLayoutOptions.gdiTextFormatFlags = ToolStripItemInternalLayout.ContentAlignToTextFormat(this.Owner.TextAlign, this.Owner.RightToLeft == RightToLeft.Yes);
			toolStripItemLayoutOptions.gdiTextFormatFlags = (this.Owner.ShowKeyboardCues ? toolStripItemLayoutOptions.gdiTextFormatFlags : (toolStripItemLayoutOptions.gdiTextFormatFlags | TextFormatFlags.HidePrefix));
			return toolStripItemLayoutOptions;
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0011C77E File Offset: 0x0011A97E
		private bool EnsureLayout()
		{
			if (this.layoutData == null || this.parentLayoutData == null || !this.parentLayoutData.IsCurrent(this.ParentInternal))
			{
				this.PerformLayout();
				return true;
			}
			return false;
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0011C7AC File Offset: 0x0011A9AC
		private ButtonBaseAdapter.LayoutData GetLayoutData()
		{
			this.currentLayoutOptions = this.CommonLayoutOptions();
			if (this.Owner.TextDirection != ToolStripTextDirection.Horizontal)
			{
				this.currentLayoutOptions.verticalText = true;
			}
			return this.currentLayoutOptions.Layout();
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x0011C7EC File Offset: 0x0011A9EC
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			this.EnsureLayout();
			if (this.ownerItem != null)
			{
				this.lastPreferredSize = this.currentLayoutOptions.GetPreferredSizeCore(constrainingSize);
				return this.lastPreferredSize;
			}
			return Size.Empty;
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x0011C82C File Offset: 0x0011AA2C
		internal void PerformLayout()
		{
			this.layoutData = this.GetLayoutData();
			ToolStrip parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				this.parentLayoutData = new ToolStripItemInternalLayout.ToolStripLayoutData(parentInternal);
				return;
			}
			this.parentLayoutData = null;
		}

		// Token: 0x04002596 RID: 9622
		private ToolStripItemInternalLayout.ToolStripItemLayoutOptions currentLayoutOptions;

		// Token: 0x04002597 RID: 9623
		private ToolStripItem ownerItem;

		// Token: 0x04002598 RID: 9624
		private ButtonBaseAdapter.LayoutData layoutData;

		// Token: 0x04002599 RID: 9625
		private const int BORDER_WIDTH = 2;

		// Token: 0x0400259A RID: 9626
		private const int BORDER_HEIGHT = 3;

		// Token: 0x0400259B RID: 9627
		private static readonly Size INVALID_SIZE = new Size(int.MinValue, int.MinValue);

		// Token: 0x0400259C RID: 9628
		private Size lastPreferredSize = ToolStripItemInternalLayout.INVALID_SIZE;

		// Token: 0x0400259D RID: 9629
		private ToolStripItemInternalLayout.ToolStripLayoutData parentLayoutData;

		// Token: 0x02000806 RID: 2054
		internal class ToolStripItemLayoutOptions : ButtonBaseAdapter.LayoutOptions
		{
			// Token: 0x06006F13 RID: 28435 RVA: 0x001977D8 File Offset: 0x001959D8
			protected override Size GetTextSize(Size proposedConstraints)
			{
				if (this.cachedSize != LayoutUtils.InvalidSize && (this.cachedProposedConstraints == proposedConstraints || this.cachedSize.Width <= proposedConstraints.Width))
				{
					return this.cachedSize;
				}
				this.cachedSize = base.GetTextSize(proposedConstraints);
				this.cachedProposedConstraints = proposedConstraints;
				return this.cachedSize;
			}

			// Token: 0x04004306 RID: 17158
			private Size cachedSize = LayoutUtils.InvalidSize;

			// Token: 0x04004307 RID: 17159
			private Size cachedProposedConstraints = LayoutUtils.InvalidSize;
		}

		// Token: 0x02000807 RID: 2055
		private class ToolStripLayoutData
		{
			// Token: 0x06006F15 RID: 28437 RVA: 0x00197858 File Offset: 0x00195A58
			public ToolStripLayoutData(ToolStrip toolStrip)
			{
				this.layoutStyle = toolStrip.LayoutStyle;
				this.autoSize = toolStrip.AutoSize;
				this.size = toolStrip.Size;
			}

			// Token: 0x06006F16 RID: 28438 RVA: 0x00197884 File Offset: 0x00195A84
			public bool IsCurrent(ToolStrip toolStrip)
			{
				return toolStrip != null && (toolStrip.Size == this.size && toolStrip.LayoutStyle == this.layoutStyle) && toolStrip.AutoSize == this.autoSize;
			}

			// Token: 0x04004308 RID: 17160
			private ToolStripLayoutStyle layoutStyle;

			// Token: 0x04004309 RID: 17161
			private bool autoSize;

			// Token: 0x0400430A RID: 17162
			private Size size;
		}
	}
}
