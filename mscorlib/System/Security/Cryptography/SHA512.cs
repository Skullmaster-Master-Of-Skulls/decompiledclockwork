using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008B1 RID: 2225
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		// Token: 0x060050D5 RID: 20693 RVA: 0x001219C1 File Offset: 0x001209C1
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x001219D4 File Offset: 0x001209D4
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x001219E0 File Offset: 0x001209E0
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}
