using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002B5 RID: 693
	internal class TypeRestriction : MemberRestriction
	{
		// Token: 0x0600294B RID: 10571 RVA: 0x000A00F6 File Offset: 0x0009E2F6
		internal TypeRestriction(MemberPath member, IEnumerable<EdmType> values) : base(new MemberProjectedSlot(member), TypeRestriction.CreateTypeConstants(values))
		{
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0009FE1C File Offset: 0x0009E01C
		internal TypeRestriction(MemberPath member, Constant value) : base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0009FE3B File Offset: 0x0009E03B
		internal TypeRestriction(MemberProjectedSlot slot, Domain domain) : base(slot, domain)
		{
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000A010C File Offset: 0x0009E30C
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			BoolLiteral boolLiteral = new TypeRestriction(base.RestrictedMemberSlot, new Domain(range, domain));
			return boolLiteral.GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000A0148 File Offset: 0x0009E348
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberProjectedSlot slot = base.RestrictedMemberSlot.RemapSlot(remap);
			return new TypeRestriction(slot, base.Domain);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000A016E File Offset: 0x0009E36E
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new TypeRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000A018C File Offset: 0x0009E38C
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			if (base.Domain.Count > 1)
			{
				builder.Append('(');
			}
			bool flag = true;
			foreach (Constant constant in base.Domain.Values)
			{
				TypeConstant typeConstant = constant as TypeConstant;
				if (!flag)
				{
					builder.Append(" OR ");
				}
				flag = false;
				if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
				{
					builder.Append("Deref(");
					base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
					builder.Append(')');
				}
				else
				{
					base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				}
				if (constant.IsNull())
				{
					builder.Append(" IS NULL");
				}
				else
				{
					builder.Append(" IS OF (ONLY ");
					CqlWriter.AppendEscapedTypeName(builder, typeConstant.EdmType);
					builder.Append(')');
				}
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000A02B4 File Offset: 0x0009E4B4
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			DbExpression cqt = base.RestrictedMemberSlot.MemberPath.AsCqt(row);
			if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
			{
				cqt = cqt.Deref();
			}
			if (base.Domain.Count == 1)
			{
				cqt = cqt.IsOfOnly(TypeUsage.Create(((TypeConstant)base.Domain.Values.Single<Constant>()).EdmType));
			}
			else
			{
				List<DbExpression> nodes = (from t in base.Domain.Values
				select cqt.IsOfOnly(TypeUsage.Create(((TypeConstant)t).EdmType))).ToList<DbExpression>();
				cqt = Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression prev, DbExpression next) => prev.Or(next));
			}
			return cqt;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000A039C File Offset: 0x0009E59C
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
			{
				builder.Append("Deref(");
				base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				builder.Append(')');
			}
			else
			{
				base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(" is a (");
			}
			else
			{
				builder.Append(" is type ");
			}
			bool flag = true;
			foreach (Constant constant in base.Domain.Values)
			{
				TypeConstant typeConstant = constant as TypeConstant;
				if (!flag)
				{
					builder.Append(" OR ");
				}
				if (constant.IsNull())
				{
					builder.Append(" NULL");
				}
				else
				{
					CqlWriter.AppendEscapedTypeName(builder, typeConstant.EdmType);
				}
				flag = false;
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000A04B8 File Offset: 0x0009E6B8
		private static IEnumerable<Constant> CreateTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType edmType in types)
			{
				if (edmType == null)
				{
					yield return Constant.Null;
				}
				else
				{
					yield return new TypeConstant(edmType);
				}
			}
			IEnumerator<EdmType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000A04C8 File Offset: 0x0009E6C8
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("type(");
			base.RestrictedMemberSlot.ToCompactString(builder);
			builder.Append(") IN (");
			StringUtil.ToCommaSeparatedStringSorted(builder, base.Domain.Values);
			builder.Append(")");
		}
	}
}
