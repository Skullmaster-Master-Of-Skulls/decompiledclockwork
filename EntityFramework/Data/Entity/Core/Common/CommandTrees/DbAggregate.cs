using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000DB RID: 219
	public abstract class DbAggregate
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x000250A8 File Offset: 0x000232A8
		internal DbAggregate(TypeUsage resultType, DbExpressionList arguments)
		{
			this._type = resultType;
			this._args = arguments;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000250BE File Offset: 0x000232BE
		public TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000250C6 File Offset: 0x000232C6
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x040001BA RID: 442
		private readonly DbExpressionList _args;

		// Token: 0x040001BB RID: 443
		private readonly TypeUsage _type;
	}
}
