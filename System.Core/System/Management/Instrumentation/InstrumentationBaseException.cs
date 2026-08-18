using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000298 RID: 664
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstrumentationBaseException : Exception
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x000573CE File Offset: 0x000555CE
		public InstrumentationBaseException()
		{
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000573D6 File Offset: 0x000555D6
		public InstrumentationBaseException(string message) : base(message)
		{
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000573DF File Offset: 0x000555DF
		public InstrumentationBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000573E9 File Offset: 0x000555E9
		protected InstrumentationBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
