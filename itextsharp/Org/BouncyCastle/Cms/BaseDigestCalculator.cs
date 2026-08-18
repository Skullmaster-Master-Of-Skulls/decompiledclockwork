using System;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000301 RID: 769
	internal class BaseDigestCalculator : IDigestCalculator
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x000A8A2B File Offset: 0x000A7A2B
		internal BaseDigestCalculator(byte[] digest)
		{
			this.digest = digest;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000A8A3A File Offset: 0x000A7A3A
		public byte[] GetDigest()
		{
			return Arrays.Clone(this.digest);
		}

		// Token: 0x04001357 RID: 4951
		private readonly byte[] digest;
	}
}
