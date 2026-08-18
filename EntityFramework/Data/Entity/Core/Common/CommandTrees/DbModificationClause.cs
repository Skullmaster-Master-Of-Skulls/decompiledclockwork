using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000116 RID: 278
	public abstract class DbModificationClause
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00028092 File Offset: 0x00026292
		internal DbModificationClause()
		{
		}

		// Token: 0x06000750 RID: 1872
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x06000751 RID: 1873
		internal abstract TreeNode Print(DbExpressionVisitor<TreeNode> visitor);
	}
}
