using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000549 RID: 1353
	public class NaccacheSternKeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002E8E RID: 11918 RVA: 0x0011FAD7 File Offset: 0x0011EAD7
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes) : this(random, strength, certainty, countSmallPrimes, false)
		{
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0011FAE8 File Offset: 0x0011EAE8
		public NaccacheSternKeyGenerationParameters(SecureRandom random, int strength, int certainty, int countSmallPrimes, bool debug) : base(random, strength)
		{
			if (countSmallPrimes % 2 == 1)
			{
				throw new ArgumentException("countSmallPrimes must be a multiple of 2");
			}
			if (countSmallPrimes < 30)
			{
				throw new ArgumentException("countSmallPrimes must be >= 30 for security reasons");
			}
			this.certainty = certainty;
			this.countSmallPrimes = countSmallPrimes;
			this.debug = debug;
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x0011FB37 File Offset: 0x0011EB37
		public int Certainty
		{
			get
			{
				return this.certainty;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x0011FB3F File Offset: 0x0011EB3F
		public int CountSmallPrimes
		{
			get
			{
				return this.countSmallPrimes;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x0011FB47 File Offset: 0x0011EB47
		public bool IsDebug
		{
			get
			{
				return this.debug;
			}
		}

		// Token: 0x0400200E RID: 8206
		private readonly int certainty;

		// Token: 0x0400200F RID: 8207
		private readonly int countSmallPrimes;

		// Token: 0x04002010 RID: 8208
		private bool debug;
	}
}
