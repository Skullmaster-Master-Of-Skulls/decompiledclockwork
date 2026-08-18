using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041C RID: 1052
	public sealed class DbFunctionExpression : DbExpression
	{
		// Token: 0x060036F8 RID: 14072 RVA: 0x000D1684 File Offset: 0x000CF884
		internal DbFunctionExpression(TypeUsage resultType, EdmFunction function, DbExpressionList arguments) : base(DbExpressionKind.Function, resultType)
		{
			this._functionInfo = function;
			this._arguments = arguments;
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000D169D File Offset: 0x000CF89D
		public EdmFunction Function
		{
			get
			{
				return this._functionInfo;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000D16A5 File Offset: 0x000CF8A5
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000D16AD File Offset: 0x000CF8AD
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000D16C4 File Offset: 0x000CF8C4
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400182A RID: 6186
		private readonly EdmFunction _functionInfo;

		// Token: 0x0400182B RID: 6187
		private readonly DbExpressionList _arguments;
	}
}
