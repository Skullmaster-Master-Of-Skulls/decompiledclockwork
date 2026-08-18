using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000488 RID: 1160
	internal class TypeRestriction : MemberRestriction
	{
		// Token: 0x06002B03 RID: 11011 RVA: 0x000CFF50 File Offset: 0x000CE150
		internal TypeRestriction(MemberPath member, IEnumerable<EdmType> values) : base(new MemberProjectedSlot(member), TypeRestriction.CreateTypeConstants(values))
		{
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000CFF64 File Offset: 0x000CE164
		internal TypeRestriction(MemberPath member, Constant value) : base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000CFF73 File Offset: 0x000CE173
		internal TypeRestriction(MemberProjectedSlot slot, Domain domain) : base(slot, domain)
		{
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000CFF80 File Offset: 0x000CE180
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			BoolLiteral boolLiteral = new TypeRestriction(base.RestrictedMemberSlot, new Domain(range, domain));
			return boolLiteral.GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000CFFBC File Offset: 0x000CE1BC
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberProjectedSlot slot = base.RestrictedMemberSlot.RemapSlot(remap);
			return new TypeRestriction(slot, base.Domain);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000CFFE2 File Offset: 0x000CE1E2
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new TypeRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000D0000 File Offset: 0x000CE200
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

		// Token: 0x06002B0A RID: 11018 RVA: 0x000D0158 File Offset: 0x000CE358
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

		// Token: 0x06002B0B RID: 11019 RVA: 0x000D0244 File Offset: 0x000CE444
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

		// Token: 0x06002B0C RID: 11020 RVA: 0x000D051C File Offset: 0x000CE71C
		private static IEnumerable<Constant> CreateTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType type in types)
			{
				if (type == null)
				{
					yield return Constant.Null;
				}
				else
				{
					yield return new TypeConstant(type);
				}
			}
			yield break;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000D053C File Offset: 0x000CE73C
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
