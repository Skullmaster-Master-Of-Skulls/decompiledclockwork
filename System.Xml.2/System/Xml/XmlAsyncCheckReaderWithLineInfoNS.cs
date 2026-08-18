using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000C7 RID: 199
	internal class XmlAsyncCheckReaderWithLineInfoNS : XmlAsyncCheckReaderWithLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x000188CE File Offset: 0x00016ACE
		public XmlAsyncCheckReaderWithLineInfoNS(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000188E3 File Offset: 0x00016AE3
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000188F1 File Offset: 0x00016AF1
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000188FF File Offset: 0x00016AFF
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040002DE RID: 734
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
