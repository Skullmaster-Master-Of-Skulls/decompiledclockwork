using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C4 RID: 708
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogNotFoundException : EventLogException
	{
		// Token: 0x060019AA RID: 6570 RVA: 0x0005D4E5 File Offset: 0x0005B6E5
		public EventLogNotFoundException()
		{
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0005D4ED File Offset: 0x0005B6ED
		public EventLogNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0005D4F6 File Offset: 0x0005B6F6
		public EventLogNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0005D500 File Offset: 0x0005B700
		protected EventLogNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0005D50A File Offset: 0x0005B70A
		internal EventLogNotFoundException(int errorCode) : base(errorCode)
		{
		}
	}
}
