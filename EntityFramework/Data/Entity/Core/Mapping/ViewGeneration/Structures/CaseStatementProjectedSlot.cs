using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000464 RID: 1124
	internal sealed class CaseStatementProjectedSlot : ProjectedSlot
	{
		// Token: 0x0600293B RID: 10555 RVA: 0x000C7E44 File Offset: 0x000C6044
		internal CaseStatementProjectedSlot(CaseStatement statement, IEnumerable<WithRelationship> withRelationships)
		{
			this.m_caseStatement = statement;
			this.m_withRelationships = withRelationships;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000C7E5C File Offset: 0x000C605C
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			CaseStatement statement = this.m_caseStatement.DeepQualify(block);
			return new CaseStatementProjectedSlot(statement, null);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000C7E7D File Offset: 0x000C607D
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			this.m_caseStatement.AsEsql(builder, this.m_withRelationships, blockAlias, indentLevel);
			return builder;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000C7E96 File Offset: 0x000C6096
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_caseStatement.AsCqt(row, this.m_withRelationships);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000C7EAA File Offset: 0x000C60AA
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_caseStatement.ToCompactString(builder);
		}

		// Token: 0x04000F5C RID: 3932
		private readonly CaseStatement m_caseStatement;

		// Token: 0x04000F5D RID: 3933
		private readonly IEnumerable<WithRelationship> m_withRelationships;
	}
}
