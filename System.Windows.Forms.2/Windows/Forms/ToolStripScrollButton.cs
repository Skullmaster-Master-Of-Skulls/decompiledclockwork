using System;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020003FE RID: 1022
	internal class ToolStripScrollButton : ToolStripControlHost
	{
		// Token: 0x0600467E RID: 18046 RVA: 0x00128A3E File Offset: 0x00126C3E
		public ToolStripScrollButton(bool up) : base(ToolStripScrollButton.CreateControlInstance(up))
		{
			this.up = up;
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x00128A5C File Offset: 0x00126C5C
		private static Control CreateControlInstance(bool up)
		{
			return new ToolStripScrollButton.StickyLabel
			{
				ImageAlign = ContentAlignment.MiddleCenter,
				Image = (up ? ToolStripScrollButton.UpImage : ToolStripScrollButton.DownImage)
			};
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected override Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x00128A8D File Offset: 0x00126C8D
		private static Image DownImage
		{
			get
			{
				if (ToolStripScrollButton.downScrollImage == null)
				{
					ToolStripScrollButton.downScrollImage = new Bitmap(typeof(ToolStripScrollButton), "ScrollButtonDown.bmp");
					ToolStripScrollButton.downScrollImage.MakeTransparent(Color.White);
				}
				return ToolStripScrollButton.downScrollImage;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x00128AC3 File Offset: 0x00126CC3
		internal ToolStripScrollButton.StickyLabel Label
		{
			get
			{
				return base.Control as ToolStripScrollButton.StickyLabel;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x00128AD0 File Offset: 0x00126CD0
		private static Image UpImage
		{
			get
			{
				if (ToolStripScrollButton.upScrollImage == null)
				{
					ToolStripScrollButton.upScrollImage = new Bitmap(typeof(ToolStripScrollButton), "ScrollButtonUp.bmp");
					ToolStripScrollButton.upScrollImage.MakeTransparent(Color.White);
				}
				return ToolStripScrollButton.upScrollImage;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x00128B06 File Offset: 0x00126D06
		private Timer MouseDownTimer
		{
			get
			{
				if (this.mouseDownTimer == null)
				{
					this.mouseDownTimer = new Timer();
				}
				return this.mouseDownTimer;
			}
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x00128B21 File Offset: 0x00126D21
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.mouseDownTimer != null)
			{
				this.mouseDownTimer.Enabled = false;
				this.mouseDownTimer.Dispose();
				this.mouseDownTimer = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x00128B54 File Offset: 0x00126D54
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.UnsubscribeAll();
			base.OnMouseDown(e);
			this.Scroll();
			this.MouseDownTimer.Interval = ToolStripScrollButton.AUTOSCROLL_PAUSE;
			this.MouseDownTimer.Tick += this.OnInitialAutoScrollMouseDown;
			this.MouseDownTimer.Enabled = true;
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x00128BA7 File Offset: 0x00126DA7
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.UnsubscribeAll();
			base.OnMouseUp(e);
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x00128BB6 File Offset: 0x00126DB6
		protected override void OnMouseLeave(EventArgs e)
		{
			this.UnsubscribeAll();
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x00128BBE File Offset: 0x00126DBE
		private void UnsubscribeAll()
		{
			this.MouseDownTimer.Enabled = false;
			this.MouseDownTimer.Tick -= this.OnInitialAutoScrollMouseDown;
			this.MouseDownTimer.Tick -= this.OnAutoScrollAccellerate;
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00128BFA File Offset: 0x00126DFA
		private void OnAutoScrollAccellerate(object sender, EventArgs e)
		{
			this.Scroll();
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00128C04 File Offset: 0x00126E04
		private void OnInitialAutoScrollMouseDown(object sender, EventArgs e)
		{
			this.MouseDownTimer.Tick -= this.OnInitialAutoScrollMouseDown;
			this.Scroll();
			this.MouseDownTimer.Interval = 50;
			this.MouseDownTimer.Tick += this.OnAutoScrollAccellerate;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00128C54 File Offset: 0x00126E54
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			empty.Height = ((this.Label.Image != null) ? (this.Label.Image.Height + 4) : 0);
			empty.Width = ((base.ParentInternal != null) ? (base.ParentInternal.Width - 2) : empty.Width);
			return empty;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x00128CB8 File Offset: 0x00126EB8
		private void Scroll()
		{
			ToolStripDropDownMenu toolStripDropDownMenu = base.ParentInternal as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null && this.Label.Enabled)
			{
				toolStripDropDownMenu.ScrollInternal(this.up);
			}
		}

		// Token: 0x040026B5 RID: 9909
		private bool up = true;

		// Token: 0x040026B6 RID: 9910
		[ThreadStatic]
		private static Bitmap upScrollImage;

		// Token: 0x040026B7 RID: 9911
		[ThreadStatic]
		private static Bitmap downScrollImage;

		// Token: 0x040026B8 RID: 9912
		private const int AUTOSCROLL_UPDATE = 50;

		// Token: 0x040026B9 RID: 9913
		private static readonly int AUTOSCROLL_PAUSE = SystemInformation.DoubleClickTime;

		// Token: 0x040026BA RID: 9914
		private Timer mouseDownTimer;

		// Token: 0x02000819 RID: 2073
		internal class StickyLabel : Label
		{
			// Token: 0x1700186B RID: 6251
			// (get) Token: 0x06006FCC RID: 28620 RVA: 0x0019ADCD File Offset: 0x00198FCD
			public bool FreezeLocationChange
			{
				get
				{
					return this.freezeLocationChange;
				}
			}

			// Token: 0x06006FCD RID: 28621 RVA: 0x0019ADD5 File Offset: 0x00198FD5
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				if ((specified & BoundsSpecified.Location) != BoundsSpecified.None && this.FreezeLocationChange)
				{
					return;
				}
				base.SetBoundsCore(x, y, width, height, specified);
			}

			// Token: 0x06006FCE RID: 28622 RVA: 0x0019ADF3 File Offset: 0x00198FF3
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					this.DefWndProc(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400432B RID: 17195
			private bool freezeLocationChange;
		}
	}
}
