using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200029A RID: 666
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstanceNotFoundException : InstrumentationException
	{
		// Token: 0x06001833 RID: 6195 RVA: 0x00057422 File Offset: 0x00055622
		public InstanceNotFoundException()
		{
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0005742A File Offset: 0x0005562A
		public InstanceNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00057433 File Offset: 0x00055633
		public InstanceNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0005743D File Offset: 0x0005563D
		protected InstanceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
