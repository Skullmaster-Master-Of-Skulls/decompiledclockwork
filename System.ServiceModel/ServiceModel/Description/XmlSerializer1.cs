using System;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003EC RID: 1004
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060025D0 RID: 9680 RVA: 0x000891AC File Offset: 0x000873AC
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderMetadataSet();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000891B3 File Offset: 0x000873B3
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterMetadataSet();
		}
	}
}
