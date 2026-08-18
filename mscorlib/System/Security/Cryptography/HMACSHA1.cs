using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000887 RID: 2183
	[ComVisible(true)]
	public class HMACSHA1 : HMAC
	{
		// Token: 0x06004F8D RID: 20365 RVA: 0x00114C05 File Offset: 0x00113C05
		public HMACSHA1() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x00114C14 File Offset: 0x00113C14
		public HMACSHA1(byte[] key) : this(key, false)
		{
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x00114C20 File Offset: 0x00113C20
		public HMACSHA1(byte[] key, bool useManagedSha1)
		{
			this.m_hashName = "SHA1";
			if (useManagedSha1)
			{
				this.m_hash1 = new SHA1Managed();
				this.m_hash2 = new SHA1Managed();
			}
			else
			{
				this.m_hash1 = new SHA1CryptoServiceProvider();
				this.m_hash2 = new SHA1CryptoServiceProvider();
			}
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}
