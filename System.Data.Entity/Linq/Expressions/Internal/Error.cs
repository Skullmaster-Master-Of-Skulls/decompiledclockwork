using System;
using System.Data;
using System.Data.Entity;

namespace System.Linq.Expressions.Internal
{
	// Token: 0x02000007 RID: 7
	internal static class Error
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002837 File Offset: 0x00000A37
		internal static Exception UnhandledExpressionType(ExpressionType expressionType)
		{
			return EntityUtil.NotSupported(Strings.ELinq_UnhandledExpressionType(expressionType));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002849 File Offset: 0x00000A49
		internal static Exception UnhandledBindingType(MemberBindingType memberBindingType)
		{
			return EntityUtil.NotSupported(Strings.ELinq_UnhandledBindingType(memberBindingType));
		}
	}
}
