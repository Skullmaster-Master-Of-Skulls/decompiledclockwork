using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000551 RID: 1361
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class EventHandlerList : IDisposable
	{
		// Token: 0x0600333B RID: 13115 RVA: 0x000E3B99 File Offset: 0x000E1D99
		public EventHandlerList()
		{
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000E3BA1 File Offset: 0x000E1DA1
		internal EventHandlerList(Component parent)
		{
			this.parent = parent;
		}

		// Token: 0x17000C88 RID: 3208
		public Delegate this[object key]
		{
			get
			{
				EventHandlerList.ListEntry listEntry = null;
				if (this.parent == null || this.parent.CanRaiseEventsInternal)
				{
					listEntry = this.Find(key);
				}
				if (listEntry != null)
				{
					return listEntry.handler;
				}
				return null;
			}
			set
			{
				EventHandlerList.ListEntry listEntry = this.Find(key);
				if (listEntry != null)
				{
					listEntry.handler = value;
					return;
				}
				this.head = new EventHandlerList.ListEntry(key, value, this.head);
			}
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000E3C1C File Offset: 0x000E1E1C
		public void AddHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Combine(listEntry.handler, value);
				return;
			}
			this.head = new EventHandlerList.ListEntry(key, value, this.head);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000E3C5C File Offset: 0x000E1E5C
		public void AddHandlers(EventHandlerList listToAddFrom)
		{
			for (EventHandlerList.ListEntry next = listToAddFrom.head; next != null; next = next.next)
			{
				this.AddHandler(next.key, next.handler);
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000E3C8E File Offset: 0x000E1E8E
		public void Dispose()
		{
			this.head = null;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000E3C98 File Offset: 0x000E1E98
		private EventHandlerList.ListEntry Find(object key)
		{
			EventHandlerList.ListEntry next = this.head;
			while (next != null && next.key != key)
			{
				next = next.next;
			}
			return next;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000E3CC4 File Offset: 0x000E1EC4
		public void RemoveHandler(object key, Delegate value)
		{
			EventHandlerList.ListEntry listEntry = this.Find(key);
			if (listEntry != null)
			{
				listEntry.handler = Delegate.Remove(listEntry.handler, value);
			}
		}

		// Token: 0x040029B9 RID: 10681
		private EventHandlerList.ListEntry head;

		// Token: 0x040029BA RID: 10682
		private Component parent;

		// Token: 0x02000894 RID: 2196
		private sealed class ListEntry
		{
			// Token: 0x0600459E RID: 17822 RVA: 0x001233F5 File Offset: 0x001215F5
			public ListEntry(object key, Delegate handler, EventHandlerList.ListEntry next)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

			// Token: 0x040037CD RID: 14285
			internal EventHandlerList.ListEntry next;

			// Token: 0x040037CE RID: 14286
			internal object key;

			// Token: 0x040037CF RID: 14287
			internal Delegate handler;
		}
	}
}
