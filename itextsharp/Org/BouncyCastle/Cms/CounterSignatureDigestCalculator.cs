using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000356 RID: 854
	internal class CounterSignatureDigestCalculator : IDigestCalculator
	{
		// Token: 0x06001EC1 RID: 7873 RVA: 0x000B97F0 File Offset: 0x000B87F0
		internal CounterSignatureDigestCalculator(string alg, byte[] data)
		{
			this.alg = alg;
			this.data = data;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000B9808 File Offset: 0x000B8808
		public byte[] GetDigest()
		{
			IDigest digestInstance = CmsSignedHelper.Instance.GetDigestInstance(this.alg);
			return DigestUtilities.DoFinal(digestInstance, this.data);
		}

		// Token: 0x04001546 RID: 5446
		private readonly string alg;

		// Token: 0x04001547 RID: 5447
		private readonly byte[] data;
	}
}
