using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DA RID: 730
	[__DynamicallyInvokable]
	[Serializable]
	public class SecurityAccessDeniedException : CommunicationException
	{
		// Token: 0x060017DE RID: 6110 RVA: 0x0005AEB2 File Offset: 0x000590B2
		public SecurityAccessDeniedException()
		{
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0005AEBA File Offset: 0x000590BA
		[__DynamicallyInvokable]
		public SecurityAccessDeniedException(string message) : base(message)
		{
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0005AEC3 File Offset: 0x000590C3
		[__DynamicallyInvokable]
		public SecurityAccessDeniedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0005AECD File Offset: 0x000590CD
		protected SecurityAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
