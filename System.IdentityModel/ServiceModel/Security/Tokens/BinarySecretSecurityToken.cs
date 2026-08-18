using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000013 RID: 19
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class BinarySecretSecurityToken : SecurityToken
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003078 File Offset: 0x00001278
		public BinarySecretSecurityToken(int keySizeInBits) : this(SecurityUniqueId.Create().Value, keySizeInBits)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000308B File Offset: 0x0000128B
		public BinarySecretSecurityToken(string id, int keySizeInBits) : this(id, keySizeInBits, true)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003096 File Offset: 0x00001296
		public BinarySecretSecurityToken(byte[] key) : this(SecurityUniqueId.Create().Value, key)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000030A9 File Offset: 0x000012A9
		public BinarySecretSecurityToken(string id, byte[] key) : this(id, key, true)
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030B4 File Offset: 0x000012B4
		protected BinarySecretSecurityToken(string id, int keySizeInBits, bool allowCrypto)
		{
			if (keySizeInBits <= 0 || keySizeInBits >= 512)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keySizeInBits", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					512
				})));
			}
			if (keySizeInBits % 8 != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keySizeInBits", SR.GetString("KeyLengthMustBeMultipleOfEight", new object[]
				{
					keySizeInBits
				})));
			}
			this.id = id;
			this.effectiveTime = DateTime.UtcNow;
			this.key = new byte[keySizeInBits / 8];
			CryptoHelper.FillRandomBytes(this.key);
			if (allowCrypto)
			{
				this.securityKeys = SecurityUtils.CreateSymmetricSecurityKeys(this.key);
				return;
			}
			this.securityKeys = EmptyReadOnlyCollection<SecurityKey>.Instance;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000318C File Offset: 0x0000138C
		protected BinarySecretSecurityToken(string id, byte[] key, bool allowCrypto)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			this.id = id;
			this.effectiveTime = DateTime.UtcNow;
			this.key = new byte[key.Length];
			Buffer.BlockCopy(key, 0, this.key, 0, key.Length);
			if (allowCrypto)
			{
				this.securityKeys = SecurityUtils.CreateSymmetricSecurityKeys(this.key);
				return;
			}
			this.securityKeys = EmptyReadOnlyCollection<SecurityKey>.Instance;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003203 File Offset: 0x00001403
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000320B File Offset: 0x0000140B
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003213 File Offset: 0x00001413
		public override DateTime ValidTo
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000321A File Offset: 0x0000141A
		public int KeySize
		{
			get
			{
				return this.key.Length * 8;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003226 File Offset: 0x00001426
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.securityKeys;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000322E File Offset: 0x0000142E
		public byte[] GetKeyBytes()
		{
			return SecurityUtils.CloneBuffer(this.key);
		}

		// Token: 0x0400007D RID: 125
		private string id;

		// Token: 0x0400007E RID: 126
		private DateTime effectiveTime;

		// Token: 0x0400007F RID: 127
		private byte[] key;

		// Token: 0x04000080 RID: 128
		private ReadOnlyCollection<SecurityKey> securityKeys;
	}
}
