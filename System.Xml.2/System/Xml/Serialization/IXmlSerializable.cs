using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000144 RID: 324
	[__DynamicallyInvokable]
	public interface IXmlSerializable
	{
		// Token: 0x0600171B RID: 5915
		[__DynamicallyInvokable]
		XmlSchema GetSchema();

		// Token: 0x0600171C RID: 5916
		[__DynamicallyInvokable]
		void ReadXml(XmlReader reader);

		// Token: 0x0600171D RID: 5917
		[__DynamicallyInvokable]
		void WriteXml(XmlWriter writer);
	}
}
