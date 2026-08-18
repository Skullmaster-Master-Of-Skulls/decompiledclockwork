using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002F7 RID: 759
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionMenuStrip")]
	public class MenuStrip : ToolStrip
	{
		// Token: 0x06003022 RID: 12322 RVA: 0x000D8F33 File Offset: 0x000D7133
		public MenuStrip()
		{
			this.CanOverflow = false;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.Stretch = true;
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000D8F50 File Offset: 0x000D7150
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x000D8F58 File Offset: 0x000D7158
		internal override bool KeyboardActive
		{
			get
			{
				return base.KeyboardActive;
			}
			set
			{
				if (base.KeyboardActive != value)
				{
					base.KeyboardActive = value;
					if (value)
					{
						this.OnMenuActivate(EventArgs.Empty);
						return;
					}
					this.OnMenuDeactivate(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x000D8F84 File Offset: 0x000D7184
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x000D8F8C File Offset: 0x000D718C
		[DefaultValue(false)]
		[SRDescription("ToolStripCanOverflowDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x000D8F95 File Offset: 0x000D7195
		protected override Padding DefaultGripMargin
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Padding(2, 2, 0, 2);
				}
				return DpiHelper.LogicalToDeviceUnits(new Padding(2, 2, 0, 2), base.DeviceDpi);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003029 RID: 12329 RVA: 0x000D8FBC File Offset: 0x000D71BC
		protected override Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(200, 24);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(200, 24), base.DeviceDpi);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000D8FEC File Offset: 0x000D71EC
		protected override Padding DefaultPadding
		{
			get
			{
				if (this.GripStyle == ToolStripGripStyle.Visible)
				{
					if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
					{
						return new Padding(3, 2, 0, 2);
					}
					return DpiHelper.LogicalToDeviceUnits(new Padding(3, 2, 0, 2), base.DeviceDpi);
				}
				else
				{
					if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
					{
						return new Padding(6, 2, 0, 2);
					}
					return DpiHelper.LogicalToDeviceUnits(new Padding(6, 2, 0, 2), base.DeviceDpi);
				}
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600302B RID: 12331 RVA: 0x000D904D File Offset: 0x000D724D
		// (set) Token: 0x0600302C RID: 12332 RVA: 0x000D9055 File Offset: 0x000D7255
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		// Token: 0x1400022E RID: 558
		// (add) Token: 0x0600302D RID: 12333 RVA: 0x000D905E File Offset: 0x000D725E
		// (remove) Token: 0x0600302E RID: 12334 RVA: 0x000D9071 File Offset: 0x000D7271
		[SRCategory("CatBehavior")]
		[SRDescription("MenuStripMenuActivateDescr")]
		public event EventHandler MenuActivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.EventMenuActivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.EventMenuActivate, value);
			}
		}

		// Token: 0x1400022F RID: 559
		// (add) Token: 0x0600302F RID: 12335 RVA: 0x000D9084 File Offset: 0x000D7284
		// (remove) Token: 0x06003030 RID: 12336 RVA: 0x000D9097 File Offset: 0x000D7297
		[SRCategory("CatBehavior")]
		[SRDescription("MenuStripMenuDeactivateDescr")]
		public event EventHandler MenuDeactivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.EventMenuDeactivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.EventMenuDeactivate, value);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000D90AA File Offset: 0x000D72AA
		// (set) Token: 0x06003032 RID: 12338 RVA: 0x000D90B2 File Offset: 0x000D72B2
		[DefaultValue(false)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000D90BB File Offset: 0x000D72BB
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x000D90C3 File Offset: 0x000D72C3
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripStretchDescr")]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000D90CC File Offset: 0x000D72CC
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x000D90D4 File Offset: 0x000D72D4
		[DefaultValue(null)]
		[MergableProperty(false)]
		[SRDescription("MenuStripMdiWindowListItem")]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(MdiWindowListItemConverter))]
		public ToolStripMenuItem MdiWindowListItem
		{
			get
			{
				return this.mdiWindowListItem;
			}
			set
			{
				this.mdiWindowListItem = value;
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000D90DD File Offset: 0x000D72DD
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new MenuStrip.MenuStripAccessibleObject(this);
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000D90E5 File Offset: 0x000D72E5
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			return new ToolStripMenuItem(text, image, onClick);
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x000D9104 File Offset: 0x000D7304
		internal override ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction, bool rtlAware)
		{
			ToolStripItem nextItem = base.GetNextItem(start, direction, rtlAware);
			if (nextItem is MdiControlStrip.SystemMenuItem && AccessibilityImprovements.Level2)
			{
				nextItem = base.GetNextItem(nextItem, direction, rtlAware);
			}
			return nextItem;
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000D9138 File Offset: 0x000D7338
		protected virtual void OnMenuActivate(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemMenuStart, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.EventMenuActivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000D9178 File Offset: 0x000D7378
		protected virtual void OnMenuDeactivate(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemMenuEnd, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.EventMenuDeactivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000D91B8 File Offset: 0x000D73B8
		internal bool OnMenuKey()
		{
			if (!this.Focused && !base.ContainsFocus)
			{
				ToolStripManager.ModalMenuFilter.SetActiveToolStrip(this, true);
				if (this.DisplayedItems.Count > 0)
				{
					if (this.DisplayedItems[0] is MdiControlStrip.SystemMenuItem)
					{
						base.SelectNextToolStripItem(this.DisplayedItems[0], true);
					}
					else
					{
						base.SelectNextToolStripItem(null, this.RightToLeft == RightToLeft.No);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000D9228 File Offset: 0x000D7428
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (ToolStripManager.ModalMenuFilter.InMenuMode && keyData == Keys.Space && (this.Focused || !base.ContainsFocus))
			{
				base.NotifySelectionChange(null);
				ToolStripManager.ModalMenuFilter.ExitMenuMode();
				UnsafeNativeMethods.PostMessage(WindowsFormsUtils.GetRootHWnd(this), 274, 61696, 32);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000D9280 File Offset: 0x000D7480
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 33 && base.ActiveDropDowns.Count == 0)
			{
				Point point = base.PointToClient(WindowsFormsUtils.LastCursorPoint);
				ToolStripItem itemAt = base.GetItemAt(point);
				if (itemAt != null && !(itemAt is ToolStripControlHost))
				{
					this.KeyboardActive = true;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040013E1 RID: 5089
		private ToolStripMenuItem mdiWindowListItem;

		// Token: 0x040013E2 RID: 5090
		private static readonly object EventMenuActivate = new object();

		// Token: 0x040013E3 RID: 5091
		private static readonly object EventMenuDeactivate = new object();

		// Token: 0x020006DD RID: 1757
		[ComVisible(true)]
		internal class MenuStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006B27 RID: 27431 RVA: 0x0018CF24 File Offset: 0x0018B124
			public MenuStripAccessibleObject(MenuStrip owner) : base(owner)
			{
			}

			// Token: 0x1700173D RID: 5949
			// (get) Token: 0x06006B28 RID: 27432 RVA: 0x0018CF30 File Offset: 0x0018B130
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.MenuBar;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.MenuBar;
				}
			}

			// Token: 0x06006B29 RID: 27433 RVA: 0x0018CF5A File Offset: 0x0018B15A
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50010;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
