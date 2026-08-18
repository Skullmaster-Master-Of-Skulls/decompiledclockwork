using System;
using System.Linq;

namespace System.Web.Optimization
{
	// Token: 0x02000038 RID: 56
	internal sealed class DefaultTransform : IBundleTransform
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000612D File Offset: 0x0000432D
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006135 File Offset: 0x00004335
		public string ContentType { get; set; }

		// Token: 0x0600018D RID: 397 RVA: 0x0000613E File Offset: 0x0000433E
		public DefaultTransform()
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006146 File Offset: 0x00004346
		public DefaultTransform(string contentType)
		{
			this.ContentType = contentType;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006158 File Offset: 0x00004358
		public void Process(BundleContext context, BundleResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (!string.IsNullOrEmpty(this.ContentType))
			{
				response.ContentType = this.ContentType;
				return;
			}
			if (string.IsNullOrEmpty(response.ContentType) && response.Files != null)
			{
				BundleFile bundleFile = response.Files.FirstOrDefault<BundleFile>();
				if (bundleFile != null)
				{
					string extension = VirtualPathUtility.GetExtension(bundleFile.VirtualFile.VirtualPath);
					if (string.Equals(extension, ".js", StringComparison.OrdinalIgnoreCase))
					{
						response.ContentType = JsMinify.JsContentType;
						return;
					}
					if (string.Equals(extension, ".css", StringComparison.OrdinalIgnoreCase))
					{
						response.ContentType = CssMinify.CssContentType;
					}
				}
			}
		}

		// Token: 0x04000081 RID: 129
		internal static readonly DefaultTransform Instance = new DefaultTransform();
	}
}
