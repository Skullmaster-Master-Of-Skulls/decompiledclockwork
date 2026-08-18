using System;

namespace System.Net.Cache
{
	// Token: 0x0200056A RID: 1386
	internal class RequestCacheBinding
	{
		// Token: 0x06002A95 RID: 10901 RVA: 0x000B4FFC File Offset: 0x000B3FFC
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x000B5019 File Offset: 0x000B4019
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000B5021 File Offset: 0x000B4021
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000B5029 File Offset: 0x000B4029
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04002915 RID: 10517
		private RequestCache m_RequestCache;

		// Token: 0x04002916 RID: 10518
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x04002917 RID: 10519
		private RequestCachePolicy m_Policy;
	}
}
