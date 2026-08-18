using System;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000336 RID: 822
	internal abstract class ExpressionResolution
	{
		// Token: 0x060030FA RID: 12538 RVA: 0x000C17B5 File Offset: 0x000BF9B5
		protected ExpressionResolution(ExpressionResolutionClass @class)
		{
			this.ExpressionClass = @class;
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060030FB RID: 12539
		internal abstract string ExpressionClassName { get; }

		// Token: 0x04001549 RID: 5449
		internal readonly ExpressionResolutionClass ExpressionClass;
	}
}
