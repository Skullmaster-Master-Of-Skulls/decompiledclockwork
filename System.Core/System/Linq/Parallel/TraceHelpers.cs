using System;
using System.Diagnostics;

namespace System.Linq.Parallel
{
	// Token: 0x02000207 RID: 519
	internal static class TraceHelpers
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0003A071 File Offset: 0x00038271
		[Conditional("PFXTRACE")]
		internal static void SetVerbose()
		{
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003A073 File Offset: 0x00038273
		[Conditional("PFXTRACE")]
		internal static void TraceInfo(string msg, params object[] args)
		{
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003A075 File Offset: 0x00038275
		[Conditional("PFXTRACE")]
		internal static void TraceWarning(string msg, params object[] args)
		{
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003A077 File Offset: 0x00038277
		[Conditional("PFXTRACE")]
		internal static void TraceError(string msg, params object[] args)
		{
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003A079 File Offset: 0x00038279
		internal static void NotYetImplemented()
		{
			TraceHelpers.NotYetImplemented(false, "NYI");
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003A086 File Offset: 0x00038286
		internal static void NotYetImplemented(string message)
		{
			TraceHelpers.NotYetImplemented(false, "NYI: " + message);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003A099 File Offset: 0x00038299
		internal static void NotYetImplemented(bool assertCondition, string message)
		{
			if (!assertCondition)
			{
				throw new NotImplementedException();
			}
		}
	}
}
