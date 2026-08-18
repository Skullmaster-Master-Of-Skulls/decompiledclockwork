using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BD RID: 701
	public interface IXmlSerializable
	{
		// Token: 0x06002171 RID: 8561
		XmlSchema GetSchema();

		// Token: 0x06002172 RID: 8562
		void ReadXml(XmlReader reader);

		// Token: 0x06002173 RID: 8563
		void WriteXml(XmlWriter writer);
	}
}
