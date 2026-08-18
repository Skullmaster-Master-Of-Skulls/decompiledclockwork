using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FF RID: 2047
	public class OneToOneConstraintIntroductionConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06005C6C RID: 23660 RVA: 0x0018F138 File Offset: 0x0018D338
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.IsOneToOne() && !item.IsSelfReferencing() && !item.IsIndependent() && item.Constraint == null)
			{
				IEnumerable<EdmProperty> source = item.SourceEnd.GetEntityType().KeyProperties();
				IEnumerable<EdmProperty> source2 = item.TargetEnd.GetEntityType().KeyProperties();
				if (source.Count<EdmProperty>() == source2.Count<EdmProperty>())
				{
					AssociationEndMember associationEndMember;
					AssociationEndMember associationEndMember2;
					if ((from p in source
					select p.UnderlyingPrimitiveType).SequenceEqual(from p in source2
					select p.UnderlyingPrimitiveType) && (item.TryGuessPrincipalAndDependentEnds(out associationEndMember, out associationEndMember2) || item.IsPrincipalConfigured()))
					{
						associationEndMember2 = (associationEndMember2 ?? item.TargetEnd);
						AssociationEndMember otherEnd = item.GetOtherEnd(associationEndMember2);
						ReferentialConstraint constraint = new ReferentialConstraint(otherEnd, associationEndMember2, otherEnd.GetEntityType().KeyProperties().ToList<EdmProperty>(), associationEndMember2.GetEntityType().KeyProperties().ToList<EdmProperty>());
						item.Constraint = constraint;
					}
				}
			}
		}
	}
}
