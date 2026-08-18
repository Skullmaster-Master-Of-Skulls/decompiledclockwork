using System;
using System.Security.Cryptography;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000080 RID: 128
	public class DsaDigitalSignature : DigitalSignature, IDisposable
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00015499 File Offset: 0x00013699
		public DsaDigitalSignature(DsaKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._key = key;
			this._hash = CryptoAbstraction.CreateSHA1();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000154C4 File Offset: 0x000136C4
		public override bool Verify(byte[] input, byte[] signature)
		{
			byte[] array = this._hash.ComputeHash(input);
			BigInteger left = new BigInteger(array.Reverse<byte>().Concat(new byte[1]));
			if (signature.Length != 40)
			{
				throw new InvalidOperationException("Invalid signature.");
			}
			byte[] array2 = new byte[21];
			byte[] array3 = new byte[21];
			int i = 0;
			int num = 20;
			while (i < 20)
			{
				array2[i] = signature[num - 1];
				array3[i] = signature[num + 20 - 1];
				i++;
				num--;
			}
			BigInteger bigInteger = new BigInteger(array2);
			BigInteger bigInteger2 = new BigInteger(array3);
			if (bigInteger <= 0L || bigInteger >= this._key.Q)
			{
				return false;
			}
			if (bigInteger2 <= 0L || bigInteger2 >= this._key.Q)
			{
				return false;
			}
			BigInteger right = BigInteger.ModInverse(bigInteger2, this._key.Q);
			BigInteger bigInteger3 = left * right % this._key.Q;
			BigInteger bigInteger4 = bigInteger * right % this._key.Q;
			bigInteger3 = BigInteger.ModPow(this._key.G, bigInteger3, this._key.P);
			bigInteger4 = BigInteger.ModPow(this._key.Y, bigInteger4, this._key.P);
			return bigInteger3 * bigInteger4 % this._key.P % this._key.Q == bigInteger;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015654 File Offset: 0x00013854
		public override byte[] Sign(byte[] input)
		{
			byte[] array = this._hash.ComputeHash(input);
			BigInteger left = new BigInteger(array.Reverse<byte>().Concat(new byte[1]));
			BigInteger right;
			BigInteger bigInteger2;
			for (;;)
			{
				BigInteger bigInteger = BigInteger.Zero;
				do
				{
					int bitLength = this._key.Q.BitLength;
					if (this._key.Q < BigInteger.Zero)
					{
						goto Block_1;
					}
					while (bigInteger <= 0L || bigInteger >= this._key.Q)
					{
						bigInteger = BigInteger.Random(bitLength);
					}
					right = BigInteger.ModPow(this._key.G, bigInteger, this._key.P) % this._key.Q;
				}
				while (right.IsZero);
				bigInteger = BigInteger.ModInverse(bigInteger, this._key.Q) * (left + this._key.X * right);
				bigInteger2 = bigInteger % this._key.Q;
				if (!bigInteger2.IsZero)
				{
					goto Block_5;
				}
			}
			Block_1:
			throw new SshException("Invalid DSA key.");
			Block_5:
			byte[] array2 = new byte[40];
			byte[] array3 = right.ToByteArray().Reverse<byte>().TrimLeadingZeros();
			Array.Copy(array3, 0, array2, 20 - array3.Length, array3.Length);
			byte[] array4 = bigInteger2.ToByteArray().Reverse<byte>().TrimLeadingZeros();
			Array.Copy(array4, 0, array2, 40 - array4.Length, array4.Length);
			return array2;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000157D1 File Offset: 0x000139D1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000157E0 File Offset: 0x000139E0
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				HashAlgorithm hash = this._hash;
				if (hash != null)
				{
					hash.Dispose();
					this._hash = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00015818 File Offset: 0x00013A18
		~DsaDigitalSignature()
		{
			this.Dispose(false);
		}

		// Token: 0x04000267 RID: 615
		private HashAlgorithm _hash;

		// Token: 0x04000268 RID: 616
		private readonly DsaKey _key;

		// Token: 0x04000269 RID: 617
		private bool _isDisposed;
	}
}
