using System;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FD RID: 1277
	internal class DocComment : PropertyGrid.SnappableControl
	{
		// Token: 0x060053A8 RID: 21416 RVA: 0x0015E6E4 File Offset: 0x0015C8E4
		internal DocComment(PropertyGrid owner) : base(owner)
		{
			base.SuspendLayout();
			this.m_labelTitle = new Label();
			this.m_labelTitle.UseMnemonic = false;
			this.m_labelTitle.Cursor = Cursors.Default;
			this.m_labelDesc = new Label();
			this.m_labelDesc.AutoEllipsis = true;
			this.m_labelDesc.Cursor = Cursors.Default;
			this.UpdateTextRenderingEngine();
			base.Controls.Add(this.m_labelTitle);
			base.Controls.Add(this.m_labelDesc);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.cBorder = base.LogicalToDeviceUnits(3);
				this.cydef = base.LogicalToDeviceUnits(59);
			}
			base.Size = new Size(0, this.cydef);
			this.Text = SR.GetString("PBRSDocCommentPaneTitle");
			base.SetStyle(ControlStyles.Selectable, false);
			base.ResumeLayout(false);
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x0015E7EC File Offset: 0x0015C9EC
		// (set) Token: 0x060053AA RID: 21418 RVA: 0x0015E801 File Offset: 0x0015CA01
		public virtual int Lines
		{
			get
			{
				this.UpdateUIWithFont();
				return base.Height / this.lineHeight;
			}
			set
			{
				this.UpdateUIWithFont();
				base.Size = new Size(base.Width, 1 + value * this.lineHeight);
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x0015E824 File Offset: 0x0015CA24
		public override int GetOptimalHeight(int width)
		{
			this.UpdateUIWithFont();
			int num = this.m_labelTitle.Size.Height;
			if (this.ownerGrid.IsHandleCreated && !base.IsHandleCreated)
			{
				base.CreateControl();
			}
			Graphics graphics = this.m_labelDesc.CreateGraphicsInternal();
			SizeF value = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, graphics, this.m_labelTitle.Text, this.Font, width);
			Size size = Size.Ceiling(value);
			graphics.Dispose();
			int num2 = DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(2) : 2;
			num += size.Height * 2 + num2;
			return Math.Max(num + 2 * num2, DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(59) : 59);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void LayoutWindow()
		{
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0015E8DF File Offset: 0x0015CADF
		protected override void OnFontChanged(EventArgs e)
		{
			this.needUpdateUIWithFont = true;
			base.PerformLayout();
			base.OnFontChanged(e);
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x0015E8F5 File Offset: 0x0015CAF5
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.UpdateUIWithFont();
			this.SetChildLabelsBounds();
			this.m_labelDesc.Text = this.fullDesc;
			this.m_labelDesc.AccessibleName = this.fullDesc;
			base.OnLayout(e);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0015E92C File Offset: 0x0015CB2C
		protected override void OnResize(EventArgs e)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (!this.rect.IsEmpty && clientRectangle.Width > this.rect.Width)
			{
				Rectangle rc = new Rectangle(this.rect.Width - 1, 0, clientRectangle.Width - this.rect.Width + 1, this.rect.Height);
				base.Invalidate(rc);
			}
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.lineHeight = this.Font.Height + base.LogicalToDeviceUnits(2);
				if (base.ClientRectangle.Width != this.rect.Width || base.ClientRectangle.Height != this.rect.Height)
				{
					this.m_labelTitle.Location = new Point(this.cBorder, this.cBorder);
					this.m_labelDesc.Location = new Point(this.cBorder, this.cBorder + this.lineHeight);
					this.SetChildLabelsBounds();
				}
			}
			this.rect = clientRectangle;
			base.OnResize(e);
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0015EA4C File Offset: 0x0015CC4C
		private void SetChildLabelsBounds()
		{
			Size clientSize = base.ClientSize;
			clientSize.Width = Math.Max(0, clientSize.Width - 2 * this.cBorder);
			clientSize.Height = Math.Max(0, clientSize.Height - 2 * this.cBorder);
			this.m_labelTitle.SetBounds(this.m_labelTitle.Top, this.m_labelTitle.Left, clientSize.Width, Math.Min(this.lineHeight, clientSize.Height), BoundsSpecified.Size);
			this.m_labelDesc.SetBounds(this.m_labelDesc.Top, this.m_labelDesc.Left, clientSize.Width, Math.Max(0, clientSize.Height - this.lineHeight - (DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(1) : 1)), BoundsSpecified.Size);
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0015EB26 File Offset: 0x0015CD26
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateUIWithFont();
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0015EB35 File Offset: 0x0015CD35
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.cBorder = base.LogicalToDeviceUnits(3);
				this.cydef = base.LogicalToDeviceUnits(59);
			}
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0015EB64 File Offset: 0x0015CD64
		public virtual void SetComment(string title, string desc)
		{
			if (this.m_labelDesc.Text != title)
			{
				this.m_labelTitle.Text = title;
			}
			if (desc != this.fullDesc)
			{
				this.fullDesc = desc;
				this.m_labelDesc.Text = this.fullDesc;
				this.m_labelDesc.AccessibleName = this.fullDesc;
			}
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0015EBC8 File Offset: 0x0015CDC8
		public override int SnapHeightRequest(int cyNew)
		{
			this.UpdateUIWithFont();
			int num = Math.Max(2, cyNew / this.lineHeight);
			return 1 + num * this.lineHeight;
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0015EBF4 File Offset: 0x0015CDF4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DocCommentAccessibleObject(this, this.ownerGrid);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x000A8615 File Offset: 0x000A6815
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0015EC10 File Offset: 0x0015CE10
		internal void UpdateTextRenderingEngine()
		{
			this.m_labelTitle.UseCompatibleTextRendering = this.ownerGrid.UseCompatibleTextRendering;
			this.m_labelDesc.UseCompatibleTextRendering = this.ownerGrid.UseCompatibleTextRendering;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0015EC40 File Offset: 0x0015CE40
		private void UpdateUIWithFont()
		{
			if (base.IsHandleCreated && this.needUpdateUIWithFont)
			{
				try
				{
					this.m_labelTitle.Font = new Font(this.Font, FontStyle.Bold);
				}
				catch
				{
				}
				this.lineHeight = this.Font.Height + 2;
				this.m_labelTitle.Location = new Point(this.cBorder, this.cBorder);
				this.m_labelDesc.Location = new Point(this.cBorder, this.cBorder + this.lineHeight);
				this.needUpdateUIWithFont = false;
				base.PerformLayout();
			}
		}

		// Token: 0x040036C3 RID: 14019
		private Label m_labelTitle;

		// Token: 0x040036C4 RID: 14020
		private Label m_labelDesc;

		// Token: 0x040036C5 RID: 14021
		private string fullDesc;

		// Token: 0x040036C6 RID: 14022
		protected int lineHeight;

		// Token: 0x040036C7 RID: 14023
		private bool needUpdateUIWithFont = true;

		// Token: 0x040036C8 RID: 14024
		protected const int CBORDER = 3;

		// Token: 0x040036C9 RID: 14025
		protected const int CXDEF = 0;

		// Token: 0x040036CA RID: 14026
		protected const int CYDEF = 59;

		// Token: 0x040036CB RID: 14027
		protected const int MIN_LINES = 2;

		// Token: 0x040036CC RID: 14028
		private int cydef = 59;

		// Token: 0x040036CD RID: 14029
		private int cBorder = 3;

		// Token: 0x040036CE RID: 14030
		internal Rectangle rect = Rectangle.Empty;
	}
}
