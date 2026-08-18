using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000575 RID: 1397
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public abstract class BasicDesignerLoader : DesignerLoader, IDesignerLoaderService
	{
		// Token: 0x06003157 RID: 12631 RVA: 0x00116E98 File Offset: 0x00115E98
		protected BasicDesignerLoader()
		{
			this._state[BasicDesignerLoader.StateFlushInProgress] = false;
			this._state[BasicDesignerLoader.StateReloadSupported] = true;
			this._state[BasicDesignerLoader.StateEnableComponentEvents] = false;
			this._hostInitialized = false;
			this._loading = false;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x00116EF8 File Offset: 0x00115EF8
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x00116F0A File Offset: 0x00115F0A
		protected virtual bool Modified
		{
			get
			{
				return this._state[BasicDesignerLoader.StateModified];
			}
			set
			{
				this._state[BasicDesignerLoader.StateModified] = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x00116F1D File Offset: 0x00115F1D
		protected IDesignerLoaderHost LoaderHost
		{
			get
			{
				if (this._host != null)
				{
					return this._host;
				}
				if (this._hostInitialized)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderNotInitialized"));
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x00116F56 File Offset: 0x00115F56
		public override bool Loading
		{
			get
			{
				return this._loadDependencyCount > 0 || this._loading;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x00116F69 File Offset: 0x00115F69
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x00116F8E File Offset: 0x00115F8E
		protected object PropertyProvider
		{
			get
			{
				if (this._serializationManager == null)
				{
					throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderNotInitialized"));
				}
				return this._serializationManager.PropertyProvider;
			}
			set
			{
				if (this._serializationManager == null)
				{
					throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderNotInitialized"));
				}
				this._serializationManager.PropertyProvider = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x00116FB4 File Offset: 0x00115FB4
		protected bool ReloadPending
		{
			get
			{
				return this._state[BasicDesignerLoader.StateReloadAtIdle];
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x00116FC8 File Offset: 0x00115FC8
		public override void BeginLoad(IDesignerLoaderHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (this._state[BasicDesignerLoader.StateLoaded])
			{
				throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderAlreadyLoaded"))
				{
					HelpLink = "BasicDesignerLoaderAlreadyLoaded"
				};
			}
			if (this._host != null && this._host != host)
			{
				throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderDifferentHost"))
				{
					HelpLink = "BasicDesignerLoaderDifferentHost"
				};
			}
			this._state[BasicDesignerLoader.StateLoaded | BasicDesignerLoader.StateLoadFailed] = false;
			this._loadDependencyCount = 0;
			if (this._host == null)
			{
				this._host = host;
				this._hostInitialized = true;
				this._serializationManager = new DesignerSerializationManager(this._host);
				DesignSurfaceServiceContainer designSurfaceServiceContainer = this.GetService(typeof(DesignSurfaceServiceContainer)) as DesignSurfaceServiceContainer;
				if (designSurfaceServiceContainer != null)
				{
					designSurfaceServiceContainer.AddFixedService(typeof(IDesignerSerializationManager), this._serializationManager);
				}
				else
				{
					IServiceContainer serviceContainer = this.GetService(typeof(IServiceContainer)) as IServiceContainer;
					if (serviceContainer == null)
					{
						this.ThrowMissingService(typeof(IServiceContainer));
					}
					serviceContainer.AddService(typeof(IDesignerSerializationManager), this._serializationManager);
				}
				this.Initialize();
				host.Activated += this.OnDesignerActivate;
				host.Deactivated += this.OnDesignerDeactivate;
			}
			bool successful = true;
			ArrayList arrayList = null;
			IDesignerLoaderService designerLoaderService = this.GetService(typeof(IDesignerLoaderService)) as IDesignerLoaderService;
			try
			{
				if (designerLoaderService != null)
				{
					designerLoaderService.AddLoadDependency();
				}
				else
				{
					this._loading = true;
					this.OnBeginLoad();
				}
				this.PerformLoad(this._serializationManager);
			}
			catch (Exception innerException)
			{
				while (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				arrayList = new ArrayList();
				arrayList.Add(innerException);
				successful = false;
			}
			if (designerLoaderService != null)
			{
				designerLoaderService.DependentLoadComplete(successful, arrayList);
				return;
			}
			this.OnEndLoad(successful, arrayList);
			this._loading = false;
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x001171C4 File Offset: 0x001161C4
		public override void Dispose()
		{
			if (this._state[BasicDesignerLoader.StateReloadAtIdle])
			{
				Application.Idle -= this.OnIdle;
			}
			this.UnloadDocument();
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded -= this.OnComponentAdded;
				componentChangeService.ComponentAdding -= this.OnComponentAdding;
				componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
				componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				componentChangeService.ComponentChanged -= this.OnComponentChanged;
				componentChangeService.ComponentChanging -= this.OnComponentChanging;
				componentChangeService.ComponentRename -= this.OnComponentRename;
			}
			if (this._host != null)
			{
				this._host.RemoveService(typeof(IDesignerLoaderService));
				this._host.Activated -= this.OnDesignerActivate;
				this._host.Deactivated -= this.OnDesignerDeactivate;
				this._host = null;
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x001172E4 File Offset: 0x001162E4
		public override void Flush()
		{
			if (this._state[BasicDesignerLoader.StateFlushInProgress] || !this._state[BasicDesignerLoader.StateLoaded] || !this.Modified)
			{
				return;
			}
			this._state[BasicDesignerLoader.StateFlushInProgress] = true;
			Cursor value = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				IDesignerLoaderHost host = this._host;
				bool flag = true;
				if (host != null && host.RootComponent != null)
				{
					using (this._serializationManager.CreateSession())
					{
						try
						{
							this.PerformFlush(this._serializationManager);
						}
						catch (CheckoutException)
						{
							flag = false;
							throw;
						}
						catch (Exception value2)
						{
							this._serializationManager.Errors.Add(value2);
						}
						ICollection errors = this._serializationManager.Errors;
						if (errors != null && errors.Count > 0)
						{
							this.ReportFlushErrors(errors);
						}
					}
				}
				if (flag)
				{
					this.Modified = false;
				}
			}
			finally
			{
				this._state[BasicDesignerLoader.StateFlushInProgress] = false;
				Cursor.Current = value;
			}
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x00117414 File Offset: 0x00116414
		protected object GetService(Type serviceType)
		{
			object result = null;
			if (this._host != null)
			{
				result = this._host.GetService(serviceType);
			}
			return result;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00117439 File Offset: 0x00116439
		protected virtual void Initialize()
		{
			this.LoaderHost.AddService(typeof(IDesignerLoaderService), this);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x00117451 File Offset: 0x00116451
		protected virtual bool IsReloadNeeded()
		{
			return true;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x00117454 File Offset: 0x00116454
		protected virtual void OnBeginLoad()
		{
			this._serializationSession = this._serializationManager.CreateSession();
			this._state[BasicDesignerLoader.StateLoaded] = false;
			this.EnableComponentNotification(false);
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded -= this.OnComponentAdded;
				componentChangeService.ComponentAdding -= this.OnComponentAdding;
				componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
				componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				componentChangeService.ComponentChanged -= this.OnComponentChanged;
				componentChangeService.ComponentChanging -= this.OnComponentChanging;
				componentChangeService.ComponentRename -= this.OnComponentRename;
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x00117524 File Offset: 0x00116524
		protected virtual bool EnableComponentNotification(bool enable)
		{
			bool flag = this._state[BasicDesignerLoader.StateEnableComponentEvents];
			if (!flag && enable)
			{
				this._state[BasicDesignerLoader.StateEnableComponentEvents] = true;
			}
			else if (flag && !enable)
			{
				this._state[BasicDesignerLoader.StateEnableComponentEvents] = false;
			}
			return flag;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00117573 File Offset: 0x00116573
		protected virtual void OnBeginUnload()
		{
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00117575 File Offset: 0x00116575
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x0011759D File Offset: 0x0011659D
		private void OnComponentAdding(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x001175C4 File Offset: 0x001165C4
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x001175EC File Offset: 0x001165EC
		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00117613 File Offset: 0x00116613
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x0011763B File Offset: 0x0011663B
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00117662 File Offset: 0x00116662
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
				this.Modified = true;
			}
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00117690 File Offset: 0x00116690
		private void OnDesignerActivate(object sender, EventArgs e)
		{
			this._state[BasicDesignerLoader.StateActiveDocument] = true;
			if (this._state[BasicDesignerLoader.StateDeferredReload] && this._host != null)
			{
				this._state[BasicDesignerLoader.StateDeferredReload] = false;
				BasicDesignerLoader.ReloadOptions reloadOptions = BasicDesignerLoader.ReloadOptions.Default;
				if (this._state[BasicDesignerLoader.StateForceReload])
				{
					reloadOptions |= BasicDesignerLoader.ReloadOptions.Force;
				}
				if (!this._state[BasicDesignerLoader.StateFlushReload])
				{
					reloadOptions |= BasicDesignerLoader.ReloadOptions.NoFlush;
				}
				if (this._state[BasicDesignerLoader.StateModifyIfErrors])
				{
					reloadOptions |= BasicDesignerLoader.ReloadOptions.ModifyOnError;
				}
				this.Reload(reloadOptions);
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00117724 File Offset: 0x00116724
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			this._state[BasicDesignerLoader.StateActiveDocument] = false;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x00117738 File Offset: 0x00116738
		protected virtual void OnEndLoad(bool successful, ICollection errors)
		{
			successful = (successful && (errors == null || errors.Count == 0) && (this._serializationManager.Errors == null || this._serializationManager.Errors.Count == 0));
			try
			{
				this._state[BasicDesignerLoader.StateLoaded] = true;
				IDesignerLoaderHost2 designerLoaderHost = this.GetService(typeof(IDesignerLoaderHost2)) as IDesignerLoaderHost2;
				if (!successful && (designerLoaderHost == null || !designerLoaderHost.IgnoreErrorsDuringReload))
				{
					if (designerLoaderHost != null)
					{
						designerLoaderHost.CanReloadWithErrors = (this.LoaderHost.RootComponent != null);
					}
					this.UnloadDocument();
				}
				else
				{
					successful = true;
				}
				if (errors != null)
				{
					foreach (object value in errors)
					{
						this._serializationManager.Errors.Add(value);
					}
				}
				errors = this._serializationManager.Errors;
			}
			finally
			{
				this._serializationSession.Dispose();
				this._serializationSession = null;
			}
			if (successful)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded += this.OnComponentAdded;
					componentChangeService.ComponentAdding += this.OnComponentAdding;
					componentChangeService.ComponentRemoving += this.OnComponentRemoving;
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					componentChangeService.ComponentChanged += this.OnComponentChanged;
					componentChangeService.ComponentChanging += this.OnComponentChanging;
					componentChangeService.ComponentRename += this.OnComponentRename;
				}
				this.EnableComponentNotification(true);
			}
			this.LoaderHost.EndLoad(this._baseComponentClassName, successful, errors);
			if (this._state[BasicDesignerLoader.StateModifyIfErrors] && errors != null && errors.Count > 0)
			{
				try
				{
					this.OnModifying();
					this.Modified = true;
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x00117958 File Offset: 0x00116958
		protected virtual void OnModifying()
		{
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x0011795C File Offset: 0x0011695C
		private void OnIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.OnIdle;
			if (this._state[BasicDesignerLoader.StateReloadAtIdle])
			{
				this._state[BasicDesignerLoader.StateReloadAtIdle] = false;
				DesignSurfaceManager designSurfaceManager = (DesignSurfaceManager)this.GetService(typeof(DesignSurfaceManager));
				DesignSurface designSurface = (DesignSurface)this.GetService(typeof(DesignSurface));
				if (designSurfaceManager != null && designSurface != null && !object.ReferenceEquals(designSurfaceManager.ActiveDesignSurface, designSurface))
				{
					this._state[BasicDesignerLoader.StateActiveDocument] = false;
					this._state[BasicDesignerLoader.StateDeferredReload] = true;
					return;
				}
				IDesignerLoaderHost loaderHost = this.LoaderHost;
				if (loaderHost != null)
				{
					if (!this._state[BasicDesignerLoader.StateForceReload])
					{
						if (!this.IsReloadNeeded())
						{
							return;
						}
					}
					try
					{
						if (this._state[BasicDesignerLoader.StateFlushReload])
						{
							this.Flush();
						}
						this.UnloadDocument();
						loaderHost.Reload();
					}
					finally
					{
						this._state[BasicDesignerLoader.StateForceReload | BasicDesignerLoader.StateModifyIfErrors | BasicDesignerLoader.StateFlushReload] = false;
					}
				}
			}
		}

		// Token: 0x06003174 RID: 12660
		protected abstract void PerformFlush(IDesignerSerializationManager serializationManager);

		// Token: 0x06003175 RID: 12661
		protected abstract void PerformLoad(IDesignerSerializationManager serializationManager);

		// Token: 0x06003176 RID: 12662 RVA: 0x00117A7C File Offset: 0x00116A7C
		protected void Reload(BasicDesignerLoader.ReloadOptions flags)
		{
			this._state[BasicDesignerLoader.StateForceReload] = ((flags & BasicDesignerLoader.ReloadOptions.Force) != BasicDesignerLoader.ReloadOptions.Default);
			this._state[BasicDesignerLoader.StateFlushReload] = ((flags & BasicDesignerLoader.ReloadOptions.NoFlush) == BasicDesignerLoader.ReloadOptions.Default);
			this._state[BasicDesignerLoader.StateModifyIfErrors] = ((flags & BasicDesignerLoader.ReloadOptions.ModifyOnError) != BasicDesignerLoader.ReloadOptions.Default);
			if (!this._state[BasicDesignerLoader.StateFlushInProgress])
			{
				if (this._state[BasicDesignerLoader.StateActiveDocument])
				{
					if (!this._state[BasicDesignerLoader.StateReloadAtIdle])
					{
						Application.Idle += this.OnIdle;
						this._state[BasicDesignerLoader.StateReloadAtIdle] = true;
						return;
					}
				}
				else
				{
					this._state[BasicDesignerLoader.StateDeferredReload] = true;
				}
			}
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x00117B3C File Offset: 0x00116B3C
		protected virtual void ReportFlushErrors(ICollection errors)
		{
			object obj = null;
			foreach (object obj2 in errors)
			{
				obj = obj2;
			}
			if (obj != null)
			{
				Exception ex = obj as Exception;
				if (ex == null)
				{
					ex = new InvalidOperationException(obj.ToString());
				}
				throw ex;
			}
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x00117BA8 File Offset: 0x00116BA8
		protected void SetBaseComponentClassName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._baseComponentClassName = name;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00117BC0 File Offset: 0x00116BC0
		private void ThrowMissingService(Type serviceType)
		{
			throw new InvalidOperationException(SR.GetString("BasicDesignerLoaderMissingService", new object[]
			{
				serviceType.Name
			}))
			{
				HelpLink = "BasicDesignerLoaderMissingService"
			};
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x00117BFA File Offset: 0x00116BFA
		private void UnloadDocument()
		{
			this.OnBeginUnload();
			this._state[BasicDesignerLoader.StateLoaded] = false;
			this._baseComponentClassName = null;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x00117C1C File Offset: 0x00116C1C
		void IDesignerLoaderService.AddLoadDependency()
		{
			if (this._serializationManager == null)
			{
				throw new InvalidOperationException();
			}
			if (this._loadDependencyCount++ == 0)
			{
				this.OnBeginLoad();
			}
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00117C50 File Offset: 0x00116C50
		void IDesignerLoaderService.DependentLoadComplete(bool successful, ICollection errorCollection)
		{
			if (this._loadDependencyCount == 0)
			{
				throw new InvalidOperationException();
			}
			if (!successful)
			{
				this._state[BasicDesignerLoader.StateLoadFailed] = true;
			}
			if (--this._loadDependencyCount == 0)
			{
				this.OnEndLoad(!this._state[BasicDesignerLoader.StateLoadFailed], errorCollection);
				return;
			}
			if (errorCollection != null)
			{
				foreach (object value in errorCollection)
				{
					this._serializationManager.Errors.Add(value);
				}
			}
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x00117D00 File Offset: 0x00116D00
		bool IDesignerLoaderService.Reload()
		{
			if (this._state[BasicDesignerLoader.StateReloadSupported] && this._loadDependencyCount == 0)
			{
				this.Reload(BasicDesignerLoader.ReloadOptions.Force);
				return true;
			}
			return false;
		}

		// Token: 0x040020FA RID: 8442
		private static readonly int StateLoaded = BitVector32.CreateMask();

		// Token: 0x040020FB RID: 8443
		private static readonly int StateLoadFailed = BitVector32.CreateMask(BasicDesignerLoader.StateLoaded);

		// Token: 0x040020FC RID: 8444
		private static readonly int StateFlushInProgress = BitVector32.CreateMask(BasicDesignerLoader.StateLoadFailed);

		// Token: 0x040020FD RID: 8445
		private static readonly int StateModified = BitVector32.CreateMask(BasicDesignerLoader.StateFlushInProgress);

		// Token: 0x040020FE RID: 8446
		private static readonly int StateReloadSupported = BitVector32.CreateMask(BasicDesignerLoader.StateModified);

		// Token: 0x040020FF RID: 8447
		private static readonly int StateActiveDocument = BitVector32.CreateMask(BasicDesignerLoader.StateReloadSupported);

		// Token: 0x04002100 RID: 8448
		private static readonly int StateDeferredReload = BitVector32.CreateMask(BasicDesignerLoader.StateActiveDocument);

		// Token: 0x04002101 RID: 8449
		private static readonly int StateReloadAtIdle = BitVector32.CreateMask(BasicDesignerLoader.StateDeferredReload);

		// Token: 0x04002102 RID: 8450
		private static readonly int StateForceReload = BitVector32.CreateMask(BasicDesignerLoader.StateReloadAtIdle);

		// Token: 0x04002103 RID: 8451
		private static readonly int StateFlushReload = BitVector32.CreateMask(BasicDesignerLoader.StateForceReload);

		// Token: 0x04002104 RID: 8452
		private static readonly int StateModifyIfErrors = BitVector32.CreateMask(BasicDesignerLoader.StateFlushReload);

		// Token: 0x04002105 RID: 8453
		private static readonly int StateEnableComponentEvents = BitVector32.CreateMask(BasicDesignerLoader.StateModifyIfErrors);

		// Token: 0x04002106 RID: 8454
		private BitVector32 _state = default(BitVector32);

		// Token: 0x04002107 RID: 8455
		private IDesignerLoaderHost _host;

		// Token: 0x04002108 RID: 8456
		private int _loadDependencyCount;

		// Token: 0x04002109 RID: 8457
		private string _baseComponentClassName;

		// Token: 0x0400210A RID: 8458
		private bool _hostInitialized;

		// Token: 0x0400210B RID: 8459
		private bool _loading;

		// Token: 0x0400210C RID: 8460
		private DesignerSerializationManager _serializationManager;

		// Token: 0x0400210D RID: 8461
		private IDisposable _serializationSession;

		// Token: 0x02000576 RID: 1398
		[Flags]
		protected enum ReloadOptions
		{
			// Token: 0x0400210F RID: 8463
			Default = 0,
			// Token: 0x04002110 RID: 8464
			ModifyOnError = 1,
			// Token: 0x04002111 RID: 8465
			Force = 2,
			// Token: 0x04002112 RID: 8466
			NoFlush = 4
		}
	}
}
