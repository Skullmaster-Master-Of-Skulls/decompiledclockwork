using System;
using System.ComponentModel;
using System.Web.Compilation;

namespace System.Web.WebPages.Razor
{
	// Token: 0x0200000B RID: 11
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002463 File Offset: 0x00000663
		public static void Start()
		{
			if (PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			PreApplicationStartCode._startWasCalled = true;
			BuildProvider.RegisterBuildProvider(".cshtml", typeof(RazorBuildProvider));
			BuildProvider.RegisterBuildProvider(".vbhtml", typeof(RazorBuildProvider));
		}

		// Token: 0x04000017 RID: 23
		private static bool _startWasCalled;
	}
}
