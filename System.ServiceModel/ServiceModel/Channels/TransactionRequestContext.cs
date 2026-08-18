using System;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A69 RID: 2665
	internal sealed class TransactionRequestContext : RequestContextBase
	{
		// Token: 0x0600692A RID: 26922 RVA: 0x00188C8E File Offset: 0x00186E8E
		public TransactionRequestContext(ITransactionChannel transactionChannel, ChannelBase channel, RequestContext innerContext, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(innerContext.RequestMessage, defaultCloseTimeout, defaultSendTimeout)
		{
			this.transactionChannel = transactionChannel;
			this.innerContext = innerContext;
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x00188CAE File Offset: 0x00186EAE
		protected override void OnAbort()
		{
			if (this.innerContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
			this.innerContext.Abort();
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x00188CE0 File Offset: 0x00186EE0
		protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.innerContext == null)
			{
				throw TraceUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName), message);
			}
			if (message != null)
			{
				this.transactionChannel.WriteIssuedTokens(message, MessageDirection.Output);
			}
			return this.innerContext.BeginReply(message, timeout, callback, state);
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x00188D2C File Offset: 0x00186F2C
		protected override void OnClose(TimeSpan timeout)
		{
			if (this.innerContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
			this.innerContext.Close(timeout);
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00188D5D File Offset: 0x00186F5D
		protected override void OnEndReply(IAsyncResult result)
		{
			if (this.innerContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
			this.innerContext.EndReply(result);
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x00188D8E File Offset: 0x00186F8E
		protected override void OnReply(Message message, TimeSpan timeout)
		{
			if (this.innerContext == null)
			{
				throw TraceUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName), message);
			}
			if (message != null)
			{
				this.transactionChannel.WriteIssuedTokens(message, MessageDirection.Output);
			}
			this.innerContext.Reply(message, timeout);
		}

		// Token: 0x04003C2D RID: 15405
		private ITransactionChannel transactionChannel;

		// Token: 0x04003C2E RID: 15406
		private RequestContext innerContext;
	}
}
