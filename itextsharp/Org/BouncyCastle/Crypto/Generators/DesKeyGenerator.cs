using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020002A5 RID: 677
	public class DesKeyGenerator : CipherKeyGenerator
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0009467A File Offset: 0x0009367A
		public DesKeyGenerator()
		{
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00094682 File Offset: 0x00093682
		internal DesKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0009468C File Offset: 0x0009368C
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			base.engineInit(parameters);
			if (this.strength == 0 || this.strength == 7)
			{
				this.strength = 8;
				return;
			}
			if (this.strength != 8)
			{
				throw new ArgumentException("DES key must be " + 64 + " bits long.");
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000946E0 File Offset: 0x000936E0
		protected override byte[] engineGenerateKey()
		{
			byte[] array;
			do
			{
				array = this.random.GenerateSeed(8);
				DesParameters.SetOddParity(array);
			}
			while (DesParameters.IsWeakKey(array, 0));
			return array;
		}
	}
}
