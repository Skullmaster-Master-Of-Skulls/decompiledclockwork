using System;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F3 RID: 1011
	public sealed class DbGroupAggregate : DbAggregate
	{
		// Token: 0x06003614 RID: 13844 RVA: 0x000D0451 File Offset: 0x000CE651
		internal DbGroupAggregate(TypeUsage resultType, DbExpressionList arguments) : base(resultType, arguments)
		{
		}
	}
}
