using System;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000026 RID: 38
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpCachePolicyBase
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HttpCacheVaryByParams VaryByParams
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AppendCacheExtension(string extension)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetAllowResponseInBrowserHistory(bool allow)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetCacheability(HttpCacheability cacheability)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetCacheability(HttpCacheability cacheability, string field)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetETag(string etag)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetETagFromFileDependencies()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetExpires(DateTime date)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetLastModified(DateTime date)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetLastModifiedFromFileDependencies()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetMaxAge(TimeSpan delta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetNoServerCaching()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetNoStore()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetNoTransforms()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetOmitVaryStar(bool omit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetProxyMaxAge(TimeSpan delta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetSlidingExpiration(bool slide)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetValidUntilExpires(bool validUntilExpires)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SetVaryByCustom(string custom)
		{
			throw new NotImplementedException();
		}
	}
}
