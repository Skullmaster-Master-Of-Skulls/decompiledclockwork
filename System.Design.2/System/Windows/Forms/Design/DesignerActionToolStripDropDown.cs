using System;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D2 RID: 722
	internal class DesignerActionToolStripDropDown : ToolStripDropDown
	{
		// Token: 0x06001CAC RID: 7340 RVA: 0x000AD44C File Offset: 0x000AB64C
		public DesignerActionToolStripDropDown(DesignerActionUI designerActionUI, IWin32Window mainParentWindow)
		{
			this._mainParentWindow = mainParentWindow;
			this._designerActionUI = designerActionUI;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x000AD462 File Offset: 0x000AB662
		public DesignerActionPanel CurrentPanel
		{
			get
			{
				if (this._panel != null)
				{
					return this._panel.Control as DesignerActionPanel;
				}
				return null;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool TopMost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000AD480 File Offset: 0x000AB680
		public void UpdateContainerSize()
		{
			if (this.CurrentPanel != null)
			{
				Size preferredSize = this.CurrentPanel.GetPreferredSize(new Size(150, int.MaxValue));
				if (this.CurrentPanel.Size == preferredSize)
				{
					this.CurrentPanel.PerformLayout();
				}
				else
				{
					this.CurrentPanel.Size = preferredSize;
				}
				base.ClientSize = preferredSize;
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000AD4E4 File Offset: 0x000AB6E4
		public void CheckFocusIsRight()
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			if (focus == base.Handle)
			{
				this._panel.Focus();
			}
			focus = UnsafeNativeMethods.GetFocus();
			if (this.CurrentPanel != null && this.CurrentPanel.Handle == focus)
			{
				this.CurrentPanel.SelectNextControl(null, true, true, true, true);
			}
			focus = UnsafeNativeMethods.GetFocus();
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000AD548 File Offset: 0x000AB748
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			this.UpdateContainerSize();
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000AD558 File Offset: 0x000AB758
		protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
		{
			if (e.CloseReason == ToolStripDropDownCloseReason.AppFocusChange && this._cancelClose)
			{
				this._cancelClose = false;
				e.Cancel = true;
			}
			else if (e.CloseReason == ToolStripDropDownCloseReason.AppFocusChange || e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
			{
				IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
				if (base.Handle == activeWindow && e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
				{
					e.Cancel = false;
				}
				else if (DesignerActionToolStripDropDown.WindowOwnsWindow(base.Handle, activeWindow))
				{
					e.Cancel = true;
				}
				else if (this._mainParentWindow != null && !DesignerActionToolStripDropDown.WindowOwnsWindow(this._mainParentWindow.Handle, activeWindow))
				{
					if (this.IsWindowEnabled(this._mainParentWindow.Handle))
					{
						e.Cancel = false;
					}
					else
					{
						e.Cancel = true;
					}
					base.OnClosing(e);
					return;
				}
				IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, activeWindow), -8);
				if (!this.IsWindowEnabled(windowLong))
				{
					e.Cancel = true;
				}
			}
			base.OnClosing(e);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000AD648 File Offset: 0x000AB848
		public void SetDesignerActionPanel(DesignerActionPanel panel, Glyph relatedGlyph)
		{
			if (this._panel != null && panel == (DesignerActionPanel)this._panel.Control)
			{
				return;
			}
			this.relatedGlyph = relatedGlyph;
			panel.SizeChanged += this.PanelResized;
			if (this._panel != null)
			{
				this.Items.Remove(this._panel);
				this._panel.Dispose();
				this._panel = null;
			}
			this._panel = new ToolStripControlHost(panel);
			this._panel.Margin = Padding.Empty;
			this._panel.Size = panel.Size;
			base.SuspendLayout();
			base.Size = panel.Size;
			this.Items.Add(this._panel);
			base.ResumeLayout();
			if (base.Visible)
			{
				this.CheckFocusIsRight();
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000AD71C File Offset: 0x000AB91C
		private void PanelResized(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (base.Size.Width != control.Size.Width || base.Size.Height != control.Size.Height)
			{
				base.SuspendLayout();
				base.Size = control.Size;
				if (this._panel != null)
				{
					this._panel.Size = control.Size;
				}
				this._designerActionUI.UpdateDAPLocation(null, this.relatedGlyph as DesignerActionGlyph);
				base.ResumeLayout();
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000AD7B5 File Offset: 0x000AB9B5
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
			if (visible)
			{
				this.CheckFocusIsRight();
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000AD7C8 File Offset: 0x000AB9C8
		private static bool WindowOwnsWindow(IntPtr hWndOwner, IntPtr hWndDescendant)
		{
			if (hWndDescendant == hWndOwner)
			{
				return true;
			}
			while (hWndDescendant != IntPtr.Zero)
			{
				hWndDescendant = UnsafeNativeMethods.GetWindowLong(new HandleRef(null, hWndDescendant), -8);
				if (hWndDescendant == IntPtr.Zero)
				{
					return false;
				}
				if (hWndDescendant == hWndOwner)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000AD818 File Offset: 0x000ABA18
		internal static string GetControlInformation(IntPtr hwnd)
		{
			if (hwnd == IntPtr.Zero)
			{
				return "Handle is IntPtr.Zero";
			}
			return string.Empty;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000AD834 File Offset: 0x000ABA34
		private bool IsWindowEnabled(IntPtr handle)
		{
			int num = (int)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -16);
			return (num & 134217728) == 0;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000AD860 File Offset: 0x000ABA60
		private void WmActivate(ref Message m)
		{
			if ((int)((long)m.WParam) == 0)
			{
				IntPtr lparam = m.LParam;
				if (DesignerActionToolStripDropDown.WindowOwnsWindow(base.Handle, lparam))
				{
					this._cancelClose = true;
				}
				else
				{
					this._cancelClose = false;
				}
			}
			else
			{
				this._cancelClose = false;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000AD8B0 File Offset: 0x000ABAB0
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 6)
			{
				this.WmActivate(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000AD8D8 File Offset: 0x000ABAD8
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Return)
			{
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				Control control = Control.FromChildHandle(focus);
				IButtonControl buttonControl = control as IButtonControl;
				if (buttonControl != null && buttonControl is Control)
				{
					buttonControl.PerformClick();
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x0400170C RID: 5900
		private IWin32Window _mainParentWindow;

		// Token: 0x0400170D RID: 5901
		private ToolStripControlHost _panel;

		// Token: 0x0400170E RID: 5902
		private DesignerActionUI _designerActionUI;

		// Token: 0x0400170F RID: 5903
		private bool _cancelClose;

		// Token: 0x04001710 RID: 5904
		private Glyph relatedGlyph;
	}
}
