using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data
{
	// Token: 0x0200001B RID: 27
	internal interface IEntityStateManager
	{
		// Token: 0x060001FA RID: 506
		IEnumerable<IEntityStateEntry> GetEntityStateEntries(EntityState state);

		// Token: 0x060001FB RID: 507
		IEnumerable<IEntityStateEntry> FindRelationshipsByKey(EntityKey key);

		// Token: 0x060001FC RID: 508
		IEntityStateEntry GetEntityStateEntry(EntityKey key);

		// Token: 0x060001FD RID: 509
		bool TryGetEntityStateEntry(EntityKey key, out IEntityStateEntry stateEntry);

		// Token: 0x060001FE RID: 510
		bool TryGetReferenceKey(EntityKey dependentKey, AssociationEndMember principalRole, out EntityKey principalKey);
	}
}
