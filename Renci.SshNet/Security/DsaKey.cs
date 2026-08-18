using System;
using Renci.SshNet.Common;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006B RID: 107
	public class DsaKey : Key, IDisposable
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00013DA3 File Offset: 0x00011FA3
		public BigInteger P
		{
			get
			{
				return this._privateKey[0];
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00013DB1 File Offset: 0x00011FB1
		public BigInteger Q
		{
			get
			{
				return this._privateKey[1];
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00013DBF File Offset: 0x00011FBF
		public BigInteger G
		{
			get
			{
				return this._privateKey[2];
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00013DCD File Offset: 0x00011FCD
		public BigInteger Y
		{
			get
			{
				return this._privateKey[3];
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00013DDB File Offset: 0x00011FDB
		public BigInteger X
		{
			get
			{
				return this._privateKey[4];
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00013DEC File Offset: 0x00011FEC
		public override int KeyLength
		{
			get
			{
				return this.P.BitLength;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00013E07 File Offset: 0x00012007
		protected override DigitalSignature DigitalSignature
		{
			get
			{
				if (this._digitalSignature == null)
				{
					this._digitalSignature = new DsaDigitalSignature(this);
				}
				return this._digitalSignature;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00013E23 File Offset: 0x00012023
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00013E5F File Offset: 0x0001205F
		public override BigInteger[] Public
		{
			get
			{
				return new BigInteger[]
				{
					this.P,
					this.Q,
					this.G,
					this.Y
				};
			}
			set
			{
				if (value.Length != 4)
				{
					throw new InvalidOperationException("Invalid public key.");
				}
				this._privateKey = value;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00013E79 File Offset: 0x00012079
		public DsaKey()
		{
			this._privateKey = new BigInteger[5];
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00013E8D File Offset: 0x0001208D
		public DsaKey(byte[] data) : base(data)
		{
			if (this._privateKey.Length != 5)
			{
				throw new InvalidOperationException("Invalid private key.");
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00013EAC File Offset: 0x000120AC
		public DsaKey(BigInteger p, BigInteger q, BigInteger g, BigInteger y, BigInteger x)
		{
			this._privateKey = new BigInteger[5];
			this._privateKey[0] = p;
			this._privateKey[1] = q;
			this._privateKey[2] = g;
			this._privateKey[3] = y;
			this._privateKey[4] = x;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00013F0E File Offset: 0x0001210E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00013F20 File Offset: 0x00012120
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				DsaDigitalSignature digitalSignature = this._digitalSignature;
				if (digitalSignature != null)
				{
					digitalSignature.Dispose();
					this._digitalSignature = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00013F58 File Offset: 0x00012158
		~DsaKey()
		{
			this.Dispose(false);
		}

		// Token: 0x04000241 RID: 577
		private DsaDigitalSignature _digitalSignature;

		// Token: 0x04000242 RID: 578
		private bool _isDisposed;
	}
}
