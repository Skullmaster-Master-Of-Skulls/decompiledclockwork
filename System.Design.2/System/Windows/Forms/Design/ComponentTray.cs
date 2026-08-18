using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A5 RID: 677
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[ProvideProperty("Location", typeof(IComponent))]
	[ProvideProperty("TrayLocation", typeof(IComponent))]
	public class ComponentTray : ScrollableControl, IExtenderProvider, ISelectionUIHandler, IOleDragClient
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x00099294 File Offset: 0x00097494
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
			IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				Color backColor;
				if (iuiservice.Styles["ArtboardBackground"] is Color)
				{
					backColor = (Color)iuiservice.Styles["ArtboardBackground"];
				}
				else if (iuiservice.Styles["VsColorDesignerTray"] is Color)
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
				if (iuiservice.Styles["ArtboardBackgroundText"] is Color)
				{
					this.ForeColor = (Color)iuiservice.Styles["ArtboardBackgroundText"];
				}
				else if (iuiservice.Styles["VsColorPanelText"] is Color)
				{
					this.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00099656 File Offset: 0x00097856
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0009965E File Offset: 0x0009785E
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

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0009968B File Offset: 0x0009788B
		public int ComponentCount
		{
			get
			{
				return base.Controls.Count;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00099698 File Offset: 0x00097898
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

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000996B4 File Offset: 0x000978B4
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000996CB File Offset: 0x000978CB
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

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x000996E6 File Offset: 0x000978E6
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

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00099711 File Offset: 0x00097911
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00099719 File Offset: 0x00097919
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0009974C File Offset: 0x0009794C
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

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00099798 File Offset: 0x00097998
		internal bool IsWindowVisible
		{
			get
			{
				return base.IsHandleCreated && NativeMethods.IsWindowVisible(base.Handle);
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000997B0 File Offset: 0x000979B0
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

		// Token: 0x06001A47 RID: 6727 RVA: 0x000997DC File Offset: 0x000979DC
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

		// Token: 0x06001A48 RID: 6728 RVA: 0x00099910 File Offset: 0x00097B10
		bool IExtenderProvider.CanExtend(object component)
		{
			IComponent component2 = component as IComponent;
			return component2 != null && ComponentTray.TrayControl.FromComponent(component2) != null;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00099934 File Offset: 0x00097B34
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

		// Token: 0x06001A4A RID: 6730 RVA: 0x000999AA File Offset: 0x00097BAA
		protected virtual bool CanDisplayComponent(IComponent component)
		{
			return TypeDescriptor.GetAttributes(component).Contains(DesignTimeVisibleAttribute.Yes);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000999BC File Offset: 0x00097BBC
		public void CreateComponentFromTool(ToolboxItem tool)
		{
			if (!this.CanCreateComponentFromTool(tool))
			{
				return;
			}
			this.GetOleDragHandler().CreateTool(tool, null, 0, 0, 0, 0, false, false);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000999E8 File Offset: 0x00097BE8
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

		// Token: 0x06001A4D RID: 6733 RVA: 0x00099A40 File Offset: 0x00097C40
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

		// Token: 0x06001A4E RID: 6734 RVA: 0x00099BE0 File Offset: 0x00097DE0
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

		// Token: 0x06001A4F RID: 6735 RVA: 0x00099CE0 File Offset: 0x00097EE0
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

		// Token: 0x06001A50 RID: 6736 RVA: 0x00099D28 File Offset: 0x00097F28
		private void DrawRubber(Point start, Point end)
		{
			this.mouseDragWorkspace.X = Math.Min(start.X, end.X);
			this.mouseDragWorkspace.Y = Math.Min(start.Y, end.Y);
			this.mouseDragWorkspace.Width = Math.Abs(end.X - start.X);
			this.mouseDragWorkspace.Height = Math.Abs(end.Y - start.Y);
			this.mouseDragWorkspace = base.RectangleToScreen(this.mouseDragWorkspace);
			ControlPaint.DrawReversibleFrame(this.mouseDragWorkspace, this.BackColor, FrameStyle.Dashed);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00099DD4 File Offset: 0x00097FD4
		internal void FocusDesigner()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && designerHost.RootComponent != null)
			{
				IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
				if (rootDesigner != null)
				{
					ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
					if (supportedTechnologies.Length != 0)
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

		// Token: 0x06001A52 RID: 6738 RVA: 0x00099E3C File Offset: 0x0009803C
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

		// Token: 0x06001A53 RID: 6739 RVA: 0x00099EA8 File Offset: 0x000980A8
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

		// Token: 0x06001A54 RID: 6740 RVA: 0x00099F4C File Offset: 0x0009814C
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

		// Token: 0x06001A55 RID: 6741 RVA: 0x0009A008 File Offset: 0x00098208
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

		// Token: 0x06001A56 RID: 6742 RVA: 0x0009A0B7 File Offset: 0x000982B7
		internal virtual OleDragDropHandler GetOleDragHandler()
		{
			if (this.oleDragDropHandler == null)
			{
				this.oleDragDropHandler = new ComponentTray.TrayOleDragDropHandler(this.DragHandler, this.serviceProvider, this);
			}
			return this.oleDragDropHandler;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0009A0E0 File Offset: 0x000982E0
		[Category("Layout")]
		[Localizable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlLocationDescr")]
		[DesignOnly(true)]
		public Point GetLocation(IComponent receiver)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(receiver.GetType())["Location"];
			if (propertyDescriptor != null)
			{
				return (Point)propertyDescriptor.GetValue(receiver);
			}
			return this.GetTrayLocation(receiver);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0009A11C File Offset: 0x0009831C
		[Category("Layout")]
		[Localizable(false)]
		[Browsable(false)]
		[SRDescription("ControlLocationDescr")]
		[DesignOnly(true)]
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

		// Token: 0x06001A59 RID: 6745 RVA: 0x0009A170 File Offset: 0x00098370
		protected override object GetService(Type serviceType)
		{
			object result = null;
			if (this.serviceProvider != null)
			{
				result = this.serviceProvider.GetService(serviceType);
			}
			return result;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0009A195 File Offset: 0x00098395
		internal ComponentTray.TrayControl GetTrayControlFromComponent(IComponent comp)
		{
			return ComponentTray.TrayControl.FromComponent(comp);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0009A1A0 File Offset: 0x000983A0
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

		// Token: 0x06001A5C RID: 6748 RVA: 0x0009A21C File Offset: 0x0009841C
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

		// Token: 0x06001A5D RID: 6749 RVA: 0x0009A28B File Offset: 0x0009848B
		private void OnComponentRemoved(object sender, ComponentEventArgs cevent)
		{
			this.RemoveComponent(cevent.Component);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0009A29C File Offset: 0x0009849C
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

		// Token: 0x06001A5F RID: 6751 RVA: 0x0009A354 File Offset: 0x00098554
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

		// Token: 0x06001A60 RID: 6752 RVA: 0x0009A3E0 File Offset: 0x000985E0
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

		// Token: 0x06001A61 RID: 6753 RVA: 0x0009A444 File Offset: 0x00098644
		protected override void OnGiveFeedback(GiveFeedbackEventArgs gfevent)
		{
			base.OnGiveFeedback(gfevent);
			this.GetOleDragHandler().DoOleGiveFeedback(gfevent);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0009A45C File Offset: 0x0009865C
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
				de.Effect = DragDropEffects.Copy;
			}
			else
			{
				this.GetOleDragHandler().DoOleDragDrop(de);
			}
			this.mouseDropLocation = ComponentTray.InvalidPoint;
			base.ResumeLayout();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0009A540 File Offset: 0x00098740
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

		// Token: 0x06001A64 RID: 6756 RVA: 0x0009A5F0 File Offset: 0x000987F0
		protected override void OnDragLeave(EventArgs e)
		{
			this.mouseDragTool = null;
			this.GetOleDragHandler().DoOleDragLeave();
			base.ResumeLayout();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0009A60A File Offset: 0x0009880A
		protected override void OnDragOver(DragEventArgs de)
		{
			if (this.mouseDragTool != null)
			{
				de.Effect = DragDropEffects.Copy;
				return;
			}
			this.GetOleDragHandler().DoOleDragOver(de);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0009A628 File Offset: 0x00098828
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.DoAutoArrange(false);
			base.Invalidate(true);
			base.OnLayout(levent);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0009A640 File Offset: 0x00098840
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

		// Token: 0x06001A68 RID: 6760 RVA: 0x0009A6A4 File Offset: 0x000988A4
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

		// Token: 0x06001A69 RID: 6761 RVA: 0x0009A734 File Offset: 0x00098934
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

		// Token: 0x06001A6A RID: 6762 RVA: 0x0009A7C4 File Offset: 0x000989C4
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

		// Token: 0x06001A6B RID: 6763 RVA: 0x0009A81C File Offset: 0x00098A1C
		internal void OnMessage(ref Message m)
		{
			this.WndProc(ref m);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0009A828 File Offset: 0x00098A28
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
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0009A9B4 File Offset: 0x00098BB4
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

		// Token: 0x06001A6E RID: 6766 RVA: 0x0009AA58 File Offset: 0x00098C58
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
				this.mouseDragStart = ComponentTray.InvalidPoint;
			}
			base.OnMouseUp(e);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0009ABEC File Offset: 0x00098DEC
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (this.fResetAmbient)
			{
				this.fResetAmbient = false;
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					Color backColor;
					if (iuiservice.Styles["ArtboardBackground"] is Color)
					{
						backColor = (Color)iuiservice.Styles["ArtboardBackground"];
					}
					else if (iuiservice.Styles["VsColorDesignerTray"] is Color)
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

		// Token: 0x06001A70 RID: 6768 RVA: 0x0009ADCC File Offset: 0x00098FCC
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

		// Token: 0x06001A71 RID: 6769 RVA: 0x0009AF74 File Offset: 0x00099174
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

		// Token: 0x06001A72 RID: 6770 RVA: 0x0009AFC3 File Offset: 0x000991C3
		private void OnSystemSettingChanged(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.fResetAmbient = true;
				this.ResetTrayControls();
				base.BeginInvoke(new ComponentTray.AsyncInvokeHandler(base.Invalidate), new object[]
				{
					true
				});
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0009AFC3 File Offset: 0x000991C3
		private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.fResetAmbient = true;
				this.ResetTrayControls();
				base.BeginInvoke(new ComponentTray.AsyncInvokeHandler(base.Invalidate), new object[]
				{
					true
				});
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0009AFFC File Offset: 0x000991FC
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

		// Token: 0x06001A75 RID: 6773 RVA: 0x0009B0FC File Offset: 0x000992FC
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

		// Token: 0x06001A76 RID: 6774 RVA: 0x0009B33C File Offset: 0x0009953C
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

		// Token: 0x06001A77 RID: 6775 RVA: 0x0009B3B8 File Offset: 0x000995B8
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

		// Token: 0x06001A78 RID: 6776 RVA: 0x0009B3F8 File Offset: 0x000995F8
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

		// Token: 0x06001A79 RID: 6777 RVA: 0x0009B460 File Offset: 0x00099660
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

		// Token: 0x06001A7A RID: 6778 RVA: 0x0009B4E8 File Offset: 0x000996E8
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 123)
			{
				if (msg == 31)
				{
					this.OnLostCapture();
					return;
				}
				if (msg == 32)
				{
					this.OnSetCursor();
					return;
				}
				if (msg == 123)
				{
					int num = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
					int num2 = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
					if (num == -1 && num2 == -1)
					{
						Point mousePosition = Control.MousePosition;
						num = mousePosition.X;
						num2 = mousePosition.Y;
					}
					this.OnContextMenu(num, num2, true);
					return;
				}
			}
			else
			{
				if (msg == 125)
				{
					base.Invalidate();
					return;
				}
				if (msg == 132)
				{
					if (this.glyphManager != null)
					{
						Point p = new Point((int)((short)NativeMethods.Util.LOWORD((int)((long)m.LParam))), (int)((short)NativeMethods.Util.HIWORD((int)((long)m.LParam))));
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
				if (msg - 276 <= 1)
				{
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

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x00003B0F File Offset: 0x00001D0F
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0009B640 File Offset: 0x00099840
		IComponent IOleDragClient.Component
		{
			get
			{
				return this.mainDesigner.Component;
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0009B650 File Offset: 0x00099850
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

		// Token: 0x06001A7E RID: 6782 RVA: 0x0009B700 File Offset: 0x00099900
		Control IOleDragClient.GetControlForComponent(object component)
		{
			IComponent component2 = component as IComponent;
			if (component2 != null)
			{
				return ComponentTray.TrayControl.FromComponent(component2);
			}
			return null;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0000CA50 File Offset: 0x0000AC50
		Control IOleDragClient.GetDesignerControl()
		{
			return this;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00003B0F File Offset: 0x00001D0F
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			return true;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0009B720 File Offset: 0x00099920
		bool ISelectionUIHandler.BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			if (this.TabOrderActive)
			{
				return false;
			}
			bool flag = this.DragHandler.BeginDrag(components, rules, initialX, initialY);
			return (!flag || this.GetOleDragHandler().DoBeginDrag(components, rules, initialX, initialY)) && flag;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0009B760 File Offset: 0x00099960
		void ISelectionUIHandler.DragMoved(object[] components, Rectangle offset)
		{
			this.DragHandler.DragMoved(components, offset);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0009B770 File Offset: 0x00099970
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

		// Token: 0x06001A84 RID: 6788 RVA: 0x0009B82F File Offset: 0x00099A2F
		Rectangle ISelectionUIHandler.GetComponentBounds(object component)
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0009B836 File Offset: 0x00099A36
		SelectionRules ISelectionUIHandler.GetComponentRules(object component)
		{
			return SelectionRules.Moveable | SelectionRules.Visible;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0009B83D File Offset: 0x00099A3D
		Rectangle ISelectionUIHandler.GetSelectionClipRect(object component)
		{
			if (base.IsHandleCreated)
			{
				return base.RectangleToScreen(base.ClientRectangle);
			}
			return Rectangle.Empty;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0009B859 File Offset: 0x00099A59
		void ISelectionUIHandler.OleDragEnter(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragEnter(de);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0009B867 File Offset: 0x00099A67
		void ISelectionUIHandler.OleDragDrop(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragDrop(de);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0009B875 File Offset: 0x00099A75
		void ISelectionUIHandler.OleDragOver(DragEventArgs de)
		{
			this.GetOleDragHandler().DoOleDragOver(de);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0009B883 File Offset: 0x00099A83
		void ISelectionUIHandler.OleDragLeave()
		{
			this.GetOleDragHandler().DoOleDragLeave();
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0009B890 File Offset: 0x00099A90
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

		// Token: 0x06001A8C RID: 6796 RVA: 0x0009B8BC File Offset: 0x00099ABC
		bool ISelectionUIHandler.QueryBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
		{
			return this.DragHandler.QueryBeginDrag(components, rules, initialX, initialY);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0009B8D0 File Offset: 0x00099AD0
		internal void RearrangeInAutoSlots(Control c, Point pos)
		{
			ComponentTray.TrayControl trayControl = (ComponentTray.TrayControl)c;
			trayControl.Positioned = true;
			trayControl.Location = pos;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0009B8F4 File Offset: 0x00099AF4
		void ISelectionUIHandler.ShowContextMenu(IComponent component)
		{
			Point mousePosition = Control.MousePosition;
			this.OnContextMenu(mousePosition.X, mousePosition.Y, true);
		}

		// Token: 0x040015DD RID: 5597
		private static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x040015DE RID: 5598
		private IServiceProvider serviceProvider;

		// Token: 0x040015DF RID: 5599
		private Point whiteSpace = Point.Empty;

		// Token: 0x040015E0 RID: 5600
		private Size grabHandle = Size.Empty;

		// Token: 0x040015E1 RID: 5601
		private ArrayList controls;

		// Token: 0x040015E2 RID: 5602
		private SelectionUIHandler dragHandler;

		// Token: 0x040015E3 RID: 5603
		private ISelectionUIService selectionUISvc;

		// Token: 0x040015E4 RID: 5604
		private IToolboxService toolboxService;

		// Token: 0x040015E5 RID: 5605
		internal OleDragDropHandler oleDragDropHandler;

		// Token: 0x040015E6 RID: 5606
		private IDesigner mainDesigner;

		// Token: 0x040015E7 RID: 5607
		private IEventHandlerService eventHandlerService;

		// Token: 0x040015E8 RID: 5608
		private bool queriedTabOrder;

		// Token: 0x040015E9 RID: 5609
		private MenuCommand tabOrderCommand;

		// Token: 0x040015EA RID: 5610
		private ICollection selectedObjects;

		// Token: 0x040015EB RID: 5611
		private IMenuCommandService menuCommandService;

		// Token: 0x040015EC RID: 5612
		private CommandSet privateCommandSet;

		// Token: 0x040015ED RID: 5613
		private InheritanceUI inheritanceUI;

		// Token: 0x040015EE RID: 5614
		private Point mouseDragStart = ComponentTray.InvalidPoint;

		// Token: 0x040015EF RID: 5615
		private Point mouseDragEnd = ComponentTray.InvalidPoint;

		// Token: 0x040015F0 RID: 5616
		private Rectangle mouseDragWorkspace = Rectangle.Empty;

		// Token: 0x040015F1 RID: 5617
		private ToolboxItem mouseDragTool;

		// Token: 0x040015F2 RID: 5618
		private Point mouseDropLocation = ComponentTray.InvalidPoint;

		// Token: 0x040015F3 RID: 5619
		private bool showLargeIcons;

		// Token: 0x040015F4 RID: 5620
		private bool autoArrange;

		// Token: 0x040015F5 RID: 5621
		private Point autoScrollPosBeforeDragging = Point.Empty;

		// Token: 0x040015F6 RID: 5622
		private MenuCommand menucmdArrangeIcons;

		// Token: 0x040015F7 RID: 5623
		private MenuCommand menucmdLineupIcons;

		// Token: 0x040015F8 RID: 5624
		private MenuCommand menucmdLargeIcons;

		// Token: 0x040015F9 RID: 5625
		private bool fResetAmbient;

		// Token: 0x040015FA RID: 5626
		private ComponentTray.ComponentTrayGlyphManager glyphManager;

		// Token: 0x02000537 RID: 1335
		// (Invoke) Token: 0x06003089 RID: 12425
		private delegate void AsyncInvokeHandler(bool children);

		// Token: 0x02000538 RID: 1336
		private class ComponentTrayGlyphManager
		{
			// Token: 0x0600308C RID: 12428 RVA: 0x0010A828 File Offset: 0x00108A28
			public ComponentTrayGlyphManager(ISelectionService selSvc, BehaviorService behaviorSvc)
			{
				this.selSvc = selSvc;
				this.behaviorSvc = behaviorSvc;
				this.traySelectionAdorner = new Adorner();
			}

			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x0600308D RID: 12429 RVA: 0x0010A849 File Offset: 0x00108A49
			public GlyphCollection SelectionGlyphs
			{
				get
				{
					return this.traySelectionAdorner.Glyphs;
				}
			}

			// Token: 0x0600308E RID: 12430 RVA: 0x0010A856 File Offset: 0x00108A56
			public void Dispose()
			{
				if (this.traySelectionAdorner != null)
				{
					this.traySelectionAdorner.Glyphs.Clear();
					this.traySelectionAdorner = null;
				}
			}

			// Token: 0x0600308F RID: 12431 RVA: 0x0010A878 File Offset: 0x00108A78
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

			// Token: 0x06003090 RID: 12432 RVA: 0x0010A8C4 File Offset: 0x00108AC4
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

			// Token: 0x06003091 RID: 12433 RVA: 0x0010A930 File Offset: 0x00108B30
			public bool OnMouseDoubleClick(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseDoubleClick(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x06003092 RID: 12434 RVA: 0x0010A984 File Offset: 0x00108B84
			public bool OnMouseDown(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseDown(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x06003093 RID: 12435 RVA: 0x0010A9D8 File Offset: 0x00108BD8
			public bool OnMouseMove(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseMove(this.hitTestedGlyph, e.Button, new Point(e.X, e.Y));
			}

			// Token: 0x06003094 RID: 12436 RVA: 0x0010AA29 File Offset: 0x00108C29
			public bool OnMouseUp(MouseEventArgs e)
			{
				return this.hitTestedGlyph != null && this.hitTestedGlyph.Behavior != null && this.hitTestedGlyph.Behavior.OnMouseUp(this.hitTestedGlyph, e.Button);
			}

			// Token: 0x06003095 RID: 12437 RVA: 0x0010AA60 File Offset: 0x00108C60
			public void OnPaintGlyphs(PaintEventArgs pe)
			{
				foreach (object obj in this.traySelectionAdorner.Glyphs)
				{
					Glyph glyph = (Glyph)obj;
					glyph.Paint(pe);
				}
			}

			// Token: 0x06003096 RID: 12438 RVA: 0x0010AAC0 File Offset: 0x00108CC0
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

			// Token: 0x040020F3 RID: 8435
			private Adorner traySelectionAdorner;

			// Token: 0x040020F4 RID: 8436
			private Glyph hitTestedGlyph;

			// Token: 0x040020F5 RID: 8437
			private ISelectionService selSvc;

			// Token: 0x040020F6 RID: 8438
			private BehaviorService behaviorSvc;
		}

		// Token: 0x02000539 RID: 1337
		private class TrayOleDragDropHandler : OleDragDropHandler
		{
			// Token: 0x06003097 RID: 12439 RVA: 0x0010AB4C File Offset: 0x00108D4C
			public TrayOleDragDropHandler(SelectionUIHandler selectionHandler, IServiceProvider serviceProvider, IOleDragClient client) : base(selectionHandler, serviceProvider, client)
			{
			}

			// Token: 0x06003098 RID: 12440 RVA: 0x0010AB58 File Offset: 0x00108D58
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

		// Token: 0x0200053A RID: 1338
		internal class AutoArrangeComparer : IComparer
		{
			// Token: 0x06003099 RID: 12441 RVA: 0x0010AC58 File Offset: 0x00108E58
			int IComparer.Compare(object o1, object o2)
			{
				Point location = ((Control)o1).Location;
				Point location2 = ((Control)o2).Location;
				int num = ((Control)o1).Height / 2;
				if (location.X == location2.X && location.Y == location2.Y)
				{
					return 0;
				}
				if (location.Y + num <= location2.Y)
				{
					return -1;
				}
				if (location2.Y + num <= location.Y)
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

		// Token: 0x0200053B RID: 1339
		internal class TrayControl : Control
		{
			// Token: 0x0600309B RID: 12443 RVA: 0x0010ACE8 File Offset: 0x00108EE8
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

			// Token: 0x17000969 RID: 2409
			// (get) Token: 0x0600309C RID: 12444 RVA: 0x0010ADF4 File Offset: 0x00108FF4
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x1700096A RID: 2410
			// (get) Token: 0x0600309D RID: 12445 RVA: 0x0010ADFC File Offset: 0x00108FFC
			public override Font Font
			{
				get
				{
					return this.tray.Font;
				}
			}

			// Token: 0x1700096B RID: 2411
			// (get) Token: 0x0600309E RID: 12446 RVA: 0x0010AE09 File Offset: 0x00109009
			public InheritanceAttribute InheritanceAttribute
			{
				get
				{
					return this.inheritanceAttribute;
				}
			}

			// Token: 0x1700096C RID: 2412
			// (get) Token: 0x0600309F RID: 12447 RVA: 0x0010AE11 File Offset: 0x00109011
			// (set) Token: 0x060030A0 RID: 12448 RVA: 0x0010AE19 File Offset: 0x00109019
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

			// Token: 0x060030A1 RID: 12449 RVA: 0x0010AE24 File Offset: 0x00109024
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

			// Token: 0x060030A2 RID: 12450 RVA: 0x0010AF38 File Offset: 0x00109138
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new ComponentTray.TrayControl.TrayControlAccessibleObject(this, this.tray);
			}

			// Token: 0x060030A3 RID: 12451 RVA: 0x0010AF48 File Offset: 0x00109148
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

			// Token: 0x060030A4 RID: 12452 RVA: 0x0010AFDC File Offset: 0x001091DC
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

			// Token: 0x060030A5 RID: 12453 RVA: 0x0010B035 File Offset: 0x00109235
			private void OnComponentRename(object sender, ComponentRenameEventArgs e)
			{
				if (e.Component == this.component)
				{
					this.Text = e.NewName;
					this.AdjustSize(true);
				}
			}

			// Token: 0x060030A6 RID: 12454 RVA: 0x0010B058 File Offset: 0x00109258
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
				this.AdjustSize(false);
			}

			// Token: 0x060030A7 RID: 12455 RVA: 0x0010B068 File Offset: 0x00109268
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

			// Token: 0x060030A8 RID: 12456 RVA: 0x0010B0DC File Offset: 0x001092DC
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

			// Token: 0x060030A9 RID: 12457 RVA: 0x0010B18C File Offset: 0x0010938C
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

			// Token: 0x060030AA RID: 12458 RVA: 0x0010B244 File Offset: 0x00109444
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

			// Token: 0x060030AB RID: 12459 RVA: 0x0010B3CC File Offset: 0x001095CC
			protected override void OnMouseUp(MouseEventArgs me)
			{
				base.OnMouseUp(me);
				this.OnEndDrag(false);
			}

			// Token: 0x060030AC RID: 12460 RVA: 0x0010B3DC File Offset: 0x001095DC
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

			// Token: 0x060030AD RID: 12461 RVA: 0x0010B46C File Offset: 0x0010966C
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

			// Token: 0x060030AE RID: 12462 RVA: 0x0010B6D0 File Offset: 0x001098D0
			protected override void OnFontChanged(EventArgs e)
			{
				this.AdjustSize(true);
				base.OnFontChanged(e);
			}

			// Token: 0x060030AF RID: 12463 RVA: 0x0010B6E0 File Offset: 0x001098E0
			protected override void OnLocationChanged(EventArgs e)
			{
				if (this.tray.glyphManager != null)
				{
					this.tray.glyphManager.UpdateLocation(this);
				}
			}

			// Token: 0x060030B0 RID: 12464 RVA: 0x0010B700 File Offset: 0x00109900
			protected override void OnTextChanged(EventArgs e)
			{
				this.AdjustSize(true);
				base.OnTextChanged(e);
			}

			// Token: 0x060030B1 RID: 12465 RVA: 0x0010B710 File Offset: 0x00109910
			private void OnSetCursor()
			{
				PropertyDescriptor propertyDescriptor;
				try
				{
					propertyDescriptor = TypeDescriptor.GetProperties(this.component)["Locked"];
				}
				catch (FileNotFoundException ex)
				{
					Cursor.Current = Cursors.Default;
					return;
				}
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

			// Token: 0x060030B2 RID: 12466 RVA: 0x0010B7C8 File Offset: 0x001099C8
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

			// Token: 0x060030B3 RID: 12467 RVA: 0x0010B84B File Offset: 0x00109A4B
			protected override void SetVisibleCore(bool value)
			{
				if (value && !this.tray.CanDisplayComponent(this.component))
				{
					return;
				}
				base.SetVisibleCore(value);
			}

			// Token: 0x060030B4 RID: 12468 RVA: 0x0010B86B File Offset: 0x00109A6B
			public override string ToString()
			{
				return "ComponentTray: " + this.component.ToString();
			}

			// Token: 0x060030B5 RID: 12469 RVA: 0x0010B884 File Offset: 0x00109A84
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

			// Token: 0x060030B6 RID: 12470 RVA: 0x0010B924 File Offset: 0x00109B24
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

			// Token: 0x060030B7 RID: 12471 RVA: 0x0010BA18 File Offset: 0x00109C18
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
					int num = NativeMethods.Util.SignedLOWORD((int)((long)m.LParam));
					int num2 = NativeMethods.Util.SignedHIWORD((int)((long)m.LParam));
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
					Point p = new Point((int)((short)NativeMethods.Util.LOWORD((int)((long)m.LParam))), (int)((short)NativeMethods.Util.HIWORD((int)((long)m.LParam))));
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

			// Token: 0x040020F7 RID: 8439
			private IComponent component;

			// Token: 0x040020F8 RID: 8440
			private Image toolboxBitmap;

			// Token: 0x040020F9 RID: 8441
			private int cxIcon;

			// Token: 0x040020FA RID: 8442
			private int cyIcon;

			// Token: 0x040020FB RID: 8443
			private InheritanceAttribute inheritanceAttribute;

			// Token: 0x040020FC RID: 8444
			private ComponentTray tray;

			// Token: 0x040020FD RID: 8445
			private Point mouseDragLast = ComponentTray.InvalidPoint;

			// Token: 0x040020FE RID: 8446
			private bool mouseDragMoved;

			// Token: 0x040020FF RID: 8447
			private bool ctrlSelect;

			// Token: 0x04002100 RID: 8448
			private bool positioned;

			// Token: 0x04002101 RID: 8449
			private const int whiteSpace = 5;

			// Token: 0x04002102 RID: 8450
			private int borderWidth;

			// Token: 0x04002103 RID: 8451
			internal bool fRecompute;

			// Token: 0x020005EA RID: 1514
			private class TrayControlAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x060034D0 RID: 13520 RVA: 0x0011EE0F File Offset: 0x0011D00F
				public TrayControlAccessibleObject(ComponentTray.TrayControl owner, ComponentTray tray) : base(owner)
				{
					this.tray = tray;
				}

				// Token: 0x17000A33 RID: 2611
				// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0011EE1F File Offset: 0x0011D01F
				private IComponent Component
				{
					get
					{
						return ((ComponentTray.TrayControl)base.Owner).Component;
					}
				}

				// Token: 0x17000A34 RID: 2612
				// (get) Token: 0x060034D2 RID: 13522 RVA: 0x0011EE34 File Offset: 0x0011D034
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

				// Token: 0x0400233C RID: 9020
				private ComponentTray tray;
			}
		}

		// Token: 0x0200053C RID: 1340
		private class TraySelectionUIHandler : SelectionUIHandler
		{
			// Token: 0x060030B8 RID: 12472 RVA: 0x0010BB57 File Offset: 0x00109D57
			public TraySelectionUIHandler(ComponentTray tray)
			{
				this.tray = tray;
				this.snapSize = default(Size);
			}

			// Token: 0x060030B9 RID: 12473 RVA: 0x0010BB80 File Offset: 0x00109D80
			public override bool BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY)
			{
				bool result = base.BeginDrag(components, rules, initialX, initialY);
				this.tray.SuspendLayout();
				return result;
			}

			// Token: 0x060030BA RID: 12474 RVA: 0x0010BBA5 File Offset: 0x00109DA5
			public override void EndDrag(object[] components, bool cancel)
			{
				base.EndDrag(components, cancel);
				this.tray.ResumeLayout();
			}

			// Token: 0x060030BB RID: 12475 RVA: 0x0010BBBA File Offset: 0x00109DBA
			protected override IComponent GetComponent()
			{
				return this.tray;
			}

			// Token: 0x060030BC RID: 12476 RVA: 0x0010BBBA File Offset: 0x00109DBA
			protected override Control GetControl()
			{
				return this.tray;
			}

			// Token: 0x060030BD RID: 12477 RVA: 0x0009A195 File Offset: 0x00098395
			protected override Control GetControl(IComponent component)
			{
				return ComponentTray.TrayControl.FromComponent(component);
			}

			// Token: 0x060030BE RID: 12478 RVA: 0x0010BBC2 File Offset: 0x00109DC2
			protected override Size GetCurrentSnapSize()
			{
				return this.snapSize;
			}

			// Token: 0x060030BF RID: 12479 RVA: 0x0010BBCA File Offset: 0x00109DCA
			protected override object GetService(Type serviceType)
			{
				return this.tray.GetService(serviceType);
			}

			// Token: 0x060030C0 RID: 12480 RVA: 0x0000445B File Offset: 0x0000265B
			protected override bool GetShouldSnapToGrid()
			{
				return false;
			}

			// Token: 0x060030C1 RID: 12481 RVA: 0x0010BBD8 File Offset: 0x00109DD8
			public override Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
			{
				return dragRect;
			}

			// Token: 0x060030C2 RID: 12482 RVA: 0x0010BBDB File Offset: 0x00109DDB
			public override void SetCursor()
			{
				this.tray.OnSetCursor();
			}

			// Token: 0x04002104 RID: 8452
			private ComponentTray tray;

			// Token: 0x04002105 RID: 8453
			private Size snapSize = Size.Empty;
		}
	}
}
