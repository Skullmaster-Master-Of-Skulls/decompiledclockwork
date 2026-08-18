using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200002A RID: 42
	internal struct CATEGORY_INSTANCE
	{
		// Token: 0x04000125 RID: 293
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x04000126 RID: 294
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
