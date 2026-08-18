using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000486 RID: 1158
	internal class ScalarRestriction : MemberRestriction
	{
		// Token: 0x06002AE9 RID: 10985 RVA: 0x000CF358 File Offset: 0x000CD558
		internal ScalarRestriction(MemberPath member, Constant value) : base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000CF367 File Offset: 0x000CD567
		internal ScalarRestriction(MemberPath member, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues) : base(new MemberProjectedSlot(member), values, possibleValues)
		{
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000CF377 File Offset: 0x000CD577
		internal ScalarRestriction(MemberProjectedSlot slot, Domain domain) : base(slot, domain)
		{
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000CF384 File Offset: 0x000CD584
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			BoolLiteral boolLiteral = new ScalarRestriction(base.RestrictedMemberSlot, new Domain(range, domain));
			return boolLiteral.GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000CF3C0 File Offset: 0x000CD5C0
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberProjectedSlot slot = base.RestrictedMemberSlot.RemapSlot(remap);
			return new ScalarRestriction(slot, base.Domain);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000CF3E6 File Offset: 0x000CD5E6
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new ScalarRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000CF404 File Offset: 0x000CD604
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, false);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			DbExpression cqt = null;
			this.AsCql(delegate(NegatedConstant negated, IEnumerable<Constant> domainValues)
			{
				cqt = negated.AsCqt(row, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
			}, delegate(Set<Constant> domainValues)
			{
				cqt = this.RestrictedMemberSlot.MemberPath.AsCqt(row);
				if (domainValues.Count == 1)
				{
					cqt = cqt.Equal(domainValues.Single<Constant>().AsCqt(row, this.RestrictedMemberSlot.MemberPath));
					return;
				}
				List<DbExpression> nodes = (from c in domainValues
				select cqt.Equal(c.AsCqt(row, this.RestrictedMemberSlot.MemberPath))).ToList<DbExpression>();
				cqt = Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression prev, DbExpression next) => prev.Or(next));
			}, delegate
			{
				DbExpression dbExpression = this.RestrictedMemberSlot.MemberPath.AsCqt(row).IsNull().Not();
				cqt = ((cqt != null) ? cqt.And(dbExpression) : dbExpression);
			}, delegate
			{
				DbExpression dbExpression = this.RestrictedMemberSlot.MemberPath.AsCqt(row).IsNull();
				cqt = ((cqt != null) ? dbExpression.Or(cqt) : dbExpression);
			}, skipIsNotNull);
			return cqt;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000CF639 File Offset: 0x000CD839
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, true);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000CF95C File Offset: 0x000CDB5C
		private StringBuilder ToStringHelper(StringBuilder inputBuilder, string blockAlias, bool skipIsNotNull, bool userString)
		{
			StringBuilder builder = new StringBuilder();
			this.AsCql(delegate(NegatedConstant negated, IEnumerable<Constant> domainValues)
			{
				if (userString)
				{
					negated.AsUserString(builder, blockAlias, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
					return;
				}
				negated.AsEsql(builder, blockAlias, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
			}, delegate(Set<Constant> domainValues)
			{
				this.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				if (domainValues.Count != 1)
				{
					builder.Append(" IN {");
					bool flag = true;
					foreach (Constant constant in domainValues)
					{
						if (!flag)
						{
							builder.Append(", ");
						}
						if (userString)
						{
							constant.ToCompactString(builder);
						}
						else
						{
							constant.AsEsql(builder, this.RestrictedMemberSlot.MemberPath, blockAlias);
						}
						flag = false;
					}
					builder.Append('}');
					return;
				}
				builder.Append(" = ");
				if (userString)
				{
					domainValues.Single<Constant>().ToCompactString(builder);
					return;
				}
				domainValues.Single<Constant>().AsEsql(builder, this.RestrictedMemberSlot.MemberPath, blockAlias);
			}, delegate
			{
				bool flag = builder.Length == 0;
				builder.Insert(0, '(');
				if (!flag)
				{
					builder.Append(" AND ");
				}
				if (userString)
				{
					this.RestrictedMemberSlot.MemberPath.ToCompactString(builder, Strings.ViewGen_EntityInstanceToken);
					builder.Append(" is not NULL)");
					return;
				}
				this.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				builder.Append(" IS NOT NULL)");
			}, delegate
			{
				bool flag = builder.Length == 0;
				StringBuilder stringBuilder = new StringBuilder();
				if (!flag)
				{
					stringBuilder.Append('(');
				}
				if (userString)
				{
					this.RestrictedMemberSlot.MemberPath.ToCompactString(stringBuilder, blockAlias);
					stringBuilder.Append(" is NULL");
				}
				else
				{
					this.RestrictedMemberSlot.MemberPath.AsEsql(stringBuilder, blockAlias);
					stringBuilder.Append(" IS NULL");
				}
				if (!flag)
				{
					stringBuilder.Append(" OR ");
				}
				builder.Insert(0, stringBuilder.ToString());
				if (!flag)
				{
					builder.Append(')');
				}
			}, skipIsNotNull);
			inputBuilder.Append(builder);
			return inputBuilder;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000CF9EC File Offset: 0x000CDBEC
		private void AsCql(Action<NegatedConstant, IEnumerable<Constant>> negatedConstantAsCql, Action<Set<Constant>> varInDomain, Action varIsNotNull, Action varIsNull, bool skipIsNotNull)
		{
			NegatedConstant negatedConstant = (NegatedConstant)base.Domain.Values.FirstOrDefault((Constant c) => c is NegatedConstant);
			if (negatedConstant != null)
			{
				negatedConstantAsCql(negatedConstant, base.Domain.Values);
				return;
			}
			Set<Constant> set = new Set<Constant>(base.Domain.Values, Constant.EqualityComparer);
			bool flag = false;
			if (set.Contains(Constant.Null))
			{
				flag = true;
				set.Remove(Constant.Null);
			}
			if (set.Contains(Constant.Undefined))
			{
				flag = true;
				set.Remove(Constant.Undefined);
			}
			bool flag2 = !skipIsNotNull && base.RestrictedMemberSlot.MemberPath.IsNullable;
			if (set.Count > 0)
			{
				varInDomain(set);
			}
			if (flag2)
			{
				varIsNotNull();
			}
			if (flag)
			{
				varIsNull();
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000CFAC9 File Offset: 0x000CDCC9
		internal override void ToCompactString(StringBuilder builder)
		{
			base.RestrictedMemberSlot.ToCompactString(builder);
			builder.Append(" IN (");
			StringUtil.ToCommaSeparatedStringSorted(builder, base.Domain.Values);
			builder.Append(")");
		}
	}
}
