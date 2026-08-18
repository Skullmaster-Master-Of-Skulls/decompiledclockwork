using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000889 RID: 2185
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x00114CCB File Offset: 0x00113CCB
		public HMACSHA384() : this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00114CE0 File Offset: 0x00113CE0
		public HMACSHA384(byte[] key)
		{
			Utils._ShowLegacyHmacWarning();
			this.m_hashName = "SHA384";
			this.m_hash1 = new SHA384Managed();
			this.m_hash2 = new SHA384Managed();
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00114D42 File Offset: 0x00113D42
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

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x00114D54 File Offset: 0x00113D54
		// (set) Token: 0x06004F96 RID: 20374 RVA: 0x00114D5C File Offset: 0x00113D5C
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

		// Token: 0x04002907 RID: 10503
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();
	}
}
