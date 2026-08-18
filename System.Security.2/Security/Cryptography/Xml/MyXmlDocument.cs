using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000064 RID: 100
	internal class MyXmlDocument : XmlDocument
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00011E22 File Offset: 0x00010022
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return this.CreateAttribute(prefix, localName, namespaceURI);
		}
	}
}
