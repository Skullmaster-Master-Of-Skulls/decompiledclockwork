using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020002A6 RID: 678
	public class DesEdeKeyGenerator : DesKeyGenerator
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x0009470A File Offset: 0x0009370A
		public DesEdeKeyGenerator()
		{
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00094712 File Offset: 0x00093712
		internal DesEdeKeyGenerator(int defaultStrength) : base(defaultStrength)
		{
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0009471C File Offset: 0x0009371C
		protected override void engineInit(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
			this.strength = (parameters.Strength + 7) / 8;
			if (this.strength == 0 || this.strength == 21)
			{
				this.strength = 24;
				return;
			}
			if (this.strength == 14)
			{
				this.strength = 16;
				return;
			}
			if (this.strength != 24 && this.strength != 16)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"DESede key must be ",
					192,
					" or ",
					128,
					" bits long."
				}));
			}
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000947CC File Offset: 0x000937CC
		protected override byte[] engineGenerateKey()
		{
			byte[] array;
			do
			{
				array = this.random.GenerateSeed(this.strength);
				DesParameters.SetOddParity(array);
			}
			while (DesEdeParameters.IsWeakKey(array, 0, array.Length));
			return array;
		}
	}
}
