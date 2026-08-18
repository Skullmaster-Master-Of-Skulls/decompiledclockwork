using System;
using System.IO;
using System.Runtime;
using System.Security.Authentication;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082E RID: 2094
	internal abstract class StreamSecurityUpgradeAcceptorAsyncResult : TraceAsyncResult
	{
		// Token: 0x06004E44 RID: 20036 RVA: 0x0011DE82 File Offset: 0x0011C082
		protected StreamSecurityUpgradeAcceptorAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0011DE8C File Offset: 0x0011C08C
		public void Begin(Stream stream)
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.OnBegin(stream, StreamSecurityUpgradeAcceptorAsyncResult.onAuthenticateAsServer);
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
			this.CompleteAuthenticateAsServer(asyncResult);
			base.Complete(true);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x0011DF20 File Offset: 0x0011C120
		private void CompleteAuthenticateAsServer(IAsyncResult result)
		{
			try
			{
				this.upgradedStream = this.OnCompleteAuthenticateAsServer(result);
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

		// Token: 0x06004E47 RID: 20039 RVA: 0x0011DFA8 File Offset: 0x0011C1A8
		public static Stream End(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
		{
			StreamSecurityUpgradeAcceptorAsyncResult streamSecurityUpgradeAcceptorAsyncResult = AsyncResult.End<StreamSecurityUpgradeAcceptorAsyncResult>(result);
			remoteSecurity = streamSecurityUpgradeAcceptorAsyncResult.remoteSecurity;
			return streamSecurityUpgradeAcceptorAsyncResult.upgradedStream;
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x0011DFCC File Offset: 0x0011C1CC
		private static void OnAuthenticateAsServer(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			StreamSecurityUpgradeAcceptorAsyncResult streamSecurityUpgradeAcceptorAsyncResult = (StreamSecurityUpgradeAcceptorAsyncResult)result.AsyncState;
			Exception exception = null;
			try
			{
				streamSecurityUpgradeAcceptorAsyncResult.CompleteAuthenticateAsServer(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			streamSecurityUpgradeAcceptorAsyncResult.Complete(false, exception);
		}

		// Token: 0x06004E49 RID: 20041
		protected abstract IAsyncResult OnBegin(Stream stream, AsyncCallback callback);

		// Token: 0x06004E4A RID: 20042
		protected abstract Stream OnCompleteAuthenticateAsServer(IAsyncResult result);

		// Token: 0x06004E4B RID: 20043
		protected abstract SecurityMessageProperty ValidateCreateSecurity();

		// Token: 0x040030DB RID: 12507
		private SecurityMessageProperty remoteSecurity;

		// Token: 0x040030DC RID: 12508
		private Stream upgradedStream;

		// Token: 0x040030DD RID: 12509
		private static AsyncCallback onAuthenticateAsServer = Fx.ThunkCallback(new AsyncCallback(StreamSecurityUpgradeAcceptorAsyncResult.OnAuthenticateAsServer));
	}
}
