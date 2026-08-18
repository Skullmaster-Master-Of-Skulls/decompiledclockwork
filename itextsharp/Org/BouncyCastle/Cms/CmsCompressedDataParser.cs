using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003E3 RID: 995
	public class CmsCompressedDataParser : CmsContentInfoParser
	{
		// Token: 0x0600229C RID: 8860 RVA: 0x000D6AA5 File Offset: 0x000D5AA5
		public CmsCompressedDataParser(byte[] compressedData) : this(new MemoryStream(compressedData, false))
		{
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000D6AB4 File Offset: 0x000D5AB4
		public CmsCompressedDataParser(Stream compressedData) : base(compressedData)
		{
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000D6AC0 File Offset: 0x000D5AC0
		public CmsTypedStream GetContent()
		{
			CmsTypedStream result;
			try
			{
				CompressedDataParser compressedDataParser = new CompressedDataParser((Asn1SequenceParser)this.contentInfo.GetContent(16));
				ContentInfoParser encapContentInfo = compressedDataParser.GetEncapContentInfo();
				Asn1OctetStringParser asn1OctetStringParser = (Asn1OctetStringParser)encapContentInfo.GetContent(4);
				result = new CmsTypedStream(encapContentInfo.ContentType.ToString(), new ZInflaterInputStream(asn1OctetStringParser.GetOctetStream()));
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading compressed content.", e);
			}
			return result;
		}
	}
}
