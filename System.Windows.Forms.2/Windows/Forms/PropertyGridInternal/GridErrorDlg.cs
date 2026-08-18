using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000507 RID: 1287
	internal partial class GridErrorDlg : Form
	{
		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x00161E58 File Offset: 0x00160058
		public bool DetailsButtonExpanded
		{
			get
			{
				return this.detailsButtonExpanded;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (set) Token: 0x06005491 RID: 21649 RVA: 0x00161E60 File Offset: 0x00160060
		public string Details
		{
			set
			{
				this.details.Text = value;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (set) Token: 0x06005492 RID: 21650 RVA: 0x00161E6E File Offset: 0x0016006E
		public string Message
		{
			set
			{
				this.lblMessage.Text = value;
			}
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x00161E7C File Offset: 0x0016007C
		public GridErrorDlg(PropertyGrid owner)
		{
			this.ownerGrid = owner;
			this.expandImage = new Bitmap(typeof(ThreadExceptionDialog), "down.bmp");
			this.expandImage.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref this.expandImage, 0);
			}
			this.collapseImage = new Bitmap(typeof(ThreadExceptionDialog), "up.bmp");
			this.collapseImage.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref this.collapseImage, 0);
			}
			this.InitializeComponent();
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.SupportsUseCompatibleTextRendering)
				{
					control.UseCompatibleTextRenderingInt = this.ownerGrid.UseCompatibleTextRendering;
				}
			}
			this.pictureBox.Image = SystemIcons.Warning.ToBitmap();
			this.detailsBtn.Text = " " + SR.GetString("ExDlgShowDetails");
			this.details.AccessibleName = SR.GetString("ExDlgDetailsText");
			this.okBtn.Text = SR.GetString("ExDlgOk");
			this.cancelBtn.Text = SR.GetString("ExDlgCancel");
			this.detailsBtn.Image = this.expandImage;
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x00161FF0 File Offset: 0x001601F0
		private void DetailsClick(object sender, EventArgs devent)
		{
			int num = this.details.Height + 8;
			if (this.details.Visible)
			{
				this.detailsBtn.Image = this.expandImage;
				this.detailsButtonExpanded = false;
				base.Height -= num;
			}
			else
			{
				this.detailsBtn.Image = this.collapseImage;
				this.detailsButtonExpanded = true;
				this.details.Width = this.overarchingTableLayoutPanel.Width - this.details.Margin.Horizontal;
				base.Height += num;
			}
			this.details.Visible = !this.details.Visible;
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				this.details.TabStop = !this.details.TabStop;
				if (this.details.Visible)
				{
					this.details.Focus();
				}
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x001620FA File Offset: 0x001602FA
		private static bool IsRTLResources
		{
			get
			{
				return SR.GetString("RTL") != "RTL_False";
			}
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x001628AF File Offset: 0x00160AAF
		private void OnButtonClick(object s, EventArgs e)
		{
			base.DialogResult = ((Button)s).DialogResult;
			base.Close();
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x001628C8 File Offset: 0x00160AC8
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				using (Graphics graphics = base.CreateGraphics())
				{
					int num = (int)Math.Ceiling((double)PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, graphics, this.detailsBtn.Text, this.detailsBtn.Font).Width);
					num += this.detailsBtn.Image.Width;
					this.detailsBtn.Width = (int)Math.Ceiling((double)((float)num * (this.ownerGrid.UseCompatibleTextRendering ? 1.15f : 1.4f)));
					this.detailsBtn.Height = this.okBtn.Height;
				}
				int x = this.details.Location.X;
				int num2 = this.detailsBtn.Location.Y + this.detailsBtn.Height + this.detailsBtn.Margin.Bottom;
				Control parent = this.detailsBtn.Parent;
				while (parent != null && !(parent is Form))
				{
					num2 += parent.Location.Y;
					parent = parent.Parent;
				}
				this.details.Location = new Point(x, num2);
				if (this.details.Visible)
				{
					this.DetailsClick(this.details, EventArgs.Empty);
				}
			}
			this.okBtn.Focus();
		}

		// Token: 0x04003717 RID: 14103
		private Bitmap expandImage;

		// Token: 0x04003718 RID: 14104
		private Bitmap collapseImage;

		// Token: 0x04003719 RID: 14105
		private PropertyGrid ownerGrid;

		// Token: 0x0400371A RID: 14106
		private bool detailsButtonExpanded;
	}
}
