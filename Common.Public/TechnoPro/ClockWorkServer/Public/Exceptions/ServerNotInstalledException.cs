using System;

namespace TechnoPro.ClockWorkServer.Public.Exceptions
{
	// Token: 0x020000B8 RID: 184
	public class ServerNotInstalledException : Exception
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x0000D70E File Offset: 0x0000B90E
		public ServerNotInstalledException()
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000D718 File Offset: 0x0000B918
		public ServerNotInstalledException(string message) : base(message)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000D723 File Offset: 0x0000B923
		public ServerNotInstalledException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
