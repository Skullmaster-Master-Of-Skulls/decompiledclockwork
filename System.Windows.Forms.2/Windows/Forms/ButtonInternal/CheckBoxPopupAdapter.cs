using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C1 RID: 1217
	internal class CheckBoxPopupAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FEF RID: 20463 RVA: 0x0014B70B File Offset: 0x0014990B
		internal CheckBoxPopupAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014B978 File Offset: 0x00149B78
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintUp(e, base.Control.CheckState);
				return;
			}
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, false).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight, true, colorData);
			ButtonBaseAdapter.DrawFlatBorder(e.Graphics, layoutData.checkBounds, (colorData.options.highContrast && !base.Control.Enabled && AccessibilityImprovements.Level1) ? colorData.windowFrame : colorData.buttonShadow);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.highlight);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014BAA0 File Offset: 0x00149CA0
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintOver(e, base.Control.CheckState);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, true).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight, true, colorData);
			CheckBoxBaseAdapter.DrawPopupBorder(graphics, layoutData.checkBounds, colorData);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.highlight);
			if (!AccessibilityImprovements.Level2 || !string.IsNullOrEmpty(base.Control.Text))
			{
				e.Graphics.Clip = clip;
				e.Graphics.ExcludeClip(layoutData.checkArea);
			}
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014BBCC File Offset: 0x00149DCC
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintDown(e, base.Control.CheckState);
				return;
			}
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, true).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.buttonFace, true, colorData);
			CheckBoxBaseAdapter.DrawPopupBorder(graphics, layoutData.checkBounds, colorData);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.buttonFace);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0014BCAA File Offset: 0x00149EAA
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonPopupAdapter(base.Control);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0014BCB8 File Offset: 0x00149EB8
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			return this.PaintPopupLayout(e, true);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0014BCD0 File Offset: 0x00149ED0
		internal static ButtonBaseAdapter.LayoutOptions PaintPopupLayout(Graphics g, bool show3D, int checkSize, Rectangle clientRectangle, Padding padding, bool isDefault, Font font, string text, bool enabled, ContentAlignment textAlign, RightToLeft rtl, Control control = null)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = ButtonBaseAdapter.CommonLayout(clientRectangle, padding, isDefault, font, text, enabled, textAlign, rtl);
			layoutOptions.shadowedText = false;
			if (show3D)
			{
				layoutOptions.checkSize = (int)((double)checkSize * CheckableControlBaseAdapter.GetDpiScaleRatio(g, control) + 1.0);
			}
			else
			{
				layoutOptions.checkSize = (int)((double)checkSize * CheckableControlBaseAdapter.GetDpiScaleRatio(g, control));
				layoutOptions.checkPaddingSize = 1;
			}
			return layoutOptions;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0014BD34 File Offset: 0x00149F34
		private ButtonBaseAdapter.LayoutOptions PaintPopupLayout(PaintEventArgs e, bool show3D)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.shadowedText = false;
			if (show3D)
			{
				layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics) + 1.0);
			}
			else
			{
				layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics));
				layoutOptions.checkPaddingSize = 1;
			}
			return layoutOptions;
		}
	}
}
