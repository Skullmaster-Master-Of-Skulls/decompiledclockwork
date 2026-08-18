using System;

namespace TechnoPro.Common.Public.Exceptions.RequestDenied
{
	// Token: 0x020000D1 RID: 209
	public class AbortedDueToRuleBreak : Exception
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000D70E File Offset: 0x0000B90E
		public AbortedDueToRuleBreak()
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000D718 File Offset: 0x0000B918
		public AbortedDueToRuleBreak(string message) : base(message)
		{
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000D723 File Offset: 0x0000B923
		public AbortedDueToRuleBreak(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
