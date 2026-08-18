using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F0 RID: 2032
	public class AssociationInverseDiscoveryConvention : IConceptualModelConvention<EdmModel>, IConvention
	{
		// Token: 0x06005C23 RID: 23587 RVA: 0x0018C9CC File Offset: 0x0018ABCC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public virtual void Apply(EdmModel item, DbModel model)
		{
			Check.NotNull<EdmModel>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			var enumerable = from g in (from a1 in item.AssociationTypes
			from a2 in item.AssociationTypes
			where a1 != a2
			where a1.SourceEnd.GetEntityType() == a2.TargetEnd.GetEntityType() && a1.TargetEnd.GetEntityType() == a2.SourceEnd.GetEntityType()
			let a1Configuration = a1.GetConfiguration() as NavigationPropertyConfiguration
			let a2Configuration = a2.GetConfiguration() as NavigationPropertyConfiguration
			where (a1Configuration == null || (a1Configuration.InverseEndKind == null && a1Configuration.InverseNavigationProperty == null)) && (a2Configuration == null || (a2Configuration.InverseEndKind == null && a2Configuration.InverseNavigationProperty == null))
			select new
			{
				a1,
				a2
			}).Distinct((a, b) => a.a1 == b.a2 && a.a2 == b.a1).GroupBy((a, b) => a.a1.SourceEnd.GetEntityType() == b.a2.TargetEnd.GetEntityType() && a.a1.TargetEnd.GetEntityType() == b.a2.SourceEnd.GetEntityType())
			where g.Count() == 1
			select g.Single();
			foreach (var <>f__AnonymousType in enumerable)
			{
				AssociationType associationType = (<>f__AnonymousType.a2.GetConfiguration() != null) ? <>f__AnonymousType.a2 : <>f__AnonymousType.a1;
				AssociationType associationType2 = (associationType == <>f__AnonymousType.a1) ? <>f__AnonymousType.a2 : <>f__AnonymousType.a1;
				associationType.SourceEnd.RelationshipMultiplicity = associationType2.TargetEnd.RelationshipMultiplicity;
				if (associationType2.Constraint != null)
				{
					associationType.Constraint = associationType2.Constraint;
					associationType.Constraint.FromRole = associationType.SourceEnd;
					associationType.Constraint.ToRole = associationType.TargetEnd;
				}
				PropertyInfo clrPropertyInfo = associationType2.SourceEnd.GetClrPropertyInfo();
				if (clrPropertyInfo != null)
				{
					associationType.TargetEnd.SetClrPropertyInfo(clrPropertyInfo);
				}
				AssociationInverseDiscoveryConvention.FixNavigationProperties(item, associationType, associationType2);
				item.RemoveAssociationType(associationType2);
			}
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x0018CCB4 File Offset: 0x0018AEB4
		private static void FixNavigationProperties(EdmModel model, AssociationType unifiedAssociation, AssociationType redundantAssociation)
		{
			foreach (NavigationProperty navigationProperty in from np in model.EntityTypes.SelectMany((EntityType e) => e.NavigationProperties)
			where np.Association == redundantAssociation
			select np)
			{
				navigationProperty.RelationshipType = unifiedAssociation;
				navigationProperty.FromEndMember = unifiedAssociation.TargetEnd;
				navigationProperty.ToEndMember = unifiedAssociation.SourceEnd;
			}
		}
	}
}
