using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B3 RID: 691
	internal abstract class MemberRestriction : BoolLiteral
	{
		// Token: 0x0600292D RID: 10541 RVA: 0x0009F8AF File Offset: 0x0009DAAF
		protected MemberRestriction(MemberProjectedSlot slot, Constant value) : this(slot, new Constant[]
		{
			value
		})
		{
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x0009F8C2 File Offset: 0x0009DAC2
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = new Domain(values, values);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0009F8DE File Offset: 0x0009DADE
		protected MemberRestriction(MemberProjectedSlot slot, Domain domain)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = domain;
			this.m_isComplete = true;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0009F8FB File Offset: 0x0009DAFB
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues) : this(slot, new Domain(values, possibleValues))
		{
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0009F90B File Offset: 0x0009DB0B
		internal bool IsComplete
		{
			get
			{
				return this.m_isComplete;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x0009F913 File Offset: 0x0009DB13
		internal MemberProjectedSlot RestrictedMemberSlot
		{
			get
			{
				return this.m_restrictedMemberSlot;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x0009F91B File Offset: 0x0009DB1B
		internal Domain Domain
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0009F924 File Offset: 0x0009DB24
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

		// Token: 0x06002935 RID: 10549
		internal abstract MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues);

		// Token: 0x06002936 RID: 10550 RVA: 0x0009F97C File Offset: 0x0009DB7C
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			MemberPath memberPath = this.RestrictedMemberSlot.MemberPath;
			int num = projectedSlotMap.IndexOf(memberPath);
			requiredSlots[num] = true;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x0009F9A4 File Offset: 0x0009DBA4
		protected override bool IsEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (this == memberRestriction || (ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot) && this.m_domain.IsEqualTo(memberRestriction.m_domain)));
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x0009F9F0 File Offset: 0x0009DBF0
		public override int GetHashCode()
		{
			int hashCode = ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot);
			return hashCode ^ this.m_domain.GetHash();
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x0009FA20 File Offset: 0x0009DC20
		protected override bool IsIdentifierEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (this == memberRestriction || ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot));
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x0009FA58 File Offset: 0x0009DC58
		protected override int GetIdentifierHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x0009FA78 File Offset: 0x0009DC78
		internal void ToUserString(bool invertOutput, StringBuilder builder, MetadataWorkspace workspace)
		{
			NegatedConstant negatedConstant = null;
			foreach (Constant constant in this.Domain.Values)
			{
				negatedConstant = (constant as NegatedConstant);
				if (negatedConstant != null)
				{
					break;
				}
			}
			Set<Constant> set;
			if (negatedConstant != null)
			{
				invertOutput = !invertOutput;
				set = new Set<Constant>(negatedConstant.Elements, Constant.EqualityComparer);
				using (IEnumerator<Constant> enumerator2 = this.Domain.Values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Constant constant2 = enumerator2.Current;
						if (!(constant2 is NegatedConstant))
						{
							set.Remove(constant2);
						}
					}
					goto IL_B8;
				}
			}
			set = new Set<Constant>(this.Domain.Values, Constant.EqualityComparer);
			IL_B8:
			bool flag = set.Count == 1 && set.Single<Constant>().IsNull();
			bool flag2 = this is TypeRestriction;
			Func<object, string> func = null;
			Func<object, object, string> func2 = null;
			if (invertOutput)
			{
				if (flag)
				{
					func = (flag2 ? new Func<object, string>(Strings.ViewGen_OneOfConst_IsNonNullable) : new Func<object, string>(Strings.ViewGen_OneOfConst_MustBeNonNullable));
				}
				else if (set.Count == 1)
				{
					func2 = (flag2 ? new Func<object, object, string>(Strings.ViewGen_OneOfConst_IsNotEqualTo) : new Func<object, object, string>(Strings.ViewGen_OneOfConst_MustNotBeEqualTo));
				}
				else
				{
					func2 = (flag2 ? new Func<object, object, string>(Strings.ViewGen_OneOfConst_IsNotOneOf) : new Func<object, object, string>(Strings.ViewGen_OneOfConst_MustNotBeOneOf));
				}
			}
			else if (flag)
			{
				func = (flag2 ? new Func<object, string>(Strings.ViewGen_OneOfConst_MustBeNull) : new Func<object, string>(Strings.ViewGen_OneOfConst_MustBeNull));
			}
			else if (set.Count == 1)
			{
				func2 = (flag2 ? new Func<object, object, string>(Strings.ViewGen_OneOfConst_IsEqualTo) : new Func<object, object, string>(Strings.ViewGen_OneOfConst_MustBeEqualTo));
			}
			else
			{
				func2 = (flag2 ? new Func<object, object, string>(Strings.ViewGen_OneOfConst_IsOneOf) : new Func<object, object, string>(Strings.ViewGen_OneOfConst_MustBeOneOf));
			}
			StringBuilder stringBuilder = new StringBuilder();
			Constant.ConstantsToUserString(stringBuilder, set);
			string text = this.m_restrictedMemberSlot.MemberPath.PathToString(new bool?(false));
			if (flag2)
			{
				text = "TypeOf(" + text + ")";
			}
			if (func != null)
			{
				builder.Append(func(text));
			}
			else
			{
				builder.Append(func2(text, stringBuilder.ToString()));
			}
			if (invertOutput && flag2)
			{
				this.InvertOutputStringForTypeConstant(builder, set, workspace);
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x0009FCE0 File Offset: 0x0009DEE0
		private void InvertOutputStringForTypeConstant(StringBuilder builder, Set<Constant> constants, MetadataWorkspace workspace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Set<EdmType> set = new Set<EdmType>();
			EdmType edmType = this.RestrictedMemberSlot.MemberPath.EdmType;
			foreach (EdmType element in MetadataHelper.GetTypeAndSubtypesOf(edmType, workspace, false))
			{
				set.Add(element);
			}
			Set<EdmType> set2 = new Set<EdmType>();
			foreach (Constant constant in constants)
			{
				TypeConstant typeConstant = (TypeConstant)constant;
				set2.Add(typeConstant.EdmType);
			}
			set.Subtract(set2);
			bool flag = true;
			foreach (EdmType edmType2 in set)
			{
				if (!flag)
				{
					stringBuilder.Append(Strings.ViewGen_CommaBlank);
				}
				flag = false;
				stringBuilder.Append(edmType2.Name);
			}
			builder.Append(Strings.ViewGen_OneOfConst_IsOneOfTypes(stringBuilder.ToString()));
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0009BBB5 File Offset: 0x00099DB5
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0009BBC0 File Offset: 0x00099DC0
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x0400127A RID: 4730
		private readonly MemberProjectedSlot m_restrictedMemberSlot;

		// Token: 0x0400127B RID: 4731
		private readonly Domain m_domain;

		// Token: 0x0400127C RID: 4732
		private readonly bool m_isComplete;
	}
}
