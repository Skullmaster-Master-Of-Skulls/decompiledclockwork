using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000245 RID: 581
	public class DHPublicKeyParameters : DHKeyParameters
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x00082583 File Offset: 0x00081583
		public DHPublicKeyParameters(BigInteger y, DHParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x000825A2 File Offset: 0x000815A2
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000825AC File Offset: 0x000815AC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPublicKeyParameters dhpublicKeyParameters = obj as DHPublicKeyParameters;
			return dhpublicKeyParameters != null && this.Equals(dhpublicKeyParameters);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000825D2 File Offset: 0x000815D2
		protected bool Equals(DHPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000825F0 File Offset: 0x000815F0
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04000F57 RID: 3927
		private readonly BigInteger y;
	}
}
