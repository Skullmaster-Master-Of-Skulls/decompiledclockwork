using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000074 RID: 116
	[__DynamicallyInvokable]
	public interface IXmlNamespaceResolver
	{
		// Token: 0x060003DD RID: 989
		[__DynamicallyInvokable]
		IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope);

		// Token: 0x060003DE RID: 990
		[__DynamicallyInvokable]
		string LookupNamespace(string prefix);

		// Token: 0x060003DF RID: 991
		[__DynamicallyInvokable]
		string LookupPrefix(string namespaceName);
	}
}
