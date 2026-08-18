using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000062 RID: 98
	internal class DotNetSecurityDictionary
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000D762 File Offset: 0x0000B962
		public DotNetSecurityDictionary(ServiceModelDictionary dictionary)
		{
			this.Namespace = dictionary.CreateString("http://schemas.microsoft.com/ws/2006/05/security", 162);
			this.Prefix = dictionary.CreateString("dnse", 163);
		}

		// Token: 0x04000544 RID: 1348
		public XmlDictionaryString Namespace;

		// Token: 0x04000545 RID: 1349
		public XmlDictionaryString Prefix;
	}
}
