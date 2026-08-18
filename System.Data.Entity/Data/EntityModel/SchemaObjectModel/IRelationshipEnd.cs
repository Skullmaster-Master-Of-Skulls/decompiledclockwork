using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F4 RID: 756
	internal interface IRelationshipEnd
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002D26 RID: 11558
		string Name { get; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002D27 RID: 11559
		SchemaEntityType Type { get; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002D28 RID: 11560
		// (set) Token: 0x06002D29 RID: 11561
		RelationshipMultiplicity? Multiplicity { get; set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002D2A RID: 11562
		ICollection<OnOperation> Operations { get; }
	}
}
