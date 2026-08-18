using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200036C RID: 876
	internal interface IRelationshipEnd
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001F6C RID: 8044
		string Name { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001F6D RID: 8045
		SchemaEntityType Type { get; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001F6E RID: 8046
		// (set) Token: 0x06001F6F RID: 8047
		RelationshipMultiplicity? Multiplicity { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001F70 RID: 8048
		ICollection<OnOperation> Operations { get; }
	}
}
