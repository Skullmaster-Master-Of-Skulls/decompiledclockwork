using System;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000E4 RID: 228
	internal sealed class BCryptAlgorithmHandleCache
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00016DAF File Offset: 0x00014FAF
		[SecurityCritical]
		public BCryptAlgorithmHandleCache()
		{
			this.m_algorithmHandles = new Dictionary<string, WeakReference>();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00016DC4 File Offset: 0x00014FC4
		[SecuritySafeCritical]
		public SafeBCryptAlgorithmHandle GetCachedAlgorithmHandle(string algorithm, string implementation)
		{
			string key = algorithm + implementation;
			SafeBCryptAlgorithmHandle safeBCryptAlgorithmHandle;
			if (this.m_algorithmHandles.ContainsKey(key))
			{
				safeBCryptAlgorithmHandle = (this.m_algorithmHandles[key].Target as SafeBCryptAlgorithmHandle);
				if (safeBCryptAlgorithmHandle != null)
				{
					return safeBCryptAlgorithmHandle;
				}
			}
			safeBCryptAlgorithmHandle = BCryptNative.OpenAlgorithm(algorithm, implementation);
			this.m_algorithmHandles[key] = new WeakReference(safeBCryptAlgorithmHandle);
			return safeBCryptAlgorithmHandle;
		}

		// Token: 0x040005F5 RID: 1525
		[SecurityCritical]
		private Dictionary<string, WeakReference> m_algorithmHandles;
	}
}
