using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F2 RID: 1010
	public abstract class DbAggregate
	{
		// Token: 0x06003611 RID: 13841 RVA: 0x000D042B File Offset: 0x000CE62B
		internal DbAggregate(TypeUsage resultType, DbExpressionList arguments)
		{
			this._type = resultType;
			this._args = arguments;
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000D0441 File Offset: 0x000CE641
		public TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000D0449 File Offset: 0x000CE649
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x040017F6 RID: 6134
		private readonly DbExpressionList _args;

		// Token: 0x040017F7 RID: 6135
		private readonly TypeUsage _type;
	}
}
