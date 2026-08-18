using System;

namespace TechnoPro.ClockWorkServer.Client.Services.Exceptions
{
	// Token: 0x02000175 RID: 373
	public class ConnectionFailedException : Exception
	{
		// Token: 0x06000E77 RID: 3703 RVA: 0x00025948 File Offset: 0x00023B48
		public ConnectionFailedException()
		{
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00025952 File Offset: 0x00023B52
		public ConnectionFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002595D File Offset: 0x00023B5D
		public ConnectionFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
