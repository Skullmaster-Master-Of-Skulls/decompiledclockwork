using System;
using System.ComponentModel;
using System.Web.WebPages;
using System.Web.WebPages.Razor;
using System.Web.WebPages.Scope;

namespace System.Web.Mvc
{
	// Token: 0x020000D2 RID: 210
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		public static void Start()
		{
			if (System.Web.Mvc.PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			System.Web.Mvc.PreApplicationStartCode._startWasCalled = true;
			System.Web.WebPages.Razor.PreApplicationStartCode.Start();
			System.Web.WebPages.PreApplicationStartCode.Start();
			ViewContext.GlobalScopeThunk = (() => ScopeStorage.CurrentScope);
		}

		// Token: 0x0400017F RID: 383
		private static bool _startWasCalled;
	}
}
