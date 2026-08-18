using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000487 RID: 1159
	internal sealed class TypeConstant : Constant
	{
		// Token: 0x06002AF6 RID: 10998 RVA: 0x000CFB00 File Offset: 0x000CDD00
		internal TypeConstant(EdmType type)
		{
			this.m_edmType = type;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000CFB0F File Offset: 0x000CDD0F
		internal EdmType EdmType
		{
			get
			{
				return this.m_edmType;
			}
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000CFB17 File Offset: 0x000CDD17
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000CFB1A File Offset: 0x000CDD1A
		internal override bool IsNotNull()
		{
			return false;
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000CFB1D File Offset: 0x000CDD1D
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000CFB20 File Offset: 0x000CDD20
		internal override bool HasNotNull()
		{
			return false;
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000CFB24 File Offset: 0x000CDD24
		protected override bool IsEqualTo(Constant right)
		{
			TypeConstant typeConstant = right as TypeConstant;
			return typeConstant != null && this.m_edmType == typeConstant.m_edmType;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000CFB4B File Offset: 0x000CDD4B
		public override int GetHashCode()
		{
			if (this.m_edmType == null)
			{
				return 0;
			}
			return this.m_edmType.GetHashCode();
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000CFCD4 File Offset: 0x000CDED4
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

		// Token: 0x06002AFF RID: 11007 RVA: 0x000CFDD4 File Offset: 0x000CDFD4
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			DbExpression cqt = null;
			this.AsCql(delegate(EntitySet refScopeEntitySet, IList<MemberPath> keyMemberOutputPaths)
			{
				EntityType entityType = (EntityType)((RefType)outputMember.EdmType).ElementType;
				cqt = refScopeEntitySet.CreateRef(entityType, from km in keyMemberOutputPaths
				select row.Property(km.CqlFieldAlias));
			}, delegate(IList<MemberPath> membersOutputPaths)
			{
				cqt = TypeUsage.Create(this.m_edmType).New(from m in membersOutputPaths
				select row.Property(m.CqlFieldAlias));
			}, outputMember);
			return cqt;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000CFE44 File Offset: 0x000CE044
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

		// Token: 0x06002B01 RID: 11009 RVA: 0x000CFF1C File Offset: 0x000CE11C
		internal override string ToUserString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000CFF3C File Offset: 0x000CE13C
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_edmType.Name);
		}

		// Token: 0x04000FBD RID: 4029
		private readonly EdmType m_edmType;
	}
}
