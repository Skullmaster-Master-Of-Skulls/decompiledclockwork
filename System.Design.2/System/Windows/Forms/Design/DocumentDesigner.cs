using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DC RID: 732
	[ToolboxItemFilter("System.Windows.Forms")]
	public class DocumentDesigner : ScrollableControlDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, IOleDragClient
	{
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x000AFB44 File Offset: 0x000ADD44
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x000AFB6C File Offset: 0x000ADD6C
		private SizeF AutoScaleDimensions
		{
			get
			{
				ContainerControl containerControl = this.Control as ContainerControl;
				if (containerControl != null)
				{
					return containerControl.CurrentAutoScaleDimensions;
				}
				return SizeF.Empty;
			}
			set
			{
				ContainerControl containerControl = this.Control as ContainerControl;
				if (containerControl != null)
				{
					containerControl.AutoScaleDimensions = value;
				}
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000AFB90 File Offset: 0x000ADD90
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x000AFBB4 File Offset: 0x000ADDB4
		private AutoScaleMode AutoScaleMode
		{
			get
			{
				ContainerControl containerControl = this.Control as ContainerControl;
				if (containerControl != null)
				{
					return containerControl.AutoScaleMode;
				}
				return AutoScaleMode.Inherit;
			}
			set
			{
				base.ShadowProperties["AutoScaleMode"] = value;
				ContainerControl containerControl = this.Control as ContainerControl;
				if (containerControl != null && containerControl.AutoScaleMode != value)
				{
					containerControl.AutoScaleMode = value;
					IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null && !designerHost.Loading)
					{
						containerControl.AutoScaleDimensions = containerControl.CurrentAutoScaleDimensions;
					}
				}
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x000AFC23 File Offset: 0x000ADE23
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x000AFC30 File Offset: 0x000ADE30
		private Color BackColor
		{
			get
			{
				return this.Control.BackColor;
			}
			set
			{
				base.ShadowProperties["BackColor"] = value;
				if (value.IsEmpty)
				{
					value = SystemColors.Control;
				}
				this.Control.BackColor = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000AFC64 File Offset: 0x000ADE64
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x000AFC7B File Offset: 0x000ADE7B
		[DefaultValue(typeof(Point), "0, 0")]
		private Point Location
		{
			get
			{
				return (Point)base.ShadowProperties["Location"];
			}
			set
			{
				base.ShadowProperties["Location"] = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x000AFC94 File Offset: 0x000ADE94
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				return selectionRules & ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.LeftSizeable);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000AFCB4 File Offset: 0x000ADEB4
		private bool TabOrderActive
		{
			get
			{
				if (!this.queriedTabOrder)
				{
					this.queriedTabOrder = true;
					IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
					if (menuCommandService != null)
					{
						this.tabOrderCommand = menuCommandService.FindCommand(StandardCommands.TabOrder);
					}
				}
				return this.tabOrderCommand != null && this.tabOrderCommand.Checked;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x000AFD0F File Offset: 0x000ADF0F
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x000AFD17 File Offset: 0x000ADF17
		[DefaultValue(true)]
		private bool TrayAutoArrange
		{
			get
			{
				return this.trayAutoArrange;
			}
			set
			{
				this.trayAutoArrange = value;
				if (this.componentTray != null)
				{
					this.componentTray.AutoArrange = this.trayAutoArrange;
				}
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x000AFD39 File Offset: 0x000ADF39
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x000AFD41 File Offset: 0x000ADF41
		[DefaultValue(false)]
		private bool TrayLargeIcon
		{
			get
			{
				return this.trayLargeIcon;
			}
			set
			{
				this.trayLargeIcon = value;
				if (this.componentTray != null)
				{
					this.componentTray.ShowLargeIcons = this.trayLargeIcon;
				}
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000AFD63 File Offset: 0x000ADF63
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x000AFD7F File Offset: 0x000ADF7F
		[DefaultValue(80)]
		private int TrayHeight
		{
			get
			{
				if (this.componentTray != null)
				{
					return this.componentTray.Height;
				}
				return this.trayHeight;
			}
			set
			{
				this.trayHeight = value;
				if (this.componentTray != null)
				{
					this.componentTray.Height = this.trayHeight;
				}
			}
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000AFDA4 File Offset: 0x000ADFA4
		Control IOleDragClient.GetControlForComponent(object component)
		{
			Control control = base.GetControl(component);
			if (control != null)
			{
				return control;
			}
			if (this.componentTray != null)
			{
				return ((IOleDragClient)this.componentTray).GetControlForComponent(component);
			}
			return null;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000AFDD4 File Offset: 0x000ADFD4
		internal virtual bool CanDropComponents(DragEventArgs de)
		{
			if (this.componentTray == null)
			{
				return true;
			}
			OleDragDropHandler oleDragHandler = base.GetOleDragHandler();
			object[] draggingObjects = oleDragHandler.GetDraggingObjects(de);
			if (draggingObjects != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				for (int i = 0; i < draggingObjects.Length; i++)
				{
					IComponent component = draggingObjects[i] as IComponent;
					if (designerHost != null && draggingObjects[i] != null && component != null && this.componentTray.IsTrayComponent(component))
					{
						return false;
					}
				}
			}
			return !(de.Data is ToolStripItemDataObject);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000AFE58 File Offset: 0x000AE058
		private ToolboxItem CreateAxToolboxItem(IDataObject dataObject)
		{
			MemoryStream memoryStream = (MemoryStream)dataObject.GetData(DocumentDesigner.axClipFormat, true);
			int num = (int)memoryStream.Length;
			byte[] array = new byte[num];
			memoryStream.Read(array, 0, num);
			string text = Encoding.Default.GetString(array);
			int num2 = text.IndexOf("}");
			text = text.Substring(0, num2 + 1);
			if (this.IsSupportedActiveXControl(text))
			{
				DocumentDesigner.AxToolboxItem axToolboxItem;
				if (this.axTools != null)
				{
					axToolboxItem = (DocumentDesigner.AxToolboxItem)this.axTools[text];
					if (axToolboxItem != null)
					{
						bool traceVerbose = DocumentDesigner.AxToolSwitch.TraceVerbose;
						return axToolboxItem;
					}
				}
				axToolboxItem = new DocumentDesigner.AxToolboxItem(text);
				if (this.axTools == null)
				{
					this.axTools = new Hashtable();
				}
				this.axTools.Add(text, axToolboxItem);
				return axToolboxItem;
			}
			return null;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000AFF1C File Offset: 0x000AE11C
		private ToolboxItem CreateCfCodeToolboxItem(IDataObject dataObject)
		{
			object data = dataObject.GetData(OleDragDropHandler.NestedToolboxItemFormat, false);
			if (data != null)
			{
				return (ToolboxItem)data;
			}
			data = dataObject.GetData(OleDragDropHandler.DataFormat, false);
			if (data != null)
			{
				return new OleDragDropHandler.CfCodeToolboxItem(data);
			}
			return null;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000AFF5C File Offset: 0x000AE15C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ToolStripAdornerWindowService toolStripAdornerWindowService = (ToolStripAdornerWindowService)this.GetService(typeof(ToolStripAdornerWindowService));
					if (toolStripAdornerWindowService != null)
					{
						toolStripAdornerWindowService.Dispose();
						designerHost.RemoveService(typeof(ToolStripAdornerWindowService));
					}
					designerHost.Activated -= this.OnDesignerActivate;
					designerHost.Deactivated -= this.OnDesignerDeactivate;
					if (this.componentTray != null)
					{
						ISplitWindowService splitWindowService = (ISplitWindowService)this.GetService(typeof(ISplitWindowService));
						if (splitWindowService != null)
						{
							splitWindowService.RemoveSplitWindow(this.componentTray);
							this.componentTray.Dispose();
							this.componentTray = null;
						}
						designerHost.RemoveService(typeof(ComponentTray));
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentAdded -= this.OnComponentAdded;
						componentChangeService.ComponentChanged -= this.OnComponentChanged;
						componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					}
					if (this.undoEngine != null)
					{
						this.undoEngine.Undoing -= this.OnUndoing;
						this.undoEngine.Undone -= this.OnUndone;
					}
					if (this.toolboxCreator != null)
					{
						IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
						if (toolboxService != null)
						{
							toolboxService.RemoveCreator(DocumentDesigner.axClipFormat, designerHost);
							toolboxService.RemoveCreator(OleDragDropHandler.DataFormat, designerHost);
							toolboxService.RemoveCreator(OleDragDropHandler.NestedToolboxItemFormat, designerHost);
						}
						this.toolboxCreator = null;
					}
				}
				if (this.menuEditorService != null)
				{
					designerHost.RemoveService(typeof(IMenuEditorService));
					this.menuEditorService = null;
				}
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SelectionChanged -= this.OnSelectionChanged;
				}
				if (this.behaviorService != null)
				{
					this.behaviorService.Dispose();
					this.behaviorService = null;
				}
				if (this.selectionManager != null)
				{
					this.selectionManager.Dispose();
					this.selectionManager = null;
				}
				if (this.componentTray != null)
				{
					if (designerHost != null)
					{
						ISplitWindowService splitWindowService2 = (ISplitWindowService)this.GetService(typeof(ISplitWindowService));
						if (splitWindowService2 != null)
						{
							splitWindowService2.RemoveSplitWindow(this.componentTray);
						}
					}
					this.componentTray.Dispose();
					this.componentTray = null;
				}
				if (this.pbrsFwd != null)
				{
					this.pbrsFwd.Dispose();
					this.pbrsFwd = null;
				}
				if (this.frame != null)
				{
					this.frame.Dispose();
					this.frame = null;
				}
				if (this.commandSet != null)
				{
					this.commandSet.Dispose();
					this.commandSet = null;
				}
				if (this.inheritanceService != null)
				{
					this.inheritanceService.Dispose();
					this.inheritanceService = null;
				}
				if (this.inheritanceUI != null)
				{
					this.inheritanceUI.Dispose();
					this.inheritanceUI = null;
				}
				if (this.designBindingValueUIHandler != null)
				{
					IPropertyValueUIService propertyValueUIService = (IPropertyValueUIService)this.GetService(typeof(IPropertyValueUIService));
					if (propertyValueUIService != null)
					{
						propertyValueUIService.RemovePropertyValueUIHandler(new PropertyValueUIHandler(this.designBindingValueUIHandler.OnGetUIValueItem));
					}
					this.designBindingValueUIHandler.Dispose();
					this.designBindingValueUIHandler = null;
				}
				if (this.designerExtenders != null)
				{
					this.designerExtenders.Dispose();
					this.designerExtenders = null;
				}
				if (this.axTools != null)
				{
					this.axTools.Clear();
				}
				if (designerHost != null)
				{
					designerHost.RemoveService(typeof(BehaviorService));
					designerHost.RemoveService(typeof(ToolStripAdornerWindowService));
					designerHost.RemoveService(typeof(SelectionManager));
					designerHost.RemoveService(typeof(IInheritanceService));
					designerHost.RemoveService(typeof(IEventHandlerService));
					designerHost.RemoveService(typeof(IOverlayService));
					designerHost.RemoveService(typeof(ISplitWindowService));
					designerHost.RemoveService(typeof(InheritanceUI));
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000B0358 File Offset: 0x000AE558
		public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			GlyphCollection glyphCollection = new GlyphCollection();
			if (selectionType != GlyphSelectionType.NotSelected)
			{
				Point location = base.BehaviorService.ControlToAdornerWindow((Control)base.Component);
				Rectangle controlBounds = new Rectangle(location, ((Control)base.Component).Size);
				bool primarySelection = selectionType == GlyphSelectionType.SelectedPrimary;
				bool flag = false;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Locked"];
				if (propertyDescriptor != null)
				{
					flag = (bool)propertyDescriptor.GetValue(base.Component);
				}
				bool flag2 = false;
				propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["AutoSize"];
				if (propertyDescriptor != null)
				{
					flag2 = (bool)propertyDescriptor.GetValue(base.Component);
				}
				AutoSizeMode autoSizeMode = AutoSizeMode.GrowOnly;
				propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["AutoSizeMode"];
				if (propertyDescriptor != null)
				{
					autoSizeMode = (AutoSizeMode)propertyDescriptor.GetValue(base.Component);
				}
				SelectionRules selectionRules = this.SelectionRules;
				if (flag)
				{
					glyphCollection.Add(new LockedHandleGlyph(controlBounds, primarySelection));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Top));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Bottom));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Left));
					glyphCollection.Add(new LockedBorderGlyph(controlBounds, SelectionBorderGlyphType.Right));
				}
				else if (flag2 && autoSizeMode == AutoSizeMode.GrowAndShrink && !(this.Control is Form))
				{
					glyphCollection.Add(new NoResizeHandleGlyph(controlBounds, selectionRules, primarySelection, null));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Top, null));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Bottom, null));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Left, null));
					glyphCollection.Add(new NoResizeSelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Right, null));
				}
				else
				{
					glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleRight, this.StandardBehavior, primarySelection));
					glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.LowerRight, this.StandardBehavior, primarySelection));
					glyphCollection.Add(new GrabHandleGlyph(controlBounds, GrabHandleGlyphType.MiddleBottom, this.StandardBehavior, primarySelection));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Top, null));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Bottom, this.StandardBehavior));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Left, null));
					glyphCollection.Add(new SelectionBorderGlyph(controlBounds, selectionRules, SelectionBorderGlyphType.Right, this.StandardBehavior));
				}
			}
			return glyphCollection;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000B0590 File Offset: 0x000AE790
		private ParentControlDesigner GetSelectedParentControlDesigner()
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			ParentControlDesigner parentControlDesigner = null;
			if (selectionService != null)
			{
				object obj = selectionService.PrimarySelection;
				if (obj == null || !(obj is Control))
				{
					obj = null;
					ICollection selectedComponents = selectionService.GetSelectedComponents();
					foreach (object obj2 in selectedComponents)
					{
						if (obj2 is Control)
						{
							obj = obj2;
							break;
						}
					}
				}
				if (obj != null)
				{
					Control control = (Control)obj;
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						while (control != null)
						{
							ParentControlDesigner parentControlDesigner2 = designerHost.GetDesigner(control) as ParentControlDesigner;
							if (parentControlDesigner2 != null)
							{
								parentControlDesigner = parentControlDesigner2;
								break;
							}
							control = control.Parent;
						}
					}
				}
			}
			if (parentControlDesigner == null)
			{
				parentControlDesigner = this;
			}
			return parentControlDesigner;
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000B067C File Offset: 0x000AE87C
		public override void Initialize(IComponent component)
		{
			this.initializing = true;
			base.Initialize(component);
			this.initializing = false;
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component.GetType())["BackColor"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(Color) && !propertyDescriptor.ShouldSerializeValue(base.Component))
			{
				this.Control.BackColor = SystemColors.Control;
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			IExtenderProviderService extenderProviderService = (IExtenderProviderService)this.GetService(typeof(IExtenderProviderService));
			if (extenderProviderService != null)
			{
				this.designerExtenders = new DesignerExtenders(extenderProviderService);
			}
			if (designerHost != null)
			{
				designerHost.Activated += this.OnDesignerActivate;
				designerHost.Deactivated += this.OnDesignerDeactivate;
				ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
				designerHost.AddService(typeof(IEventHandlerService), callback);
				this.frame = new DesignerFrame(component.Site);
				IOverlayService serviceInstance = this.frame;
				designerHost.AddService(typeof(IOverlayService), serviceInstance);
				designerHost.AddService(typeof(ISplitWindowService), this.frame);
				this.behaviorService = new BehaviorService(base.Component.Site, this.frame);
				designerHost.AddService(typeof(BehaviorService), this.behaviorService);
				this.selectionManager = new SelectionManager(designerHost, this.behaviorService);
				designerHost.AddService(typeof(SelectionManager), this.selectionManager);
				designerHost.AddService(typeof(ToolStripAdornerWindowService), callback);
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded += this.OnComponentAdded;
					componentChangeService.ComponentChanged += this.OnComponentChanged;
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				}
				this.inheritanceUI = new InheritanceUI();
				designerHost.AddService(typeof(InheritanceUI), this.inheritanceUI);
				InheritanceService inheritanceService = new DocumentDesigner.DocumentInheritanceService(this);
				designerHost.AddService(typeof(IInheritanceService), inheritanceService);
				DocumentDesigner.manager = (designerHost.GetService(typeof(IDesignerSerializationManager)) as IDesignerSerializationManager);
				inheritanceService.AddInheritedComponents(component, component.Site.Container);
				DocumentDesigner.manager = null;
				this.inheritanceService = inheritanceService;
				if (this.Control.IsHandleCreated)
				{
					this.OnCreateHandle();
				}
				IPropertyValueUIService propertyValueUIService = (IPropertyValueUIService)component.Site.GetService(typeof(IPropertyValueUIService));
				if (propertyValueUIService != null)
				{
					this.designBindingValueUIHandler = new DesignBindingValueUIHandler();
					propertyValueUIService.AddPropertyValueUIHandler(new PropertyValueUIHandler(this.designBindingValueUIHandler.OnGetUIValueItem));
				}
				IToolboxService toolboxService = (IToolboxService)designerHost.GetService(typeof(IToolboxService));
				if (toolboxService != null)
				{
					this.toolboxCreator = new ToolboxItemCreatorCallback(this.OnCreateToolboxItem);
					toolboxService.AddCreator(this.toolboxCreator, DocumentDesigner.axClipFormat, designerHost);
					toolboxService.AddCreator(this.toolboxCreator, OleDragDropHandler.DataFormat, designerHost);
					toolboxService.AddCreator(this.toolboxCreator, OleDragDropHandler.NestedToolboxItemFormat, designerHost);
				}
				designerHost.LoadComplete += this.OnLoadComplete;
			}
			this.commandSet = new ControlCommandSet(component.Site);
			this.frame.Initialize(this.Control);
			this.pbrsFwd = new PbrsForward(this.frame, component.Site);
			this.Location = new Point(0, 0);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000B0A00 File Offset: 0x000AEC00
		private bool IsSupportedActiveXControl(string clsid)
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			bool result;
			try
			{
				string name = "CLSID\\" + clsid + "\\Control";
				registryKey = Registry.ClassesRoot.OpenSubKey(name);
				if (registryKey != null)
				{
					string name2 = string.Concat(new string[]
					{
						"CLSID\\",
						clsid,
						"\\Implemented Categories\\{",
						DocumentDesigner.htmlDesignTime.ToString(),
						"}"
					});
					registryKey2 = Registry.ClassesRoot.OpenSubKey(name2);
					result = (registryKey2 == null);
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
			return result;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000B0AAC File Offset: 0x000AECAC
		private void OnUndone(object source, EventArgs e)
		{
			if (this.suspendedComponents != null)
			{
				foreach (object obj in this.suspendedComponents)
				{
					Control control = (Control)obj;
					control.ResumeLayout(false);
					control.PerformLayout();
				}
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000B0B14 File Offset: 0x000AED14
		private void OnUndoing(object source, EventArgs e)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				IContainer container = designerHost.Container;
				if (container != null)
				{
					this.suspendedComponents = new ArrayList(container.Components.Count + 1);
					foreach (object obj in container.Components)
					{
						IComponent component = (IComponent)obj;
						Control control = component as Control;
						if (control != null)
						{
							control.SuspendLayout();
							this.suspendedComponents.Add(control);
						}
					}
					Control control2 = designerHost.RootComponent as Control;
					if (control2 != null)
					{
						Control parent = control2.Parent;
						if (parent != null)
						{
							parent.SuspendLayout();
							this.suspendedComponents.Add(parent);
						}
					}
				}
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000B0C00 File Offset: 0x000AEE00
		private void OnComponentAdded(object source, ComponentEventArgs ce)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				IComponent component = ce.Component;
				this.EnsureMenuEditorService(ce.Component);
				bool flag = true;
				if (!(designerHost.GetDesigner(component) is ToolStripDesigner))
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(component) as ControlDesigner;
					if (controlDesigner != null)
					{
						Form form = controlDesigner.Control as Form;
						if (form == null || !form.TopLevel)
						{
							flag = false;
						}
					}
				}
				if (flag && TypeDescriptor.GetAttributes(component).Contains(DesignTimeVisibleAttribute.Yes))
				{
					if (this.componentTray == null)
					{
						ISplitWindowService splitWindowService = (ISplitWindowService)this.GetService(typeof(ISplitWindowService));
						if (splitWindowService != null)
						{
							this.componentTray = new ComponentTray(this, base.Component.Site);
							splitWindowService.AddSplitWindow(this.componentTray);
							this.componentTray.Height = this.trayHeight;
							this.componentTray.ShowLargeIcons = this.trayLargeIcon;
							this.componentTray.AutoArrange = this.trayAutoArrange;
							designerHost.AddService(typeof(ComponentTray), this.componentTray);
						}
					}
					if (this.componentTray != null)
					{
						if (designerHost != null && designerHost.Loading && !this.trayLayoutSuspended)
						{
							this.trayLayoutSuspended = true;
							this.componentTray.SuspendLayout();
						}
						this.componentTray.AddComponent(component);
					}
				}
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000B0D64 File Offset: 0x000AEF64
		private void OnComponentRemoved(object source, ComponentEventArgs ce)
		{
			if ((!(ce.Component is Control) || ce.Component is ToolStrip || (ce.Component is Form && ((Form)ce.Component).TopLevel)) && this.componentTray != null)
			{
				this.componentTray.RemoveComponent(ce.Component);
				if (this.componentTray.ComponentCount == 0)
				{
					ISplitWindowService splitWindowService = (ISplitWindowService)this.GetService(typeof(ISplitWindowService));
					if (splitWindowService != null)
					{
						splitWindowService.RemoveSplitWindow(this.componentTray);
						IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							designerHost.RemoveService(typeof(ComponentTray));
						}
						this.componentTray.Dispose();
						this.componentTray = null;
					}
				}
			}
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000B0E40 File Offset: 0x000AF040
		protected override void OnContextMenu(int x, int y)
		{
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					if (selectionService.SelectionCount == 1 && selectionService.GetComponentSelected(base.Component))
					{
						menuCommandService.ShowContextMenu(MenuCommands.ContainerMenu, x, y);
						return;
					}
					Component component = selectionService.PrimarySelection as Component;
					if (component != null)
					{
						IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							ComponentDesigner componentDesigner = designerHost.GetDesigner(component) as ComponentDesigner;
							if (componentDesigner != null)
							{
								componentDesigner.ShowContextMenu(x, y);
								return;
							}
						}
					}
					menuCommandService.ShowContextMenu(MenuCommands.SelectionMenu, x, y);
				}
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000B0EF8 File Offset: 0x000AF0F8
		protected override void OnCreateHandle()
		{
			if (this.inheritanceService != null)
			{
				base.OnCreateHandle();
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000B0F08 File Offset: 0x000AF108
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(IEventHandlerService))
			{
				if (this.eventHandlerService == null)
				{
					this.eventHandlerService = new EventHandlerService(this.frame);
				}
				return this.eventHandlerService;
			}
			if (serviceType == typeof(ToolStripAdornerWindowService))
			{
				return new ToolStripAdornerWindowService(base.Component.Site, this.frame);
			}
			return null;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000B0F74 File Offset: 0x000AF174
		private ToolboxItem OnCreateToolboxItem(object serializedData, string format)
		{
			IDataObject dataObject = serializedData as IDataObject;
			if (dataObject == null)
			{
				return null;
			}
			if (format.Equals(DocumentDesigner.axClipFormat))
			{
				return this.CreateAxToolboxItem(dataObject);
			}
			if (format.Equals(OleDragDropHandler.DataFormat) || format.Equals(OleDragDropHandler.NestedToolboxItemFormat))
			{
				return this.CreateCfCodeToolboxItem(dataObject);
			}
			return null;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000B0FC8 File Offset: 0x000AF1C8
		private void OnDesignerActivate(object source, EventArgs evevent)
		{
			if (this.undoEngine == null)
			{
				this.undoEngine = (this.GetService(typeof(UndoEngine)) as UndoEngine);
				if (this.undoEngine != null)
				{
					this.undoEngine.Undoing += this.OnUndoing;
					this.undoEngine.Undone += this.OnUndone;
				}
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000B1030 File Offset: 0x000AF230
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 0, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000B107C File Offset: 0x000AF27C
		private void OnLoadComplete(object sender, EventArgs e)
		{
			((IDesignerHost)sender).LoadComplete -= this.OnLoadComplete;
			if (this.trayLayoutSuspended && this.componentTray != null)
			{
				this.componentTray.ResumeLayout();
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SelectionChanged += this.OnSelectionChanged;
				selectionService.SetSelectedComponents(new object[]
				{
					base.Component
				}, SelectionTypes.Replace);
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000B10FC File Offset: 0x000AF2FC
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && control.IsHandleCreated)
			{
				UnsafeNativeMethods.NotifyWinEvent(32779, new HandleRef(control, control.Handle), -4, 0);
				if (this.frame.Focused)
				{
					UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(control, control.Handle), -4, 0);
				}
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000B1160 File Offset: 0x000AF360
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				foreach (object obj in selectedComponents)
				{
					Control control = obj as Control;
					if (control != null)
					{
						UnsafeNativeMethods.NotifyWinEvent(32775, new HandleRef(control, control.Handle), -4, 0);
					}
				}
				Control control2 = selectionService.PrimarySelection as Control;
				if (control2 != null)
				{
					UnsafeNativeMethods.NotifyWinEvent(32773, new HandleRef(control2, control2.Handle), -4, 0);
				}
				IHelpService helpService = (IHelpService)this.GetService(typeof(IHelpService));
				if (helpService != null)
				{
					ushort num = 0;
					string[] array = new string[]
					{
						"VisualSelection",
						"NonVisualSelection",
						"MixedSelection"
					};
					foreach (object obj2 in selectedComponents)
					{
						if (obj2 is Control)
						{
							if (obj2 != base.Component)
							{
								num |= 1;
							}
						}
						else
						{
							num |= 2;
						}
						if (num == 3)
						{
							break;
						}
					}
					for (int i = 0; i < array.Length; i++)
					{
						helpService.RemoveContextAttribute("Keyword", array[i]);
					}
					if (num != 0)
					{
						helpService.AddContextAttribute("Keyword", array[(int)(num - 1)], HelpKeywordType.GeneralKeyword);
					}
				}
				if (this.menuEditorService != null)
				{
					this.DoProperMenuSelection(selectedComponents);
				}
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000B1314 File Offset: 0x000AF514
		internal virtual void DoProperMenuSelection(ICollection selComponents)
		{
			foreach (object obj in selComponents)
			{
				ContextMenu contextMenu = obj as ContextMenu;
				if (contextMenu != null)
				{
					this.menuEditorService.SetMenu((Menu)obj);
				}
				else
				{
					MenuItem menuItem = obj as MenuItem;
					if (menuItem != null)
					{
						MenuItem menuItem2 = menuItem;
						while (menuItem2.Parent is MenuItem)
						{
							menuItem2 = (MenuItem)menuItem2.Parent;
						}
						if (this.menuEditorService.GetMenu() != menuItem2.Parent)
						{
							this.menuEditorService.SetMenu(menuItem2.Parent);
						}
						if (selComponents.Count == 1)
						{
							this.menuEditorService.SetSelection(menuItem);
						}
					}
					else
					{
						this.menuEditorService.SetMenu(null);
					}
				}
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000B13F8 File Offset: 0x000AF5F8
		protected virtual void EnsureMenuEditorService(IComponent c)
		{
			if (this.menuEditorService == null && c is ContextMenu)
			{
				this.menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000B1428 File Offset: 0x000AF628
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties["TrayHeight"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), "TrayHeight", typeof(int), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignOnlyAttribute.Yes,
				new SRDescriptionAttribute("FormDocumentDesignerTraySizeDescr"),
				CategoryAttribute.Design
			});
			properties["TrayLargeIcon"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), "TrayLargeIcon", typeof(bool), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignOnlyAttribute.Yes,
				CategoryAttribute.Design
			});
			properties["DoubleBuffered"] = TypeDescriptor.CreateProperty(typeof(Control), "DoubleBuffered", typeof(bool), new Attribute[]
			{
				BrowsableAttribute.Yes,
				DesignOnlyAttribute.No
			});
			string[] array = new string[]
			{
				"Location",
				"BackColor"
			};
			string[] array2 = new string[]
			{
				"Anchor",
				"Dock",
				"TabIndex",
				"TabStop",
				"Visible"
			};
			Attribute[] attributes = new Attribute[0];
			PropertyDescriptor propertyDescriptor;
			for (int i = 0; i < array.Length; i++)
			{
				propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), propertyDescriptor, attributes);
				}
			}
			propertyDescriptor = (PropertyDescriptor)properties["AutoScaleDimensions"];
			if (propertyDescriptor != null)
			{
				properties["AutoScaleDimensions"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), propertyDescriptor, new Attribute[]
				{
					DesignerSerializationVisibilityAttribute.Visible
				});
			}
			propertyDescriptor = (PropertyDescriptor)properties["AutoScaleMode"];
			if (propertyDescriptor != null)
			{
				properties["AutoScaleMode"] = TypeDescriptor.CreateProperty(typeof(DocumentDesigner), propertyDescriptor, new Attribute[]
				{
					DesignerSerializationVisibilityAttribute.Visible,
					BrowsableAttribute.Yes
				});
			}
			for (int j = 0; j < array2.Length; j++)
			{
				propertyDescriptor = (PropertyDescriptor)properties[array2[j]];
				if (propertyDescriptor != null)
				{
					properties[array2[j]] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
					{
						BrowsableAttribute.No,
						DesignerSerializationVisibilityAttribute.Hidden
					});
				}
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000B1676 File Offset: 0x000AF876
		private void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000B1683 File Offset: 0x000AF883
		private bool ShouldSerializeAutoScaleDimensions()
		{
			return !this.initializing && this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000B16A3 File Offset: 0x000AF8A3
		private bool ShouldSerializeAutoScaleMode()
		{
			return !this.initializing && base.ShadowProperties.Contains("AutoScaleMode");
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000B16C0 File Offset: 0x000AF8C0
		private bool ShouldSerializeBackColor()
		{
			return base.ShadowProperties.Contains("BackColor") && !((Color)base.ShadowProperties["BackColor"]).IsEmpty;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000B1704 File Offset: 0x000AF904
		protected virtual void ToolPicked(ToolboxItem tool)
		{
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				MenuCommand menuCommand = menuCommandService.FindCommand(StandardCommands.TabOrder);
				if (menuCommand != null && menuCommand.Checked)
				{
					return;
				}
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.Activate();
			}
			try
			{
				ParentControlDesigner selectedParentControlDesigner = this.GetSelectedParentControlDesigner();
				if (!base.InvokeGetInheritanceAttribute(selectedParentControlDesigner).Equals(InheritanceAttribute.InheritedReadOnly))
				{
					ParentControlDesigner.InvokeCreateTool(selectedParentControlDesigner, tool);
					IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
					if (toolboxService != null)
					{
						toolboxService.SelectedToolboxItemUsed();
					}
				}
			}
			catch (Exception ex)
			{
				base.DisplayError(ex);
				if (ClientUtils.IsCriticalException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0009C128 File Offset: 0x0009A328
		ViewTechnology[] IRootDesigner.SupportedTechnologies
		{
			get
			{
				return new ViewTechnology[]
				{
					ViewTechnology.Default,
					ViewTechnology.WindowsForms
				};
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000B17D0 File Offset: 0x000AF9D0
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			if (technology != ViewTechnology.Default && technology != ViewTechnology.WindowsForms)
			{
				throw new ArgumentException();
			}
			return this.frame;
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000B17E6 File Offset: 0x000AF9E6
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return this.GetToolSupported(tool);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000B17EF File Offset: 0x000AF9EF
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			this.ToolPicked(tool);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000B17F8 File Offset: 0x000AF9F8
		private unsafe void WmWindowPosChanged(ref Message m)
		{
			NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)((void*)m.LParam);
			if ((ptr->flags & 1) == 0 && this.menuEditorService != null)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000B1830 File Offset: 0x000AFA30
		protected override void WndProc(ref Message m)
		{
			if (this.menuEditorService != null && (!this.TabOrderActive || (m.Msg != 161 && m.Msg != 164)) && this.menuEditorService.MessageFilter(ref m))
			{
				return;
			}
			base.WndProc(ref m);
			if (m.Msg == 71)
			{
				this.WmWindowPosChanged(ref m);
			}
		}

		// Token: 0x04001750 RID: 5968
		private DesignerFrame frame;

		// Token: 0x04001751 RID: 5969
		private ControlCommandSet commandSet;

		// Token: 0x04001752 RID: 5970
		private InheritanceService inheritanceService;

		// Token: 0x04001753 RID: 5971
		private EventHandlerService eventHandlerService;

		// Token: 0x04001754 RID: 5972
		private DesignBindingValueUIHandler designBindingValueUIHandler;

		// Token: 0x04001755 RID: 5973
		private BehaviorService behaviorService;

		// Token: 0x04001756 RID: 5974
		private SelectionManager selectionManager;

		// Token: 0x04001757 RID: 5975
		private DesignerExtenders designerExtenders;

		// Token: 0x04001758 RID: 5976
		private InheritanceUI inheritanceUI;

		// Token: 0x04001759 RID: 5977
		private PbrsForward pbrsFwd;

		// Token: 0x0400175A RID: 5978
		private ArrayList suspendedComponents;

		// Token: 0x0400175B RID: 5979
		private UndoEngine undoEngine;

		// Token: 0x0400175C RID: 5980
		private bool initializing;

		// Token: 0x0400175D RID: 5981
		private bool queriedTabOrder;

		// Token: 0x0400175E RID: 5982
		private MenuCommand tabOrderCommand;

		// Token: 0x0400175F RID: 5983
		internal static IDesignerSerializationManager manager;

		// Token: 0x04001760 RID: 5984
		protected IMenuEditorService menuEditorService;

		// Token: 0x04001761 RID: 5985
		private ComponentTray componentTray;

		// Token: 0x04001762 RID: 5986
		private int trayHeight = 80;

		// Token: 0x04001763 RID: 5987
		private bool trayLargeIcon;

		// Token: 0x04001764 RID: 5988
		private bool trayAutoArrange;

		// Token: 0x04001765 RID: 5989
		private bool trayLayoutSuspended;

		// Token: 0x04001766 RID: 5990
		private static Guid htmlDesignTime = new Guid("73CEF3DD-AE85-11CF-A406-00AA00C00940");

		// Token: 0x04001767 RID: 5991
		private Hashtable axTools;

		// Token: 0x04001768 RID: 5992
		private static TraceSwitch AxToolSwitch = new TraceSwitch("AxTool", "ActiveX Toolbox Tracing");

		// Token: 0x04001769 RID: 5993
		private static readonly string axClipFormat = "CLSID";

		// Token: 0x0400176A RID: 5994
		private ToolboxItemCreatorCallback toolboxCreator;

		// Token: 0x0200056D RID: 1389
		[Serializable]
		private class AxToolboxItem : ToolboxItem
		{
			// Token: 0x060031D5 RID: 12757 RVA: 0x0010F046 File Offset: 0x0010D246
			public AxToolboxItem(string clsid) : base(typeof(AxHost))
			{
				this.clsid = clsid;
				base.Company = null;
				this.LoadVersionInfo();
			}

			// Token: 0x060031D6 RID: 12758 RVA: 0x0010F077 File Offset: 0x0010D277
			private AxToolboxItem(SerializationInfo info, StreamingContext context)
			{
				this.Deserialize(info, context);
			}

			// Token: 0x170009AB RID: 2475
			// (get) Token: 0x060031D7 RID: 12759 RVA: 0x0010F092 File Offset: 0x0010D292
			public override string ComponentType
			{
				get
				{
					return SR.GetString("Ax_Control");
				}
			}

			// Token: 0x170009AC RID: 2476
			// (get) Token: 0x060031D8 RID: 12760 RVA: 0x0010F09E File Offset: 0x0010D29E
			public override string Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x060031D9 RID: 12761 RVA: 0x0010F0A8 File Offset: 0x0010D2A8
			private void LoadVersionInfo()
			{
				string name = "CLSID\\" + this.clsid;
				RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name);
				if (registryKey != null)
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey("Version");
					if (registryKey2 != null)
					{
						this.version = (string)registryKey2.GetValue("");
						registryKey2.Close();
					}
					registryKey.Close();
				}
			}

			// Token: 0x060031DA RID: 12762 RVA: 0x0010F108 File Offset: 0x0010D308
			protected override IComponent[] CreateComponentsCore(IDesignerHost host)
			{
				IComponent[] array = null;
				object references = this.GetReferences(host);
				if (references != null)
				{
					try
					{
						System.Runtime.InteropServices.ComTypes.TYPELIBATTR typeLibAttr = this.GetTypeLibAttr();
						object[] array2 = new object[]
						{
							"{" + typeLibAttr.guid.ToString() + "}",
							(int)typeLibAttr.wMajorVerNum,
							(int)typeLibAttr.wMinorVerNum,
							typeLibAttr.lcid,
							""
						};
						object obj = references.GetType().InvokeMember("AddActiveX", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, references, array2, CultureInfo.InvariantCulture);
						array2[4] = "aximp";
						object reference = references.GetType().InvokeMember("AddActiveX", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, references, array2, CultureInfo.InvariantCulture);
						this.axctlType = this.GetAxTypeFromReference(reference, host);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
					catch (Exception ex2)
					{
						throw ex2;
					}
				}
				if (this.axctlType == null)
				{
					IUIService iuiservice = (IUIService)host.GetService(typeof(IUIService));
					if (iuiservice == null)
					{
						RTLAwareMessageBox.Show(null, SR.GetString("AxImportFailed"), null, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
					else
					{
						iuiservice.ShowError(SR.GetString("AxImportFailed"));
					}
					return new IComponent[0];
				}
				array = new IComponent[1];
				try
				{
					array[0] = host.CreateComponent(this.axctlType);
				}
				catch (Exception ex3)
				{
					throw ex3;
				}
				return array;
			}

			// Token: 0x060031DB RID: 12763 RVA: 0x0010F290 File Offset: 0x0010D490
			protected override void Deserialize(SerializationInfo info, StreamingContext context)
			{
				base.Deserialize(info, context);
				this.clsid = info.GetString("Clsid");
			}

			// Token: 0x060031DC RID: 12764 RVA: 0x0010F2AC File Offset: 0x0010D4AC
			private Type GetAxTypeFromReference(object reference, IDesignerHost host)
			{
				string text = (string)reference.GetType().InvokeMember("Path", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, reference, null, CultureInfo.InvariantCulture);
				if (text == null || text.Length <= 0)
				{
					return null;
				}
				FileInfo fileInfo = new FileInfo(text);
				string fullName = fileInfo.FullName;
				ITypeResolutionService typeResolutionService = (ITypeResolutionService)host.GetService(typeof(ITypeResolutionService));
				Assembly assembly = typeResolutionService.GetAssembly(AssemblyName.GetAssemblyName(fullName));
				return this.GetAxTypeFromAssembly(assembly);
			}

			// Token: 0x060031DD RID: 12765 RVA: 0x0010F324 File Offset: 0x0010D524
			private Type GetAxTypeFromAssembly(Assembly a)
			{
				Type[] types = a.GetTypes();
				int num = types.Length;
				for (int i = 0; i < num; i++)
				{
					Type type = types[i];
					if (typeof(AxHost).IsAssignableFrom(type))
					{
						object[] customAttributes = type.GetCustomAttributes(typeof(AxHost.ClsidAttribute), false);
						AxHost.ClsidAttribute clsidAttribute = (AxHost.ClsidAttribute)customAttributes[0];
						if (string.Equals(clsidAttribute.Value, this.clsid, StringComparison.OrdinalIgnoreCase))
						{
							return type;
						}
					}
				}
				return null;
			}

			// Token: 0x060031DE RID: 12766 RVA: 0x0010F394 File Offset: 0x0010D594
			private object GetReferences(IDesignerHost host)
			{
				Type type = Type.GetType("EnvDTE.ProjectItem, EnvDTE, Version=7.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				if (type == null)
				{
					return null;
				}
				object service = host.GetService(type);
				if (service == null)
				{
					return null;
				}
				string text = service.GetType().InvokeMember("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, service, null, CultureInfo.InvariantCulture).ToString();
				object obj = service.GetType().InvokeMember("ContainingProject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, service, null, CultureInfo.InvariantCulture);
				object obj2 = obj.GetType().InvokeMember("Object", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, obj, null, CultureInfo.InvariantCulture);
				return obj2.GetType().InvokeMember("References", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, obj2, null, CultureInfo.InvariantCulture);
			}

			// Token: 0x060031DF RID: 12767 RVA: 0x0010F44C File Offset: 0x0010D64C
			private System.Runtime.InteropServices.ComTypes.TYPELIBATTR GetTypeLibAttr()
			{
				string text = "CLSID\\" + this.clsid;
				RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(text);
				if (registryKey == null)
				{
					bool traceVerbose = DocumentDesigner.AxToolSwitch.TraceVerbose;
					throw new ArgumentException(SR.GetString("AXNotRegistered", new object[]
					{
						text.ToString()
					}));
				}
				ITypeLib typeLib = null;
				Guid empty = Guid.Empty;
				RegistryKey registryKey2 = registryKey.OpenSubKey("TypeLib");
				if (registryKey2 != null)
				{
					RegistryKey registryKey3 = registryKey.OpenSubKey("Version");
					string text2 = (string)registryKey3.GetValue("");
					int num = text2.IndexOf('.');
					short majorVersion;
					short minorVersion;
					if (num == -1)
					{
						majorVersion = short.Parse(text2, CultureInfo.InvariantCulture);
						minorVersion = 0;
					}
					else
					{
						majorVersion = short.Parse(text2.Substring(0, num), CultureInfo.InvariantCulture);
						minorVersion = short.Parse(text2.Substring(num + 1, text2.Length - num - 1), CultureInfo.InvariantCulture);
					}
					registryKey3.Close();
					object value = registryKey2.GetValue("");
					empty = new Guid((string)value);
					registryKey2.Close();
					try
					{
						typeLib = NativeMethods.LoadRegTypeLib(ref empty, majorVersion, minorVersion, Application.CurrentCulture.LCID);
					}
					catch (Exception ex)
					{
						bool enabled = AxWrapperGen.AxWrapper.Enabled;
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (typeLib == null)
				{
					RegistryKey registryKey4 = registryKey.OpenSubKey("InprocServer32");
					if (registryKey4 != null)
					{
						string typelib = (string)registryKey4.GetValue("");
						registryKey4.Close();
						typeLib = NativeMethods.LoadTypeLib(typelib);
					}
				}
				registryKey.Close();
				if (typeLib != null)
				{
					try
					{
						IntPtr invalidIntPtr = NativeMethods.InvalidIntPtr;
						typeLib.GetLibAttr(out invalidIntPtr);
						if (invalidIntPtr == NativeMethods.InvalidIntPtr)
						{
							throw new ArgumentException(SR.GetString("AXNotRegistered", new object[]
							{
								text.ToString()
							}));
						}
						System.Runtime.InteropServices.ComTypes.TYPELIBATTR result = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR)Marshal.PtrToStructure(invalidIntPtr, typeof(System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
						typeLib.ReleaseTLibAttr(invalidIntPtr);
						return result;
					}
					finally
					{
						Marshal.ReleaseComObject(typeLib);
					}
				}
				throw new ArgumentException(SR.GetString("AXNotRegistered", new object[]
				{
					text.ToString()
				}));
			}

			// Token: 0x060031E0 RID: 12768 RVA: 0x0010F684 File Offset: 0x0010D884
			protected override void Serialize(SerializationInfo info, StreamingContext context)
			{
				bool traceVerbose = DocumentDesigner.AxToolSwitch.TraceVerbose;
				base.Serialize(info, context);
				info.AddValue("Clsid", this.clsid);
			}

			// Token: 0x0400215E RID: 8542
			private string clsid;

			// Token: 0x0400215F RID: 8543
			private Type axctlType;

			// Token: 0x04002160 RID: 8544
			private string version = string.Empty;
		}

		// Token: 0x0200056E RID: 1390
		private class DocumentInheritanceService : InheritanceService
		{
			// Token: 0x060031E1 RID: 12769 RVA: 0x0010F6AA File Offset: 0x0010D8AA
			public DocumentInheritanceService(DocumentDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x060031E2 RID: 12770 RVA: 0x0010F6BC File Offset: 0x0010D8BC
			protected override bool IgnoreInheritedMember(MemberInfo member, IComponent component)
			{
				FieldInfo fieldInfo = member as FieldInfo;
				MethodInfo methodInfo = member as MethodInfo;
				bool flag;
				Type c;
				if (fieldInfo != null)
				{
					flag = (fieldInfo.IsPrivate || fieldInfo.IsAssembly);
					c = fieldInfo.FieldType;
				}
				else
				{
					if (!(methodInfo != null))
					{
						return true;
					}
					flag = (methodInfo.IsPrivate || methodInfo.IsAssembly);
					c = methodInfo.ReturnType;
				}
				if (flag)
				{
					if (typeof(Control).IsAssignableFrom(c))
					{
						Control control = null;
						if (fieldInfo != null)
						{
							control = (Control)fieldInfo.GetValue(component);
						}
						else if (methodInfo != null)
						{
							control = (Control)methodInfo.Invoke(component, null);
						}
						Control control2 = this.designer.Control;
						while (control != null && control != control2)
						{
							control = control.Parent;
						}
						if (control != null)
						{
							return false;
						}
					}
					else if (typeof(Menu).IsAssignableFrom(c))
					{
						object obj = null;
						if (fieldInfo != null)
						{
							obj = fieldInfo.GetValue(component);
						}
						else if (methodInfo != null)
						{
							obj = methodInfo.Invoke(component, null);
						}
						if (obj != null)
						{
							return false;
						}
					}
				}
				return base.IgnoreInheritedMember(member, component);
			}

			// Token: 0x04002161 RID: 8545
			private DocumentDesigner designer;
		}
	}
}
