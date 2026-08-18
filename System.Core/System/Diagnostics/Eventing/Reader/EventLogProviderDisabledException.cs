using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C6 RID: 710
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogProviderDisabledException : EventLogException
	{
		// Token: 0x060019B4 RID: 6580 RVA: 0x0005D541 File Offset: 0x0005B741
		public EventLogProviderDisabledException()
		{
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0005D549 File Offset: 0x0005B749
		public EventLogProviderDisabledException(string message) : base(message)
		{
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0005D552 File Offset: 0x0005B752
		public EventLogProviderDisabledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0005D55C File Offset: 0x0005B75C
		protected EventLogProviderDisabledException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0005D566 File Offset: 0x0005B766
		internal EventLogProviderDisabledException(int errorCode) : base(errorCode)
		{
		}
	}
}
