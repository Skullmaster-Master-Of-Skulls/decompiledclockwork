using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C5 RID: 709
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogReadingException : EventLogException
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x0005D513 File Offset: 0x0005B713
		public EventLogReadingException()
		{
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0005D51B File Offset: 0x0005B71B
		public EventLogReadingException(string message) : base(message)
		{
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0005D524 File Offset: 0x0005B724
		public EventLogReadingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005D52E File Offset: 0x0005B72E
		protected EventLogReadingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0005D538 File Offset: 0x0005B738
		internal EventLogReadingException(int errorCode) : base(errorCode)
		{
		}
	}
}
