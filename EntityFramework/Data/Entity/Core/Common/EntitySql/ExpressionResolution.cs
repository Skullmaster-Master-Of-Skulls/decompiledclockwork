using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000249 RID: 585
	internal abstract class ExpressionResolution
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x000625B2 File Offset: 0x000607B2
		protected ExpressionResolution(ExpressionResolutionClass @class)
		{
			this.ExpressionClass = @class;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060014A5 RID: 5285
		internal abstract string ExpressionClassName { get; }

		// Token: 0x04000705 RID: 1797
		internal readonly ExpressionResolutionClass ExpressionClass;
	}
}
