using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200018E RID: 398
	internal interface IExtensibleSyndicationObject
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C6F RID: 3183
		Dictionary<XmlQualifiedName, string> AttributeExtensions { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C70 RID: 3184
		SyndicationElementExtensionCollection ElementExtensions { get; }
	}
}
