using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020008AF RID: 2223
	[ComVisible(true)]
	public abstract class SHA384 : HashAlgorithm
	{
		// Token: 0x060050C1 RID: 20673 RVA: 0x00120F99 File Offset: 0x0011FF99
		protected SHA384()
		{
			this.HashSizeValue = 384;
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x00120FAC File Offset: 0x0011FFAC
		public new static SHA384 Create()
		{
			return SHA384.Create("System.Security.Cryptography.SHA384");
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00120FB8 File Offset: 0x0011FFB8
		public new static SHA384 Create(string hashName)
		{
			return (SHA384)CryptoConfig.CreateFromName(hashName);
		}
	}
}
