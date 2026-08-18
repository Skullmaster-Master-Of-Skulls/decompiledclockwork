using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000805 RID: 2053
	public class TypeNameForeignKeyDiscoveryConvention : ForeignKeyDiscoveryConvention
	{
		// Token: 0x06005C8E RID: 23694 RVA: 0x0018FCE0 File Offset: 0x0018DEE0
		protected override bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty)
		{
			Check.NotNull<AssociationType>(associationType, "associationType");
			Check.NotNull<AssociationEndMember>(dependentAssociationEnd, "dependentAssociationEnd");
			Check.NotNull<EdmProperty>(dependentProperty, "dependentProperty");
			Check.NotNull<EntityType>(principalEntityType, "principalEntityType");
			Check.NotNull<EdmProperty>(principalKeyProperty, "principalKeyProperty");
			return string.Equals(dependentProperty.Name, principalEntityType.Name + principalKeyProperty.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
