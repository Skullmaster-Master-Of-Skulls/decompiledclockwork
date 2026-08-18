using System;

namespace System.Web.Caching
{
	// Token: 0x02000886 RID: 2182
	internal class DependencyCacheEntry
	{
		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x001698EB File Offset: 0x00167AEB
		internal string ProviderName
		{
			get
			{
				return this._providerName;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x001698F3 File Offset: 0x00167AF3
		internal string OutputCacheEntryKey
		{
			get
			{
				return this._outputCacheEntryKey;
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x001698FB File Offset: 0x00167AFB
		internal string KernelCacheEntryKey
		{
			get
			{
				return this._kernelCacheEntryKey;
			}
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x00169903 File Offset: 0x00167B03
		internal DependencyCacheEntry(string oceKey, string kernelCacheEntryKey, string providerName)
		{
			this._outputCacheEntryKey = oceKey;
			this._kernelCacheEntryKey = kernelCacheEntryKey;
			this._providerName = providerName;
		}

		// Token: 0x040034EF RID: 13551
		private string _providerName;

		// Token: 0x040034F0 RID: 13552
		private string _outputCacheEntryKey;

		// Token: 0x040034F1 RID: 13553
		private string _kernelCacheEntryKey;
	}
}
