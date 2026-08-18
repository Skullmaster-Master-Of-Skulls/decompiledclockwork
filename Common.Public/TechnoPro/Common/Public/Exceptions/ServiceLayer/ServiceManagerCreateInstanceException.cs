using System;

namespace TechnoPro.Common.Public.Exceptions.ServiceLayer
{
	// Token: 0x020000CD RID: 205
	public class ServiceManagerCreateInstanceException : Exception
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000D70E File Offset: 0x0000B90E
		public ServiceManagerCreateInstanceException()
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000D718 File Offset: 0x0000B918
		public ServiceManagerCreateInstanceException(string msg) : base(msg)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000D723 File Offset: 0x0000B923
		public ServiceManagerCreateInstanceException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
