using System;

namespace System.Xml.Linq
{
	// Token: 0x0200001E RID: 30
	internal struct NamespaceCache
	{
		// Token: 0x0600011C RID: 284 RVA: 0x000067DD File Offset: 0x000049DD
		public XNamespace Get(string namespaceName)
		{
			if (namespaceName == this.namespaceName)
			{
				return this.ns;
			}
			this.namespaceName = namespaceName;
			this.ns = XNamespace.Get(namespaceName);
			return this.ns;
		}

		// Token: 0x0400008B RID: 139
		private XNamespace ns;

		// Token: 0x0400008C RID: 140
		private string namespaceName;
	}
}
