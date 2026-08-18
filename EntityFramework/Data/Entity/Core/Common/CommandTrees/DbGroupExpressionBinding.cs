using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200011B RID: 283
	public sealed class DbGroupExpressionBinding
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0002840E File Offset: 0x0002660E
		internal DbGroupExpressionBinding(DbExpression input, DbVariableReferenceExpression inputRef, DbVariableReferenceExpression groupRef)
		{
			this._expr = input;
			this._varRef = inputRef;
			this._groupVarRef = groupRef;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0002842B File Offset: 0x0002662B
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00028433 File Offset: 0x00026633
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00028440 File Offset: 0x00026640
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0002844D File Offset: 0x0002664D
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00028455 File Offset: 0x00026655
		public string GroupVariableName
		{
			get
			{
				return this._groupVarRef.VariableName;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00028462 File Offset: 0x00026662
		public TypeUsage GroupVariableType
		{
			get
			{
				return this._groupVarRef.ResultType;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0002846F File Offset: 0x0002666F
		public DbVariableReferenceExpression GroupVariable
		{
			get
			{
				return this._groupVarRef;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00028477 File Offset: 0x00026677
		public DbGroupAggregate GroupAggregate
		{
			get
			{
				if (this._groupAggregate == null)
				{
					this._groupAggregate = DbExpressionBuilder.GroupAggregate(this.GroupVariable);
				}
				return this._groupAggregate;
			}
		}

		// Token: 0x04000258 RID: 600
		private readonly DbExpression _expr;

		// Token: 0x04000259 RID: 601
		private readonly DbVariableReferenceExpression _varRef;

		// Token: 0x0400025A RID: 602
		private readonly DbVariableReferenceExpression _groupVarRef;

		// Token: 0x0400025B RID: 603
		private DbGroupAggregate _groupAggregate;
	}
}
