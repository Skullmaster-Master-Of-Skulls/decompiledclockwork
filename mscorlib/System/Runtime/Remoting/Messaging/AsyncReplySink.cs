using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A2 RID: 1954
	internal class AsyncReplySink : IMessageSink
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x000ECC56 File Offset: 0x000EBC56
		internal AsyncReplySink(IMessageSink replySink, Context cliCtx)
		{
			this._replySink = replySink;
			this._cliCtx = cliCtx;
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x000ECC6C File Offset: 0x000EBC6C
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage msg = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Thread.CurrentContext.NotifyDynamicSinks(msg, true, false, true, true);
			return messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000ECCA4 File Offset: 0x000EBCA4
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage result = null;
			if (this._replySink != null)
			{
				object[] args = new object[]
				{
					reqMsg,
					this._replySink
				};
				InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(AsyncReplySink.SyncProcessMessageCallback);
				result = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._cliCtx, ftnToCall, args);
			}
			return result;
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x000ECCF7 File Offset: 0x000EBCF7
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000ECCFE File Offset: 0x000EBCFE
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x0400229A RID: 8858
		private IMessageSink _replySink;

		// Token: 0x0400229B RID: 8859
		private Context _cliCtx;
	}
}
