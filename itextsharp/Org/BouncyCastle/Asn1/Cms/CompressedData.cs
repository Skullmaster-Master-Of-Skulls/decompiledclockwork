using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000157 RID: 343
	public class CompressedData : Asn1Encodable
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x000439B0 File Offset: 0x000429B0
		public CompressedData(AlgorithmIdentifier compressionAlgorithm, ContentInfo encapContentInfo)
		{
			this.version = new DerInteger(0);
			this.compressionAlgorithm = compressionAlgorithm;
			this.encapContentInfo = encapContentInfo;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000439D2 File Offset: 0x000429D2
		public CompressedData(Asn1Sequence seq)
		{
			this.version = (DerInteger)seq[0];
			this.compressionAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.encapContentInfo = ContentInfo.GetInstance(seq[2]);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00043A10 File Offset: 0x00042A10
		public static CompressedData GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return CompressedData.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00043A20 File Offset: 0x00042A20
		public static CompressedData GetInstance(object obj)
		{
			if (obj == null || obj is CompressedData)
			{
				return (CompressedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new CompressedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid CompressedData: " + obj.GetType().Name);
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00043A6D File Offset: 0x00042A6D
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00043A75 File Offset: 0x00042A75
		public AlgorithmIdentifier CompressionAlgorithmIdentifier
		{
			get
			{
				return this.compressionAlgorithm;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00043A7D File Offset: 0x00042A7D
		public ContentInfo EncapContentInfo
		{
			get
			{
				return this.encapContentInfo;
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00043A88 File Offset: 0x00042A88
		public override Asn1Object ToAsn1Object()
		{
			return new BerSequence(new Asn1Encodable[]
			{
				this.version,
				this.compressionAlgorithm,
				this.encapContentInfo
			});
		}

		// Token: 0x04000994 RID: 2452
		private DerInteger version;

		// Token: 0x04000995 RID: 2453
		private AlgorithmIdentifier compressionAlgorithm;

		// Token: 0x04000996 RID: 2454
		private ContentInfo encapContentInfo;
	}
}
