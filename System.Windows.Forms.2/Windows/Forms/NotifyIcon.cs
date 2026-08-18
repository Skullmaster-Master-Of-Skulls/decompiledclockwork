using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200030D RID: 781
	[DefaultProperty("Text")]
	[DefaultEvent("MouseDoubleClick")]
	[Designer("System.Windows.Forms.Design.NotifyIconDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionNotifyIcon")]
	public sealed class NotifyIcon : Component
	{
		// Token: 0x0600317C RID: 12668 RVA: 0x000DF8E4 File Offset: 0x000DDAE4
		public NotifyIcon()
		{
			this.id = ++NotifyIcon.nextId;
			this.window = new NotifyIcon.NotifyIconNativeWindow(this);
			this.UpdateIcon(this.visible);
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000DF94E File Offset: 0x000DDB4E
		public NotifyIcon(IContainer container) : this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000DF96B File Offset: 0x000DDB6B
		// (set) Token: 0x0600317F RID: 12671 RVA: 0x000DF973 File Offset: 0x000DDB73
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconBalloonTipTextDescr")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string BalloonTipText
		{
			get
			{
				return this.balloonTipText;
			}
			set
			{
				if (value != this.balloonTipText)
				{
					this.balloonTipText = value;
				}
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000DF98A File Offset: 0x000DDB8A
		// (set) Token: 0x06003181 RID: 12673 RVA: 0x000DF992 File Offset: 0x000DDB92
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolTipIcon.None)]
		[SRDescription("NotifyIconBalloonTipIconDescr")]
		public ToolTipIcon BalloonTipIcon
		{
			get
			{
				return this.balloonTipIcon;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolTipIcon));
				}
				if (value != this.balloonTipIcon)
				{
					this.balloonTipIcon = value;
				}
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000DF9CA File Offset: 0x000DDBCA
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x000DF9D2 File Offset: 0x000DDBD2
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconBalloonTipTitleDescr")]
		public string BalloonTipTitle
		{
			get
			{
				return this.balloonTipTitle;
			}
			set
			{
				if (value != this.balloonTipTitle)
				{
					this.balloonTipTitle = value;
				}
			}
		}

		// Token: 0x1400023F RID: 575
		// (add) Token: 0x06003184 RID: 12676 RVA: 0x000DF9E9 File Offset: 0x000DDBE9
		// (remove) Token: 0x06003185 RID: 12677 RVA: 0x000DF9FC File Offset: 0x000DDBFC
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipClickedDescr")]
		public event EventHandler BalloonTipClicked
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPCLICKED, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPCLICKED, value);
			}
		}

		// Token: 0x14000240 RID: 576
		// (add) Token: 0x06003186 RID: 12678 RVA: 0x000DFA0F File Offset: 0x000DDC0F
		// (remove) Token: 0x06003187 RID: 12679 RVA: 0x000DFA22 File Offset: 0x000DDC22
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipClosedDescr")]
		public event EventHandler BalloonTipClosed
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPCLOSED, value);
			}
		}

		// Token: 0x14000241 RID: 577
		// (add) Token: 0x06003188 RID: 12680 RVA: 0x000DFA35 File Offset: 0x000DDC35
		// (remove) Token: 0x06003189 RID: 12681 RVA: 0x000DFA48 File Offset: 0x000DDC48
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconOnBalloonTipShownDescr")]
		public event EventHandler BalloonTipShown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_BALLOONTIPSHOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_BALLOONTIPSHOWN, value);
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000DFA5B File Offset: 0x000DDC5B
		// (set) Token: 0x0600318B RID: 12683 RVA: 0x000DFA63 File Offset: 0x000DDC63
		[Browsable(false)]
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("NotifyIconMenuDescr")]
		public ContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
			set
			{
				this.contextMenu = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000DFA6C File Offset: 0x000DDC6C
		// (set) Token: 0x0600318D RID: 12685 RVA: 0x000DFA74 File Offset: 0x000DDC74
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("NotifyIconMenuDescr")]
		public ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000DFA7D File Offset: 0x000DDC7D
		// (set) Token: 0x0600318F RID: 12687 RVA: 0x000DFA85 File Offset: 0x000DDC85
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(null)]
		[SRDescription("NotifyIconIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.icon != value)
				{
					this.icon = value;
					this.UpdateIcon(this.visible);
				}
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003190 RID: 12688 RVA: 0x000DFAA3 File Offset: 0x000DDCA3
		// (set) Token: 0x06003191 RID: 12689 RVA: 0x000DFAAC File Offset: 0x000DDCAC
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[SRDescription("NotifyIconTextDescr")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value != null && !value.Equals(this.text))
				{
					if (value != null && value.Length > 63)
					{
						throw new ArgumentOutOfRangeException("Text", value, SR.GetString("TrayIcon_TextTooLong"));
					}
					this.text = value;
					if (this.added)
					{
						this.UpdateIcon(true);
					}
				}
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000DFB0D File Offset: 0x000DDD0D
		// (set) Token: 0x06003193 RID: 12691 RVA: 0x000DFB15 File Offset: 0x000DDD15
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("NotifyIconVisDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.UpdateIcon(value);
					this.visible = value;
				}
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000DFB2E File Offset: 0x000DDD2E
		// (set) Token: 0x06003195 RID: 12693 RVA: 0x000DFB36 File Offset: 0x000DDD36
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x14000242 RID: 578
		// (add) Token: 0x06003196 RID: 12694 RVA: 0x000DFB3F File Offset: 0x000DDD3F
		// (remove) Token: 0x06003197 RID: 12695 RVA: 0x000DFB52 File Offset: 0x000DDD52
		[SRCategory("CatAction")]
		[SRDescription("ControlOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_CLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_CLICK, value);
			}
		}

		// Token: 0x14000243 RID: 579
		// (add) Token: 0x06003198 RID: 12696 RVA: 0x000DFB65 File Offset: 0x000DDD65
		// (remove) Token: 0x06003199 RID: 12697 RVA: 0x000DFB78 File Offset: 0x000DDD78
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_DOUBLECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_DOUBLECLICK, value);
			}
		}

		// Token: 0x14000244 RID: 580
		// (add) Token: 0x0600319A RID: 12698 RVA: 0x000DFB8B File Offset: 0x000DDD8B
		// (remove) Token: 0x0600319B RID: 12699 RVA: 0x000DFB9E File Offset: 0x000DDD9E
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconMouseClickDescr")]
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSECLICK, value);
			}
		}

		// Token: 0x14000245 RID: 581
		// (add) Token: 0x0600319C RID: 12700 RVA: 0x000DFBB1 File Offset: 0x000DDDB1
		// (remove) Token: 0x0600319D RID: 12701 RVA: 0x000DFBC4 File Offset: 0x000DDDC4
		[SRCategory("CatAction")]
		[SRDescription("NotifyIconMouseDoubleClickDescr")]
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEDOUBLECLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEDOUBLECLICK, value);
			}
		}

		// Token: 0x14000246 RID: 582
		// (add) Token: 0x0600319E RID: 12702 RVA: 0x000DFBD7 File Offset: 0x000DDDD7
		// (remove) Token: 0x0600319F RID: 12703 RVA: 0x000DFBEA File Offset: 0x000DDDEA
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEDOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEDOWN, value);
			}
		}

		// Token: 0x14000247 RID: 583
		// (add) Token: 0x060031A0 RID: 12704 RVA: 0x000DFBFD File Offset: 0x000DDDFD
		// (remove) Token: 0x060031A1 RID: 12705 RVA: 0x000DFC10 File Offset: 0x000DDE10
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEMOVE, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEMOVE, value);
			}
		}

		// Token: 0x14000248 RID: 584
		// (add) Token: 0x060031A2 RID: 12706 RVA: 0x000DFC23 File Offset: 0x000DDE23
		// (remove) Token: 0x060031A3 RID: 12707 RVA: 0x000DFC36 File Offset: 0x000DDE36
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(NotifyIcon.EVENT_MOUSEUP, value);
			}
			remove
			{
				base.Events.RemoveHandler(NotifyIcon.EVENT_MOUSEUP, value);
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000DFC4C File Offset: 0x000DDE4C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.window != null)
				{
					this.icon = null;
					this.Text = string.Empty;
					this.UpdateIcon(false);
					this.window.DestroyHandle();
					this.window = null;
					this.contextMenu = null;
					this.contextMenuStrip = null;
				}
			}
			else if (this.window != null && this.window.Handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this.window, this.window.Handle), 16, 0, 0);
				this.window.ReleaseHandle();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000DFCF4 File Offset: 0x000DDEF4
		private void OnBalloonTipClicked()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPCLICKED];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000DFD28 File Offset: 0x000DDF28
		private void OnBalloonTipClosed()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPCLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000DFD5C File Offset: 0x000DDF5C
		private void OnBalloonTipShown()
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_BALLOONTIPSHOWN];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000DFD90 File Offset: 0x000DDF90
		private void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_CLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000DFDC0 File Offset: 0x000DDFC0
		private void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[NotifyIcon.EVENT_DOUBLECLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000DFDF0 File Offset: 0x000DDFF0
		private void OnMouseClick(MouseEventArgs mea)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSECLICK];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, mea);
			}
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000DFE20 File Offset: 0x000DE020
		private void OnMouseDoubleClick(MouseEventArgs mea)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEDOUBLECLICK];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, mea);
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000DFE50 File Offset: 0x000DE050
		private void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEDOWN];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000DFE80 File Offset: 0x000DE080
		private void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEMOVE];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000DFEB0 File Offset: 0x000DE0B0
		private void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[NotifyIcon.EVENT_MOUSEUP];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000DFEDE File Offset: 0x000DE0DE
		public void ShowBalloonTip(int timeout)
		{
			this.ShowBalloonTip(timeout, this.balloonTipTitle, this.balloonTipText, this.balloonTipIcon);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000DFEFC File Offset: 0x000DE0FC
		public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.GetString("InvalidArgument", new object[]
				{
					"timeout",
					timeout.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (string.IsNullOrEmpty(tipText))
			{
				throw new ArgumentException(SR.GetString("NotifyIconEmptyOrNullTipText"));
			}
			if (!ClientUtils.IsEnumValid(tipIcon, (int)tipIcon, 0, 3))
			{
				throw new InvalidEnumArgumentException("tipIcon", (int)tipIcon, typeof(ToolTipIcon));
			}
			if (this.added)
			{
				if (base.DesignMode)
				{
					return;
				}
				IntSecurity.UnrestrictedWindows.Demand();
				NativeMethods.NOTIFYICONDATA notifyicondata = new NativeMethods.NOTIFYICONDATA();
				if (this.window.Handle == IntPtr.Zero)
				{
					this.window.CreateHandle(new CreateParams());
				}
				notifyicondata.hWnd = this.window.Handle;
				notifyicondata.uID = this.id;
				notifyicondata.uFlags = 16;
				notifyicondata.uTimeoutOrVersion = timeout;
				notifyicondata.szInfoTitle = tipTitle;
				notifyicondata.szInfo = tipText;
				switch (tipIcon)
				{
				case ToolTipIcon.None:
					notifyicondata.dwInfoFlags = 0;
					break;
				case ToolTipIcon.Info:
					notifyicondata.dwInfoFlags = 1;
					break;
				case ToolTipIcon.Warning:
					notifyicondata.dwInfoFlags = 2;
					break;
				case ToolTipIcon.Error:
					notifyicondata.dwInfoFlags = 3;
					break;
				}
				UnsafeNativeMethods.Shell_NotifyIcon(1, notifyicondata);
			}
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000E0048 File Offset: 0x000DE248
		private void ShowContextMenu()
		{
			if (this.contextMenu != null || this.contextMenuStrip != null)
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this.window, this.window.Handle));
				if (this.contextMenu != null)
				{
					this.contextMenu.OnPopup(EventArgs.Empty);
					SafeNativeMethods.TrackPopupMenuEx(new HandleRef(this.contextMenu, this.contextMenu.Handle), 72, point.x, point.y, new HandleRef(this.window, this.window.Handle), null);
					UnsafeNativeMethods.PostMessage(new HandleRef(this.window, this.window.Handle), 0, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				if (this.contextMenuStrip != null)
				{
					this.contextMenuStrip.ShowInTaskbar(point.x, point.y);
				}
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000E0134 File Offset: 0x000DE334
		private void UpdateIcon(bool showIconInTray)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (!base.DesignMode)
				{
					IntSecurity.UnrestrictedWindows.Demand();
					this.window.LockReference(showIconInTray);
					NativeMethods.NOTIFYICONDATA notifyicondata = new NativeMethods.NOTIFYICONDATA();
					notifyicondata.uCallbackMessage = 2048;
					notifyicondata.uFlags = 1;
					if (showIconInTray && this.window.Handle == IntPtr.Zero)
					{
						this.window.CreateHandle(new CreateParams());
					}
					notifyicondata.hWnd = this.window.Handle;
					notifyicondata.uID = this.id;
					notifyicondata.hIcon = IntPtr.Zero;
					notifyicondata.szTip = null;
					if (this.icon != null)
					{
						notifyicondata.uFlags |= 2;
						notifyicondata.hIcon = this.icon.Handle;
					}
					notifyicondata.uFlags |= 4;
					notifyicondata.szTip = this.text;
					if (showIconInTray && this.icon != null)
					{
						if (!this.added)
						{
							UnsafeNativeMethods.Shell_NotifyIcon(0, notifyicondata);
							this.added = true;
						}
						else
						{
							UnsafeNativeMethods.Shell_NotifyIcon(1, notifyicondata);
						}
					}
					else if (this.added)
					{
						UnsafeNativeMethods.Shell_NotifyIcon(2, notifyicondata);
						this.added = false;
					}
				}
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000E0294 File Offset: 0x000DE494
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			if (clicks == 2)
			{
				this.OnDoubleClick(new MouseEventArgs(button, 2, 0, 0, 0));
				this.OnMouseDoubleClick(new MouseEventArgs(button, 2, 0, 0, 0));
				this.doubleClick = true;
			}
			this.OnMouseDown(new MouseEventArgs(button, clicks, 0, 0, 0));
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000E02D1 File Offset: 0x000DE4D1
		private void WmMouseMove(ref Message m)
		{
			this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, 0, 0, 0));
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000E02E8 File Offset: 0x000DE4E8
		private void WmMouseUp(ref Message m, MouseButtons button)
		{
			this.OnMouseUp(new MouseEventArgs(button, 0, 0, 0, 0));
			if (!this.doubleClick)
			{
				this.OnClick(new MouseEventArgs(button, 0, 0, 0, 0));
				this.OnMouseClick(new MouseEventArgs(button, 0, 0, 0, 0));
			}
			this.doubleClick = false;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000E0334 File Offset: 0x000DE534
		private void WmTaskbarCreated(ref Message m)
		{
			this.added = false;
			this.UpdateIcon(this.visible);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000E034C File Offset: 0x000DE54C
		private void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 <= 44)
			{
				if (msg2 == 2)
				{
					this.UpdateIcon(false);
					return;
				}
				if (msg2 != 43)
				{
					if (msg2 == 44)
					{
						if (msg.WParam == IntPtr.Zero)
						{
							this.WmMeasureMenuItem(ref msg);
							return;
						}
						return;
					}
				}
				else
				{
					if (msg.WParam == IntPtr.Zero)
					{
						this.WmDrawItemMenuItem(ref msg);
						return;
					}
					return;
				}
			}
			else if (msg2 != 273)
			{
				if (msg2 == 279)
				{
					this.WmInitMenuPopup(ref msg);
					return;
				}
				if (msg2 == 2048)
				{
					int num = (int)msg.LParam;
					switch (num)
					{
					case 512:
						this.WmMouseMove(ref msg);
						return;
					case 513:
						this.WmMouseDown(ref msg, MouseButtons.Left, 1);
						return;
					case 514:
						this.WmMouseUp(ref msg, MouseButtons.Left);
						return;
					case 515:
						this.WmMouseDown(ref msg, MouseButtons.Left, 2);
						return;
					case 516:
						this.WmMouseDown(ref msg, MouseButtons.Right, 1);
						return;
					case 517:
						if (this.contextMenu != null || this.contextMenuStrip != null)
						{
							this.ShowContextMenu();
						}
						this.WmMouseUp(ref msg, MouseButtons.Right);
						return;
					case 518:
						this.WmMouseDown(ref msg, MouseButtons.Right, 2);
						return;
					case 519:
						this.WmMouseDown(ref msg, MouseButtons.Middle, 1);
						return;
					case 520:
						this.WmMouseUp(ref msg, MouseButtons.Middle);
						return;
					case 521:
						this.WmMouseDown(ref msg, MouseButtons.Middle, 2);
						return;
					default:
						switch (num)
						{
						case 1026:
							this.OnBalloonTipShown();
							return;
						case 1027:
							this.OnBalloonTipClosed();
							return;
						case 1028:
							this.OnBalloonTipClosed();
							return;
						case 1029:
							this.OnBalloonTipClicked();
							return;
						default:
							return;
						}
						break;
					}
				}
			}
			else
			{
				if (IntPtr.Zero == msg.LParam)
				{
					Command.DispatchID((int)msg.WParam & 65535);
					return;
				}
				this.window.DefWndProc(ref msg);
				return;
			}
			if (msg.Msg == NotifyIcon.WM_TASKBARCREATED)
			{
				this.WmTaskbarCreated(ref msg);
			}
			this.window.DefWndProc(ref msg);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000E054F File Offset: 0x000DE74F
		private void WmInitMenuPopup(ref Message m)
		{
			if (this.contextMenu != null && this.contextMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			this.window.DefWndProc(ref m);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000E057C File Offset: 0x000DE77C
		private void WmMeasureMenuItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(measureitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmMeasureItem(ref m);
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000E05B8 File Offset: 0x000DE7B8
		private void WmDrawItemMenuItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(drawitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmDrawItem(ref m);
			}
		}

		// Token: 0x04001E31 RID: 7729
		private static readonly object EVENT_MOUSEDOWN = new object();

		// Token: 0x04001E32 RID: 7730
		private static readonly object EVENT_MOUSEMOVE = new object();

		// Token: 0x04001E33 RID: 7731
		private static readonly object EVENT_MOUSEUP = new object();

		// Token: 0x04001E34 RID: 7732
		private static readonly object EVENT_CLICK = new object();

		// Token: 0x04001E35 RID: 7733
		private static readonly object EVENT_DOUBLECLICK = new object();

		// Token: 0x04001E36 RID: 7734
		private static readonly object EVENT_MOUSECLICK = new object();

		// Token: 0x04001E37 RID: 7735
		private static readonly object EVENT_MOUSEDOUBLECLICK = new object();

		// Token: 0x04001E38 RID: 7736
		private static readonly object EVENT_BALLOONTIPSHOWN = new object();

		// Token: 0x04001E39 RID: 7737
		private static readonly object EVENT_BALLOONTIPCLICKED = new object();

		// Token: 0x04001E3A RID: 7738
		private static readonly object EVENT_BALLOONTIPCLOSED = new object();

		// Token: 0x04001E3B RID: 7739
		private const int WM_TRAYMOUSEMESSAGE = 2048;

		// Token: 0x04001E3C RID: 7740
		private static int WM_TASKBARCREATED = SafeNativeMethods.RegisterWindowMessage("TaskbarCreated");

		// Token: 0x04001E3D RID: 7741
		private object syncObj = new object();

		// Token: 0x04001E3E RID: 7742
		private Icon icon;

		// Token: 0x04001E3F RID: 7743
		private string text = "";

		// Token: 0x04001E40 RID: 7744
		private int id;

		// Token: 0x04001E41 RID: 7745
		private bool added;

		// Token: 0x04001E42 RID: 7746
		private NotifyIcon.NotifyIconNativeWindow window;

		// Token: 0x04001E43 RID: 7747
		private ContextMenu contextMenu;

		// Token: 0x04001E44 RID: 7748
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x04001E45 RID: 7749
		private ToolTipIcon balloonTipIcon;

		// Token: 0x04001E46 RID: 7750
		private string balloonTipText = "";

		// Token: 0x04001E47 RID: 7751
		private string balloonTipTitle = "";

		// Token: 0x04001E48 RID: 7752
		private static int nextId = 0;

		// Token: 0x04001E49 RID: 7753
		private object userData;

		// Token: 0x04001E4A RID: 7754
		private bool doubleClick;

		// Token: 0x04001E4B RID: 7755
		private bool visible;

		// Token: 0x020007C9 RID: 1993
		private class NotifyIconNativeWindow : NativeWindow
		{
			// Token: 0x06006D78 RID: 28024 RVA: 0x0019261A File Offset: 0x0019081A
			internal NotifyIconNativeWindow(NotifyIcon component)
			{
				this.reference = component;
			}

			// Token: 0x06006D79 RID: 28025 RVA: 0x0019262C File Offset: 0x0019082C
			~NotifyIconNativeWindow()
			{
				if (base.Handle != IntPtr.Zero)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 16, 0, 0);
				}
			}

			// Token: 0x06006D7A RID: 28026 RVA: 0x0019267C File Offset: 0x0019087C
			public void LockReference(bool locked)
			{
				if (locked)
				{
					if (!this.rootRef.IsAllocated)
					{
						this.rootRef = GCHandle.Alloc(this.reference, GCHandleType.Normal);
						return;
					}
				}
				else if (this.rootRef.IsAllocated)
				{
					this.rootRef.Free();
				}
			}

			// Token: 0x06006D7B RID: 28027 RVA: 0x0003BADD File Offset: 0x00039CDD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x06006D7C RID: 28028 RVA: 0x001926B9 File Offset: 0x001908B9
			protected override void WndProc(ref Message m)
			{
				this.reference.WndProc(ref m);
			}

			// Token: 0x040041C4 RID: 16836
			internal NotifyIcon reference;

			// Token: 0x040041C5 RID: 16837
			private GCHandle rootRef;
		}
	}
}
