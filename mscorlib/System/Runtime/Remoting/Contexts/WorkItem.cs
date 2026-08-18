using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200079C RID: 1948
	internal class WorkItem
	{
		// Token: 0x06004571 RID: 17777 RVA: 0x000EC52A File Offset: 0x000EB52A
		internal WorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._replyMsg = null;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._ctx = Thread.CurrentContext;
			this._callCtx = CallContext.GetLogicalCallContext();
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000EC564 File Offset: 0x000EB564
		internal virtual void SetWaiting()
		{
			this._flags |= 1;
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x000EC574 File Offset: 0x000EB574
		internal virtual bool IsWaiting()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x000EC581 File Offset: 0x000EB581
		internal virtual void SetSignaled()
		{
			this._flags |= 2;
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x000EC591 File Offset: 0x000EB591
		internal virtual bool IsSignaled()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000EC59E File Offset: 0x000EB59E
		internal virtual void SetAsync()
		{
			this._flags |= 4;
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x000EC5AE File Offset: 0x000EB5AE
		internal virtual bool IsAsync()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000EC5BB File Offset: 0x000EB5BB
		internal virtual void SetDummy()
		{
			this._flags |= 8;
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000EC5CB File Offset: 0x000EB5CB
		internal virtual bool IsDummy()
		{
			return (this._flags & 8) == 8;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x000EC5D8 File Offset: 0x000EB5D8
		internal static object ExecuteCallback(object[] args)
		{
			WorkItem workItem = (WorkItem)args[0];
			if (workItem.IsAsync())
			{
				workItem._nextSink.AsyncProcessMessage(workItem._reqMsg, workItem._replySink);
			}
			else if (workItem._nextSink != null)
			{
				workItem._replyMsg = workItem._nextSink.SyncProcessMessage(workItem._reqMsg);
			}
			return null;
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x000EC630 File Offset: 0x000EB630
		internal virtual void Execute()
		{
			Thread.CurrentThread.InternalCrossContextCallback(this._ctx, WorkItem._xctxDel, new object[]
			{
				this
			});
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x000EC65F File Offset: 0x000EB65F
		internal virtual IMessage ReplyMessage
		{
			get
			{
				return this._replyMsg;
			}
		}

		// Token: 0x04002281 RID: 8833
		private const int FLG_WAITING = 1;

		// Token: 0x04002282 RID: 8834
		private const int FLG_SIGNALED = 2;

		// Token: 0x04002283 RID: 8835
		private const int FLG_ASYNC = 4;

		// Token: 0x04002284 RID: 8836
		private const int FLG_DUMMY = 8;

		// Token: 0x04002285 RID: 8837
		internal int _flags;

		// Token: 0x04002286 RID: 8838
		internal IMessage _reqMsg;

		// Token: 0x04002287 RID: 8839
		internal IMessageSink _nextSink;

		// Token: 0x04002288 RID: 8840
		internal IMessageSink _replySink;

		// Token: 0x04002289 RID: 8841
		internal IMessage _replyMsg;

		// Token: 0x0400228A RID: 8842
		internal Context _ctx;

		// Token: 0x0400228B RID: 8843
		internal LogicalCallContext _callCtx;

		// Token: 0x0400228C RID: 8844
		internal static InternalCrossContextDelegate _xctxDel = new InternalCrossContextDelegate(WorkItem.ExecuteCallback);
	}
}
