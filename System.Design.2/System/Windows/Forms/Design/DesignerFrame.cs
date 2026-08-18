using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002D6 RID: 726
	internal class DesignerFrame : Control, IOverlayService, ISplitWindowService, IContainsThemedScrollbarWindows
	{
		// Token: 0x06001CCB RID: 7371 RVA: 0x000ADAEC File Offset: 0x000ABCEC
		public DesignerFrame(ISite site)
		{
			this.Text = "DesignerFrame";
			this.designerSite = site;
			this.designerRegion = new DesignerFrame.OverlayControl(site);
			this.uiService = (this.designerSite.GetService(typeof(IUIService)) as IUIService);
			if (this.uiService != null && this.uiService.Styles["ArtboardBackground"] is Color)
			{
				this.BackColor = (Color)this.uiService.Styles["ArtboardBackground"];
			}
			base.Controls.Add(this.designerRegion);
			this.designerRegion.AutoScroll = true;
			this.designerRegion.Dock = DockStyle.Fill;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000ADBAA File Offset: 0x000ABDAA
		internal Point AutoScrollPosition
		{
			get
			{
				return this.designerRegion.AutoScrollPosition;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x000ADBB7 File Offset: 0x000ABDB7
		private BehaviorService BehaviorService
		{
			get
			{
				if (this.behaviorService == null)
				{
					this.behaviorService = (this.designerSite.GetService(typeof(BehaviorService)) as BehaviorService);
				}
				return this.behaviorService;
			}
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000ADBE8 File Offset: 0x000ABDE8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.designer != null)
				{
					Control control = this.designer;
					this.designer = null;
					control.Visible = false;
					control.Parent = null;
					SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
				}
				if (this.splitter != null)
				{
					this.splitter.SplitterMoved -= this.OnSplitterMoved;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000ADC54 File Offset: 0x000ABE54
		private void ForceDesignerRedraw(bool focus)
		{
			if (this.designer != null && this.designer.IsHandleCreated)
			{
				NativeMethods.SendMessage(this.designer.Handle, 134, focus ? 1 : 0, 0);
				SafeNativeMethods.RedrawWindow(this.designer.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000ADCB0 File Offset: 0x000ABEB0
		public void Initialize(Control view)
		{
			this.designer = view;
			Form form = this.designer as Form;
			if (form != null)
			{
				form.TopLevel = false;
			}
			this.designerRegion.Controls.Add(this.designer);
			this.SyncDesignerUI();
			this.designer.Visible = true;
			this.designer.Enabled = true;
			IntPtr handle = this.designer.Handle;
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000ADD2C File Offset: 0x000ABF2C
		protected override void OnGotFocus(EventArgs e)
		{
			this.ForceDesignerRedraw(true);
			ISelectionService selectionService = (ISelectionService)this.designerSite.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				Control control = selectionService.PrimarySelection as Control;
				if (control != null && !control.IsDisposed)
				{
					UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(control, control.Handle), -4, 0);
				}
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000ADD8E File Offset: 0x000ABF8E
		protected override void OnLostFocus(EventArgs e)
		{
			this.ForceDesignerRedraw(false);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000ADD98 File Offset: 0x000ABF98
		private void OnSplitterMoved(object sender, SplitterEventArgs e)
		{
			IComponentChangeService componentChangeService = this.designerSite.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				try
				{
					componentChangeService.OnComponentChanging(this.designerSite.Component, null);
					componentChangeService.OnComponentChanged(this.designerSite.Component, null, null, null);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000ADE00 File Offset: 0x000AC000
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window && this.designer != null)
			{
				this.SyncDesignerUI();
			}
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return false;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000ADE1C File Offset: 0x000AC01C
		private void SyncDesignerUI()
		{
			Size adornmentDimensions = DesignerUtils.GetAdornmentDimensions(AdornmentType.Maximum);
			this.designerRegion.AutoScrollMargin = adornmentDimensions;
			this.designer.Location = new Point(adornmentDimensions.Width, adornmentDimensions.Height);
			if (this.BehaviorService != null)
			{
				this.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000ADE70 File Offset: 0x000AC070
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 123)
			{
				if (msg != 256)
				{
					if (msg == 522 && !this.designerRegion.messageMouseWheelProcessed)
					{
						this.designerRegion.messageMouseWheelProcessed = true;
						NativeMethods.SendMessage(this.designerRegion.Handle, 522, m.WParam, m.LParam);
						return;
					}
				}
				else
				{
					int low = 0;
					int num = 0;
					switch ((int)((long)m.WParam) & 65535)
					{
					case 33:
						low = 2;
						num = 277;
						break;
					case 34:
						low = 3;
						num = 277;
						break;
					case 35:
						low = 7;
						num = 277;
						break;
					case 36:
						low = 6;
						num = 277;
						break;
					case 37:
						low = 0;
						num = 276;
						break;
					case 38:
						low = 0;
						num = 277;
						break;
					case 39:
						low = 1;
						num = 276;
						break;
					case 40:
						low = 1;
						num = 277;
						break;
					}
					if (num == 277 || num == 276)
					{
						NativeMethods.SendMessage(this.designerRegion.Handle, num, NativeMethods.Util.MAKELONG(low, 0), 0);
						return;
					}
				}
				base.WndProc(ref m);
				return;
			}
			NativeMethods.SendMessage(this.designer.Handle, m.Msg, m.WParam, m.LParam);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000ADFC8 File Offset: 0x000AC1C8
		int IOverlayService.PushOverlay(Control control)
		{
			return this.designerRegion.PushOverlay(control);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000ADFD6 File Offset: 0x000AC1D6
		void IOverlayService.RemoveOverlay(Control control)
		{
			this.designerRegion.RemoveOverlay(control);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000ADFE4 File Offset: 0x000AC1E4
		void IOverlayService.InsertOverlay(Control control, int index)
		{
			this.designerRegion.InsertOverlay(control, index);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000ADFF3 File Offset: 0x000AC1F3
		void IOverlayService.InvalidateOverlays(Rectangle screenRectangle)
		{
			this.designerRegion.InvalidateOverlays(screenRectangle);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000AE001 File Offset: 0x000AC201
		void IOverlayService.InvalidateOverlays(Region screenRegion)
		{
			this.designerRegion.InvalidateOverlays(screenRegion);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000AE010 File Offset: 0x000AC210
		void ISplitWindowService.AddSplitWindow(Control window)
		{
			if (this.splitter == null)
			{
				this.splitter = new Splitter();
				if (this.uiService != null && this.uiService.Styles["HorizontalResizeGrip"] is Color)
				{
					this.splitter.BackColor = (Color)this.uiService.Styles["HorizontalResizeGrip"];
				}
				else
				{
					this.splitter.BackColor = SystemColors.Control;
				}
				this.splitter.BorderStyle = BorderStyle.Fixed3D;
				this.splitter.Height = 7;
				this.splitter.Dock = DockStyle.Bottom;
				this.splitter.SplitterMoved += this.OnSplitterMoved;
			}
			base.SuspendLayout();
			window.Dock = DockStyle.Bottom;
			int num = 80;
			if (window.Height < num)
			{
				window.Height = num;
			}
			base.Controls.Add(this.splitter);
			base.Controls.Add(window);
			base.ResumeLayout();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000AE10C File Offset: 0x000AC30C
		void ISplitWindowService.RemoveSplitWindow(Control window)
		{
			base.SuspendLayout();
			base.Controls.Remove(window);
			base.Controls.Remove(this.splitter);
			base.ResumeLayout();
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000AE138 File Offset: 0x000AC338
		IEnumerable IContainsThemedScrollbarWindows.ThemedScrollbarWindows()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				ThemedScrollbarWindow themedScrollbarWindow = default(ThemedScrollbarWindow);
				themedScrollbarWindow.Handle = control.Handle;
				if (control is DesignerFrame.OverlayControl)
				{
					themedScrollbarWindow.Mode = ThemedScrollbarMode.OnlyTopLevel;
				}
				else
				{
					themedScrollbarWindow.Mode = ThemedScrollbarMode.All;
				}
				arrayList.Add(themedScrollbarWindow);
			}
			return arrayList;
		}

		// Token: 0x04001716 RID: 5910
		private ISite designerSite;

		// Token: 0x04001717 RID: 5911
		private DesignerFrame.OverlayControl designerRegion;

		// Token: 0x04001718 RID: 5912
		private Splitter splitter;

		// Token: 0x04001719 RID: 5913
		private Control designer;

		// Token: 0x0400171A RID: 5914
		private BehaviorService behaviorService;

		// Token: 0x0400171B RID: 5915
		private IUIService uiService;

		// Token: 0x0200056B RID: 1387
		private class OverlayControl : ScrollableControl
		{
			// Token: 0x060031BE RID: 12734 RVA: 0x0010E1B4 File Offset: 0x0010C3B4
			public OverlayControl(IServiceProvider provider)
			{
				this.provider = provider;
				this.overlayList = new ArrayList();
				this.AutoScroll = true;
				this.Text = "OverlayControl";
			}

			// Token: 0x060031BF RID: 12735 RVA: 0x0010E1E0 File Offset: 0x0010C3E0
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new DesignerFrame.OverlayControl.OverlayControlAccessibleObject(this);
			}

			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x060031C0 RID: 12736 RVA: 0x0010E1E8 File Offset: 0x0010C3E8
			private BehaviorService BehaviorService
			{
				get
				{
					if (this.behaviorService == null)
					{
						this.behaviorService = (this.provider.GetService(typeof(BehaviorService)) as BehaviorService);
					}
					return this.behaviorService;
				}
			}

			// Token: 0x060031C1 RID: 12737 RVA: 0x0010E218 File Offset: 0x0010C418
			protected override void OnCreateControl()
			{
				base.OnCreateControl();
				if (this.overlayList != null)
				{
					foreach (object obj in this.overlayList)
					{
						Control control = (Control)obj;
						this.ParentOverlay(control);
					}
				}
				if (this.BehaviorService != null)
				{
					this.BehaviorService.SyncSelection();
				}
			}

			// Token: 0x060031C2 RID: 12738 RVA: 0x0010E294 File Offset: 0x0010C494
			protected override void OnLayout(LayoutEventArgs e)
			{
				base.OnLayout(e);
				Rectangle displayRectangle = this.DisplayRectangle;
				if (this.overlayList != null)
				{
					foreach (object obj in this.overlayList)
					{
						Control control = (Control)obj;
						control.Bounds = displayRectangle;
					}
				}
			}

			// Token: 0x060031C3 RID: 12739 RVA: 0x0010E304 File Offset: 0x0010C504
			private void ParentOverlay(Control control)
			{
				NativeMethods.SetParent(control.Handle, base.Handle);
				SafeNativeMethods.SetWindowPos(control.Handle, (IntPtr)0, 0, 0, 0, 0, 3);
			}

			// Token: 0x060031C4 RID: 12740 RVA: 0x0010E32F File Offset: 0x0010C52F
			public int PushOverlay(Control control)
			{
				this.overlayList.Add(control);
				if (base.IsHandleCreated)
				{
					this.ParentOverlay(control);
					control.Bounds = this.DisplayRectangle;
				}
				return this.overlayList.IndexOf(control);
			}

			// Token: 0x060031C5 RID: 12741 RVA: 0x0010E365 File Offset: 0x0010C565
			public void RemoveOverlay(Control control)
			{
				this.overlayList.Remove(control);
				control.Visible = false;
				control.Parent = null;
			}

			// Token: 0x060031C6 RID: 12742 RVA: 0x0010E384 File Offset: 0x0010C584
			public void InsertOverlay(Control control, int index)
			{
				Control control2 = (Control)this.overlayList[index];
				this.RemoveOverlay(control2);
				this.PushOverlay(control);
				this.PushOverlay(control2);
				control2.Visible = true;
			}

			// Token: 0x060031C7 RID: 12743 RVA: 0x0010E3C4 File Offset: 0x0010C5C4
			public void InvalidateOverlays(Rectangle screenRectangle)
			{
				for (int i = this.overlayList.Count - 1; i >= 0; i--)
				{
					Control control = this.overlayList[i] as Control;
					if (control != null)
					{
						Rectangle rectangle = new Rectangle(control.PointToClient(screenRectangle.Location), screenRectangle.Size);
						if (control.ClientRectangle.IntersectsWith(rectangle))
						{
							control.Invalidate(rectangle);
						}
					}
				}
			}

			// Token: 0x060031C8 RID: 12744 RVA: 0x0010E434 File Offset: 0x0010C634
			public void InvalidateOverlays(Region screenRegion)
			{
				for (int i = this.overlayList.Count - 1; i >= 0; i--)
				{
					Control control = this.overlayList[i] as Control;
					if (control != null)
					{
						Rectangle bounds = control.Bounds;
						bounds.Location = control.PointToScreen(control.Location);
						using (Region region = screenRegion.Clone())
						{
							region.Intersect(bounds);
							region.Translate(-bounds.X, -bounds.Y);
							control.Invalidate(region);
						}
					}
				}
			}

			// Token: 0x060031C9 RID: 12745 RVA: 0x0010E4D0 File Offset: 0x0010C6D0
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				if (m.Msg == 528 && NativeMethods.Util.LOWORD((int)((long)m.WParam)) == 1)
				{
					if (this.overlayList == null)
					{
						return;
					}
					bool flag = false;
					foreach (object obj in this.overlayList)
					{
						Control control = (Control)obj;
						if (control.IsHandleCreated && m.LParam == control.Handle)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						return;
					}
					using (IEnumerator enumerator2 = this.overlayList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							Control control2 = (Control)obj2;
							SafeNativeMethods.SetWindowPos(control2.Handle, (IntPtr)0, 0, 0, 0, 0, 3);
						}
						return;
					}
				}
				if ((m.Msg == 277 || m.Msg == 276) && this.BehaviorService != null)
				{
					this.BehaviorService.SyncSelection();
					return;
				}
				if (m.Msg == 522)
				{
					this.messageMouseWheelProcessed = false;
					if (this.BehaviorService != null)
					{
						this.BehaviorService.SyncSelection();
					}
				}
			}

			// Token: 0x0400213A RID: 8506
			private ArrayList overlayList;

			// Token: 0x0400213B RID: 8507
			private IServiceProvider provider;

			// Token: 0x0400213C RID: 8508
			internal bool messageMouseWheelProcessed;

			// Token: 0x0400213D RID: 8509
			private BehaviorService behaviorService;

			// Token: 0x020005EE RID: 1518
			public class OverlayControlAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x060034DF RID: 13535 RVA: 0x0011F032 File Offset: 0x0011D232
				public OverlayControlAccessibleObject(DesignerFrame.OverlayControl owner) : base(owner)
				{
				}

				// Token: 0x060034E0 RID: 13536 RVA: 0x0011F03C File Offset: 0x0011D23C
				public override AccessibleObject HitTest(int x, int y)
				{
					foreach (object obj in base.Owner.Controls)
					{
						Control control = (Control)obj;
						AccessibleObject accessibilityObject = control.AccessibilityObject;
						if (accessibilityObject.Bounds.Contains(x, y))
						{
							return accessibilityObject;
						}
					}
					return base.HitTest(x, y);
				}
			}
		}
	}
}
