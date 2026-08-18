using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F6 RID: 1014
	public sealed class DbExpressionBinding
	{
		// Token: 0x06003653 RID: 13907 RVA: 0x000D0ACC File Offset: 0x000CECCC
		internal DbExpressionBinding(DbExpression input, DbVariableReferenceExpression varRef)
		{
			this._expr = input;
			this._varRef = varRef;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x000D0AE2 File Offset: 0x000CECE2
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000D0AEA File Offset: 0x000CECEA
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x000D0AF7 File Offset: 0x000CECF7
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x000D0B04 File Offset: 0x000CED04
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x040017FA RID: 6138
		private readonly DbExpression _expr;

		// Token: 0x040017FB RID: 6139
		private readonly DbVariableReferenceExpression _varRef;
	}
}
