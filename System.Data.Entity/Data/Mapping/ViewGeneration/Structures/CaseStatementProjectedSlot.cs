using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029F RID: 671
	internal sealed class CaseStatementProjectedSlot : ProjectedSlot
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x0009AD5C File Offset: 0x00098F5C
		internal CaseStatementProjectedSlot(CaseStatement statement, IEnumerable<WithRelationship> withRelationships)
		{
			this.m_caseStatement = statement;
			this.m_withRelationships = withRelationships;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0009AD74 File Offset: 0x00098F74
		internal override ProjectedSlot DeepQualify(CqlBlock block)
		{
			CaseStatement statement = this.m_caseStatement.DeepQualify(block);
			return new CaseStatementProjectedSlot(statement, null);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0009AD95 File Offset: 0x00098F95
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			this.m_caseStatement.AsEsql(builder, this.m_withRelationships, blockAlias, indentLevel);
			return builder;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0009ADAE File Offset: 0x00098FAE
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return this.m_caseStatement.AsCqt(row, this.m_withRelationships);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0009ADC2 File Offset: 0x00098FC2
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_caseStatement.ToCompactString(builder);
		}

		// Token: 0x04001235 RID: 4661
		private readonly CaseStatement m_caseStatement;

		// Token: 0x04001236 RID: 4662
		private readonly IEnumerable<WithRelationship> m_withRelationships;
	}
}
