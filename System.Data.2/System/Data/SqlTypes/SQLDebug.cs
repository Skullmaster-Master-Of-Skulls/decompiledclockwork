using System;
using System.Diagnostics;

namespace System.Data.SqlTypes
{
	// Token: 0x02000188 RID: 392
	internal sealed class SQLDebug
	{
		// Token: 0x06001794 RID: 6036 RVA: 0x000A8880 File Offset: 0x000A7C80
		private SQLDebug()
		{
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000A8894 File Offset: 0x000A7C94
		[Conditional("DEBUG")]
		internal static void Check(bool condition)
		{
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000A88A4 File Offset: 0x000A7CA4
		[Conditional("DEBUG")]
		internal static void Check(bool condition, string conditionString, string message)
		{
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000A88B4 File Offset: 0x000A7CB4
		[Conditional("DEBUG")]
		internal static void Check(bool condition, string conditionString)
		{
		}
	}
}
