using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001C6 RID: 454
	public class ComponentDocumentDesigner : ComponentDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, IOleDragClient, ITypeDescriptorFilterService
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x000565CC File Offset: 0x000555CC
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

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00056718 File Offset: 0x00055718
		public Control Control
		{
			get
			{
				return this.compositionUI;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00056720 File Offset: 0x00055720
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x00056728 File Offset: 0x00055728
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

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0005673D File Offset: 0x0005573D
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x00056745 File Offset: 0x00055745
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

		// Token: 0x060011BA RID: 4538 RVA: 0x0005675A File Offset: 0x0005575A
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00056760 File Offset: 0x00055760
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

		// Token: 0x060011BC RID: 4540 RVA: 0x00056955 File Offset: 0x00055955
		private void OnComponentAdded(object sender, ComponentEventArgs ce)
		{
			if (ce.Component != base.Component)
			{
				this.compositionUI.AddComponent(ce.Component);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00056976 File Offset: 0x00055976
		private void OnComponentRemoved(object sender, ComponentEventArgs ce)
		{
			this.compositionUI.RemoveComponent(ce.Component);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0005698C File Offset: 0x0005598C
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x000569E5 File Offset: 0x000559E5
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000569E8 File Offset: 0x000559E8
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
			return container2 != container;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00056A8E File Offset: 0x00055A8E
		Control IOleDragClient.GetControlForComponent(object component)
		{
			if (this.compositionUI != null)
			{
				return ((IOleDragClient)this.compositionUI).GetControlForComponent(component);
			}
			return null;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00056AA6 File Offset: 0x00055AA6
		Control IOleDragClient.GetDesignerControl()
		{
			if (this.compositionUI != null)
			{
				return ((IOleDragClient)this.compositionUI).GetDesignerControl();
			}
			return null;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00056ABD File Offset: 0x00055ABD
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			return true;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00056AC0 File Offset: 0x00055AC0
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

		// Token: 0x060011C5 RID: 4549 RVA: 0x00056ADD File Offset: 0x00055ADD
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			if (technology != ViewTechnology.Default && technology != ViewTechnology.WindowsForms)
			{
				throw new ArgumentException();
			}
			return this.compositionUI;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00056AF3 File Offset: 0x00055AF3
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00056AF8 File Offset: 0x00055AF8
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			this.compositionUI.CreateComponentFromTool(tool);
			IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
			if (toolboxService != null)
			{
				toolboxService.SelectedToolboxItemUsed();
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00056B30 File Offset: 0x00055B30
		bool ITypeDescriptorFilterService.FilterAttributes(IComponent component, IDictionary attributes)
		{
			return this.delegateFilterService == null || this.delegateFilterService.FilterAttributes(component, attributes);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00056B49 File Offset: 0x00055B49
		bool ITypeDescriptorFilterService.FilterEvents(IComponent component, IDictionary events)
		{
			return this.delegateFilterService == null || this.delegateFilterService.FilterEvents(component, events);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00056B64 File Offset: 0x00055B64
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

		// Token: 0x040010DD RID: 4317
		private ComponentDocumentDesigner.CompositionUI compositionUI;

		// Token: 0x040010DE RID: 4318
		private CompositionCommandSet commandSet;

		// Token: 0x040010DF RID: 4319
		private IEventHandlerService eventHandlerService;

		// Token: 0x040010E0 RID: 4320
		private InheritanceService inheritanceService;

		// Token: 0x040010E1 RID: 4321
		private SelectionUIService selectionUIService;

		// Token: 0x040010E2 RID: 4322
		private DesignerExtenders designerExtenders;

		// Token: 0x040010E3 RID: 4323
		private ITypeDescriptorFilterService delegateFilterService;

		// Token: 0x040010E4 RID: 4324
		private bool largeIcons;

		// Token: 0x040010E5 RID: 4325
		private bool autoArrange = true;

		// Token: 0x040010E6 RID: 4326
		private PbrsForward pbrsFwd;

		// Token: 0x020001C7 RID: 455
		private class WatermarkLabel : LinkLabel
		{
			// Token: 0x060011CC RID: 4556 RVA: 0x00056BD2 File Offset: 0x00055BD2
			public WatermarkLabel(ComponentDocumentDesigner.CompositionUI compositionUI)
			{
				this.compositionUI = compositionUI;
			}

			// Token: 0x060011CD RID: 4557 RVA: 0x00056BE4 File Offset: 0x00055BE4
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
					Point point = base.PointToClient(new Point((int)m.LParam));
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

			// Token: 0x040010E7 RID: 4327
			private ComponentDocumentDesigner.CompositionUI compositionUI;
		}

		// Token: 0x020001C8 RID: 456
		private class CompositionUI : ComponentTray
		{
			// Token: 0x060011CE RID: 4558 RVA: 0x00056C70 File Offset: 0x00055C70
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
				catch
				{
				}
				base.Controls.Add(this.watermark);
			}

			// Token: 0x060011CF RID: 4559 RVA: 0x00056DCC File Offset: 0x00055DCC
			public override void AddComponent(IComponent component)
			{
				base.AddComponent(component);
				if (base.Controls.Count > 0)
				{
					this.watermark.Visible = false;
				}
			}

			// Token: 0x060011D0 RID: 4560 RVA: 0x00056DEF File Offset: 0x00055DEF
			protected override bool CanCreateComponentFromTool(ToolboxItem tool)
			{
				return true;
			}

			// Token: 0x060011D1 RID: 4561 RVA: 0x00056DF2 File Offset: 0x00055DF2
			internal override OleDragDropHandler GetOleDragHandler()
			{
				if (this.oleDragDropHandler == null)
				{
					this.oleDragDropHandler = new OleDragDropHandler(this.DragHandler, this.serviceProvider, this);
				}
				return this.oleDragDropHandler;
			}

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00056E1A File Offset: 0x00055E1A
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

			// Token: 0x060011D3 RID: 4563 RVA: 0x00056E3C File Offset: 0x00055E3C
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

			// Token: 0x060011D4 RID: 4564 RVA: 0x00056ED1 File Offset: 0x00055ED1
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

			// Token: 0x060011D5 RID: 4565 RVA: 0x00056F14 File Offset: 0x00055F14
			protected override void OnDragDrop(DragEventArgs de)
			{
				if (base.ClientRectangle.Contains(base.PointToClient(new Point(de.X, de.Y))))
				{
					base.OnDragDrop(de);
					return;
				}
				de.Effect = DragDropEffects.None;
			}

			// Token: 0x060011D6 RID: 4566 RVA: 0x00056F58 File Offset: 0x00055F58
			protected override void OnDragOver(DragEventArgs de)
			{
				if (base.ClientRectangle.Contains(base.PointToClient(new Point(de.X, de.Y))))
				{
					base.OnDragOver(de);
					return;
				}
				de.Effect = DragDropEffects.None;
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x00056F9C File Offset: 0x00055F9C
			protected override void OnResize(EventArgs e)
			{
				base.OnResize(e);
				if (this.watermark != null)
				{
					this.watermark.Location = new Point(0, base.Size.Height / 2);
					this.watermark.Size = new Size(base.Width, base.Size.Height / 2);
				}
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x00056FFF File Offset: 0x00055FFF
			protected override void OnSetCursor()
			{
				this.SetCursor();
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x00057007 File Offset: 0x00056007
			public override void RemoveComponent(IComponent component)
			{
				base.RemoveComponent(component);
				if (base.Controls.Count == 1)
				{
					this.watermark.Visible = true;
				}
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0005702A File Offset: 0x0005602A
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				base.WndProc(ref m);
			}

			// Token: 0x040010E8 RID: 4328
			private const int bannerHeight = 40;

			// Token: 0x040010E9 RID: 4329
			private const int borderWidth = 10;

			// Token: 0x040010EA RID: 4330
			private ComponentDocumentDesigner.WatermarkLabel watermark;

			// Token: 0x040010EB RID: 4331
			private IToolboxService toolboxService;

			// Token: 0x040010EC RID: 4332
			private ComponentDocumentDesigner compositionDesigner;

			// Token: 0x040010ED RID: 4333
			private IServiceProvider serviceProvider;

			// Token: 0x040010EE RID: 4334
			private SelectionUIHandler dragHandler;

			// Token: 0x020001C9 RID: 457
			private class CompositionSelectionUIHandler : SelectionUIHandler
			{
				// Token: 0x060011DB RID: 4571 RVA: 0x0005703A File Offset: 0x0005603A
				public CompositionSelectionUIHandler(ComponentDocumentDesigner compositionDesigner)
				{
					this.compositionDesigner = compositionDesigner;
				}

				// Token: 0x060011DC RID: 4572 RVA: 0x00057049 File Offset: 0x00056049
				protected override IComponent GetComponent()
				{
					return this.compositionDesigner.Component;
				}

				// Token: 0x060011DD RID: 4573 RVA: 0x00057056 File Offset: 0x00056056
				protected override Control GetControl()
				{
					return this.compositionDesigner.Control;
				}

				// Token: 0x060011DE RID: 4574 RVA: 0x00057063 File Offset: 0x00056063
				protected override Control GetControl(IComponent component)
				{
					return ComponentTray.TrayControl.FromComponent(component);
				}

				// Token: 0x060011DF RID: 4575 RVA: 0x0005706B File Offset: 0x0005606B
				protected override Size GetCurrentSnapSize()
				{
					return new Size(8, 8);
				}

				// Token: 0x060011E0 RID: 4576 RVA: 0x00057074 File Offset: 0x00056074
				protected override object GetService(Type serviceType)
				{
					return this.compositionDesigner.GetService(serviceType);
				}

				// Token: 0x060011E1 RID: 4577 RVA: 0x00057082 File Offset: 0x00056082
				protected override bool GetShouldSnapToGrid()
				{
					return false;
				}

				// Token: 0x060011E2 RID: 4578 RVA: 0x00057088 File Offset: 0x00056088
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

				// Token: 0x060011E3 RID: 4579 RVA: 0x000571B7 File Offset: 0x000561B7
				public override void SetCursor()
				{
					this.compositionDesigner.compositionUI.OnSetCursor();
				}

				// Token: 0x040010EF RID: 4335
				private ComponentDocumentDesigner compositionDesigner;
			}
		}
	}
}
