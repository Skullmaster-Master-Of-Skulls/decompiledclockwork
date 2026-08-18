using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002BA RID: 698
	internal sealed class TypeConstant : Constant
	{
		// Token: 0x0600298E RID: 10638 RVA: 0x000A14F6 File Offset: 0x0009F6F6
		internal TypeConstant(EdmType type)
		{
			this.m_edmType = type;
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000A1505 File Offset: 0x0009F705
		internal EdmType EdmType
		{
			get
			{
				return this.m_edmType;
			}
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000A1510 File Offset: 0x0009F710
		protected override bool IsEqualTo(Constant right)
		{
			TypeConstant typeConstant = right as TypeConstant;
			return typeConstant != null && this.m_edmType == typeConstant.m_edmType;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000A1537 File Offset: 0x0009F737
		public override int GetHashCode()
		{
			if (this.m_edmType == null)
			{
				return 0;
			}
			return this.m_edmType.GetHashCode();
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000A1550 File Offset: 0x0009F750
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			this.AsCql(delegate(EntitySet refScopeEntitySet, IList<MemberPath> keyMemberOutputPaths)
			{
				EntityType type = (EntityType)((RefType)outputMember.EdmType).ElementType;
				builder.Append("CreateRef(");
				CqlWriter.AppendEscapedQualifiedName(builder, refScopeEntitySet.EntityContainer.Name, refScopeEntitySet.Name);
				builder.Append(", row(");
				for (int i = 0; i < keyMemberOutputPaths.Count; i++)
				{
					if (i > 0)
					{
						builder.Append(", ");
					}
					string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, keyMemberOutputPaths[i].CqlFieldAlias);
					builder.Append(qualifiedName);
				}
				builder.Append("), ");
				CqlWriter.AppendEscapedTypeName(builder, type);
				builder.Append(')');
			}, delegate(IList<MemberPath> membersOutputPaths)
			{
				CqlWriter.AppendEscapedTypeName(builder, this.m_edmType);
				builder.Append('(');
				for (int i = 0; i < membersOutputPaths.Count; i++)
				{
					if (i > 0)
					{
						builder.Append(", ");
					}
					string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, membersOutputPaths[i].CqlFieldAlias);
					builder.Append(qualifiedName);
				}
				builder.Append(')');
			}, outputMember);
			return builder;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000A15AC File Offset: 0x0009F7AC
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			DbExpression cqt = null;
			Func<MemberPath, DbPropertyExpression> <>9__2;
			Func<MemberPath, DbPropertyExpression> <>9__3;
			this.AsCql(delegate(EntitySet refScopeEntitySet, IList<MemberPath> keyMemberOutputPaths)
			{
				EntityType entityType = (EntityType)((RefType)outputMember.EdmType).ElementType;
				EntityType entityType2 = entityType;
				Func<MemberPath, DbPropertyExpression> selector;
				if ((selector = <>9__2) == null)
				{
					selector = (<>9__2 = ((MemberPath km) => row.Property(km.CqlFieldAlias)));
				}
				cqt = refScopeEntitySet.CreateRef(entityType2, keyMemberOutputPaths.Select(selector));
			}, delegate(IList<MemberPath> membersOutputPaths)
			{
				TypeUsage instanceType = TypeUsage.Create(this.m_edmType);
				Func<MemberPath, DbPropertyExpression> selector;
				if ((selector = <>9__3) == null)
				{
					selector = (<>9__3 = ((MemberPath m) => row.Property(m.CqlFieldAlias)));
				}
				cqt = instanceType.New(membersOutputPaths.Select(selector));
			}, outputMember);
			return cqt;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000A1608 File Offset: 0x0009F808
		private void AsCql(Action<EntitySet, IList<MemberPath>> createRef, Action<IList<MemberPath>> createType, MemberPath outputMember)
		{
			EntitySet scopeOfRelationEnd = outputMember.GetScopeOfRelationEnd();
			if (scopeOfRelationEnd != null)
			{
				EntityType elementType = scopeOfRelationEnd.ElementType;
				List<MemberPath> arg = new List<MemberPath>(from km in elementType.KeyMembers
				select new MemberPath(outputMember, km));
				createRef(scopeOfRelationEnd, arg);
				return;
			}
			List<MemberPath> list = new List<MemberPath>();
			foreach (object obj in Helper.GetAllStructuralMembers(this.m_edmType))
			{
				EdmMember last = (EdmMember)obj;
				list.Add(new MemberPath(outputMember, last));
			}
			createType(list);
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000A16D4 File Offset: 0x0009F8D4
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000A16F4 File Offset: 0x0009F8F4
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_edmType.Name);
		}

		// Token: 0x04001285 RID: 4741
		private readonly EdmType m_edmType;
	}
}
