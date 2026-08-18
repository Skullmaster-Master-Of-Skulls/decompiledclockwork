using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000072 RID: 114
	internal class XmlCharCheckingReaderWithNS : XmlCharCheckingReader, IXmlNamespaceResolver
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0001503D File Offset: 0x0001403D
		internal XmlCharCheckingReaderWithNS(XmlReader reader, IXmlNamespaceResolver readerAsNSResolver, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, bool prohibitDtd) : base(reader, checkCharacters, ignoreWhitespace, ignoreComments, ignorePis, prohibitDtd)
		{
			this.readerAsNSResolver = readerAsNSResolver;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00015056 File Offset: 0x00014056
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015064 File Offset: 0x00014064
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015072 File Offset: 0x00014072
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040005F6 RID: 1526
		internal IXmlNamespaceResolver readerAsNSResolver;
	}
}
