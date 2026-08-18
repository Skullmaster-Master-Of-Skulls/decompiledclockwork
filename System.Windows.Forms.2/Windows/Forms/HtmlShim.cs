using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000284 RID: 644
	internal abstract class HtmlShim : IDisposable
	{
		// Token: 0x0600292D RID: 10541 RVA: 0x000BCFC4 File Offset: 0x000BB1C4
		~HtmlShim()
		{
			this.Dispose(false);
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x000BCFF4 File Offset: 0x000BB1F4
		private EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x0600292F RID: 10543
		public abstract void AttachEventHandler(string eventName, EventHandler eventHandler);

		// Token: 0x06002930 RID: 10544 RVA: 0x000BD00F File Offset: 0x000BB20F
		public void AddHandler(object key, Delegate value)
		{
			this.eventCount++;
			this.Events.AddHandler(key, value);
			this.OnEventHandlerAdded();
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000BD034 File Offset: 0x000BB234
		protected HtmlToClrEventProxy AddEventProxy(string eventName, EventHandler eventHandler)
		{
			if (this.attachedEventList == null)
			{
				this.attachedEventList = new Dictionary<EventHandler, HtmlToClrEventProxy>();
			}
			HtmlToClrEventProxy htmlToClrEventProxy = new HtmlToClrEventProxy(this, eventName, eventHandler);
			this.attachedEventList[eventHandler] = htmlToClrEventProxy;
			return htmlToClrEventProxy;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002932 RID: 10546
		public abstract UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow { get; }

		// Token: 0x06002933 RID: 10547
		public abstract void ConnectToEvents();

		// Token: 0x06002934 RID: 10548
		public abstract void DetachEventHandler(string eventName, EventHandler eventHandler);

		// Token: 0x06002935 RID: 10549 RVA: 0x000BD06C File Offset: 0x000BB26C
		public virtual void DisconnectFromEvents()
		{
			if (this.attachedEventList != null)
			{
				EventHandler[] array = new EventHandler[this.attachedEventList.Count];
				this.attachedEventList.Keys.CopyTo(array, 0);
				foreach (EventHandler eventHandler in array)
				{
					HtmlToClrEventProxy htmlToClrEventProxy = this.attachedEventList[eventHandler];
					this.DetachEventHandler(htmlToClrEventProxy.EventName, eventHandler);
				}
			}
		}

		// Token: 0x06002936 RID: 10550
		protected abstract object GetEventSender();

		// Token: 0x06002937 RID: 10551 RVA: 0x000BD0D4 File Offset: 0x000BB2D4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000BD0E3 File Offset: 0x000BB2E3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisconnectFromEvents();
				if (this.events != null)
				{
					this.events.Dispose();
					this.events = null;
				}
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000BD108 File Offset: 0x000BB308
		public void FireEvent(object key, EventArgs e)
		{
			Delegate @delegate = this.Events[key];
			if (@delegate != null)
			{
				try
				{
					@delegate.DynamicInvoke(new object[]
					{
						this.GetEventSender(),
						e
					});
				}
				catch (Exception t)
				{
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						throw;
					}
					Application.OnThreadException(t);
				}
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000BD164 File Offset: 0x000BB364
		protected virtual void OnEventHandlerAdded()
		{
			this.ConnectToEvents();
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000BD16C File Offset: 0x000BB36C
		protected virtual void OnEventHandlerRemoved()
		{
			if (this.eventCount <= 0)
			{
				this.DisconnectFromEvents();
				this.eventCount = 0;
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000BD184 File Offset: 0x000BB384
		public void RemoveHandler(object key, Delegate value)
		{
			this.eventCount--;
			this.Events.RemoveHandler(key, value);
			this.OnEventHandlerRemoved();
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000BD1A8 File Offset: 0x000BB3A8
		protected HtmlToClrEventProxy RemoveEventProxy(EventHandler eventHandler)
		{
			if (this.attachedEventList == null)
			{
				return null;
			}
			if (this.attachedEventList.ContainsKey(eventHandler))
			{
				HtmlToClrEventProxy result = this.attachedEventList[eventHandler];
				this.attachedEventList.Remove(eventHandler);
				return result;
			}
			return null;
		}

		// Token: 0x040010DF RID: 4319
		private EventHandlerList events;

		// Token: 0x040010E0 RID: 4320
		private int eventCount;

		// Token: 0x040010E1 RID: 4321
		private Dictionary<EventHandler, HtmlToClrEventProxy> attachedEventList;
	}
}
