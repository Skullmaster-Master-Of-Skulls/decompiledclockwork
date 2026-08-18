using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000240 RID: 576
	public class KeyGenerationParameters
	{
		// Token: 0x06001651 RID: 5713 RVA: 0x00082390 File Offset: 0x00081390
		public KeyGenerationParameters(SecureRandom random, int strength)
		{
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			if (strength < 1)
			{
				throw new ArgumentException("strength must be a positive value", "strength");
			}
			this.random = random;
			this.strength = strength;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000823C8 File Offset: 0x000813C8
		public SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000823D0 File Offset: 0x000813D0
		public int Strength
		{
			get
			{
				return this.strength;
			}
		}

		// Token: 0x04000F4F RID: 3919
		private SecureRandom random;

		// Token: 0x04000F50 RID: 3920
		private int strength;
	}
}
