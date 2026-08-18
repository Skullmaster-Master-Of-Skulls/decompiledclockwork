using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010A RID: 266
	public sealed class DbFunctionAggregate : DbAggregate
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00026185 File Offset: 0x00024385
		internal DbFunctionAggregate(TypeUsage resultType, DbExpressionList arguments, EdmFunction function, bool isDistinct) : base(resultType, arguments)
		{
			this._aggregateFunction = function;
			this._distinct = isDistinct;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0002619E File Offset: 0x0002439E
		public bool Distinct
		{
			get
			{
				return this._distinct;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x000261A6 File Offset: 0x000243A6
		public EdmFunction Function
		{
			get
			{
				return this._aggregateFunction;
			}
		}

		// Token: 0x04000200 RID: 512
		private readonly bool _distinct;

		// Token: 0x04000201 RID: 513
		private readonly EdmFunction _aggregateFunction;
	}
}
