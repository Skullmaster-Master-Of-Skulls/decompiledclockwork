using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000027 RID: 39
	internal struct STORE_CATEGORY_INSTANCE
	{
		// Token: 0x04000121 RID: 289
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x04000122 RID: 290
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
