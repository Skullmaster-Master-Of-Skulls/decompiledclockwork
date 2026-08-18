using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002EB RID: 747
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public sealed class MdiClient : Control
	{
		// Token: 0x06002F6A RID: 12138 RVA: 0x000D5B9C File Offset: 0x000D3D9C
		public MdiClient()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			this.BackColor = SystemColors.AppWorkspace;
			this.Dock = DockStyle.Fill;
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000D5BD0 File Offset: 0x000D3DD0
		// (set) Token: 0x06002F6C RID: 12140 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Localizable(true)]
		public override Image BackgroundImage
		{
			get
			{
				Image backgroundImage = base.BackgroundImage;
				if (backgroundImage == null && this.ParentInternal != null)
				{
					backgroundImage = this.ParentInternal.BackgroundImage;
				}
				return backgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000D5BFC File Offset: 0x000D3DFC
		// (set) Token: 0x06002F6E RID: 12142 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				Image backgroundImage = this.BackgroundImage;
				if (backgroundImage != null && this.ParentInternal != null)
				{
					ImageLayout backgroundImageLayout = base.BackgroundImageLayout;
					if (backgroundImageLayout != this.ParentInternal.BackgroundImageLayout)
					{
						return this.ParentInternal.BackgroundImageLayout;
					}
				}
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000D5C44 File Offset: 0x000D3E44
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "MDICLIENT";
				createParams.Style |= 3145728;
				createParams.ExStyle |= 512;
				createParams.Param = new NativeMethods.CLIENTCREATESTRUCT(IntPtr.Zero, 1);
				ISite site = (this.ParentInternal == null) ? null : this.ParentInternal.Site;
				if (site != null && site.DesignMode)
				{
					createParams.Style |= 134217728;
					base.SetState(4, false);
				}
				if (this.RightToLeft == RightToLeft.Yes && this.ParentInternal != null && this.ParentInternal.IsMirrored)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000D5D18 File Offset: 0x000D3F18
		public Form[] MdiChildren
		{
			get
			{
				Form[] array = new Form[this.children.Count];
				this.children.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000D5D44 File Offset: 0x000D3F44
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new MdiClient.ControlCollection(this);
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000D5D4C File Offset: 0x000D3F4C
		public void LayoutMdi(MdiLayout value)
		{
			if (base.Handle == IntPtr.Zero)
			{
				return;
			}
			switch (value)
			{
			case MdiLayout.Cascade:
				base.SendMessage(551, 0, 0);
				return;
			case MdiLayout.TileHorizontal:
				base.SendMessage(550, 1, 0);
				return;
			case MdiLayout.TileVertical:
				base.SendMessage(550, 0, 0);
				return;
			case MdiLayout.ArrangeIcons:
				base.SendMessage(552, 0, 0);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000D5DC0 File Offset: 0x000D3FC0
		protected override void OnResize(EventArgs e)
		{
			ISite site = (this.ParentInternal == null) ? null : this.ParentInternal.Site;
			if (site != null && site.DesignMode && base.Handle != IntPtr.Zero)
			{
				this.SetWindowRgn();
			}
			base.OnResize(e);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000D5E10 File Offset: 0x000D4010
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.SuspendLayout();
			try
			{
				Rectangle bounds = base.Bounds;
				int num = (int)Math.Round((double)((float)bounds.X * dx));
				int num2 = (int)Math.Round((double)((float)bounds.Y * dy));
				int width = (int)Math.Round((double)((float)(bounds.X + bounds.Width) * dx - (float)num));
				int height = (int)Math.Round((double)((float)(bounds.Y + bounds.Height) * dy - (float)num2));
				base.SetBounds(num, num2, width, height, BoundsSpecified.All);
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000D5EB0 File Offset: 0x000D40B0
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			specified &= ~(BoundsSpecified.X | BoundsSpecified.Y);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000D5EC0 File Offset: 0x000D40C0
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			ISite site = (this.ParentInternal == null) ? null : this.ParentInternal.Site;
			if (base.IsHandleCreated && (site == null || !site.DesignMode))
			{
				Rectangle bounds = base.Bounds;
				base.SetBoundsCore(x, y, width, height, specified);
				Rectangle bounds2 = base.Bounds;
				int num = bounds.Height - bounds2.Height;
				if (num != 0)
				{
					NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
					windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
					for (int i = 0; i < base.Controls.Count; i++)
					{
						Control control = base.Controls[i];
						if (control != null && control is Form)
						{
							Form form = (Form)control;
							if (form.CanRecreateHandle() && form.WindowState == FormWindowState.Minimized)
							{
								UnsafeNativeMethods.GetWindowPlacement(new HandleRef(form, form.Handle), ref windowplacement);
								windowplacement.ptMinPosition_y -= num;
								if (windowplacement.ptMinPosition_y == -1)
								{
									if (num < 0)
									{
										windowplacement.ptMinPosition_y = 0;
									}
									else
									{
										windowplacement.ptMinPosition_y = -2;
									}
								}
								windowplacement.flags = 1;
								UnsafeNativeMethods.SetWindowPlacement(new HandleRef(form, form.Handle), ref windowplacement);
								windowplacement.flags = 0;
							}
						}
					}
					return;
				}
			}
			else
			{
				base.SetBoundsCore(x, y, width, height, specified);
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000D6024 File Offset: 0x000D4224
		private void SetWindowRgn()
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			CreateParams createParams = this.CreateParams;
			base.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
			Rectangle bounds = base.Bounds;
			intPtr = SafeNativeMethods.CreateRectRgn(0, 0, bounds.Width, bounds.Height);
			try
			{
				intPtr2 = SafeNativeMethods.CreateRectRgn(-rect.left, -rect.top, bounds.Width - rect.right, bounds.Height - rect.bottom);
				try
				{
					if (intPtr == IntPtr.Zero || intPtr2 == IntPtr.Zero)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					if (SafeNativeMethods.CombineRgn(new HandleRef(null, intPtr), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), 4) == 0)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, base.Handle), new HandleRef(null, intPtr), true) == 0)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					intPtr = IntPtr.Zero;
				}
				finally
				{
					if (intPtr2 != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr2));
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000D618C File Offset: 0x000D438C
		internal override bool ShouldSerializeBackColor()
		{
			return this.BackColor != SystemColors.AppWorkspace;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x00011A20 File Offset: 0x0000FC20
		private bool ShouldSerializeLocation()
		{
			return false;
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool ShouldSerializeSize()
		{
			return false;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000D61A0 File Offset: 0x000D43A0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 1)
			{
				if (msg == 7)
				{
					base.InvokeGotFocus(this.ParentInternal, EventArgs.Empty);
					Form form = null;
					if (this.ParentInternal is Form)
					{
						form = ((Form)this.ParentInternal).ActiveMdiChildInternal;
					}
					if (form == null && this.MdiChildren.Length != 0 && this.MdiChildren[0].IsMdiChildFocusable)
					{
						form = this.MdiChildren[0];
					}
					if (form != null && form.Visible)
					{
						form.Active = true;
					}
					base.WmImeSetFocus();
					this.DefWndProc(ref m);
					base.InvokeGotFocus(this, EventArgs.Empty);
					return;
				}
				if (msg == 8)
				{
					base.InvokeLostFocus(this.ParentInternal, EventArgs.Empty);
				}
			}
			else if (this.ParentInternal != null && this.ParentInternal.Site != null && this.ParentInternal.Site.DesignMode && base.Handle != IntPtr.Zero)
			{
				this.SetWindowRgn();
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000D62B1 File Offset: 0x000D44B1
		internal override void OnInvokedSetScrollPosition(object sender, EventArgs e)
		{
			Application.Idle += this.OnIdle;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000D62C4 File Offset: 0x000D44C4
		private void OnIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.OnIdle;
			base.OnInvokedSetScrollPosition(sender, e);
		}

		// Token: 0x04001399 RID: 5017
		private ArrayList children = new ArrayList();

		// Token: 0x020006D2 RID: 1746
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			// Token: 0x06006AD8 RID: 27352 RVA: 0x0018C093 File Offset: 0x0018A293
			public ControlCollection(MdiClient owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006AD9 RID: 27353 RVA: 0x0018C0A4 File Offset: 0x0018A2A4
			public override void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (!(value is Form) || !((Form)value).IsMdiChild)
				{
					throw new ArgumentException(SR.GetString("MDIChildAddToNonMDIParent"), "value");
				}
				if (this.owner.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"), "value");
				}
				this.owner.children.Add((Form)value);
				base.Add(value);
			}

			// Token: 0x06006ADA RID: 27354 RVA: 0x0018C125 File Offset: 0x0018A325
			public override void Remove(Control value)
			{
				this.owner.children.Remove(value);
				base.Remove(value);
			}

			// Token: 0x04003B4F RID: 15183
			private MdiClient owner;
		}
	}
}
