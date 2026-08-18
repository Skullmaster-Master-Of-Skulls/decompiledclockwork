using System;
using System.Xml;

namespace System.Data.Design
{
	// Token: 0x0200024A RID: 586
	internal interface IDataSourceXmlSerializable
	{
		// Token: 0x060016AA RID: 5802
		void ReadXml(XmlElement xmlElement, DataSourceXmlSerializer serializer);

		// Token: 0x060016AB RID: 5803
		void WriteXml(XmlWriter writer, DataSourceXmlSerializer serializer);
	}
}
