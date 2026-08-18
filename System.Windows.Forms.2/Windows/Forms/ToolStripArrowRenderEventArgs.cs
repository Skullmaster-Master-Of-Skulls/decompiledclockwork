using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020003B4 RID: 948
	public class ToolStripArrowRenderEventArgs : EventArgs
	{
		// Token: 0x06003EE4 RID: 16100 RVA: 0x00110FC0 File Offset: 0x0010F1C0
		public ToolStripArrowRenderEventArgs(Graphics g, ToolStripItem toolStripItem, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
		{
			this.item = toolStripItem;
			this.graphics = g;
			this.arrowRect = arrowRectangle;
			this.defaultArrowColor = arrowColor;
			this.arrowDirection = arrowDirection;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x00111021 File Offset: 0x0010F221
		// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x00111029 File Offset: 0x0010F229
		public Rectangle ArrowRectangle
		{
			get
			{
				return this.arrowRect;
			}
			set
			{
				this.arrowRect = value;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x00111032 File Offset: 0x0010F232
		// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x00111049 File Offset: 0x0010F249
		public Color ArrowColor
		{
			get
			{
				if (this.arrowColorChanged)
				{
					return this.arrowColor;
				}
				return this.DefaultArrowColor;
			}
			set
			{
				this.arrowColor = value;
				this.arrowColorChanged = true;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x00111059 File Offset: 0x0010F259
		// (set) Token: 0x06003EEA RID: 16106 RVA: 0x00111061 File Offset: 0x0010F261
		internal Color DefaultArrowColor
		{
			get
			{
				return this.defaultArrowColor;
			}
			set
			{
				this.defaultArrowColor = value;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x0011106A File Offset: 0x0010F26A
		// (set) Token: 0x06003EEC RID: 16108 RVA: 0x00111072 File Offset: 0x0010F272
		public ArrowDirection Direction
		{
			get
			{
				return this.arrowDirection;
			}
			set
			{
				this.arrowDirection = value;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x0011107B File Offset: 0x0010F27B
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x00111083 File Offset: 0x0010F283
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04002499 RID: 9369
		private Graphics graphics;

		// Token: 0x0400249A RID: 9370
		private Rectangle arrowRect = Rectangle.Empty;

		// Token: 0x0400249B RID: 9371
		private Color arrowColor = Color.Empty;

		// Token: 0x0400249C RID: 9372
		private Color defaultArrowColor = Color.Empty;

		// Token: 0x0400249D RID: 9373
		private ArrowDirection arrowDirection = ArrowDirection.Down;

		// Token: 0x0400249E RID: 9374
		private ToolStripItem item;

		// Token: 0x0400249F RID: 9375
		private bool arrowColorChanged;
	}
}
