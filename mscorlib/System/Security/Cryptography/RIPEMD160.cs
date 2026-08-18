using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000897 RID: 2199
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		// Token: 0x06005008 RID: 20488 RVA: 0x00116742 File Offset: 0x00115742
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0011674E File Offset: 0x0011574E
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}
