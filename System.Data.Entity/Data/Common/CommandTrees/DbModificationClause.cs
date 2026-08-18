using System;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E9 RID: 1001
	public abstract class DbModificationClause
	{
		// Token: 0x060035C8 RID: 13768 RVA: 0x00002050 File Offset: 0x00000250
		internal DbModificationClause()
		{
		}

		// Token: 0x060035C9 RID: 13769
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x060035CA RID: 13770
		internal abstract TreeNode Print(DbExpressionVisitor<TreeNode> visitor);
	}
}
