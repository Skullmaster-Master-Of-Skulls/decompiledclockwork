using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000801 RID: 2049
	public class PrimaryKeyNameForeignKeyDiscoveryConvention : ForeignKeyDiscoveryConvention
	{
		// Token: 0x06005C73 RID: 23667 RVA: 0x0018F2F0 File Offset: 0x0018D4F0
		protected override bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty)
		{
			Check.NotNull<AssociationType>(associationType, "associationType");
			Check.NotNull<AssociationEndMember>(dependentAssociationEnd, "dependentAssociationEnd");
			Check.NotNull<EdmProperty>(dependentProperty, "dependentProperty");
			Check.NotNull<EntityType>(principalEntityType, "principalEntityType");
			Check.NotNull<EdmProperty>(principalKeyProperty, "principalKeyProperty");
			return string.Equals(dependentProperty.Name, principalKeyProperty.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
