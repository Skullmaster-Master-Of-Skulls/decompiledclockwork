using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C0 RID: 1216
	internal class CheckBoxFlatAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FE7 RID: 20455 RVA: 0x0014B70B File Offset: 0x0014990B
		internal CheckBoxFlatAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0014B714 File Offset: 0x00149914
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.CheckState);
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

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0014B79C File Offset: 0x0014999C
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.CheckState);
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

		// Token: 0x06004FEA RID: 20458 RVA: 0x0014B824 File Offset: 0x00149A24
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.CheckState);
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

		// Token: 0x06004FEB RID: 20459 RVA: 0x0014B8AC File Offset: 0x00149AAC
		private void PaintFlatWorker(PaintEventArgs e, Color checkColor, Color checkBackground, Color checkBorder, ButtonBaseAdapter.ColorData colors)
		{
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.LayoutData layout = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layout);
			base.DrawCheckFlat(e, layout, checkColor, colors.options.highContrast ? colors.buttonFace : checkBackground, checkBorder, colors);
			base.AdjustFocusRectangle(layout);
			base.PaintField(e, layout, colors, checkColor, true);
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x0014B920 File Offset: 0x00149B20
		private new ButtonFlatAdapter ButtonAdapter
		{
			get
			{
				return (ButtonFlatAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014B92D File Offset: 0x00149B2D
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonFlatAdapter(base.Control);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0014B93C File Offset: 0x00149B3C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics));
			layoutOptions.shadowedText = false;
			return layoutOptions;
		}
	}
}
