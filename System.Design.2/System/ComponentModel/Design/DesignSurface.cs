using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C5 RID: 453
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignSurface : IDisposable, IServiceProvider
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x0005DBB0 File Offset: 0x0005BDB0
		public DesignSurface() : this(null)
		{
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0005DBBC File Offset: 0x0005BDBC
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

		// Token: 0x060010BB RID: 4283 RVA: 0x0005DC84 File Offset: 0x0005BE84
		public DesignSurface(Type rootComponentType) : this(null, rootComponentType)
		{
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0005DC8E File Offset: 0x0005BE8E
		public DesignSurface(IServiceProvider parentProvider, Type rootComponentType) : this(parentProvider)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			this.BeginLoad(rootComponentType);
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0005DCB2 File Offset: 0x0005BEB2
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0005DCD8 File Offset: 0x0005BED8
		public bool IsLoaded
		{
			get
			{
				return this._loaded;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0005DCE0 File Offset: 0x0005BEE0
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

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0005DCF7 File Offset: 0x0005BEF7
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x0005DCFF File Offset: 0x0005BEFF
		public bool DtelLoading { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0005DD08 File Offset: 0x0005BF08
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

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0005DD2C File Offset: 0x0005BF2C
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060010C4 RID: 4292 RVA: 0x0005DE68 File Offset: 0x0005C068
		// (remove) Token: 0x060010C5 RID: 4293 RVA: 0x0005DEA0 File Offset: 0x0005C0A0
		public event EventHandler Disposed;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060010C6 RID: 4294 RVA: 0x0005DED8 File Offset: 0x0005C0D8
		// (remove) Token: 0x060010C7 RID: 4295 RVA: 0x0005DF10 File Offset: 0x0005C110
		public event EventHandler Flushed;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060010C8 RID: 4296 RVA: 0x0005DF48 File Offset: 0x0005C148
		// (remove) Token: 0x060010C9 RID: 4297 RVA: 0x0005DF80 File Offset: 0x0005C180
		public event LoadedEventHandler Loaded;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060010CA RID: 4298 RVA: 0x0005DFB8 File Offset: 0x0005C1B8
		// (remove) Token: 0x060010CB RID: 4299 RVA: 0x0005DFF0 File Offset: 0x0005C1F0
		public event EventHandler Loading;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060010CC RID: 4300 RVA: 0x0005E028 File Offset: 0x0005C228
		// (remove) Token: 0x060010CD RID: 4301 RVA: 0x0005E060 File Offset: 0x0005C260
		public event EventHandler Unloaded;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060010CE RID: 4302 RVA: 0x0005E098 File Offset: 0x0005C298
		// (remove) Token: 0x060010CF RID: 4303 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
		public event EventHandler Unloading;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060010D0 RID: 4304 RVA: 0x0005E108 File Offset: 0x0005C308
		// (remove) Token: 0x060010D1 RID: 4305 RVA: 0x0005E140 File Offset: 0x0005C340
		public event EventHandler ViewActivated;

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005E175 File Offset: 0x0005C375
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

		// Token: 0x060010D3 RID: 4307 RVA: 0x0005E1B1 File Offset: 0x0005C3B1
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

		// Token: 0x060010D4 RID: 4308 RVA: 0x0005E1EC File Offset: 0x0005C3EC
		[Obsolete("CreateComponent has been replaced by CreateInstance and will be removed after Beta2")]
		protected internal virtual IComponent CreateComponent(Type componentType)
		{
			return this.CreateInstance(componentType) as IComponent;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0005E1FC File Offset: 0x0005C3FC
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

		// Token: 0x060010D6 RID: 4310 RVA: 0x0005E260 File Offset: 0x0005C460
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

		// Token: 0x060010D7 RID: 4311 RVA: 0x0005E334 File Offset: 0x0005C534
		public INestedContainer CreateNestedContainer(IComponent owningComponent)
		{
			return this.CreateNestedContainer(owningComponent, null);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0005E33E File Offset: 0x0005C53E
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

		// Token: 0x060010D9 RID: 4313 RVA: 0x0005E374 File Offset: 0x0005C574
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0005E380 File Offset: 0x0005C580
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

		// Token: 0x060010DB RID: 4315 RVA: 0x0005E414 File Offset: 0x0005C614
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

		// Token: 0x060010DC RID: 4316 RVA: 0x0005E442 File Offset: 0x0005C642
		public object GetService(Type serviceType)
		{
			if (this._serviceContainer != null)
			{
				return this._serviceContainer.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0005E45A File Offset: 0x0005C65A
		internal void OnViewActivate()
		{
			this.OnViewActivate(EventArgs.Empty);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0005E468 File Offset: 0x0005C668
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

		// Token: 0x060010DF RID: 4319 RVA: 0x0005E4FC File Offset: 0x0005C6FC
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

		// Token: 0x060010E0 RID: 4320 RVA: 0x0005E56F File Offset: 0x0005C76F
		protected virtual void OnLoaded(LoadedEventArgs e)
		{
			if (this.Loaded != null)
			{
				this.Loaded(this, e);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005E586 File Offset: 0x0005C786
		internal void OnLoading()
		{
			this.OnLoading(EventArgs.Empty);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0005E593 File Offset: 0x0005C793
		protected virtual void OnLoading(EventArgs e)
		{
			if (this.Loading != null)
			{
				this.Loading(this, e);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0005E5AA File Offset: 0x0005C7AA
		internal void OnUnloaded()
		{
			this.OnUnloaded(EventArgs.Empty);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0005E5B7 File Offset: 0x0005C7B7
		protected virtual void OnUnloaded(EventArgs e)
		{
			if (this.Unloaded != null)
			{
				this.Unloaded(this, e);
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005E5CE File Offset: 0x0005C7CE
		internal void OnUnloading()
		{
			this.OnUnloading(EventArgs.Empty);
			this._loaded = false;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005E5E2 File Offset: 0x0005C7E2
		protected virtual void OnUnloading(EventArgs e)
		{
			if (this.Unloading != null)
			{
				this.Unloading(this, e);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0005E5F9 File Offset: 0x0005C7F9
		protected virtual void OnViewActivate(EventArgs e)
		{
			if (this.ViewActivated != null)
			{
				this.ViewActivated(this, e);
			}
		}

		// Token: 0x04000995 RID: 2453
		private IServiceProvider _parentProvider;

		// Token: 0x04000996 RID: 2454
		private ServiceContainer _serviceContainer;

		// Token: 0x04000997 RID: 2455
		private DesignerHost _host;

		// Token: 0x04000998 RID: 2456
		private ICollection _loadErrors;

		// Token: 0x04000999 RID: 2457
		private bool _loaded;

		// Token: 0x0200049A RID: 1178
		private class DefaultDesignerLoader : DesignerLoader
		{
			// Token: 0x06002B6E RID: 11118 RVA: 0x001039C1 File Offset: 0x00101BC1
			public DefaultDesignerLoader(Type type)
			{
				this._type = type;
			}

			// Token: 0x06002B6F RID: 11119 RVA: 0x001039D0 File Offset: 0x00101BD0
			public DefaultDesignerLoader(ICollection components)
			{
				this._components = components;
			}

			// Token: 0x06002B70 RID: 11120 RVA: 0x001039E0 File Offset: 0x00101BE0
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

			// Token: 0x06002B71 RID: 11121 RVA: 0x00003937 File Offset: 0x00001B37
			public override void Dispose()
			{
			}

			// Token: 0x04001E26 RID: 7718
			private Type _type;

			// Token: 0x04001E27 RID: 7719
			private ICollection _components;
		}
	}
}
