using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F8 RID: 2040
	public class ForeignKeyAssociationMultiplicityConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06005C51 RID: 23633 RVA: 0x0018E428 File Offset: 0x0018C628
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			ReferentialConstraint constraint = item.Constraint;
			if (constraint == null)
			{
				return;
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = item.Annotations.GetConfiguration() as NavigationPropertyConfiguration;
			if (constraint.ToProperties.All((EdmProperty p) => !p.Nullable))
			{
				AssociationEndMember principalEnd = item.GetOtherEnd(constraint.DependentEnd);
				NavigationProperty navigationProperty = model.ConceptualModel.EntityTypes.SelectMany((EntityType et) => et.DeclaredNavigationProperties).SingleOrDefault((NavigationProperty np) => np.ResultEnd == principalEnd);
				PropertyInfo clrPropertyInfo;
				if (navigationPropertyConfiguration != null && navigationProperty != null && (clrPropertyInfo = navigationProperty.Annotations.GetClrPropertyInfo()) != null && ((clrPropertyInfo == navigationPropertyConfiguration.NavigationProperty && navigationPropertyConfiguration.RelationshipMultiplicity != null) || (clrPropertyInfo == navigationPropertyConfiguration.InverseNavigationProperty && navigationPropertyConfiguration.InverseEndKind != null)))
				{
					return;
				}
				principalEnd.RelationshipMultiplicity = RelationshipMultiplicity.One;
			}
		}
	}
}
