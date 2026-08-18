using System;
using Org.BouncyCastle.Asn1.X9;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200060D RID: 1549
	public abstract class ECPointBase : ECPoint
	{
		// Token: 0x060034C0 RID: 13504 RVA: 0x00147EE0 File Offset: 0x00146EE0
		protected internal ECPointBase(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060034C1 RID: 13505
		protected internal abstract bool YTilde { get; }

		// Token: 0x060034C2 RID: 13506 RVA: 0x00147EF0 File Offset: 0x00146EF0
		public override byte[] GetEncoded()
		{
			if (base.IsInfinity)
			{
				return new byte[1];
			}
			int byteLength = X9IntegerConverter.GetByteLength(this.x);
			byte[] array = X9IntegerConverter.IntegerToBytes(base.X.ToBigInteger(), byteLength);
			byte[] array2;
			if (this.withCompression)
			{
				array2 = new byte[1 + array.Length];
				array2[0] = (this.YTilde ? 3 : 2);
			}
			else
			{
				byte[] array3 = X9IntegerConverter.IntegerToBytes(base.Y.ToBigInteger(), byteLength);
				array2 = new byte[1 + array.Length + array3.Length];
				array2[0] = 4;
				array3.CopyTo(array2, 1 + array.Length);
			}
			array.CopyTo(array2, 1);
			return array2;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x00147F89 File Offset: 0x00146F89
		public override ECPoint Multiply(BigInteger k)
		{
			if (base.IsInfinity)
			{
				return this;
			}
			if (k.SignValue == 0)
			{
				return this.curve.Infinity;
			}
			this.AssertECMultiplier();
			return this.multiplier.Multiply(this, k, this.preCompInfo);
		}
	}
}
