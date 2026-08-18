using System;

namespace TechnoPro.Common.Public.Exceptions
{
	// Token: 0x020000C9 RID: 201
	public class InvalidLicenseProductInfoException : Exception
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0000D70E File Offset: 0x0000B90E
		public InvalidLicenseProductInfoException()
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000D718 File Offset: 0x0000B918
		public InvalidLicenseProductInfoException(string msg) : base(msg)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000D723 File Offset: 0x0000B923
		public InvalidLicenseProductInfoException(string msg, Exception innerEx) : base(msg, innerEx)
		{
		}
	}
}
