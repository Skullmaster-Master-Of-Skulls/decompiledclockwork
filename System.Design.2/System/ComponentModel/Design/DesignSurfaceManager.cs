using System;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C9 RID: 457
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignSurfaceManager : IServiceProvider, IDisposable
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x0005E727 File Offset: 0x0005C927
		public DesignSurfaceManager() : this(null)
		{
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0005E730 File Offset: 0x0005C930
		public DesignSurfaceManager(IServiceProvider parentProvider)
		{
			this._parentProvider = parentProvider;
			ServiceCreatorCallback callback = new ServiceCreatorCallback(this.OnCreateService);
			this.ServiceContainer.AddService(typeof(IDesignerEventService), callback);
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0005E770 File Offset: 0x0005C970
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0005E7CC File Offset: 0x0005C9CC
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0005E7F5 File Offset: 0x0005C9F5
		private IDesignerEventService EventService
		{
			get
			{
				return this.GetService(typeof(IDesignerEventService)) as IDesignerEventService;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0005E80C File Offset: 0x0005CA0C
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

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060010FF RID: 4351 RVA: 0x0005E830 File Offset: 0x0005CA30
		// (remove) Token: 0x06001100 RID: 4352 RVA: 0x0005E878 File Offset: 0x0005CA78
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

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001101 RID: 4353 RVA: 0x0005E8C0 File Offset: 0x0005CAC0
		// (remove) Token: 0x06001102 RID: 4354 RVA: 0x0005E908 File Offset: 0x0005CB08
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

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001103 RID: 4355 RVA: 0x0005E950 File Offset: 0x0005CB50
		// (remove) Token: 0x06001104 RID: 4356 RVA: 0x0005E998 File Offset: 0x0005CB98
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

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001105 RID: 4357 RVA: 0x0005E9E0 File Offset: 0x0005CBE0
		// (remove) Token: 0x06001106 RID: 4358 RVA: 0x0005EA28 File Offset: 0x0005CC28
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

		// Token: 0x06001107 RID: 4359 RVA: 0x0005EA70 File Offset: 0x0005CC70
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

		// Token: 0x06001108 RID: 4360 RVA: 0x0005EAA8 File Offset: 0x0005CCA8
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

		// Token: 0x06001109 RID: 4361 RVA: 0x0005EAF4 File Offset: 0x0005CCF4
		protected virtual DesignSurface CreateDesignSurfaceCore(IServiceProvider parentProvider)
		{
			return new DesignSurface(parentProvider);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0005EB05 File Offset: 0x0005CD05
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._serviceContainer != null)
			{
				this._serviceContainer.Dispose();
				this._serviceContainer = null;
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0005EB24 File Offset: 0x0005CD24
		public object GetService(Type serviceType)
		{
			if (this._serviceContainer != null)
			{
				return this._serviceContainer.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0005EB3C File Offset: 0x0005CD3C
		private object OnCreateService(IServiceContainer container, Type serviceType)
		{
			if (serviceType == typeof(IDesignerEventService))
			{
				return new DesignerEventService();
			}
			return null;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0005EB58 File Offset: 0x0005CD58
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

		// Token: 0x0600110F RID: 4367 RVA: 0x0005EBCC File Offset: 0x0005CDCC
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

		// Token: 0x06001110 RID: 4368 RVA: 0x0005EC14 File Offset: 0x0005CE14
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

		// Token: 0x06001111 RID: 4369 RVA: 0x0005EC59 File Offset: 0x0005CE59
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this._selectionChanged != null)
			{
				this._selectionChanged(this, e);
			}
		}

		// Token: 0x040009A4 RID: 2468
		private IServiceProvider _parentProvider;

		// Token: 0x040009A5 RID: 2469
		private ServiceContainer _serviceContainer;

		// Token: 0x040009A6 RID: 2470
		private ActiveDesignSurfaceChangedEventHandler _activeDesignSurfaceChanged;

		// Token: 0x040009A7 RID: 2471
		private DesignSurfaceEventHandler _designSurfaceCreated;

		// Token: 0x040009A8 RID: 2472
		private DesignSurfaceEventHandler _designSurfaceDisposed;

		// Token: 0x040009A9 RID: 2473
		private EventHandler _selectionChanged;

		// Token: 0x0200049C RID: 1180
		private sealed class MergedServiceProvider : IServiceProvider
		{
			// Token: 0x06002B76 RID: 11126 RVA: 0x00103ADC File Offset: 0x00101CDC
			internal MergedServiceProvider(IServiceProvider primaryProvider, IServiceProvider secondaryProvider)
			{
				this._primaryProvider = primaryProvider;
				this._secondaryProvider = secondaryProvider;
			}

			// Token: 0x06002B77 RID: 11127 RVA: 0x00103AF4 File Offset: 0x00101CF4
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

			// Token: 0x04001E29 RID: 7721
			private IServiceProvider _primaryProvider;

			// Token: 0x04001E2A RID: 7722
			private IServiceProvider _secondaryProvider;
		}
	}
}
