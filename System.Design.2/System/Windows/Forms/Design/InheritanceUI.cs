using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FA RID: 762
	internal class InheritanceUI
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000B714C File Offset: 0x000B534C
		public Bitmap InheritanceGlyph
		{
			get
			{
				if (InheritanceUI.inheritanceGlyph == null)
				{
					InheritanceUI.inheritanceGlyph = new Bitmap(BitmapSelector.GetResourceStream(typeof(InheritanceUI), "InheritedGlyph.bmp"));
					InheritanceUI.inheritanceGlyph.MakeTransparent();
					if (DpiHelper.IsScalingRequired)
					{
						DpiHelper.ScaleBitmapLogicalToDevice(ref InheritanceUI.inheritanceGlyph, 0);
					}
				}
				return InheritanceUI.inheritanceGlyph;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000B71A0 File Offset: 0x000B53A0
		public Rectangle InheritanceGlyphRectangle
		{
			get
			{
				if (InheritanceUI.inheritanceGlyphRect == Rectangle.Empty)
				{
					Size size = this.InheritanceGlyph.Size;
					InheritanceUI.inheritanceGlyphRect = new Rectangle(0, 0, size.Width, size.Height);
				}
				return InheritanceUI.inheritanceGlyphRect;
			}
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000B71EC File Offset: 0x000B53EC
		public void AddInheritedControl(Control c, InheritanceLevel level)
		{
			if (this.tooltip == null)
			{
				this.tooltip = new ToolTip();
				this.tooltip.ShowAlways = true;
			}
			string @string;
			if (level == InheritanceLevel.InheritedReadOnly)
			{
				@string = SR.GetString("DesignerInheritedReadOnly");
			}
			else
			{
				@string = SR.GetString("DesignerInherited");
			}
			this.tooltip.SetToolTip(c, @string);
			foreach (object obj in c.Controls)
			{
				Control control = (Control)obj;
				if (control.Site == null)
				{
					this.tooltip.SetToolTip(control, @string);
				}
			}
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000B729C File Offset: 0x000B549C
		public void Dispose()
		{
			if (this.tooltip != null)
			{
				this.tooltip.Dispose();
			}
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B72B4 File Offset: 0x000B54B4
		public void RemoveInheritedControl(Control c)
		{
			if (this.tooltip != null && this.tooltip.GetToolTip(c).Length > 0)
			{
				this.tooltip.SetToolTip(c, null);
				foreach (object obj in c.Controls)
				{
					Control control = (Control)obj;
					if (control.Site == null)
					{
						this.tooltip.SetToolTip(control, null);
					}
				}
			}
		}

		// Token: 0x040017CA RID: 6090
		private static Bitmap inheritanceGlyph;

		// Token: 0x040017CB RID: 6091
		private static Rectangle inheritanceGlyphRect;

		// Token: 0x040017CC RID: 6092
		private ToolTip tooltip;
	}
}
