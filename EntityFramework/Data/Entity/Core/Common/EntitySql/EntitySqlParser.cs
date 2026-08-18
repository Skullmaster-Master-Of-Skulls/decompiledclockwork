using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200024B RID: 587
	public sealed class EntitySqlParser
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x000625DF File Offset: 0x000607DF
		internal EntitySqlParser(Perspective perspective)
		{
			this._perspective = perspective;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000625F0 File Offset: 0x000607F0
		public ParseResult Parse(string query, params DbParameterReferenceExpression[] parameters)
		{
			Check.NotNull<string>(query, "query");
			if (parameters != null)
			{
				IEnumerable<DbParameterReferenceExpression> enumerable = parameters;
				EntityUtil.CheckArgumentContainsNull<DbParameterReferenceExpression>(ref enumerable, "parameters");
			}
			return CqlQuery.Compile(query, this._perspective, null, parameters);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0006262C File Offset: 0x0006082C
		public DbLambda ParseLambda(string query, params DbVariableReferenceExpression[] variables)
		{
			Check.NotNull<string>(query, "query");
			if (variables != null)
			{
				IEnumerable<DbVariableReferenceExpression> enumerable = variables;
				EntityUtil.CheckArgumentContainsNull<DbVariableReferenceExpression>(ref enumerable, "variables");
			}
			return CqlQuery.CompileQueryCommandLambda(query, this._perspective, null, null, variables);
		}

		// Token: 0x04000707 RID: 1799
		private readonly Perspective _perspective;
	}
}
