using System;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000027 RID: 39
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpCachePolicyWrapper : HttpCachePolicyBase
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00004AD2 File Offset: 0x00002CD2
		public HttpCachePolicyWrapper(HttpCachePolicy httpCachePolicy)
		{
			if (httpCachePolicy == null)
			{
				throw new ArgumentNullException("httpCachePolicy");
			}
			this._httpCachePolicy = httpCachePolicy;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004AEF File Offset: 0x00002CEF
		public override HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				return this._httpCachePolicy.VaryByContentEncodings;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004AFC File Offset: 0x00002CFC
		public override HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				return this._httpCachePolicy.VaryByHeaders;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004B09 File Offset: 0x00002D09
		public override HttpCacheVaryByParams VaryByParams
		{
			get
			{
				return this._httpCachePolicy.VaryByParams;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00004B16 File Offset: 0x00002D16
		public override void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			this._httpCachePolicy.AddValidationCallback(handler, data);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004B25 File Offset: 0x00002D25
		public override void AppendCacheExtension(string extension)
		{
			this._httpCachePolicy.AppendCacheExtension(extension);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00004B33 File Offset: 0x00002D33
		public override void SetAllowResponseInBrowserHistory(bool allow)
		{
			this._httpCachePolicy.SetAllowResponseInBrowserHistory(allow);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00004B41 File Offset: 0x00002D41
		public override void SetCacheability(HttpCacheability cacheability)
		{
			this._httpCachePolicy.SetCacheability(cacheability);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00004B4F File Offset: 0x00002D4F
		public override void SetCacheability(HttpCacheability cacheability, string field)
		{
			this._httpCachePolicy.SetCacheability(cacheability, field);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00004B5E File Offset: 0x00002D5E
		public override void SetETag(string etag)
		{
			this._httpCachePolicy.SetETag(etag);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00004B6C File Offset: 0x00002D6C
		public override void SetETagFromFileDependencies()
		{
			this._httpCachePolicy.SetETagFromFileDependencies();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00004B79 File Offset: 0x00002D79
		public override void SetExpires(DateTime date)
		{
			this._httpCachePolicy.SetExpires(date);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004B87 File Offset: 0x00002D87
		public override void SetLastModified(DateTime date)
		{
			this._httpCachePolicy.SetLastModified(date);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00004B95 File Offset: 0x00002D95
		public override void SetLastModifiedFromFileDependencies()
		{
			this._httpCachePolicy.SetLastModifiedFromFileDependencies();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00004BA2 File Offset: 0x00002DA2
		public override void SetMaxAge(TimeSpan delta)
		{
			this._httpCachePolicy.SetMaxAge(delta);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public override void SetNoServerCaching()
		{
			this._httpCachePolicy.SetNoServerCaching();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00004BBD File Offset: 0x00002DBD
		public override void SetNoStore()
		{
			this._httpCachePolicy.SetNoStore();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00004BCA File Offset: 0x00002DCA
		public override void SetNoTransforms()
		{
			this._httpCachePolicy.SetNoTransforms();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public override void SetOmitVaryStar(bool omit)
		{
			this._httpCachePolicy.SetOmitVaryStar(omit);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00004BE5 File Offset: 0x00002DE5
		public override void SetProxyMaxAge(TimeSpan delta)
		{
			this._httpCachePolicy.SetProxyMaxAge(delta);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public override void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			this._httpCachePolicy.SetRevalidation(revalidation);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00004C01 File Offset: 0x00002E01
		public override void SetSlidingExpiration(bool slide)
		{
			this._httpCachePolicy.SetSlidingExpiration(slide);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00004C0F File Offset: 0x00002E0F
		public override void SetValidUntilExpires(bool validUntilExpires)
		{
			this._httpCachePolicy.SetValidUntilExpires(validUntilExpires);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00004C1D File Offset: 0x00002E1D
		public override void SetVaryByCustom(string custom)
		{
			this._httpCachePolicy.SetVaryByCustom(custom);
		}

		// Token: 0x04000109 RID: 265
		private HttpCachePolicy _httpCachePolicy;
	}
}
