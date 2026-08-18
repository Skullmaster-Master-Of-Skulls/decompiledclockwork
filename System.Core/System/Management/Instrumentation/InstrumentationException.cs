using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000299 RID: 665
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstrumentationException : InstrumentationBaseException
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x000573F3 File Offset: 0x000555F3
		public InstrumentationException()
		{
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000573FB File Offset: 0x000555FB
		public InstrumentationException(string message) : base(message)
		{
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00057404 File Offset: 0x00055604
		public InstrumentationException(Exception innerException) : base(null, innerException)
		{
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0005740E File Offset: 0x0005560E
		public InstrumentationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00057418 File Offset: 0x00055618
		protected InstrumentationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
