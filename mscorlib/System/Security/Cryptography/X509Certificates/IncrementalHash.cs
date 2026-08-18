using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D0 RID: 2256
	internal class IncrementalHash : IDisposable
	{
		// Token: 0x06005256 RID: 21078 RVA: 0x0012763C File Offset: 0x0012663C
		private IncrementalHash(HashAlgorithm algorithm)
		{
			this._algorithm = algorithm;
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0012764C File Offset: 0x0012664C
		public static IncrementalHash CreateHash(HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new IncrementalHash(MD5.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new IncrementalHash(SHA1.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new IncrementalHash(SHA256.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new IncrementalHash(SHA384.Create());
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new IncrementalHash(SHA512.Create());
			}
			throw new CryptographicException();
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x001276D8 File Offset: 0x001266D8
		public void AppendData(ReadOnlySpan<byte> data)
		{
			ArraySegment<byte> arraySegment = data.DangerousGetArraySegment();
			this._algorithm.TransformBlock(arraySegment.Array, arraySegment.Offset, arraySegment.Count, null, 0);
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00127710 File Offset: 0x00126710
		public bool TryGetHashAndReset(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < this._algorithm.HashSize / 8)
			{
				bytesWritten = 0;
				return false;
			}
			this._algorithm.TransformFinalBlock(IncrementalHash.s_Empty, 0, 0);
			byte[] hash = this._algorithm.Hash;
			this._algorithm.Initialize();
			new ReadOnlyMemory<byte>(hash).CopyTo(destination);
			bytesWritten = hash.Length;
			return true;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00127777 File Offset: 0x00126777
		public void Dispose()
		{
			this._algorithm.Clear();
		}

		// Token: 0x04002A59 RID: 10841
		private readonly HashAlgorithm _algorithm;

		// Token: 0x04002A5A RID: 10842
		private static readonly byte[] s_Empty = new byte[0];
	}
}
