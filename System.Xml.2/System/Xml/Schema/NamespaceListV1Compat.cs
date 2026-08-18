using System;

namespace System.Xml.Schema
{
	// Token: 0x02000251 RID: 593
	internal class NamespaceListV1Compat : NamespaceList
	{
		// Token: 0x0600231E RID: 8990 RVA: 0x000BA6C9 File Offset: 0x000B88C9
		public NamespaceListV1Compat(string namespaces, string targetNamespace) : base(namespaces, targetNamespace)
		{
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000BA6D3 File Offset: 0x000B88D3
		public override bool Allows(string ns)
		{
			if (base.Type == NamespaceList.ListType.Other)
			{
				return ns != base.Excluded;
			}
			return base.Allows(ns);
		}
	}
}
