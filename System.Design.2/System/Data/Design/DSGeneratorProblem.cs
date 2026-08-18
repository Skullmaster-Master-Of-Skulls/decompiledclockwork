using System;

namespace System.Data.Design
{
	// Token: 0x02000242 RID: 578
	internal sealed class DSGeneratorProblem
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0007BFFA File Offset: 0x0007A1FA
		internal string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0007C002 File Offset: 0x0007A202
		internal ProblemSeverity Severity
		{
			get
			{
				return this.severity;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0007C00A File Offset: 0x0007A20A
		internal DataSourceComponent ProblemSource
		{
			get
			{
				return this.problemSource;
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0007C012 File Offset: 0x0007A212
		internal DSGeneratorProblem(string message, ProblemSeverity severity, DataSourceComponent problemSource)
		{
			this.message = message;
			this.severity = severity;
			this.problemSource = problemSource;
		}

		// Token: 0x04000B8E RID: 2958
		private string message;

		// Token: 0x04000B8F RID: 2959
		private ProblemSeverity severity;

		// Token: 0x04000B90 RID: 2960
		private DataSourceComponent problemSource;
	}
}
