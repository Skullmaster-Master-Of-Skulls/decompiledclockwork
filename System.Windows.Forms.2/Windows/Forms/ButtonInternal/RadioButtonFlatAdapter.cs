using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C4 RID: 1220
	internal class RadioButtonFlatAdapter : RadioButtonBaseAdapter
	{
		// Token: 0x0600500B RID: 20491 RVA: 0x0014C8C2 File Offset: 0x0014AAC2
		internal RadioButtonFlatAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0014C8CC File Offset: 0x0014AACC
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.highlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0014C960 File Offset: 0x0014AB60
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.lowHighlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014C9F4 File Offset: 0x0014ABF4
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.highlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0014CA88 File Offset: 0x0014AC88
		private void PaintFlatWorker(PaintEventArgs e, Color checkColor, Color checkBackground, Color checkBorder, ButtonBaseAdapter.ColorData colors)
		{
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.LayoutData layout = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layout);
			base.DrawCheckFlat(e, layout, checkColor, colors.options.highContrast ? colors.buttonFace : checkBackground, checkBorder);
			base.AdjustFocusRectangle(layout);
			base.PaintField(e, layout, colors, checkColor, true);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014CAFA File Offset: 0x0014ACFA
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonFlatAdapter(base.Control);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014CB08 File Offset: 0x0014AD08
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkSize = (int)(12.0 * base.GetDpiScaleRatio(e.Graphics));
			layoutOptions.shadowedText = false;
			return layoutOptions;
		}

		// Token: 0x0400347C RID: 13436
		protected const int flatCheckSize = 12;
	}
}
