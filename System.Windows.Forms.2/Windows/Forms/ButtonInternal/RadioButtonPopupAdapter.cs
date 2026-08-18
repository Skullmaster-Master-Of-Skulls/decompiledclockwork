using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C5 RID: 1221
	internal class RadioButtonPopupAdapter : RadioButtonFlatAdapter
	{
		// Token: 0x06005012 RID: 20498 RVA: 0x0014CB41 File Offset: 0x0014AD41
		internal RadioButtonPopupAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014CB4C File Offset: 0x0014AD4C
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackgroundFlat(e, layoutData.checkBounds, colorData.buttonShadow, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight);
			base.DrawCheckOnly(e, layoutData, colorData.windowText, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0014CC28 File Offset: 0x0014AE28
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			Color checkBackground = (colorData.options.highContrast && AccessibilityImprovements.Level1) ? colorData.buttonFace : colorData.highlight;
			base.DrawCheckBackground3DLite(e, layoutData.checkBounds, colorData.windowText, checkBackground, colorData, true);
			base.DrawCheckOnly(e, layoutData, colorData.windowText, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014CD10 File Offset: 0x0014AF10
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground3DLite(e, layoutData.checkBounds, colorData.windowText, colorData.highlight, colorData, true);
			base.DrawCheckOnly(e, layoutData, colorData.buttonShadow, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0014CDD8 File Offset: 0x0014AFD8
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonPopupAdapter(base.Control);
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0014CDE8 File Offset: 0x0014AFE8
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.Layout(e);
			if (!base.Control.MouseIsDown && !base.Control.MouseIsOver)
			{
				layoutOptions.shadowedText = true;
			}
			return layoutOptions;
		}
	}
}
