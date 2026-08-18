using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001EE RID: 494
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class BasicDesignerLoader : DesignerLoader, IDesignerLoaderService
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0006B410 File Offset: 0x00069610
		protected BasicDesignerLoader()
		{
			this._state[BasicDesignerLoader.StateFlushInProgress] = false;
			this._state[BasicDesignerLoader.StateReloadSupported] = true;
			this._state[BasicDesignerLoader.StateEnableComponentEvents] = false;
			this._hostInitialized = false;
			this._loading = false;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0006B464 File Offset: 0x00069664
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x0006B476 File Offset: 0x00069676
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0006B489 File Offset: 0x00069689
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

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0006B4C2 File Offset: 0x000696C2
		public override bool Loading
		{
			get
			{
				return this._loadDependencyCount > 0 || this._loading;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0006B4D5 File Offset: 0x000696D5
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0006B4FA File Offset: 0x000696FA
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0006B520 File Offset: 0x00069720
		protected bool ReloadPending
		{
			get
			{
				return this._state[BasicDesignerLoader.StateReloadAtIdle];
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0006B534 File Offset: 0x00069734
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

		// Token: 0x0600127F RID: 4735 RVA: 0x0006B72C File Offset: 0x0006992C
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

		// Token: 0x06001280 RID: 4736 RVA: 0x0006B84C File Offset: 0x00069A4C
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

		// Token: 0x06001281 RID: 4737 RVA: 0x0006B978 File Offset: 0x00069B78
		protected object GetService(Type serviceType)
		{
			object result = null;
			if (this._host != null)
			{
				result = this._host.GetService(serviceType);
			}
			return result;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0006B99D File Offset: 0x00069B9D
		protected virtual void Initialize()
		{
			this.LoaderHost.AddService(typeof(IDesignerLoaderService), this);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool IsReloadNeeded()
		{
			return true;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0006B9B8 File Offset: 0x00069BB8
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

		// Token: 0x06001285 RID: 4741 RVA: 0x0006BA88 File Offset: 0x00069C88
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

		// Token: 0x06001286 RID: 4742 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnBeginUnload()
		{
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006BAD9 File Offset: 0x00069CD9
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0006BB01 File Offset: 0x00069D01
		private void OnComponentAdding(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0006BAD9 File Offset: 0x00069CD9
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0006BB01 File Offset: 0x00069D01
		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0006BAD9 File Offset: 0x00069CD9
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.Modified = true;
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0006BB01 File Offset: 0x00069D01
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0006BB28 File Offset: 0x00069D28
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (this._state[BasicDesignerLoader.StateEnableComponentEvents] && !this.LoaderHost.Loading)
			{
				this.OnModifying();
				this.Modified = true;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0006BB58 File Offset: 0x00069D58
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

		// Token: 0x0600128F RID: 4751 RVA: 0x0006BBEC File Offset: 0x00069DEC
		private void OnDesignerDeactivate(object sender, EventArgs e)
		{
			this._state[BasicDesignerLoader.StateActiveDocument] = false;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0006BC00 File Offset: 0x00069E00
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

		// Token: 0x06001291 RID: 4753 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnModifying()
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0006BE24 File Offset: 0x0006A024
		private void OnIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.OnIdle;
			if (this._state[BasicDesignerLoader.StateReloadAtIdle])
			{
				this._state[BasicDesignerLoader.StateReloadAtIdle] = false;
				DesignSurfaceManager designSurfaceManager = (DesignSurfaceManager)this.GetService(typeof(DesignSurfaceManager));
				DesignSurface designSurface = (DesignSurface)this.GetService(typeof(DesignSurface));
				if (designSurfaceManager != null && designSurface != null && designSurfaceManager.ActiveDesignSurface != designSurface)
				{
					this._state[BasicDesignerLoader.StateActiveDocument] = false;
					this._state[BasicDesignerLoader.StateDeferredReload] = true;
					return;
				}
				IDesignerLoaderHost loaderHost = this.LoaderHost;
				if (loaderHost != null && (this._state[BasicDesignerLoader.StateForceReload] || this.IsReloadNeeded()))
				{
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

		// Token: 0x06001293 RID: 4755
		protected abstract void PerformFlush(IDesignerSerializationManager serializationManager);

		// Token: 0x06001294 RID: 4756
		protected abstract void PerformLoad(IDesignerSerializationManager serializationManager);

		// Token: 0x06001295 RID: 4757 RVA: 0x0006BF40 File Offset: 0x0006A140
		protected void Reload(BasicDesignerLoader.ReloadOptions flags)
		{
			this._state[BasicDesignerLoader.StateForceReload] = ((flags & BasicDesignerLoader.ReloadOptions.Force) > BasicDesignerLoader.ReloadOptions.Default);
			this._state[BasicDesignerLoader.StateFlushReload] = ((flags & BasicDesignerLoader.ReloadOptions.NoFlush) == BasicDesignerLoader.ReloadOptions.Default);
			this._state[BasicDesignerLoader.StateModifyIfErrors] = ((flags & BasicDesignerLoader.ReloadOptions.ModifyOnError) > BasicDesignerLoader.ReloadOptions.Default);
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

		// Token: 0x06001296 RID: 4758 RVA: 0x0006BFFC File Offset: 0x0006A1FC
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

		// Token: 0x06001297 RID: 4759 RVA: 0x0006C068 File Offset: 0x0006A268
		protected void SetBaseComponentClassName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._baseComponentClassName = name;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0006C080 File Offset: 0x0006A280
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

		// Token: 0x06001299 RID: 4761 RVA: 0x0006C0B8 File Offset: 0x0006A2B8
		private void UnloadDocument()
		{
			this.OnBeginUnload();
			this._state[BasicDesignerLoader.StateLoaded] = false;
			this._baseComponentClassName = null;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0006C0D8 File Offset: 0x0006A2D8
		void IDesignerLoaderService.AddLoadDependency()
		{
			if (this._serializationManager == null)
			{
				throw new InvalidOperationException();
			}
			int loadDependencyCount = this._loadDependencyCount;
			this._loadDependencyCount = loadDependencyCount + 1;
			if (loadDependencyCount == 0)
			{
				this.OnBeginLoad();
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0006C10C File Offset: 0x0006A30C
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
			int num = this._loadDependencyCount - 1;
			this._loadDependencyCount = num;
			if (num == 0)
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

		// Token: 0x0600129C RID: 4764 RVA: 0x0006C1BC File Offset: 0x0006A3BC
		bool IDesignerLoaderService.Reload()
		{
			if (this._state[BasicDesignerLoader.StateReloadSupported] && this._loadDependencyCount == 0)
			{
				this.Reload(BasicDesignerLoader.ReloadOptions.Force);
				return true;
			}
			return false;
		}

		// Token: 0x04000A0F RID: 2575
		private static readonly int StateLoaded = BitVector32.CreateMask();

		// Token: 0x04000A10 RID: 2576
		private static readonly int StateLoadFailed = BitVector32.CreateMask(BasicDesignerLoader.StateLoaded);

		// Token: 0x04000A11 RID: 2577
		private static readonly int StateFlushInProgress = BitVector32.CreateMask(BasicDesignerLoader.StateLoadFailed);

		// Token: 0x04000A12 RID: 2578
		private static readonly int StateModified = BitVector32.CreateMask(BasicDesignerLoader.StateFlushInProgress);

		// Token: 0x04000A13 RID: 2579
		private static readonly int StateReloadSupported = BitVector32.CreateMask(BasicDesignerLoader.StateModified);

		// Token: 0x04000A14 RID: 2580
		private static readonly int StateActiveDocument = BitVector32.CreateMask(BasicDesignerLoader.StateReloadSupported);

		// Token: 0x04000A15 RID: 2581
		private static readonly int StateDeferredReload = BitVector32.CreateMask(BasicDesignerLoader.StateActiveDocument);

		// Token: 0x04000A16 RID: 2582
		private static readonly int StateReloadAtIdle = BitVector32.CreateMask(BasicDesignerLoader.StateDeferredReload);

		// Token: 0x04000A17 RID: 2583
		private static readonly int StateForceReload = BitVector32.CreateMask(BasicDesignerLoader.StateReloadAtIdle);

		// Token: 0x04000A18 RID: 2584
		private static readonly int StateFlushReload = BitVector32.CreateMask(BasicDesignerLoader.StateForceReload);

		// Token: 0x04000A19 RID: 2585
		private static readonly int StateModifyIfErrors = BitVector32.CreateMask(BasicDesignerLoader.StateFlushReload);

		// Token: 0x04000A1A RID: 2586
		private static readonly int StateEnableComponentEvents = BitVector32.CreateMask(BasicDesignerLoader.StateModifyIfErrors);

		// Token: 0x04000A1B RID: 2587
		private BitVector32 _state;

		// Token: 0x04000A1C RID: 2588
		private IDesignerLoaderHost _host;

		// Token: 0x04000A1D RID: 2589
		private int _loadDependencyCount;

		// Token: 0x04000A1E RID: 2590
		private string _baseComponentClassName;

		// Token: 0x04000A1F RID: 2591
		private bool _hostInitialized;

		// Token: 0x04000A20 RID: 2592
		private bool _loading;

		// Token: 0x04000A21 RID: 2593
		private DesignerSerializationManager _serializationManager;

		// Token: 0x04000A22 RID: 2594
		private IDisposable _serializationSession;

		// Token: 0x020004AF RID: 1199
		[Flags]
		protected enum ReloadOptions
		{
			// Token: 0x04001E6E RID: 7790
			Default = 0,
			// Token: 0x04001E6F RID: 7791
			ModifyOnError = 1,
			// Token: 0x04001E70 RID: 7792
			Force = 2,
			// Token: 0x04001E71 RID: 7793
			NoFlush = 4
		}
	}
}
