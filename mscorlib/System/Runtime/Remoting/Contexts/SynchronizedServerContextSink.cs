using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200079B RID: 1947
	internal class SynchronizedServerContextSink : InternalSink, IMessageSink
	{
		// Token: 0x0600456C RID: 17772 RVA: 0x000EC478 File Offset: 0x000EB478
		internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000EC490 File Offset: 0x000EB490
		~SynchronizedServerContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000EC4C4 File Offset: 0x000EB4C4
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
			this._property.HandleWorkRequest(workItem);
			return workItem.ReplyMessage;
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x000EC4F4 File Offset: 0x000EB4F4
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, replySink);
			workItem.SetAsync();
			this._property.HandleWorkRequest(workItem);
			return null;
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x000EC522 File Offset: 0x000EB522
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x0400227F RID: 8831
		internal IMessageSink _nextSink;

		// Token: 0x04002280 RID: 8832
		internal SynchronizationAttribute _property;
	}
}
