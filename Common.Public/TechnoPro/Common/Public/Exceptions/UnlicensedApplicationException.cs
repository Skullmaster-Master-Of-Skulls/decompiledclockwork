using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000CC RID: 204
	public class UnlicensedApplicationException : Exception
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000D70E File Offset: 0x0000B90E
		public UnlicensedApplicationException()
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000D718 File Offset: 0x0000B918
		public UnlicensedApplicationException(string msg) : base(msg)
		{
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000D723 File Offset: 0x0000B923
		public UnlicensedApplicationException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
