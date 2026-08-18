using System;

namespace TechnoPro.ClockWorkServer.Client.Configuration.Exceptions
{
	// Token: 0x02000005 RID: 5
	public class ClientConfigInsufficientPermissionException : Exception
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002A8B File Offset: 0x00000C8B
		public ClientConfigInsufficientPermissionException()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A95 File Offset: 0x00000C95
		public ClientConfigInsufficientPermissionException(string message) : base(message)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public ClientConfigInsufficientPermissionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
