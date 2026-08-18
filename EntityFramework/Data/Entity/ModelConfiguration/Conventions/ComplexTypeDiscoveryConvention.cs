using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F1 RID: 2033
	public class ComplexTypeDiscoveryConvention : IConceptualModelConvention<EdmModel>, IConvention
	{
		// Token: 0x06005C32 RID: 23602 RVA: 0x0018D8E4 File Offset: 0x0018BAE4
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public virtual void Apply(EdmModel item, DbModel model)
		{
			Check.NotNull<EdmModel>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			var source = from entityType in item.EntityTypes
			where entityType.KeyProperties.Count == 0 && entityType.BaseType == null
			let entityTypeConfiguration = entityType.GetConfiguration() as EntityTypeConfiguration
			where (entityTypeConfiguration == null || (!entityTypeConfiguration.IsExplicitEntity && entityTypeConfiguration.IsStructuralConfigurationOnly)) && !entityType.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty)).Any<EdmMember>()
			let matchingAssociations = 
				from associationType in item.AssociationTypes
				where associationType.SourceEnd.GetEntityType() == entityType || associationType.TargetEnd.GetEntityType() == entityType
				let declaringEnd = (associationType.SourceEnd.GetEntityType() == entityType) ? associationType.SourceEnd : associationType.TargetEnd
				let declaringEntity = associationType.GetOtherEnd(declaringEnd).GetEntityType()
				let navigationProperties = 
					from NavigationProperty n in declaringEntity.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty))
					where n.ResultEnd.GetEntityType() == entityType
					select n
				select new
				{
					DeclaringEnd = declaringEnd,
					AssociationType = associationType,
					DeclaringEntityType = declaringEntity,
					NavigationProperties = navigationProperties.ToList<NavigationProperty>()
				}
			where matchingAssociations.All(delegate(a)
			{
				if (a.AssociationType.Constraint == null && a.AssociationType.GetConfiguration() == null && !a.AssociationType.IsSelfReferencing() && a.DeclaringEnd.IsOptional())
				{
					return a.NavigationProperties.All((NavigationProperty n) => n.GetConfiguration() == null);
				}
				return false;
			})
			select new
			{
				EntityType = entityType,
				MatchingAssociations = matchingAssociations.ToList()
			};
			foreach (var <>f__AnonymousType in source.ToList())
			{
				ComplexType complexType = item.AddComplexType(<>f__AnonymousType.EntityType.Name, <>f__AnonymousType.EntityType.NamespaceName);
				foreach (EdmProperty member in <>f__AnonymousType.EntityType.DeclaredProperties)
				{
					complexType.AddMember(member);
				}
				foreach (MetadataProperty item2 in <>f__AnonymousType.EntityType.Annotations)
				{
					complexType.GetMetadataProperties().Add(item2);
				}
				foreach (var <>f__AnonymousType4f in <>f__AnonymousType.MatchingAssociations)
				{
					foreach (NavigationProperty navigationProperty in <>f__AnonymousType4f.NavigationProperties)
					{
						if (<>f__AnonymousType4f.DeclaringEntityType.Members.Where(new Func<EdmMember, bool>(Helper.IsNavigationProperty)).Contains(navigationProperty))
						{
							<>f__AnonymousType4f.DeclaringEntityType.RemoveMember(navigationProperty);
							EdmProperty edmProperty = <>f__AnonymousType4f.DeclaringEntityType.AddComplexProperty(navigationProperty.Name, complexType);
							foreach (MetadataProperty item3 in navigationProperty.Annotations)
							{
								edmProperty.GetMetadataProperties().Add(item3);
							}
						}
					}
					item.RemoveAssociationType(<>f__AnonymousType4f.AssociationType);
				}
				item.RemoveEntityType(<>f__AnonymousType.EntityType);
			}
		}
	}
}
