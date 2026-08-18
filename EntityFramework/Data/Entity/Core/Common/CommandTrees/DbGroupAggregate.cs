using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F0 RID: 240
	public sealed class DbGroupAggregate : DbAggregate
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x000258EA File Offset: 0x00023AEA
		internal DbGroupAggregate(TypeUsage resultType, DbExpressionList arguments) : base(resultType, arguments)
		{
		}
	}
}
