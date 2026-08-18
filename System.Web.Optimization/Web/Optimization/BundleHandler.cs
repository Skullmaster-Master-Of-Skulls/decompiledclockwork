using System;
using System.Web.Hosting;

namespace System.Web.Optimization
{
	// Token: 0x0200000D RID: 13
	internal sealed class BundleHandler : IHttpHandler
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000036AA File Offset: 0x000018AA
		public BundleHandler(Bundle requestBundle, string bundleVirtualPath)
		{
			this.RequestBundle = requestBundle;
			this.BundleVirtualPath = bundleVirtualPath;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000036C0 File Offset: 0x000018C0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000036C8 File Offset: 0x000018C8
		public Bundle RequestBundle { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000036D1 File Offset: 0x000018D1
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000036D9 File Offset: 0x000018D9
		public string BundleVirtualPath { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000036E2 File Offset: 0x000018E2
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000036E5 File Offset: 0x000018E5
		internal static string GetBundleUrlFromContext(HttpContextBase context)
		{
			return context.Request.AppRelativeCurrentExecutionFilePath + context.Request.PathInfo;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003704 File Offset: 0x00001904
		internal static bool RemapHandlerForBundleRequests(HttpApplication app)
		{
			HttpContextBase httpContextBase = new HttpContextWrapper(app.Context);
			string appRelativeCurrentExecutionFilePath = httpContextBase.Request.AppRelativeCurrentExecutionFilePath;
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			if (virtualPathProvider.FileExists(appRelativeCurrentExecutionFilePath) || virtualPathProvider.DirectoryExists(appRelativeCurrentExecutionFilePath))
			{
				return false;
			}
			string bundleUrlFromContext = BundleHandler.GetBundleUrlFromContext(httpContextBase);
			Bundle bundleFor = BundleTable.Bundles.GetBundleFor(bundleUrlFromContext);
			if (bundleFor != null)
			{
				httpContextBase.RemapHandler(new BundleHandler(bundleFor, bundleUrlFromContext));
				return true;
			}
			return false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000376C File Offset: 0x0000196C
		public void ProcessRequest(HttpContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.Response.Clear();
			BundleContext context2 = new BundleContext(new HttpContextWrapper(context), BundleTable.Bundles, this.BundleVirtualPath);
			this.RequestBundle.ProcessRequest(context2);
		}
	}
}
