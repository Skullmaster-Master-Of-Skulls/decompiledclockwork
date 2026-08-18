using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005C RID: 92
	internal enum StoreTransactionOperationType
	{
		// Token: 0x04000190 RID: 400
		Invalid,
		// Token: 0x04000191 RID: 401
		SetCanonicalizationContext = 14,
		// Token: 0x04000192 RID: 402
		StageComponent = 20,
		// Token: 0x04000193 RID: 403
		PinDeployment,
		// Token: 0x04000194 RID: 404
		UnpinDeployment,
		// Token: 0x04000195 RID: 405
		StageComponentFile,
		// Token: 0x04000196 RID: 406
		InstallDeployment,
		// Token: 0x04000197 RID: 407
		UninstallDeployment,
		// Token: 0x04000198 RID: 408
		SetDeploymentMetadata,
		// Token: 0x04000199 RID: 409
		Scavenge
	}
}
