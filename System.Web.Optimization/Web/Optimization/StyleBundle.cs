using System;

namespace System.Web.Optimization
{
	// Token: 0x0200002D RID: 45
	public class StyleBundle : Bundle
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00005478 File Offset: 0x00003678
		public StyleBundle(string virtualPath) : base(virtualPath, new IBundleTransform[]
		{
			new CssMinify()
		})
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000549C File Offset: 0x0000369C
		public StyleBundle(string virtualPath, string cdnPath) : base(virtualPath, cdnPath, new IBundleTransform[]
		{
			new CssMinify()
		})
		{
		}
	}
}
