using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200044C RID: 1100
	internal abstract class BoolLiteral : InternalBase
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x000C54A4 File Offset: 0x000C36A4
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, IEnumerable<Constant> domain, IEnumerable<Constant> range)
		{
			Set<Constant> domain2 = new Set<Constant>(domain, Constant.EqualityComparer);
			Set<Constant> range2 = new Set<Constant>(range, Constant.EqualityComparer);
			return BoolLiteral.MakeTermExpression(literal, domain2, range2);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000C54D4 File Offset: 0x000C36D4
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, Set<Constant> domain, Set<Constant> range)
		{
			domain.MakeReadOnly();
			range.MakeReadOnly();
			DomainVariable<BoolLiteral, Constant> variable = new DomainVariable<BoolLiteral, Constant>(literal, domain, BoolLiteral.EqualityIdentifierComparer);
			DomainConstraint<BoolLiteral, Constant> identifier = new DomainConstraint<BoolLiteral, Constant>(variable, range);
			return new TermExpr<DomainConstraint<BoolLiteral, Constant>>(EqualityComparer<DomainConstraint<BoolLiteral, Constant>>.Default, identifier);
		}

		// Token: 0x0600287D RID: 10365
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap);

		// Token: 0x0600287E RID: 10366
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap);

		// Token: 0x0600287F RID: 10367
		internal abstract BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap);

		// Token: 0x06002880 RID: 10368
		internal abstract void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots);

		// Token: 0x06002881 RID: 10369
		internal abstract StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x06002882 RID: 10370
		internal abstract DbExpression AsCqt(DbExpression row, bool skipIsNotNull);

		// Token: 0x06002883 RID: 10371
		internal abstract StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x06002884 RID: 10372
		internal abstract StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x06002885 RID: 10373 RVA: 0x000C5511 File Offset: 0x000C3711
		protected virtual bool IsIdentifierEqualTo(BoolLiteral right)
		{
			return this.IsEqualTo(right);
		}

		// Token: 0x06002886 RID: 10374
		protected abstract bool IsEqualTo(BoolLiteral right);

		// Token: 0x06002887 RID: 10375 RVA: 0x000C551A File Offset: 0x000C371A
		protected virtual int GetIdentifierHash()
		{
			return this.GetHashCode();
		}

		// Token: 0x04000F34 RID: 3892
		internal static readonly IEqualityComparer<BoolLiteral> EqualityComparer = new BoolLiteral.BoolLiteralComparer();

		// Token: 0x04000F35 RID: 3893
		internal static readonly IEqualityComparer<BoolLiteral> EqualityIdentifierComparer = new BoolLiteral.IdentifierComparer();

		// Token: 0x0200044D RID: 1101
		private sealed class BoolLiteralComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x0600288A RID: 10378 RVA: 0x000C5540 File Offset: 0x000C3740
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x0600288B RID: 10379 RVA: 0x000C555C File Offset: 0x000C375C
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetHashCode();
			}
		}

		// Token: 0x0200044E RID: 1102
		private sealed class IdentifierComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x0600288D RID: 10381 RVA: 0x000C556C File Offset: 0x000C376C
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.IsIdentifierEqualTo(right));
			}

			// Token: 0x0600288E RID: 10382 RVA: 0x000C5588 File Offset: 0x000C3788
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetIdentifierHash();
			}
		}
	}
}
