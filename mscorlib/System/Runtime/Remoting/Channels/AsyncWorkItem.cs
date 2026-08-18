using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006CB RID: 1739
	internal class AsyncWorkItem : IMessageSink
	{
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000D75BA File Offset: 0x000D65BA
		internal AsyncWorkItem(IMessageSink replySink, Context oldCtx) : this(null, replySink, oldCtx, null)
		{
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000D75C6 File Offset: 0x000D65C6
		internal AsyncWorkItem(IMessage reqMsg, IMessageSink replySink, Context oldCtx, ServerIdentity srvID)
		{
			this._reqMsg = reqMsg;
			this._replySink = replySink;
			this._oldCtx = oldCtx;
			this._callCtx = CallContext.GetLogicalCallContext();
			this._srvID = srvID;
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000D75F8 File Offset: 0x000D65F8
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessageSink messageSink = (IMessageSink)args[0];
			IMessage msg = (IMessage)args[1];
			return messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000D7620 File Offset: 0x000D6620
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage result = null;
			if (this._replySink != null)
			{
				Thread.CurrentContext.NotifyDynamicSinks(msg, false, false, true, true);
				object[] args = new object[]
				{
					this._replySink,
					msg
				};
				InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncWorkItem.SyncProcessMessageCallback);
				result = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._oldCtx, ftnToCall, args);
			}
			return result;
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000D7683 File Offset: 0x000D6683
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000D7694 File Offset: 0x000D6694
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000D769C File Offset: 0x000D669C
		internal static object FinishAsyncWorkCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = (AsyncWorkItem)args[0];
			Context serverContext = asyncWorkItem._srvID.ServerContext;
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(asyncWorkItem._callCtx);
			serverContext.NotifyDynamicSinks(asyncWorkItem._reqMsg, false, true, true, true);
			serverContext.GetServerContextChain().AsyncProcessMessage(asyncWorkItem._reqMsg, asyncWorkItem);
			CallContext.SetLogicalCallContext(logicalCallContext);
			return null;
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000D76F8 File Offset: 0x000D66F8
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncWorkItem.FinishAsyncWorkCallback);
			object[] args = new object[]
			{
				this
			};
			Thread.CurrentThread.InternalCrossContextCallback(this._srvID.ServerContext, ftnToCall, args);
		}

		// Token: 0x04001FEE RID: 8174
		private IMessageSink _replySink;

		// Token: 0x04001FEF RID: 8175
		private ServerIdentity _srvID;

		// Token: 0x04001FF0 RID: 8176
		private Context _oldCtx;

		// Token: 0x04001FF1 RID: 8177
		private LogicalCallContext _callCtx;

		// Token: 0x04001FF2 RID: 8178
		private IMessage _reqMsg;
	}
}
