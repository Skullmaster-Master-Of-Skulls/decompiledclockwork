using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x02000557 RID: 1367
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignSurface : IDisposable, IServiceProvider
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x00112553 File Offset: 0x00111553
		public DesignSurface() : this(null)
		{
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0011255C File Offset: 0x0011155C
		public DesignSurface(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
			this._serviceContainer = new DesignSurfaceServiceContainer(this._parentProvider);
			ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
			this.ServiceContainer.AddService(typeof(ISelectionService), callback);
			this.ServiceContainer.AddService(typeof(IExtenderProviderService), callback);
			this.ServiceContainer.AddService(typeof(IExtenderListService), callback);
			this.ServiceContainer.AddService(typeof(ITypeDescriptorFilterService), callback);
			this.ServiceContainer.AddService(typeof(IReferenceService), callback);
			this.ServiceContainer.AddService(typeof(DesignSurface), this);
			this._host = new DesignerHost(this);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00112624 File Offset: 0x00111624
		public DesignSurface(Type rootComponentType) : this(null, rootComponentType)
		{
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0011262E File Offset: 0x0011162E
		public DesignSurface(IServiceProvider parentProvider, Type rootComponentType) : this(parentProvider)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			this.BeginLoad(rootComponentType);
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600303E RID: 12350 RVA: 0x0011264C File Offset: 0x0011164C
		public IContainer ComponentContainer
		{
			get
			{
				if (this._host == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return ((IDesignerHost)this._host).Container;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x00112672 File Offset: 0x00111672
		public bool IsLoaded
		{
			get
			{
				return this._loaded;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003040 RID: 12352 RVA: 0x0011267A File Offset: 0x0011167A
		public ICollection LoadErrors
		{
			get
			{
				if (this._loadErrors != null)
				{
					return this._loadErrors;
				}
				return new object[0];
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x00112691 File Offset: 0x00111691
		protected ServiceContainer ServiceContainer
		{
			get
			{
				if (this._serviceContainer == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return this._serviceContainer;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x001126B4 File Offset: 0x001116B4
		public object View
		{
			get
			{
				if (this._host == null)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				IComponent rootComponent = ((IDesignerHost)this._host).RootComponent;
				if (rootComponent == null)
				{
					if (this._loadErrors != null)
					{
						using (IEnumerator enumerator = this._loadErrors.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Exception ex = obj as Exception;
								if (ex != null)
								{
									throw new InvalidOperationException(ex.Message, ex);
								}
								throw new InvalidOperationException(obj.ToString());
							}
						}
					}
					throw new InvalidOperationException(SR.GetString("DesignSurfaceNoRootComponent"))
					{
						HelpLink = "DesignSurfaceNoRootComponent"
					};
				}
				IRootDesigner rootDesigner = ((IDesignerHost)this._host).GetDesigner(rootComponent) as IRootDesigner;
				if (rootDesigner == null)
				{
					throw new InvalidOperationException(SR.GetString("DesignSurfaceDesignerNotLoaded"))
					{
						HelpLink = "DesignSurfaceDesignerNotLoaded"
					};
				}
				ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
				ViewTechnology[] array = supportedTechnologies;
				int num = 0;
				if (num >= array.Length)
				{
					throw new NotSupportedException(SR.GetString("DesignSurfaceNoSupportedTechnology"))
					{
						HelpLink = "DesignSurfaceNoSupportedTechnology"
					};
				}
				ViewTechnology technology = array[num];
				return rootDesigner.GetView(technology);
			}
		}

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003043 RID: 12355 RVA: 0x001127F0 File Offset: 0x001117F0
		// (remove) Token: 0x06003044 RID: 12356 RVA: 0x00112809 File Offset: 0x00111809
		public event EventHandler Disposed;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06003045 RID: 12357 RVA: 0x00112822 File Offset: 0x00111822
		// (remove) Token: 0x06003046 RID: 12358 RVA: 0x0011283B File Offset: 0x0011183B
		public event EventHandler Flushed;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06003047 RID: 12359 RVA: 0x00112854 File Offset: 0x00111854
		// (remove) Token: 0x06003048 RID: 12360 RVA: 0x0011286D File Offset: 0x0011186D
		public event LoadedEventHandler Loaded;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06003049 RID: 12361 RVA: 0x00112886 File Offset: 0x00111886
		// (remove) Token: 0x0600304A RID: 12362 RVA: 0x0011289F File Offset: 0x0011189F
		public event EventHandler Loading;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600304B RID: 12363 RVA: 0x001128B8 File Offset: 0x001118B8
		// (remove) Token: 0x0600304C RID: 12364 RVA: 0x001128D1 File Offset: 0x001118D1
		public event EventHandler Unloaded;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x0600304D RID: 12365 RVA: 0x001128EA File Offset: 0x001118EA
		// (remove) Token: 0x0600304E RID: 12366 RVA: 0x00112903 File Offset: 0x00111903
		public event EventHandler Unloading;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x0600304F RID: 12367 RVA: 0x0011291C File Offset: 0x0011191C
		// (remove) Token: 0x06003050 RID: 12368 RVA: 0x00112935 File Offset: 0x00111935
		public event EventHandler ViewActivated;

		// Token: 0x06003051 RID: 12369 RVA: 0x0011294E File Offset: 0x0011194E
		public void BeginLoad(DesignerLoader loader)
		{
			if (loader == null)
			{
				throw new ArgumentNullException("loader");
			}
			if (this._host == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this._loadErrors = null;
			this._host.BeginLoad(loader);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0011298A File Offset: 0x0011198A
		public void BeginLoad(Type rootComponentType)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			if (this._host == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			this.BeginLoad(new DesignSurface.DefaultDesignerLoader(rootComponentType));
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x001129BF File Offset: 0x001119BF
		[Obsolete("CreateComponent has been replaced by CreateInstance and will be removed after Beta2")]
		protected internal virtual IComponent CreateComponent(Type componentType)
		{
			return this.CreateInstance(componentType) as IComponent;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x001129D0 File Offset: 0x001119D0
		protected internal virtual IDesigner CreateDesigner(IComponent component, bool rootDesigner)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (this._host == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			IDesigner result;
			if (rootDesigner)
			{
				result = (TypeDescriptor.CreateDesigner(component, typeof(IRootDesigner)) as IRootDesigner);
			}
			else
			{
				result = TypeDescriptor.CreateDesigner(component, typeof(IDesigner));
			}
			return result;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x00112A34 File Offset: 0x00111A34
		protected internal virtual object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = null;
			ConstructorInfo constructor = TypeDescriptor.GetReflectionType(type).GetConstructor(new Type[0]);
			if (constructor != null)
			{
				obj = TypeDescriptor.CreateInstance(this, type, new Type[0], new object[0]);
			}
			else
			{
				if (typeof(IComponent).IsAssignableFrom(type))
				{
					constructor = TypeDescriptor.GetReflectionType(type).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.ExactBinding, null, new Type[]
					{
						typeof(IContainer)
					}, null);
				}
				if (constructor != null)
				{
					obj = TypeDescriptor.CreateInstance(this, type, new Type[]
					{
						typeof(IContainer)
					}, new object[]
					{
						this.ComponentContainer
					});
				}
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
			}
			return obj;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x00112AFF File Offset: 0x00111AFF
		public INestedContainer CreateNestedContainer(IComponent owningComponent)
		{
			return this.CreateNestedContainer(owningComponent, null);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x00112B09 File Offset: 0x00111B09
		public INestedContainer CreateNestedContainer(IComponent owningComponent, string containerName)
		{
			if (this._host == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (owningComponent == null)
			{
				throw new ArgumentNullException("owningComponent");
			}
			return new SiteNestedContainer(owningComponent, containerName, this._host);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x00112B3F File Offset: 0x00111B3F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x00112B48 File Offset: 0x00111B48
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.Disposed != null)
				{
					this.Disposed(this, EventArgs.Empty);
				}
				try
				{
					try
					{
						if (this._host != null)
						{
							this._host.DisposeHost();
						}
					}
					finally
					{
						if (this._serviceContainer != null)
						{
							this._serviceContainer.RemoveService(typeof(DesignSurface));
							this._serviceContainer.Dispose();
						}
					}
				}
				finally
				{
					this._host = null;
					this._serviceContainer = null;
				}
			}
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x00112BDC File Offset: 0x00111BDC
		public void Flush()
		{
			if (this._host != null)
			{
				this._host.Flush();
			}
			if (this.Flushed != null)
			{
				this.Flushed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x00112C0A File Offset: 0x00111C0A
		public object GetService(Type serviceType)
		{
			if (this._serviceContainer != null)
			{
				return this._serviceContainer.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x00112C22 File Offset: 0x00111C22
		internal void OnViewActivate()
		{
			this.OnViewActivate(EventArgs.Empty);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x00112C30 File Offset: 0x00111C30
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(ISelectionService))
			{
				return new SelectionService(container);
			}
			if (serviceType == typeof(IExtenderProviderService))
			{
				return new ExtenderProviderService();
			}
			if (serviceType == typeof(IExtenderListService))
			{
				return this.GetService(typeof(IExtenderProviderService));
			}
			if (serviceType == typeof(ITypeDescriptorFilterService))
			{
				return new TypeDescriptorFilterService();
			}
			if (serviceType == typeof(IReferenceService))
			{
				return new ReferenceService(container);
			}
			return null;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x00112CAC File Offset: 0x00111CAC
		internal void OnLoaded(bool successful, ICollection errors)
		{
			this._loaded = successful;
			this._loadErrors = errors;
			if (successful && ((IDesignerHost)this._host).RootComponent == null)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new InvalidOperationException(SR.GetString("DesignSurfaceNoRootComponent"))
				{
					HelpLink = "DesignSurfaceNoRootComponent"
				});
				if (errors != null)
				{
					arrayList.AddRange(errors);
				}
				errors = arrayList;
				successful = false;
			}
			this.OnLoaded(new LoadedEventArgs(successful, errors));
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x00112D1F File Offset: 0x00111D1F
		protected virtual void OnLoaded(LoadedEventArgs e)
		{
			if (this.Loaded != null)
			{
				this.Loaded(this, e);
			}
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x00112D36 File Offset: 0x00111D36
		internal void OnLoading()
		{
			this.OnLoading(EventArgs.Empty);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x00112D43 File Offset: 0x00111D43
		protected virtual void OnLoading(EventArgs e)
		{
			if (this.Loading != null)
			{
				this.Loading(this, e);
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x00112D5A File Offset: 0x00111D5A
		internal void OnUnloaded()
		{
			this.OnUnloaded(EventArgs.Empty);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x00112D67 File Offset: 0x00111D67
		protected virtual void OnUnloaded(EventArgs e)
		{
			if (this.Unloaded != null)
			{
				this.Unloaded(this, e);
			}
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x00112D7E File Offset: 0x00111D7E
		internal void OnUnloading()
		{
			this.OnUnloading(EventArgs.Empty);
			this._loaded = false;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x00112D92 File Offset: 0x00111D92
		protected virtual void OnUnloading(EventArgs e)
		{
			if (this.Unloading != null)
			{
				this.Unloading(this, e);
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00112DA9 File Offset: 0x00111DA9
		protected virtual void OnViewActivate(EventArgs e)
		{
			if (this.ViewActivated != null)
			{
				this.ViewActivated(this, e);
			}
		}

		// Token: 0x04002092 RID: 8338
		private IServiceProvider _parentProvider;

		// Token: 0x04002093 RID: 8339
		private ServiceContainer _serviceContainer;

		// Token: 0x04002094 RID: 8340
		private DesignerHost _host;

		// Token: 0x04002095 RID: 8341
		private ICollection _loadErrors;

		// Token: 0x04002096 RID: 8342
		private bool _loaded;

		// Token: 0x02000558 RID: 1368
		private class DefaultDesignerLoader : DesignerLoader
		{
			// Token: 0x06003067 RID: 12391 RVA: 0x00112DC0 File Offset: 0x00111DC0
			public DefaultDesignerLoader(Type type)
			{
				this._type = type;
			}

			// Token: 0x06003068 RID: 12392 RVA: 0x00112DCF File Offset: 0x00111DCF
			public DefaultDesignerLoader(ICollection components)
			{
				this._components = components;
			}

			// Token: 0x06003069 RID: 12393 RVA: 0x00112DE0 File Offset: 0x00111DE0
			public override void BeginLoad(IDesignerLoaderHost loaderHost)
			{
				string baseClassName = null;
				if (this._type != null)
				{
					loaderHost.CreateComponent(this._type);
					baseClassName = this._type.FullName;
				}
				else
				{
					foreach (object obj in this._components)
					{
						IComponent component = (IComponent)obj;
						loaderHost.Container.Add(component);
					}
				}
				loaderHost.EndLoad(baseClassName, true, null);
			}

			// Token: 0x0600306A RID: 12394 RVA: 0x00112E6C File Offset: 0x00111E6C
			public override void Dispose()
			{
			}

			// Token: 0x0400209E RID: 8350
			private Type _type;

			// Token: 0x0400209F RID: 8351
			private ICollection _components;
		}
	}
}
