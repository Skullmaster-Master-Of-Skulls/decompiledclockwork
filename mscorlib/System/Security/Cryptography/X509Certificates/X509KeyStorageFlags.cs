using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008C1 RID: 2241
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum X509KeyStorageFlags
	{
		// Token: 0x04002A14 RID: 10772
		DefaultKeySet = 0,
		// Token: 0x04002A15 RID: 10773
		UserKeySet = 1,
		// Token: 0x04002A16 RID: 10774
		MachineKeySet = 2,
		// Token: 0x04002A17 RID: 10775
		Exportable = 4,
		// Token: 0x04002A18 RID: 10776
		UserProtected = 8,
		// Token: 0x04002A19 RID: 10777
		PersistKeySet = 16
	}
}
