using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer
{
	// Token: 0x02000030 RID: 48
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000CF56 File Offset: 0x0000B156
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderDataSvcMapFileImpl();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CF5D File Offset: 0x0000B15D
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterDataSvcMapFileImpl();
		}
	}
}
