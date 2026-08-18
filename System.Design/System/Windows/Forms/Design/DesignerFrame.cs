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
	// Token: 0x0200021B RID: 539
	internal class DesignerFrame : Control, IOverlayService, ISplitWindowService
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x00066994 File Offset: 0x00065994
		public DesignerFrame(ISite site)
		{
			this.Text = "DesignerFrame";
			this.designerSite = site;
			this.designerRegion = new DesignerFrame.OverlayControl(site);
			base.Controls.Add(this.designerRegion);
			this.designerRegion.AutoScroll = true;
			this.designerRegion.Dock = DockStyle.Fill;
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x000669FF File Offset: 0x000659FF
		internal Point AutoScrollPosition
		{
			get
			{
				return this.designerRegion.AutoScrollPosition;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00066A0C File Offset: 0x00065A0C
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

		// Token: 0x06001435 RID: 5173 RVA: 0x00066A3C File Offset: 0x00065A3C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.designer != null)
			{
				Control control = this.designer;
				this.designer = null;
				control.Visible = false;
				control.Parent = null;
				SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00066A88 File Offset: 0x00065A88
		private void ForceDesignerRedraw(bool focus)
		{
			if (this.designer != null && this.designer.IsHandleCreated)
			{
				NativeMethods.SendMessage(this.designer.Handle, 134, focus ? 1 : 0, 0);
				SafeNativeMethods.RedrawWindow(this.designer.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00066AE4 File Offset: 0x00065AE4
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
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00066B50 File Offset: 0x00065B50
		protected override void OnGotFocus(EventArgs e)
		{
			this.ForceDesignerRedraw(true);
			ISelectionService selectionService = (ISelectionService)this.designerSite.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				Control control = selectionService.PrimarySelection as Control;
				if (control != null)
				{
					UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(control, control.Handle), -4, 0);
				}
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x00066BAA File Offset: 0x00065BAA
		protected override void OnLostFocus(EventArgs e)
		{
			this.ForceDesignerRedraw(false);
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00066BB4 File Offset: 0x00065BB4
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

		// Token: 0x0600143B RID: 5179 RVA: 0x00066C1C File Offset: 0x00065C1C
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Window)
			{
				this.SyncDesignerUI();
			}
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00066C2E File Offset: 0x00065C2E
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return false;
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00066C34 File Offset: 0x00065C34
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

		// Token: 0x0600143E RID: 5182 RVA: 0x00066C88 File Offset: 0x00065C88
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
					switch ((int)m.WParam & 65535)
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

		// Token: 0x0600143F RID: 5183 RVA: 0x00066DDF File Offset: 0x00065DDF
		int IOverlayService.PushOverlay(Control control)
		{
			return this.designerRegion.PushOverlay(control);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x00066DED File Offset: 0x00065DED
		void IOverlayService.RemoveOverlay(Control control)
		{
			this.designerRegion.RemoveOverlay(control);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00066DFB File Offset: 0x00065DFB
		void IOverlayService.InsertOverlay(Control control, int index)
		{
			this.designerRegion.InsertOverlay(control, index);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x00066E0A File Offset: 0x00065E0A
		void IOverlayService.InvalidateOverlays(Rectangle screenRectangle)
		{
			this.designerRegion.InvalidateOverlays(screenRectangle);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x00066E18 File Offset: 0x00065E18
		void IOverlayService.InvalidateOverlays(Region screenRegion)
		{
			this.designerRegion.InvalidateOverlays(screenRegion);
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00066E28 File Offset: 0x00065E28
		void ISplitWindowService.AddSplitWindow(Control window)
		{
			if (this.splitter == null)
			{
				this.splitter = new Splitter();
				this.splitter.BackColor = SystemColors.Control;
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

		// Token: 0x06001445 RID: 5189 RVA: 0x00066ED6 File Offset: 0x00065ED6
		void ISplitWindowService.RemoveSplitWindow(Control window)
		{
			base.SuspendLayout();
			base.Controls.Remove(window);
			base.Controls.Remove(this.splitter);
			base.ResumeLayout();
		}

		// Token: 0x040011F3 RID: 4595
		private ISite designerSite;

		// Token: 0x040011F4 RID: 4596
		private DesignerFrame.OverlayControl designerRegion;

		// Token: 0x040011F5 RID: 4597
		private Splitter splitter;

		// Token: 0x040011F6 RID: 4598
		private Control designer;

		// Token: 0x040011F7 RID: 4599
		private BehaviorService behaviorService;

		// Token: 0x0200021C RID: 540
		private class OverlayControl : ScrollableControl
		{
			// Token: 0x06001446 RID: 5190 RVA: 0x00066F01 File Offset: 0x00065F01
			public OverlayControl(IServiceProvider provider)
			{
				this.provider = provider;
				this.overlayList = new ArrayList();
				this.AutoScroll = true;
				this.Text = "OverlayControl";
			}

			// Token: 0x06001447 RID: 5191 RVA: 0x00066F2D File Offset: 0x00065F2D
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new DesignerFrame.OverlayControl.OverlayControlAccessibleObject(this);
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06001448 RID: 5192 RVA: 0x00066F35 File Offset: 0x00065F35
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

			// Token: 0x06001449 RID: 5193 RVA: 0x00066F68 File Offset: 0x00065F68
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

			// Token: 0x0600144A RID: 5194 RVA: 0x00066FE4 File Offset: 0x00065FE4
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

			// Token: 0x0600144B RID: 5195 RVA: 0x00067054 File Offset: 0x00066054
			private void ParentOverlay(Control control)
			{
				NativeMethods.SetParent(control.Handle, base.Handle);
				SafeNativeMethods.SetWindowPos(control.Handle, (IntPtr)0, 0, 0, 0, 0, 3);
			}

			// Token: 0x0600144C RID: 5196 RVA: 0x0006707F File Offset: 0x0006607F
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

			// Token: 0x0600144D RID: 5197 RVA: 0x000670B5 File Offset: 0x000660B5
			public void RemoveOverlay(Control control)
			{
				this.overlayList.Remove(control);
				control.Visible = false;
				control.Parent = null;
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x000670D4 File Offset: 0x000660D4
			public void InsertOverlay(Control control, int index)
			{
				Control control2 = (Control)this.overlayList[index];
				this.RemoveOverlay(control2);
				this.PushOverlay(control);
				this.PushOverlay(control2);
				control2.Visible = true;
			}

			// Token: 0x0600144F RID: 5199 RVA: 0x00067114 File Offset: 0x00066114
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

			// Token: 0x06001450 RID: 5200 RVA: 0x00067184 File Offset: 0x00066184
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

			// Token: 0x06001451 RID: 5201 RVA: 0x00067220 File Offset: 0x00066220
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				if (m.Msg == 528 && NativeMethods.Util.LOWORD((int)m.WParam) == 1)
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

			// Token: 0x040011F8 RID: 4600
			private ArrayList overlayList;

			// Token: 0x040011F9 RID: 4601
			private IServiceProvider provider;

			// Token: 0x040011FA RID: 4602
			internal bool messageMouseWheelProcessed;

			// Token: 0x040011FB RID: 4603
			private BehaviorService behaviorService;

			// Token: 0x0200021D RID: 541
			public class OverlayControlAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x06001452 RID: 5202 RVA: 0x00067388 File Offset: 0x00066388
				public OverlayControlAccessibleObject(DesignerFrame.OverlayControl owner) : base(owner)
				{
				}

				// Token: 0x06001453 RID: 5203 RVA: 0x00067394 File Offset: 0x00066394
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
