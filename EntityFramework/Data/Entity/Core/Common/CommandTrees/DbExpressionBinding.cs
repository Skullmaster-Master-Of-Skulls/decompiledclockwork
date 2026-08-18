using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000ED RID: 237
	public sealed class DbExpressionBinding
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x000257FA File Offset: 0x000239FA
		internal DbExpressionBinding(DbExpression input, DbVariableReferenceExpression varRef)
		{
			this._expr = input;
			this._varRef = varRef;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00025810 File Offset: 0x00023A10
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00025818 File Offset: 0x00023A18
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00025825 File Offset: 0x00023A25
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00025832 File Offset: 0x00023A32
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x040001D1 RID: 465
		private readonly DbExpression _expr;

		// Token: 0x040001D2 RID: 466
		private readonly DbVariableReferenceExpression _varRef;
	}
}
