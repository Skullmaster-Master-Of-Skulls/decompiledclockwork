using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000546 RID: 1350
	[Serializable]
	internal class RelationshipFixer<TSourceEntity, TTargetEntity> : IRelationshipFixer where TSourceEntity : class where TTargetEntity : class
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x000F63BD File Offset: 0x000F45BD
		internal RelationshipFixer(RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity)
		{
			this._sourceRoleMultiplicity = sourceRoleMultiplicity;
			this._targetRoleMultiplicity = targetRoleMultiplicity;
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000F63D3 File Offset: 0x000F45D3
		RelatedEnd IRelationshipFixer.CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager)
		{
			return relationshipManager.CreateRelatedEnd<TTargetEntity, TSourceEntity>(navigation, this._targetRoleMultiplicity, this._sourceRoleMultiplicity, null);
		}

		// Token: 0x04001399 RID: 5017
		private readonly RelationshipMultiplicity _sourceRoleMultiplicity;

		// Token: 0x0400139A RID: 5018
		private readonly RelationshipMultiplicity _targetRoleMultiplicity;
	}
}
