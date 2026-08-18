using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000101 RID: 257
	public struct ECParameters
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		public void Validate()
		{
			bool flag = false;
			if (this.Q.X == null || this.Q.Y == null || this.Q.X.Length != this.Q.Y.Length)
			{
				flag = true;
			}
			if (!flag)
			{
				if (this.Curve.IsExplicit)
				{
					flag = (this.D != null && this.D.Length != this.Curve.Order.Length);
				}
				else if (this.Curve.IsNamed)
				{
					flag = (this.D != null && this.D.Length != this.Q.X.Length);
				}
			}
			if (flag)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidCurveKeyParameters"));
			}
			this.Curve.Validate();
		}

		// Token: 0x0400067A RID: 1658
		public ECPoint Q;

		// Token: 0x0400067B RID: 1659
		public byte[] D;

		// Token: 0x0400067C RID: 1660
		public ECCurve Curve;
	}
}
