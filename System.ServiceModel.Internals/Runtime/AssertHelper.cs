using System;
using System.Diagnostics;

namespace System.Runtime
{
	// Token: 0x02000005 RID: 5
	internal static class AssertHelper
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000255C File Offset: 0x0000075C
		internal static void FireAssert(string message)
		{
			try
			{
			}
			finally
			{
				Debug.Assert(false, message);
			}
		}
	}
}
