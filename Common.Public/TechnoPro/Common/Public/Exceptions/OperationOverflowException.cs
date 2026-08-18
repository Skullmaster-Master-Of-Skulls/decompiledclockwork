using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C6 RID: 198
	public class OperationOverflowException : Exception
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000D70E File Offset: 0x0000B90E
		public OperationOverflowException()
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000D718 File Offset: 0x0000B918
		public OperationOverflowException(string msg) : base(msg)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000D723 File Offset: 0x0000B923
		public OperationOverflowException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
