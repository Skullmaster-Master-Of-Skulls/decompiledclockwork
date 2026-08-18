using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.WebPages.Scope;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace System.Web.WebPages
{
	// Token: 0x02000097 RID: 151
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0000F620 File Offset: 0x0000D820
		public static void Start()
		{
			if (PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			PreApplicationStartCode._startWasCalled = true;
			WebPageHttpHandler.RegisterExtension("cshtml");
			WebPageHttpHandler.RegisterExtension("vbhtml");
			PageParser.EnableLongStringsAsResources = false;
			DynamicModuleUtility.RegisterModule(typeof(WebPageHttpModule));
			ScopeStorage.CurrentProvider = new AspNetRequestScopeStorageProvider();
		}

		// Token: 0x04000155 RID: 341
		private static bool _startWasCalled;
	}
}
