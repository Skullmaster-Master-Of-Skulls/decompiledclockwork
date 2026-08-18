using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000872 RID: 2162
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CspProviderFlags
	{
		// Token: 0x040028AD RID: 10413
		NoFlags = 0,
		// Token: 0x040028AE RID: 10414
		UseMachineKeyStore = 1,
		// Token: 0x040028AF RID: 10415
		UseDefaultKeyContainer = 2,
		// Token: 0x040028B0 RID: 10416
		UseNonExportableKey = 4,
		// Token: 0x040028B1 RID: 10417
		UseExistingKey = 8,
		// Token: 0x040028B2 RID: 10418
		UseArchivableKey = 16,
		// Token: 0x040028B3 RID: 10419
		UseUserProtectedKey = 32,
		// Token: 0x040028B4 RID: 10420
		NoPrompt = 64
	}
}
