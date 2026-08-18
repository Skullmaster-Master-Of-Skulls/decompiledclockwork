using System;
using System.Globalization;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007EF RID: 2031
	internal class ContentTypeStringDecoder : StringDecoder
	{
		// Token: 0x06004CC0 RID: 19648 RVA: 0x0011827F File Offset: 0x0011647F
		public ContentTypeStringDecoder(int sizeQuota) : base(sizeQuota)
		{
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x00118288 File Offset: 0x00116488
		protected override Exception OnSizeQuotaExceeded(int size)
		{
			Exception ex = new InvalidDataException(SR.GetString("FramingContentTypeTooLong", new object[]
			{
				size
			}));
			FramingEncodingString.AddFaultString(ex, "http://schemas.microsoft.com/ws/2006/05/framing/faults/ContentTypeTooLong");
			return ex;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x001182C0 File Offset: 0x001164C0
		public static string GetString(FramingEncodingType type)
		{
			switch (type)
			{
			case FramingEncodingType.Soap11Utf8:
				return "text/xml; charset=utf-8";
			case FramingEncodingType.Soap11Utf16:
				return "text/xml; charset=utf16";
			case FramingEncodingType.Soap11Utf16FFFE:
				return "text/xml; charset=unicodeFFFE";
			case FramingEncodingType.Soap12Utf8:
				return "application/soap+xml; charset=utf-8";
			case FramingEncodingType.Soap12Utf16:
				return "application/soap+xml; charset=utf16";
			case FramingEncodingType.Soap12Utf16FFFE:
				return "application/soap+xml; charset=unicodeFFFE";
			case FramingEncodingType.MTOM:
				return "multipart/related";
			case FramingEncodingType.Binary:
				return "application/soap+msbin1";
			case FramingEncodingType.BinarySession:
				return "application/soap+msbinsession1";
			default:
			{
				string str = "unknown";
				int num = (int)type;
				return str + num.ToString(CultureInfo.InvariantCulture);
			}
			}
		}
	}
}
