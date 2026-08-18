using System;
using System.Collections.Generic;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages.Scope
{
	// Token: 0x02000077 RID: 119
	public class AspNetRequestScopeStorageProvider : IScopeStorageProvider
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000C29B File Offset: 0x0000A49B
		public AspNetRequestScopeStorageProvider() : this(null, () => WebPageHttpModule.AppStartExecuteCompleted)
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C2C1 File Offset: 0x0000A4C1
		internal AspNetRequestScopeStorageProvider(HttpContextBase httpContext, Func<bool> appStartExecuted)
		{
			this._httpContext = httpContext;
			this._appStartExecuted = appStartExecuted;
			this.ApplicationScope = new ApplicationScopeStorageDictionary();
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000C2FE File Offset: 0x0000A4FE
		public IDictionary<object, object> CurrentScope
		{
			get
			{
				IDictionary<object, object> result;
				if ((result = this.PageScope) == null)
				{
					result = (this.RequestScopeInternal ?? this.ApplicationScope);
				}
				return result;
			}
			set
			{
				if (!this._appStartExecuted())
				{
					throw new InvalidOperationException(WebPageResources.StateStorage_StorageScopesCannotBeCreated);
				}
				this.PageScope = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000C31F File Offset: 0x0000A51F
		public IDictionary<object, object> GlobalScope
		{
			get
			{
				return this.ApplicationScope;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000C327 File Offset: 0x0000A527
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000C32F File Offset: 0x0000A52F
		public IDictionary<object, object> ApplicationScope { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000C338 File Offset: 0x0000A538
		public IDictionary<object, object> RequestScope
		{
			get
			{
				IDictionary<object, object> requestScopeInternal = this.RequestScopeInternal;
				if (requestScopeInternal == null)
				{
					throw new InvalidOperationException(WebPageResources.StateStorage_RequestScopeNotAvailable);
				}
				return requestScopeInternal;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000C35C File Offset: 0x0000A55C
		private HttpContextBase HttpContext
		{
			get
			{
				HttpContext httpContext = System.Web.HttpContext.Current;
				HttpContextBase result;
				if ((result = this._httpContext) == null)
				{
					if (httpContext != null)
					{
						return new HttpContextWrapper(httpContext);
					}
					result = null;
				}
				return result;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000C384 File Offset: 0x0000A584
		private IDictionary<object, object> RequestScopeInternal
		{
			get
			{
				if (this._appStartExecuted())
				{
					IDictionary<object, object> dictionary = (IDictionary<object, object>)this.HttpContext.Items[AspNetRequestScopeStorageProvider._requestScopeKey];
					if (dictionary == null)
					{
						dictionary = (this.HttpContext.Items[AspNetRequestScopeStorageProvider._requestScopeKey] = new ScopeStorageDictionary(this.ApplicationScope));
					}
					return dictionary;
				}
				return null;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000C407 File Offset: 0x0000A607
		private IDictionary<object, object> PageScope
		{
			get
			{
				if (this.HttpContext == null)
				{
					return null;
				}
				return (IDictionary<object, object>)this.HttpContext.Items[AspNetRequestScopeStorageProvider._pageScopeKey];
			}
			set
			{
				this.HttpContext.Items[AspNetRequestScopeStorageProvider._pageScopeKey] = value;
			}
		}

		// Token: 0x04000109 RID: 265
		private static readonly object _pageScopeKey = new object();

		// Token: 0x0400010A RID: 266
		private static readonly object _requestScopeKey = new object();

		// Token: 0x0400010B RID: 267
		private readonly HttpContextBase _httpContext;

		// Token: 0x0400010C RID: 268
		private readonly Func<bool> _appStartExecuted;
	}
}
