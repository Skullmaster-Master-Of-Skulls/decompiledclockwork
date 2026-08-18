using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C2 RID: 450
	internal sealed class DesignerEventService : IDesignerEventService
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0000362F File Offset: 0x0000182F
		internal DesignerEventService()
		{
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0005B810 File Offset: 0x00059A10
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

		// Token: 0x0600103E RID: 4158 RVA: 0x0005B8CC File Offset: 0x00059ACC
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

		// Token: 0x0600103F RID: 4159 RVA: 0x0005B918 File Offset: 0x00059B18
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

		// Token: 0x06001040 RID: 4160 RVA: 0x0005B96C File Offset: 0x00059B6C
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

		// Token: 0x06001041 RID: 4161 RVA: 0x0005B9F0 File Offset: 0x00059BF0
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

		// Token: 0x06001042 RID: 4162 RVA: 0x0005BA7C File Offset: 0x00059C7C
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

		// Token: 0x06001043 RID: 4163 RVA: 0x0005BAC2 File Offset: 0x00059CC2
		private void OnLoadComplete(object sender, EventArgs e)
		{
			if (this._deferredSelChange)
			{
				this._deferredSelChange = false;
				this.OnSelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0005BADF File Offset: 0x00059CDF
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

		// Token: 0x06001045 RID: 4165 RVA: 0x0005BB0B File Offset: 0x00059D0B
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this._inTransaction = true;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0005BB14 File Offset: 0x00059D14
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0005BC99 File Offset: 0x00059E99
		IDesignerHost IDesignerEventService.ActiveDesigner
		{
			get
			{
				return this._activeDesigner;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0005BCA1 File Offset: 0x00059EA1
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001049 RID: 4169 RVA: 0x0005BCD5 File Offset: 0x00059ED5
		// (remove) Token: 0x0600104A RID: 4170 RVA: 0x0005BD10 File Offset: 0x00059F10
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

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600104B RID: 4171 RVA: 0x0005BD40 File Offset: 0x00059F40
		// (remove) Token: 0x0600104C RID: 4172 RVA: 0x0005BD7B File Offset: 0x00059F7B
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

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600104D RID: 4173 RVA: 0x0005BDAB File Offset: 0x00059FAB
		// (remove) Token: 0x0600104E RID: 4174 RVA: 0x0005BDE6 File Offset: 0x00059FE6
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

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600104F RID: 4175 RVA: 0x0005BE16 File Offset: 0x0005A016
		// (remove) Token: 0x06001050 RID: 4176 RVA: 0x0005BE51 File Offset: 0x0005A051
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

		// Token: 0x04000964 RID: 2404
		private static readonly object EventActiveDesignerChanged = new object();

		// Token: 0x04000965 RID: 2405
		private static readonly object EventDesignerCreated = new object();

		// Token: 0x04000966 RID: 2406
		private static readonly object EventDesignerDisposed = new object();

		// Token: 0x04000967 RID: 2407
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x04000968 RID: 2408
		private ArrayList _designerList;

		// Token: 0x04000969 RID: 2409
		private DesignerCollection _designerCollection;

		// Token: 0x0400096A RID: 2410
		private IDesignerHost _activeDesigner;

		// Token: 0x0400096B RID: 2411
		private EventHandlerList _events;

		// Token: 0x0400096C RID: 2412
		private bool _inTransaction;

		// Token: 0x0400096D RID: 2413
		private bool _deferredSelChange;
	}
}
