using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000375 RID: 885
	public sealed class BehaviorService : IDisposable
	{
		// Token: 0x06002431 RID: 9265 RVA: 0x000E0AB0 File Offset: 0x000DECB0
		internal BehaviorService(IServiceProvider serviceProvider, Control windowFrame)
		{
			this.serviceProvider = serviceProvider;
			this.adornerWindow = new BehaviorService.AdornerWindow(this, windowFrame);
			IOverlayService overlayService = (IOverlayService)serviceProvider.GetService(typeof(IOverlayService));
			if (overlayService != null)
			{
				this.adornerWindowIndex = overlayService.PushOverlay(this.adornerWindow);
			}
			this.dragEnterReplies = new Hashtable();
			this.adorners = new BehaviorServiceAdornerCollection(this);
			this.behaviorStack = new ArrayList();
			this.hitTestedGlyph = null;
			this.validDragArgs = null;
			this.actionPointer = null;
			this.trackMouseEvent = null;
			this.trackingMouseEvent = false;
			IMenuCommandService menuCommandService = serviceProvider.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
			IDesignerHost designerHost = serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (menuCommandService != null && designerHost != null)
			{
				this.menuCommandHandler = new BehaviorService.MenuCommandHandler(this, menuCommandService);
				designerHost.RemoveService(typeof(IMenuCommandService));
				designerHost.AddService(typeof(IMenuCommandService), this.menuCommandHandler);
			}
			this.useSnapLines = false;
			this.queriedSnapLines = false;
			BehaviorService.WM_GETALLSNAPLINES = SafeNativeMethods.RegisterWindowMessage("WM_GETALLSNAPLINES");
			BehaviorService.WM_GETRECENTSNAPLINES = SafeNativeMethods.RegisterWindowMessage("WM_GETRECENTSNAPLINES");
			SystemEvents.DisplaySettingsChanged += this.OnSystemSettingChanged;
			SystemEvents.InstalledFontsChanged += this.OnSystemSettingChanged;
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x000E0C12 File Offset: 0x000DEE12
		public BehaviorServiceAdornerCollection Adorners
		{
			get
			{
				return this.adorners;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x000E0C1A File Offset: 0x000DEE1A
		internal int AdornerWindowIndex
		{
			get
			{
				return this.adornerWindowIndex;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x000E0C22 File Offset: 0x000DEE22
		internal Control AdornerWindowControl
		{
			get
			{
				return this.adornerWindow;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x000E0C2C File Offset: 0x000DEE2C
		public Graphics AdornerWindowGraphics
		{
			get
			{
				Graphics graphics = this.adornerWindow.CreateGraphics();
				graphics.Clip = new Region(this.adornerWindow.DesignerFrameDisplayRectangle);
				return graphics;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x000E0C5C File Offset: 0x000DEE5C
		public Behavior CurrentBehavior
		{
			get
			{
				if (this.behaviorStack != null && this.behaviorStack.Count > 0)
				{
					return this.behaviorStack[0] as Behavior;
				}
				return null;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x000E0C87 File Offset: 0x000DEE87
		// (set) Token: 0x06002438 RID: 9272 RVA: 0x000E0C8F File Offset: 0x000DEE8F
		internal bool CancelDrag
		{
			get
			{
				return this.cancelDrag;
			}
			set
			{
				this.cancelDrag = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000E0C98 File Offset: 0x000DEE98
		// (set) Token: 0x0600243A RID: 9274 RVA: 0x000E0CA0 File Offset: 0x000DEEA0
		internal DesignerActionUI DesignerActionUI
		{
			get
			{
				return this.actionPointer;
			}
			set
			{
				this.actionPointer = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000E0CA9 File Offset: 0x000DEEA9
		internal bool Dragging
		{
			get
			{
				return this.dragging;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x000E0CB1 File Offset: 0x000DEEB1
		internal bool HasCapture
		{
			get
			{
				return this.captureBehavior != null;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000E0CBC File Offset: 0x000DEEBC
		internal bool UseSnapLines
		{
			get
			{
				if (!this.queriedSnapLines)
				{
					this.queriedSnapLines = true;
					this.useSnapLines = DesignerUtils.UseSnapLines(this.serviceProvider);
				}
				return this.useSnapLines;
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000E0CE4 File Offset: 0x000DEEE4
		public Point AdornerWindowPointToScreen(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			NativeMethods.MapWindowPoints(this.adornerWindow.Handle, IntPtr.Zero, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000E0D30 File Offset: 0x000DEF30
		public Point AdornerWindowToScreen()
		{
			Point p = new Point(0, 0);
			return this.AdornerWindowPointToScreen(p);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000E0D50 File Offset: 0x000DEF50
		public Point ControlToAdornerWindow(Control c)
		{
			if (c.Parent == null)
			{
				return Point.Empty;
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = c.Left;
			point.y = c.Top;
			NativeMethods.MapWindowPoints(c.Parent.Handle, this.adornerWindow.Handle, point, 1);
			if (c.Parent.IsMirrored)
			{
				point.x -= c.Width;
			}
			return new Point(point.x, point.y);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000E0DD8 File Offset: 0x000DEFD8
		public Point MapAdornerWindowPoint(IntPtr handle, Point pt)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = pt.X;
			point.y = pt.Y;
			NativeMethods.MapWindowPoints(handle, this.adornerWindow.Handle, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000E0E2C File Offset: 0x000DF02C
		public Rectangle ControlRectInAdornerWindow(Control c)
		{
			if (c.Parent == null)
			{
				return Rectangle.Empty;
			}
			Point location = this.ControlToAdornerWindow(c);
			return new Rectangle(location, c.Size);
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000E0E5B File Offset: 0x000DF05B
		internal bool IsDisposed
		{
			get
			{
				return this.adornerWindow == null || this.adornerWindow.IsDisposed;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000E0E72 File Offset: 0x000DF072
		private Control DropSource
		{
			get
			{
				if (this.dropSource == null)
				{
					this.dropSource = new Control();
				}
				return this.dropSource;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x000E0E8D File Offset: 0x000DF08D
		internal string[] RecentSnapLines
		{
			set
			{
				this.testHook_RecentSnapLines = value;
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002446 RID: 9286 RVA: 0x000E0E96 File Offset: 0x000DF096
		// (remove) Token: 0x06002447 RID: 9287 RVA: 0x000E0EAF File Offset: 0x000DF0AF
		public event BehaviorDragDropEventHandler BeginDrag
		{
			add
			{
				this.beginDragHandler = (BehaviorDragDropEventHandler)Delegate.Combine(this.beginDragHandler, value);
			}
			remove
			{
				this.beginDragHandler = (BehaviorDragDropEventHandler)Delegate.Remove(this.beginDragHandler, value);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002448 RID: 9288 RVA: 0x000E0EC8 File Offset: 0x000DF0C8
		// (remove) Token: 0x06002449 RID: 9289 RVA: 0x000E0EE1 File Offset: 0x000DF0E1
		public event BehaviorDragDropEventHandler EndDrag
		{
			add
			{
				this.endDragHandler = (BehaviorDragDropEventHandler)Delegate.Combine(this.endDragHandler, value);
			}
			remove
			{
				this.endDragHandler = (BehaviorDragDropEventHandler)Delegate.Remove(this.endDragHandler, value);
			}
		}

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x0600244A RID: 9290 RVA: 0x000E0EFA File Offset: 0x000DF0FA
		// (remove) Token: 0x0600244B RID: 9291 RVA: 0x000E0F13 File Offset: 0x000DF113
		public event EventHandler Synchronize
		{
			add
			{
				this.synchronizeEventHandler = (EventHandler)Delegate.Combine(this.synchronizeEventHandler, value);
			}
			remove
			{
				this.synchronizeEventHandler = (EventHandler)Delegate.Remove(this.synchronizeEventHandler, value);
			}
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000E0F2C File Offset: 0x000DF12C
		public void Dispose()
		{
			IOverlayService overlayService = (IOverlayService)this.serviceProvider.GetService(typeof(IOverlayService));
			if (overlayService != null)
			{
				overlayService.RemoveOverlay(this.adornerWindow);
			}
			if (this.dropSource != null)
			{
				this.dropSource.Dispose();
			}
			IMenuCommandService menuCommandService = this.serviceProvider.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			BehaviorService.MenuCommandHandler menuCommandHandler = null;
			if (menuCommandService != null)
			{
				menuCommandHandler = (menuCommandService as BehaviorService.MenuCommandHandler);
			}
			if (menuCommandHandler != null && designerHost != null)
			{
				IMenuCommandService menuService = menuCommandHandler.MenuService;
				designerHost.RemoveService(typeof(IMenuCommandService));
				designerHost.AddService(typeof(IMenuCommandService), menuService);
			}
			this.adornerWindow.Dispose();
			SystemEvents.DisplaySettingsChanged -= this.OnSystemSettingChanged;
			SystemEvents.InstalledFontsChanged -= this.OnSystemSettingChanged;
			SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000E1028 File Offset: 0x000DF228
		internal DragDropEffects DoDragDrop(DropSourceBehavior dropSourceBehavior)
		{
			this.DropSource.QueryContinueDrag += dropSourceBehavior.QueryContinueDrag;
			this.DropSource.GiveFeedback += dropSourceBehavior.GiveFeedback;
			DragDropEffects result = DragDropEffects.None;
			ICollection dragComponents = ((DropSourceBehavior.BehaviorDataObject)dropSourceBehavior.DataObject).DragComponents;
			BehaviorDragDropEventArgs e = new BehaviorDragDropEventArgs(dragComponents);
			try
			{
				try
				{
					this.OnBeginDrag(e);
					this.dragging = true;
					this.cancelDrag = false;
					this.dragEnterReplies.Clear();
					result = this.DropSource.DoDragDrop(dropSourceBehavior.DataObject, dropSourceBehavior.AllowedEffects);
				}
				finally
				{
					this.DropSource.QueryContinueDrag -= dropSourceBehavior.QueryContinueDrag;
					this.DropSource.GiveFeedback -= dropSourceBehavior.GiveFeedback;
					this.EndDragNotification();
					this.validDragArgs = null;
					this.dragging = false;
					this.cancelDrag = false;
					this.OnEndDrag(e);
				}
			}
			catch (CheckoutException ex)
			{
				if (ex != CheckoutException.Canceled)
				{
					throw;
				}
				result = DragDropEffects.None;
			}
			finally
			{
				if (dropSourceBehavior != null)
				{
					dropSourceBehavior.CleanupDrag();
				}
			}
			return result;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000E1150 File Offset: 0x000DF350
		private void TestHook_GetAllSnapLines(ref Message m)
		{
			string text = "";
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost == null)
			{
				return;
			}
			foreach (object obj in designerHost.Container.Components)
			{
				Component component = (Component)obj;
				if (component is Control)
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
					if (controlDesigner != null)
					{
						foreach (object obj2 in controlDesigner.SnapLines)
						{
							SnapLine snapLine = (SnapLine)obj2;
							text = string.Concat(new string[]
							{
								text,
								snapLine.ToString(),
								"\tAssociated Control = ",
								controlDesigner.Control.Name,
								":::"
							});
						}
					}
				}
			}
			this.TestHook_SetText(ref m, text);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000E1280 File Offset: 0x000DF480
		internal void EndDragNotification()
		{
			this.adornerWindow.EndDragNotification();
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000E1290 File Offset: 0x000DF490
		private MenuCommand FindCommand(CommandID commandID, IMenuCommandService menuService)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			if (appropriateBehavior != null)
			{
				if (appropriateBehavior.DisableAllCommands)
				{
					MenuCommand menuCommand = menuService.FindCommand(commandID);
					if (menuCommand != null)
					{
						menuCommand.Enabled = false;
					}
					return menuCommand;
				}
				MenuCommand menuCommand2 = appropriateBehavior.FindCommand(commandID);
				if (menuCommand2 != null)
				{
					return menuCommand2;
				}
			}
			return menuService.FindCommand(commandID);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000E12E0 File Offset: 0x000DF4E0
		private void TestHook_GetRecentSnapLines(ref Message m)
		{
			string text = "";
			if (this.testHook_RecentSnapLines != null)
			{
				foreach (string str in this.testHook_RecentSnapLines)
				{
					text = text + str + "\n";
				}
			}
			this.TestHook_SetText(ref m, text);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000E132C File Offset: 0x000DF52C
		private void TestHook_SetText(ref Message m, string text)
		{
			if (m.LParam == IntPtr.Zero)
			{
				m.Result = (IntPtr)((text.Length + 1) * Marshal.SystemDefaultCharSize);
				return;
			}
			if ((int)((long)m.WParam) < text.Length + 1)
			{
				m.Result = (IntPtr)(-1);
				return;
			}
			char[] chars = new char[1];
			byte[] bytes;
			byte[] bytes2;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				bytes = Encoding.Default.GetBytes(text);
				bytes2 = Encoding.Default.GetBytes(chars);
			}
			else
			{
				bytes = Encoding.Unicode.GetBytes(text);
				bytes2 = Encoding.Unicode.GetBytes(chars);
			}
			Marshal.Copy(bytes, 0, m.LParam, bytes.Length);
			Marshal.Copy(bytes2, 0, (IntPtr)((long)m.LParam + (long)bytes.Length), bytes2.Length);
			m.Result = (IntPtr)((bytes.Length + bytes2.Length) / Marshal.SystemDefaultCharSize);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000E1410 File Offset: 0x000DF610
		private Behavior GetAppropriateBehavior(Glyph g)
		{
			if (this.behaviorStack != null && this.behaviorStack.Count > 0)
			{
				return this.behaviorStack[0] as Behavior;
			}
			if (g != null && g.Behavior != null)
			{
				return g.Behavior;
			}
			return null;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000E1450 File Offset: 0x000DF650
		public Behavior GetNextBehavior(Behavior behavior)
		{
			if (this.behaviorStack != null && this.behaviorStack.Count > 0)
			{
				int num = this.behaviorStack.IndexOf(behavior);
				if (num != -1 && num < this.behaviorStack.Count - 1)
				{
					return this.behaviorStack[num + 1] as Behavior;
				}
			}
			return null;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000E14AC File Offset: 0x000DF6AC
		internal Glyph[] GetIntersectingGlyphs(Glyph primaryGlyph)
		{
			if (primaryGlyph == null)
			{
				return new Glyph[0];
			}
			Rectangle bounds = primaryGlyph.Bounds;
			ArrayList arrayList = new ArrayList();
			for (int i = this.adorners.Count - 1; i >= 0; i--)
			{
				if (this.adorners[i].Enabled)
				{
					for (int j = 0; j < this.adorners[i].Glyphs.Count; j++)
					{
						Glyph glyph = this.adorners[i].Glyphs[j];
						if (bounds.IntersectsWith(glyph.Bounds))
						{
							arrayList.Add(glyph);
						}
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return new Glyph[0];
			}
			return (Glyph[])arrayList.ToArray(typeof(Glyph));
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000E1574 File Offset: 0x000DF774
		private void HookMouseEvent()
		{
			if (!this.trackingMouseEvent)
			{
				this.trackingMouseEvent = true;
				if (this.trackMouseEvent == null)
				{
					this.trackMouseEvent = new NativeMethods.TRACKMOUSEEVENT();
					this.trackMouseEvent.dwFlags = NativeMethods.TME_HOVER;
					this.trackMouseEvent.hwndTrack = this.adornerWindow.Handle;
				}
				SafeNativeMethods.TrackMouseEvent(this.trackMouseEvent);
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000E15D8 File Offset: 0x000DF7D8
		internal void EnableAllAdorners(bool enabled)
		{
			foreach (Adorner adorner in this.Adorners)
			{
				adorner.EnabledInternal = enabled;
			}
			this.Invalidate();
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000E1634 File Offset: 0x000DF834
		public void Invalidate()
		{
			this.adornerWindow.InvalidateAdornerWindow();
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000E1641 File Offset: 0x000DF841
		public void Invalidate(Rectangle rect)
		{
			this.adornerWindow.InvalidateAdornerWindow(rect);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000E164F File Offset: 0x000DF84F
		public void Invalidate(Region r)
		{
			this.adornerWindow.InvalidateAdornerWindow(r);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000E1660 File Offset: 0x000DF860
		private void InvokeMouseEnterLeave(Glyph leaveGlyph, Glyph enterGlyph)
		{
			if (leaveGlyph != null)
			{
				if (enterGlyph != null && leaveGlyph.Equals(enterGlyph))
				{
					return;
				}
				if (this.validDragArgs != null)
				{
					this.OnDragLeave(leaveGlyph, EventArgs.Empty);
				}
				else
				{
					this.OnMouseLeave(leaveGlyph);
				}
			}
			if (enterGlyph != null)
			{
				if (this.validDragArgs != null)
				{
					this.OnDragEnter(enterGlyph, this.validDragArgs);
					return;
				}
				this.OnMouseEnter(enterGlyph);
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000E16BC File Offset: 0x000DF8BC
		public void SyncSelection()
		{
			if (this.synchronizeEventHandler != null)
			{
				this.synchronizeEventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000E16D7 File Offset: 0x000DF8D7
		private void OnSystemSettingChanged(object sender, EventArgs e)
		{
			this.SyncSelection();
			DesignerUtils.SyncBrushes();
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000E16D7 File Offset: 0x000DF8D7
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			this.SyncSelection();
			DesignerUtils.SyncBrushes();
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000E16E4 File Offset: 0x000DF8E4
		public Behavior PopBehavior(Behavior behavior)
		{
			if (this.behaviorStack.Count == 0)
			{
				throw new InvalidOperationException();
			}
			int num = this.behaviorStack.IndexOf(behavior);
			if (num == -1)
			{
				return null;
			}
			this.behaviorStack.RemoveAt(num);
			if (behavior == this.captureBehavior)
			{
				this.adornerWindow.Capture = false;
				if (this.captureBehavior != null)
				{
					this.OnLoseCapture();
				}
			}
			return behavior;
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000E1747 File Offset: 0x000DF947
		internal void ProcessPaintMessage(Rectangle paintRect)
		{
			this.adornerWindow.Invalidate(paintRect);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000E1758 File Offset: 0x000DF958
		private bool PropagateHitTest(Point pt)
		{
			for (int i = this.adorners.Count - 1; i >= 0; i--)
			{
				if (this.adorners[i].Enabled)
				{
					for (int j = 0; j < this.adorners[i].Glyphs.Count; j++)
					{
						Cursor hitTest = this.adorners[i].Glyphs[j].GetHitTest(pt);
						if (hitTest != null)
						{
							Glyph enterGlyph = this.adorners[i].Glyphs[j];
							this.InvokeMouseEnterLeave(this.hitTestedGlyph, enterGlyph);
							if (this.validDragArgs == null)
							{
								this.SetAppropriateCursor(hitTest);
							}
							this.hitTestedGlyph = enterGlyph;
							return this.hitTestedGlyph.Behavior is ControlDesigner.TransparentBehavior;
						}
					}
				}
			}
			this.InvokeMouseEnterLeave(this.hitTestedGlyph, null);
			if (this.validDragArgs == null)
			{
				Cursor appropriateCursor = Cursors.Default;
				if (this.behaviorStack != null && this.behaviorStack.Count > 0)
				{
					Behavior behavior = this.behaviorStack[0] as Behavior;
					if (behavior != null)
					{
						appropriateCursor = behavior.Cursor;
					}
				}
				this.SetAppropriateCursor(appropriateCursor);
			}
			this.hitTestedGlyph = null;
			return true;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000E1898 File Offset: 0x000DFA98
		private void PropagatePaint(PaintEventArgs pe)
		{
			for (int i = 0; i < this.adorners.Count; i++)
			{
				if (this.adorners[i].Enabled)
				{
					for (int j = this.adorners[i].Glyphs.Count - 1; j >= 0; j--)
					{
						this.adorners[i].Glyphs[j].Paint(pe);
					}
				}
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000E190E File Offset: 0x000DFB0E
		public void PushBehavior(Behavior behavior)
		{
			if (behavior == null)
			{
				throw new ArgumentNullException("behavior");
			}
			this.behaviorStack.Insert(0, behavior);
			if (this.captureBehavior != null && this.captureBehavior != behavior)
			{
				this.OnLoseCapture();
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000E1944 File Offset: 0x000DFB44
		public void PushCaptureBehavior(Behavior behavior)
		{
			this.PushBehavior(behavior);
			this.captureBehavior = behavior;
			this.adornerWindow.Capture = true;
			IUIService iuiservice = (IUIService)this.serviceProvider.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				IWin32Window dialogOwnerWindow = iuiservice.GetDialogOwnerWindow();
				if (dialogOwnerWindow != null && dialogOwnerWindow.Handle != IntPtr.Zero && dialogOwnerWindow.Handle != UnsafeNativeMethods.GetActiveWindow())
				{
					UnsafeNativeMethods.SetActiveWindow(new HandleRef(this, dialogOwnerWindow.Handle));
				}
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000E19CC File Offset: 0x000DFBCC
		public Point ScreenToAdornerWindow(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = p.X;
			point.y = p.Y;
			NativeMethods.MapWindowPoints(IntPtr.Zero, this.adornerWindow.Handle, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000E1A24 File Offset: 0x000DFC24
		private void SetAppropriateCursor(Cursor cursor)
		{
			if (cursor == Cursors.Default)
			{
				if (this.toolboxSvc == null)
				{
					this.toolboxSvc = (IToolboxService)this.serviceProvider.GetService(typeof(IToolboxService));
				}
				if (this.toolboxSvc != null && this.toolboxSvc.SetCursor())
				{
					cursor = new Cursor(NativeMethods.GetCursor());
				}
			}
			this.adornerWindow.Cursor = cursor;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000E1A94 File Offset: 0x000DFC94
		private void ShowError(Exception ex)
		{
			IUIService iuiservice = this.serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				iuiservice.ShowError(ex);
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000E1AC6 File Offset: 0x000DFCC6
		internal void StartDragNotification()
		{
			this.adornerWindow.StartDragNotification();
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000E1AD3 File Offset: 0x000DFCD3
		private void UnHookMouseEvent()
		{
			this.trackingMouseEvent = false;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000E1ADC File Offset: 0x000DFCDC
		private void OnBeginDrag(BehaviorDragDropEventArgs e)
		{
			if (this.beginDragHandler != null)
			{
				this.beginDragHandler(this, e);
			}
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000E1AF3 File Offset: 0x000DFCF3
		private void OnEndDrag(BehaviorDragDropEventArgs e)
		{
			if (this.endDragHandler != null)
			{
				this.endDragHandler(this, e);
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000E1B0C File Offset: 0x000DFD0C
		internal void OnLoseCapture()
		{
			if (this.captureBehavior != null)
			{
				Behavior behavior = this.captureBehavior;
				this.captureBehavior = null;
				try
				{
					behavior.OnLoseCapture(this.hitTestedGlyph, EventArgs.Empty);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000E1B58 File Offset: 0x000DFD58
		private bool OnMouseDoubleClick(MouseButtons button, Point mouseLoc)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			return appropriateBehavior != null && appropriateBehavior.OnMouseDoubleClick(this.hitTestedGlyph, button, mouseLoc);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000E1B88 File Offset: 0x000DFD88
		private bool OnMouseDown(MouseButtons button, Point mouseLoc)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			return appropriateBehavior != null && appropriateBehavior.OnMouseDown(this.hitTestedGlyph, button, mouseLoc);
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000E1BB8 File Offset: 0x000DFDB8
		private bool OnMouseEnter(Glyph g)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(g);
			return appropriateBehavior != null && appropriateBehavior.OnMouseEnter(g);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000E1BDC File Offset: 0x000DFDDC
		private bool OnMouseHover(Point mouseLoc)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			return appropriateBehavior != null && appropriateBehavior.OnMouseHover(this.hitTestedGlyph, mouseLoc);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000E1C08 File Offset: 0x000DFE08
		private bool OnMouseLeave(Glyph g)
		{
			this.UnHookMouseEvent();
			Behavior appropriateBehavior = this.GetAppropriateBehavior(g);
			return appropriateBehavior != null && appropriateBehavior.OnMouseLeave(g);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000E1C30 File Offset: 0x000DFE30
		private bool OnMouseMove(MouseButtons button, Point mouseLoc)
		{
			this.HookMouseEvent();
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			return appropriateBehavior != null && appropriateBehavior.OnMouseMove(this.hitTestedGlyph, button, mouseLoc);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000E1C64 File Offset: 0x000DFE64
		private bool OnMouseUp(MouseButtons button)
		{
			this.dragEnterReplies.Clear();
			this.validDragArgs = null;
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			return appropriateBehavior != null && appropriateBehavior.OnMouseUp(this.hitTestedGlyph, button);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		private void OnDragDrop(DragEventArgs e)
		{
			this.validDragArgs = null;
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			if (appropriateBehavior == null)
			{
				return;
			}
			appropriateBehavior.OnDragDrop(this.hitTestedGlyph, e);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000E1CD8 File Offset: 0x000DFED8
		private void OnDragEnter(Glyph g, DragEventArgs e)
		{
			if (g == null)
			{
				g = this.hitTestedGlyph;
			}
			Behavior appropriateBehavior = this.GetAppropriateBehavior(g);
			if (appropriateBehavior == null)
			{
				return;
			}
			appropriateBehavior.OnDragEnter(g, e);
			if (g != null && g is ControlBodyGlyph && e.Effect == DragDropEffects.None)
			{
				this.dragEnterReplies[g] = this;
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000E1D24 File Offset: 0x000DFF24
		private void OnDragLeave(Glyph g, EventArgs e)
		{
			this.dragEnterReplies.Clear();
			if (g == null)
			{
				g = this.hitTestedGlyph;
			}
			Behavior appropriateBehavior = this.GetAppropriateBehavior(g);
			if (appropriateBehavior == null)
			{
				return;
			}
			appropriateBehavior.OnDragLeave(g, e);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000E1D5C File Offset: 0x000DFF5C
		private void OnDragOver(DragEventArgs e)
		{
			this.validDragArgs = e;
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			if (appropriateBehavior == null)
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			if (this.hitTestedGlyph == null || (this.hitTestedGlyph != null && !this.dragEnterReplies.ContainsKey(this.hitTestedGlyph)))
			{
				appropriateBehavior.OnDragOver(this.hitTestedGlyph, e);
				return;
			}
			e.Effect = DragDropEffects.None;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000E1DC0 File Offset: 0x000DFFC0
		private void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			if (appropriateBehavior == null)
			{
				return;
			}
			appropriateBehavior.OnGiveFeedback(this.hitTestedGlyph, e);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000E1DEC File Offset: 0x000DFFEC
		private void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
			Behavior appropriateBehavior = this.GetAppropriateBehavior(this.hitTestedGlyph);
			if (appropriateBehavior == null)
			{
				return;
			}
			appropriateBehavior.OnQueryContinueDrag(this.hitTestedGlyph, e);
		}

		// Token: 0x04001A51 RID: 6737
		private IServiceProvider serviceProvider;

		// Token: 0x04001A52 RID: 6738
		private BehaviorService.AdornerWindow adornerWindow;

		// Token: 0x04001A53 RID: 6739
		private BehaviorServiceAdornerCollection adorners;

		// Token: 0x04001A54 RID: 6740
		private ArrayList behaviorStack;

		// Token: 0x04001A55 RID: 6741
		private Behavior captureBehavior;

		// Token: 0x04001A56 RID: 6742
		private Glyph hitTestedGlyph;

		// Token: 0x04001A57 RID: 6743
		private IToolboxService toolboxSvc;

		// Token: 0x04001A58 RID: 6744
		private Control dropSource;

		// Token: 0x04001A59 RID: 6745
		private DragEventArgs validDragArgs;

		// Token: 0x04001A5A RID: 6746
		private BehaviorDragDropEventHandler beginDragHandler;

		// Token: 0x04001A5B RID: 6747
		private BehaviorDragDropEventHandler endDragHandler;

		// Token: 0x04001A5C RID: 6748
		private EventHandler synchronizeEventHandler;

		// Token: 0x04001A5D RID: 6749
		private NativeMethods.TRACKMOUSEEVENT trackMouseEvent;

		// Token: 0x04001A5E RID: 6750
		private bool trackingMouseEvent;

		// Token: 0x04001A5F RID: 6751
		private string[] testHook_RecentSnapLines;

		// Token: 0x04001A60 RID: 6752
		private BehaviorService.MenuCommandHandler menuCommandHandler;

		// Token: 0x04001A61 RID: 6753
		private bool useSnapLines;

		// Token: 0x04001A62 RID: 6754
		private bool queriedSnapLines;

		// Token: 0x04001A63 RID: 6755
		private Hashtable dragEnterReplies;

		// Token: 0x04001A64 RID: 6756
		private static TraceSwitch dragDropSwitch = new TraceSwitch("BSDRAGDROP", "Behavior service drag & drop messages");

		// Token: 0x04001A65 RID: 6757
		private bool dragging;

		// Token: 0x04001A66 RID: 6758
		private bool cancelDrag;

		// Token: 0x04001A67 RID: 6759
		private int adornerWindowIndex = -1;

		// Token: 0x04001A68 RID: 6760
		private static int WM_GETALLSNAPLINES;

		// Token: 0x04001A69 RID: 6761
		private static int WM_GETRECENTSNAPLINES;

		// Token: 0x04001A6A RID: 6762
		private DesignerActionUI actionPointer;

		// Token: 0x04001A6B RID: 6763
		private const string ToolboxFormat = ".NET Toolbox Item";

		// Token: 0x020005A1 RID: 1441
		private class AdornerWindow : Control
		{
			// Token: 0x0600338A RID: 13194 RVA: 0x0011A694 File Offset: 0x00118894
			internal AdornerWindow(BehaviorService behaviorService, Control designerFrame)
			{
				this.behaviorService = behaviorService;
				this.designerFrame = designerFrame;
				this.Dock = DockStyle.Fill;
				this.AllowDrop = true;
				this.Text = "AdornerWindow";
				base.SetStyle(ControlStyles.Opaque, true);
			}

			// Token: 0x17000A0C RID: 2572
			// (get) Token: 0x0600338B RID: 13195 RVA: 0x0011A6CC File Offset: 0x001188CC
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style &= -100663297;
					createParams.ExStyle |= 32;
					return createParams;
				}
			}

			// Token: 0x17000A0D RID: 2573
			// (get) Token: 0x0600338C RID: 13196 RVA: 0x0011A702 File Offset: 0x00118902
			// (set) Token: 0x0600338D RID: 13197 RVA: 0x0011A70A File Offset: 0x0011890A
			internal bool ProcessingDrag
			{
				get
				{
					return this.processingDrag;
				}
				set
				{
					this.processingDrag = value;
				}
			}

			// Token: 0x0600338E RID: 13198 RVA: 0x0011A713 File Offset: 0x00118913
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				BehaviorService.AdornerWindow.AdornerWindowList.Add(this);
				if (BehaviorService.AdornerWindow.mouseHook == null)
				{
					BehaviorService.AdornerWindow.mouseHook = new BehaviorService.AdornerWindow.MouseHook();
				}
			}

			// Token: 0x0600338F RID: 13199 RVA: 0x0011A738 File Offset: 0x00118938
			protected override void OnHandleDestroyed(EventArgs e)
			{
				BehaviorService.AdornerWindow.AdornerWindowList.Remove(this);
				if (BehaviorService.AdornerWindow.AdornerWindowList.Count == 0 && BehaviorService.AdornerWindow.mouseHook != null)
				{
					BehaviorService.AdornerWindow.mouseHook.Dispose();
					BehaviorService.AdornerWindow.mouseHook = null;
				}
				base.OnHandleDestroyed(e);
			}

			// Token: 0x06003390 RID: 13200 RVA: 0x0011A770 File Offset: 0x00118970
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.designerFrame != null)
				{
					this.designerFrame = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x17000A0E RID: 2574
			// (get) Token: 0x06003391 RID: 13201 RVA: 0x0011A78B File Offset: 0x0011898B
			internal Control DesignerFrame
			{
				get
				{
					return this.designerFrame;
				}
			}

			// Token: 0x17000A0F RID: 2575
			// (get) Token: 0x06003392 RID: 13202 RVA: 0x0011A793 File Offset: 0x00118993
			internal Rectangle DesignerFrameDisplayRectangle
			{
				get
				{
					if (this.DesignerFrameValid)
					{
						return ((DesignerFrame)this.designerFrame).DisplayRectangle;
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x17000A10 RID: 2576
			// (get) Token: 0x06003393 RID: 13203 RVA: 0x0011A7B3 File Offset: 0x001189B3
			internal bool DesignerFrameValid
			{
				get
				{
					return this.designerFrame != null && !this.designerFrame.IsDisposed && this.designerFrame.IsHandleCreated;
				}
			}

			// Token: 0x06003394 RID: 13204 RVA: 0x0011A7DA File Offset: 0x001189DA
			internal void EndDragNotification()
			{
				this.ProcessingDrag = false;
			}

			// Token: 0x06003395 RID: 13205 RVA: 0x0011A7E3 File Offset: 0x001189E3
			internal void InvalidateAdornerWindow()
			{
				if (this.DesignerFrameValid)
				{
					this.designerFrame.Invalidate(true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003396 RID: 13206 RVA: 0x0011A804 File Offset: 0x00118A04
			internal void InvalidateAdornerWindow(Region region)
			{
				if (this.DesignerFrameValid)
				{
					Point autoScrollPosition = ((DesignerFrame)this.designerFrame).AutoScrollPosition;
					region.Translate(autoScrollPosition.X, autoScrollPosition.Y);
					this.designerFrame.Invalidate(region, true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003397 RID: 13207 RVA: 0x0011A858 File Offset: 0x00118A58
			internal void InvalidateAdornerWindow(Rectangle rectangle)
			{
				if (this.DesignerFrameValid)
				{
					Point autoScrollPosition = ((DesignerFrame)this.designerFrame).AutoScrollPosition;
					rectangle.Offset(autoScrollPosition.X, autoScrollPosition.Y);
					this.designerFrame.Invalidate(rectangle, true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003398 RID: 13208 RVA: 0x0011A8AC File Offset: 0x00118AAC
			protected override void OnDragDrop(DragEventArgs e)
			{
				try
				{
					this.behaviorService.OnDragDrop(e);
				}
				finally
				{
					this.ProcessingDrag = false;
				}
			}

			// Token: 0x06003399 RID: 13209 RVA: 0x0011A8E0 File Offset: 0x00118AE0
			private static bool IsLocalDrag(DragEventArgs e)
			{
				if (e.Data is DropSourceBehavior.BehaviorDataObject)
				{
					return true;
				}
				string[] formats = e.Data.GetFormats();
				for (int i = 0; i < formats.Length; i++)
				{
					if (formats[i].Length == ".NET Toolbox Item".Length && string.Equals(".NET Toolbox Item", formats[i]))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600339A RID: 13210 RVA: 0x0011A93C File Offset: 0x00118B3C
			protected override void OnDragEnter(DragEventArgs e)
			{
				this.ProcessingDrag = true;
				if (!BehaviorService.AdornerWindow.IsLocalDrag(e))
				{
					this.behaviorService.validDragArgs = e;
					NativeMethods.POINT point = new NativeMethods.POINT();
					NativeMethods.GetCursorPos(point);
					NativeMethods.MapWindowPoints(IntPtr.Zero, base.Handle, point, 1);
					Point pt = new Point(point.x, point.y);
					this.behaviorService.PropagateHitTest(pt);
				}
				this.behaviorService.OnDragEnter(null, e);
			}

			// Token: 0x0600339B RID: 13211 RVA: 0x0011A9B4 File Offset: 0x00118BB4
			protected override void OnDragLeave(EventArgs e)
			{
				this.behaviorService.validDragArgs = null;
				try
				{
					this.behaviorService.OnDragLeave(null, e);
				}
				finally
				{
					this.ProcessingDrag = false;
				}
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x0011A9F4 File Offset: 0x00118BF4
			protected override void OnDragOver(DragEventArgs e)
			{
				this.ProcessingDrag = true;
				if (!BehaviorService.AdornerWindow.IsLocalDrag(e))
				{
					this.behaviorService.validDragArgs = e;
					NativeMethods.POINT point = new NativeMethods.POINT();
					NativeMethods.GetCursorPos(point);
					NativeMethods.MapWindowPoints(IntPtr.Zero, base.Handle, point, 1);
					Point pt = new Point(point.x, point.y);
					this.behaviorService.PropagateHitTest(pt);
				}
				this.behaviorService.OnDragOver(e);
			}

			// Token: 0x0600339D RID: 13213 RVA: 0x0011AA68 File Offset: 0x00118C68
			protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
			{
				this.behaviorService.OnGiveFeedback(e);
			}

			// Token: 0x0600339E RID: 13214 RVA: 0x0011AA76 File Offset: 0x00118C76
			protected override void OnQueryContinueDrag(QueryContinueDragEventArgs e)
			{
				this.behaviorService.OnQueryContinueDrag(e);
			}

			// Token: 0x0600339F RID: 13215 RVA: 0x0011AA84 File Offset: 0x00118C84
			internal void StartDragNotification()
			{
				this.ProcessingDrag = true;
			}

			// Token: 0x060033A0 RID: 13216 RVA: 0x0011AA90 File Offset: 0x00118C90
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == BehaviorService.WM_GETALLSNAPLINES)
				{
					this.behaviorService.TestHook_GetAllSnapLines(ref m);
				}
				else if (m.Msg == BehaviorService.WM_GETRECENTSNAPLINES)
				{
					this.behaviorService.TestHook_GetRecentSnapLines(ref m);
				}
				int msg = m.Msg;
				if (msg != 15)
				{
					if (msg != 132)
					{
						if (msg != 533)
						{
							base.WndProc(ref m);
							return;
						}
						base.WndProc(ref m);
						this.behaviorService.OnLoseCapture();
						return;
					}
				}
				else
				{
					IntPtr intPtr = NativeMethods.CreateRectRgn(0, 0, 0, 0);
					NativeMethods.GetUpdateRgn(m.HWnd, intPtr, true);
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					NativeMethods.GetUpdateRect(m.HWnd, ref rect, true);
					Rectangle clipRect = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					try
					{
						using (Region region = Region.FromHrgn(intPtr))
						{
							this.DefWndProc(ref m);
							using (Graphics graphics = Graphics.FromHwnd(m.HWnd))
							{
								using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, clipRect))
								{
									graphics.Clip = region;
									this.behaviorService.PropagatePaint(paintEventArgs);
									return;
								}
							}
						}
					}
					finally
					{
						NativeMethods.DeleteObject(intPtr);
					}
				}
				Point pt = new Point((int)((short)NativeMethods.Util.LOWORD((int)((long)m.LParam))), (int)((short)NativeMethods.Util.HIWORD((int)((long)m.LParam))));
				NativeMethods.POINT point = new NativeMethods.POINT();
				point.x = 0;
				point.y = 0;
				NativeMethods.MapWindowPoints(IntPtr.Zero, base.Handle, point, 1);
				pt.Offset(point.x, point.y);
				if (this.behaviorService.PropagateHitTest(pt) && !this.ProcessingDrag)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				m.Result = (IntPtr)1;
			}

			// Token: 0x060033A1 RID: 13217 RVA: 0x0011ACB0 File Offset: 0x00118EB0
			private bool WndProcProxy(ref Message m, int x, int y)
			{
				Point point = new Point(x, y);
				this.behaviorService.PropagateHitTest(point);
				int msg = m.Msg;
				switch (msg)
				{
				case 512:
					if (this.behaviorService.OnMouseMove(Control.MouseButtons, point))
					{
						return false;
					}
					break;
				case 513:
					if (this.behaviorService.OnMouseDown(MouseButtons.Left, point))
					{
						return false;
					}
					break;
				case 514:
					if (this.behaviorService.OnMouseUp(MouseButtons.Left))
					{
						return false;
					}
					break;
				case 515:
					if (this.behaviorService.OnMouseDoubleClick(MouseButtons.Left, point))
					{
						return false;
					}
					break;
				case 516:
					if (this.behaviorService.OnMouseDown(MouseButtons.Right, point))
					{
						return false;
					}
					break;
				case 517:
					if (this.behaviorService.OnMouseUp(MouseButtons.Right))
					{
						return false;
					}
					break;
				case 518:
					if (this.behaviorService.OnMouseDoubleClick(MouseButtons.Right, point))
					{
						return false;
					}
					break;
				default:
					if (msg == 673)
					{
						if (this.behaviorService.OnMouseHover(point))
						{
							return false;
						}
					}
					break;
				}
				return true;
			}

			// Token: 0x04002281 RID: 8833
			private BehaviorService behaviorService;

			// Token: 0x04002282 RID: 8834
			private Control designerFrame;

			// Token: 0x04002283 RID: 8835
			private static BehaviorService.AdornerWindow.MouseHook mouseHook;

			// Token: 0x04002284 RID: 8836
			private static List<BehaviorService.AdornerWindow> AdornerWindowList = new List<BehaviorService.AdornerWindow>();

			// Token: 0x04002285 RID: 8837
			private bool processingDrag;

			// Token: 0x020005F4 RID: 1524
			private class MouseHook
			{
				// Token: 0x060034F6 RID: 13558 RVA: 0x0011F4CD File Offset: 0x0011D6CD
				public MouseHook()
				{
					this.HookMouse();
				}

				// Token: 0x060034F7 RID: 13559 RVA: 0x0011F4E6 File Offset: 0x0011D6E6
				public void Dispose()
				{
					this.UnhookMouse();
				}

				// Token: 0x060034F8 RID: 13560 RVA: 0x0011F4F0 File Offset: 0x0011D6F0
				private void HookMouse()
				{
					lock (this)
					{
						if (!(this.mouseHookHandle != IntPtr.Zero) && BehaviorService.AdornerWindow.AdornerWindowList.Count != 0)
						{
							if (this.thisProcessID == 0)
							{
								BehaviorService.AdornerWindow adornerWindow = BehaviorService.AdornerWindow.AdornerWindowList[0];
								UnsafeNativeMethods.GetWindowThreadProcessId(new HandleRef(adornerWindow, adornerWindow.Handle), out this.thisProcessID);
							}
							UnsafeNativeMethods.HookProc hookProc = new UnsafeNativeMethods.HookProc(this.MouseHookProc);
							this.mouseHookRoot = GCHandle.Alloc(hookProc);
							this.mouseHookHandle = UnsafeNativeMethods.SetWindowsHookEx(7, hookProc, new HandleRef(null, IntPtr.Zero), AppDomain.GetCurrentThreadId());
							if (this.mouseHookHandle != IntPtr.Zero)
							{
								this.isHooked = true;
							}
						}
					}
				}

				// Token: 0x060034F9 RID: 13561 RVA: 0x0011F5C4 File Offset: 0x0011D7C4
				private unsafe IntPtr MouseHookProc(int nCode, IntPtr wparam, IntPtr lparam)
				{
					if (this.isHooked && nCode == 0)
					{
						NativeMethods.MOUSEHOOKSTRUCT* ptr = (NativeMethods.MOUSEHOOKSTRUCT*)((void*)lparam);
						if (ptr != null)
						{
							try
							{
								if (this.ProcessMouseMessage(ptr->hWnd, (int)((long)wparam), ptr->pt_x, ptr->pt_y))
								{
									return (IntPtr)1;
								}
							}
							catch (Exception ex)
							{
								this.currentAdornerWindow.Capture = false;
								if (ex != CheckoutException.Canceled)
								{
									this.currentAdornerWindow.behaviorService.ShowError(ex);
								}
								if (ClientUtils.IsCriticalException(ex))
								{
									throw;
								}
							}
							finally
							{
								this.currentAdornerWindow = null;
							}
						}
					}
					return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.mouseHookHandle), nCode, wparam, lparam);
				}

				// Token: 0x060034FA RID: 13562 RVA: 0x0011F680 File Offset: 0x0011D880
				private void UnhookMouse()
				{
					lock (this)
					{
						if (this.mouseHookHandle != IntPtr.Zero)
						{
							UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.mouseHookHandle));
							this.mouseHookRoot.Free();
							this.mouseHookHandle = IntPtr.Zero;
							this.isHooked = false;
						}
					}
				}

				// Token: 0x060034FB RID: 13563 RVA: 0x0011F6F8 File Offset: 0x0011D8F8
				private bool ProcessMouseMessage(IntPtr hWnd, int msg, int x, int y)
				{
					if (this.processingMessage)
					{
						return false;
					}
					new NamedPermissionSet("FullTrust").Assert();
					foreach (BehaviorService.AdornerWindow adornerWindow in BehaviorService.AdornerWindow.AdornerWindowList)
					{
						if (adornerWindow.DesignerFrameValid)
						{
							this.currentAdornerWindow = adornerWindow;
							IntPtr handle = adornerWindow.DesignerFrame.Handle;
							if (adornerWindow.ProcessingDrag || (hWnd != handle && SafeNativeMethods.IsChild(new HandleRef(this, handle), new HandleRef(this, hWnd))))
							{
								int num;
								UnsafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, hWnd), out num);
								if (num != this.thisProcessID)
								{
									return false;
								}
								try
								{
									this.processingMessage = true;
									NativeMethods.POINT point = new NativeMethods.POINT();
									point.x = x;
									point.y = y;
									NativeMethods.MapWindowPoints(IntPtr.Zero, adornerWindow.Handle, point, 1);
									Message message = Message.Create(hWnd, msg, (IntPtr)0, (IntPtr)BehaviorService.AdornerWindow.MouseHook.MAKELONG(point.y, point.x));
									if (message.Msg == 513)
									{
										this.lastLButtonDownTimeStamp = UnsafeNativeMethods.GetMessageTime();
									}
									else if (message.Msg == 515)
									{
										int messageTime = UnsafeNativeMethods.GetMessageTime();
										if (messageTime == this.lastLButtonDownTimeStamp)
										{
											return true;
										}
									}
									if (!adornerWindow.WndProcProxy(ref message, point.x, point.y))
									{
										return true;
									}
									break;
								}
								finally
								{
									this.processingMessage = false;
								}
							}
						}
					}
					return false;
				}

				// Token: 0x060034FC RID: 13564 RVA: 0x0010754E File Offset: 0x0010574E
				public static int MAKELONG(int low, int high)
				{
					return high << 16 | (low & 65535);
				}

				// Token: 0x0400234C RID: 9036
				private BehaviorService.AdornerWindow currentAdornerWindow;

				// Token: 0x0400234D RID: 9037
				private int thisProcessID;

				// Token: 0x0400234E RID: 9038
				private GCHandle mouseHookRoot;

				// Token: 0x0400234F RID: 9039
				private IntPtr mouseHookHandle = IntPtr.Zero;

				// Token: 0x04002350 RID: 9040
				private bool processingMessage;

				// Token: 0x04002351 RID: 9041
				private bool isHooked;

				// Token: 0x04002352 RID: 9042
				private int lastLButtonDownTimeStamp;
			}
		}

		// Token: 0x020005A2 RID: 1442
		private class MenuCommandHandler : IMenuCommandService
		{
			// Token: 0x060033A3 RID: 13219 RVA: 0x0011ADC0 File Offset: 0x00118FC0
			public MenuCommandHandler(BehaviorService owner, IMenuCommandService menuService)
			{
				this.owner = owner;
				this.menuService = menuService;
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x060033A4 RID: 13220 RVA: 0x0011ADE1 File Offset: 0x00118FE1
			public IMenuCommandService MenuService
			{
				get
				{
					return this.menuService;
				}
			}

			// Token: 0x060033A5 RID: 13221 RVA: 0x0011ADE9 File Offset: 0x00118FE9
			void IMenuCommandService.AddCommand(MenuCommand command)
			{
				this.menuService.AddCommand(command);
			}

			// Token: 0x060033A6 RID: 13222 RVA: 0x0011ADF7 File Offset: 0x00118FF7
			void IMenuCommandService.RemoveVerb(DesignerVerb verb)
			{
				this.menuService.RemoveVerb(verb);
			}

			// Token: 0x060033A7 RID: 13223 RVA: 0x0011AE05 File Offset: 0x00119005
			void IMenuCommandService.RemoveCommand(MenuCommand command)
			{
				this.menuService.RemoveCommand(command);
			}

			// Token: 0x060033A8 RID: 13224 RVA: 0x0011AE14 File Offset: 0x00119014
			MenuCommand IMenuCommandService.FindCommand(CommandID commandID)
			{
				MenuCommand result;
				try
				{
					if (this.currentCommands.Contains(commandID))
					{
						result = null;
					}
					else
					{
						this.currentCommands.Push(commandID);
						result = this.owner.FindCommand(commandID, this.menuService);
					}
				}
				finally
				{
					this.currentCommands.Pop();
				}
				return result;
			}

			// Token: 0x060033A9 RID: 13225 RVA: 0x0011AE74 File Offset: 0x00119074
			bool IMenuCommandService.GlobalInvoke(CommandID commandID)
			{
				return this.menuService.GlobalInvoke(commandID);
			}

			// Token: 0x060033AA RID: 13226 RVA: 0x0011AE82 File Offset: 0x00119082
			void IMenuCommandService.ShowContextMenu(CommandID menuID, int x, int y)
			{
				this.menuService.ShowContextMenu(menuID, x, y);
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x0011AE92 File Offset: 0x00119092
			void IMenuCommandService.AddVerb(DesignerVerb verb)
			{
				this.menuService.AddVerb(verb);
			}

			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x060033AC RID: 13228 RVA: 0x0011AEA0 File Offset: 0x001190A0
			DesignerVerbCollection IMenuCommandService.Verbs
			{
				get
				{
					return this.menuService.Verbs;
				}
			}

			// Token: 0x04002286 RID: 8838
			private BehaviorService owner;

			// Token: 0x04002287 RID: 8839
			private IMenuCommandService menuService;

			// Token: 0x04002288 RID: 8840
			private Stack<CommandID> currentCommands = new Stack<CommandID>();
		}
	}
}
