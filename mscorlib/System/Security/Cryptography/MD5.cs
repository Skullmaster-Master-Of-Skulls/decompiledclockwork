using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200088F RID: 2191
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		// Token: 0x06004FC4 RID: 20420 RVA: 0x0011579E File Offset: 0x0011479E
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x001157B1 File Offset: 0x001147B1
		public new static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x001157BD File Offset: 0x001147BD
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}
