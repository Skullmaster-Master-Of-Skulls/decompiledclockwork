using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006B3 RID: 1715
	internal class ServerAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x06003DF0 RID: 15856 RVA: 0x000D3DF0 File Offset: 0x000D2DF0
		internal ServerAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000D3E00 File Offset: 0x000D2E00
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid;
			RemotingServices.CORProfilerRemotingServerSendingReply(out guid, true);
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				replyMsg.Properties["CORProfilerCookie"] = guid;
			}
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000D3E3E File Offset: 0x000D2E3E
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000D3E41 File Offset: 0x000D2E41
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04001F99 RID: 8089
		internal IMessageSink _nextSink;
	}
}
