using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x0200070A RID: 1802
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06004012 RID: 16402 RVA: 0x000DA302 File Offset: 0x000D9302
		public LeaseSink(Lease lease, IMessageSink nextSink)
		{
			this.lease = lease;
			this.nextSink = nextSink;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000DA318 File Offset: 0x000D9318
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.lease.RenewOnCall();
			return this.nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000DA331 File Offset: 0x000D9331
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.lease.RenewOnCall();
			return this.nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000DA34B File Offset: 0x000D934B
		public IMessageSink NextSink
		{
			get
			{
				return this.nextSink;
			}
		}

		// Token: 0x04002056 RID: 8278
		private Lease lease;

		// Token: 0x04002057 RID: 8279
		private IMessageSink nextSink;
	}
}
