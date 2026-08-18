using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200048A RID: 1162
	internal sealed class WithRelationship : InternalBase
	{
		// Token: 0x06002B0F RID: 11023 RVA: 0x000D058C File Offset: 0x000CE78C
		internal WithRelationship(AssociationSet associationSet, AssociationEndMember fromEnd, EntityType fromEndEntityType, AssociationEndMember toEnd, EntityType toEndEntityType, IEnumerable<MemberPath> toEndEntityKeyMemberPaths)
		{
			this.m_associationSet = associationSet;
			this.m_fromEnd = fromEnd;
			this.m_fromEndEntityType = fromEndEntityType;
			this.m_toEnd = toEnd;
			this.m_toEndEntityType = toEndEntityType;
			this.m_toEndEntitySet = MetadataHelper.GetEntitySetAtEnd(associationSet, toEnd);
			this.m_toEndEntityKeyMemberPaths = toEndEntityKeyMemberPaths;
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000D05DA File Offset: 0x000CE7DA
		internal EntityType FromEndEntityType
		{
			get
			{
				return this.m_fromEndEntityType;
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000D05E4 File Offset: 0x000CE7E4
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, int indentLevel)
		{
			StringUtil.IndentNewLine(builder, indentLevel + 1);
			builder.Append("RELATIONSHIP(");
			List<string> list = new List<string>();
			builder.Append("CREATEREF(");
			CqlWriter.AppendEscapedQualifiedName(builder, this.m_toEndEntitySet.EntityContainer.Name, this.m_toEndEntitySet.Name);
			builder.Append(", ROW(");
			foreach (MemberPath memberPath in this.m_toEndEntityKeyMemberPaths)
			{
				string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, memberPath.CqlFieldAlias);
				list.Add(qualifiedName);
			}
			StringUtil.ToSeparatedString(builder, list, ", ", null);
			builder.Append(')');
			builder.Append(",");
			CqlWriter.AppendEscapedTypeName(builder, this.m_toEndEntityType);
			builder.Append(')');
			builder.Append(',');
			CqlWriter.AppendEscapedTypeName(builder, this.m_associationSet.ElementType);
			builder.Append(',');
			CqlWriter.AppendEscapedName(builder, this.m_fromEnd.Name);
			builder.Append(',');
			CqlWriter.AppendEscapedName(builder, this.m_toEnd.Name);
			builder.Append(')');
			builder.Append(' ');
			return builder;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000D0748 File Offset: 0x000CE948
		internal DbRelatedEntityRef AsCqt(DbExpression row)
		{
			return DbExpressionBuilder.CreateRelatedEntityRef(this.m_fromEnd, this.m_toEnd, this.m_toEndEntitySet.CreateRef(this.m_toEndEntityType, from keyMember in this.m_toEndEntityKeyMemberPaths
			select row.Property(keyMember.CqlFieldAlias)));
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000D079B File Offset: 0x000CE99B
		internal override void ToCompactString(StringBuilder builder)
		{
		}

		// Token: 0x04000FC2 RID: 4034
		private readonly AssociationSet m_associationSet;

		// Token: 0x04000FC3 RID: 4035
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x04000FC4 RID: 4036
		private readonly EntityType m_fromEndEntityType;

		// Token: 0x04000FC5 RID: 4037
		private readonly RelationshipEndMember m_toEnd;

		// Token: 0x04000FC6 RID: 4038
		private readonly EntityType m_toEndEntityType;

		// Token: 0x04000FC7 RID: 4039
		private readonly EntitySet m_toEndEntitySet;

		// Token: 0x04000FC8 RID: 4040
		private readonly IEnumerable<MemberPath> m_toEndEntityKeyMemberPaths;
	}
}
