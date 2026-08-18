using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000481 RID: 1153
	internal abstract class MemberRestriction : BoolLiteral
	{
		// Token: 0x06002A9C RID: 10908 RVA: 0x000CDCA8 File Offset: 0x000CBEA8
		protected MemberRestriction(MemberProjectedSlot slot, Constant value) : this(slot, new Constant[]
		{
			value
		})
		{
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000CDCC8 File Offset: 0x000CBEC8
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = new Domain(values, values);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000CDCE4 File Offset: 0x000CBEE4
		protected MemberRestriction(MemberProjectedSlot slot, Domain domain)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = domain;
			this.m_isComplete = true;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000CDD01 File Offset: 0x000CBF01
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues) : this(slot, new Domain(values, possibleValues))
		{
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000CDD11 File Offset: 0x000CBF11
		internal bool IsComplete
		{
			get
			{
				return this.m_isComplete;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x000CDD19 File Offset: 0x000CBF19
		internal MemberProjectedSlot RestrictedMemberSlot
		{
			get
			{
				return this.m_restrictedMemberSlot;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000CDD21 File Offset: 0x000CBF21
		internal Domain Domain
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000CDD2C File Offset: 0x000CBF2C
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap)
		{
			TermExpr<DomainConstraint<BoolLiteral, Constant>> result;
			if (domainMap != null)
			{
				IEnumerable<Constant> domain = domainMap.GetDomain(this.m_restrictedMemberSlot.MemberPath);
				result = BoolLiteral.MakeTermExpression(this, domain, this.m_domain.Values);
			}
			else
			{
				result = BoolLiteral.MakeTermExpression(this, this.m_domain.AllPossibleValues, this.m_domain.Values);
			}
			return result;
		}

		// Token: 0x06002AA4 RID: 10916
		internal abstract MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues);

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000CDD84 File Offset: 0x000CBF84
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			MemberPath memberPath = this.RestrictedMemberSlot.MemberPath;
			int num = projectedSlotMap.IndexOf(memberPath);
			requiredSlots[num] = true;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000CDDAC File Offset: 0x000CBFAC
		protected override bool IsEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (object.ReferenceEquals(this, memberRestriction) || (ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot) && this.m_domain.IsEqualTo(memberRestriction.m_domain)));
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000CDDFC File Offset: 0x000CBFFC
		public override int GetHashCode()
		{
			int hashCode = ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot);
			return hashCode ^ this.m_domain.GetHash();
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000CDE2C File Offset: 0x000CC02C
		protected override bool IsIdentifierEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (object.ReferenceEquals(this, memberRestriction) || ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot));
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000CDE68 File Offset: 0x000CC068
		protected override int GetIdentifierHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000CDE87 File Offset: 0x000CC087
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000CDE92 File Offset: 0x000CC092
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x04000FB1 RID: 4017
		private readonly MemberProjectedSlot m_restrictedMemberSlot;

		// Token: 0x04000FB2 RID: 4018
		private readonly Domain m_domain;

		// Token: 0x04000FB3 RID: 4019
		private readonly bool m_isComplete;
	}
}
