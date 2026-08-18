using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002DF RID: 735
	public sealed class EventHandlerService : IEventHandlerService
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x000B1EC3 File Offset: 0x000B00C3
		public EventHandlerService(Control focusWnd)
		{
			this.focusWnd = focusWnd;
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001D76 RID: 7542 RVA: 0x000B1ED2 File Offset: 0x000B00D2
		// (remove) Token: 0x06001D77 RID: 7543 RVA: 0x000B1EEB File Offset: 0x000B00EB
		public event EventHandler EventHandlerChanged
		{
			add
			{
				this.changedEvent = (EventHandler)Delegate.Combine(this.changedEvent, value);
			}
			remove
			{
				this.changedEvent = (EventHandler)Delegate.Remove(this.changedEvent, value);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000B1F04 File Offset: 0x000B0104
		public Control FocusWindow
		{
			get
			{
				return this.focusWnd;
			}
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000B1F0C File Offset: 0x000B010C
		public object GetHandler(Type handlerType)
		{
			if (handlerType == this.lastHandlerType)
			{
				return this.lastHandler;
			}
			for (EventHandlerService.HandlerEntry next = this.handlerHead; next != null; next = next.next)
			{
				if (next.handler != null && handlerType.IsInstanceOfType(next.handler))
				{
					this.lastHandlerType = handlerType;
					this.lastHandler = next.handler;
					return next.handler;
				}
			}
			return null;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x000B1F72 File Offset: 0x000B0172
		private void OnEventHandlerChanged(EventArgs e)
		{
			if (this.changedEvent != null)
			{
				this.changedEvent(this, e);
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x000B1F8C File Offset: 0x000B018C
		public void PopHandler(object handler)
		{
			for (EventHandlerService.HandlerEntry next = this.handlerHead; next != null; next = next.next)
			{
				if (next.handler == handler)
				{
					this.handlerHead = next.next;
					this.lastHandler = null;
					this.lastHandlerType = null;
					this.OnEventHandlerChanged(EventArgs.Empty);
					return;
				}
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000B1FDB File Offset: 0x000B01DB
		public void PushHandler(object handler)
		{
			this.handlerHead = new EventHandlerService.HandlerEntry(handler, this.handlerHead);
			this.lastHandlerType = handler.GetType();
			this.lastHandler = this.handlerHead.handler;
			this.OnEventHandlerChanged(EventArgs.Empty);
		}

		// Token: 0x04001771 RID: 6001
		private object lastHandler;

		// Token: 0x04001772 RID: 6002
		private Type lastHandlerType;

		// Token: 0x04001773 RID: 6003
		private EventHandlerService.HandlerEntry handlerHead;

		// Token: 0x04001774 RID: 6004
		private EventHandler changedEvent;

		// Token: 0x04001775 RID: 6005
		private readonly Control focusWnd;

		// Token: 0x0200056F RID: 1391
		private sealed class HandlerEntry
		{
			// Token: 0x060031E3 RID: 12771 RVA: 0x0010F7E3 File Offset: 0x0010D9E3
			public HandlerEntry(object handler, EventHandlerService.HandlerEntry next)
			{
				this.handler = handler;
				this.next = next;
			}

			// Token: 0x04002162 RID: 8546
			public object handler;

			// Token: 0x04002163 RID: 8547
			public EventHandlerService.HandlerEntry next;
		}
	}
}
