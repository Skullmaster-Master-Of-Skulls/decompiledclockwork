using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000545 RID: 1349
	internal interface IRelationshipFixer
	{
		// Token: 0x06003429 RID: 13353
		RelatedEnd CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager);
	}
}
