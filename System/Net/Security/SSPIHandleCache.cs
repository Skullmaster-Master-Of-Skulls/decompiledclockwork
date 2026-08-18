using System;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x0200053F RID: 1343
	internal static class SSPIHandleCache
	{
		// Token: 0x06002908 RID: 10504 RVA: 0x000AA910 File Offset: 0x000A9910
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

		// Token: 0x040027DC RID: 10204
		private const int c_MaxCacheSize = 31;

		// Token: 0x040027DD RID: 10205
		private static SafeCredentialReference[] _CacheSlots = new SafeCredentialReference[32];

		// Token: 0x040027DE RID: 10206
		private static int _Current = -1;
	}
}
