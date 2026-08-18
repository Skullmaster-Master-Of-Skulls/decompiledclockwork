using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x0200055D RID: 1373
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignSurfaceManager : IServiceProvider, IDisposable
	{
		// Token: 0x0600307F RID: 12415 RVA: 0x00112FF9 File Offset: 0x00111FF9
		public DesignSurfaceManager() : this(null)
		{
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x00113004 File Offset: 0x00112004
		public DesignSurfaceManager(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
			ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
			this.ServiceContainer.AddService(typeof(IDesignerEventService), callback);
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x00113044 File Offset: 0x00112044
		// (set) Token: 0x06003082 RID: 12418 RVA: 0x0011307C File Offset: 0x0011207C
		public virtual DesignSurface ActiveDesignSurface
		{
			get
			{
				IDesignerEventService eventService = this.EventService;
				if (eventService != null)
				{
					IDesignerHost activeDesigner = eventService.ActiveDesigner;
					if (activeDesigner != null)
					{
						return activeDesigner.GetService(typeof(DesignSurface)) as DesignSurface;
					}
				}
				return null;
			}
			set
			{
				DesignerEventService designerEventService = this.EventService as DesignerEventService;
				if (designerEventService != null)
				{
					designerEventService.OnActivateDesigner(value);
				}
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x001130A0 File Offset: 0x001120A0
		public DesignSurfaceCollection DesignSurfaces
		{
			get
			{
				IDesignerEventService eventService = this.EventService;
				if (eventService != null)
				{
					return new DesignSurfaceCollection(eventService.Designers);
				}
				return new DesignSurfaceCollection(null);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x001130C9 File Offset: 0x001120C9
		private IDesignerEventService EventService
		{
			get
			{
				return this.GetService(typeof(IDesignerEventService)) as IDesignerEventService;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x001130E0 File Offset: 0x001120E0
		protected ServiceContainer ServiceContainer
		{
			get
			{
				if (this._serviceContainer == null)
				{
					this._serviceContainer = new ServiceContainer(this._parentProvider);
				}
				return this._serviceContainer;
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06003086 RID: 12422 RVA: 0x00113104 File Offset: 0x00112104
		// (remove) Token: 0x06003087 RID: 12423 RVA: 0x0011314C File Offset: 0x0011214C
		public event ActiveDesignSurfaceChangedEventHandler ActiveDesignSurfaceChanged
		{
			add
			{
				if (this._activeDesignSurfaceChanged == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.ActiveDesignerChanged += this.OnActiveDesignerChanged;
					}
				}
				this._activeDesignSurfaceChanged = (ActiveDesignSurfaceChangedEventHandler)Delegate.Combine(this._activeDesignSurfaceChanged, value);
			}
			remove
			{
				this._activeDesignSurfaceChanged = (ActiveDesignSurfaceChangedEventHandler)Delegate.Remove(this._activeDesignSurfaceChanged, value);
				if (this._activeDesignSurfaceChanged == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.ActiveDesignerChanged -= this.OnActiveDesignerChanged;
					}
				}
			}
		}

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06003088 RID: 12424 RVA: 0x00113194 File Offset: 0x00112194
		// (remove) Token: 0x06003089 RID: 12425 RVA: 0x001131DC File Offset: 0x001121DC
		public event DesignSurfaceEventHandler DesignSurfaceCreated
		{
			add
			{
				if (this._designSurfaceCreated == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.DesignerCreated += this.OnDesignerCreated;
					}
				}
				this._designSurfaceCreated = (DesignSurfaceEventHandler)Delegate.Combine(this._designSurfaceCreated, value);
			}
			remove
			{
				this._designSurfaceCreated = (DesignSurfaceEventHandler)Delegate.Remove(this._designSurfaceCreated, value);
				if (this._designSurfaceCreated == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.DesignerCreated -= this.OnDesignerCreated;
					}
				}
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x0600308A RID: 12426 RVA: 0x00113224 File Offset: 0x00112224
		// (remove) Token: 0x0600308B RID: 12427 RVA: 0x0011326C File Offset: 0x0011226C
		public event DesignSurfaceEventHandler DesignSurfaceDisposed
		{
			add
			{
				if (this._designSurfaceDisposed == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.DesignerDisposed += this.OnDesignerDisposed;
					}
				}
				this._designSurfaceDisposed = (DesignSurfaceEventHandler)Delegate.Combine(this._designSurfaceDisposed, value);
			}
			remove
			{
				this._designSurfaceDisposed = (DesignSurfaceEventHandler)Delegate.Remove(this._designSurfaceDisposed, value);
				if (this._designSurfaceDisposed == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.DesignerDisposed -= this.OnDesignerDisposed;
					}
				}
			}
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x0600308C RID: 12428 RVA: 0x001132B4 File Offset: 0x001122B4
		// (remove) Token: 0x0600308D RID: 12429 RVA: 0x001132FC File Offset: 0x001122FC
		public event EventHandler SelectionChanged
		{
			add
			{
				if (this._selectionChanged == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.SelectionChanged += this.OnSelectionChanged;
					}
				}
				this._selectionChanged = (EventHandler)Delegate.Combine(this._selectionChanged, value);
			}
			remove
			{
				this._selectionChanged = (EventHandler)Delegate.Remove(this._selectionChanged, value);
				if (this._selectionChanged == null)
				{
					IDesignerEventService eventService = this.EventService;
					if (eventService != null)
					{
						eventService.SelectionChanged -= this.OnSelectionChanged;
					}
				}
			}
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00113344 File Offset: 0x00112344
		public DesignSurface CreateDesignSurface()
		{
			DesignSurface designSurface = this.CreateDesignSurfaceCore(this);
			DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				designerEventService.OnCreateDesigner(designSurface);
			}
			return designSurface;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0011337C File Offset: 0x0011237C
		public DesignSurface CreateDesignSurface(IServiceProvider parentProvider)
		{
			if (parentProvider == null)
			{
				throw new ArgumentNullException("parentProvider");
			}
			IServiceProvider parentProvider2 = new DesignSurfaceManager.MergedServiceProvider(parentProvider, this);
			DesignSurface designSurface = this.CreateDesignSurfaceCore(parentProvider2);
			DesignerEventService designerEventService = this.GetService(typeof(IDesignerEventService)) as DesignerEventService;
			if (designerEventService != null)
			{
				designerEventService.OnCreateDesigner(designSurface);
			}
			return designSurface;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x001133C8 File Offset: 0x001123C8
		protected virtual DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
			return new DesignSurface(parentProvider);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x001133D0 File Offset: 0x001123D0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x001133D9 File Offset: 0x001123D9
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._serviceContainer != null)
			{
				this._serviceContainer.Dispose();
				this._serviceContainer = null;
			}
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x001133F8 File Offset: 0x001123F8
		public object GetService(Type serviceType)
		{
			if (this._serviceContainer != null)
			{
				return this._serviceContainer.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x00113410 File Offset: 0x00112410
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(IDesignerEventService))
			{
				return new DesignerEventService();
			}
			return null;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x00113428 File Offset: 0x00112428
		private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs e)
		{
			if (this._activeDesignSurfaceChanged != null)
			{
				DesignSurface newSurface = null;
				DesignSurface oldSurface = null;
				if (e.OldDesigner != null)
				{
					oldSurface = (e.OldDesigner.GetService(typeof(DesignSurface)) as DesignSurface);
				}
				if (e.NewDesigner != null)
				{
					newSurface = (e.NewDesigner.GetService(typeof(DesignSurface)) as DesignSurface);
				}
				this._activeDesignSurfaceChanged(this, new ActiveDesignSurfaceChangedEventArgs(oldSurface, newSurface));
			}
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0011349C File Offset: 0x0011249C
		private void OnDesignerCreated(object sender, DesignerEventArgs e)
		{
			if (this._designSurfaceCreated != null)
			{
				DesignSurface designSurface = e.Designer.GetService(typeof(DesignSurface)) as DesignSurface;
				if (designSurface != null)
				{
					this._designSurfaceCreated(this, new DesignSurfaceEventArgs(designSurface));
				}
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x001134E4 File Offset: 0x001124E4
		private void OnDesignerDisposed(object sender, DesignerEventArgs e)
		{
			if (this._designSurfaceDisposed != null)
			{
				DesignSurface designSurface = e.Designer.GetService(typeof(DesignSurface)) as DesignSurface;
				if (designSurface != null)
				{
					this._designSurfaceDisposed(this, new DesignSurfaceEventArgs(designSurface));
				}
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x00113529 File Offset: 0x00112529
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this._selectionChanged != null)
			{
				this._selectionChanged(this, e);
			}
		}

		// Token: 0x040020A3 RID: 8355
		private IServiceProvider _parentProvider;

		// Token: 0x040020A4 RID: 8356
		private ServiceContainer _serviceContainer;

		// Token: 0x040020A5 RID: 8357
		private ActiveDesignSurfaceChangedEventHandler _activeDesignSurfaceChanged;

		// Token: 0x040020A6 RID: 8358
		private DesignSurfaceEventHandler _designSurfaceCreated;

		// Token: 0x040020A7 RID: 8359
		private DesignSurfaceEventHandler _designSurfaceDisposed;

		// Token: 0x040020A8 RID: 8360
		private EventHandler _selectionChanged;

		// Token: 0x0200055E RID: 1374
		private sealed class MergedServiceProvider : IServiceProvider
		{
			// Token: 0x06003099 RID: 12441 RVA: 0x00113540 File Offset: 0x00112540
			internal MergedServiceProvider(IServiceProvider primaryProvider, IServiceProvider secondaryProvider)
			{
				this._primaryProvider = primaryProvider;
				this._secondaryProvider = secondaryProvider;
			}

			// Token: 0x0600309A RID: 12442 RVA: 0x00113558 File Offset: 0x00112558
			object IServiceProvider.GetService(Type serviceType)
			{
				if (serviceType == null)
				{
					throw new ArgumentNullException("serviceType");
				}
				object service = this._primaryProvider.GetService(serviceType);
				if (service == null)
				{
					service = this._secondaryProvider.GetService(serviceType);
				}
				return service;
			}

			// Token: 0x040020A9 RID: 8361
			private IServiceProvider _primaryProvider;

			// Token: 0x040020AA RID: 8362
			private IServiceProvider _secondaryProvider;
		}
	}
}
