using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007BD RID: 1981
	[ComVisible(true)]
	[SoapType(Embedded = true)]
	[Serializable]
	public sealed class ServerFault
	{
		// Token: 0x06004699 RID: 18073 RVA: 0x000F0AAD File Offset: 0x000EFAAD
		internal ServerFault(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000F0ABC File Offset: 0x000EFABC
		public ServerFault(string exceptionType, string message, string stackTrace)
		{
			this.exceptionType = exceptionType;
			this.message = message;
			this.stackTrace = stackTrace;
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x000F0AD9 File Offset: 0x000EFAD9
		// (set) Token: 0x0600469C RID: 18076 RVA: 0x000F0AE1 File Offset: 0x000EFAE1
		public string ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
			set
			{
				this.exceptionType = value;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x000F0AEA File Offset: 0x000EFAEA
		// (set) Token: 0x0600469E RID: 18078 RVA: 0x000F0AF2 File Offset: 0x000EFAF2
		public string ExceptionMessage
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x000F0AFB File Offset: 0x000EFAFB
		// (set) Token: 0x060046A0 RID: 18080 RVA: 0x000F0B03 File Offset: 0x000EFB03
		public string StackTrace
		{
			get
			{
				return this.stackTrace;
			}
			set
			{
				this.stackTrace = value;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x000F0B0C File Offset: 0x000EFB0C
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04002319 RID: 8985
		private string exceptionType;

		// Token: 0x0400231A RID: 8986
		private string message;

		// Token: 0x0400231B RID: 8987
		private string stackTrace;

		// Token: 0x0400231C RID: 8988
		private Exception exception;
	}
}
