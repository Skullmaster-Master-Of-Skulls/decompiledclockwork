using System;

namespace System.Web
{
	// Token: 0x020000B6 RID: 182
	public enum ApplicationShutdownReason
	{
		// Token: 0x040004C5 RID: 1221
		None,
		// Token: 0x040004C6 RID: 1222
		HostingEnvironment,
		// Token: 0x040004C7 RID: 1223
		ChangeInGlobalAsax,
		// Token: 0x040004C8 RID: 1224
		ConfigurationChange,
		// Token: 0x040004C9 RID: 1225
		UnloadAppDomainCalled,
		// Token: 0x040004CA RID: 1226
		ChangeInSecurityPolicyFile,
		// Token: 0x040004CB RID: 1227
		BinDirChangeOrDirectoryRename,
		// Token: 0x040004CC RID: 1228
		BrowsersDirChangeOrDirectoryRename,
		// Token: 0x040004CD RID: 1229
		CodeDirChangeOrDirectoryRename,
		// Token: 0x040004CE RID: 1230
		ResourcesDirChangeOrDirectoryRename,
		// Token: 0x040004CF RID: 1231
		IdleTimeout,
		// Token: 0x040004D0 RID: 1232
		PhysicalApplicationPathChanged,
		// Token: 0x040004D1 RID: 1233
		HttpRuntimeClose,
		// Token: 0x040004D2 RID: 1234
		InitializationError,
		// Token: 0x040004D3 RID: 1235
		MaxRecompilationsReached,
		// Token: 0x040004D4 RID: 1236
		BuildManagerChange
	}
}
