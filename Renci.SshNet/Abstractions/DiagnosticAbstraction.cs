using System;
using System.Diagnostics;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000115 RID: 277
	internal static class DiagnosticAbstraction
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0000262A File Offset: 0x0000082A
		[Conditional("DEBUG")]
		public static void Log(string text)
		{
		}

		// Token: 0x0400047F RID: 1151
		private static readonly TraceSource Loggging = new TraceSource("SshNet.Logging");
	}
}
