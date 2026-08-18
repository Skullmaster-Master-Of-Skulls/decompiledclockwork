using System;

namespace System.Web.WebPages
{
	// Token: 0x02000057 RID: 87
	internal class UrlRewriterHelper
	{
		// Token: 0x06000212 RID: 530 RVA: 0x000087DC File Offset: 0x000069DC
		private static bool WasThisRequestRewritten(HttpContextBase httpContext)
		{
			if (httpContext.Items.Contains("IIS_WasUrlRewritten"))
			{
				return object.Equals(httpContext.Items["IIS_WasUrlRewritten"], "true");
			}
			HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)httpContext.GetService(typeof(HttpWorkerRequest));
			bool flag = httpWorkerRequest != null && httpWorkerRequest.GetServerVariable("IIS_WasUrlRewritten") != null;
			if (flag)
			{
				httpContext.Items.Add("IIS_WasUrlRewritten", "true");
			}
			else
			{
				httpContext.Items.Add("IIS_WasUrlRewritten", "false");
			}
			return flag;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008874 File Offset: 0x00006A74
		private bool IsUrlRewriterTurnedOn(HttpContextBase httpContext)
		{
			if (!this._urlRewriterIsTurnedOnCalculated)
			{
				lock (this._lockObject)
				{
					if (!this._urlRewriterIsTurnedOnCalculated)
					{
						HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)httpContext.GetService(typeof(HttpWorkerRequest));
						bool urlRewriterIsTurnedOnValue = httpWorkerRequest != null && httpWorkerRequest.GetServerVariable("IIS_UrlRewriteModule") != null;
						this._urlRewriterIsTurnedOnValue = urlRewriterIsTurnedOnValue;
						this._urlRewriterIsTurnedOnCalculated = true;
					}
				}
			}
			return this._urlRewriterIsTurnedOnValue;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008908 File Offset: 0x00006B08
		public virtual bool WasRequestRewritten(HttpContextBase httpContext)
		{
			return this.IsUrlRewriterTurnedOn(httpContext) && UrlRewriterHelper.WasThisRequestRewritten(httpContext);
		}

		// Token: 0x040000AB RID: 171
		internal const string UrlWasRewrittenServerVar = "IIS_WasUrlRewritten";

		// Token: 0x040000AC RID: 172
		internal const string UrlRewriterEnabledServerVar = "IIS_UrlRewriteModule";

		// Token: 0x040000AD RID: 173
		internal const string UrlWasRequestRewrittenTrueValue = "true";

		// Token: 0x040000AE RID: 174
		internal const string UrlWasRequestRewrittenFalseValue = "false";

		// Token: 0x040000AF RID: 175
		private object _lockObject = new object();

		// Token: 0x040000B0 RID: 176
		private bool _urlRewriterIsTurnedOnValue;

		// Token: 0x040000B1 RID: 177
		private volatile bool _urlRewriterIsTurnedOnCalculated;
	}
}
