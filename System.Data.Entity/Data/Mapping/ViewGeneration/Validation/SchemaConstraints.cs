using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000285 RID: 645
	internal class SchemaConstraints<TKeyConstraint> : InternalBase where TKeyConstraint : InternalBase
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x00095A3A File Offset: 0x00093C3A
		internal SchemaConstraints()
		{
			this.m_keyConstraints = new List<TKeyConstraint>();
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00095A4D File Offset: 0x00093C4D
		internal IEnumerable<TKeyConstraint> KeyConstraints
		{
			get
			{
				return this.m_keyConstraints;
			}
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00095A55 File Offset: 0x00093C55
		internal void Add(TKeyConstraint constraint)
		{
			EntityUtil.CheckArgumentNull<TKeyConstraint>(constraint, "constraint");
			this.m_keyConstraints.Add(constraint);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00095A70 File Offset: 0x00093C70
		private static void ConstraintsToBuilder<Constraint>(IEnumerable<Constraint> constraints, StringBuilder builder) where Constraint : InternalBase
		{
			foreach (Constraint constraint in constraints)
			{
				constraint.ToCompactString(builder);
				builder.Append(Environment.NewLine);
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00095ACC File Offset: 0x00093CCC
		internal override void ToCompactString(StringBuilder builder)
		{
			SchemaConstraints<TKeyConstraint>.ConstraintsToBuilder<TKeyConstraint>(this.m_keyConstraints, builder);
		}

		// Token: 0x040011E2 RID: 4578
		private List<TKeyConstraint> m_keyConstraints;
	}
}
