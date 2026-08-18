using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FC RID: 2044
	internal class EncodedContentType : EncodedFramingRecord
	{
		// Token: 0x06004D0C RID: 19724 RVA: 0x00119A1D File Offset: 0x00117C1D
		private EncodedContentType(FramingEncodingType encodingType) : base(new byte[]
		{
			3,
			(byte)encodingType
		})
		{
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x00119A34 File Offset: 0x00117C34
		private EncodedContentType(string contentType) : base(FramingRecordType.ExtensibleEncoding, contentType)
		{
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x00119A40 File Offset: 0x00117C40
		public static EncodedContentType Create(string contentType)
		{
			if (contentType == "application/soap+msbinsession1")
			{
				return new EncodedContentType(FramingEncodingType.BinarySession);
			}
			if (contentType == "application/soap+msbin1")
			{
				return new EncodedContentType(FramingEncodingType.Binary);
			}
			if (contentType == "application/soap+xml; charset=utf-8")
			{
				return new EncodedContentType(FramingEncodingType.Soap12Utf8);
			}
			if (contentType == "text/xml; charset=utf-8")
			{
				return new EncodedContentType(FramingEncodingType.Soap11Utf8);
			}
			if (contentType == "application/soap+xml; charset=utf16")
			{
				return new EncodedContentType(FramingEncodingType.Soap12Utf16);
			}
			if (contentType == "text/xml; charset=utf16")
			{
				return new EncodedContentType(FramingEncodingType.Soap11Utf16);
			}
			if (contentType == "application/soap+xml; charset=unicodeFFFE")
			{
				return new EncodedContentType(FramingEncodingType.Soap12Utf16FFFE);
			}
			if (contentType == "text/xml; charset=unicodeFFFE")
			{
				return new EncodedContentType(FramingEncodingType.Soap11Utf16FFFE);
			}
			if (contentType == "multipart/related")
			{
				return new EncodedContentType(FramingEncodingType.MTOM);
			}
			return new EncodedContentType(contentType);
		}
	}
}
