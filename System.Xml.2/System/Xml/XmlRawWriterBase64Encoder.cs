using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000064 RID: 100
	internal class XmlRawWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000E117 File Offset: 0x0000C317
		internal XmlRawWriterBase64Encoder(XmlRawWriter rawWriter)
		{
			this.rawWriter = rawWriter;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E126 File Offset: 0x0000C326
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.rawWriter.WriteRaw(chars, index, count);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E136 File Offset: 0x0000C336
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			return this.rawWriter.WriteRawAsync(chars, index, count);
		}

		// Token: 0x0400019F RID: 415
		private XmlRawWriter rawWriter;
	}
}
