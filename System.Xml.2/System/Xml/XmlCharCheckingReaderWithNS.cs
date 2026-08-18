using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000CC RID: 204
	internal class XmlCharCheckingReaderWithNS : XmlCharCheckingReader, IXmlNamespaceResolver
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x0001A31F File Offset: 0x0001851F
		internal XmlCharCheckingReaderWithNS(XmlReader reader, IXmlNamespaceResolver readerAsNSResolver, bool checkCharacters, bool ignoreWhitespace, bool ignoreComments, bool ignorePis, DtdProcessing dtdProcessing) : base(reader, checkCharacters, ignoreWhitespace, ignoreComments, ignorePis, dtdProcessing)
		{
			this.readerAsNSResolver = readerAsNSResolver;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A338 File Offset: 0x00018538
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsNSResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A346 File Offset: 0x00018546
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsNSResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A354 File Offset: 0x00018554
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsNSResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040002F1 RID: 753
		internal IXmlNamespaceResolver readerAsNSResolver;
	}
}
