using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer
{
	// Token: 0x0200002B RID: 43
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000B4D1 File Offset: 0x000096D1
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderSvcMapFileImpl();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B4D8 File Offset: 0x000096D8
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterSvcMapFileImpl();
		}
	}
}
