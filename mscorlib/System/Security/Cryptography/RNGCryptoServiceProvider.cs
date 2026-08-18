using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000867 RID: 2151
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x06004E81 RID: 20097 RVA: 0x0011004D File Offset: 0x0010F04D
		public RNGCryptoServiceProvider() : this(null)
		{
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00110056 File Offset: 0x0010F056
		public RNGCryptoServiceProvider(string str) : this(null)
		{
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0011005F File Offset: 0x0010F05F
		public RNGCryptoServiceProvider(byte[] rgb) : this(null)
		{
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00110068 File Offset: 0x0010F068
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			if (cspParams != null)
			{
				this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
				return;
			}
			this.m_safeProvHandle = Utils.StaticProvHandle;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0011008B File Offset: 0x0010F08B
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Utils._GetBytes(this.m_safeProvHandle, data);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x001100A7 File Offset: 0x0010F0A7
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Utils._GetNonZeroBytes(this.m_safeProvHandle, data);
		}

		// Token: 0x04002896 RID: 10390
		private SafeProvHandle m_safeProvHandle;
	}
}
