using System;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x020005FD RID: 1533
	public class TspValidationException : TspException
	{
		// Token: 0x0600344C RID: 13388 RVA: 0x001450C3 File Offset: 0x001440C3
		public TspValidationException(string message) : base(message)
		{
			this.failureCode = -1;
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x001450D3 File Offset: 0x001440D3
		public TspValidationException(string message, int failureCode) : base(message)
		{
			this.failureCode = failureCode;
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x001450E3 File Offset: 0x001440E3
		public int FailureCode
		{
			get
			{
				return this.failureCode;
			}
		}

		// Token: 0x04002345 RID: 9029
		private int failureCode;
	}
}
