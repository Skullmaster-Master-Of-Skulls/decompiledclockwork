using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000132 RID: 306
	public class CipherKeyGenerator
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x0003F7E7 File Offset: 0x0003E7E7
		public CipherKeyGenerator()
		{
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0003F7F6 File Offset: 0x0003E7F6
		internal CipherKeyGenerator(int defaultStrength)
		{
			if (defaultStrength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "defaultStrength");
			}
			this.defaultStrength = defaultStrength;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0003F820 File Offset: 0x0003E820
		public int DefaultStrength
		{
			get
			{
				return this.defaultStrength;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0003F828 File Offset: 0x0003E828
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.uninitialised = false;
			this.engineInit(parameters);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0003F846 File Offset: 0x0003E846
		protected virtual void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0003F864 File Offset: 0x0003E864
		public byte[] GenerateKey()
		{
			if (this.uninitialised)
			{
				if (this.defaultStrength < 1)
				{
					throw new InvalidOperationException("Generator has not been initialised");
				}
				this.uninitialised = false;
				this.engineInit(new KeyGenerationParameters(new SecureRandom(), this.defaultStrength));
			}
			return this.engineGenerateKey();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0003F8B0 File Offset: 0x0003E8B0
		protected virtual byte[] engineGenerateKey()
		{
			return this.random.GenerateSeed(this.strength);
		}

		// Token: 0x040008D9 RID: 2265
		protected internal SecureRandom random;

		// Token: 0x040008DA RID: 2266
		protected internal int strength;

		// Token: 0x040008DB RID: 2267
		private bool uninitialised = true;

		// Token: 0x040008DC RID: 2268
		private int defaultStrength;
	}
}
