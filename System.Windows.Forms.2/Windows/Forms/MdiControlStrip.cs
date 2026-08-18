using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x020002EC RID: 748
	internal class MdiControlStrip : MenuStrip
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x000D62E0 File Offset: 0x000D44E0
		public MdiControlStrip(IWin32Window target)
		{
			IntPtr systemMenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(target)), false);
			this.target = target;
			this.minimize = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61472, target);
			this.close = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61536, target);
			this.restore = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61728, target);
			this.system = new MdiControlStrip.SystemMenuItem();
			Control control = target as Control;
			if (control != null)
			{
				control.HandleCreated += this.OnTargetWindowHandleRecreated;
				control.Disposed += this.OnTargetWindowDisposed;
			}
			this.Items.AddRange(new ToolStripItem[]
			{
				this.minimize,
				this.restore,
				this.close,
				this.system
			});
			base.SuspendLayout();
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
				toolStripItem.MergeIndex = 0;
				toolStripItem.MergeAction = MergeAction.Insert;
				toolStripItem.Overflow = ToolStripItemOverflow.Never;
				toolStripItem.Alignment = ToolStripItemAlignment.Right;
				toolStripItem.Padding = Padding.Empty;
				toolStripItem.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}
			this.system.Image = this.GetTargetWindowIcon();
			this.system.Alignment = ToolStripItemAlignment.Left;
			this.system.DropDownOpening += this.OnSystemMenuDropDownOpening;
			this.system.ImageScaling = ToolStripItemImageScaling.None;
			this.system.DoubleClickEnabled = true;
			this.system.DoubleClick += this.OnSystemMenuDoubleClick;
			this.system.Padding = Padding.Empty;
			this.system.ShortcutKeys = (Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Alt);
			base.ResumeLayout(false);
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000D64C0 File Offset: 0x000D46C0
		public ToolStripMenuItem Close
		{
			get
			{
				return this.close;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000D64C8 File Offset: 0x000D46C8
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x000D64D0 File Offset: 0x000D46D0
		internal MenuStrip MergedMenu
		{
			get
			{
				return this.mergedMenu;
			}
			set
			{
				this.mergedMenu = value;
			}
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000D64DC File Offset: 0x000D46DC
		private Image GetTargetWindowIcon()
		{
			Image result = null;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, Control.GetSafeHandle(this.target)), 127, 0, 0);
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				Icon original = (intPtr != IntPtr.Zero) ? Icon.FromHandle(intPtr) : Form.DefaultIcon;
				Icon icon = new Icon(original, SystemInformation.SmallIconSize);
				result = icon.ToBitmap();
				icon.Dispose();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000D6560 File Offset: 0x000D4760
		protected internal override void OnItemAdded(ToolStripItemEventArgs e)
		{
			base.OnItemAdded(e);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000D6569 File Offset: 0x000D4769
		private void OnTargetWindowDisposed(object sender, EventArgs e)
		{
			this.UnhookTarget();
			this.target = null;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000D6578 File Offset: 0x000D4778
		private void OnTargetWindowHandleRecreated(object sender, EventArgs e)
		{
			this.system.SetNativeTargetWindow(this.target);
			this.minimize.SetNativeTargetWindow(this.target);
			this.close.SetNativeTargetWindow(this.target);
			this.restore.SetNativeTargetWindow(this.target);
			IntPtr systemMenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(this.target)), false);
			this.system.SetNativeTargetMenu(systemMenu);
			this.minimize.SetNativeTargetMenu(systemMenu);
			this.close.SetNativeTargetMenu(systemMenu);
			this.restore.SetNativeTargetMenu(systemMenu);
			if (this.system.HasDropDownItems)
			{
				this.system.DropDown.Items.Clear();
				this.system.DropDown.Dispose();
			}
			this.system.Image = this.GetTargetWindowIcon();
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000D6654 File Offset: 0x000D4854
		private void OnSystemMenuDropDownOpening(object sender, EventArgs e)
		{
			if (!this.system.HasDropDownItems && this.target != null)
			{
				this.system.DropDown = ToolStripDropDownMenu.FromHMenu(UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(this.target)), false), this.target);
				return;
			}
			if (this.MergedMenu == null)
			{
				this.system.DropDown.Dispose();
			}
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000D66BC File Offset: 0x000D48BC
		private void OnSystemMenuDoubleClick(object sender, EventArgs e)
		{
			this.Close.PerformClick();
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000D66C9 File Offset: 0x000D48C9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.UnhookTarget();
				this.target = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000D66E4 File Offset: 0x000D48E4
		private void UnhookTarget()
		{
			if (this.target != null)
			{
				Control control = this.target as Control;
				if (control != null)
				{
					control.HandleCreated -= this.OnTargetWindowHandleRecreated;
					control.Disposed -= this.OnTargetWindowDisposed;
				}
				this.target = null;
			}
		}

		// Token: 0x0400139A RID: 5018
		private ToolStripMenuItem system;

		// Token: 0x0400139B RID: 5019
		private ToolStripMenuItem close;

		// Token: 0x0400139C RID: 5020
		private ToolStripMenuItem minimize;

		// Token: 0x0400139D RID: 5021
		private ToolStripMenuItem restore;

		// Token: 0x0400139E RID: 5022
		private MenuStrip mergedMenu;

		// Token: 0x0400139F RID: 5023
		private IWin32Window target;

		// Token: 0x020006D3 RID: 1747
		internal class ControlBoxMenuItem : ToolStripMenuItem
		{
			// Token: 0x06006ADB RID: 27355 RVA: 0x0018C13F File Offset: 0x0018A33F
			internal ControlBoxMenuItem(IntPtr hMenu, int nativeMenuCommandId, IWin32Window targetWindow) : base(hMenu, nativeMenuCommandId, targetWindow)
			{
			}

			// Token: 0x1700172C RID: 5932
			// (get) Token: 0x06006ADC RID: 27356 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override bool CanKeyboardSelect
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020006D4 RID: 1748
		internal class SystemMenuItem : ToolStripMenuItem
		{
			// Token: 0x06006ADD RID: 27357 RVA: 0x0018C14A File Offset: 0x0018A34A
			public SystemMenuItem()
			{
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibleName = SR.GetString("MDIChildSystemMenuItemAccessibleName");
				}
			}

			// Token: 0x06006ADE RID: 27358 RVA: 0x0018C169 File Offset: 0x0018A369
			protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
			{
				if (base.Visible && base.ShortcutKeys == keyData)
				{
					base.ShowDropDown();
					base.DropDown.SelectNextToolStripItem(null, true);
					return true;
				}
				return base.ProcessCmdKey(ref m, keyData);
			}

			// Token: 0x06006ADF RID: 27359 RVA: 0x0018C19A File Offset: 0x0018A39A
			protected override void OnOwnerChanged(EventArgs e)
			{
				if (this.HasDropDownItems && base.DropDown.Visible)
				{
					base.HideDropDown();
				}
				base.OnOwnerChanged(e);
			}
		}
	}
}
