using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000019 RID: 25
	public interface IBasicAgreement
	{
		// Token: 0x060000AB RID: 171
		void Init(ICipherParameters parameters);

		// Token: 0x060000AC RID: 172
		BigInteger CalculateAgreement(ICipherParameters pubKey);
	}
}
