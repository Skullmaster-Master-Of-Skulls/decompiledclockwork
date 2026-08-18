using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000359 RID: 857
	public class CmsCompressedDataGenerator
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000B9BC8 File Offset: 0x000B8BC8
		public CmsCompressedData Generate(CmsProcessable content, string compressionOid)
		{
			AlgorithmIdentifier compressionAlgorithm;
			Asn1OctetString content2;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				ZDeflaterOutputStream zdeflaterOutputStream = new ZDeflaterOutputStream(memoryStream);
				content.Write(zdeflaterOutputStream);
				zdeflaterOutputStream.Close();
				compressionAlgorithm = new AlgorithmIdentifier(new DerObjectIdentifier(compressionOid));
				content2 = new BerOctetString(memoryStream.ToArray());
			}
			catch (IOException e)
			{
				throw new CmsException("exception encoding data.", e);
			}
			ContentInfo encapContentInfo = new ContentInfo(CmsObjectIdentifiers.Data, content2);
			ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.CompressedData, new CompressedData(compressionAlgorithm, encapContentInfo));
			return new CmsCompressedData(contentInfo);
		}

		// Token: 0x04001550 RID: 5456
		public const string ZLib = "1.2.840.113549.1.9.16.3.8";
	}
}
