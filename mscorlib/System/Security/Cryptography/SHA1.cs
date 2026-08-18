using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008AA RID: 2218
	[ComVisible(true)]
	public abstract class SHA1 : HashAlgorithm
	{
		// Token: 0x0600509C RID: 20636 RVA: 0x0011FEAC File Offset: 0x0011EEAC
		protected SHA1()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x0011FEBF File Offset: 0x0011EEBF
		public new static SHA1 Create()
		{
			return SHA1.Create("System.Security.Cryptography.SHA1");
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0011FECB File Offset: 0x0011EECB
		public new static SHA1 Create(string hashName)
		{
			return (SHA1)CryptoConfig.CreateFromName(hashName);
		}
	}
}
