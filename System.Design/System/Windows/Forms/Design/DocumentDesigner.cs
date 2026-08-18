using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
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
	// Token: 0x0200022A RID: 554
	[ToolboxItemFilter("System.Windows.Forms")]
	public class DocumentDesigner : ScrollableControlDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, IOleDragClient
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0006C8A4 File Offset: 0x0006B8A4
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x0006C8CC File Offset: 0x0006B8CC
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0006C8F0 File Offset: 0x0006B8F0
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0006C914 File Offset: 0x0006B914
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

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006C983 File Offset: 0x0006B983
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x0006C990 File Offset: 0x0006B990
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

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0006C9C4 File Offset: 0x0006B9C4
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x0006C9DB File Offset: 0x0006B9DB
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

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0006C9F4 File Offset: 0x0006B9F4
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				return selectionRules & ~(SelectionRules.Moveable | SelectionRules.TopSizeable | SelectionRules.LeftSizeable);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0006CA14 File Offset: 0x0006BA14
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

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0006CA6F File Offset: 0x0006BA6F
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x0006CA77 File Offset: 0x0006BA77
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0006CA99 File Offset: 0x0006BA99
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x0006CAA1 File Offset: 0x0006BAA1
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0006CAC3 File Offset: 0x0006BAC3
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0006CADF File Offset: 0x0006BADF
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

		// Token: 0x0600150B RID: 5387 RVA: 0x0006CB04 File Offset: 0x0006BB04
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

		// Token: 0x0600150C RID: 5388 RVA: 0x0006CB34 File Offset: 0x0006BB34
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

		// Token: 0x0600150D RID: 5389 RVA: 0x0006CBB8 File Offset: 0x0006BBB8
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

		// Token: 0x0600150E RID: 5390 RVA: 0x0006CC7C File Offset: 0x0006BC7C
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

		// Token: 0x0600150F RID: 5391 RVA: 0x0006CCBC File Offset: 0x0006BCBC
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

		// Token: 0x06001510 RID: 5392 RVA: 0x0006D098 File Offset: 0x0006C098
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

		// Token: 0x06001511 RID: 5393 RVA: 0x0006D2D0 File Offset: 0x0006C2D0
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

		// Token: 0x06001512 RID: 5394 RVA: 0x0006D3BC File Offset: 0x0006C3BC
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0006D3C0 File Offset: 0x0006C3C0
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
				inheritanceService.AddInheritedComponents(component, component.Site.Container);
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

		// Token: 0x06001514 RID: 5396 RVA: 0x0006D720 File Offset: 0x0006C720
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

		// Token: 0x06001515 RID: 5397 RVA: 0x0006D7D4 File Offset: 0x0006C7D4
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

		// Token: 0x06001516 RID: 5398 RVA: 0x0006D83C File Offset: 0x0006C83C
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

		// Token: 0x06001517 RID: 5399 RVA: 0x0006D928 File Offset: 0x0006C928
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

		// Token: 0x06001518 RID: 5400 RVA: 0x0006DA8C File Offset: 0x0006CA8C
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

		// Token: 0x06001519 RID: 5401 RVA: 0x0006DB68 File Offset: 0x0006CB68
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

		// Token: 0x0600151A RID: 5402 RVA: 0x0006DC20 File Offset: 0x0006CC20
		protected override void OnCreateHandle()
		{
			if (this.inheritanceService != null)
			{
				base.OnCreateHandle();
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0006DC30 File Offset: 0x0006CC30
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

		// Token: 0x0600151C RID: 5404 RVA: 0x0006DC90 File Offset: 0x0006CC90
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

		// Token: 0x0600151D RID: 5405 RVA: 0x0006DCE4 File Offset: 0x0006CCE4
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

		// Token: 0x0600151E RID: 5406 RVA: 0x0006DD4C File Offset: 0x0006CD4C
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			Control control = this.Control;
			if (control != null && control.IsHandleCreated)
			{
				NativeMethods.SendMessage(control.Handle, 134, 0, 0);
				SafeNativeMethods.RedrawWindow(control.Handle, null, IntPtr.Zero, 1024);
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0006DD98 File Offset: 0x0006CD98
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

		// Token: 0x06001520 RID: 5408 RVA: 0x0006DE1C File Offset: 0x0006CE1C
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

		// Token: 0x06001521 RID: 5409 RVA: 0x0006DE80 File Offset: 0x0006CE80
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

		// Token: 0x06001522 RID: 5410 RVA: 0x0006E03C File Offset: 0x0006D03C
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

		// Token: 0x06001523 RID: 5411 RVA: 0x0006E11C File Offset: 0x0006D11C
		protected virtual void EnsureMenuEditorService(IComponent c)
		{
			if (this.menuEditorService == null && c is ContextMenu)
			{
				this.menuEditorService = (IMenuEditorService)this.GetService(typeof(IMenuEditorService));
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0006E14C File Offset: 0x0006D14C
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

		// Token: 0x06001525 RID: 5413 RVA: 0x0006E3CF File Offset: 0x0006D3CF
		private void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0006E3DC File Offset: 0x0006D3DC
		private bool ShouldSerializeAutoScaleDimensions()
		{
			return !this.initializing && this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0006E3FC File Offset: 0x0006D3FC
		private bool ShouldSerializeAutoScaleMode()
		{
			return !this.initializing && base.ShadowProperties.Contains("AutoScaleMode");
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006E418 File Offset: 0x0006D418
		private bool ShouldSerializeBackColor()
		{
			return base.ShadowProperties.Contains("BackColor") && !((Color)base.ShadowProperties["BackColor"]).IsEmpty;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0006E45C File Offset: 0x0006D45C
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
			catch
			{
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0006E534 File Offset: 0x0006D534
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

		// Token: 0x0600152B RID: 5419 RVA: 0x0006E551 File Offset: 0x0006D551
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			if (technology != ViewTechnology.Default && technology != ViewTechnology.WindowsForms)
			{
				throw new ArgumentException();
			}
			return this.frame;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0006E567 File Offset: 0x0006D567
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return this.GetToolSupported(tool);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0006E570 File Offset: 0x0006D570
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			this.ToolPicked(tool);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0006E57C File Offset: 0x0006D57C
		private unsafe void WmWindowPosChanged(ref Message m)
		{
			NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)((void*)m.LParam);
			if ((ptr->flags & 1) == 0 && this.menuEditorService != null)
			{
				base.BehaviorService.SyncSelection();
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0006E5B4 File Offset: 0x0006D5B4
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

		// Token: 0x0400125D RID: 4701
		private DesignerFrame frame;

		// Token: 0x0400125E RID: 4702
		private ControlCommandSet commandSet;

		// Token: 0x0400125F RID: 4703
		private InheritanceService inheritanceService;

		// Token: 0x04001260 RID: 4704
		private EventHandlerService eventHandlerService;

		// Token: 0x04001261 RID: 4705
		private DesignBindingValueUIHandler designBindingValueUIHandler;

		// Token: 0x04001262 RID: 4706
		private BehaviorService behaviorService;

		// Token: 0x04001263 RID: 4707
		private SelectionManager selectionManager;

		// Token: 0x04001264 RID: 4708
		private DesignerExtenders designerExtenders;

		// Token: 0x04001265 RID: 4709
		private InheritanceUI inheritanceUI;

		// Token: 0x04001266 RID: 4710
		private PbrsForward pbrsFwd;

		// Token: 0x04001267 RID: 4711
		private ArrayList suspendedComponents;

		// Token: 0x04001268 RID: 4712
		private UndoEngine undoEngine;

		// Token: 0x04001269 RID: 4713
		private bool initializing;

		// Token: 0x0400126A RID: 4714
		private bool queriedTabOrder;

		// Token: 0x0400126B RID: 4715
		private MenuCommand tabOrderCommand;

		// Token: 0x0400126C RID: 4716
		protected IMenuEditorService menuEditorService;

		// Token: 0x0400126D RID: 4717
		private ComponentTray componentTray;

		// Token: 0x0400126E RID: 4718
		private int trayHeight = 80;

		// Token: 0x0400126F RID: 4719
		private bool trayLargeIcon;

		// Token: 0x04001270 RID: 4720
		private bool trayAutoArrange;

		// Token: 0x04001271 RID: 4721
		private bool trayLayoutSuspended;

		// Token: 0x04001272 RID: 4722
		private static Guid htmlDesignTime = new Guid("73CEF3DD-AE85-11CF-A406-00AA00C00940");

		// Token: 0x04001273 RID: 4723
		private Hashtable axTools;

		// Token: 0x04001274 RID: 4724
		private static TraceSwitch AxToolSwitch = new TraceSwitch("AxTool", "ActiveX Toolbox Tracing");

		// Token: 0x04001275 RID: 4725
		private static readonly string axClipFormat = "CLSID";

		// Token: 0x04001276 RID: 4726
		private ToolboxItemCreatorCallback toolboxCreator;

		// Token: 0x0200022B RID: 555
		[Serializable]
		private class AxToolboxItem : ToolboxItem
		{
			// Token: 0x06001532 RID: 5426 RVA: 0x0006E651 File Offset: 0x0006D651
			public AxToolboxItem(string clsid) : base(typeof(AxHost))
			{
				this.clsid = clsid;
				base.Company = null;
				this.LoadVersionInfo();
			}

			// Token: 0x06001533 RID: 5427 RVA: 0x0006E682 File Offset: 0x0006D682
			private AxToolboxItem(SerializationInfo info, StreamingContext context)
			{
				this.Deserialize(info, context);
			}

			// Token: 0x1700036B RID: 875
			// (get) Token: 0x06001534 RID: 5428 RVA: 0x0006E69D File Offset: 0x0006D69D
			public override string ComponentType
			{
				get
				{
					return SR.GetString("Ax_Control");
				}
			}

			// Token: 0x1700036C RID: 876
			// (get) Token: 0x06001535 RID: 5429 RVA: 0x0006E6A9 File Offset: 0x0006D6A9
			public override string Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x0006E6B4 File Offset: 0x0006D6B4
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
					}
				}
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x0006E708 File Offset: 0x0006D708
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
						references.GetType().InvokeMember("AddActiveX", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, references, array2, CultureInfo.InvariantCulture);
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
					catch
					{
						throw;
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
				catch
				{
					throw;
				}
				return array;
			}

			// Token: 0x06001538 RID: 5432 RVA: 0x0006E8AC File Offset: 0x0006D8AC
			protected override void Deserialize(SerializationInfo info, StreamingContext context)
			{
				base.Deserialize(info, context);
				this.clsid = info.GetString("Clsid");
			}

			// Token: 0x06001539 RID: 5433 RVA: 0x0006E8C8 File Offset: 0x0006D8C8
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

			// Token: 0x0600153A RID: 5434 RVA: 0x0006E940 File Offset: 0x0006D940
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

			// Token: 0x0600153B RID: 5435 RVA: 0x0006E9B0 File Offset: 0x0006D9B0
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
				service.GetType().InvokeMember("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, service, null, CultureInfo.InvariantCulture).ToString();
				object obj = service.GetType().InvokeMember("ContainingProject", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, service, null, CultureInfo.InvariantCulture);
				object obj2 = obj.GetType().InvokeMember("Object", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, obj, null, CultureInfo.InvariantCulture);
				return obj2.GetType().InvokeMember("References", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, obj2, null, CultureInfo.InvariantCulture);
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0006EA5C File Offset: 0x0006DA5C
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
				Guid guid = Guid.Empty;
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
					guid = new Guid((string)value);
					registryKey2.Close();
					try
					{
						typeLib = NativeMethods.LoadRegTypeLib(ref guid, majorVersion, minorVersion, Application.CurrentCulture.LCID);
					}
					catch (Exception ex)
					{
						bool enabled = AxWrapperGen.AxWrapper.Enabled;
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					catch
					{
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

			// Token: 0x0600153D RID: 5437 RVA: 0x0006ECB8 File Offset: 0x0006DCB8
			protected override void Serialize(SerializationInfo info, StreamingContext context)
			{
				bool traceVerbose = DocumentDesigner.AxToolSwitch.TraceVerbose;
				base.Serialize(info, context);
				info.AddValue("Clsid", this.clsid);
			}

			// Token: 0x04001277 RID: 4727
			private string clsid;

			// Token: 0x04001278 RID: 4728
			private Type axctlType;

			// Token: 0x04001279 RID: 4729
			private string version = string.Empty;
		}

		// Token: 0x0200022C RID: 556
		private class DocumentInheritanceService : InheritanceService
		{
			// Token: 0x0600153E RID: 5438 RVA: 0x0006ECDE File Offset: 0x0006DCDE
			public DocumentInheritanceService(DocumentDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x0600153F RID: 5439 RVA: 0x0006ECF0 File Offset: 0x0006DCF0
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
					if (methodInfo == null)
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

			// Token: 0x0400127A RID: 4730
			private DocumentDesigner designer;
		}
	}
}
