using System;

namespace TechnoPro.ClockWorkServer.Client.Messaging.Core
{
	// Token: 0x02000006 RID: 6
	public class PublicMessageFailedException : Exception
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000026F4 File Offset: 0x000008F4
		public PublicMessageFailedException()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026FC File Offset: 0x000008FC
		public PublicMessageFailedException(string message) : base(message)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002705 File Offset: 0x00000905
		public PublicMessageFailedException(string message, Exception innerEx) : base(message, innerEx)
		{
		}
	}
}
