using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200018D RID: 397
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("GridEditName")]
	public class DataGridTextBox : TextBox
	{
		// Token: 0x06001842 RID: 6210 RVA: 0x00056FFE File Offset: 0x000551FE
		public DataGridTextBox()
		{
			base.TabStop = false;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00057014 File Offset: 0x00055214
		public void SetDataGrid(DataGrid parentGrid)
		{
			this.dataGrid = parentGrid;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00057020 File Offset: 0x00055220
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 770 || m.Msg == 768 || m.Msg == 771)
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00057073 File Offset: 0x00055273
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.dataGrid.TextBoxOnMouseWheel(e);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00057084 File Offset: 0x00055284
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.KeyChar == ' ' && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				return;
			}
			if (base.ReadOnly)
			{
				return;
			}
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control && (Control.ModifierKeys & Keys.Alt) == Keys.None)
			{
				return;
			}
			this.IsInEditOrNavigateMode = false;
			this.dataGrid.ColumnStartedEditing(base.Bounds);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000570F8 File Offset: 0x000552F8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessKeyMessage(ref Message m)
		{
			Keys keys = (Keys)((long)m.WParam);
			Keys modifierKeys = Control.ModifierKeys;
			if ((keys | modifierKeys) == Keys.Return || (keys | modifierKeys) == Keys.Escape || (keys | modifierKeys) == (Keys.LButton | Keys.MButton | Keys.Back | Keys.Control))
			{
				return m.Msg == 258 || this.ProcessKeyPreview(ref m);
			}
			if (m.Msg == 258)
			{
				return keys == Keys.LineFeed || this.ProcessKeyEventArgs(ref m);
			}
			if (m.Msg == 257)
			{
				return true;
			}
			Keys keys2 = keys & Keys.KeyCode;
			if (keys2 <= Keys.Add)
			{
				if (keys2 <= Keys.Delete)
				{
					if (keys2 != Keys.Tab)
					{
						switch (keys2)
						{
						case Keys.Space:
							if (this.IsInEditOrNavigateMode && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								return m.Msg == 258 || this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						case Keys.Prior:
						case Keys.Next:
							break;
						case Keys.End:
						case Keys.Home:
							if (this.SelectionLength == this.Text.Length)
							{
								return this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						case Keys.Left:
							if (base.SelectionStart + this.SelectionLength == 0 || (this.IsInEditOrNavigateMode && this.SelectionLength == this.Text.Length))
							{
								return this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						case Keys.Up:
							if (this.Text.IndexOf("\r\n") < 0 || base.SelectionStart + this.SelectionLength < this.Text.IndexOf("\r\n"))
							{
								return this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						case Keys.Right:
							if (base.SelectionStart + this.SelectionLength == this.Text.Length)
							{
								return this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						case Keys.Down:
						{
							int startIndex = base.SelectionStart + this.SelectionLength;
							if (this.Text.IndexOf("\r\n", startIndex) == -1)
							{
								return this.ProcessKeyPreview(ref m);
							}
							return this.ProcessKeyEventArgs(ref m);
						}
						case Keys.Select:
						case Keys.Print:
						case Keys.Execute:
						case Keys.Snapshot:
						case Keys.Insert:
							goto IL_317;
						case Keys.Delete:
							if (!this.IsInEditOrNavigateMode)
							{
								return this.ProcessKeyEventArgs(ref m);
							}
							if (this.ProcessKeyPreview(ref m))
							{
								return true;
							}
							this.IsInEditOrNavigateMode = false;
							this.dataGrid.ColumnStartedEditing(base.Bounds);
							return this.ProcessKeyEventArgs(ref m);
						default:
							goto IL_317;
						}
					}
					else
					{
						if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
						{
							return this.ProcessKeyPreview(ref m);
						}
						return this.ProcessKeyEventArgs(ref m);
					}
				}
				else if (keys2 != Keys.A)
				{
					if (keys2 != Keys.Add)
					{
						goto IL_317;
					}
				}
				else
				{
					if (this.IsInEditOrNavigateMode && (Control.ModifierKeys & Keys.Control) == Keys.Control)
					{
						return m.Msg == 258 || this.ProcessKeyPreview(ref m);
					}
					return this.ProcessKeyEventArgs(ref m);
				}
			}
			else if (keys2 <= Keys.F2)
			{
				if (keys2 != Keys.Subtract)
				{
					if (keys2 != Keys.F2)
					{
						goto IL_317;
					}
					this.IsInEditOrNavigateMode = false;
					base.SelectionStart = this.Text.Length;
					return true;
				}
			}
			else if (keys2 != Keys.Oemplus && keys2 != Keys.OemMinus)
			{
				goto IL_317;
			}
			if (this.IsInEditOrNavigateMode)
			{
				return this.ProcessKeyPreview(ref m);
			}
			return this.ProcessKeyEventArgs(ref m);
			IL_317:
			return this.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x00057423 File Offset: 0x00055623
		// (set) Token: 0x06001849 RID: 6217 RVA: 0x0005742B File Offset: 0x0005562B
		public bool IsInEditOrNavigateMode
		{
			get
			{
				return this.isInEditOrNavigateMode;
			}
			set
			{
				this.isInEditOrNavigateMode = value;
				if (value)
				{
					base.SelectAll();
				}
			}
		}

		// Token: 0x04000AD3 RID: 2771
		private bool isInEditOrNavigateMode = true;

		// Token: 0x04000AD4 RID: 2772
		private DataGrid dataGrid;
	}
}
