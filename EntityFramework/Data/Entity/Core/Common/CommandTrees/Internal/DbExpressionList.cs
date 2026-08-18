using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000129 RID: 297
	internal sealed class DbExpressionList : ReadOnlyCollection<DbExpression>
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x00031E2E File Offset: 0x0003002E
		internal DbExpressionList(IList<DbExpression> elements) : base(elements)
		{
		}
	}
}
