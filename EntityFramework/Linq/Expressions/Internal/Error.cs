using System;
using System.Data.Entity.Resources;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x02000553 RID: 1363
	internal static class Error
	{
		// Token: 0x060034E4 RID: 13540 RVA: 0x000F9DCD File Offset: 0x000F7FCD
		internal static Exception UnhandledExpressionType(ExpressionType expressionType)
		{
			return new NotSupportedException(Strings.ELinq_UnhandledExpressionType(expressionType));
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000F9DDF File Offset: 0x000F7FDF
		internal static Exception UnhandledBindingType(MemberBindingType memberBindingType)
		{
			return new NotSupportedException(Strings.ELinq_UnhandledBindingType(memberBindingType));
		}
	}
}
