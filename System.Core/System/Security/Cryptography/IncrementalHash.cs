using System;

namespace System.Security.Cryptography
{
	// Token: 0x020000DB RID: 219
	public sealed class IncrementalHash : IDisposable
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00015CE9 File Offset: 0x00013EE9
		private IncrementalHash(HashAlgorithmName name, HashAlgorithm hash)
		{
			this._algorithmName = name;
			this._hash = hash;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00015CFF File Offset: 0x00013EFF
		public HashAlgorithmName AlgorithmName
		{
			get
			{
				return this._algorithmName;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00015D07 File Offset: 0x00013F07
		public void AppendData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.AppendData(data, 0, data.Length);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00015D24 File Offset: 0x00013F24
		public void AppendData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "ArgumentOutOfRange_NeedNonNegNum");
			}
			if (count < 0 || count > data.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (data.Length - count < offset)
			{
				throw new ArgumentException("Argument_InvalidOffLen");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
				this._resetPending = false;
			}
			this._hash.TransformBlock(data, offset, count, null, 0);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public byte[] GetHashAndReset()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
			}
			this._hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] hash = this._hash.Hash;
			this._resetPending = true;
			return hash;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00015E29 File Offset: 0x00014029
		public void Dispose()
		{
			this._disposed = true;
			if (this._hash != null)
			{
				this._hash.Dispose();
				this._hash = null;
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00015E4C File Offset: 0x0001404C
		public static IncrementalHash CreateHash(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("Cryptography_HashAlgorithmNameNullOrEmpty", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHashAlgorithm(hashAlgorithm));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00015E78 File Offset: 0x00014078
		public static IncrementalHash CreateHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("Cryptography_HashAlgorithmNameNullOrEmpty", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHMAC(hashAlgorithm, key));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00015EB4 File Offset: 0x000140B4
		private static HashAlgorithm GetHashAlgorithm(HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new MD5CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new SHA1CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new SHA256CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new SHA384CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new SHA512CryptoServiceProvider();
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00015F2C File Offset: 0x0001412C
		private static HashAlgorithm GetHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new HMACMD5(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(key);
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x040005CD RID: 1485
		private const int NTE_BAD_ALGID = -2146893816;

		// Token: 0x040005CE RID: 1486
		private readonly HashAlgorithmName _algorithmName;

		// Token: 0x040005CF RID: 1487
		private HashAlgorithm _hash;

		// Token: 0x040005D0 RID: 1488
		private bool _disposed;

		// Token: 0x040005D1 RID: 1489
		private bool _resetPending;
	}
}
