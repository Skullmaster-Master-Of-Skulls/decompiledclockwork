using System;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer
{
	// Token: 0x0200002C RID: 44
	internal sealed class SvcMapFileImplSerializer : XmlSerializer1
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000B4E7 File Offset: 0x000096E7
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("ReferenceGroup", "urn:schemas-microsoft-com:xml-wcfservicemap");
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B4F9 File Offset: 0x000096F9
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterSvcMapFileImpl)writer).Write16_ReferenceGroup(objectToSerialize);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B507 File Offset: 0x00009707
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderSvcMapFileImpl)reader).Read16_ReferenceGroup();
		}
	}
}
