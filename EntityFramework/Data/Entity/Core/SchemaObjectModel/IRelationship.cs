using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200036B RID: 875
	internal interface IRelationship
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001F65 RID: 8037
		string Name { get; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001F66 RID: 8038
		string FQName { get; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001F67 RID: 8039
		IList<IRelationshipEnd> Ends { get; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001F68 RID: 8040
		IList<ReferentialConstraint> Constraints { get; }

		// Token: 0x06001F69 RID: 8041
		bool TryGetEnd(string roleName, out IRelationshipEnd end);

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001F6A RID: 8042
		RelationshipKind RelationshipKind { get; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001F6B RID: 8043
		bool IsForeignKey { get; }
	}
}
