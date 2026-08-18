using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020000A6 RID: 166
	internal interface IDtdAttributeListInfo
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005C8 RID: 1480
		string Prefix { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005C9 RID: 1481
		string LocalName { get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005CA RID: 1482
		bool HasNonCDataAttributes { get; }

		// Token: 0x060005CB RID: 1483
		IDtdAttributeInfo LookupAttribute(string prefix, string localName);

		// Token: 0x060005CC RID: 1484
		IEnumerable<IDtdDefaultAttributeInfo> LookupDefaultAttributes();

		// Token: 0x060005CD RID: 1485
		IDtdAttributeInfo LookupIdAttribute();
	}
}
