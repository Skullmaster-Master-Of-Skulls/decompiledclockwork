using System;
using System.IO;
using System.Runtime;
using System.Security.Authentication;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000831 RID: 2097
	internal abstract class StreamSecurityUpgradeInitiatorAsyncResult : AsyncResult
	{
		// Token: 0x06004E5A RID: 20058 RVA: 0x0011E137 File Offset: 0x0011C337
		public StreamSecurityUpgradeInitiatorAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x0011E144 File Offset: 0x0011C344
		public void Begin(Stream stream)
		{
			this.originalStream = stream;
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.OnBeginAuthenticateAsClient(this.originalStream, StreamSecurityUpgradeInitiatorAsyncResult.onAuthenticateAsClient);
			}
			catch (AuthenticationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
			}
			catch (IOException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NegotiationFailedIO", new object[]
				{
					ex2.Message
				}), ex2));
			}
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			this.CompleteAuthenticateAsClient(asyncResult);
			base.Complete(true);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x0011E1E4 File Offset: 0x0011C3E4
		private void CompleteAuthenticateAsClient(IAsyncResult result)
		{
			try
			{
				this.upgradedStream = this.OnCompleteAuthenticateAsClient(result);
			}
			catch (AuthenticationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
			}
			catch (IOException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NegotiationFailedIO", new object[]
				{
					ex2.Message
				}), ex2));
			}
			this.remoteSecurity = this.ValidateCreateSecurity();
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x0011E26C File Offset: 0x0011C46C
		public static Stream End(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
		{
			StreamSecurityUpgradeInitiatorAsyncResult streamSecurityUpgradeInitiatorAsyncResult = AsyncResult.End<StreamSecurityUpgradeInitiatorAsyncResult>(result);
			remoteSecurity = streamSecurityUpgradeInitiatorAsyncResult.remoteSecurity;
			return streamSecurityUpgradeInitiatorAsyncResult.upgradedStream;
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x0011E290 File Offset: 0x0011C490
		private static void OnAuthenticateAsClient(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			StreamSecurityUpgradeInitiatorAsyncResult streamSecurityUpgradeInitiatorAsyncResult = (StreamSecurityUpgradeInitiatorAsyncResult)result.AsyncState;
			Exception exception = null;
			try
			{
				streamSecurityUpgradeInitiatorAsyncResult.CompleteAuthenticateAsClient(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			streamSecurityUpgradeInitiatorAsyncResult.Complete(false, exception);
		}

		// Token: 0x06004E5F RID: 20063
		protected abstract IAsyncResult OnBeginAuthenticateAsClient(Stream stream, AsyncCallback callback);

		// Token: 0x06004E60 RID: 20064
		protected abstract Stream OnCompleteAuthenticateAsClient(IAsyncResult result);

		// Token: 0x06004E61 RID: 20065
		protected abstract SecurityMessageProperty ValidateCreateSecurity();

		// Token: 0x040030E2 RID: 12514
		private Stream originalStream;

		// Token: 0x040030E3 RID: 12515
		private SecurityMessageProperty remoteSecurity;

		// Token: 0x040030E4 RID: 12516
		private Stream upgradedStream;

		// Token: 0x040030E5 RID: 12517
		private static AsyncCallback onAuthenticateAsClient = Fx.ThunkCallback(new AsyncCallback(StreamSecurityUpgradeInitiatorAsyncResult.OnAuthenticateAsClient));
	}
}
