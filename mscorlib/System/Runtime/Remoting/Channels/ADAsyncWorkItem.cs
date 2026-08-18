using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006D2 RID: 1746
	internal class ADAsyncWorkItem
	{
		// Token: 0x06003EF8 RID: 16120 RVA: 0x000D7DCC File Offset: 0x000D6DCC
		internal ADAsyncWorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._callCtx = CallContext.GetLogicalCallContext();
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000D7DF4 File Offset: 0x000D6DF4
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(this._callCtx);
			IMessage msg = this._nextSink.SyncProcessMessage(this._reqMsg);
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
			CallContext.SetLogicalCallContext(logicalCallContext);
		}

		// Token: 0x04002001 RID: 8193
		private IMessageSink _replySink;

		// Token: 0x04002002 RID: 8194
		private IMessageSink _nextSink;

		// Token: 0x04002003 RID: 8195
		private LogicalCallContext _callCtx;

		// Token: 0x04002004 RID: 8196
		private IMessage _reqMsg;
	}
}
