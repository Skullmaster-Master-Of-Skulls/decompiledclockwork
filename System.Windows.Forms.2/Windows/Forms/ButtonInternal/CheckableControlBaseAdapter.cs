using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BE RID: 1214
	internal abstract class CheckableControlBaseAdapter : ButtonBaseAdapter
	{
		// Token: 0x06004FD1 RID: 20433 RVA: 0x0014982C File Offset: 0x00147A2C
		internal CheckableControlBaseAdapter(ButtonBase control) : base(control)
		{
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06004FD2 RID: 20434 RVA: 0x0014ACA5 File Offset: 0x00148EA5
		protected ButtonBaseAdapter ButtonAdapter
		{
			get
			{
				if (this.buttonAdapter == null)
				{
					this.buttonAdapter = this.CreateButtonAdapter();
				}
				return this.buttonAdapter;
			}
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0014ACC4 File Offset: 0x00148EC4
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.Appearance == Appearance.Button)
			{
				return this.ButtonAdapter.GetPreferredSizeCore(proposedSize);
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

		// Token: 0x06004FD4 RID: 20436
		protected abstract ButtonBaseAdapter CreateButtonAdapter();

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0014AD44 File Offset: 0x00148F44
		private Appearance Appearance
		{
			get
			{
				CheckBox checkBox = base.Control as CheckBox;
				if (checkBox != null)
				{
					return checkBox.Appearance;
				}
				RadioButton radioButton = base.Control as RadioButton;
				if (radioButton != null)
				{
					return radioButton.Appearance;
				}
				return Appearance.Normal;
			}
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0014AD80 File Offset: 0x00148F80
		internal override ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.CommonLayout();
			layoutOptions.growBorderBy1PxWhenDefault = false;
			layoutOptions.borderSize = 0;
			layoutOptions.paddingSize = 0;
			layoutOptions.maxFocus = false;
			layoutOptions.focusOddEvenFixup = true;
			layoutOptions.checkSize = 13;
			return layoutOptions;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0014ADC0 File Offset: 0x00148FC0
		internal double GetDpiScaleRatio(Graphics g)
		{
			return CheckableControlBaseAdapter.GetDpiScaleRatio(g, base.Control);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014ADCE File Offset: 0x00148FCE
		internal static double GetDpiScaleRatio(Graphics g, Control control)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling && control != null && control.IsHandleCreated)
			{
				return (double)control.deviceDpi / 96.0;
			}
			if (g == null)
			{
				return 1.0;
			}
			return (double)(g.DpiX / 96f);
		}

		// Token: 0x04003475 RID: 13429
		private const int standardCheckSize = 13;

		// Token: 0x04003476 RID: 13430
		private ButtonBaseAdapter buttonAdapter;
	}
}
