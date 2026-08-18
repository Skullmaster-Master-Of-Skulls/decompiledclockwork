using System;
using System.IO;
using Org.BouncyCastle.Apache.Bzip2;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000180 RID: 384
	public class PgpCompressedData : PgpObject
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00056CD8 File Offset: 0x00055CD8
		public PgpCompressedData(BcpgInputStream bcpgInput)
		{
			this.data = (CompressedDataPacket)bcpgInput.ReadPacket();
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00056CF1 File Offset: 0x00055CF1
		public CompressionAlgorithmTag Algorithm
		{
			get
			{
				return this.data.Algorithm;
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00056CFE File Offset: 0x00055CFE
		public Stream GetInputStream()
		{
			return this.data.GetInputStream();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00056D0C File Offset: 0x00055D0C
		public Stream GetDataStream()
		{
			switch (this.Algorithm)
			{
			case CompressionAlgorithmTag.Uncompressed:
				return this.GetInputStream();
			case CompressionAlgorithmTag.Zip:
				return new ZInflaterInputStream(this.GetInputStream(), true);
			case CompressionAlgorithmTag.ZLib:
				return new ZInflaterInputStream(this.GetInputStream());
			case CompressionAlgorithmTag.BZip2:
				return new CBZip2InputStream(this.GetInputStream());
			default:
				throw new PgpException("can't recognise compression algorithm: " + this.Algorithm);
			}
		}

		// Token: 0x04000B07 RID: 2823
		private readonly CompressedDataPacket data;
	}
}
