using System;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DD RID: 733
	internal partial class DropDownHolder : Form
	{
		// Token: 0x06001D59 RID: 7513 RVA: 0x000B18D0 File Offset: 0x000AFAD0
		public DropDownHolder(Control parent)
		{
			this.parent = parent;
			base.ShowInTaskbar = false;
			base.ControlBox = false;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			this.Text = "";
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.StartPosition = FormStartPosition.Manual;
			this.Font = parent.Font;
			base.Visible = false;
			this.BackColor = SystemColors.Window;
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x000B1940 File Offset: 0x000AFB40
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 128;
				createParams.Style |= -2139095040;
				if (this.parent != null)
				{
					createParams.Parent = this.parent.Handle;
				}
				return createParams;
			}
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000B1992 File Offset: 0x000AFB92
		public void DoModalLoop()
		{
			while (base.Visible)
			{
				Application.DoEvents();
				UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 250, 255, 4);
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x000B19BA File Offset: 0x000AFBBA
		public virtual Control Component
		{
			get
			{
				return this.currentControl;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000B19C2 File Offset: 0x000AFBC2
		public virtual bool GetUsed()
		{
			return this.currentControl != null;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000B19CD File Offset: 0x000AFBCD
		protected override void OnMouseDown(MouseEventArgs me)
		{
			if (me.Button == MouseButtons.Left)
			{
				base.Visible = false;
			}
			base.OnMouseDown(me);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000B19EC File Offset: 0x000AFBEC
		private bool OwnsWindow(IntPtr hWnd)
		{
			while (hWnd != IntPtr.Zero)
			{
				hWnd = UnsafeNativeMethods.GetWindowLong(new HandleRef(null, hWnd), -8);
				if (hWnd == IntPtr.Zero)
				{
					return false;
				}
				if (hWnd == base.Handle)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000B1A38 File Offset: 0x000AFC38
		public virtual void FocusComponent()
		{
			if (this.currentControl != null && base.Visible)
			{
				this.currentControl.Focus();
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x000B1A58 File Offset: 0x000AFC58
		private void OnCurrentControlResize(object o, EventArgs e)
		{
			if (this.currentControl != null)
			{
				int width = base.Width;
				this.UpdateSize();
				this.currentControl.Location = new Point(1, 1);
				base.Left -= base.Width - width;
			}
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000B1AA4 File Offset: 0x000AFCA4
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					return true;
				}
				if (keys == Keys.Escape)
				{
					base.Visible = false;
					return true;
				}
				if (keys == Keys.F4)
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000B1AE8 File Offset: 0x000AFCE8
		public virtual void SetComponent(Control ctl)
		{
			if (this.currentControl != null)
			{
				base.Controls.Remove(this.currentControl);
				this.currentControl = null;
			}
			if (ctl != null)
			{
				base.Controls.Add(ctl);
				ctl.Location = new Point(1, 1);
				ctl.Visible = true;
				this.currentControl = ctl;
				this.UpdateSize();
				this.currentControl.Resize += this.OnCurrentControlResize;
			}
			base.Enabled = (this.currentControl != null);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000B1B6B File Offset: 0x000AFD6B
		private void UpdateSize()
		{
			base.Size = new Size(2 + this.currentControl.Width + 2, 2 + this.currentControl.Height + 2);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000B1B98 File Offset: 0x000AFD98
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 6 && base.Visible && NativeMethods.Util.LOWORD((int)((long)m.WParam)) == 0 && !this.OwnsWindow(m.LParam))
			{
				base.Visible = false;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0400176B RID: 5995
		private Control parent;

		// Token: 0x0400176C RID: 5996
		private Control currentControl;

		// Token: 0x0400176D RID: 5997
		private const int BORDER = 1;
	}
}
