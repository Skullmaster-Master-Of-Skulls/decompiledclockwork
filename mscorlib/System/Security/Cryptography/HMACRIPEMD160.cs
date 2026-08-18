using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000886 RID: 2182
	[ComVisible(true)]
	public class HMACRIPEMD160 : HMAC
	{
		// Token: 0x06004F8B RID: 20363 RVA: 0x00114BBB File Offset: 0x00113BBB
		public HMACRIPEMD160() : this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00114BCA File Offset: 0x00113BCA
		public HMACRIPEMD160(byte[] key)
		{
			this.m_hashName = "RIPEMD160";
			this.m_hash1 = new RIPEMD160Managed();
			this.m_hash2 = new RIPEMD160Managed();
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}
