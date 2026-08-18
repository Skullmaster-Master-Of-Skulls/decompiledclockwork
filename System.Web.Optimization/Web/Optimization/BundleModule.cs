using System;

namespace System.Web.Optimization
{
	// Token: 0x0200000F RID: 15
	public class BundleModule : IHttpModule
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003AE0 File Offset: 0x00001CE0
		protected virtual void Dispose()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003AE2 File Offset: 0x00001CE2
		protected virtual void Init(HttpApplication application)
		{
			if (application == null)
			{
				throw new ArgumentNullException("application");
			}
			application.PostResolveRequestCache += this.OnApplicationPostResolveRequestCache;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B04 File Offset: 0x00001D04
		private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			if (BundleTable.Bundles.Count > 0)
			{
				BundleHandler.RemapHandlerForBundleRequests(app);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B2C File Offset: 0x00001D2C
		void IHttpModule.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B34 File Offset: 0x00001D34
		void IHttpModule.Init(HttpApplication application)
		{
			this.Init(application);
		}
	}
}
