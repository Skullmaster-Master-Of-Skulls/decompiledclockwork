using System;

namespace System.Web.Optimization
{
	// Token: 0x0200002C RID: 44
	public class ScriptBundle : Bundle
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00005432 File Offset: 0x00003632
		public ScriptBundle(string virtualPath) : this(virtualPath, null)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000543C File Offset: 0x0000363C
		public ScriptBundle(string virtualPath, string cdnPath) : base(virtualPath, cdnPath, new IBundleTransform[]
		{
			new JsMinify()
		})
		{
			base.ConcatenationToken = ";" + Environment.NewLine;
		}
	}
}
