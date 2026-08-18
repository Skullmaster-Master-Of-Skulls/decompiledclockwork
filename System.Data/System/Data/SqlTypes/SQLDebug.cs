using System;
using System.Diagnostics;

namespace System.Data.SqlTypes
{
	// Token: 0x02000377 RID: 887
	internal sealed class SQLDebug
	{
		// Token: 0x06002F3E RID: 12094 RVA: 0x002D3CA8 File Offset: 0x002D30A8
		private SQLDebug()
		{
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x002D3CC8 File Offset: 0x002D30C8
		[Conditional("DEBUG")]
		internal static void Check(bool condition)
		{
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x002D3CD8 File Offset: 0x002D30D8
		[Conditional("DEBUG")]
		internal static void Check(bool condition, string conditionString, string message)
		{
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x002D3CE8 File Offset: 0x002D30E8
		[Conditional("DEBUG")]
		internal static void Check(bool condition, string conditionString)
		{
		}
	}
}
