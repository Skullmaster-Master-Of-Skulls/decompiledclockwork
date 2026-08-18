using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200088A RID: 2186
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		// Token: 0x06004F97 RID: 20375 RVA: 0x00114D7D File Offset: 0x00113D7D
		public HMACSHA512() : this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00114D90 File Offset: 0x00113D90
		public HMACSHA512(byte[] key)
		{
			Utils._ShowLegacyHmacWarning();
			this.m_hashName = "SHA512";
			this.m_hash1 = new SHA512Managed();
			this.m_hash2 = new SHA512Managed();
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x00114DF2 File Offset: 0x00113DF2
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00114E04 File Offset: 0x00113E04
		// (set) Token: 0x06004F9B RID: 20379 RVA: 0x00114E0C File Offset: 0x00113E0C
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04002908 RID: 10504
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}
