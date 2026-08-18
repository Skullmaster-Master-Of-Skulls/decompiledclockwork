using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000436 RID: 1078
	internal sealed class DbExpressionList : ReadOnlyCollection<DbExpression>
	{
		// Token: 0x06003A01 RID: 14849 RVA: 0x000DD369 File Offset: 0x000DB569
		internal DbExpressionList(IList<DbExpression> elements) : base(elements)
		{
		}
	}
}
