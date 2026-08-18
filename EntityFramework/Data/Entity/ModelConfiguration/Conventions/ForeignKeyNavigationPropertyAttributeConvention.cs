using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FA RID: 2042
	public class ForeignKeyNavigationPropertyAttributeConvention : IConceptualModelConvention<NavigationProperty>, IConvention
	{
		// Token: 0x06005C5C RID: 23644 RVA: 0x0018E9A8 File Offset: 0x0018CBA8
		public virtual void Apply(NavigationProperty item, DbModel model)
		{
			Check.NotNull<NavigationProperty>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			AssociationType association = item.Association;
			if (association.Constraint != null)
			{
				return;
			}
			ForeignKeyAttribute foreignKeyAttribute = item.GetClrAttributes<ForeignKeyAttribute>().SingleOrDefault<ForeignKeyAttribute>();
			if (foreignKeyAttribute == null)
			{
				return;
			}
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			if (association.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2) || association.IsPrincipalConfigured())
			{
				associationEndMember2 = (associationEndMember2 ?? association.TargetEnd);
				associationEndMember = (associationEndMember ?? association.SourceEnd);
				IEnumerable<string> dependentPropertyNames = from p in foreignKeyAttribute.Name.Split(new char[]
				{
					','
				})
				select p.Trim();
				EntityType declaringEntityType = model.ConceptualModel.EntityTypes.Single((EntityType e) => e.DeclaredNavigationProperties.Contains(item));
				List<EdmProperty> toProperties = ForeignKeyNavigationPropertyAttributeConvention.GetDependentProperties(associationEndMember2.GetEntityType(), dependentPropertyNames, declaringEntityType, item).ToList<EdmProperty>();
				ReferentialConstraint constraint = new ReferentialConstraint(associationEndMember, associationEndMember2, associationEndMember.GetEntityType().KeyProperties().ToList<EdmProperty>(), toProperties);
				IEnumerable<EdmProperty> source = associationEndMember2.GetEntityType().KeyProperties();
				if (source.Count<EdmProperty>() == constraint.ToProperties.Count<EdmProperty>() && source.All((EdmProperty kp) => constraint.ToProperties.Contains(kp)))
				{
					associationEndMember.RelationshipMultiplicity = RelationshipMultiplicity.One;
					if (associationEndMember2.RelationshipMultiplicity.IsMany())
					{
						associationEndMember2.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
					}
				}
				if (associationEndMember.IsRequired())
				{
					constraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
				}
				association.Constraint = constraint;
			}
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x0018EE24 File Offset: 0x0018D024
		private static IEnumerable<EdmProperty> GetDependentProperties(EntityType dependentType, IEnumerable<string> dependentPropertyNames, EntityType declaringEntityType, NavigationProperty navigationProperty)
		{
			using (IEnumerator<string> enumerator = dependentPropertyNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string dependentPropertyName = enumerator.Current;
					if (string.IsNullOrWhiteSpace(dependentPropertyName))
					{
						throw Error.ForeignKeyAttributeConvention_EmptyKey(navigationProperty.Name, declaringEntityType.GetClrType());
					}
					EdmProperty dependentProperty = dependentType.Properties.SingleOrDefault((EdmProperty p) => p.Name.Equals(dependentPropertyName, StringComparison.Ordinal));
					if (dependentProperty == null)
					{
						throw Error.ForeignKeyAttributeConvention_InvalidKey(navigationProperty.Name, declaringEntityType.GetClrType(), dependentPropertyName, dependentType.GetClrType());
					}
					yield return dependentProperty;
				}
			}
			yield break;
		}
	}
}
