using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00006BEC File Offset: 0x00005FEC
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006BD4 File Offset: 0x00005FD4
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006BC0 File Offset: 0x00005FC0
		public Exception(string message) : base(message)
		{
		}
	}
}
