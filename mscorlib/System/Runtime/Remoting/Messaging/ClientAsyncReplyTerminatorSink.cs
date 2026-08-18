using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007A6 RID: 1958
	internal class ClientAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x000ECFCF File Offset: 0x000EBFCF
		internal ClientAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x000ECFE0 File Offset: 0x000EBFE0
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid id = Guid.Empty;
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				object obj = replyMsg.Properties["CORProfilerCookie"];
				if (obj != null)
				{
					id = (Guid)obj;
				}
			}
			RemotingServices.CORProfilerRemotingClientReceivingReply(id, true);
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x000ED028 File Offset: 0x000EC028
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x000ED02B File Offset: 0x000EC02B
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040022A1 RID: 8865
		internal IMessageSink _nextSink;
	}
}
