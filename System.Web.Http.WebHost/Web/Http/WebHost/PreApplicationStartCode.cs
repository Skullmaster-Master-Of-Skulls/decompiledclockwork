using System;
using System.ComponentModel;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000014 RID: 20
	[Obsolete("Use of this type is not recommended because it no longer has initialization logic.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PreApplicationStartCode
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000038DF File Offset: 0x00001ADF
		public static void Start()
		{
			if (PreApplicationStartCode._startWasCalled)
			{
				return;
			}
			PreApplicationStartCode._startWasCalled = true;
		}

		// Token: 0x0400001F RID: 31
		private static bool _startWasCalled;
	}
}
