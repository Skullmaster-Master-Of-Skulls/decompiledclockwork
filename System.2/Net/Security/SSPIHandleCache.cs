using System;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000353 RID: 851
	internal static class SSPIHandleCache
	{
		// Token: 0x06001E92 RID: 7826 RVA: 0x0008FE84 File Offset: 0x0008E084
		internal static void CacheCredential(SafeFreeCredentials newHandle)
		{
			try
			{
				SafeCredentialReference safeCredentialReference = SafeCredentialReference.CreateReference(newHandle);
				if (safeCredentialReference != null)
				{
					int num = Interlocked.Increment(ref SSPIHandleCache._Current) & 31;
					safeCredentialReference = Interlocked.Exchange<SafeCredentialReference>(ref SSPIHandleCache._CacheSlots[num], safeCredentialReference);
					if (safeCredentialReference != null)
					{
						safeCredentialReference.Close();
					}
				}
			}
			catch (Exception exception)
			{
				NclUtilities.IsFatal(exception);
			}
		}

		// Token: 0x04001CEE RID: 7406
		private const int c_MaxCacheSize = 31;

		// Token: 0x04001CEF RID: 7407
		private static SafeCredentialReference[] _CacheSlots = new SafeCredentialReference[32];

		// Token: 0x04001CF0 RID: 7408
		private static int _Current = -1;
	}
}
