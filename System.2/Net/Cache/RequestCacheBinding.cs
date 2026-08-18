using System;

namespace System.Net.Cache
{
	// Token: 0x02000310 RID: 784
	internal class RequestCacheBinding
	{
		// Token: 0x06001C15 RID: 7189 RVA: 0x00085E6C File Offset: 0x0008406C
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00085E89 File Offset: 0x00084089
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00085E91 File Offset: 0x00084091
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x00085E99 File Offset: 0x00084099
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x04001B54 RID: 6996
		private RequestCache m_RequestCache;

		// Token: 0x04001B55 RID: 6997
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x04001B56 RID: 6998
		private RequestCachePolicy m_Policy;
	}
}
