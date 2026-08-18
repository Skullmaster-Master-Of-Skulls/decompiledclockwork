using System;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	internal class RelationshipFixer<TSourceEntity, TTargetEntity> : IRelationshipFixer where TSourceEntity : class where TTargetEntity : class
	{
		// Token: 0x06001D88 RID: 7560 RVA: 0x0006655F File Offset: 0x0006475F
		internal RelationshipFixer(RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity)
		{
			this._sourceRoleMultiplicity = sourceRoleMultiplicity;
			this._targetRoleMultiplicity = targetRoleMultiplicity;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00066575 File Offset: 0x00064775
		RelatedEnd IRelationshipFixer.CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager)
		{
			return relationshipManager.CreateRelatedEnd<TTargetEntity, TSourceEntity>(navigation, this._targetRoleMultiplicity, this._sourceRoleMultiplicity, null);
		}

		// Token: 0x04000BD0 RID: 3024
		private RelationshipMultiplicity _sourceRoleMultiplicity;

		// Token: 0x04000BD1 RID: 3025
		private RelationshipMultiplicity _targetRoleMultiplicity;
	}
}
