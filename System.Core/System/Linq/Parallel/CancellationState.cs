using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001EA RID: 490
	internal class CancellationState
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00037FEA File Offset: 0x000361EA
		internal CancellationToken MergedCancellationToken
		{
			get
			{
				if (this.MergedCancellationTokenSource != null)
				{
					return this.MergedCancellationTokenSource.Token;
				}
				return new CancellationToken(false);
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00038006 File Offset: 0x00036206
		internal CancellationState(CancellationToken externalCancellationToken)
		{
			this.ExternalCancellationToken = externalCancellationToken;
			this.TopLevelDisposedFlag = new Shared<bool>(false);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00038021 File Offset: 0x00036221
		internal static void ThrowIfCanceled(CancellationToken token)
		{
			if (token.IsCancellationRequested)
			{
				throw new OperationCanceledException(token);
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00038034 File Offset: 0x00036234
		internal static void ThrowWithStandardMessageIfCanceled(CancellationToken externalCancellationToken)
		{
			if (externalCancellationToken.IsCancellationRequested)
			{
				string @string = SR.GetString("PLINQ_ExternalCancellationRequested");
				throw new OperationCanceledException(@string, externalCancellationToken);
			}
		}

		// Token: 0x04000900 RID: 2304
		internal CancellationTokenSource InternalCancellationTokenSource;

		// Token: 0x04000901 RID: 2305
		internal CancellationToken ExternalCancellationToken;

		// Token: 0x04000902 RID: 2306
		internal CancellationTokenSource MergedCancellationTokenSource;

		// Token: 0x04000903 RID: 2307
		internal Shared<bool> TopLevelDisposedFlag;

		// Token: 0x04000904 RID: 2308
		internal const int POLL_INTERVAL = 63;
	}
}
