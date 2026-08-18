using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029C RID: 668
	internal abstract class BoolLiteral : InternalBase
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x0009A540 File Offset: 0x00098740
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, IEnumerable<Constant> domain, IEnumerable<Constant> range)
		{
			Set<Constant> domain2 = new Set<Constant>(domain, Constant.EqualityComparer);
			Set<Constant> range2 = new Set<Constant>(range, Constant.EqualityComparer);
			return BoolLiteral.MakeTermExpression(literal, domain2, range2);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0009A570 File Offset: 0x00098770
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, Set<Constant> domain, Set<Constant> range)
		{
			domain.MakeReadOnly();
			range.MakeReadOnly();
			DomainVariable<BoolLiteral, Constant> variable = new DomainVariable<BoolLiteral, Constant>(literal, domain, BoolLiteral.EqualityIdentifierComparer);
			DomainConstraint<BoolLiteral, Constant> identifier = new DomainConstraint<BoolLiteral, Constant>(variable, range);
			return new TermExpr<DomainConstraint<BoolLiteral, Constant>>(EqualityComparer<DomainConstraint<BoolLiteral, Constant>>.Default, identifier);
		}

		// Token: 0x060027C5 RID: 10181
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap);

		// Token: 0x060027C6 RID: 10182
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap);

		// Token: 0x060027C7 RID: 10183
		internal abstract BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap);

		// Token: 0x060027C8 RID: 10184
		internal abstract void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots);

		// Token: 0x060027C9 RID: 10185
		internal abstract StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x060027CA RID: 10186
		internal abstract DbExpression AsCqt(DbExpression row, bool skipIsNotNull);

		// Token: 0x060027CB RID: 10187
		internal abstract StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x060027CC RID: 10188
		internal abstract StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x060027CD RID: 10189 RVA: 0x0009A5AD File Offset: 0x000987AD
		protected virtual bool IsIdentifierEqualTo(BoolLiteral right)
		{
			return this.IsEqualTo(right);
		}

		// Token: 0x060027CE RID: 10190
		protected abstract bool IsEqualTo(BoolLiteral right);

		// Token: 0x060027CF RID: 10191 RVA: 0x0009A5B6 File Offset: 0x000987B6
		protected virtual int GetIdentifierHash()
		{
			return this.GetHashCode();
		}

		// Token: 0x0400122F RID: 4655
		internal static readonly IEqualityComparer<BoolLiteral> EqualityComparer = new BoolLiteral.BoolLiteralComparer();

		// Token: 0x04001230 RID: 4656
		internal static readonly IEqualityComparer<BoolLiteral> EqualityIdentifierComparer = new BoolLiteral.IdentifierComparer();

		// Token: 0x020005D1 RID: 1489
		private sealed class BoolLiteralComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x06004145 RID: 16709 RVA: 0x000EECCD File Offset: 0x000ECECD
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x06004146 RID: 16710 RVA: 0x0003C7A1 File Offset: 0x0003A9A1
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetHashCode();
			}
		}

		// Token: 0x020005D2 RID: 1490
		private sealed class IdentifierComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x06004148 RID: 16712 RVA: 0x000EECE4 File Offset: 0x000ECEE4
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return left == right || (left != null && right != null && left.IsIdentifierEqualTo(right));
			}

			// Token: 0x06004149 RID: 16713 RVA: 0x000EECFB File Offset: 0x000ECEFB
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetIdentifierHash();
			}
		}
	}
}
