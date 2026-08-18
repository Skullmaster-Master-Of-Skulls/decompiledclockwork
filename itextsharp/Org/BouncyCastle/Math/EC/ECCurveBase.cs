using System;

namespace Org.BouncyCastle.Math.EC
{
	// Token: 0x0200029C RID: 668
	public abstract class ECCurveBase : ECCurve
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x00093447 File Offset: 0x00092447
		protected internal ECCurveBase()
		{
		}

		// Token: 0x06001923 RID: 6435
		protected internal abstract ECPoint DecompressPoint(int yTilde, BigInteger X1);

		// Token: 0x06001924 RID: 6436 RVA: 0x00093450 File Offset: 0x00092450
		public override ECPoint DecodePoint(byte[] encoded)
		{
			int num = (this.FieldSize + 7) / 8;
			switch (encoded[0])
			{
			case 0:
				if (encoded.Length != 1)
				{
					throw new ArgumentException("Incorrect length for infinity encoding", "encoded");
				}
				return this.Infinity;
			case 2:
			case 3:
			{
				if (encoded.Length != num + 1)
				{
					throw new ArgumentException("Incorrect length for compressed encoding", "encoded");
				}
				int yTilde = (int)(encoded[0] & 1);
				BigInteger x = new BigInteger(1, encoded, 1, encoded.Length - 1);
				return this.DecompressPoint(yTilde, x);
			}
			case 4:
			case 6:
			case 7:
			{
				if (encoded.Length != 2 * num + 1)
				{
					throw new ArgumentException("Incorrect length for uncompressed/hybrid encoding", "encoded");
				}
				BigInteger x2 = new BigInteger(1, encoded, 1, num);
				BigInteger y = new BigInteger(1, encoded, 1 + num, num);
				return this.CreatePoint(x2, y, false);
			}
			}
			throw new FormatException("Invalid point encoding " + encoded[0]);
		}
	}
}
