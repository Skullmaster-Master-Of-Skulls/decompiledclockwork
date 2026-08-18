using System;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F4 RID: 1012
	public sealed class DbFunctionAggregate : DbAggregate
	{
		// Token: 0x06003615 RID: 13845 RVA: 0x000D045B File Offset: 0x000CE65B
		internal DbFunctionAggregate(TypeUsage resultType, DbExpressionList arguments, EdmFunction function, bool isDistinct) : base(resultType, arguments)
		{
			this._aggregateFunction = function;
			this._distinct = isDistinct;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000D0474 File Offset: 0x000CE674
		public bool Distinct
		{
			get
			{
				return this._distinct;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000D047C File Offset: 0x000CE67C
		public EdmFunction Function
		{
			get
			{
				return this._aggregateFunction;
			}
		}

		// Token: 0x040017F8 RID: 6136
		private bool _distinct;

		// Token: 0x040017F9 RID: 6137
		private EdmFunction _aggregateFunction;
	}
}
