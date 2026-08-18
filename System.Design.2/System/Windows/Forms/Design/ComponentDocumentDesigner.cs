using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002A7 RID: 679
	public class ComponentDocumentDesigner : ComponentDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, IOleDragClient, ITypeDescriptorFilterService
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x0009BC44 File Offset: 0x00099E44
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.RemoveService(typeof(IInheritanceService));
					designerHost.RemoveService(typeof(IEventHandlerService));
					designerHost.RemoveService(typeof(ISelectionUIService));
					designerHost.RemoveService(typeof(ComponentTray));
					IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentAdded -= this.OnComponentAdded;
						componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					}
				}
				if (this.selectionUIService != null)
				{
					this.selectionUIService.Dispose();
					this.selectionUIService = null;
				}
				if (this.commandSet != null)
				{
					this.commandSet.Dispose();
					this.commandSet = null;
				}
				if (this.pbrsFwd != null)
				{
					this.pbrsFwd.Dispose();
					this.pbrsFwd = null;
				}
				if (this.compositionUI != null)
				{
					this.compositionUI.Dispose();
					this.compositionUI = null;
				}
				if (this.designerExtenders != null)
				{
					this.designerExtenders.Dispose();
					this.designerExtenders = null;
				}
				if (this.inheritanceService != null)
				{
					this.inheritanceService.Dispose();
					this.inheritanceService = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0009BD90 File Offset: 0x00099F90
		public Control Control
		{
			get
			{
				return this.compositionUI;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0009BD98 File Offset: 0x00099F98
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x0009BDA0 File Offset: 0x00099FA0
		public bool TrayAutoArrange
		{
			get
			{
				return this.autoArrange;
			}
			set
			{
				this.autoArrange = value;
				this.compositionUI.AutoArrange = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0009BDB5 File Offset: 0x00099FB5
		// (set) Token: 0x06001A9B RID: 6811 RVA: 0x0009BDBD File Offset: 0x00099FBD
		public bool TrayLargeIcon
		{
			get
			{
				return this.largeIcons;
			}
			set
			{
				this.largeIcons = value;
				this.compositionUI.ShowLargeIcons = value;
			}
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0009BDD4 File Offset: 0x00099FD4
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.inheritanceService = new InheritanceService();
			ISite site = component.Site;
			IContainer container = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			IExtenderProviderService extenderProviderService = (IExtenderProviderService)this.GetService(typeof(IExtenderProviderService));
			if (extenderProviderService != null)
			{
				this.designerExtenders = new DesignerExtenders(extenderProviderService);
			}
			if (designerHost != null)
			{
				this.eventHandlerService = new EventHandlerService(null);
				this.selectionUIService = new SelectionUIService(designerHost);
				designerHost.AddService(typeof(IInheritanceService), this.inheritanceService);
				designerHost.AddService(typeof(IEventHandlerService), this.eventHandlerService);
				designerHost.AddService(typeof(ISelectionUIService), this.selectionUIService);
				this.compositionUI = new ComponentDocumentDesigner.CompositionUI(this, site);
				designerHost.AddService(typeof(ComponentTray), this.compositionUI);
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded += this.OnComponentAdded;
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				}
				ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
				if (selectionService != null)
				{
					selectionService.SetSelectedComponents(new object[]
					{
						component
					}, SelectionTypes.Auto);
				}
			}
			if (site != null)
			{
				this.commandSet = new CompositionCommandSet(this.compositionUI, site);
				container = site.Container;
			}
			this.pbrsFwd = new PbrsForward(this.compositionUI, site);
			this.inheritanceService.AddInheritedComponents(component, container);
			IServiceContainer serviceContainer = (IServiceContainer)this.GetService(typeof(IServiceContainer));
			if (serviceContainer != null)
			{
				this.delegateFilterService = (ITypeDescriptorFilterService)this.GetService(typeof(ITypeDescriptorFilterService));
				if (this.delegateFilterService != null)
				{
					serviceContainer.RemoveService(typeof(ITypeDescriptorFilterService));
				}
				serviceContainer.AddService(typeof(ITypeDescriptorFilterService), this);
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0009BFC4 File Offset: 0x0009A1C4
		private void OnComponentAdded(object sender, ComponentEventArgs ce)
		{
			if (ce.Component != base.Component)
			{
				this.compositionUI.AddComponent(ce.Component);
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0009BFE5 File Offset: 0x0009A1E5
		private void OnComponentRemoved(object sender, ComponentEventArgs ce)
		{
			this.compositionUI.RemoveComponent(ce.Component);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			properties["TrayLargeIcon"] = TypeDescriptor.CreateProperty(base.GetType(), "TrayLargeIcon", typeof(bool), new Attribute[]
			{
				BrowsableAttribute.No,
				DesignOnlyAttribute.Yes,
				CategoryAttribute.Design
			});
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x00003B0F File Offset: 0x00001D0F
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0009C050 File Offset: 0x0009A250
		bool IOleDragClient.AddComponent(IComponent component, string name, bool firstAdd)
		{
			IContainer container = base.Component.Site.Container;
			if (container != null && name != null && container.Components[name] != null)
			{
				name = null;
			}
			IContainer container2 = null;
			bool flag = false;
			if (!firstAdd)
			{
				if (component.Site != null)
				{
					container2 = component.Site.Container;
					if (container2 != container)
					{
						container2.Remove(component);
						flag = true;
					}
				}
				if (container2 != container)
				{
					container.Add(component, name);
				}
			}
			if (flag)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IComponentInitializer componentInitializer = designerHost.GetDesigner(component) as IComponentInitializer;
					if (componentInitializer != null)
					{
						componentInitializer.InitializeExistingComponent(null);
					}
				}
			}
			return container2 != container || !firstAdd;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0009C0F9 File Offset: 0x0009A2F9
		Control IOleDragClient.GetControlForComponent(object component)
		{
			if (this.compositionUI != null)
			{
				return ((IOleDragClient)this.compositionUI).GetControlForComponent(component);
			}
			return null;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0009C111 File Offset: 0x0009A311
		Control IOleDragClient.GetDesignerControl()
		{
			if (this.compositionUI != null)
			{
				return ((IOleDragClient)this.compositionUI).GetDesignerControl();
			}
			return null;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00003B0F File Offset: 0x00001D0F
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			return true;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x0009C128 File Offset: 0x0009A328
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

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0009C138 File Offset: 0x0009A338
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			if (technology != ViewTechnology.Default && technology != ViewTechnology.WindowsForms)
			{
				throw new ArgumentException();
			}
			return this.compositionUI;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00003B0F File Offset: 0x00001D0F
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0009C150 File Offset: 0x0009A350
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			this.compositionUI.CreateComponentFromTool(tool);
			IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			if (toolboxService != null)
			{
				toolboxService.SelectedToolboxItemUsed();
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0009C188 File Offset: 0x0009A388
		bool ITypeDescriptorFilterService.FilterAttributes(IComponent component, IDictionary attributes)
		{
			return this.delegateFilterService == null || this.delegateFilterService.FilterAttributes(component, attributes);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0009C1A1 File Offset: 0x0009A3A1
		bool ITypeDescriptorFilterService.FilterEvents(IComponent component, IDictionary events)
		{
			return this.delegateFilterService == null || this.delegateFilterService.FilterEvents(component, events);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0009C1BC File Offset: 0x0009A3BC
		bool ITypeDescriptorFilterService.FilterProperties(IComponent component, IDictionary properties)
		{
			if (this.delegateFilterService != null)
			{
				this.delegateFilterService.FilterProperties(component, properties);
			}
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["Location"];
			if (propertyDescriptor != null)
			{
				properties["Location"] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
				{
					BrowsableAttribute.No
				});
			}
			return true;
		}

		// Token: 0x040015FD RID: 5629
		private ComponentDocumentDesigner.CompositionUI compositionUI;

		// Token: 0x040015FE RID: 5630
		private CompositionCommandSet commandSet;

		// Token: 0x040015FF RID: 5631
		private IEventHandlerService eventHandlerService;

		// Token: 0x04001600 RID: 5632
		private InheritanceService inheritanceService;

		// Token: 0x04001601 RID: 5633
		private SelectionUIService selectionUIService;

		// Token: 0x04001602 RID: 5634
		private DesignerExtenders designerExtenders;

		// Token: 0x04001603 RID: 5635
		private ITypeDescriptorFilterService delegateFilterService;

		// Token: 0x04001604 RID: 5636
		private bool largeIcons;

		// Token: 0x04001605 RID: 5637
		private bool autoArrange = true;

		// Token: 0x04001606 RID: 5638
		private PbrsForward pbrsFwd;

		// Token: 0x0200053D RID: 1341
		private class WatermarkLabel : LinkLabel
		{
			// Token: 0x060030C3 RID: 12483 RVA: 0x0010BBE8 File Offset: 0x00109DE8
			public WatermarkLabel(ComponentDocumentDesigner.CompositionUI compositionUI)
			{
				this.compositionUI = compositionUI;
			}

			// Token: 0x060030C4 RID: 12484 RVA: 0x0010BBF8 File Offset: 0x00109DF8
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 32)
				{
					if (msg != 132)
					{
						base.WndProc(ref m);
						return;
					}
					Point point = base.PointToClient(new Point((int)((long)m.LParam)));
					if (base.PointInLink(point.X, point.Y) == null)
					{
						m.Result = (IntPtr)(-1);
						return;
					}
					base.WndProc(ref m);
					return;
				}
				else
				{
					if (base.OverrideCursor == null)
					{
						this.compositionUI.SetCursor();
						return;
					}
					base.WndProc(ref m);
					return;
				}
			}

			// Token: 0x04002106 RID: 8454
			private ComponentDocumentDesigner.CompositionUI compositionUI;
		}

		// Token: 0x0200053E RID: 1342
		private class CompositionUI : ComponentTray
		{
			// Token: 0x060030C5 RID: 12485 RVA: 0x0010BC88 File Offset: 0x00109E88
			public CompositionUI(ComponentDocumentDesigner compositionDesigner, IServiceProvider provider) : base(compositionDesigner, provider)
			{
				this.compositionDesigner = compositionDesigner;
				this.serviceProvider = provider;
				this.watermark = new ComponentDocumentDesigner.WatermarkLabel(this);
				this.watermark.Font = new Font(this.watermark.Font.FontFamily, 11f);
				this.watermark.TextAlign = ContentAlignment.MiddleCenter;
				this.watermark.LinkClicked += this.OnLinkClick;
				this.watermark.Dock = DockStyle.Fill;
				this.watermark.TabStop = false;
				this.watermark.Text = SR.GetString("CompositionDesignerWaterMark");
				IUIService iuiservice = (IUIService)compositionDesigner.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					if (iuiservice.Styles["VsColorPanelGradientDark"] is Color)
					{
						this.BackColor = (Color)iuiservice.Styles["VsColorPanelGradientDark"];
					}
					if (iuiservice.Styles["VsColorPanelText"] is Color)
					{
						this.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
					}
				}
				try
				{
					string @string = SR.GetString("CompositionDesignerWaterMarkFirstLink");
					int start = this.watermark.Text.IndexOf(@string);
					int length = @string.Length;
					this.watermark.Links.Add(start, length, "Toolbox");
					@string = SR.GetString("CompositionDesignerWaterMarkSecondLink");
					start = this.watermark.Text.IndexOf(@string);
					length = @string.Length;
					this.watermark.Links.Add(start, length, "CodeView");
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
				}
				base.Controls.Add(this.watermark);
			}

			// Token: 0x060030C6 RID: 12486 RVA: 0x0010BE58 File Offset: 0x0010A058
			public override void AddComponent(IComponent component)
			{
				base.AddComponent(component);
				if (base.Controls.Count > 0)
				{
					this.watermark.Visible = false;
				}
			}

			// Token: 0x060030C7 RID: 12487 RVA: 0x00003B0F File Offset: 0x00001D0F
			protected override bool CanCreateComponentFromTool(ToolboxItem tool)
			{
				return true;
			}

			// Token: 0x060030C8 RID: 12488 RVA: 0x0010BE7B File Offset: 0x0010A07B
			internal override OleDragDropHandler GetOleDragHandler()
			{
				if (this.oleDragDropHandler == null)
				{
					this.oleDragDropHandler = new OleDragDropHandler(this.DragHandler, this.serviceProvider, this);
				}
				return this.oleDragDropHandler;
			}

			// Token: 0x1700096D RID: 2413
			// (get) Token: 0x060030C9 RID: 12489 RVA: 0x0010BEA3 File Offset: 0x0010A0A3
			internal override SelectionUIHandler DragHandler
			{
				get
				{
					if (this.dragHandler == null)
					{
						this.dragHandler = new ComponentDocumentDesigner.CompositionUI.CompositionSelectionUIHandler(this.compositionDesigner);
					}
					return this.dragHandler;
				}
			}

			// Token: 0x060030CA RID: 12490 RVA: 0x0010BEC4 File Offset: 0x0010A0C4
			private void OnLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
			{
				IUIService iuiservice = (IUIService)this.compositionDesigner.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					string a = (string)e.Link.LinkData;
					if (a == "ServerExplorer")
					{
						iuiservice.ShowToolWindow(StandardToolWindows.ServerExplorer);
						return;
					}
					if (a == "Toolbox")
					{
						iuiservice.ShowToolWindow(StandardToolWindows.Toolbox);
						return;
					}
					IEventBindingService eventBindingService = (IEventBindingService)this.serviceProvider.GetService(typeof(IEventBindingService));
					if (eventBindingService != null)
					{
						eventBindingService.ShowCode();
					}
				}
			}

			// Token: 0x060030CB RID: 12491 RVA: 0x0010BF59 File Offset: 0x0010A159
			internal void SetCursor()
			{
				if (this.toolboxService == null)
				{
					this.toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				}
				if (this.toolboxService == null || !this.toolboxService.SetCursor())
				{
					base.OnSetCursor();
				}
			}

			// Token: 0x060030CC RID: 12492 RVA: 0x0010BF9C File Offset: 0x0010A19C
			protected override void OnDragDrop(DragEventArgs de)
			{
				if (base.ClientRectangle.Contains(base.PointToClient(new Point(de.X, de.Y))))
				{
					base.OnDragDrop(de);
					return;
				}
				de.Effect = DragDropEffects.None;
			}

			// Token: 0x060030CD RID: 12493 RVA: 0x0010BFE0 File Offset: 0x0010A1E0
			protected override void OnDragOver(DragEventArgs de)
			{
				if (base.ClientRectangle.Contains(base.PointToClient(new Point(de.X, de.Y))))
				{
					base.OnDragOver(de);
					return;
				}
				de.Effect = DragDropEffects.None;
			}

			// Token: 0x060030CE RID: 12494 RVA: 0x0010C024 File Offset: 0x0010A224
			protected override void OnResize(EventArgs e)
			{
				base.OnResize(e);
				if (this.watermark != null)
				{
					this.watermark.Location = new Point(0, base.Size.Height / 2);
					this.watermark.Size = new Size(base.Width, base.Size.Height / 2);
				}
			}

			// Token: 0x060030CF RID: 12495 RVA: 0x0010C087 File Offset: 0x0010A287
			protected override void OnSetCursor()
			{
				this.SetCursor();
			}

			// Token: 0x060030D0 RID: 12496 RVA: 0x0010C08F File Offset: 0x0010A28F
			public override void RemoveComponent(IComponent component)
			{
				base.RemoveComponent(component);
				if (base.Controls.Count == 1)
				{
					this.watermark.Visible = true;
				}
			}

			// Token: 0x060030D1 RID: 12497 RVA: 0x0010C0B4 File Offset: 0x0010A2B4
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				base.WndProc(ref m);
			}

			// Token: 0x04002107 RID: 8455
			private ComponentDocumentDesigner.WatermarkLabel watermark;

			// Token: 0x04002108 RID: 8456
			private const int bannerHeight = 40;

			// Token: 0x04002109 RID: 8457
			private const int borderWidth = 10;

			// Token: 0x0400210A RID: 8458
			private IToolboxService toolboxService;

			// Token: 0x0400210B RID: 8459
			private ComponentDocumentDesigner compositionDesigner;

			// Token: 0x0400210C RID: 8460
			private IServiceProvider serviceProvider;

			// Token: 0x0400210D RID: 8461
			private SelectionUIHandler dragHandler;

			// Token: 0x020005EB RID: 1515
			private class CompositionSelectionUIHandler : SelectionUIHandler
			{
				// Token: 0x060034D3 RID: 13523 RVA: 0x0011EE8B File Offset: 0x0011D08B
				public CompositionSelectionUIHandler(ComponentDocumentDesigner compositionDesigner)
				{
					this.compositionDesigner = compositionDesigner;
				}

				// Token: 0x060034D4 RID: 13524 RVA: 0x0011EE9A File Offset: 0x0011D09A
				protected override IComponent GetComponent()
				{
					return this.compositionDesigner.Component;
				}

				// Token: 0x060034D5 RID: 13525 RVA: 0x0011EEA7 File Offset: 0x0011D0A7
				protected override Control GetControl()
				{
					return this.compositionDesigner.Control;
				}

				// Token: 0x060034D6 RID: 13526 RVA: 0x0009A195 File Offset: 0x00098395
				protected override Control GetControl(IComponent component)
				{
					return ComponentTray.TrayControl.FromComponent(component);
				}

				// Token: 0x060034D7 RID: 13527 RVA: 0x0011EEB4 File Offset: 0x0011D0B4
				protected override Size GetCurrentSnapSize()
				{
					return new Size(8, 8);
				}

				// Token: 0x060034D8 RID: 13528 RVA: 0x0011EEBD File Offset: 0x0011D0BD
				protected override object GetService(Type serviceType)
				{
					return this.compositionDesigner.GetService(serviceType);
				}

				// Token: 0x060034D9 RID: 13529 RVA: 0x0000445B File Offset: 0x0000265B
				protected override bool GetShouldSnapToGrid()
				{
					return false;
				}

				// Token: 0x060034DA RID: 13530 RVA: 0x0011EECC File Offset: 0x0011D0CC
				public override Rectangle GetUpdatedRect(Rectangle originalRect, Rectangle dragRect, bool updateSize)
				{
					Rectangle result;
					if (this.GetShouldSnapToGrid())
					{
						Rectangle rectangle = dragRect;
						int x = dragRect.X;
						int y = dragRect.Y;
						int num = dragRect.X + dragRect.Width;
						int num2 = dragRect.Y + dragRect.Height;
						Size size = new Size(8, 8);
						int num3 = size.Width / 2 * ((x < 0) ? -1 : 1);
						int num4 = size.Height / 2 * ((y < 0) ? -1 : 1);
						rectangle.X = (x + num3) / size.Width * size.Width;
						rectangle.Y = (y + num4) / size.Height * size.Height;
						num3 = size.Width / 2 * ((num < 0) ? -1 : 1);
						num4 = size.Height / 2 * ((num2 < 0) ? -1 : 1);
						if (updateSize)
						{
							rectangle.Width = (num + num3) / size.Width * size.Width - rectangle.X;
							rectangle.Height = (num2 + num4) / size.Height * size.Height - rectangle.Y;
						}
						result = rectangle;
					}
					else
					{
						result = dragRect;
					}
					return result;
				}

				// Token: 0x060034DB RID: 13531 RVA: 0x0011EFFB File Offset: 0x0011D1FB
				public override void SetCursor()
				{
					this.compositionDesigner.compositionUI.OnSetCursor();
				}

				// Token: 0x0400233D RID: 9021
				private ComponentDocumentDesigner compositionDesigner;
			}
		}
	}
}
