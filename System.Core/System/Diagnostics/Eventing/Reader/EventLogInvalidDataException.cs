using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C7 RID: 711
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogInvalidDataException : EventLogException
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x0005D56F File Offset: 0x0005B76F
		public EventLogInvalidDataException()
		{
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0005D577 File Offset: 0x0005B777
		public EventLogInvalidDataException(string message) : base(message)
		{
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0005D580 File Offset: 0x0005B780
		public EventLogInvalidDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0005D58A File Offset: 0x0005B78A
		protected EventLogInvalidDataException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0005D594 File Offset: 0x0005B794
		internal EventLogInvalidDataException(int errorCode) : base(errorCode)
		{
		}
	}
}
