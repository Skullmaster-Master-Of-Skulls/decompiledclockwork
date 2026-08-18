using System;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003E9 RID: 1001
	internal class ToolStripMenuItemInternalLayout : ToolStripItemInternalLayout
	{
		// Token: 0x0600443C RID: 17468 RVA: 0x00120C6E File Offset: 0x0011EE6E
		public ToolStripMenuItemInternalLayout(ToolStripMenuItem ownerItem) : base(ownerItem)
		{
			this.ownerItem = ownerItem;
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x00120C80 File Offset: 0x0011EE80
		public bool ShowCheckMargin
		{
			get
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				return toolStripDropDownMenu != null && toolStripDropDownMenu.ShowCheckMargin;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x00120CAC File Offset: 0x0011EEAC
		public bool ShowImageMargin
		{
			get
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				return toolStripDropDownMenu != null && toolStripDropDownMenu.ShowImageMargin;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x00120CD5 File Offset: 0x0011EED5
		public bool PaintCheck
		{
			get
			{
				return this.ShowCheckMargin || this.ShowImageMargin;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x00120CE7 File Offset: 0x0011EEE7
		public bool PaintImage
		{
			get
			{
				return this.ShowImageMargin;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x00120CF0 File Offset: 0x0011EEF0
		public Rectangle ArrowRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle arrowRectangle = toolStripDropDownMenu.ArrowRectangle;
						arrowRectangle.Y = LayoutUtils.VAlign(arrowRectangle.Size, this.ownerItem.ClientBounds, ContentAlignment.MiddleCenter).Y;
						return arrowRectangle;
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x00120D50 File Offset: 0x0011EF50
		public Rectangle CheckRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle checkRectangle = toolStripDropDownMenu.CheckRectangle;
						if (this.ownerItem.CheckedImage != null)
						{
							int height = this.ownerItem.CheckedImage.Height;
							checkRectangle.Y += (checkRectangle.Height - height) / 2;
							checkRectangle.Height = height;
							return checkRectangle;
						}
					}
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x00120DC8 File Offset: 0x0011EFC8
		public override Rectangle ImageRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						Rectangle imageRectangle = toolStripDropDownMenu.ImageRectangle;
						if (this.ownerItem.ImageScaling == ToolStripItemImageScaling.SizeToFit)
						{
							imageRectangle.Size = toolStripDropDownMenu.ImageScalingSize;
						}
						else
						{
							Image image = this.ownerItem.Image ?? this.ownerItem.CheckedImage;
							imageRectangle.Size = image.Size;
						}
						imageRectangle.Y = LayoutUtils.VAlign(imageRectangle.Size, this.ownerItem.ClientBounds, ContentAlignment.MiddleCenter).Y;
						return imageRectangle;
					}
				}
				return base.ImageRectangle;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x00120E70 File Offset: 0x0011F070
		public override Rectangle TextRectangle
		{
			get
			{
				if (this.UseMenuLayout)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						return toolStripDropDownMenu.TextRectangle;
					}
				}
				return base.TextRectangle;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x00120EA6 File Offset: 0x0011F0A6
		public bool UseMenuLayout
		{
			get
			{
				return this.ownerItem.Owner is ToolStripDropDownMenu;
			}
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x00120EBC File Offset: 0x0011F0BC
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.UseMenuLayout)
			{
				ToolStripDropDownMenu toolStripDropDownMenu = this.ownerItem.Owner as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					return toolStripDropDownMenu.MaxItemSize;
				}
			}
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x04002620 RID: 9760
		private ToolStripMenuItem ownerItem;
	}
}
