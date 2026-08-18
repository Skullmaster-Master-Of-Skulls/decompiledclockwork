using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003DE RID: 990
	public class ToolStripItemTextRenderEventArgs : ToolStripItemRenderEventArgs
	{
		// Token: 0x0600435B RID: 17243 RVA: 0x0011D228 File Offset: 0x0011B428
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, TextFormatFlags format) : base(g, item)
		{
			this.text = text;
			this.textRectangle = textRectangle;
			this.defaultTextColor = textColor;
			this.textFont = textFont;
			this.textAlignment = item.TextAlign;
			this.textFormat = format;
			this.textDirection = item.TextDirection;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x0011D2A4 File Offset: 0x0011B4A4
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, ContentAlignment textAlign) : base(g, item)
		{
			this.text = text;
			this.textRectangle = textRectangle;
			this.defaultTextColor = textColor;
			this.textFont = textFont;
			this.textFormat = ToolStripItemInternalLayout.ContentAlignToTextFormat(textAlign, item.RightToLeft == RightToLeft.Yes);
			this.textFormat = (item.ShowKeyboardCues ? this.textFormat : (this.textFormat | TextFormatFlags.HidePrefix));
			this.textDirection = item.TextDirection;
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x0011D344 File Offset: 0x0011B544
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x0011D34C File Offset: 0x0011B54C
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x0011D355 File Offset: 0x0011B555
		// (set) Token: 0x06004360 RID: 17248 RVA: 0x0011D36C File Offset: 0x0011B56C
		public Color TextColor
		{
			get
			{
				if (this.textColorChanged)
				{
					return this.textColor;
				}
				return this.DefaultTextColor;
			}
			set
			{
				this.textColor = value;
				this.textColorChanged = true;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x0011D37C File Offset: 0x0011B57C
		// (set) Token: 0x06004362 RID: 17250 RVA: 0x0011D384 File Offset: 0x0011B584
		internal Color DefaultTextColor
		{
			get
			{
				return this.defaultTextColor;
			}
			set
			{
				this.defaultTextColor = value;
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x0011D38D File Offset: 0x0011B58D
		// (set) Token: 0x06004364 RID: 17252 RVA: 0x0011D395 File Offset: 0x0011B595
		public Font TextFont
		{
			get
			{
				return this.textFont;
			}
			set
			{
				this.textFont = value;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x0011D39E File Offset: 0x0011B59E
		// (set) Token: 0x06004366 RID: 17254 RVA: 0x0011D3A6 File Offset: 0x0011B5A6
		public Rectangle TextRectangle
		{
			get
			{
				return this.textRectangle;
			}
			set
			{
				this.textRectangle = value;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x0011D3AF File Offset: 0x0011B5AF
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x0011D3B7 File Offset: 0x0011B5B7
		public TextFormatFlags TextFormat
		{
			get
			{
				return this.textFormat;
			}
			set
			{
				this.textFormat = value;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x0011D3C0 File Offset: 0x0011B5C0
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x0011D3C8 File Offset: 0x0011B5C8
		public ToolStripTextDirection TextDirection
		{
			get
			{
				return this.textDirection;
			}
			set
			{
				this.textDirection = value;
			}
		}

		// Token: 0x040025CF RID: 9679
		private string text;

		// Token: 0x040025D0 RID: 9680
		private Rectangle textRectangle = Rectangle.Empty;

		// Token: 0x040025D1 RID: 9681
		private Color textColor = SystemColors.ControlText;

		// Token: 0x040025D2 RID: 9682
		private Font textFont;

		// Token: 0x040025D3 RID: 9683
		private ContentAlignment textAlignment;

		// Token: 0x040025D4 RID: 9684
		private ToolStripTextDirection textDirection = ToolStripTextDirection.Horizontal;

		// Token: 0x040025D5 RID: 9685
		private TextFormatFlags textFormat;

		// Token: 0x040025D6 RID: 9686
		private Color defaultTextColor = SystemColors.ControlText;

		// Token: 0x040025D7 RID: 9687
		private bool textColorChanged;
	}
}
