using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000244 RID: 580
	internal class EncodedStreamFactory
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00071E70 File Offset: 0x00070070
		internal static int DefaultMaxLineLength
		{
			get
			{
				return 70;
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00071E74 File Offset: 0x00070074
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException("Encoding Stream");
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00071EAC File Offset: 0x000700AC
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] header = this.CreateHeader(encoding, useBase64Encoding);
			byte[] footer = this.CreateFooter();
			WriteStateInfoBase writeStateInfoBase;
			if (useBase64Encoding)
			{
				writeStateInfoBase = new Base64WriteStateInfo(1024, header, footer, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength);
				return new Base64Stream((Base64WriteStateInfo)writeStateInfoBase);
			}
			writeStateInfoBase = new WriteStateInfoBase(1024, header, footer, EncodedStreamFactory.DefaultMaxLineLength, headerTextLength);
			return new QEncodedStream(writeStateInfoBase);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00071F04 File Offset: 0x00070104
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			string s = string.Format("=?{0}?{1}?", encoding.HeaderName, useBase64Encoding ? "B" : "Q");
			return Encoding.ASCII.GetBytes(s);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00071F3C File Offset: 0x0007013C
		protected byte[] CreateFooter()
		{
			return new byte[]
			{
				63,
				61
			};
		}

		// Token: 0x04001710 RID: 5904
		private const int defaultMaxLineLength = 70;

		// Token: 0x04001711 RID: 5905
		private const int initialBufferSize = 1024;
	}
}
