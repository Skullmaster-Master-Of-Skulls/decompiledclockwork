using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B4 RID: 692
	internal class ScalarRestriction : MemberRestriction
	{
		// Token: 0x0600293F RID: 10559 RVA: 0x0009FE1C File Offset: 0x0009E01C
		internal ScalarRestriction(MemberPath member, Constant value) : base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0009FE2B File Offset: 0x0009E02B
		internal ScalarRestriction(MemberPath member, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues) : base(new MemberProjectedSlot(member), values, possibleValues)
		{
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0009FE3B File Offset: 0x0009E03B
		internal ScalarRestriction(MemberProjectedSlot slot, Domain domain) : base(slot, domain)
		{
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009FE48 File Offset: 0x0009E048
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			BoolLiteral boolLiteral = new ScalarRestriction(base.RestrictedMemberSlot, new Domain(range, domain));
			return boolLiteral.GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0009FE84 File Offset: 0x0009E084
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberProjectedSlot slot = base.RestrictedMemberSlot.RemapSlot(remap);
			return new ScalarRestriction(slot, base.Domain);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0009FEAA File Offset: 0x0009E0AA
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new ScalarRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0009FEC8 File Offset: 0x0009E0C8
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, false);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0009FED4 File Offset: 0x0009E0D4
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			DbExpression cqt = null;
			Func<Constant, DbExpression> <>9__4;
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
				Func<Constant, DbExpression> selector;
				if ((selector = <>9__4) == null)
				{
					selector = (<>9__4 = ((Constant c) => cqt.Equal(c.AsCqt(row, this.RestrictedMemberSlot.MemberPath))));
				}
				List<DbExpression> nodes = domainValues.Select(selector).ToList<DbExpression>();
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

		// Token: 0x06002947 RID: 10567 RVA: 0x0009FF45 File Offset: 0x0009E145
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, true);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0009FF54 File Offset: 0x0009E154
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
			inputBuilder.Append(builder.ToString());
			return inputBuilder;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009FFE0 File Offset: 0x0009E1E0
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

		// Token: 0x0600294A RID: 10570 RVA: 0x000A00BF File Offset: 0x0009E2BF
		internal override void ToCompactString(StringBuilder builder)
		{
			base.RestrictedMemberSlot.ToCompactString(builder);
			builder.Append(" IN (");
			StringUtil.ToCommaSeparatedStringSorted(builder, base.Domain.Values);
			builder.Append(")");
		}
	}
}
