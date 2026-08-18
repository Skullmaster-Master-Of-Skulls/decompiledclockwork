using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C3 RID: 451
	internal sealed class DesignerHost : Container, IDesignerLoaderHost2, IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider, IDesignerHostTransactionState, IComponentChangeService, IReflect
	{
		// Token: 0x06001052 RID: 4178 RVA: 0x0005BEAC File Offset: 0x0005A0AC
		public DesignerHost(DesignSurface surface)
		{
			this._surface = surface;
			this._state = default(BitVector32);
			this._designers = new Hashtable();
			this._events = new EventHandlerList();
			DesignSurfaceServiceContainer designSurfaceServiceContainer = this.GetService(typeof(DesignSurfaceServiceContainer)) as DesignSurfaceServiceContainer;
			if (designSurfaceServiceContainer != null)
			{
				foreach (Type serviceType in DesignerHost.DefaultServices)
				{
					designSurfaceServiceContainer.AddFixedService(serviceType, this);
				}
				return;
			}
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer != null)
			{
				foreach (Type serviceType2 in DesignerHost.DefaultServices)
				{
					serviceContainer.AddService(serviceType2, this);
				}
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0005BF68 File Offset: 0x0005A168
		internal HostDesigntimeLicenseContext LicenseContext
		{
			get
			{
				if (this._licenseCtx == null)
				{
					this._licenseCtx = new HostDesigntimeLicenseContext(this);
				}
				return this._licenseCtx;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0005BF84 File Offset: 0x0005A184
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0005BF96 File Offset: 0x0005A196
		internal bool IsClosingTransaction
		{
			get
			{
				return this._state[DesignerHost.StateIsClosingTransaction];
			}
			set
			{
				this._state[DesignerHost.StateIsClosingTransaction] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0005BFA9 File Offset: 0x0005A1A9
		bool IDesignerHostTransactionState.IsClosingTransaction
		{
			get
			{
				return this.IsClosingTransaction;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005BFB4 File Offset: 0x0005A1B4
		public override void Add(IComponent component, string name)
		{
			if (!this._typeServiceChecked)
			{
				this._typeService = (this.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService);
				this._typeServiceChecked = true;
			}
			if (this._typeService != null)
			{
				Type reflectionType = TypeDescriptor.GetProvider(component).GetReflectionType(typeof(object));
				if (!reflectionType.IsDefined(typeof(ProjectTargetFrameworkAttribute), false))
				{
					TypeDescriptionProvider provider = this._typeService.GetProvider(component);
					if (provider != null)
					{
						TypeDescriptor.AddProvider(provider, component);
					}
				}
			}
			this.PerformAdd(component, name);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005C03C File Offset: 0x0005A23C
		private void PerformAdd(IComponent component, string name)
		{
			if (this.AddToContainerPreProcess(component, name, this))
			{
				base.Add(component, name);
				try
				{
					this.AddToContainerPostProcess(component, name, this);
				}
				catch (Exception ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						this.Remove(component);
					}
					throw;
				}
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005C08C File Offset: 0x0005A28C
		internal bool AddToContainerPreProcess(IComponent component, string name, IContainer containerToAddTo)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (this._state[DesignerHost.StateUnloading])
			{
				throw new Exception(SR.GetString("DesignerHostUnloading"))
				{
					HelpLink = "DesignerHostUnloading"
				};
			}
			if (this._rootComponent != null && string.Equals(component.GetType().FullName, this._rootComponentClassName, StringComparison.OrdinalIgnoreCase))
			{
				throw new Exception(SR.GetString("DesignerHostCyclicAdd", new object[]
				{
					component.GetType().FullName,
					this._rootComponentClassName
				}))
				{
					HelpLink = "DesignerHostCyclicAdd"
				};
			}
			ISite site = component.Site;
			if (site != null && site.Container == this)
			{
				if (name != null)
				{
					site.Name = name;
				}
				return false;
			}
			ComponentEventArgs e = new ComponentEventArgs(component);
			ComponentEventHandler componentEventHandler = this._events[DesignerHost.EventComponentAdding] as ComponentEventHandler;
			if (componentEventHandler != null)
			{
				componentEventHandler(containerToAddTo, e);
			}
			return true;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0005C17C File Offset: 0x0005A37C
		internal void AddToContainerPostProcess(IComponent component, string name, IContainer containerToAddTo)
		{
			if (component is IExtenderProvider && !TypeDescriptor.GetAttributes(component).Contains(InheritanceAttribute.InheritedReadOnly))
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.AddExtenderProvider((IExtenderProvider)component);
				}
			}
			IDesigner designer = null;
			if (this._rootComponent == null)
			{
				designer = (this._surface.CreateDesigner(component, true) as IRootDesigner);
				if (designer == null)
				{
					throw new Exception(SR.GetString("DesignerHostNoTopLevelDesigner", new object[]
					{
						component.GetType().FullName
					}))
					{
						HelpLink = "DesignerHostNoTopLevelDesigner"
					};
				}
				this._rootComponent = component;
				if (this._rootComponentClassName == null)
				{
					this._rootComponentClassName = component.Site.Name;
				}
			}
			else
			{
				designer = this._surface.CreateDesigner(component, false);
			}
			if (designer != null)
			{
				this._designers[component] = designer;
				try
				{
					designer.Initialize(component);
					if (designer.Component == null)
					{
						throw new InvalidOperationException(SR.GetString("DesignerHostDesignerNeedsComponent"));
					}
				}
				catch
				{
					this._designers.Remove(component);
					throw;
				}
				if (designer is IExtenderProvider)
				{
					IExtenderProviderService extenderProviderService2 = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
					if (extenderProviderService2 != null)
					{
						extenderProviderService2.AddExtenderProvider((IExtenderProvider)designer);
					}
				}
			}
			ComponentEventArgs e = new ComponentEventArgs(component);
			ComponentEventHandler componentEventHandler = this._events[DesignerHost.EventComponentAdded] as ComponentEventHandler;
			if (componentEventHandler != null)
			{
				componentEventHandler(containerToAddTo, e);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		internal void BeginLoad(DesignerLoader loader)
		{
			if (this._loader != null && this._loader != loader)
			{
				throw new InvalidOperationException(SR.GetString("DesignerHostLoaderSpecified"))
				{
					HelpLink = "DesignerHostLoaderSpecified"
				};
			}
			bool flag = this._loader != null;
			this._loader = loader;
			if (!flag)
			{
				if (loader is IExtenderProvider)
				{
					IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
					if (extenderProviderService != null)
					{
						extenderProviderService.AddExtenderProvider((IExtenderProvider)loader);
					}
				}
				IDesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as IDesignerEventService;
				if (designerEventService != null)
				{
					designerEventService.ActiveDesignerChanged += this.OnActiveDesignerChanged;
					this._designerEventService = designerEventService;
				}
			}
			this._state[DesignerHost.StateLoading] = true;
			this._surface.OnLoading();
			try
			{
				this._loader.BeginLoad(this);
			}
			catch (Exception ex)
			{
				if (ex is TargetInvocationException)
				{
					ex = ex.InnerException;
				}
				string message = ex.Message;
				if (message == null || message.Length == 0)
				{
					ex = new Exception(SR.GetString("DesignSurfaceFatalError", new object[]
					{
						ex.ToString()
					}), ex);
				}
				((IDesignerLoaderHost)this).EndLoad(null, false, new object[]
				{
					ex
				});
			}
			if (this._designerEventService == null)
			{
				this.OnActiveDesignerChanged(null, new ActiveDesignerEventArgs(null, this));
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0005C458 File Offset: 0x0005A658
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (this._newComponentName != null)
			{
				name = this._newComponentName;
				this._newComponentName = null;
			}
			INameCreationService nameCreationService = this.GetService(typeof(INameCreationService)) as INameCreationService;
			if (name == null)
			{
				if (nameCreationService != null)
				{
					Type type = TypeDescriptor.GetReflectionType(component);
					if (type.FullName.Equals(component.GetType().FullName))
					{
						type = component.GetType();
					}
					name = nameCreationService.CreateName(this, type);
				}
				else
				{
					name = string.Empty;
				}
			}
			else if (nameCreationService != null)
			{
				nameCreationService.ValidateName(name);
			}
			return new DesignerHost.Site(component, this, name, this);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005C4E6 File Offset: 0x0005A6E6
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				throw new InvalidOperationException(SR.GetString("DesignSurfaceContainerDispose"));
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0005C504 File Offset: 0x0005A704
		internal void DisposeHost()
		{
			try
			{
				if (this._loader != null)
				{
					this._loader.Dispose();
					this.Unload();
				}
				if (this._surface != null)
				{
					if (this._designerEventService != null)
					{
						this._designerEventService.ActiveDesignerChanged -= this.OnActiveDesignerChanged;
					}
					DesignSurfaceServiceContainer designSurfaceServiceContainer = this.GetService(typeof(DesignSurfaceServiceContainer)) as DesignSurfaceServiceContainer;
					if (designSurfaceServiceContainer != null)
					{
						foreach (Type serviceType in DesignerHost.DefaultServices)
						{
							designSurfaceServiceContainer.RemoveFixedService(serviceType);
						}
					}
					else
					{
						IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
						if (serviceContainer != null)
						{
							foreach (Type serviceType2 in DesignerHost.DefaultServices)
							{
								serviceContainer.RemoveService(serviceType2);
							}
						}
					}
				}
			}
			finally
			{
				this._loader = null;
				this._surface = null;
				this._events.Dispose();
			}
			base.Dispose(true);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0005C608 File Offset: 0x0005A808
		internal void Flush()
		{
			if (this._loader != null)
			{
				this._loader.Flush();
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0005C620 File Offset: 0x0005A820
		protected override object GetService(Type service)
		{
			object obj = null;
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			if (service == typeof(IMultitargetHelperService))
			{
				IServiceProvider serviceProvider = this._loader as IServiceProvider;
				if (serviceProvider != null)
				{
					obj = serviceProvider.GetService(typeof(IMultitargetHelperService));
				}
			}
			else
			{
				obj = base.GetService(service);
				if (obj == null && this._surface != null)
				{
					obj = this._surface.GetService(service);
				}
			}
			return obj;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0005C698 File Offset: 0x0005A898
		private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs e)
		{
			object obj = null;
			if (e.OldDesigner == this)
			{
				obj = DesignerHost.EventDeactivated;
			}
			else if (e.NewDesigner == this)
			{
				obj = DesignerHost.EventActivated;
			}
			if (obj == null)
			{
				return;
			}
			if (e.OldDesigner == this && this._surface != null)
			{
				this._surface.Flush();
			}
			EventHandler eventHandler = this._events[obj] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005C708 File Offset: 0x0005A908
		private void OnComponentRename(IComponent component, string oldName, string newName)
		{
			if (component == this._rootComponent)
			{
				string rootComponentClassName = this._rootComponentClassName;
				int num = rootComponentClassName.LastIndexOf(oldName);
				if (num + oldName.Length == rootComponentClassName.Length && num - 1 >= 0 && rootComponentClassName[num - 1] == '.')
				{
					this._rootComponentClassName = rootComponentClassName.Substring(0, num) + newName;
				}
				else
				{
					this._rootComponentClassName = newName;
				}
			}
			ComponentRenameEventHandler componentRenameEventHandler = this._events[DesignerHost.EventComponentRename] as ComponentRenameEventHandler;
			if (componentRenameEventHandler != null)
			{
				componentRenameEventHandler(this, new ComponentRenameEventArgs(component, oldName, newName));
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005C798 File Offset: 0x0005A998
		private void OnLoadComplete(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventLoadComplete] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0005C7C8 File Offset: 0x0005A9C8
		private void OnTransactionClosed(DesignerTransactionCloseEventArgs e)
		{
			DesignerTransactionCloseEventHandler designerTransactionCloseEventHandler = this._events[DesignerHost.EventTransactionClosed] as DesignerTransactionCloseEventHandler;
			if (designerTransactionCloseEventHandler != null)
			{
				designerTransactionCloseEventHandler(this, e);
			}
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005C7F8 File Offset: 0x0005A9F8
		private void OnTransactionClosing(DesignerTransactionCloseEventArgs e)
		{
			DesignerTransactionCloseEventHandler designerTransactionCloseEventHandler = this._events[DesignerHost.EventTransactionClosing] as DesignerTransactionCloseEventHandler;
			if (designerTransactionCloseEventHandler != null)
			{
				designerTransactionCloseEventHandler(this, e);
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005C828 File Offset: 0x0005AA28
		private void OnTransactionOpened(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventTransactionOpened] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0005C858 File Offset: 0x0005AA58
		private void OnTransactionOpening(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventTransactionOpening] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0005C888 File Offset: 0x0005AA88
		public override void Remove(IComponent component)
		{
			if (this.RemoveFromContainerPreProcess(component, this))
			{
				DesignerHost.Site site = component.Site as DesignerHost.Site;
				base.RemoveWithoutUnsiting(component);
				this.RemoveFromContainerPostProcess(component, this);
				site.Disposed = true;
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
		internal bool RemoveFromContainerPreProcess(IComponent component, IContainer container)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			ISite site = component.Site;
			if (site == null || site.Container != container)
			{
				return false;
			}
			ComponentEventArgs e = new ComponentEventArgs(component);
			ComponentEventHandler componentEventHandler = this._events[DesignerHost.EventComponentRemoving] as ComponentEventHandler;
			if (componentEventHandler != null)
			{
				componentEventHandler(this, e);
			}
			if (component is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.RemoveExtenderProvider((IExtenderProvider)component);
				}
			}
			IDesigner designer = this._designers[component] as IDesigner;
			if (designer is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService2 = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService2 != null)
				{
					extenderProviderService2.RemoveExtenderProvider((IExtenderProvider)designer);
				}
			}
			if (designer != null)
			{
				designer.Dispose();
				this._designers.Remove(component);
			}
			if (component == this._rootComponent)
			{
				this._rootComponent = null;
				this._rootComponentClassName = null;
			}
			return true;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		internal void RemoveFromContainerPostProcess(IComponent component, IContainer container)
		{
			try
			{
				ComponentEventHandler componentEventHandler = this._events[DesignerHost.EventComponentRemoved] as ComponentEventHandler;
				ComponentEventArgs e = new ComponentEventArgs(component);
				if (componentEventHandler != null)
				{
					componentEventHandler(this, e);
				}
			}
			finally
			{
				component.Site = null;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0005CA0C File Offset: 0x0005AC0C
		private void Unload()
		{
			this._surface.OnUnloading();
			IHelpService helpService = this.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService != null && this._rootComponent != null && this._designers[this._rootComponent] != null)
			{
				helpService.RemoveContextAttribute("Keyword", "Designer_" + this._designers[this._rootComponent].GetType().FullName);
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(null, SelectionTypes.Replace);
			}
			this._state[DesignerHost.StateUnloading] = true;
			DesignerTransaction designerTransaction = ((IDesignerHost)this).CreateTransaction();
			ArrayList arrayList = new ArrayList();
			try
			{
				IComponent[] array = new IComponent[this.Components.Count];
				this.Components.CopyTo(array, 0);
				foreach (IComponent component in array)
				{
					if (component != this._rootComponent)
					{
						IDesigner designer = this._designers[component] as IDesigner;
						if (designer != null)
						{
							this._designers.Remove(component);
							try
							{
								designer.Dispose();
							}
							catch (Exception value)
							{
								string text = (designer != null) ? designer.GetType().Name : string.Empty;
								arrayList.Add(value);
							}
						}
						try
						{
							component.Dispose();
						}
						catch (Exception value2)
						{
							string text2 = (component != null) ? component.GetType().Name : string.Empty;
							arrayList.Add(value2);
						}
					}
				}
				if (this._rootComponent != null)
				{
					IDesigner designer2 = this._designers[this._rootComponent] as IDesigner;
					if (designer2 != null)
					{
						this._designers.Remove(this._rootComponent);
						try
						{
							designer2.Dispose();
						}
						catch (Exception value3)
						{
							string text3 = (designer2 != null) ? designer2.GetType().Name : string.Empty;
							arrayList.Add(value3);
						}
					}
					try
					{
						this._rootComponent.Dispose();
					}
					catch (Exception value4)
					{
						string text4 = (this._rootComponent != null) ? this._rootComponent.GetType().Name : string.Empty;
						arrayList.Add(value4);
					}
				}
				this._designers.Clear();
				while (this.Components.Count > 0)
				{
					this.Remove(this.Components[0]);
				}
			}
			finally
			{
				designerTransaction.Commit();
				this._state[DesignerHost.StateUnloading] = false;
			}
			if (this._transactions != null && this._transactions.Count > 0)
			{
				while (this._transactions.Count > 0)
				{
					DesignerTransaction designerTransaction2 = (DesignerTransaction)this._transactions.Peek();
					designerTransaction2.Commit();
				}
			}
			this._surface.OnUnloaded();
			if (arrayList.Count > 0)
			{
				throw new ExceptionCollection(arrayList);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600106C RID: 4204 RVA: 0x0005CD5C File Offset: 0x0005AF5C
		// (remove) Token: 0x0600106D RID: 4205 RVA: 0x0005CD6F File Offset: 0x0005AF6F
		event ComponentEventHandler IComponentChangeService.ComponentAdded
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentAdded, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentAdded, value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600106E RID: 4206 RVA: 0x0005CD82 File Offset: 0x0005AF82
		// (remove) Token: 0x0600106F RID: 4207 RVA: 0x0005CD95 File Offset: 0x0005AF95
		event ComponentEventHandler IComponentChangeService.ComponentAdding
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentAdding, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentAdding, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001070 RID: 4208 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
		// (remove) Token: 0x06001071 RID: 4209 RVA: 0x0005CDBB File Offset: 0x0005AFBB
		event ComponentChangedEventHandler IComponentChangeService.ComponentChanged
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentChanged, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentChanged, value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001072 RID: 4210 RVA: 0x0005CDCE File Offset: 0x0005AFCE
		// (remove) Token: 0x06001073 RID: 4211 RVA: 0x0005CDE1 File Offset: 0x0005AFE1
		event ComponentChangingEventHandler IComponentChangeService.ComponentChanging
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentChanging, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentChanging, value);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001074 RID: 4212 RVA: 0x0005CDF4 File Offset: 0x0005AFF4
		// (remove) Token: 0x06001075 RID: 4213 RVA: 0x0005CE07 File Offset: 0x0005B007
		event ComponentEventHandler IComponentChangeService.ComponentRemoved
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentRemoved, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentRemoved, value);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001076 RID: 4214 RVA: 0x0005CE1A File Offset: 0x0005B01A
		// (remove) Token: 0x06001077 RID: 4215 RVA: 0x0005CE2D File Offset: 0x0005B02D
		event ComponentEventHandler IComponentChangeService.ComponentRemoving
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentRemoving, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentRemoving, value);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001078 RID: 4216 RVA: 0x0005CE40 File Offset: 0x0005B040
		// (remove) Token: 0x06001079 RID: 4217 RVA: 0x0005CE53 File Offset: 0x0005B053
		event ComponentRenameEventHandler IComponentChangeService.ComponentRename
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventComponentRename, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventComponentRename, value);
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0005CE68 File Offset: 0x0005B068
		void IComponentChangeService.OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			if (!((IDesignerHost)this).Loading)
			{
				ComponentChangedEventHandler componentChangedEventHandler = this._events[DesignerHost.EventComponentChanged] as ComponentChangedEventHandler;
				if (componentChangedEventHandler != null)
				{
					componentChangedEventHandler(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
				}
			}
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
		void IComponentChangeService.OnComponentChanging(object component, MemberDescriptor member)
		{
			if (!((IDesignerHost)this).Loading)
			{
				ComponentChangingEventHandler componentChangingEventHandler = this._events[DesignerHost.EventComponentChanging] as ComponentChangingEventHandler;
				if (componentChangingEventHandler != null)
				{
					componentChangingEventHandler(this, new ComponentChangingEventArgs(component, member));
				}
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0005CEE4 File Offset: 0x0005B0E4
		bool IDesignerHost.Loading
		{
			get
			{
				return this._state[DesignerHost.StateLoading] || this._state[DesignerHost.StateUnloading] || (this._loader != null && this._loader.Loading);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0005CF21 File Offset: 0x0005B121
		bool IDesignerHost.InTransaction
		{
			get
			{
				return (this._transactions != null && this._transactions.Count > 0) || this.IsClosingTransaction;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0000CA50 File Offset: 0x0000AC50
		IContainer IDesignerHost.Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0005CF41 File Offset: 0x0005B141
		IComponent IDesignerHost.RootComponent
		{
			get
			{
				return this._rootComponent;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0005CF49 File Offset: 0x0005B149
		string IDesignerHost.RootComponentClassName
		{
			get
			{
				return this._rootComponentClassName;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0005CF51 File Offset: 0x0005B151
		string IDesignerHost.TransactionDescription
		{
			get
			{
				if (this._transactions != null && this._transactions.Count > 0)
				{
					return ((DesignerTransaction)this._transactions.Peek()).Description;
				}
				return null;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001082 RID: 4226 RVA: 0x0005CF80 File Offset: 0x0005B180
		// (remove) Token: 0x06001083 RID: 4227 RVA: 0x0005CF93 File Offset: 0x0005B193
		event EventHandler IDesignerHost.Activated
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventActivated, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventActivated, value);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001084 RID: 4228 RVA: 0x0005CFA6 File Offset: 0x0005B1A6
		// (remove) Token: 0x06001085 RID: 4229 RVA: 0x0005CFB9 File Offset: 0x0005B1B9
		event EventHandler IDesignerHost.Deactivated
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventDeactivated, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventDeactivated, value);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001086 RID: 4230 RVA: 0x0005CFCC File Offset: 0x0005B1CC
		// (remove) Token: 0x06001087 RID: 4231 RVA: 0x0005CFDF File Offset: 0x0005B1DF
		event EventHandler IDesignerHost.LoadComplete
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventLoadComplete, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventLoadComplete, value);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001088 RID: 4232 RVA: 0x0005CFF2 File Offset: 0x0005B1F2
		// (remove) Token: 0x06001089 RID: 4233 RVA: 0x0005D005 File Offset: 0x0005B205
		event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosed
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventTransactionClosed, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventTransactionClosed, value);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600108A RID: 4234 RVA: 0x0005D018 File Offset: 0x0005B218
		// (remove) Token: 0x0600108B RID: 4235 RVA: 0x0005D02B File Offset: 0x0005B22B
		event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosing
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventTransactionClosing, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventTransactionClosing, value);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600108C RID: 4236 RVA: 0x0005D03E File Offset: 0x0005B23E
		// (remove) Token: 0x0600108D RID: 4237 RVA: 0x0005D051 File Offset: 0x0005B251
		event EventHandler IDesignerHost.TransactionOpened
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventTransactionOpened, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventTransactionOpened, value);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600108E RID: 4238 RVA: 0x0005D064 File Offset: 0x0005B264
		// (remove) Token: 0x0600108F RID: 4239 RVA: 0x0005D077 File Offset: 0x0005B277
		event EventHandler IDesignerHost.TransactionOpening
		{
			add
			{
				this._events.AddHandler(DesignerHost.EventTransactionOpening, value);
			}
			remove
			{
				this._events.RemoveHandler(DesignerHost.EventTransactionOpening, value);
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0005D08A File Offset: 0x0005B28A
		void IDesignerHost.Activate()
		{
			this._surface.OnViewActivate();
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0005D097 File Offset: 0x0005B297
		IComponent IDesignerHost.CreateComponent(Type componentType)
		{
			return ((IDesignerHost)this).CreateComponent(componentType, null);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0005D0A4 File Offset: 0x0005B2A4
		IComponent IDesignerHost.CreateComponent(Type componentType, string name)
		{
			if (componentType == null)
			{
				throw new ArgumentNullException("componentType");
			}
			LicenseContext currentContext = LicenseManager.CurrentContext;
			bool flag = false;
			if (currentContext != this.LicenseContext)
			{
				LicenseManager.CurrentContext = this.LicenseContext;
				LicenseManager.LockContext(DesignerHost._selfLock);
				flag = true;
			}
			IComponent component;
			try
			{
				try
				{
					this._newComponentName = name;
					component = (this._surface.CreateInstance(componentType) as IComponent);
				}
				finally
				{
					this._newComponentName = null;
				}
				if (component == null)
				{
					throw new InvalidOperationException(SR.GetString("DesignerHostFailedComponentCreate", new object[]
					{
						componentType.Name
					}))
					{
						HelpLink = "DesignerHostFailedComponentCreate"
					};
				}
				if (component.Site == null || component.Site.Container != this)
				{
					this.PerformAdd(component, name);
				}
			}
			finally
			{
				if (flag)
				{
					LicenseManager.UnlockContext(DesignerHost._selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return component;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0005D194 File Offset: 0x0005B394
		DesignerTransaction IDesignerHost.CreateTransaction()
		{
			return ((IDesignerHost)this).CreateTransaction(null);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0005D19D File Offset: 0x0005B39D
		DesignerTransaction IDesignerHost.CreateTransaction(string description)
		{
			if (description == null)
			{
				description = SR.GetString("DesignerHostGenericTransactionName");
			}
			return new DesignerHost.DesignerHostTransaction(this, description);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
		void IDesignerHost.DestroyComponent(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			string name;
			if (component.Site != null && component.Site.Name != null)
			{
				name = component.Site.Name;
			}
			else
			{
				name = component.GetType().Name;
			}
			InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(component)[typeof(InheritanceAttribute)];
			if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
			{
				throw new InvalidOperationException(SR.GetString("DesignerHostCantDestroyInheritedComponent", new object[]
				{
					name
				}))
				{
					HelpLink = "DesignerHostCantDestroyInheritedComponent"
				};
			}
			if (((IDesignerHost)this).InTransaction)
			{
				this.Remove(component);
				component.Dispose();
				return;
			}
			DesignerTransaction designerTransaction2;
			DesignerTransaction designerTransaction = designerTransaction2 = ((IDesignerHost)this).CreateTransaction(SR.GetString("DesignerHostDestroyComponentTransaction", new object[]
			{
				name
			}));
			try
			{
				this.Remove(component);
				component.Dispose();
				designerTransaction.Commit();
			}
			finally
			{
				if (designerTransaction2 != null)
				{
					((IDisposable)designerTransaction2).Dispose();
				}
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		IDesigner IDesignerHost.GetDesigner(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._designers[component] as IDesigner;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		Type IDesignerHost.GetType(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			ITypeResolutionService typeResolutionService = this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			if (typeResolutionService != null)
			{
				return typeResolutionService.GetType(typeName);
			}
			return Type.GetType(typeName);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0005D320 File Offset: 0x0005B520
		void IDesignerLoaderHost.EndLoad(string rootClassName, bool successful, ICollection errorCollection)
		{
			bool flag = this._state[DesignerHost.StateLoading];
			this._state[DesignerHost.StateLoading] = false;
			if (rootClassName != null)
			{
				this._rootComponentClassName = rootClassName;
			}
			else if (this._rootComponent != null && this._rootComponent.Site != null)
			{
				this._rootComponentClassName = this._rootComponent.Site.Name;
			}
			if (successful && this._rootComponent == null)
			{
				errorCollection = new ArrayList
				{
					new InvalidOperationException(SR.GetString("DesignerHostNoBaseClass"))
					{
						HelpLink = "DesignerHostNoBaseClass"
					}
				};
				successful = false;
			}
			if (!successful)
			{
				this.Unload();
			}
			if (flag && this._surface != null)
			{
				this._surface.OnLoaded(successful, errorCollection);
			}
			if (successful && flag)
			{
				IRootDesigner rootDesigner = ((IDesignerHost)this).GetDesigner(this._rootComponent) as IRootDesigner;
				IHelpService helpService = this.GetService(typeof(IHelpService)) as IHelpService;
				if (helpService != null)
				{
					helpService.AddContextAttribute("Keyword", "Designer_" + rootDesigner.GetType().FullName, HelpKeywordType.F1Keyword);
				}
				try
				{
					this.OnLoadComplete(EventArgs.Empty);
				}
				catch (Exception value)
				{
					this._state[DesignerHost.StateLoading] = true;
					this.Unload();
					ArrayList arrayList = new ArrayList();
					arrayList.Add(value);
					if (errorCollection != null)
					{
						arrayList.AddRange(errorCollection);
					}
					errorCollection = arrayList;
					successful = false;
					if (this._surface != null)
					{
						this._surface.OnLoaded(successful, errorCollection);
					}
					throw;
				}
				if (successful && this._savedSelection != null)
				{
					ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
					if (selectionService != null)
					{
						ArrayList arrayList2 = new ArrayList(this._savedSelection.Count);
						foreach (object obj in this._savedSelection)
						{
							string name = (string)obj;
							IComponent component = this.Components[name];
							if (component != null)
							{
								arrayList2.Add(component);
							}
						}
						this._savedSelection = null;
						selectionService.SetSelectedComponents(arrayList2, SelectionTypes.Replace);
					}
				}
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0005D564 File Offset: 0x0005B764
		void IDesignerLoaderHost.Reload()
		{
			if (this._loader != null)
			{
				this._surface.Flush();
				ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					ArrayList arrayList = new ArrayList(selectionService.SelectionCount);
					foreach (object obj in selectionService.GetSelectedComponents())
					{
						IComponent component = obj as IComponent;
						if (component != null && component.Site != null && component.Site.Name != null)
						{
							arrayList.Add(component.Site.Name);
						}
					}
					this._savedSelection = arrayList;
				}
				this.Unload();
				this.BeginLoad(this._loader);
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0005D640 File Offset: 0x0005B840
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0005D648 File Offset: 0x0005B848
		bool IDesignerLoaderHost2.IgnoreErrorsDuringReload
		{
			get
			{
				return this._ignoreErrorsDuringReload;
			}
			set
			{
				if (!value || ((IDesignerLoaderHost2)this).CanReloadWithErrors)
				{
					this._ignoreErrorsDuringReload = value;
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0005D65C File Offset: 0x0005B85C
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0005D664 File Offset: 0x0005B864
		bool IDesignerLoaderHost2.CanReloadWithErrors
		{
			get
			{
				return this._canReloadWithErrors;
			}
			set
			{
				this._canReloadWithErrors = value;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0005D66D File Offset: 0x0005B86D
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IDesignerHost).GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005D685 File Offset: 0x0005B885
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMethod(name, bindingAttr);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0005D698 File Offset: 0x0005B898
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMethods(bindingAttr);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0005D6AA File Offset: 0x0005B8AA
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetField(name, bindingAttr);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0005D6BD File Offset: 0x0005B8BD
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetFields(bindingAttr);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0005D6CF File Offset: 0x0005B8CF
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetProperty(name, bindingAttr);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0005D6E2 File Offset: 0x0005B8E2
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IDesignerHost).GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0005D6FC File Offset: 0x0005B8FC
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetProperties(bindingAttr);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0005D70E File Offset: 0x0005B90E
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMember(name, bindingAttr);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0005D721 File Offset: 0x0005B921
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMembers(bindingAttr);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0005D734 File Offset: 0x0005B934
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return typeof(IDesignerHost).InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0005D75D File Offset: 0x0005B95D
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return typeof(IDesignerHost).UnderlyingSystemType;
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0005D770 File Offset: 0x0005B970
		void IServiceContainer.AddService(Type serviceType, object serviceInstance)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, serviceInstance);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0005D7AC File Offset: 0x0005B9AC
		void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, serviceInstance, promote);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0005D7E8 File Offset: 0x0005B9E8
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, callback);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0005D824 File Offset: 0x0005BA24
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, callback, promote);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0005D860 File Offset: 0x0005BA60
		void IServiceContainer.RemoveService(Type serviceType)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.RemoveService(serviceType);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0005D898 File Offset: 0x0005BA98
		void IServiceContainer.RemoveService(Type serviceType, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.RemoveService(serviceType, promote);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0005D8D1 File Offset: 0x0005BAD1
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x0400096E RID: 2414
		private static readonly int StateLoading = BitVector32.CreateMask();

		// Token: 0x0400096F RID: 2415
		private static readonly int StateUnloading = BitVector32.CreateMask(DesignerHost.StateLoading);

		// Token: 0x04000970 RID: 2416
		private static readonly int StateIsClosingTransaction = BitVector32.CreateMask(DesignerHost.StateUnloading);

		// Token: 0x04000971 RID: 2417
		private static Type[] DefaultServices = new Type[]
		{
			typeof(IDesignerHost),
			typeof(IContainer),
			typeof(IComponentChangeService),
			typeof(IDesignerLoaderHost2)
		};

		// Token: 0x04000972 RID: 2418
		private static readonly object EventActivated = new object();

		// Token: 0x04000973 RID: 2419
		private static readonly object EventDeactivated = new object();

		// Token: 0x04000974 RID: 2420
		private static readonly object EventLoadComplete = new object();

		// Token: 0x04000975 RID: 2421
		private static readonly object EventTransactionClosed = new object();

		// Token: 0x04000976 RID: 2422
		private static readonly object EventTransactionClosing = new object();

		// Token: 0x04000977 RID: 2423
		private static readonly object EventTransactionOpened = new object();

		// Token: 0x04000978 RID: 2424
		private static readonly object EventTransactionOpening = new object();

		// Token: 0x04000979 RID: 2425
		private static readonly object EventComponentAdding = new object();

		// Token: 0x0400097A RID: 2426
		private static readonly object EventComponentAdded = new object();

		// Token: 0x0400097B RID: 2427
		private static readonly object EventComponentChanging = new object();

		// Token: 0x0400097C RID: 2428
		private static readonly object EventComponentChanged = new object();

		// Token: 0x0400097D RID: 2429
		private static readonly object EventComponentRemoving = new object();

		// Token: 0x0400097E RID: 2430
		private static readonly object EventComponentRemoved = new object();

		// Token: 0x0400097F RID: 2431
		private static readonly object EventComponentRename = new object();

		// Token: 0x04000980 RID: 2432
		private BitVector32 _state;

		// Token: 0x04000981 RID: 2433
		private DesignSurface _surface;

		// Token: 0x04000982 RID: 2434
		private string _newComponentName;

		// Token: 0x04000983 RID: 2435
		private Stack _transactions;

		// Token: 0x04000984 RID: 2436
		private IComponent _rootComponent;

		// Token: 0x04000985 RID: 2437
		private string _rootComponentClassName;

		// Token: 0x04000986 RID: 2438
		private Hashtable _designers;

		// Token: 0x04000987 RID: 2439
		private EventHandlerList _events;

		// Token: 0x04000988 RID: 2440
		private DesignerLoader _loader;

		// Token: 0x04000989 RID: 2441
		private ICollection _savedSelection;

		// Token: 0x0400098A RID: 2442
		private HostDesigntimeLicenseContext _licenseCtx;

		// Token: 0x0400098B RID: 2443
		private IDesignerEventService _designerEventService;

		// Token: 0x0400098C RID: 2444
		private static readonly object _selfLock = new object();

		// Token: 0x0400098D RID: 2445
		private bool _ignoreErrorsDuringReload;

		// Token: 0x0400098E RID: 2446
		private bool _canReloadWithErrors;

		// Token: 0x0400098F RID: 2447
		private TypeDescriptionProviderService _typeService;

		// Token: 0x04000990 RID: 2448
		private bool _typeServiceChecked;

		// Token: 0x02000497 RID: 1175
		private sealed class DesignerHostTransaction : DesignerTransaction
		{
			// Token: 0x06002B56 RID: 11094 RVA: 0x001033DC File Offset: 0x001015DC
			public DesignerHostTransaction(DesignerHost host, string description) : base(description)
			{
				this._host = host;
				if (this._host._transactions == null)
				{
					this._host._transactions = new Stack();
				}
				this._host._transactions.Push(this);
				this._host.OnTransactionOpening(EventArgs.Empty);
				this._host.OnTransactionOpened(EventArgs.Empty);
			}

			// Token: 0x06002B57 RID: 11095 RVA: 0x00103448 File Offset: 0x00101648
			protected override void OnCancel()
			{
				if (this._host != null)
				{
					if (this._host._transactions.Peek() != this)
					{
						string description = ((DesignerTransaction)this._host._transactions.Peek()).Description;
						throw new InvalidOperationException(SR.GetString("DesignerHostNestedTransaction", new object[]
						{
							base.Description,
							description
						}));
					}
					this._host.IsClosingTransaction = true;
					try
					{
						this._host._transactions.Pop();
						DesignerTransactionCloseEventArgs e = new DesignerTransactionCloseEventArgs(false, this._host._transactions.Count == 0);
						this._host.OnTransactionClosing(e);
						this._host.OnTransactionClosed(e);
					}
					finally
					{
						this._host.IsClosingTransaction = false;
						this._host = null;
					}
				}
			}

			// Token: 0x06002B58 RID: 11096 RVA: 0x00103528 File Offset: 0x00101728
			protected override void OnCommit()
			{
				if (this._host != null)
				{
					if (this._host._transactions.Peek() != this)
					{
						string description = ((DesignerTransaction)this._host._transactions.Peek()).Description;
						throw new InvalidOperationException(SR.GetString("DesignerHostNestedTransaction", new object[]
						{
							base.Description,
							description
						}));
					}
					this._host.IsClosingTransaction = true;
					try
					{
						this._host._transactions.Pop();
						DesignerTransactionCloseEventArgs e = new DesignerTransactionCloseEventArgs(true, this._host._transactions.Count == 0);
						this._host.OnTransactionClosing(e);
						this._host.OnTransactionClosed(e);
					}
					finally
					{
						this._host.IsClosingTransaction = false;
						this._host = null;
					}
				}
			}

			// Token: 0x04001E1C RID: 7708
			private DesignerHost _host;
		}

		// Token: 0x02000498 RID: 1176
		internal class Site : ISite, IServiceProvider, IServiceContainer, IDictionaryService
		{
			// Token: 0x06002B59 RID: 11097 RVA: 0x00103608 File Offset: 0x00101808
			internal Site(IComponent component, DesignerHost host, string name, Container container)
			{
				this._component = component;
				this._host = host;
				this._name = name;
				this._container = container;
			}

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x06002B5A RID: 11098 RVA: 0x00103630 File Offset: 0x00101830
			private IServiceContainer SiteServiceContainer
			{
				get
				{
					SiteNestedContainer siteNestedContainer = ((IServiceProvider)this).GetService(typeof(INestedContainer)) as SiteNestedContainer;
					return siteNestedContainer.GetServiceInternal(typeof(IServiceContainer)) as IServiceContainer;
				}
			}

			// Token: 0x06002B5B RID: 11099 RVA: 0x0010366C File Offset: 0x0010186C
			object IDictionaryService.GetKey(object value)
			{
				if (this._dictionary != null)
				{
					foreach (object obj in this._dictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						object value2 = dictionaryEntry.Value;
						if (value != null && value.Equals(value2))
						{
							return dictionaryEntry.Key;
						}
					}
				}
				return null;
			}

			// Token: 0x06002B5C RID: 11100 RVA: 0x001036EC File Offset: 0x001018EC
			object IDictionaryService.GetValue(object key)
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return null;
			}

			// Token: 0x06002B5D RID: 11101 RVA: 0x00103704 File Offset: 0x00101904
			void IDictionaryService.SetValue(object key, object value)
			{
				if (this._dictionary == null)
				{
					this._dictionary = new Hashtable();
				}
				if (value == null)
				{
					this._dictionary.Remove(key);
					return;
				}
				this._dictionary[key] = value;
			}

			// Token: 0x06002B5E RID: 11102 RVA: 0x00103736 File Offset: 0x00101936
			void IServiceContainer.AddService(Type serviceType, object serviceInstance)
			{
				this.SiteServiceContainer.AddService(serviceType, serviceInstance);
			}

			// Token: 0x06002B5F RID: 11103 RVA: 0x00103745 File Offset: 0x00101945
			void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
			{
				this.SiteServiceContainer.AddService(serviceType, serviceInstance, promote);
			}

			// Token: 0x06002B60 RID: 11104 RVA: 0x00103755 File Offset: 0x00101955
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
			{
				this.SiteServiceContainer.AddService(serviceType, callback);
			}

			// Token: 0x06002B61 RID: 11105 RVA: 0x00103764 File Offset: 0x00101964
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
			{
				this.SiteServiceContainer.AddService(serviceType, callback, promote);
			}

			// Token: 0x06002B62 RID: 11106 RVA: 0x00103774 File Offset: 0x00101974
			void IServiceContainer.RemoveService(Type serviceType)
			{
				this.SiteServiceContainer.RemoveService(serviceType);
			}

			// Token: 0x06002B63 RID: 11107 RVA: 0x00103782 File Offset: 0x00101982
			void IServiceContainer.RemoveService(Type serviceType, bool promote)
			{
				this.SiteServiceContainer.RemoveService(serviceType, promote);
			}

			// Token: 0x06002B64 RID: 11108 RVA: 0x00103794 File Offset: 0x00101994
			object IServiceProvider.GetService(Type service)
			{
				if (service == null)
				{
					throw new ArgumentNullException("service");
				}
				if (service == typeof(IDictionaryService))
				{
					return this;
				}
				if (service == typeof(INestedContainer))
				{
					if (this._nestedContainer == null)
					{
						this._nestedContainer = new SiteNestedContainer(this._component, null, this._host);
					}
					return this._nestedContainer;
				}
				if (service != typeof(IServiceContainer) && service != typeof(IContainer) && this._nestedContainer != null)
				{
					return this._nestedContainer.GetServiceInternal(service);
				}
				return this._host.GetService(service);
			}

			// Token: 0x17000924 RID: 2340
			// (get) Token: 0x06002B65 RID: 11109 RVA: 0x00103847 File Offset: 0x00101A47
			IComponent ISite.Component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x17000925 RID: 2341
			// (get) Token: 0x06002B66 RID: 11110 RVA: 0x0010384F File Offset: 0x00101A4F
			IContainer ISite.Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x06002B67 RID: 11111 RVA: 0x00003B0F File Offset: 0x00001D0F
			bool ISite.DesignMode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00103857 File Offset: 0x00101A57
			// (set) Token: 0x06002B69 RID: 11113 RVA: 0x0010385F File Offset: 0x00101A5F
			internal bool Disposed
			{
				get
				{
					return this._disposed;
				}
				set
				{
					this._disposed = value;
					if (this._disposed)
					{
						this._dictionary = null;
					}
				}
			}

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x06002B6A RID: 11114 RVA: 0x00103877 File Offset: 0x00101A77
			// (set) Token: 0x06002B6B RID: 11115 RVA: 0x00103880 File Offset: 0x00101A80
			string ISite.Name
			{
				get
				{
					return this._name;
				}
				set
				{
					if (value == null)
					{
						value = string.Empty;
					}
					if (this._name != value)
					{
						bool flag = true;
						if (value.Length > 0)
						{
							IComponent component = this._container.Components[value];
							flag = (this._component != component);
							if (component != null && flag)
							{
								throw new Exception(SR.GetString("DesignerHostDuplicateName", new object[]
								{
									value
								}))
								{
									HelpLink = "DesignerHostDuplicateName"
								};
							}
						}
						if (flag)
						{
							INameCreationService nameCreationService = (INameCreationService)((IServiceProvider)this).GetService(typeof(INameCreationService));
							if (nameCreationService != null)
							{
								nameCreationService.ValidateName(value);
							}
						}
						string name = this._name;
						this._name = value;
						this._host.OnComponentRename(this._component, name, this._name);
					}
				}
			}

			// Token: 0x04001E1D RID: 7709
			private IComponent _component;

			// Token: 0x04001E1E RID: 7710
			private Hashtable _dictionary;

			// Token: 0x04001E1F RID: 7711
			private DesignerHost _host;

			// Token: 0x04001E20 RID: 7712
			private string _name;

			// Token: 0x04001E21 RID: 7713
			private bool _disposed;

			// Token: 0x04001E22 RID: 7714
			private SiteNestedContainer _nestedContainer;

			// Token: 0x04001E23 RID: 7715
			private Container _container;
		}
	}
}
