using System;
using System.Security.Cryptography;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Security.Cryptography.Ciphers;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x02000081 RID: 129
	public class RsaDigitalSignature : CipherDigitalSignature, IDisposable
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00015848 File Offset: 0x00013A48
		public RsaDigitalSignature(RsaKey rsaKey) : base(new ObjectIdentifier(new ulong[]
		{
			1UL,
			3UL,
			14UL,
			3UL,
			2UL,
			26UL
		}), new RsaCipher(rsaKey))
		{
			this._hash = CryptoAbstraction.CreateSHA1();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00015877 File Offset: 0x00013A77
		protected override byte[] Hash(byte[] input)
		{
			return this._hash.ComputeHash(input);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00015885 File Offset: 0x00013A85
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00015894 File Offset: 0x00013A94
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

		// Token: 0x060006F1 RID: 1777 RVA: 0x000158CC File Offset: 0x00013ACC
		~RsaDigitalSignature()
		{
			this.Dispose(false);
		}

		// Token: 0x0400026A RID: 618
		private HashAlgorithm _hash;

		// Token: 0x0400026B RID: 619
		private bool _isDisposed;
	}
}
