using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000065 RID: 101
	internal class XmlTextWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000E146 File Offset: 0x0000C346
		internal XmlTextWriterBase64Encoder(XmlTextEncoder xmlTextEncoder)
		{
			this.xmlTextEncoder = xmlTextEncoder;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000E155 File Offset: 0x0000C355
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.xmlTextEncoder.WriteRaw(chars, index, count);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000E165 File Offset: 0x0000C365
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001A0 RID: 416
		private XmlTextEncoder xmlTextEncoder;
	}
}
