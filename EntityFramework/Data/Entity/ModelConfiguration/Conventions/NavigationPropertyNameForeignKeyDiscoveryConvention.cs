using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FD RID: 2045
	public class NavigationPropertyNameForeignKeyDiscoveryConvention : ForeignKeyDiscoveryConvention
	{
		// Token: 0x06005C67 RID: 23655 RVA: 0x0018EFF8 File Offset: 0x0018D1F8
		protected override bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty)
		{
			Check.NotNull<AssociationType>(associationType, "associationType");
			Check.NotNull<AssociationEndMember>(dependentAssociationEnd, "dependentAssociationEnd");
			Check.NotNull<EdmProperty>(dependentProperty, "dependentProperty");
			Check.NotNull<EntityType>(principalEntityType, "principalEntityType");
			Check.NotNull<EdmProperty>(principalKeyProperty, "principalKeyProperty");
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentAssociationEnd);
			NavigationProperty navigationProperty = dependentAssociationEnd.GetEntityType().NavigationProperties.SingleOrDefault((NavigationProperty n) => n.ResultEnd == otherEnd);
			return navigationProperty != null && string.Equals(dependentProperty.Name, navigationProperty.Name + principalKeyProperty.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x0018F096 File Offset: 0x0018D296
		protected override bool SupportsMultipleAssociations
		{
			get
			{
				return true;
			}
		}
	}
}
