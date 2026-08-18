using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001B7 RID: 439
	[ToolboxItem(false)]
	[ProvideProperty("Location", typeof(IComponent))]
	[ProvideProperty("TrayLocation", typeof(IComponent))]
	[DesignTimeVisible(false)]
	public class ComponentTray : ScrollableControl, IExtenderProvider, ISelectionUIHandler, IOleDragClient
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0004FD70 File Offset: 0x0004ED70
		public ComponentTray(IDesigner mainDesigner, IServiceProvider serviceProvider)
		{
			this.AutoScroll = true;
			this.mainDesigner = mainDesigner;
			this.serviceProvider = serviceProvider;
			this.AllowDrop = true;
			this.Text = "ComponentTray";
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			this.controls = new ArrayList();
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			IExtenderProviderService extenderProviderService = (IExtenderProviderService)this.GetService(typeof(IExtenderProviderService));
			if (extenderProviderService != null)
			{
				extenderProviderService.AddExtenderProvider(this);
			}
			if (this.GetService(typeof(IEventHandlerService)) == null && designerHost != null)
			{
				this.eventHandlerService = new EventHandlerService(this);
				designerHost.AddService(typeof(IEventHandlerService), this.eventHandlerService);
			}
			IMenuCommandService menuService = this.MenuService;
			if (menuService != null)
			{
				this.menucmdArrangeIcons = new MenuCommand(new EventHandler(this.OnMenuArrangeIcons), StandardCommands.ArrangeIcons);
				this.menucmdLineupIcons = new MenuCommand(new EventHandler(this.OnMenuLineupIcons), StandardCommands.LineupIcons);
				this.menucmdLargeIcons = new MenuCommand(new EventHandler(this.OnMenuShowLargeIcons), StandardCommands.ShowLargeIcons);
				this.menucmdArrangeIcons.Checked = this.AutoArrange;
				this.menucmdLargeIcons.Checked = this.ShowLargeIcons;
				menuService.AddCommand(this.menucmdArrangeIcons);
				menuService.AddCommand(this.menucmdLineupIcons);
				menuService.AddCommand(this.menucmdLargeIcons);
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				Color backColor;
				if (iuiservice.Styles["VsColorDesignerTray"] is Color)
				{
					backColor = (Color)iuiservice.Styles["VsColorDesignerTray"];
				}
				else if (iuiservice.Styles["HighlightColor"] is Color)
				{
					backColor = (Color)iuiservice.Styles["HighlightColor"];
				}
				else
				{
					backColor = SystemColors.Info;
				}
				this.BackColor = backColor;
				this.Font = (Font)iuiservice.Styles["DialogFont"];
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged += this.OnSelectionChanged;
			}
			SystemEvents.DisplaySettingsChanged += this.OnSystemSettingChanged;
			SystemEvents.InstalledFontsChanged += this.OnSystemSettingChanged;
			SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
			TypeDescriptor.Refreshed += this.OnComponentRefresh;
			BehaviorService behaviorService = this.GetService(typeof(BehaviorService)) as BehaviorService;
			if (behaviorService != null)
			{
				this.glyphManager = new ComponentTray.ComponentTrayGlyphManager(selectionService, behaviorService);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00050096 File Offset: 0x0004F096
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x0005009E File Offset: 0x0004F09E
		public bool AutoArrange
		{
			get
			{
				return this.autoArrange;
			}
			set
			{
				if (this.autoArrange != value)
				{
					this.autoArrange = value;
					this.menucmdArrangeIcons.Checked = value;
					if (this.autoArrange)
					{
						this.DoAutoArrange(true);
					}
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000500CB File Offset: 0x0004F0CB
		public int ComponentCount
		{
			get
			{
				return base.Controls.Count;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x000500D8 File Offset: 0x0004F0D8
		internal virtual SelectionUIHandler DragHandler
		{
			get
			{
				if (this.dragHandler == null)
				{
					this.dragHandler = new ComponentTray.TraySelectionUIHandler(this);
				}
				return this.dragHandler;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x000500F4 File Offset: 0x0004F0F4
		internal GlyphCollection SelectionGlyphs
		{
			get
			{
				if (this.glyphManager != null)
				{
					return this.glyphManager.SelectionGlyphs;
				}
				return null;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0005010B File Offset: 0x0004F10B
		private InheritanceUI InheritanceUI
		{
			get
			{
				if (this.inheritanceUI == null)
				{
					this.inheritanceUI = new InheritanceUI();
				}
				return this.inheritanceUI;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00050126 File Offset: 0x0004F126
		private IMenuCommandService MenuService
		{
			get
			{
				if (this.menuCommandService == null)
				{
					this.menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				}
				return this.menuCommandService;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00050151 File Offset: 0x0004F151
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00050159 File Offset: 0x0004F159
		public bool ShowLargeIcons
		{
			get
			{
				return this.showLargeIcons;
			}
			set
			{
				if (this.showLargeIcons != value)
				{
					this.showLargeIcons = value;
					this.menucmdLargeIcons.Checked = this.ShowLargeIcons;
					this.ResetTrayControls();
					base.Invalidate(true);
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0005018C File Offset: 0x0004F18C
		private bool TabOrderActive
		{
			get
			{
				if (!this.queriedTabOrder)
				{
					this.queriedTabOrder = true;
					IMenuCommandService menuService = this.MenuService;
					if (menuService != null)
					{
						this.tabOrderCommand = menuService.FindCommand(StandardCommands.TabOrder);
					}
				}
				return this.tabOrderCommand != null && this.tabOrderCommand.Checked;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000501D8 File Offset: 0x0004F1D8
		internal bool IsWindowVisible
		{
			get
			{
				return base.IsHandleCreated && NativeMethods.IsWindowVisible(base.Handle);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000501F0 File Offset: 0x0004F1F0
		internal Size ParentGridSize
		{
			get
			{
				ParentControlDesigner parentControlDesigner = this.mainDesigner as ParentControlDesigner;
				if (parentControlDesigner != null)
				{
					return parentControlDesigner.ParentGridSize;
				}
				return new Size(8, 8);
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0005021C File Offset: 0x0004F21C
		public virtual void AddComponent(IComponent component)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!this.CanDisplayComponent(component))
			{
				return;
			}
			if (this.selectionUISvc == null)
			{
				this.selectionUISvc = (ISelectionUIService)this.GetService(typeof(ISelectionUIService));
				if (this.selectionUISvc == null)
				{
					this.selectionUISvc = new SelectionUIService(designerHost);
					designerHost.AddService(typeof(ISelectionUIService), this.selectionUISvc);
				}
				this.grabHandle = this.selectionUISvc.GetAdornmentDimensions(AdornmentType.GrabHandle);
			}
			ComponentTray.TrayControl trayControl = new ComponentTray.TrayControl(this, component);
			base.SuspendLayout();
			try
			{
				base.Controls.Add(trayControl);
				this.controls.Add(trayControl);
				TypeDescriptor.Refresh(component);
				if (designerHost != null && !designerHost.Loading)
				{
					this.PositionControl(trayControl);
				}
				if (this.selectionUISvc != null)
				{
					this.selectionUISvc.AssignSelectionUIHandler(component, this);
				}
				InheritanceAttribute inheritanceAttribute = trayControl.InheritanceAttribute;
				if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
				{
					InheritanceUI inheritanceUI = this.InheritanceUI;
					if (inheritanceUI != null)
					{
						inheritanceUI.AddInheritedControl(trayControl, inheritanceAttribute.InheritanceLevel);
					}
				}
			}
			finally
			{
				base.ResumeLayout();
			}
			if (designerHost != null && !designerHost.Loading)
			{
				base.ScrollControlIntoView(trayControl);
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00050350 File Offset: 0x0004F350
		bool IExtenderProvider.CanExtend(object component)
		{
			IComponent component2 = component as IComponent;
			return component2 != null && ComponentTray.TrayControl.FromComponent(component2) != null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00050378 File Offset: 0x0004F378
		protected virtual bool CanCreateComponentFromTool(ToolboxItem tool)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			Type type = designerHost.GetType(tool.TypeName);
			if (type == null)
			{
				return true;
			}
			if (!type.IsSubclassOf(typeof(Control)))
			{
				return true;
			}
			Type designerType = this.GetDesignerType(type, typeof(IDesigner));
			return !typeof(ControlDesigner).IsAssignableFrom(designerType);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000503E8 File Offset: 0x0004F3E8
		protected virtual bool CanDisplayComponent(IComponent component)
		{
			return TypeDescriptor.GetAttributes(component).Contains(DesignTimeVisibleAttribute.Yes);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000503FC File Offset: 0x0004F3FC
		public void CreateComponentFromTool(ToolboxItem tool)
		{
			if (!this.CanCreateComponentFromTool(tool))
			{
				return;
			}
			this.GetOleDragHandler().CreateTool(tool, null, 0, 0, 0, 0, false, false);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00050428 File Offset: 0x0004F428
		protected void DisplayError(Exception e)
		{
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				iuiservice.ShowError(e);
				return;
			}
			string text = e.Message;
			if (text == null || text.Length == 0)
			{
				text = e.ToString();
			}
			RTLAwareMessageBox.Show(null, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00050480 File Offset: 0x0004F480
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.controls != null)
			{
				IExtenderProviderService extenderProviderService = (IExtenderProviderService)this.GetService(typeof(IExtenderProviderService));
				bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
				if (extenderProviderService != null)
				{
					extenderProviderService.RemoveExtenderProvider(this);
				}
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (this.eventHandlerService != null && designerHost != null)
				{
					designerHost.RemoveService(typeof(IEventHandlerService));
					this.eventHandlerService = null;
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				}
				TypeDescriptor.Refreshed -= this.OnComponentRefresh;
				SystemEvents.DisplaySettingsChanged -= this.OnSystemSettingChanged;
				SystemEvents.InstalledFontsChanged -= this.OnSystemSettingChanged;
				SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
				IMenuCommandService menuService = this.MenuService;
				if (menuService != null)
				{
					menuService.RemoveCommand(this.menucmdArrangeIcons);
					menuService.RemoveCommand(this.menucmdLineupIcons);
					menuService.RemoveCommand(this.menucmdLargeIcons);
				}
				if (this.privateCommandSet != null)
				{
					this.privateCommandSet.Dispose();
					if (designerHost != null)
					{
						designerHost.RemoveService(typeof(ISelectionUIService));
					}
				}
				this.selectionUISvc = null;
				if (this.inheritanceUI != null)
				{
					this.inheritanceUI.Dispose();
					this.inheritanceUI = null;
				}
				this.serviceProvider = null;
				this.controls.Clear();
				this.controls = null;
				if (this.glyphManager != null)
				{
					this.glyphManager.Dispose();
					this.glyphManager = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00050620 File Offset: 0x0004F620
		private void DoAutoArrange(bool dirtyDesigner)
		{
			if (this.controls == null || this.controls.Count <= 0)
			{
				return;
			}
			this.controls.Sort(new ComponentTray.AutoArrangeComparer());
			base.SuspendLayout();
			base.AutoScrollPosition = new Point(0, 0);
			try
			{
				Control prevCtl = null;
				bool flag = true;
				foreach (object obj in this.controls)
				{
					Control control = (Control)obj;
					if (control.Visible)
					{
						if (this.autoArrange)
						{
							this.PositionInNextAutoSlot(control as ComponentTray.TrayControl, prevCtl, dirtyDesigner);
						}
						else if (!((ComponentTray.TrayControl)control).Positioned || !flag)
						{
							this.PositionInNextAutoSlot(control as ComponentTray.TrayControl, prevCtl, false);
							flag = false;
						}
						prevCtl = control;
					}
				}
				if (this.selectionUISvc != null)
				{
					this.selectionUISvc.SyncSelection();
				}
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00050720 File Offset: 0x0004F720
		private void DoLineupIcons()
		{
			if (this.autoArrange)
			{
				return;
			}
			bool flag = this.autoArrange;
			this.autoArrange = true;
			try
			{
				this.DoAutoArrange(true);
			}
			finally
			{
				this.autoArrange = flag;
			}
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00050768 File Offset: 0x0004F768
		private void DrawRubber(Point start, Point end)
		{
			this.mouseDragWorkspace.X = Math.Min(start.X, end.X);
			this.mouseDragWorkspace.Y = Math.Min(start.Y, end.Y);
			this.mouseDragWorkspace.Width = Math.Abs(end.X - start.X);
			this.mouseDragWorkspace.Height = Math.Abs(end.Y - start.Y);
			this.mouseDragWorkspace = base.RectangleToScreen(this.mouseDragWorkspace);
			ControlPaint.DrawReversibleFrame(this.mouseDragWorkspace, this.BackColor, FrameStyle.Dashed);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00050814 File Offset: 0x0004F814
		internal void FocusDesigner()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && designerHost.RootComponent != null)
			{
				IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
				if (rootDesigner != null)
				{
					ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
					if (supportedTechnologies.Length > 0)
					{
						Control control = rootDesigner.GetView(supportedTechnologies[0]) as Control;
						if (control != null)
						{
							control.Focus();
						}
					}
				}
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00050880 File Offset: 0x0004F880
		private object[] GetComponentsInRect(Rectangle rect)
		{
			ArrayList arrayList = new ArrayList();
			int count = base.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = base.Controls[i];
				Rectangle bounds = control.Bounds;
				ComponentTray.TrayControl trayControl = control as ComponentTray.TrayControl;
				if (trayControl != null && bounds.IntersectsWith(rect))
				{
					arrayList.Add(trayControl.Component);
				}
			}
			return arrayList.ToArray();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000508EC File Offset: 0x0004F8EC
		private Type GetDesignerType(Type t, Type designerBaseType)
		{
			Type type = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(t);
			for (int i = 0; i < attributes.Count; i++)
			{
				DesignerAttribute designerAttribute = attributes[i] as DesignerAttribute;
				if (designerAttribute != null)
				{
					Type type2 = Type.GetType(designerAttribute.DesignerBaseTypeName);
					if (type2 != null && type2 == designerBaseType)
					{
						bool flag = false;
						ITypeResolutionService typeResolutionService = (ITypeResolutionService)this.GetService(typeof(ITypeResolutionService));
						if (typeResolutionService != null)
						{
							flag = true;
							type = typeResolutionService.GetType(designerAttribute.DesignerTypeName);
						}
						if (!flag)
						{
							type = Type.GetType(designerAttribute.DesignerTypeName);
						}
						if (type != null)
						{
							break;
						}
					}
				}
			}
			return type;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0005097C File Offset: 0x0004F97C
		internal Size GetDragDimensions()
		{
			if (this.AutoArrange)
			{
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				IComponent component = null;
				if (selectionService != null)
				{
					component = (IComponent)selectionService.PrimarySelection;
				}
				Control control = null;
				if (component != null)
				{
					control = ((IOleDragClient)this).GetControlForComponent(component);
				}
				if (control == null && this.controls.Count > 0)
				{
					control = (Control)this.controls[0];
				}
				if (control != null)
				{
					Size size = control.Size;
					size.Width += 2 * this.whiteSpace.X;
					size.Height += 2 * this.whiteSpace.Y;
					return size;
				}
			}
			return new Size(10, 10);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00050A38 File Offset: 0x0004FA38
		public IComponent GetNextComponent(IComponent component, bool forward)
		{
			int i = 0;
			while (i < this.controls.Count)
			{
				ComponentTray.TrayControl trayControl = (ComponentTray.TrayControl)this.controls[i];
				if (trayControl.Component == component)
				{
					int num = forward ? (i + 1) : (i - 1);
					if (num >= 0 && num < this.controls.Count)
					{
						return ((ComponentTray.TrayControl)this.controls[num]).Component;
					}
					return null;
				}
				else
				{
					i++;
				}
			}
			if (this.controls.Count > 0)
			{
				int index = forward ? 0 : (this.controls.Count - 1);
				return ((ComponentTray.TrayControl)this.controls[index]).Component;
			}
			return null;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00050AE7 File Offset: 0x0004FAE7
		internal virtual OleDragDropHandler GetOleDragHandler()
		{
			if (this.oleDragDropHandler == null)
			{
				this.oleDragDropHandler = new ComponentTray.TrayOleDragDropHandler(this.DragHandler, this.serviceProvider, this);
			}
			return this.oleDragDropHandler;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00050B10 File Offset: 0x0004FB10
		[DesignOnly(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlLocationDescr")]
		[Category("Layout")]
		[Browsable(false)]
		[Localizable(false)]
		public Point GetLocation(IComponent receiver)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(receiver.GetType())["Location"];
			if (propertyDescriptor != null)
			{
				return (Point)propertyDescriptor.GetValue(receiver);
			}
			return this.GetTrayLocation(receiver);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00050B4C File Offset: 0x0004FB4C
		[SRDescription("ControlLocationDescr")]
		[DesignOnly(true)]
		[Category("Layout")]
		[Localizable(false)]
		[Browsable(false)]
		public Point GetTrayLocation(IComponent receiver)
		{
			Control control = ComponentTray.TrayControl.FromComponent(receiver);
			if (control == null)
			{
				return default(Point);
			}
			Point location = control.Location;
			Point autoScrollPosition = base.AutoScrollPosition;
			return new Point(location.X - autoScrollPosition.X, location.Y - autoScrollPosition.Y);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00050BA0 File Offset: 0x0004FBA0
		protected override object GetService(Type serviceType)
		{
			object result = null;
			if (this.serviceProvider != null)
			{
				result = this.serviceProvider.GetService(serviceType);
			}
			return result;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00050BC5 File Offset: 0x0004FBC5
		internal ComponentTray.TrayControl GetTrayControlFromComponent(IComponent comp)
		{
			return ComponentTray.TrayControl.FromComponent(comp);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00050BD0 File Offset: 0x0004FBD0
		public bool IsTrayComponent(IComponent comp)
		{
			if (ComponentTray.TrayControl.FromComponent(comp) == null)
			{
				return false;
			}
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				ComponentTray.TrayControl trayControl = control as ComponentTray.TrayControl;
				if (trayControl != null && trayControl.Component == comp)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00050C4C File Offset: 0x0004FC4C
		private void OnComponentRefresh(RefreshEventArgs e)
		{
			IComponent component = e.ComponentChanged as IComponent;
			if (component != null)
			{
				ComponentTray.TrayControl trayControl = ComponentTray.TrayControl.FromComponent(component);
				if (trayControl != null)
				{
					bool flag = this.CanDisplayComponent(component);
					if (flag != trayControl.Visible || !flag)
					{
						trayControl.Visible = flag;
						Rectangle bounds = trayControl.Bounds;
						bounds.Inflate(this.grabHandle);
						bounds.Inflate(this.grabHandle);
						base.Invalidate(bounds);
						base.PerformLayout();
					}
				}
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00050CBB File Offset: 0x0004FCBB
		private void OnComponentRemoved(object sender, ComponentEventArgs cevent)
		{
			this.RemoveComponent(cevent.Component);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00050CCC File Offset: 0x0004FCCC
		internal void UpdatePastePositions(ArrayList components)
		{
			foreach (object obj in components)
			{
				ComponentTray.TrayControl trayControl = (ComponentTray.TrayControl)obj;
				if (!this.CanDisplayComponent(trayControl.Component))
				{
					break;
				}
				if (this.mouseDropLocation == ComponentTray.InvalidPoint)
				{
					Control prevCtl = null;
					if (this.controls.Count > 1)
					{
						prevCtl = (Control)this.controls[this.controls.Count - 1];
					}
					this.PositionInNextAutoSlot(trayControl, prevCtl, true);
				}
				else
				{
					this.PositionControl(trayControl);
				}
				trayControl.BringToFront();
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00050D84 File Offset: 0x0004FD84
		private void OnContextMenu(int x, int y, bool useSelection)
		{
			if (!this.TabOrderActive)
			{
				base.Capture = false;
				IMenuCommandService menuService = this.MenuService;
				if (menuService != null)
				{
					base.Capture = false;
					Cursor.Clip = Rectangle.Empty;
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					if (useSelection && selectionService != null && (1 != selectionService.SelectionCount || selectionService.PrimarySelection != this.mainDesigner.Component))
					{
						menuService.ShowContextMenu(MenuCommands.TraySelectionMenu, x, y);
						return;
					}
					menuService.ShowContextMenu(MenuCommands.ComponentTrayMenu, x, y);
				}
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00050E10 File Offset: 0x0004FE10
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			if (this.glyphManager != null && this.glyphManager.OnMouseDoubleClick(e))
			{
				return;
			}
			base.OnDoubleClick(e);
			if (!this.TabOrderActive)
			{
				this.OnLostCapture();
				IEventBindingService eventBindingService = (IEventBindingService)this.GetService(typeof(IEventBindingService));
				bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
				if (eventBindingService != null)
				{
					eventBindingService.ShowCode();
				}
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00050E74 File Offset: 0x0004FE74
		protected override void OnGiveFeedback(GiveFeedbackEventArgs gfevent)
		{
			base.OnGiveFeedback(gfevent);
			this.GetOleDragHandler().DoOleGiveFeedback(gfevent);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00050E8C File Offset: 0x0004FE8C
		protected override void OnDragDrop(DragEventArgs de)
		{
			this.mouseDropLocation = base.PointToClient(new Point(de.X, de.Y));
			this.autoScrollPosBeforeDragging = base.AutoScrollPosition;
			if (this.mouseDragTool != null)
			{
				ToolboxItem tool = this.mouseDragTool;
				this.mouseDragTool = null;
				bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
				try
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					IDesigner designer = designerHost.GetDesigner(designerHost.RootComponent);
					IToolboxUser toolboxUser = designer as IToolboxUser;
					if (toolboxUser != null)
					{
						toolboxUser.ToolPicked(tool);
					}
					else
					{
						this.CreateComponentFromTool(tool);
					}
				}
				catch (Exception ex)
				{
					this.DisplayError(ex);
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				catch
				{
				}
				de.Effect = DragDropEffects.Copy;
			}
			else
			{
				this.GetOleDragHandler().DoOleDragDrop(de);
			}
			this.mouseDropLocation = ComponentTray.InvalidPoint;
			base.ResumeLayout();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00050F80 File Offset: 0x0004FF80
		protected override void OnDragEnter(DragEventArgs de)
		{
			if (!this.TabOrderActive)
			{
				base.SuspendLayout();
				if (this.toolboxService == null)
				{
					this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				}
				OleDragDropHandler oleDragHandler = this.GetOleDragHandler();
				object[] draggingObjects = oleDragHandler.GetDraggingObjects(de);
				if (this.toolboxService != null && draggingObjects == null)
				{
					this.mouseDragTool = this.toolboxService.DeserializeToolboxItem(de.Data, (IDesignerHost)this.GetService(typeof(IDesignerHost)));
				}
				if (this.mouseDragTool != null)
				{
					if ((de.AllowedEffect & DragDropEffects.Move) != DragDropEffects.None)
					{
						de.Effect = DragDropEffects.Move;
						return;
					}
					de.Effect = DragDropEffects.Copy;
					return;
				}
				else
				{
					oleDragHandler.DoOleDragEnter(de);
				}
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00051030 File Offset: 0x00050030
		protected override void OnDragLeave(EventArgs e)
		{
			this.mouseDragTool = null;
			this.GetOleDragHandler().DoOleDragLeave();
			base.ResumeLayout();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0005104A File Offset: 0x0005004A
		protected override void OnDragOver(DragEventArgs de)
		{
			if (this.mouseDragTool != null)
			{
				de.Effect = DragDropEffects.Copy;
				return;
			}
			this.GetOleDragHandler().DoOleDragOver(de);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00051068 File Offset: 0x00050068
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.DoAutoArrange(false);
			base.Invalidate(true);
			base.OnLayout(levent);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00051080 File Offset: 0x00050080
		protected virtual void OnLostCapture()
		{
			if (this.mouseDragStart != ComponentTray.InvalidPoint)
			{
				Cursor.Clip = Rectangle.Empty;
				if (this.mouseDragEnd != ComponentTray.InvalidPoint)
				{
					this.DrawRubber(this.mouseDragStart, this.mouseDragEnd);
					this.mouseDragEnd = ComponentTray.InvalidPoint;
				}
				this.mouseDragStart = ComponentTray.InvalidPoint;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000510E4 File Offset: 0x000500E4
		private void OnMenuArrangeIcons(object sender, EventArgs e)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			try
			{
				designerTransaction = designerHost.CreateTransaction(SR.GetString("TrayAutoArrange"));
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.mainDesigner.Component)["TrayAutoArrange"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(this.mainDesigner.Component, !this.AutoArrange);
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00051174 File Offset: 0x00050174
		private void OnMenuShowLargeIcons(object sender, EventArgs e)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			try
			{
				designerTransaction = designerHost.CreateTransaction(SR.GetString("TrayShowLargeIcons"));
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.mainDesigner.Component)["TrayLargeIcon"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(this.mainDesigner.Component, !this.ShowLargeIcons);
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00051204 File Offset: 0x00050204
		private void OnMenuLineupIcons(object sender, EventArgs e)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			try
			{
				designerTransaction = designerHost.CreateTransaction(SR.GetString("TrayLineUpIcons"));
				this.DoLineupIcons();
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0005125C File Offset: 0x0005025C
		internal void OnMessage(ref Message m)
		{
			this.WndProc(ref m);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00051268 File Offset: 0x00050268
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.glyphManager != null && this.glyphManager.OnMouseDown(e))
			{
				return;
			}
			base.OnMouseDown(e);
			if (!this.TabOrderActive)
			{
				if (this.toolboxService == null)
				{
					this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				}
				this.FocusDesigner();
				if (e.Button == MouseButtons.Left && this.toolboxService != null)
				{
					ToolboxItem selectedToolboxItem = this.toolboxService.GetSelectedToolboxItem((IDesignerHost)this.GetService(typeof(IDesignerHost)));
					if (selectedToolboxItem != null)
					{
						this.mouseDropLocation = new Point(e.X, e.Y);
						try
						{
							this.CreateComponentFromTool(selectedToolboxItem);
							this.toolboxService.SelectedToolboxItemUsed();
						}
						catch (Exception ex)
						{
							this.DisplayError(ex);
							if (ClientUtils.IsCriticalException(ex))
							{
								throw;
							}
						}
						catch
						{
						}
						this.mouseDropLocation = ComponentTray.InvalidPoint;
						return;
					}
				}
				if (e.Button == MouseButtons.Left)
				{
					this.mouseDragStart = new Point(e.X, e.Y);
					base.Capture = true;
					Cursor.Clip = base.RectangleToScreen(base.ClientRectangle);
					return;
				}
				try
				{
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
					if (selectionService != null)
					{
						selectionService.SetSelectedComponents(new object[]
						{
							this.mainDesigner.Component
						});
					}
				}
				catch (Exception ex2)
				{
					if (ClientUtils.IsCriticalException(ex2))
					{
						throw;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00051414 File Offset: 0x00050414
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.glyphManager != null && this.glyphManager.OnMouseMove(e))
			{
				return;
			}
			base.OnMouseMove(e);
			if (this.mouseDragStart != ComponentTray.InvalidPoint)
			{
				if (this.mouseDragEnd != ComponentTray.InvalidPoint)
				{
					this.DrawRubber(this.mouseDragStart, this.mouseDragEnd);
				}
				else
				{
					this.mouseDragEnd = new Point(0, 0);
				}
				this.mouseDragEnd.X = e.X;
				this.mouseDragEnd.Y = e.Y;
				this.DrawRubber(this.mouseDragStart, this.mouseDragEnd);
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000514B8 File Offset: 0x000504B8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.glyphManager != null && this.glyphManager.OnMouseUp(e))
			{
				return;
			}
			if (this.mouseDragStart != ComponentTray.InvalidPoint && e.Button == MouseButtons.Left)
			{
				base.Capture = false;
				Cursor.Clip = Rectangle.Empty;
				object[] array;
				if (this.mouseDragEnd != ComponentTray.InvalidPoint)
				{
					this.DrawRubber(this.mouseDragStart, this.mouseDragEnd);
					array = this.GetComponentsInRect(new Rectangle
					{
						X = Math.Min(this.mouseDragStart.X, e.X),
						Y = Math.Min(this.mouseDragStart.Y, e.Y),
						Width = Math.Abs(e.X - this.mouseDragStart.X),
						Height = Math.Abs(e.Y - this.mouseDragStart.Y)
					});
					this.mouseDragEnd = ComponentTray.InvalidPoint;
				}
				else
				{
					array = new object[0];
				}
				if (array.Length == 0)
				{
					array = new object[]
					{
						this.mainDesigner.Component
					};
				}
				try
				{
					ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
					bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
					if (selectionService != null)
					{
						selectionService.SetSelectedComponents(array);
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				catch
				{
				}
				this.mouseDragStart = ComponentTray.InvalidPoint;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00051660 File Offset: 0x00050660
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (this.fResetAmbient)
			{
				this.fResetAmbient = false;
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					Color backColor;
					if (iuiservice.Styles["VsColorDesignerTray"] is Color)
					{
						backColor = (Color)iuiservice.Styles["VsColorDesignerTray"];
					}
					else if (iuiservice.Styles["HighlightColor"] is Color)
					{
						backColor = (Color)iuiservice.Styles["HighlightColor"];
					}
					else
					{
						backColor = SystemColors.Info;
					}
					this.BackColor = backColor;
					this.Font = (Font)iuiservice.Styles["DialogFont"];
				}
			}
			base.OnPaint(pe);
			Graphics graphics = pe.Graphics;
			if (this.selectedObjects != null)
			{
				bool flag = true;
				foreach (object component in this.selectedObjects)
				{
					Control controlForComponent = ((IOleDragClient)this).GetControlForComponent(component);
					if (controlForComponent != null && controlForComponent.Visible)
					{
						Rectangle bounds = controlForComponent.Bounds;
						NoResizeHandleGlyph noResizeHandleGlyph = new NoResizeHandleGlyph(bounds, SelectionRules.None, flag, null);
						DesignerUtils.DrawSelectionBorder(graphics, DesignerUtils.GetBoundsForNoResizeSelectionType(bounds, SelectionBorderGlyphType.Top));
						DesignerUtils.DrawSelectionBorder(graphics, DesignerUtils.GetBoundsForNoResizeSelectionType(bounds, SelectionBorderGlyphType.Bottom));
						DesignerUtils.DrawSelectionBorder(graphics, DesignerUtils.GetBoundsForNoResizeSelectionType(bounds, SelectionBorderGlyphType.Left));
						DesignerUtils.DrawSelectionBorder(graphics, DesignerUtils.GetBoundsForNoResizeSelectionType(bounds, SelectionBorderGlyphType.Right));
						DesignerUtils.DrawNoResizeHandle(graphics, noResizeHandleGlyph.Bounds, flag, noResizeHandleGlyph);
					}
					flag = false;
				}
			}
			if (this.glyphManager != null)
			{
				this.glyphManager.OnPaintGlyphs(pe);
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00051814 File Offset: 0x00050814
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			this.selectedObjects = ((ISelectionService)sender).GetSelectedComponents();
			object primarySelection = ((ISelectionService)sender).PrimarySelection;
			base.Invalidate();
			foreach (object obj in this.selectedObjects)
			{
				IComponent component = obj as IComponent;
				if (component != null)
				{
					Control control = ComponentTray.TrayControl.FromComponent(component);
					if (control != null)
					{
						UnsafeNativeMethods.NotifyWinEvent(32775, new HandleRef(control, control.Handle), -4, 0);
					}
				}
			}
			IComponent component2 = primarySelection as IComponent;
			if (component2 != null)
			{
				Control control2 = ComponentTray.TrayControl.FromComponent(component2);
				if (control2 != null && base.IsHandleCreated)
				{
					base.ScrollControlIntoView(control2);
					UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(control2, control2.Handle), -4, 0);
				}
				if (this.glyphManager != null)
				{
					this.glyphManager.SelectionGlyphs.Clear();
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					foreach (object obj2 in this.selectedObjects)
					{
						IComponent component3 = obj2 as IComponent;
						if (component3 != null && !(designerHost.GetDesigner(component3) is ControlDesigner))
						{
							GlyphCollection glyphsForComponent = this.glyphManager.GetGlyphsForComponent(component3);
							if (glyphsForComponent != null && glyphsForComponent.Count > 0)
							{
								this.SelectionGlyphs.AddRange(glyphsForComponent);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000519BC File Offset: 0x000509BC
		protected virtual void OnSetCursor()
		{
			if (this.toolboxService == null)
			{
				this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			}
			if (this.toolboxService == null || !this.toolboxService.SetCursor())
			{
				Cursor.Current = Cursors.Default;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00051A0C File Offset: 0x00050A0C
		private void OnSystemSettingChanged(object sender, EventArgs e)
		{
			this.fResetAmbient = true;
			this.ResetTrayControls();
			base.BeginInvoke(new ComponentTray.AsyncInvokeHandler(base.Invalidate), new object[]
			{
				true
			});
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00051A4C File Offset: 0x00050A4C
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			this.fResetAmbient = true;
			this.ResetTrayControls();
			base.BeginInvoke(new ComponentTray.AsyncInvokeHandler(base.Invalidate), new object[]
			{
				true
			});
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00051A8C File Offset: 0x00050A8C
		private void PositionControl(ComponentTray.TrayControl c)
		{
			if (!this.autoArrange)
			{
				if (!(this.mouseDropLocation != ComponentTray.InvalidPoint))
				{
					Control prevCtl = null;
					if (this.controls.Count > 1)
					{
						int num = this.controls.IndexOf(c);
						if (num >= 1)
						{
							prevCtl = (Control)this.controls[num - 1];
						}
					}
					this.PositionInNextAutoSlot(c, prevCtl, true);
					return;
				}
				if (!c.Location.Equals(this.mouseDropLocation))
				{
					c.Location = this.mouseDropLocation;
					return;
				}
			}
			else
			{
				if (this.mouseDropLocation != ComponentTray.InvalidPoint)
				{
					this.RearrangeInAutoSlots(c, this.mouseDropLocation);
					return;
				}
				Control prevCtl2 = null;
				if (this.controls.Count > 1)
				{
					int num2 = this.controls.IndexOf(c);
					if (num2 >= 1)
					{
						prevCtl2 = (Control)this.controls[num2 - 1];
					}
				}
				this.PositionInNextAutoSlot(c, prevCtl2, true);
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00051B88 File Offset: 0x00050B88
		private bool PositionInNextAutoSlot(ComponentTray.TrayControl c, Control prevCtl, bool dirtyDesigner)
		{
			if (this.whiteSpace.IsEmpty)
			{
				this.whiteSpace = new Point(this.selectionUISvc.GetAdornmentDimensions(AdornmentType.GrabHandle));
				this.whiteSpace.X = this.whiteSpace.X * 2 + 3;
				this.whiteSpace.Y = this.whiteSpace.Y * 2 + 3;
			}
			if (prevCtl == null)
			{
				Rectangle displayRectangle = this.DisplayRectangle;
				Point point = new Point(displayRectangle.X + this.whiteSpace.X, displayRectangle.Y + this.whiteSpace.Y);
				if (!c.Location.Equals(point))
				{
					c.Location = point;
					if (dirtyDesigner)
					{
						IComponent component = c.Component;
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["TrayLocation"];
						if (propertyDescriptor != null)
						{
							Point autoScrollPosition = base.AutoScrollPosition;
							point = new Point(point.X - autoScrollPosition.X, point.Y - autoScrollPosition.Y);
							propertyDescriptor.SetValue(component, point);
						}
					}
					else
					{
						c.Location = point;
					}
					return true;
				}
			}
			else
			{
				Rectangle bounds = prevCtl.Bounds;
				Point point2 = new Point(bounds.X + bounds.Width + this.whiteSpace.X, bounds.Y);
				if (point2.X + c.Size.Width > base.Size.Width)
				{
					point2.X = this.whiteSpace.X;
					point2.Y += bounds.Height + this.whiteSpace.Y;
				}
				if (!c.Location.Equals(point2))
				{
					if (dirtyDesigner)
					{
						IComponent component2 = c.Component;
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component2)["TrayLocation"];
						if (propertyDescriptor2 != null)
						{
							Point autoScrollPosition2 = base.AutoScrollPosition;
							point2 = new Point(point2.X - autoScrollPosition2.X, point2.Y - autoScrollPosition2.Y);
							propertyDescriptor2.SetValue(component2, point2);
						}
					}
					else
					{
						c.Location = point2;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00051DC8 File Offset: 0x00050DC8
		public virtual void RemoveComponent(IComponent component)
		{
			ComponentTray.TrayControl trayControl = ComponentTray.TrayControl.FromComponent(component);
			if (trayControl != null)
			{
				try
				{
					InheritanceAttribute inheritanceAttribute = trayControl.InheritanceAttribute;
					if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited && this.inheritanceUI != null)
					{
						this.inheritanceUI.RemoveInheritedControl(trayControl);
					}
					if (this.controls != null)
					{
						int num = this.controls.IndexOf(trayControl);
						if (num != -1)
						{
							this.controls.RemoveAt(num);
						}
					}
				}
				finally
				{
					trayControl.Dispose();
				}
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00051E44 File Offset: 0x00050E44
		private void ResetTrayControls()
		{
			Control.ControlCollection controlCollection = base.Controls;
			if (controlCollection == null)
			{
				return;
			}
			for (int i = 0; i < controlCollection.Count; i++)
			{
				ComponentTray.TrayControl trayControl = controlCollection[i] as ComponentTray.TrayControl;
				if (trayControl != null)
				{
					trayControl.fRecompute = true;
				}
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00051E84 File Offset: 0x00050E84
		public void SetLocation(IComponent receiver, Point location)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && designerHost.Loading)
			{
				this.SetTrayLocation(receiver, location);
				return;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(receiver.GetType())["Location"];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(receiver, location);
				return;
			}
			this.SetTrayLocation(receiver, location);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00051EEC File Offset: 0x00050EEC
		public void SetTrayLocation(IComponent receiver, Point location)
		{
			ComponentTray.TrayControl trayControl = ComponentTray.TrayControl.FromComponent(receiver);
			if (trayControl == null)
			{
				return;
			}
			if (trayControl.Parent == this)
			{
				Point autoScrollPosition = base.AutoScrollPosition;
				location = new Point(location.X + autoScrollPosition.X, location.Y + autoScrollPosition.Y);
				if (trayControl.Visible)
				{
					this.RearrangeInAutoSlots(trayControl, location);
					return;
				}
			}
			else if (!trayControl.Location.Equals(location))
			{
				trayControl.Location = location;
				trayControl.Positioned = true;
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00051F74 File Offset: 0x00050F74
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 125)
			{
				switch (msg)
				{
				case 31:
					this.OnLostCapture();
					return;
				case 32:
					this.OnSetCursor();
					return;
				default:
					switch (msg)
					{
					case 123:
					{
						int num = NativeMethods.Util.SignedLOWORD((int)m.LParam);
						int num2 = NativeMethods.Util.SignedHIWORD((int)m.LParam);
						if (num == -1 && num2 == -1)
						{
							Point mousePosition = Control.MousePosition;
							num = mousePosition.X;
							num2 = mousePosition.Y;
						}
						this.OnContextMenu(num, num2, true);
						return;
					}
					case 125:
						base.Invalidate();
						return;
					}
					break;
				}
			}
			else
			{
				if (msg == 132)
				{
					if (this.glyphManager != null)
					{
						Point p = new Point((int)((short)NativeMethods.Util.LOWORD((int)m.LParam)), (int)((short)NativeMethods.Util.HIWORD((int)m.LParam)));
						NativeMethods.POINT point = new NativeMethods.POINT();
						point.x = 0;
						point.y = 0;
						NativeMethods.MapWindowPoints(IntPtr.Zero, base.Handle, point, 1);
						p.Offset(point.x, point.y);
						this.glyphManager.GetHitTest(p);
					}
					base.WndProc(ref m);
					return;
				}
				switch (msg)
				{
				case 276:
				case 277:
					base.WndProc(ref m);
					if (this.selectionUISvc != null)
					{
						this.selectionUISvc.SyncSelection();
					}
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000520E9 File Offset: 0x000510E9
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x000520EC File Offset: 0x000510EC
		IComponent IOleDragClient.Component
		{
			get
			{
				return this.mainDesigner.Component;
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000520FC File Offset: 0x000510FC
		bool IOleDragClient.AddComponent(IComponent component, string name, bool firstAdd)
		{
			IOleDragClient oleDragClient = this.mainDesigner as IOleDragClient;
			if (oleDragClient != null)
			{
				try
				{
					oleDragClient.AddComponent(component, name, firstAdd);
					this.PositionControl(ComponentTray.TrayControl.FromComponent(component));
					this.mouseDropLocation = ComponentTray.InvalidPoint;
					return true;
				}
				catch
				{
					return false;
				}
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			try
			{
				if (designerHost != null && designerHost.Container != null)
				{
					if (designerHost.Container.Components[name] != null)
					{
						name = null;
					}
					designerHost.Container.Add(component, name);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000521AC File Offset: 0x000511AC
		Control IOleDragClient.GetControlForComponent(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null)
			{
				return ComponentTray.TrayControl.FromComponent(component2);
			}
			return null;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000521CB File Offset: 0x000511CB
		Control IOleDragClient.GetDesignerControl()
		{
			return this;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000521CE File Offset: 0x000511CE
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			return true;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000521D4 File Offset: 0x000511D4
		bool ISelectionUIHandler.BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			if (this.TabOrderActive)
			{
				return false;
			}
			bool flag = this.DragHandler.BeginDrag(components, rules, initialX, initialY);
			return (!flag || this.GetOleDragHandler().DoBeginDrag(components, rules, initialX, initialY)) && flag;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00052214 File Offset: 0x00051214
		void ISelectionUIHandler.DragMoved(object[] components, Rectangle offset)
		{
			this.DragHandler.DragMoved(components, offset);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00052224 File Offset: 0x00051224
		void ISelectionUIHandler.EndDrag(object[] components, bool cancel)
		{
			this.DragHandler.EndDrag(components, cancel);
			this.GetOleDragHandler().DoEndDrag(components, cancel);
			if (!this.autoScrollPosBeforeDragging.IsEmpty)
			{
				foreach (IComponent component in components)
				{
					ComponentTray.TrayControl trayControl = ComponentTray.TrayControl.FromComponent(component);
					if (trayControl != null)
					{
						this.SetTrayLocation(component, new Point(trayControl.Location.X - this.autoScrollPosBeforeDragging.X, trayControl.Location.Y - this.autoScrollPosBeforeDragging.Y));
					}
				}
				base.AutoScrollPosition = new Point(-this.autoScrollPosBeforeDragging.X, -this.autoScrollPosBeforeDragging.Y);
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000522E3 File Offset: 0x000512E3
		Rectangle ISelectionUIHandler.GetComponentBounds(object component)
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000522EA File Offset: 0x000512EA
		SelectionRules ISelectionUIHandler.GetComponentRules(object component)
		{
			return SelectionRules.Moveable | SelectionRules.Visible;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000522F1 File Offset: 0x000512F1
		Rectangle ISelectionUIHandler.GetSelectionClipRect(object component)
		{
			if (base.IsHandleCreated)
			{
				return base.RectangleToScreen(base.ClientRectangle);
			}
			return Rectangle.Empty;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0005230D File Offset: 0x0005130D
		void ISelectionUIHandler.OleDragEnter(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragEnter(de);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0005231B File Offset: 0x0005131B
		void ISelectionUIHandler.OleDragDrop(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragDrop(de);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00052329 File Offset: 0x00051329
		void ISelectionUIHandler.OleDragOver(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragOver(de);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00052337 File Offset: 0x00051337
		void ISelectionUIHandler.OleDragLeave()
		{
			this.GetOleDragHandler().DoOleDragLeave();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00052344 File Offset: 0x00051344
		void ISelectionUIHandler.OnSelectionDoubleClick(IComponent component)
		{
			if (!this.TabOrderActive)
			{
				ComponentTray.TrayControl trayControl = ((IOleDragClient)this).GetControlForComponent(component) as ComponentTray.TrayControl;
				if (trayControl != null)
				{
					trayControl.ViewDefaultEvent(component);
				}
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00052370 File Offset: 0x00051370
		bool ISelectionUIHandler.QueryBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			return this.DragHandler.QueryBeginDrag(components, rules, initialX, initialY);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00052384 File Offset: 0x00051384
		internal void RearrangeInAutoSlots(Control c, Point pos)
		{
			ComponentTray.TrayControl trayControl = (ComponentTray.TrayControl)c;
			trayControl.Positioned = true;
			trayControl.Location = pos;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000523A8 File Offset: 0x000513A8
		void ISelectionUIHandler.ShowContextMenu(IComponent component)
		{
			Point mousePosition = Control.MousePosition;
			this.OnContextMenu(mousePosition.X, mousePosition.Y, true);
		}

		// Token: 0x0400107D RID: 4221
		private static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x0400107E RID: 4222
		private IServiceProvider serviceProvider;

		// Token: 0x0400107F RID: 4223
		private Point whiteSpace = Point.Empty;

		// Token: 0x04001080 RID: 4224
		private Size grabHandle = Size.Empty;

		// Token: 0x04001081 RID: 4225
		private ArrayList controls;

		// Token: 0x04001082 RID: 4226
		private SelectionUIHandler dragHandler;

		// Token: 0x04001083 RID: 4227
		private ISelectionUIService selectionUISvc;

		// Token: 0x04001084 RID: 4228
		private IToolboxService toolboxService;

		// Token: 0x04001085 RID: 4229
		internal OleDragDropHandler oleDragDropHandler;

		// Token: 0x04001086 RID: 4230
		private IDesigner mainDesigner;

		// Token: 0x04001087 RID: 4231
		private IEventHandlerService eventHandlerService;

		// Token: 0x04001088 RID: 4232
		private bool queriedTabOrder;

		// Token: 0x04001089 RID: 4233
		private MenuCommand tabOrderCommand;

		// Token: 0x0400108A RID: 4234
		private ICollection selectedObjects;

		// Token: 0x0400108B RID: 4235
		private IMenuCommandService menuCommandService;

		// Token: 0x0400108C RID: 4236
		private CommandSet privateCommandSet;

		// Token: 0x0400108D RID: 4237
		private InheritanceUI inheritanceUI;

		// Token: 0x0400108E RID: 4238
		private Point mouseDragStart = ComponentTray.InvalidPoint;

		// Token: 0x0400108F RID: 4239
		private Point mouseDragEnd = ComponentTray.InvalidPoint;

		// Token: 0x04001090 RID: 4240
		private Rectangle mouseDragWorkspace = Rectangle.Empty;

		// Token: 0x04001091 RID: 4241
		private ToolboxItem mouseDragTool;

		// Token: 0x04001092 RID: 4242
		private Point mouseDropLocation = ComponentTray.InvalidPoint;

		// Token: 0x04001093 RID: 4243
		private bool showLargeIcons;

		// Token: 0x04001094 RID: 4244
		private bool autoArrange;

		// Token: 0x04001095 RID: 4245
		private Point autoScrollPosBeforeDragging = Point.Empty;

		// Token: 0x04001096 RID: 4246
		private MenuCommand menucmdArrangeIcons;

		// Token: 0x04001097 RID: 4247
		private MenuCommand menucmdLineupIcons;

		// Token: 0x04001098 RID: 4248
		private MenuCommand menucmdLargeIcons;

		// Token: 0x04001099 RID: 4249
		private bool fResetAmbient;

		// Token: 0x0400109A RID: 4250
		private ComponentTray.ComponentTrayGlyphManager glyphManager;

		// Token: 0x020001B8 RID: 440
		// (Invoke) Token: 0x06001120 RID: 4384
		private delegate void AsyncInvokeHandler(bool children);

		// Token: 0x020001B9 RID: 441
		private class ComponentTrayGlyphManager
		{
			// Token: 0x06001123 RID: 4387 RVA: 0x000523E6 File Offset: 0x000513E6
			public ComponentTrayGlyphManager(ISelectionService selSvc, BehaviorService behaviorSvc)
			{
				this.selSvc = selSvc;
				this.behaviorSvc = behaviorSvc;
				this.traySelectionAdorner = new Adorner();
			}

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06001124 RID: 4388 RVA: 0x00052407 File Offset: 0x00051407
			public GlyphCollection SelectionGlyphs
			{
				get
				{
					return this.traySelectionAdorner.Glyphs;
				}
			}

			// Token: 0x06001125 RID: 4389 RVA: 0x00052414 File Offset: 0x00051414
			public void Dispose()
			{
				if (this.traySelectionAdorner != null)
				{
					this.traySelectionAdorner.Glyphs.Clear();
					this.traySelectionAdorner = null;
				}
			}

			// Token: 0x06001126 RID: 4390 RVA: 0x00052438 File Offset: 0x00051438
			public GlyphCollection GetGlyphsForComponent(IComponent comp)
			{
				GlyphCollection glyphCollection = new GlyphCollection();
				if (this.behaviorSvc != null && comp != null && this.behaviorSvc.DesignerActionUI != null)
				{
					Glyph designerActionGlyph = this.behaviorSvc.DesignerActionUI.GetDesignerActionGlyph(comp);
					if (designerActionGlyph != null)
					{
						glyphCollection.Add(designerActionGlyph);
					}
				}
				return glyphCollection;
			}

			// Token: 0x06001127 RID: 4391 RVA: 0x00052484 File Offset: 0x00051484
			public Cursor GetHitTest(Point p)
			{
				for (int i = 0; i < this.traySelectionAdorner.Glyphs.Count; i++)
				{
					Cursor hitTest = this.traySelectionAdorner.Glyphs[i].GetHitTest(p);
					if (hitTest != null)
					{
						this.hitTestedGlyph = this.traySelectionAdorner.Glyphs[i];
						return hitTest;
					}
				}
				this.hitTestedGlyph = null;
				return null;
			}

			// Token: 0x06001128 RID: 4392 RVA: 0x000524F0 File Offset: 0x000514F0
			public bool OnMouseDoubleClick(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseDoubleClick(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x06001129 RID: 4393 RVA: 0x00052544 File Offset: 0x00051544
			public bool OnMouseDown(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseDown(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x00052598 File Offset: 0x00051598
			public bool OnMouseMove(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseMove(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x0600112B RID: 4395 RVA: 0x000525E9 File Offset: 0x000515E9
			public bool OnMouseUp(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseUp(this.hitTestedGlyph, e.Button);
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x00052620 File Offset: 0x00051620
			public void OnPaintGlyphs(PaintEventArgs pe)
			{
				foreach (object obj in this.traySelectionAdorner.Glyphs)
				{
					Glyph glyph = (Glyph)obj;
					glyph.Paint(pe);
				}
			}

			// Token: 0x0600112D RID: 4397 RVA: 0x00052680 File Offset: 0x00051680
			public void UpdateLocation(ComponentTray.TrayControl trayControl)
			{
				foreach (object obj in this.traySelectionAdorner.Glyphs)
				{
					Glyph glyph = (Glyph)obj;
					DesignerActionGlyph designerActionGlyph = glyph as DesignerActionGlyph;
					if (designerActionGlyph != null && ((DesignerActionBehavior)designerActionGlyph.Behavior).RelatedComponent.Equals(trayControl.Component))
					{
						designerActionGlyph.UpdateAlternativeBounds(trayControl.Bounds);
					}
				}
			}

			// Token: 0x0400109B RID: 4251
			private Adorner traySelectionAdorner;

			// Token: 0x0400109C RID: 4252
			private Glyph hitTestedGlyph;

			// Token: 0x0400109D RID: 4253
			private ISelectionService selSvc;

			// Token: 0x0400109E RID: 4254
			private BehaviorService behaviorSvc;
		}

		// Token: 0x020001BE RID: 446
		private class TrayOleDragDropHandler : OleDragDropHandler
		{
			// Token: 0x0600116A RID: 4458 RVA: 0x00054722 File Offset: 0x00053722
			public TrayOleDragDropHandler(SelectionUIHandler selectionHandler, IServiceProvider serviceProvider, IOleDragClient client) : base(selectionHandler, serviceProvider, client)
			{
			}

			// Token: 0x0600116B RID: 4459 RVA: 0x00054730 File Offset: 0x00053730
			protected override bool CanDropDataObject(IDataObject dataObj)
			{
				ICollection collection = null;
				if (dataObj != null)
				{
					OleDragDropHandler.ComponentDataObjectWrapper componentDataObjectWrapper = dataObj as OleDragDropHandler.ComponentDataObjectWrapper;
					if (componentDataObjectWrapper != null)
					{
						OleDragDropHandler.ComponentDataObject innerData = componentDataObjectWrapper.InnerData;
						collection = innerData.Components;
					}
					else
					{
						try
						{
							object data = dataObj.GetData(OleDragDropHandler.DataFormat, true);
							if (data == null)
							{
								return false;
							}
							IDesignerSerializationService designerSerializationService = (IDesignerSerializationService)base.GetService(typeof(IDesignerSerializationService));
							if (designerSerializationService == null)
							{
								return false;
							}
							collection = designerSerializationService.Deserialize(data);
						}
						catch (Exception ex)
						{
							if (ClientUtils.IsCriticalException(ex))
							{
								throw;
							}
						}
						catch
						{
						}
					}
				}
				if (collection != null && collection.Count > 0)
				{
					foreach (object obj in collection)
					{
						if (!(obj is Point) && (obj is Control || !(obj is IComponent)))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x020001BF RID: 447
		internal class AutoArrangeComparer : IComparer
		{
			// Token: 0x0600116C RID: 4460 RVA: 0x00054844 File Offset: 0x00053844
			int IComparer.Compare(object o1, object o2)
			{
				Point location = ((Control)o1).Location;
				Point location2 = ((Control)o2).Location;
				int num = ((Control)o1).Width / 2;
				int num2 = ((Control)o1).Height / 2;
				if (location.X == location2.X && location.Y == location2.Y)
				{
					return 0;
				}
				if (location.Y + num2 <= location2.Y)
				{
					return -1;
				}
				if (location2.Y + num2 <= location.Y)
				{
					return 1;
				}
				if (location.X > location2.X)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x020001C0 RID: 448
		internal class TrayControl : Control
		{
			// Token: 0x0600116E RID: 4462 RVA: 0x000548EC File Offset: 0x000538EC
			public TrayControl(ComponentTray tray, IComponent component)
			{
				this.tray = tray;
				this.component = component;
				base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.borderWidth = SystemInformation.BorderSize.Width;
				this.UpdateIconInfo();
				IComponentChangeService componentChangeService = (IComponentChangeService)tray.GetService(typeof(IComponentChangeService));
				bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRename += this.OnComponentRename;
				}
				ISite site = component.Site;
				string text = null;
				if (site != null)
				{
					text = site.Name;
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						dictionaryService.SetValue(base.GetType(), this);
					}
				}
				if (text == null)
				{
					text = component.GetType().Name;
				}
				this.Text = text;
				this.inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(component)[typeof(InheritanceAttribute)];
				base.TabStop = false;
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x0600116F RID: 4463 RVA: 0x000549F6 File Offset: 0x000539F6
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06001170 RID: 4464 RVA: 0x000549FE File Offset: 0x000539FE
			public override Font Font
			{
				get
				{
					return this.tray.Font;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06001171 RID: 4465 RVA: 0x00054A0B File Offset: 0x00053A0B
			public InheritanceAttribute InheritanceAttribute
			{
				get
				{
					return this.inheritanceAttribute;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06001172 RID: 4466 RVA: 0x00054A13 File Offset: 0x00053A13
			// (set) Token: 0x06001173 RID: 4467 RVA: 0x00054A1B File Offset: 0x00053A1B
			public bool Positioned
			{
				get
				{
					return this.positioned;
				}
				set
				{
					this.positioned = value;
				}
			}

			// Token: 0x06001174 RID: 4468 RVA: 0x00054A24 File Offset: 0x00053A24
			private void AdjustSize(bool autoArrange)
			{
				using (Graphics graphics = base.CreateGraphics())
				{
					Size size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));
					Rectangle bounds = base.Bounds;
					if (this.tray.ShowLargeIcons)
					{
						bounds.Width = Math.Max(this.cxIcon, size.Width) + 4 * this.borderWidth + 10;
						bounds.Height = this.cyIcon + 10 + size.Height + 4 * this.borderWidth;
					}
					else
					{
						bounds.Width = this.cxIcon + size.Width + 4 * this.borderWidth + 10;
						bounds.Height = Math.Max(this.cyIcon, size.Height) + 4 * this.borderWidth;
					}
					base.Bounds = bounds;
					base.Invalidate();
				}
				if (this.tray.glyphManager != null)
				{
					this.tray.glyphManager.UpdateLocation(this);
				}
			}

			// Token: 0x06001175 RID: 4469 RVA: 0x00054B38 File Offset: 0x00053B38
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new ComponentTray.TrayControl.TrayControlAccessibleObject(this, this.tray);
			}

			// Token: 0x06001176 RID: 4470 RVA: 0x00054B48 File Offset: 0x00053B48
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					ISite site = this.component.Site;
					if (site != null)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
						bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
						if (componentChangeService != null)
						{
							componentChangeService.ComponentRename -= this.OnComponentRename;
						}
						IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
						bool enabled2 = CompModSwitches.CommonDesignerServices.Enabled;
						if (dictionaryService != null)
						{
							dictionaryService.SetValue(typeof(ComponentTray.TrayControl), null);
						}
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x06001177 RID: 4471 RVA: 0x00054BDC File Offset: 0x00053BDC
			public static ComponentTray.TrayControl FromComponent(IComponent component)
			{
				ComponentTray.TrayControl result = null;
				if (component == null)
				{
					return null;
				}
				ISite site = component.Site;
				if (site != null)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
					if (dictionaryService != null)
					{
						result = (ComponentTray.TrayControl)dictionaryService.GetValue(typeof(ComponentTray.TrayControl));
					}
				}
				return result;
			}

			// Token: 0x06001178 RID: 4472 RVA: 0x00054C35 File Offset: 0x00053C35
			private void OnComponentRename(object sender, ComponentRenameEventArgs e)
			{
				if (e.Component == this.component)
				{
					this.Text = e.NewName;
					this.AdjustSize(true);
				}
			}

			// Token: 0x06001179 RID: 4473 RVA: 0x00054C58 File Offset: 0x00053C58
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				this.AdjustSize(false);
			}

			// Token: 0x0600117A RID: 4474 RVA: 0x00054C68 File Offset: 0x00053C68
			protected override void OnDoubleClick(EventArgs e)
			{
				base.OnDoubleClick(e);
				if (!this.tray.TabOrderActive)
				{
					IDesignerHost designerHost = (IDesignerHost)this.tray.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						this.mouseDragLast = ComponentTray.InvalidPoint;
						base.Capture = false;
						IDesigner designer = designerHost.GetDesigner(this.component);
						if (designer == null)
						{
							this.ViewDefaultEvent(this.component);
							return;
						}
						designer.DoDefaultAction();
					}
				}
			}

			// Token: 0x0600117B RID: 4475 RVA: 0x00054CDC File Offset: 0x00053CDC
			private void OnEndDrag(bool cancel)
			{
				this.mouseDragLast = ComponentTray.InvalidPoint;
				if (!this.mouseDragMoved)
				{
					if (this.ctrlSelect)
					{
						ISelectionService selectionService = (ISelectionService)this.tray.GetService(typeof(ISelectionService));
						if (selectionService != null)
						{
							selectionService.SetSelectedComponents(new object[]
							{
								this.Component
							}, SelectionTypes.Click);
						}
						this.ctrlSelect = false;
					}
					return;
				}
				this.mouseDragMoved = false;
				this.ctrlSelect = false;
				base.Capture = false;
				this.OnSetCursor();
				if (this.tray.selectionUISvc != null && this.tray.selectionUISvc.Dragging)
				{
					this.tray.selectionUISvc.EndDrag(cancel);
				}
			}

			// Token: 0x0600117C RID: 4476 RVA: 0x00054D90 File Offset: 0x00053D90
			protected override void OnMouseDown(MouseEventArgs me)
			{
				base.OnMouseDown(me);
				if (!this.tray.TabOrderActive)
				{
					this.tray.FocusDesigner();
					if (me.Button == MouseButtons.Left)
					{
						base.Capture = true;
						this.mouseDragLast = base.PointToScreen(new Point(me.X, me.Y));
						this.ctrlSelect = (NativeMethods.GetKeyState(17) != 0);
						if (!this.ctrlSelect)
						{
							ISelectionService selectionService = (ISelectionService)this.tray.GetService(typeof(ISelectionService));
							bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
							if (selectionService != null)
							{
								selectionService.SetSelectedComponents(new object[]
								{
									this.Component
								}, SelectionTypes.Click);
							}
						}
					}
				}
			}

			// Token: 0x0600117D RID: 4477 RVA: 0x00054E50 File Offset: 0x00053E50
			protected override void OnMouseMove(MouseEventArgs me)
			{
				base.OnMouseMove(me);
				if (this.mouseDragLast == ComponentTray.InvalidPoint)
				{
					return;
				}
				if (!this.mouseDragMoved)
				{
					Size dragSize = SystemInformation.DragSize;
					Size doubleClickSize = SystemInformation.DoubleClickSize;
					dragSize.Width = Math.Max(dragSize.Width, doubleClickSize.Width);
					dragSize.Height = Math.Max(dragSize.Height, doubleClickSize.Height);
					Point point = base.PointToScreen(new Point(me.X, me.Y));
					if (this.mouseDragLast == ComponentTray.InvalidPoint || (Math.Abs(this.mouseDragLast.X - point.X) < dragSize.Width && Math.Abs(this.mouseDragLast.Y - point.Y) < dragSize.Height))
					{
						return;
					}
					this.mouseDragMoved = true;
					this.ctrlSelect = false;
				}
				try
				{
					ISelectionService selectionService = (ISelectionService)this.tray.GetService(typeof(ISelectionService));
					if (selectionService != null)
					{
						selectionService.SetSelectedComponents(new object[]
						{
							this.Component
						}, SelectionTypes.Click);
					}
					if (this.tray.selectionUISvc != null && this.tray.selectionUISvc.BeginDrag(SelectionRules.Moveable | SelectionRules.Visible, this.mouseDragLast.X, this.mouseDragLast.Y))
					{
						this.OnSetCursor();
					}
				}
				finally
				{
					this.mouseDragMoved = false;
					this.mouseDragLast = ComponentTray.InvalidPoint;
				}
			}

			// Token: 0x0600117E RID: 4478 RVA: 0x00054FDC File Offset: 0x00053FDC
			protected override void OnMouseUp(MouseEventArgs me)
			{
				base.OnMouseUp(me);
				this.OnEndDrag(false);
			}

			// Token: 0x0600117F RID: 4479 RVA: 0x00054FEC File Offset: 0x00053FEC
			private void OnContextMenu(int x, int y)
			{
				if (!this.tray.TabOrderActive)
				{
					base.Capture = false;
					ISelectionService selectionService = (ISelectionService)this.tray.GetService(typeof(ISelectionService));
					if (selectionService != null && !selectionService.GetComponentSelected(this.component))
					{
						selectionService.SetSelectedComponents(new object[]
						{
							this.component
						}, SelectionTypes.Replace);
					}
					IMenuCommandService menuService = this.tray.MenuService;
					if (menuService != null)
					{
						base.Capture = false;
						Cursor.Clip = Rectangle.Empty;
						menuService.ShowContextMenu(MenuCommands.TraySelectionMenu, x, y);
					}
				}
			}

			// Token: 0x06001180 RID: 4480 RVA: 0x00055080 File Offset: 0x00054080
			protected override void OnPaint(PaintEventArgs e)
			{
				if (this.fRecompute)
				{
					this.fRecompute = false;
					this.UpdateIconInfo();
				}
				base.OnPaint(e);
				Rectangle clientRectangle = base.ClientRectangle;
				clientRectangle.X += 5 + this.borderWidth;
				clientRectangle.Y += this.borderWidth;
				clientRectangle.Width -= 2 * this.borderWidth + 5;
				clientRectangle.Height -= 2 * this.borderWidth;
				StringFormat stringFormat = new StringFormat();
				Brush brush = new SolidBrush(this.ForeColor);
				try
				{
					stringFormat.Alignment = StringAlignment.Center;
					if (this.tray.ShowLargeIcons)
					{
						if (this.toolboxBitmap != null)
						{
							int x = clientRectangle.X + (clientRectangle.Width - this.cxIcon) / 2;
							int y = clientRectangle.Y + 5;
							e.Graphics.DrawImage(this.toolboxBitmap, new Rectangle(x, y, this.cxIcon, this.cyIcon));
						}
						clientRectangle.Y += this.cyIcon + 5;
						clientRectangle.Height -= this.cyIcon;
						e.Graphics.DrawString(this.Text, this.Font, brush, clientRectangle, stringFormat);
					}
					else
					{
						if (this.toolboxBitmap != null)
						{
							int y2 = clientRectangle.Y + (clientRectangle.Height - this.cyIcon) / 2;
							e.Graphics.DrawImage(this.toolboxBitmap, new Rectangle(clientRectangle.X, y2, this.cxIcon, this.cyIcon));
						}
						clientRectangle.X += this.cxIcon + this.borderWidth;
						clientRectangle.Width -= this.cxIcon;
						clientRectangle.Y += 3;
						e.Graphics.DrawString(this.Text, this.Font, brush, clientRectangle);
					}
				}
				finally
				{
					if (stringFormat != null)
					{
						stringFormat.Dispose();
					}
					if (brush != null)
					{
						brush.Dispose();
					}
				}
				if (!InheritanceAttribute.NotInherited.Equals(this.inheritanceAttribute))
				{
					InheritanceUI inheritanceUI = this.tray.InheritanceUI;
					if (inheritanceUI != null)
					{
						e.Graphics.DrawImage(inheritanceUI.InheritanceGlyph, 0, 0);
					}
				}
			}

			// Token: 0x06001181 RID: 4481 RVA: 0x000552E4 File Offset: 0x000542E4
			protected override void OnFontChanged(EventArgs e)
			{
				this.AdjustSize(true);
				base.OnFontChanged(e);
			}

			// Token: 0x06001182 RID: 4482 RVA: 0x000552F4 File Offset: 0x000542F4
			protected override void OnLocationChanged(EventArgs e)
			{
				if (this.tray.glyphManager != null)
				{
					this.tray.glyphManager.UpdateLocation(this);
				}
			}

			// Token: 0x06001183 RID: 4483 RVA: 0x00055314 File Offset: 0x00054314
			protected override void OnTextChanged(EventArgs e)
			{
				this.AdjustSize(true);
				base.OnTextChanged(e);
			}

			// Token: 0x06001184 RID: 4484 RVA: 0x00055324 File Offset: 0x00054324
			private void OnSetCursor()
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.component)["Locked"];
				if (propertyDescriptor != null && (bool)propertyDescriptor.GetValue(this.component))
				{
					Cursor.Current = Cursors.Default;
					return;
				}
				if (this.tray.TabOrderActive)
				{
					Cursor.Current = Cursors.Default;
					return;
				}
				if (this.mouseDragMoved)
				{
					Cursor.Current = Cursors.Default;
					return;
				}
				if (this.mouseDragLast != ComponentTray.InvalidPoint)
				{
					Cursor.Current = Cursors.Cross;
					return;
				}
				Cursor.Current = Cursors.SizeAll;
			}

			// Token: 0x06001185 RID: 4485 RVA: 0x000553BC File Offset: 0x000543BC
			protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
			{
				if (!this.tray.AutoArrange || (specified & BoundsSpecified.Width) == BoundsSpecified.Width || (specified & BoundsSpecified.Height) == BoundsSpecified.Height)
				{
					base.SetBoundsCore(x, y, width, height, specified);
				}
				Rectangle bounds = base.Bounds;
				Size parentGridSize = this.tray.ParentGridSize;
				if (Math.Abs(bounds.X - x) > parentGridSize.Width || Math.Abs(bounds.Y - y) > parentGridSize.Height)
				{
					base.SetBoundsCore(x, y, width, height, specified);
				}
			}

			// Token: 0x06001186 RID: 4486 RVA: 0x0005543F File Offset: 0x0005443F
			protected override void SetVisibleCore(bool value)
			{
				if (value && !this.tray.CanDisplayComponent(this.component))
				{
					return;
				}
				base.SetVisibleCore(value);
			}

			// Token: 0x06001187 RID: 4487 RVA: 0x0005545F File Offset: 0x0005445F
			public override string ToString()
			{
				return "ComponentTray: " + this.component.ToString();
			}

			// Token: 0x06001188 RID: 4488 RVA: 0x00055478 File Offset: 0x00054478
			internal void UpdateIconInfo()
			{
				ToolboxBitmapAttribute toolboxBitmapAttribute = (ToolboxBitmapAttribute)TypeDescriptor.GetAttributes(this.component)[typeof(ToolboxBitmapAttribute)];
				if (toolboxBitmapAttribute != null)
				{
					this.toolboxBitmap = toolboxBitmapAttribute.GetImage(this.component, this.tray.ShowLargeIcons);
				}
				if (this.toolboxBitmap == null)
				{
					this.cxIcon = 0;
					this.cyIcon = SystemInformation.IconSize.Height;
				}
				else
				{
					Size size = this.toolboxBitmap.Size;
					this.cxIcon = size.Width;
					this.cyIcon = size.Height;
				}
				this.AdjustSize(true);
			}

			// Token: 0x06001189 RID: 4489 RVA: 0x00055518 File Offset: 0x00054518
			public virtual void ViewDefaultEvent(IComponent component)
			{
				EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(component);
				PropertyDescriptor propertyDescriptor = null;
				bool flag = false;
				IEventBindingService eventBindingService = (IEventBindingService)this.GetService(typeof(IEventBindingService));
				bool enabled = CompModSwitches.CommonDesignerServices.Enabled;
				if (eventBindingService != null)
				{
					propertyDescriptor = eventBindingService.GetEventProperty(defaultEvent);
				}
				if (propertyDescriptor == null || propertyDescriptor.IsReadOnly)
				{
					if (eventBindingService != null)
					{
						eventBindingService.ShowCode();
					}
					return;
				}
				string text = (string)propertyDescriptor.GetValue(component);
				if (text == null)
				{
					flag = true;
					text = eventBindingService.CreateUniqueMethodName(component, defaultEvent);
				}
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				DesignerTransaction designerTransaction = null;
				try
				{
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("WindowsFormsAddEvent", new object[]
						{
							defaultEvent.Name
						}));
					}
					if (flag && propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(component, text);
					}
					eventBindingService.ShowCode(component, defaultEvent);
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
			}

			// Token: 0x0600118A RID: 4490 RVA: 0x00055610 File Offset: 0x00054610
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 32)
				{
					this.OnSetCursor();
					return;
				}
				if (msg == 123)
				{
					int num = NativeMethods.Util.SignedLOWORD((int)m.LParam);
					int num2 = NativeMethods.Util.SignedHIWORD((int)m.LParam);
					if (num == -1 && num2 == -1)
					{
						Point mousePosition = Control.MousePosition;
						num = mousePosition.X;
						num2 = mousePosition.Y;
					}
					this.OnContextMenu(num, num2);
					return;
				}
				if (msg != 132)
				{
					base.WndProc(ref m);
					return;
				}
				if (this.tray.glyphManager != null)
				{
					Point p = new Point((int)((short)NativeMethods.Util.LOWORD((int)m.LParam)), (int)((short)NativeMethods.Util.HIWORD((int)m.LParam)));
					NativeMethods.POINT point = new NativeMethods.POINT();
					point.x = 0;
					point.y = 0;
					NativeMethods.MapWindowPoints(IntPtr.Zero, base.Handle, point, 1);
					p.Offset(point.x, point.y);
					p.Offset(base.Location.X, base.Location.Y);
					this.tray.glyphManager.GetHitTest(p);
				}
				base.WndProc(ref m);
			}

			// Token: 0x040010BD RID: 4285
			private const int whiteSpace = 5;

			// Token: 0x040010BE RID: 4286
			private IComponent component;

			// Token: 0x040010BF RID: 4287
			private Image toolboxBitmap;

			// Token: 0x040010C0 RID: 4288
			private int cxIcon;

			// Token: 0x040010C1 RID: 4289
			private int cyIcon;

			// Token: 0x040010C2 RID: 4290
			private InheritanceAttribute inheritanceAttribute;

			// Token: 0x040010C3 RID: 4291
			private ComponentTray tray;

			// Token: 0x040010C4 RID: 4292
			private Point mouseDragLast = ComponentTray.InvalidPoint;

			// Token: 0x040010C5 RID: 4293
			private bool mouseDragMoved;

			// Token: 0x040010C6 RID: 4294
			private bool ctrlSelect;

			// Token: 0x040010C7 RID: 4295
			private bool positioned;

			// Token: 0x040010C8 RID: 4296
			private int borderWidth;

			// Token: 0x040010C9 RID: 4297
			internal bool fRecompute;

			// Token: 0x020001C1 RID: 449
			private class TrayControlAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x0600118B RID: 4491 RVA: 0x0005574E File Offset: 0x0005474E
				public TrayControlAccessibleObject(ComponentTray.TrayControl owner, ComponentTray tray) : base(owner)
				{
					this.tray = tray;
				}

				// Token: 0x170002D7 RID: 727
				// (get) Token: 0x0600118C RID: 4492 RVA: 0x0005575E File Offset: 0x0005475E
				private IComponent Component
				{
					get
					{
						return ((ComponentTray.TrayControl)base.Owner).Component;
					}
				}

				// Token: 0x170002D8 RID: 728
				// (get) Token: 0x0600118D RID: 4493 RVA: 0x00055770 File Offset: 0x00054770
				public override AccessibleStates State
				{
					get
					{
						AccessibleStates accessibleStates = base.State;
						ISelectionService selectionService = (ISelectionService)this.tray.GetService(typeof(ISelectionService));
						if (selectionService != null)
						{
							if (selectionService.GetComponentSelected(this.Component))
							{
								accessibleStates |= AccessibleStates.Selected;
							}
							if (selectionService.PrimarySelection == this.Component)
							{
								accessibleStates |= AccessibleStates.Focused;
							}
						}
						return accessibleStates;
					}
				}

				// Token: 0x040010CA RID: 4298
				private ComponentTray tray;
			}
		}

		// Token: 0x020001C4 RID: 452
		private class TraySelectionUIHandler : SelectionUIHandler
		{
			// Token: 0x060011A3 RID: 4515 RVA: 0x0005621F File Offset: 0x0005521F
			public TraySelectionUIHandler(ComponentTray tray)
			{
				this.tray = tray;
				this.snapSize = default(Size);
			}

			// Token: 0x060011A4 RID: 4516 RVA: 0x00056248 File Offset: 0x00055248
			public override bool BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
			{
				bool result = base.BeginDrag(components, rules, initialX, initialY);
				this.tray.SuspendLayout();
				return result;
			}

			// Token: 0x060011A5 RID: 4517 RVA: 0x0005626D File Offset: 0x0005526D
			public override void EndDrag(object[] components, bool cancel)
			{
				base.EndDrag(components, cancel);
				this.tray.ResumeLayout();
			}

			// Token: 0x060011A6 RID: 4518 RVA: 0x00056282 File Offset: 0x00055282
			protected override IComponent GetComponent()
			{
				return this.tray;
			}

			// Token: 0x060011A7 RID: 4519 RVA: 0x0005628A File Offset: 0x0005528A
			protected override Control GetControl()
			{
				return this.tray;
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00056292 File Offset: 0x00055292
			protected override Control GetControl(IComponent component)
			{
				return ComponentTray.TrayControl.FromComponent(component);
			}

			// Token: 0x060011A9 RID: 4521 RVA: 0x0005629A File Offset: 0x0005529A
			protected override Size GetCurrentSnapSize()
			{
				return this.snapSize;
			}

			// Token: 0x060011AA RID: 4522 RVA: 0x000562A2 File Offset: 0x000552A2
			protected override object GetService(Type serviceType)
			{
				return this.tray.GetService(serviceType);
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x000562B0 File Offset: 0x000552B0
			protected override bool GetShouldSnapToGrid()
			{
				return false;
			}

			// Token: 0x060011AC RID: 4524 RVA: 0x000562B3 File Offset: 0x000552B3
			public override Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
			{
				return dragRect;
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x000562B6 File Offset: 0x000552B6
			public override void SetCursor()
			{
				this.tray.OnSetCursor();
			}

			// Token: 0x040010D9 RID: 4313
			private ComponentTray tray;

			// Token: 0x040010DA RID: 4314
			private Size snapSize = Size.Empty;
		}
	}
}
