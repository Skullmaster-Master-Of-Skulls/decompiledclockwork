using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002F3 RID: 755
	internal interface IRelationship
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002D1F RID: 11551
		string Name { get; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002D20 RID: 11552
		string FQName { get; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002D21 RID: 11553
		IList<IRelationshipEnd> Ends { get; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002D22 RID: 11554
		IList<ReferentialConstraint> Constraints { get; }

		// Token: 0x06002D23 RID: 11555
		bool TryGetEnd(string roleName, out IRelationshipEnd end);

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002D24 RID: 11556
		RelationshipKind RelationshipKind { get; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002D25 RID: 11557
		bool IsForeignKey { get; }
	}
}
