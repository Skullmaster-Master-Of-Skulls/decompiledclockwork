using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000CB RID: 203
	public class OutdatedLicenseException : Exception
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0000D70E File Offset: 0x0000B90E
		public OutdatedLicenseException()
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000D718 File Offset: 0x0000B918
		public OutdatedLicenseException(string msg) : base(msg)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000D723 File Offset: 0x0000B923
		public OutdatedLicenseException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
