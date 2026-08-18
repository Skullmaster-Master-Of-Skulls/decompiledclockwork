using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x02000552 RID: 1362
	internal sealed class DesignerHost : Container, IDesignerLoaderHost2, IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider, IDesignerHostTransactionState, IComponentChangeService, IReflect
	{
		// Token: 0x06002FBC RID: 12220 RVA: 0x00110318 File Offset: 0x0010F318
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

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x001103D7 File Offset: 0x0010F3D7
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

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x001103F3 File Offset: 0x0010F3F3
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x00110405 File Offset: 0x0010F405
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

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x00110418 File Offset: 0x0010F418
		bool IDesignerHostTransactionState.IsClosingTransaction
		{
			get
			{
				return this.IsClosingTransaction;
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x00110420 File Offset: 0x0010F420
		public override void Add(IComponent component, string name)
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
				catch
				{
					this.Remove(component);
					throw;
				}
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00110484 File Offset: 0x0010F484
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

		// Token: 0x06002FC3 RID: 12227 RVA: 0x0011057C File Offset: 0x0010F57C
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

		// Token: 0x06002FC4 RID: 12228 RVA: 0x001106F8 File Offset: 0x0010F6F8
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

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00110868 File Offset: 0x0010F868
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
					name = nameCreationService.CreateName(this, TypeDescriptor.GetReflectionType(component));
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

		// Token: 0x06002FC6 RID: 12230 RVA: 0x001108D5 File Offset: 0x0010F8D5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				throw new InvalidOperationException(SR.GetString("DesignSurfaceContainerDispose"));
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x001108F4 File Offset: 0x0010F8F4
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

		// Token: 0x06002FC8 RID: 12232 RVA: 0x001109FC File Offset: 0x0010F9FC
		internal void Flush()
		{
			if (this._loader != null)
			{
				this._loader.Flush();
			}
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00110A14 File Offset: 0x0010FA14
		protected override object GetService(Type service)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			object service2 = base.GetService(service);
			if (service2 == null && this._surface != null)
			{
				service2 = this._surface.GetService(service);
			}
			return service2;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00110A54 File Offset: 0x0010FA54
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

		// Token: 0x06002FCB RID: 12235 RVA: 0x00110AC4 File Offset: 0x0010FAC4
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

		// Token: 0x06002FCC RID: 12236 RVA: 0x00110B54 File Offset: 0x0010FB54
		private void OnLoadComplete(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventLoadComplete] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x00110B84 File Offset: 0x0010FB84
		private void OnTransactionClosed(DesignerTransactionCloseEventArgs e)
		{
			DesignerTransactionCloseEventHandler designerTransactionCloseEventHandler = this._events[DesignerHost.EventTransactionClosed] as DesignerTransactionCloseEventHandler;
			if (designerTransactionCloseEventHandler != null)
			{
				designerTransactionCloseEventHandler(this, e);
			}
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00110BB4 File Offset: 0x0010FBB4
		private void OnTransactionClosing(DesignerTransactionCloseEventArgs e)
		{
			DesignerTransactionCloseEventHandler designerTransactionCloseEventHandler = this._events[DesignerHost.EventTransactionClosing] as DesignerTransactionCloseEventHandler;
			if (designerTransactionCloseEventHandler != null)
			{
				designerTransactionCloseEventHandler(this, e);
			}
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x00110BE4 File Offset: 0x0010FBE4
		private void OnTransactionOpened(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventTransactionOpened] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00110C14 File Offset: 0x0010FC14
		private void OnTransactionOpening(EventArgs e)
		{
			EventHandler eventHandler = this._events[DesignerHost.EventTransactionOpening] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00110C44 File Offset: 0x0010FC44
		public override void Remove(IComponent component)
		{
			if (this.RemoveFromContainerPreProcess(component, this))
			{
				DesignerHost.Site site = component.Site as DesignerHost.Site;
				base.RemoveWithoutUnsiting(component);
				site.Disposed = true;
				this.RemoveFromContainerPostProcess(component, this);
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00110C80 File Offset: 0x0010FC80
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

		// Token: 0x06002FD3 RID: 12243 RVA: 0x00110D7C File Offset: 0x0010FD7C
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

		// Token: 0x06002FD4 RID: 12244 RVA: 0x00110DCC File Offset: 0x0010FDCC
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
					if (!object.ReferenceEquals(component, this._rootComponent))
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
								if (designer == null)
								{
									string empty = string.Empty;
								}
								else
								{
									string name = designer.GetType().Name;
								}
								arrayList.Add(value);
							}
						}
						try
						{
							component.Dispose();
						}
						catch (Exception value2)
						{
							if (component == null)
							{
								string empty2 = string.Empty;
							}
							else
							{
								string name2 = component.GetType().Name;
							}
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
							if (designer2 == null)
							{
								string empty3 = string.Empty;
							}
							else
							{
								string name3 = designer2.GetType().Name;
							}
							arrayList.Add(value3);
						}
					}
					try
					{
						this._rootComponent.Dispose();
					}
					catch (Exception value4)
					{
						if (this._rootComponent == null)
						{
							string empty4 = string.Empty;
						}
						else
						{
							string name4 = this._rootComponent.GetType().Name;
						}
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

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002FD5 RID: 12245 RVA: 0x00111120 File Offset: 0x00110120
		// (remove) Token: 0x06002FD6 RID: 12246 RVA: 0x00111133 File Offset: 0x00110133
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

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002FD7 RID: 12247 RVA: 0x00111146 File Offset: 0x00110146
		// (remove) Token: 0x06002FD8 RID: 12248 RVA: 0x00111159 File Offset: 0x00110159
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

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06002FD9 RID: 12249 RVA: 0x0011116C File Offset: 0x0011016C
		// (remove) Token: 0x06002FDA RID: 12250 RVA: 0x0011117F File Offset: 0x0011017F
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

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002FDB RID: 12251 RVA: 0x00111192 File Offset: 0x00110192
		// (remove) Token: 0x06002FDC RID: 12252 RVA: 0x001111A5 File Offset: 0x001101A5
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

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002FDD RID: 12253 RVA: 0x001111B8 File Offset: 0x001101B8
		// (remove) Token: 0x06002FDE RID: 12254 RVA: 0x001111CB File Offset: 0x001101CB
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

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002FDF RID: 12255 RVA: 0x001111DE File Offset: 0x001101DE
		// (remove) Token: 0x06002FE0 RID: 12256 RVA: 0x001111F1 File Offset: 0x001101F1
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

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002FE1 RID: 12257 RVA: 0x00111204 File Offset: 0x00110204
		// (remove) Token: 0x06002FE2 RID: 12258 RVA: 0x00111217 File Offset: 0x00110217
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

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0011122C File Offset: 0x0011022C
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

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0011126C File Offset: 0x0011026C
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

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x001112A8 File Offset: 0x001102A8
		bool IDesignerHost.Loading
		{
			get
			{
				return this._state[DesignerHost.StateLoading] || this._state[DesignerHost.StateUnloading] || (this._loader != null && this._loader.Loading);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x001112E5 File Offset: 0x001102E5
		bool IDesignerHost.InTransaction
		{
			get
			{
				return (this._transactions != null && this._transactions.Count > 0) || this.IsClosingTransaction;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x00111305 File Offset: 0x00110305
		IContainer IDesignerHost.Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00111308 File Offset: 0x00110308
		IComponent IDesignerHost.RootComponent
		{
			get
			{
				return this._rootComponent;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x00111310 File Offset: 0x00110310
		string IDesignerHost.RootComponentClassName
		{
			get
			{
				return this._rootComponentClassName;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x00111318 File Offset: 0x00110318
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

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002FEB RID: 12267 RVA: 0x00111347 File Offset: 0x00110347
		// (remove) Token: 0x06002FEC RID: 12268 RVA: 0x0011135A File Offset: 0x0011035A
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

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002FED RID: 12269 RVA: 0x0011136D File Offset: 0x0011036D
		// (remove) Token: 0x06002FEE RID: 12270 RVA: 0x00111380 File Offset: 0x00110380
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

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002FEF RID: 12271 RVA: 0x00111393 File Offset: 0x00110393
		// (remove) Token: 0x06002FF0 RID: 12272 RVA: 0x001113A6 File Offset: 0x001103A6
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

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002FF1 RID: 12273 RVA: 0x001113B9 File Offset: 0x001103B9
		// (remove) Token: 0x06002FF2 RID: 12274 RVA: 0x001113CC File Offset: 0x001103CC
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

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002FF3 RID: 12275 RVA: 0x001113DF File Offset: 0x001103DF
		// (remove) Token: 0x06002FF4 RID: 12276 RVA: 0x001113F2 File Offset: 0x001103F2
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

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06002FF5 RID: 12277 RVA: 0x00111405 File Offset: 0x00110405
		// (remove) Token: 0x06002FF6 RID: 12278 RVA: 0x00111418 File Offset: 0x00110418
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

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06002FF7 RID: 12279 RVA: 0x0011142B File Offset: 0x0011042B
		// (remove) Token: 0x06002FF8 RID: 12280 RVA: 0x0011143E File Offset: 0x0011043E
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

		// Token: 0x06002FF9 RID: 12281 RVA: 0x00111451 File Offset: 0x00110451
		void IDesignerHost.Activate()
		{
			this._surface.OnViewActivate();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0011145E File Offset: 0x0011045E
		IComponent IDesignerHost.CreateComponent(Type componentType)
		{
			return ((IDesignerHost)this).CreateComponent(componentType, null);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x00111468 File Offset: 0x00110468
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
					this.Add(component, name);
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

		// Token: 0x06002FFC RID: 12284 RVA: 0x00111554 File Offset: 0x00110554
		DesignerTransaction IDesignerHost.CreateTransaction()
		{
			return ((IDesignerHost)this).CreateTransaction(null);
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0011155D File Offset: 0x0011055D
		DesignerTransaction IDesignerHost.CreateTransaction(string description)
		{
			if (description == null)
			{
				description = SR.GetString("DesignerHostGenericTransactionName");
			}
			return new DesignerHost.DesignerHostTransaction(this, description);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x00111578 File Offset: 0x00110578
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

		// Token: 0x06002FFF RID: 12287 RVA: 0x00111680 File Offset: 0x00110680
		IDesigner IDesignerHost.GetDesigner(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._designers[component] as IDesigner;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x001116A4 File Offset: 0x001106A4
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

		// Token: 0x06003001 RID: 12289 RVA: 0x001116E8 File Offset: 0x001106E8
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

		// Token: 0x06003002 RID: 12290 RVA: 0x0011192C File Offset: 0x0011092C
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

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x00111A08 File Offset: 0x00110A08
		// (set) Token: 0x06003004 RID: 12292 RVA: 0x00111A10 File Offset: 0x00110A10
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

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x00111A24 File Offset: 0x00110A24
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x00111A2C File Offset: 0x00110A2C
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

		// Token: 0x06003007 RID: 12295 RVA: 0x00111A35 File Offset: 0x00110A35
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IDesignerHost).GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00111A4D File Offset: 0x00110A4D
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMethod(name, bindingAttr);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00111A60 File Offset: 0x00110A60
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMethods(bindingAttr);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00111A72 File Offset: 0x00110A72
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetField(name, bindingAttr);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x00111A85 File Offset: 0x00110A85
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetFields(bindingAttr);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00111A97 File Offset: 0x00110A97
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetProperty(name, bindingAttr);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00111AAA File Offset: 0x00110AAA
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return typeof(IDesignerHost).GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00111AC4 File Offset: 0x00110AC4
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetProperties(bindingAttr);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00111AD6 File Offset: 0x00110AD6
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMember(name, bindingAttr);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00111AE9 File Offset: 0x00110AE9
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return typeof(IDesignerHost).GetMembers(bindingAttr);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x00111AFC File Offset: 0x00110AFC
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return typeof(IDesignerHost).InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x00111B25 File Offset: 0x00110B25
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return typeof(IDesignerHost).UnderlyingSystemType;
			}
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x00111B38 File Offset: 0x00110B38
		void IServiceContainer.AddService(Type serviceType, object serviceInstance)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, serviceInstance);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00111B74 File Offset: 0x00110B74
		void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, serviceInstance, promote);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x00111BB0 File Offset: 0x00110BB0
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, callback);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00111BEC File Offset: 0x00110BEC
		void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.AddService(serviceType, callback, promote);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00111C28 File Offset: 0x00110C28
		void IServiceContainer.RemoveService(Type serviceType)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.RemoveService(serviceType);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00111C60 File Offset: 0x00110C60
		void IServiceContainer.RemoveService(Type serviceType, bool promote)
		{
			IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
			if (serviceContainer == null)
			{
				throw new ObjectDisposedException("IServiceContainer");
			}
			serviceContainer.RemoveService(serviceType, promote);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00111C99 File Offset: 0x00110C99
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x04002063 RID: 8291
		private static readonly int StateLoading = BitVector32.CreateMask();

		// Token: 0x04002064 RID: 8292
		private static readonly int StateUnloading = BitVector32.CreateMask(DesignerHost.StateLoading);

		// Token: 0x04002065 RID: 8293
		private static readonly int StateIsClosingTransaction = BitVector32.CreateMask(DesignerHost.StateUnloading);

		// Token: 0x04002066 RID: 8294
		private static Type[] DefaultServices = new Type[]
		{
			typeof(IDesignerHost),
			typeof(IContainer),
			typeof(IComponentChangeService),
			typeof(IDesignerLoaderHost2)
		};

		// Token: 0x04002067 RID: 8295
		private static readonly object EventActivated = new object();

		// Token: 0x04002068 RID: 8296
		private static readonly object EventDeactivated = new object();

		// Token: 0x04002069 RID: 8297
		private static readonly object EventLoadComplete = new object();

		// Token: 0x0400206A RID: 8298
		private static readonly object EventTransactionClosed = new object();

		// Token: 0x0400206B RID: 8299
		private static readonly object EventTransactionClosing = new object();

		// Token: 0x0400206C RID: 8300
		private static readonly object EventTransactionOpened = new object();

		// Token: 0x0400206D RID: 8301
		private static readonly object EventTransactionOpening = new object();

		// Token: 0x0400206E RID: 8302
		private static readonly object EventComponentAdding = new object();

		// Token: 0x0400206F RID: 8303
		private static readonly object EventComponentAdded = new object();

		// Token: 0x04002070 RID: 8304
		private static readonly object EventComponentChanging = new object();

		// Token: 0x04002071 RID: 8305
		private static readonly object EventComponentChanged = new object();

		// Token: 0x04002072 RID: 8306
		private static readonly object EventComponentRemoving = new object();

		// Token: 0x04002073 RID: 8307
		private static readonly object EventComponentRemoved = new object();

		// Token: 0x04002074 RID: 8308
		private static readonly object EventComponentRename = new object();

		// Token: 0x04002075 RID: 8309
		private BitVector32 _state;

		// Token: 0x04002076 RID: 8310
		private DesignSurface _surface;

		// Token: 0x04002077 RID: 8311
		private string _newComponentName;

		// Token: 0x04002078 RID: 8312
		private Stack _transactions;

		// Token: 0x04002079 RID: 8313
		private IComponent _rootComponent;

		// Token: 0x0400207A RID: 8314
		private string _rootComponentClassName;

		// Token: 0x0400207B RID: 8315
		private Hashtable _designers;

		// Token: 0x0400207C RID: 8316
		private EventHandlerList _events;

		// Token: 0x0400207D RID: 8317
		private DesignerLoader _loader;

		// Token: 0x0400207E RID: 8318
		private ICollection _savedSelection;

		// Token: 0x0400207F RID: 8319
		private HostDesigntimeLicenseContext _licenseCtx;

		// Token: 0x04002080 RID: 8320
		private IDesignerEventService _designerEventService;

		// Token: 0x04002081 RID: 8321
		private static readonly object _selfLock = new object();

		// Token: 0x04002082 RID: 8322
		private bool _ignoreErrorsDuringReload;

		// Token: 0x04002083 RID: 8323
		private bool _canReloadWithErrors;

		// Token: 0x02000553 RID: 1363
		private sealed class DesignerHostTransaction : DesignerTransaction
		{
			// Token: 0x0600301B RID: 12315 RVA: 0x00111DB0 File Offset: 0x00110DB0
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

			// Token: 0x0600301C RID: 12316 RVA: 0x00111E1C File Offset: 0x00110E1C
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

			// Token: 0x0600301D RID: 12317 RVA: 0x00111EFC File Offset: 0x00110EFC
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

			// Token: 0x04002084 RID: 8324
			private DesignerHost _host;
		}

		// Token: 0x02000554 RID: 1364
		internal class Site : ISite, IServiceContainer, IServiceProvider, IDictionaryService
		{
			// Token: 0x0600301E RID: 12318 RVA: 0x00111FDC File Offset: 0x00110FDC
			internal Site(IComponent component, DesignerHost host, string name, Container container)
			{
				this._component = component;
				this._host = host;
				this._name = name;
				this._container = container;
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x0600301F RID: 12319 RVA: 0x00112004 File Offset: 0x00111004
			private IServiceContainer SiteServiceContainer
			{
				get
				{
					SiteNestedContainer siteNestedContainer = ((IServiceProvider)this).GetService(typeof(INestedContainer)) as SiteNestedContainer;
					return siteNestedContainer.GetServiceInternal(typeof(IServiceContainer)) as IServiceContainer;
				}
			}

			// Token: 0x06003020 RID: 12320 RVA: 0x00112040 File Offset: 0x00111040
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

			// Token: 0x06003021 RID: 12321 RVA: 0x001120C0 File Offset: 0x001110C0
			object IDictionaryService.GetValue(object key)
			{
				if (this._dictionary != null)
				{
					return this._dictionary[key];
				}
				return null;
			}

			// Token: 0x06003022 RID: 12322 RVA: 0x001120D8 File Offset: 0x001110D8
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

			// Token: 0x06003023 RID: 12323 RVA: 0x0011210A File Offset: 0x0011110A
			void IServiceContainer.AddService(Type serviceType, object serviceInstance)
			{
				this.SiteServiceContainer.AddService(serviceType, serviceInstance);
			}

			// Token: 0x06003024 RID: 12324 RVA: 0x00112119 File Offset: 0x00111119
			void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
			{
				this.SiteServiceContainer.AddService(serviceType, serviceInstance, promote);
			}

			// Token: 0x06003025 RID: 12325 RVA: 0x00112129 File Offset: 0x00111129
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
			{
				this.SiteServiceContainer.AddService(serviceType, callback);
			}

			// Token: 0x06003026 RID: 12326 RVA: 0x00112138 File Offset: 0x00111138
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
			{
				this.SiteServiceContainer.AddService(serviceType, callback, promote);
			}

			// Token: 0x06003027 RID: 12327 RVA: 0x00112148 File Offset: 0x00111148
			void IServiceContainer.RemoveService(Type serviceType)
			{
				this.SiteServiceContainer.RemoveService(serviceType);
			}

			// Token: 0x06003028 RID: 12328 RVA: 0x00112156 File Offset: 0x00111156
			void IServiceContainer.RemoveService(Type serviceType, bool promote)
			{
				this.SiteServiceContainer.RemoveService(serviceType, promote);
			}

			// Token: 0x06003029 RID: 12329 RVA: 0x00112168 File Offset: 0x00111168
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

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x0600302A RID: 12330 RVA: 0x00112201 File Offset: 0x00111201
			IComponent ISite.Component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x0600302B RID: 12331 RVA: 0x00112209 File Offset: 0x00111209
			IContainer ISite.Container
			{
				get
				{
					return this._container;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x0600302C RID: 12332 RVA: 0x00112211 File Offset: 0x00111211
			bool ISite.DesignMode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x0600302D RID: 12333 RVA: 0x00112214 File Offset: 0x00111214
			// (set) Token: 0x0600302E RID: 12334 RVA: 0x0011221C File Offset: 0x0011121C
			internal bool Disposed
			{
				get
				{
					return this._disposed;
				}
				set
				{
					this._disposed = value;
				}
			}

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x0600302F RID: 12335 RVA: 0x00112225 File Offset: 0x00111225
			// (set) Token: 0x06003030 RID: 12336 RVA: 0x00112230 File Offset: 0x00111230
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

			// Token: 0x04002085 RID: 8325
			private IComponent _component;

			// Token: 0x04002086 RID: 8326
			private Hashtable _dictionary;

			// Token: 0x04002087 RID: 8327
			private DesignerHost _host;

			// Token: 0x04002088 RID: 8328
			private string _name;

			// Token: 0x04002089 RID: 8329
			private bool _disposed;

			// Token: 0x0400208A RID: 8330
			private SiteNestedContainer _nestedContainer;

			// Token: 0x0400208B RID: 8331
			private Container _container;
		}
	}
}
