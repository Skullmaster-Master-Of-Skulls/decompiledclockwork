using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000298 RID: 664
	internal sealed class WithRelationship : InternalBase
	{
		// Token: 0x06002785 RID: 10117 RVA: 0x00099B8C File Offset: 0x00097D8C
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

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x00099BDA File Offset: 0x00097DDA
		internal EntityType FromEndEntityType
		{
			get
			{
				return this.m_fromEndEntityType;
			}
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00099BE4 File Offset: 0x00097DE4
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

		// Token: 0x06002788 RID: 10120 RVA: 0x00099D2C File Offset: 0x00097F2C
		internal DbRelatedEntityRef AsCqt(DbExpression row)
		{
			return DbExpressionBuilder.CreateRelatedEntityRef(this.m_fromEnd, this.m_toEnd, this.m_toEndEntitySet.CreateRef(this.m_toEndEntityType, from keyMember in this.m_toEndEntityKeyMemberPaths
			select row.Property(keyMember.CqlFieldAlias)));
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ToCompactString(StringBuilder builder)
		{
		}

		// Token: 0x0400121F RID: 4639
		private readonly AssociationSet m_associationSet;

		// Token: 0x04001220 RID: 4640
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x04001221 RID: 4641
		private readonly EntityType m_fromEndEntityType;

		// Token: 0x04001222 RID: 4642
		private readonly RelationshipEndMember m_toEnd;

		// Token: 0x04001223 RID: 4643
		private readonly EntityType m_toEndEntityType;

		// Token: 0x04001224 RID: 4644
		private readonly EntitySet m_toEndEntitySet;

		// Token: 0x04001225 RID: 4645
		private readonly IEnumerable<MemberPath> m_toEndEntityKeyMemberPaths;
	}
}
