using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000A5 RID: 165
	internal interface IDtdInfo
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005C1 RID: 1473
		XmlQualifiedName Name { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005C2 RID: 1474
		string InternalDtdSubset { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005C3 RID: 1475
		bool HasDefaultAttributes { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005C4 RID: 1476
		bool HasNonCDataAttributes { get; }

		// Token: 0x060005C5 RID: 1477
		IDtdAttributeListInfo LookupAttributeList(string prefix, string localName);

		// Token: 0x060005C6 RID: 1478
		IEnumerable<IDtdAttributeListInfo> GetAttributeLists();

		// Token: 0x060005C7 RID: 1479
		IDtdEntityInfo LookupEntity(string name);
	}
}
