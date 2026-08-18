using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200087E RID: 2174
	[ComVisible(true)]
	public interface ICspAsymmetricAlgorithm
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06004F34 RID: 20276
		CspKeyContainerInfo CspKeyContainerInfo { get; }

		// Token: 0x06004F35 RID: 20277
		byte[] ExportCspBlob(bool includePrivateParameters);

		// Token: 0x06004F36 RID: 20278
		void ImportCspBlob(byte[] rawData);
	}
}
