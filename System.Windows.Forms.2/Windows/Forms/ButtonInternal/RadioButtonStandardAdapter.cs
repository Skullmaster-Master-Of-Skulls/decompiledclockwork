using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C6 RID: 1222
	internal class RadioButtonStandardAdapter : RadioButtonBaseAdapter
	{
		// Token: 0x06005018 RID: 20504 RVA: 0x0014C8C2 File Offset: 0x0014AAC2
		internal RadioButtonStandardAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0014CE20 File Offset: 0x0014B020
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layout = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layout);
			base.DrawCheckBox(e, layout);
			base.AdjustFocusRectangle(layout);
			base.PaintField(e, layout, colorData, colorData.windowText, true);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0014CEB2 File Offset: 0x0014B0B2
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0014CEE8 File Offset: 0x0014B0E8
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x0014C000 File Offset: 0x0014A200
		private new ButtonStandardAdapter ButtonAdapter
		{
			get
			{
				return (ButtonStandardAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0014CF1E File Offset: 0x0014B11E
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonStandardAdapter(base.Control);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0014CF2C File Offset: 0x0014B12C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.hintTextUp = false;
			layoutOptions.everettButtonCompat = !Application.RenderWithVisualStyles;
			if (Application.RenderWithVisualStyles)
			{
				ButtonBase control = base.Control;
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					layoutOptions.checkSize = RadioButtonRenderer.GetGlyphSize(graphics, RadioButtonRenderer.ConvertFromButtonState(base.GetState(), control.MouseIsOver), control.HandleInternal).Width;
					return layoutOptions;
				}
			}
			layoutOptions.checkSize = (int)((double)layoutOptions.checkSize * base.GetDpiScaleRatio(e.Graphics));
			return layoutOptions;
		}
	}
}
