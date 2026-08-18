using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000860 RID: 2144
	[ComVisible(true)]
	[Serializable]
	public enum CipherMode
	{
		// Token: 0x04002885 RID: 10373
		CBC = 1,
		// Token: 0x04002886 RID: 10374
		ECB,
		// Token: 0x04002887 RID: 10375
		OFB,
		// Token: 0x04002888 RID: 10376
		CFB,
		// Token: 0x04002889 RID: 10377
		CTS
	}
}
