using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C8 RID: 200
	public class InvalidLicenseKeyException : Exception
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D70E File Offset: 0x0000B90E
		public InvalidLicenseKeyException()
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000D718 File Offset: 0x0000B918
		public InvalidLicenseKeyException(string msg) : base(msg)
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000D723 File Offset: 0x0000B923
		public InvalidLicenseKeyException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
