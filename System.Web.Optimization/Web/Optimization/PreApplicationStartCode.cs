using System;
using System.ComponentModel;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace System.Web.Optimization
{
	// Token: 0x02000039 RID: 57
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00006201 File Offset: 0x00004401
		public static void Start()
		{
			if (PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			PreApplicationStartCode._startWasCalled = true;
			DynamicModuleUtility.RegisterModule(typeof(BundleModule));
		}

		// Token: 0x04000083 RID: 131
		private static bool _startWasCalled;
	}
}
