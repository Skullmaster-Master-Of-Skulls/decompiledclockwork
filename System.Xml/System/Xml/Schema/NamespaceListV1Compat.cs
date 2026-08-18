using System;

namespace System.Xml.Schema
{
	// Token: 0x02000204 RID: 516
	internal class NamespaceListV1Compat : NamespaceList
	{
		// Token: 0x0600186F RID: 6255 RVA: 0x0006D611 File Offset: 0x0006C611
		public NamespaceListV1Compat(string namespaces, string targetNamespace) : base(namespaces, targetNamespace)
		{
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006D61B File Offset: 0x0006C61B
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
