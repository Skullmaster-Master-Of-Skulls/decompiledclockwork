using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000194 RID: 404
	internal interface IRelationshipFixer
	{
		// Token: 0x06001D04 RID: 7428
		RelatedEnd CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager);
	}
}
