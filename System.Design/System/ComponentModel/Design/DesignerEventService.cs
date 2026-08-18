using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x02000551 RID: 1361
	internal sealed class DesignerEventService : IDesignerEventService
	{
		// Token: 0x06002FA6 RID: 12198 RVA: 0x0010FC71 File Offset: 0x0010EC71
		internal DesignerEventService()
		{
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x0010FC7C File Offset: 0x0010EC7C
		internal void OnActivateDesigner(DesignSurface surface)
		{
			IDesignerHost designerHost = null;
			if (surface != null)
			{
				designerHost = (surface.GetService(typeof(IDesignerHost)) as IDesignerHost);
			}
			if (designerHost != null && (this._designerList == null || !this._designerList.Contains(designerHost)))
			{
				this.OnCreateDesigner(surface);
			}
			if (this._activeDesigner != designerHost)
			{
				IDesignerHost activeDesigner = this._activeDesigner;
				this._activeDesigner = designerHost;
				if (activeDesigner != null)
				{
					this.SinkChangeEvents(activeDesigner, false);
				}
				if (this._activeDesigner != null)
				{
					this.SinkChangeEvents(this._activeDesigner, true);
				}
				if (this._events != null)
				{
					ActiveDesignerEventHandler activeDesignerEventHandler = this._events[DesignerEventService.EventActiveDesignerChanged] as ActiveDesignerEventHandler;
					if (activeDesignerEventHandler != null)
					{
						activeDesignerEventHandler(this, new ActiveDesignerEventArgs(activeDesigner, designerHost));
					}
				}
				this.OnSelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0010FD38 File Offset: 0x0010ED38
		private void OnComponentAddedRemoved(object sender, ComponentEventArgs ce)
		{
			IComponent component = ce.Component;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null)
				{
					IDesignerHost designerHost = site.Container as IDesignerHost;
					if (designerHost != null && designerHost.Loading)
					{
						this._deferredSelChange = true;
						return;
					}
				}
			}
			this.OnSelectionChanged(this, EventArgs.Empty);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x0010FD84 File Offset: 0x0010ED84
		private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
		{
			IComponent component = ce.Component as IComponent;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null)
				{
					ISelectionService selectionService = site.GetService(typeof(ISelectionService)) as ISelectionService;
					if (selectionService != null && selectionService.GetComponentSelected(component))
					{
						this.OnSelectionChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x0010FDD8 File Offset: 0x0010EDD8
		internal void OnCreateDesigner(DesignSurface surface)
		{
			IDesignerHost designerHost = surface.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (this._designerList == null)
			{
				this._designerList = new ArrayList();
			}
			this._designerList.Add(designerHost);
			surface.Disposed += this.OnDesignerDisposed;
			if (this._events != null)
			{
				DesignerEventHandler designerEventHandler = this._events[DesignerEventService.EventDesignerCreated] as DesignerEventHandler;
				if (designerEventHandler != null)
				{
					designerEventHandler(this, new DesignerEventArgs(designerHost));
				}
			}
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x0010FE5C File Offset: 0x0010EE5C
		private void OnDesignerDisposed(object sender, EventArgs e)
		{
			DesignSurface designSurface = (DesignSurface)sender;
			designSurface.Disposed -= this.OnDesignerDisposed;
			this.SinkChangeEvents(designSurface, false);
			IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				if (this._events != null)
				{
					DesignerEventHandler designerEventHandler = this._events[DesignerEventService.EventDesignerDisposed] as DesignerEventHandler;
					if (designerEventHandler != null)
					{
						designerEventHandler(this, new DesignerEventArgs(designerHost));
					}
				}
				if (this._designerList != null)
				{
					this._designerList.Remove(designerHost);
				}
			}
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x0010FEE8 File Offset: 0x0010EEE8
		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (this._inTransaction)
			{
				this._deferredSelChange = true;
				return;
			}
			if (this._events != null)
			{
				EventHandler eventHandler = this._events[DesignerEventService.EventSelectionChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x0010FF2E File Offset: 0x0010EF2E
		private void OnLoadComplete(object sender, EventArgs e)
		{
			if (this._deferredSelChange)
			{
				this._deferredSelChange = false;
				this.OnSelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x0010FF4B File Offset: 0x0010EF4B
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				this._inTransaction = false;
				if (this._deferredSelChange)
				{
					this._deferredSelChange = false;
					this.OnSelectionChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x0010FF77 File Offset: 0x0010EF77
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this._inTransaction = true;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x0010FF80 File Offset: 0x0010EF80
		private void SinkChangeEvents(IServiceProvider provider, bool sink)
		{
			ISelectionService selectionService = provider.GetService(typeof(ISelectionService)) as ISelectionService;
			IComponentChangeService componentChangeService = provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			IDesignerHost designerHost = provider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (sink)
			{
				if (selectionService != null)
				{
					selectionService.SelectionChanged += this.OnSelectionChanged;
				}
				if (componentChangeService != null)
				{
					ComponentEventHandler value = new ComponentEventHandler(this.OnComponentAddedRemoved);
					componentChangeService.ComponentAdded += value;
					componentChangeService.ComponentRemoved += value;
					componentChangeService.ComponentChanged += this.OnComponentChanged;
				}
				if (designerHost != null)
				{
					designerHost.TransactionOpened += this.OnTransactionOpened;
					designerHost.TransactionClosed += this.OnTransactionClosed;
					designerHost.LoadComplete += this.OnLoadComplete;
					if (designerHost.InTransaction)
					{
						this.OnTransactionOpened(designerHost, EventArgs.Empty);
						return;
					}
				}
			}
			else
			{
				if (selectionService != null)
				{
					selectionService.SelectionChanged -= this.OnSelectionChanged;
				}
				if (componentChangeService != null)
				{
					ComponentEventHandler value2 = new ComponentEventHandler(this.OnComponentAddedRemoved);
					componentChangeService.ComponentAdded -= value2;
					componentChangeService.ComponentRemoved -= value2;
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
				}
				if (designerHost != null)
				{
					designerHost.TransactionOpened -= this.OnTransactionOpened;
					designerHost.TransactionClosed -= this.OnTransactionClosed;
					designerHost.LoadComplete -= this.OnLoadComplete;
					if (designerHost.InTransaction)
					{
						this.OnTransactionClosed(designerHost, new DesignerTransactionCloseEventArgs(false, true));
					}
				}
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x00110105 File Offset: 0x0010F105
		IDesignerHost IDesignerEventService.ActiveDesigner
		{
			get
			{
				return this._activeDesigner;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0011010D File Offset: 0x0010F10D
		DesignerCollection IDesignerEventService.Designers
		{
			get
			{
				if (this._designerList == null)
				{
					this._designerList = new ArrayList();
				}
				if (this._designerCollection == null)
				{
					this._designerCollection = new DesignerCollection(this._designerList);
				}
				return this._designerCollection;
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06002FB3 RID: 12211 RVA: 0x00110141 File Offset: 0x0010F141
		// (remove) Token: 0x06002FB4 RID: 12212 RVA: 0x0011017C File Offset: 0x0010F17C
		event ActiveDesignerEventHandler IDesignerEventService.ActiveDesignerChanged
		{
			add
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				this._events[DesignerEventService.EventActiveDesignerChanged] = Delegate.Combine(this._events[DesignerEventService.EventActiveDesignerChanged], value);
			}
			remove
			{
				if (this._events != null)
				{
					this._events[DesignerEventService.EventActiveDesignerChanged] = Delegate.Remove(this._events[DesignerEventService.EventActiveDesignerChanged], value);
				}
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06002FB5 RID: 12213 RVA: 0x001101AC File Offset: 0x0010F1AC
		// (remove) Token: 0x06002FB6 RID: 12214 RVA: 0x001101E7 File Offset: 0x0010F1E7
		event DesignerEventHandler IDesignerEventService.DesignerCreated
		{
			add
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				this._events[DesignerEventService.EventDesignerCreated] = Delegate.Combine(this._events[DesignerEventService.EventDesignerCreated], value);
			}
			remove
			{
				if (this._events != null)
				{
					this._events[DesignerEventService.EventDesignerCreated] = Delegate.Remove(this._events[DesignerEventService.EventDesignerCreated], value);
				}
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002FB7 RID: 12215 RVA: 0x00110217 File Offset: 0x0010F217
		// (remove) Token: 0x06002FB8 RID: 12216 RVA: 0x00110252 File Offset: 0x0010F252
		event DesignerEventHandler IDesignerEventService.DesignerDisposed
		{
			add
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				this._events[DesignerEventService.EventDesignerDisposed] = Delegate.Combine(this._events[DesignerEventService.EventDesignerDisposed], value);
			}
			remove
			{
				if (this._events != null)
				{
					this._events[DesignerEventService.EventDesignerDisposed] = Delegate.Remove(this._events[DesignerEventService.EventDesignerDisposed], value);
				}
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002FB9 RID: 12217 RVA: 0x00110282 File Offset: 0x0010F282
		// (remove) Token: 0x06002FBA RID: 12218 RVA: 0x001102BD File Offset: 0x0010F2BD
		event EventHandler IDesignerEventService.SelectionChanged
		{
			add
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				this._events[DesignerEventService.EventSelectionChanged] = Delegate.Combine(this._events[DesignerEventService.EventSelectionChanged], value);
			}
			remove
			{
				if (this._events != null)
				{
					this._events[DesignerEventService.EventSelectionChanged] = Delegate.Remove(this._events[DesignerEventService.EventSelectionChanged], value);
				}
			}
		}

		// Token: 0x04002059 RID: 8281
		private static readonly object EventActiveDesignerChanged = new object();

		// Token: 0x0400205A RID: 8282
		private static readonly object EventDesignerCreated = new object();

		// Token: 0x0400205B RID: 8283
		private static readonly object EventDesignerDisposed = new object();

		// Token: 0x0400205C RID: 8284
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x0400205D RID: 8285
		private ArrayList _designerList;

		// Token: 0x0400205E RID: 8286
		private DesignerCollection _designerCollection;

		// Token: 0x0400205F RID: 8287
		private IDesignerHost _activeDesigner;

		// Token: 0x04002060 RID: 8288
		private EventHandlerList _events;

		// Token: 0x04002061 RID: 8289
		private bool _inTransaction;

		// Token: 0x04002062 RID: 8290
		private bool _deferredSelChange;
	}
}
