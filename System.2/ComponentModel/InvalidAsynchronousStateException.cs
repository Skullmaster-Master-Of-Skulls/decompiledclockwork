using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000572 RID: 1394
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		// Token: 0x060033DB RID: 13275 RVA: 0x000E4364 File Offset: 0x000E2564
		public InvalidAsynchronousStateException() : this(null)
		{
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000E436D File Offset: 0x000E256D
		public InvalidAsynchronousStateException(string message) : base(message)
		{
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000E4376 File Offset: 0x000E2576
		public InvalidAsynchronousStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000E4380 File Offset: 0x000E2580
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
