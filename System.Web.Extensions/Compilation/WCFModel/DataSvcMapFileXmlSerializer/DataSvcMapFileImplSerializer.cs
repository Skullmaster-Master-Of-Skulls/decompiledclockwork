using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer
{
	// Token: 0x02000031 RID: 49
	internal sealed class DataSvcMapFileImplSerializer : XmlSerializer1
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000CF64 File Offset: 0x0000B164
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("ReferenceGroup", "urn:schemas-microsoft-com:xml-dataservicemap");
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CF76 File Offset: 0x0000B176
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterDataSvcMapFileImpl)writer).Write9_ReferenceGroup(objectToSerialize);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CF84 File Offset: 0x0000B184
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderDataSvcMapFileImpl)reader).Read9_ReferenceGroup();
		}
	}
}
