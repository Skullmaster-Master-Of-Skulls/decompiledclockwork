using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000499 RID: 1177
	internal class SchemaConstraints<TKeyConstraint> : InternalBase where TKeyConstraint : InternalBase
	{
		// Token: 0x06002B74 RID: 11124 RVA: 0x000D3554 File Offset: 0x000D1754
		internal SchemaConstraints()
		{
			this.m_keyConstraints = new List<TKeyConstraint>();
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000D3567 File Offset: 0x000D1767
		internal IEnumerable<TKeyConstraint> KeyConstraints
		{
			get
			{
				return this.m_keyConstraints;
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000D356F File Offset: 0x000D176F
		internal void Add(TKeyConstraint constraint)
		{
			this.m_keyConstraints.Add(constraint);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000D3580 File Offset: 0x000D1780
		private static void ConstraintsToBuilder<Constraint>(IEnumerable<Constraint> constraints, StringBuilder builder) where Constraint : InternalBase
		{
			foreach (Constraint constraint in constraints)
			{
				constraint.ToCompactString(builder);
				builder.Append(Environment.NewLine);
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000D35DC File Offset: 0x000D17DC
		internal override void ToCompactString(StringBuilder builder)
		{
			SchemaConstraints<TKeyConstraint>.ConstraintsToBuilder<TKeyConstraint>(this.m_keyConstraints, builder);
		}

		// Token: 0x0400100B RID: 4107
		private readonly List<TKeyConstraint> m_keyConstraints;
	}
}
