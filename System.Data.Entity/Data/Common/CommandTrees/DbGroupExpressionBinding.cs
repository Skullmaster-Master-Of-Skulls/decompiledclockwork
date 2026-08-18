using System;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F7 RID: 1015
	public sealed class DbGroupExpressionBinding
	{
		// Token: 0x06003658 RID: 13912 RVA: 0x000D0B0C File Offset: 0x000CED0C
		internal DbGroupExpressionBinding(DbExpression input, DbVariableReferenceExpression inputRef, DbVariableReferenceExpression groupRef)
		{
			this._expr = input;
			this._varRef = inputRef;
			this._groupVarRef = groupRef;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000D0B29 File Offset: 0x000CED29
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x000D0B31 File Offset: 0x000CED31
		public string VariableName
		{
			get
			{
				return this._varRef.VariableName;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000D0B3E File Offset: 0x000CED3E
		public TypeUsage VariableType
		{
			get
			{
				return this._varRef.ResultType;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000D0B4B File Offset: 0x000CED4B
		public DbVariableReferenceExpression Variable
		{
			get
			{
				return this._varRef;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000D0B53 File Offset: 0x000CED53
		public string GroupVariableName
		{
			get
			{
				return this._groupVarRef.VariableName;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000D0B60 File Offset: 0x000CED60
		public TypeUsage GroupVariableType
		{
			get
			{
				return this._groupVarRef.ResultType;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000D0B6D File Offset: 0x000CED6D
		public DbVariableReferenceExpression GroupVariable
		{
			get
			{
				return this._groupVarRef;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000D0B75 File Offset: 0x000CED75
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

		// Token: 0x040017FC RID: 6140
		private DbExpression _expr;

		// Token: 0x040017FD RID: 6141
		private readonly DbVariableReferenceExpression _varRef;

		// Token: 0x040017FE RID: 6142
		private readonly DbVariableReferenceExpression _groupVarRef;

		// Token: 0x040017FF RID: 6143
		private DbGroupAggregate _groupAggregate;
	}
}
