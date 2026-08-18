using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000269 RID: 617
	public class CompressedDataParser
	{
		// Token: 0x0600173C RID: 5948 RVA: 0x00085C04 File Offset: 0x00084C04
		public CompressedDataParser(Asn1SequenceParser seq)
		{
			this._version = (DerInteger)seq.ReadObject();
			this._compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq.ReadObject().ToAsn1Object());
			this._encapContentInfo = new ContentInfoParser((Asn1SequenceParser)seq.ReadObject());
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00085C54 File Offset: 0x00084C54
		public DerInteger Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00085C5C File Offset: 0x00084C5C
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this._compressionAlgorithm;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00085C64 File Offset: 0x00084C64
		public ContentInfoParser GetEncapContentInfo()
		{
			return this._encapContentInfo;
		}

		// Token: 0x04000FF0 RID: 4080
		private DerInteger _version;

		// Token: 0x04000FF1 RID: 4081
		private AlgorithmIdentifier _compressionAlgorithm;

		// Token: 0x04000FF2 RID: 4082
		private ContentInfoParser _encapContentInfo;
	}
}
