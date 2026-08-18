using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities.Zlib;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200047C RID: 1148
	public class CmsCompressedData
	{
		// Token: 0x06002705 RID: 9989 RVA: 0x000EC737 File Offset: 0x000EB737
		public CmsCompressedData(byte[] compressedData) : this(CmsUtilities.ReadContentInfo(compressedData))
		{
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000EC745 File Offset: 0x000EB745
		public CmsCompressedData(Stream compressedDataStream) : this(CmsUtilities.ReadContentInfo(compressedDataStream))
		{
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000EC753 File Offset: 0x000EB753
		public CmsCompressedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000EC764 File Offset: 0x000EB764
		public byte[] GetContent()
		{
			CompressedData instance = CompressedData.GetInstance(this.contentInfo.Content);
			ContentInfo encapContentInfo = instance.EncapContentInfo;
			Asn1OctetString asn1OctetString = (Asn1OctetString)encapContentInfo.Content;
			ZInflaterInputStream zinflaterInputStream = new ZInflaterInputStream(asn1OctetString.GetOctetStream());
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(zinflaterInputStream);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			finally
			{
				zinflaterInputStream.Close();
			}
			return result;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000EC7E0 File Offset: 0x000EB7E0
		public byte[] GetContent(int limit)
		{
			CompressedData instance = CompressedData.GetInstance(this.contentInfo.Content);
			ContentInfo encapContentInfo = instance.EncapContentInfo;
			Asn1OctetString asn1OctetString = (Asn1OctetString)encapContentInfo.Content;
			ZInflaterInputStream inStream = new ZInflaterInputStream(new MemoryStream(asn1OctetString.GetOctets(), false));
			byte[] result;
			try
			{
				result = CmsUtilities.StreamToByteArray(inStream, limit);
			}
			catch (IOException e)
			{
				throw new CmsException("exception reading compressed stream.", e);
			}
			return result;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000EC850 File Offset: 0x000EB850
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000EC858 File Offset: 0x000EB858
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04001ACB RID: 6859
		internal ContentInfo contentInfo;
	}
}
