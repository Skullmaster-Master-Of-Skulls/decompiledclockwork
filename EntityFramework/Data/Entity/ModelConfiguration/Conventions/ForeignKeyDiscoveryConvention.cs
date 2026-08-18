using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007F9 RID: 2041
	public abstract class ForeignKeyDiscoveryConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x0018E55F File Offset: 0x0018C75F
		protected virtual bool SupportsMultipleAssociations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C56 RID: 23638
		protected abstract bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty);

		// Token: 0x06005C57 RID: 23639 RVA: 0x0018E718 File Offset: 0x0018C918
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.Constraint != null || item.IsIndependent() || (item.IsOneToOne() && item.IsSelfReferencing()))
			{
				return;
			}
			AssociationEndMember principalEnd;
			AssociationEndMember dependentEnd;
			if (!item.TryGuessPrincipalAndDependentEnds(out principalEnd, out dependentEnd))
			{
				return;
			}
			IEnumerable<EdmProperty> source = principalEnd.GetEntityType().KeyProperties();
			if (!source.Any<EdmProperty>())
			{
				return;
			}
			if (!this.SupportsMultipleAssociations && model.ConceptualModel.GetAssociationTypesBetween(principalEnd.GetEntityType(), dependentEnd.GetEntityType()).Count<AssociationType>() > 1)
			{
				return;
			}
			IEnumerable<EdmProperty> enumerable = from p in source
			from d in dependentEnd.GetEntityType().DeclaredProperties
			where this.MatchDependentKeyProperty(item, dependentEnd, d, principalEnd.GetEntityType(), p) && p.UnderlyingPrimitiveType == d.UnderlyingPrimitiveType
			select d;
			if (!enumerable.Any<EdmProperty>() || enumerable.Count<EdmProperty>() != source.Count<EdmProperty>())
			{
				return;
			}
			IEnumerable<EdmProperty> source2 = dependentEnd.GetEntityType().KeyProperties();
			bool flag = source2.Count<EdmProperty>() == enumerable.Count<EdmProperty>() && source2.All(new Func<EdmProperty, bool>(enumerable.Contains<EdmProperty>));
			if ((dependentEnd.IsMany() || item.IsSelfReferencing()) && flag)
			{
				return;
			}
			if (!dependentEnd.IsMany() && !flag)
			{
				return;
			}
			ReferentialConstraint referentialConstraint = new ReferentialConstraint(principalEnd, dependentEnd, source.ToList<EdmProperty>(), enumerable.ToList<EdmProperty>());
			item.Constraint = referentialConstraint;
			if (principalEnd.IsRequired())
			{
				referentialConstraint.ToProperties.Each((EdmProperty p) => p.Nullable = false);
			}
		}
	}
}
