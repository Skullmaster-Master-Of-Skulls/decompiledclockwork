using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C2 RID: 1218
	internal sealed class CheckBoxStandardAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FF7 RID: 20471 RVA: 0x0014B70B File Offset: 0x0014990B
		internal CheckBoxStandardAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014BDA0 File Offset: 0x00149FA0
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.CheckState);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			if (!layoutData.options.everettButtonCompat)
			{
				layoutData.textBounds.Offset(-1, -1);
			}
			layoutData.imageBounds.Offset(-1, -1);
			base.AdjustFocusRectangle(layoutData);
			if (!AccessibilityImprovements.Level2 || !string.IsNullOrEmpty(base.Control.Text))
			{
				int num = layoutData.focus.X & 1;
				if (!Application.RenderWithVisualStyles)
				{
					num = 1 - num;
				}
				layoutData.focus.Offset(-(num + 1), -2);
				layoutData.focus.Width = layoutData.textBounds.Width + layoutData.imageBounds.Width - 1;
				layoutData.focus.Intersect(layoutData.textBounds);
				if (layoutData.options.textAlign != (ContentAlignment)273 && layoutData.options.useCompatibleTextRendering && layoutData.options.font.Italic)
				{
					ButtonBaseAdapter.LayoutData layoutData2 = layoutData;
					layoutData2.focus.Width = layoutData2.focus.Width + 2;
				}
			}
			base.PaintImage(e, layoutData);
			base.DrawCheckBox(e, layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014BF12 File Offset: 0x0014A112
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.CheckState);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014BF42 File Offset: 0x0014A142
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.CheckState);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014BF74 File Offset: 0x0014A174
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonStandardAdapter buttonStandardAdapter = new ButtonStandardAdapter(base.Control);
				return buttonStandardAdapter.GetPreferredSizeCore(proposedSize);
			}
			Size preferredSizeCore;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, default(Rectangle)))
				{
					ButtonBaseAdapter.LayoutOptions layoutOptions = this.Layout(paintEventArgs);
					preferredSizeCore = layoutOptions.GetPreferredSizeCore(proposedSize);
				}
			}
			return preferredSizeCore;
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x0014C000 File Offset: 0x0014A200
		private new ButtonStandardAdapter ButtonAdapter
		{
			get
			{
				return (ButtonStandardAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0014C00D File Offset: 0x0014A20D
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonStandardAdapter(base.Control);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0014C01C File Offset: 0x0014A21C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkPaddingSize = 1;
			layoutOptions.everettButtonCompat = !Application.RenderWithVisualStyles;
			if (Application.RenderWithVisualStyles)
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					layoutOptions.checkSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxRenderer.ConvertFromButtonState(base.GetState(), true, base.Control.MouseIsOver), base.Control.HandleInternal).Width;
					return layoutOptions;
				}
			}
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				layoutOptions.checkSize = base.Control.LogicalToDeviceUnits(layoutOptions.checkSize);
			}
			else
			{
				layoutOptions.checkSize = (int)((double)layoutOptions.checkSize * base.GetDpiScaleRatio(e.Graphics));
			}
			return layoutOptions;
		}
	}
}
