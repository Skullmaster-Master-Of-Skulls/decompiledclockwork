using System;
using System.IO;
using Org.BouncyCastle.Apache.Bzip2;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200060B RID: 1547
	public class PgpCompressedDataGenerator : IStreamGenerator
	{
		// Token: 0x060034AA RID: 13482 RVA: 0x00147B6F File Offset: 0x00146B6F
		public PgpCompressedDataGenerator(CompressionAlgorithmTag algorithm) : this(algorithm, -1)
		{
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x00147B7C File Offset: 0x00146B7C
		public PgpCompressedDataGenerator(CompressionAlgorithmTag algorithm, int compression)
		{
			switch (algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
			case CompressionAlgorithmTag.Zip:
			case CompressionAlgorithmTag.ZLib:
			case CompressionAlgorithmTag.BZip2:
				if (compression != -1 && (compression < 0 || compression > 9))
				{
					throw new ArgumentException("unknown compression level: " + compression);
				}
				this.algorithm = algorithm;
				this.compression = compression;
				return;
			default:
				throw new ArgumentException("unknown compression algorithm", "algorithm");
			}
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x00147BE8 File Offset: 0x00146BE8
		public Stream Open(Stream outStr)
		{
			if (this.dOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.CompressedData);
			this.doOpen();
			return new WrappedGeneratorStream(this, this.dOut);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00147C38 File Offset: 0x00146C38
		public Stream Open(Stream outStr, byte[] buffer)
		{
			if (this.dOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.pkOut = new BcpgOutputStream(outStr, PacketTag.CompressedData, buffer);
			this.doOpen();
			return new WrappedGeneratorStream(this, this.dOut);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00147C94 File Offset: 0x00146C94
		private void doOpen()
		{
			this.pkOut.WriteByte((byte)this.algorithm);
			switch (this.algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
				this.dOut = this.pkOut;
				return;
			case CompressionAlgorithmTag.Zip:
				this.dOut = new ZDeflaterOutputStream(this.pkOut, this.compression, true);
				return;
			case CompressionAlgorithmTag.ZLib:
				this.dOut = new ZDeflaterOutputStream(this.pkOut, this.compression, false);
				return;
			case CompressionAlgorithmTag.BZip2:
				this.dOut = new CBZip2OutputStream(this.pkOut);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x00147D28 File Offset: 0x00146D28
		public void Close()
		{
			if (this.dOut != null)
			{
				switch (this.algorithm)
				{
				case CompressionAlgorithmTag.Zip:
				case CompressionAlgorithmTag.ZLib:
					((ZDeflaterOutputStream)this.dOut).Finish();
					break;
				case CompressionAlgorithmTag.BZip2:
					((CBZip2OutputStream)this.dOut).Finish();
					break;
				}
				this.dOut.Flush();
				this.pkOut.Finish();
				this.pkOut.Flush();
				this.dOut = null;
				this.pkOut = null;
			}
		}

		// Token: 0x04002366 RID: 9062
		private readonly CompressionAlgorithmTag algorithm;

		// Token: 0x04002367 RID: 9063
		private readonly int compression;

		// Token: 0x04002368 RID: 9064
		private Stream dOut;

		// Token: 0x04002369 RID: 9065
		private BcpgOutputStream pkOut;
	}
}
