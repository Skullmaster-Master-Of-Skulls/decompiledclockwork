using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core
{
	// Token: 0x020003A2 RID: 930
	internal interface IEntityStateManager
	{
		// Token: 0x060021A4 RID: 8612
		IEnumerable<IEntityStateEntry> GetEntityStateEntries(EntityState state);

		// Token: 0x060021A5 RID: 8613
		IEnumerable<IEntityStateEntry> FindRelationshipsByKey(EntityKey key);

		// Token: 0x060021A6 RID: 8614
		IEntityStateEntry GetEntityStateEntry(EntityKey key);

		// Token: 0x060021A7 RID: 8615
		bool TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry stateEntry);

		// Token: 0x060021A8 RID: 8616
		bool TryGetReferenceKey(EntityKey dependentKey, AssociationEndMember principalRole, out EntityKey principalKey);
	}
}
