using System;
using Renci.SshNet.Common;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006D RID: 109
	public class RsaKey : Key, IDisposable
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public BigInteger Modulus
		{
			get
			{
				return this._privateKey[0];
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00013DB1 File Offset: 0x00011FB1
		public BigInteger Exponent
		{
			get
			{
				return this._privateKey[1];
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00013FFC File Offset: 0x000121FC
		public BigInteger D
		{
			get
			{
				if (this._privateKey.Length > 2)
				{
					return this._privateKey[2];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001401B File Offset: 0x0001221B
		public BigInteger P
		{
			get
			{
				if (this._privateKey.Length > 3)
				{
					return this._privateKey[3];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001403A File Offset: 0x0001223A
		public BigInteger Q
		{
			get
			{
				if (this._privateKey.Length > 4)
				{
					return this._privateKey[4];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00014059 File Offset: 0x00012259
		public BigInteger DP
		{
			get
			{
				if (this._privateKey.Length > 5)
				{
					return this._privateKey[5];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00014078 File Offset: 0x00012278
		public BigInteger DQ
		{
			get
			{
				if (this._privateKey.Length > 6)
				{
					return this._privateKey[6];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00014097 File Offset: 0x00012297
		public BigInteger InverseQ
		{
			get
			{
				if (this._privateKey.Length > 7)
				{
					return this._privateKey[7];
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000140B8 File Offset: 0x000122B8
		public override int KeyLength
		{
			get
			{
				return this.Modulus.BitLength;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x000140D3 File Offset: 0x000122D3
		protected override DigitalSignature DigitalSignature
		{
			get
			{
				if (this._digitalSignature == null)
				{
					this._digitalSignature = new RsaDigitalSignature(this);
				}
				return this._digitalSignature;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000140EF File Offset: 0x000122EF
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00014111 File Offset: 0x00012311
		public override BigInteger[] Public
		{
			get
			{
				return new BigInteger[]
				{
					this.Exponent,
					this.Modulus
				};
			}
			set
			{
				if (value.Length != 2)
				{
					throw new InvalidOperationException("Invalid private key.");
				}
				this._privateKey = new BigInteger[]
				{
					value[1],
					value[0]
				};
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001414C File Offset: 0x0001234C
		public RsaKey()
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00014154 File Offset: 0x00012354
		public RsaKey(byte[] data) : base(data)
		{
			if (this._privateKey.Length != 8)
			{
				throw new InvalidOperationException("Invalid private key.");
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014174 File Offset: 0x00012374
		public RsaKey(BigInteger modulus, BigInteger exponent, BigInteger d, BigInteger p, BigInteger q, BigInteger inverseQ)
		{
			this._privateKey = new BigInteger[8];
			this._privateKey[0] = modulus;
			this._privateKey[1] = exponent;
			this._privateKey[2] = d;
			this._privateKey[3] = p;
			this._privateKey[4] = q;
			this._privateKey[5] = RsaKey.PrimeExponent(d, p);
			this._privateKey[6] = RsaKey.PrimeExponent(d, q);
			this._privateKey[7] = inverseQ;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001420C File Offset: 0x0001240C
		private static BigInteger PrimeExponent(BigInteger privateExponent, BigInteger prime)
		{
			BigInteger divisor = prime - new BigInteger(1);
			return privateExponent % divisor;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001422D File Offset: 0x0001242D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001423C File Offset: 0x0001243C
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				RsaDigitalSignature digitalSignature = this._digitalSignature;
				if (digitalSignature != null)
				{
					digitalSignature.Dispose();
					this._digitalSignature = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00014274 File Offset: 0x00012474
		~RsaKey()
		{
			this.Dispose(false);
		}

		// Token: 0x04000244 RID: 580
		private RsaDigitalSignature _digitalSignature;

		// Token: 0x04000245 RID: 581
		private bool _isDisposed;
	}
}
