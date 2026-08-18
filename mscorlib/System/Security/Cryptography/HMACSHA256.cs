using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000888 RID: 2184
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		// Token: 0x06004F90 RID: 20368 RVA: 0x00114C81 File Offset: 0x00113C81
		public HMACSHA256() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x00114C90 File Offset: 0x00113C90
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.m_hash1 = new SHA256Managed();
			this.m_hash2 = new SHA256Managed();
			this.HashSizeValue = 256;
			base.InitializeKey(key);
		}
	}
}
