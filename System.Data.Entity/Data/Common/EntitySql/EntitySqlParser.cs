using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200035A RID: 858
	public sealed class EntitySqlParser
	{
		// Token: 0x060031C9 RID: 12745 RVA: 0x000C3D9B File Offset: 0x000C1F9B
		internal EntitySqlParser(Perspective perspective)
		{
			this._perspective = perspective;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000C3DAC File Offset: 0x000C1FAC
		public ParseResult Parse(string query, params DbParameterReferenceExpression[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(query, "query");
			if (parameters != null)
			{
				IEnumerable<DbParameterReferenceExpression> enumerable = parameters;
				EntityUtil.CheckArgumentContainsNull<DbParameterReferenceExpression>(ref enumerable, "parameters");
			}
			return CqlQuery.Compile(query, this._perspective, null, parameters);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000C3DE8 File Offset: 0x000C1FE8
		public DbLambda ParseLambda(string query, params DbVariableReferenceExpression[] variables)
		{
			EntityUtil.CheckArgumentNull<string>(query, "query");
			if (variables != null)
			{
				IEnumerable<DbVariableReferenceExpression> enumerable = variables;
				EntityUtil.CheckArgumentContainsNull<DbVariableReferenceExpression>(ref enumerable, "variables");
			}
			return CqlQuery.CompileQueryCommandLambda(query, this._perspective, null, null, variables);
		}

		// Token: 0x040015A2 RID: 5538
		private readonly Perspective _perspective;
	}
}
