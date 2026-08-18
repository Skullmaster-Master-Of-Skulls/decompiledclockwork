using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008AD RID: 2221
	[ComVisible(true)]
	public abstract class SHA256 : HashAlgorithm
	{
		// Token: 0x060050AD RID: 20653 RVA: 0x0012073C File Offset: 0x0011F73C
		protected SHA256()
		{
			this.HashSizeValue = 256;
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0012074F File Offset: 0x0011F74F
		public new static SHA256 Create()
		{
			return SHA256.Create("System.Security.Cryptography.SHA256");
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0012075B File Offset: 0x0011F75B
		public new static SHA256 Create(string hashName)
		{
			return (SHA256)CryptoConfig.CreateFromName(hashName);
		}
	}
}
