using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000C5 RID: 197
	internal class XmlAsyncCheckReaderWithNS : XmlAsyncCheckReader, IXmlNamespaceResolver
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00018853 File Offset: 0x00016A53
		public XmlAsyncCheckReaderWithNS(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlNamespaceResolver = (IXmlNamespaceResolver)reader;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00018868 File Offset: 0x00016A68
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.readerAsIXmlNamespaceResolver.GetNamespacesInScope(scope);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018876 File Offset: 0x00016A76
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.readerAsIXmlNamespaceResolver.LookupNamespace(prefix);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00018884 File Offset: 0x00016A84
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.readerAsIXmlNamespaceResolver.LookupPrefix(namespaceName);
		}

		// Token: 0x040002DC RID: 732
		private readonly IXmlNamespaceResolver readerAsIXmlNamespaceResolver;
	}
}
