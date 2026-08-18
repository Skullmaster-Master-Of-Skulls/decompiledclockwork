using System;
using System.Xml;
using iTextSharp.text;

namespace MailBee.Pdf
{
	// Token: 0x02000015 RID: 21
	// (Invoke) Token: 0x060000DA RID: 218
	[CLSCompliant(false)]
	public delegate IElement ConvertXmlNodeToPdfDelegate(XmlNode xmlNode, IElement pdfChunk);
}
