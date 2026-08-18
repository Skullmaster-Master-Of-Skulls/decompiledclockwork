using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002ED RID: 749
	public class RC5Parameters : KeyParameter
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x000A5EDB File Offset: 0x000A4EDB
		public RC5Parameters(byte[] key, int rounds) : base(key)
		{
			if (key.Length > 255)
			{
				throw new ArgumentException("RC5 key length can be no greater than 255");
			}
			this.rounds = rounds;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000A5F00 File Offset: 0x000A4F00
		public int Rounds
		{
			get
			{
				return this.rounds;
			}
		}

		// Token: 0x04001300 RID: 4864
		private readonly int rounds;
	}
}
