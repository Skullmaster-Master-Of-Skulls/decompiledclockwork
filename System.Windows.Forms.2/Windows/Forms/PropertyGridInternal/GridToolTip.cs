using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050A RID: 1290
	internal class GridToolTip : Control
	{
		// Token: 0x060054A4 RID: 21668 RVA: 0x00162B38 File Offset: 0x00160D38
		internal GridToolTip(Control[] controls)
		{
			this.controls = controls;
			base.SetStyle(ControlStyles.UserPaint, false);
			this.Font = controls[0].Font;
			this.toolInfos = new NativeMethods.TOOLINFO_T[controls.Length];
			for (int i = 0; i < controls.Length; i++)
			{
				controls[i].HandleCreated += this.OnControlCreateHandle;
				controls[i].HandleDestroyed += this.OnControlDestroyHandle;
				if (controls[i].IsHandleCreated)
				{
					this.SetupToolTip(controls[i]);
				}
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x00162BD5 File Offset: 0x00160DD5
		// (set) Token: 0x060054A6 RID: 21670 RVA: 0x00162BE0 File Offset: 0x00160DE0
		public string ToolTip
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (base.IsHandleCreated || !string.IsNullOrEmpty(value))
				{
					this.Reset();
				}
				if (value != null && value.Length > this.maximumToolTipLength)
				{
					value = value.Substring(0, this.maximumToolTipLength) + "...";
				}
				this.toolTipText = value;
				if (base.IsHandleCreated)
				{
					bool visible = base.Visible;
					if (visible)
					{
						base.Visible = false;
					}
					if (value == null || value.Length == 0)
					{
						this.dontShow = true;
						value = "";
					}
					else
					{
						this.dontShow = false;
					}
					for (int i = 0; i < this.controls.Length; i++)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_UPDATETIPTEXT, 0, this.GetTOOLINFO(this.controls[i]));
					}
					if (visible && !this.dontShow)
					{
						base.Visible = true;
					}
				}
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00162CBC File Offset: 0x00160EBC
		protected override CreateParams CreateParams
		{
			get
			{
				SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
				{
					dwICC = 8
				});
				CreateParams createParams = new CreateParams();
				createParams.Parent = IntPtr.Zero;
				createParams.ClassName = "tooltips_class32";
				createParams.Style |= 3;
				createParams.ExStyle = 0;
				createParams.Caption = this.ToolTip;
				return createParams;
			}
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00162D1C File Offset: 0x00160F1C
		private NativeMethods.TOOLINFO_T GetTOOLINFO(Control c)
		{
			int num = Array.IndexOf<Control>(this.controls, c);
			if (this.toolInfos[num] == null)
			{
				this.toolInfos[num] = new NativeMethods.TOOLINFO_T();
				this.toolInfos[num].cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				this.toolInfos[num].uFlags |= 273;
			}
			this.toolInfos[num].lpszText = this.toolTipText;
			this.toolInfos[num].hwnd = c.Handle;
			this.toolInfos[num].uId = c.Handle;
			return this.toolInfos[num];
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00162DC3 File Offset: 0x00160FC3
		private void OnControlCreateHandle(object sender, EventArgs e)
		{
			this.SetupToolTip((Control)sender);
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x00162DD1 File Offset: 0x00160FD1
		private void OnControlDestroyHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetTOOLINFO((Control)sender));
			}
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x00162E00 File Offset: 0x00161000
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			for (int i = 0; i < this.controls.Length; i++)
			{
				if (this.controls[i].IsHandleCreated)
				{
					this.SetupToolTip(this.controls[i]);
				}
			}
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00162E44 File Offset: 0x00161044
		internal void PositionToolTip(Control parent, Rectangle itemRect)
		{
			if (this._positioned && DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				return;
			}
			base.Visible = false;
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(itemRect.X, itemRect.Y, itemRect.Width, itemRect.Height);
			base.SendMessage(1055, 1, ref rect);
			Point location = parent.PointToScreen(new Point(rect.left, rect.top));
			base.Location = location;
			int num = base.Location.X + base.Size.Width - SystemInformation.VirtualScreen.Width;
			if (num > 0)
			{
				location.X -= num;
				base.Location = location;
			}
			base.Visible = true;
			this._positioned = true;
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00162F10 File Offset: 0x00161110
		private void SetupToolTip(Control c)
		{
			if (base.IsHandleCreated)
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(c));
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
			}
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00162F8C File Offset: 0x0016118C
		public void Reset()
		{
			string toolTip = this.ToolTip;
			this.toolTipText = "";
			for (int i = 0; i < this.controls.Length; i++)
			{
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TTM_UPDATETIPTEXT, 0, this.GetTOOLINFO(this.controls[i]));
			}
			this.toolTipText = toolTip;
			base.SendMessage(1053, 0, 0);
			this._positioned = false;
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00163008 File Offset: 0x00161208
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 != 24)
			{
				if (msg2 == 132)
				{
					msg.Result = (IntPtr)(-1);
					return;
				}
			}
			else if ((int)((long)msg.WParam) != 0 && this.dontShow)
			{
				msg.WParam = IntPtr.Zero;
			}
			base.WndProc(ref msg);
		}

		// Token: 0x0400371D RID: 14109
		private Control[] controls;

		// Token: 0x0400371E RID: 14110
		private string toolTipText;

		// Token: 0x0400371F RID: 14111
		private NativeMethods.TOOLINFO_T[] toolInfos;

		// Token: 0x04003720 RID: 14112
		private bool dontShow;

		// Token: 0x04003721 RID: 14113
		private Point lastMouseMove = Point.Empty;

		// Token: 0x04003722 RID: 14114
		private int maximumToolTipLength = 1000;

		// Token: 0x04003723 RID: 14115
		private bool _positioned;
	}
}
