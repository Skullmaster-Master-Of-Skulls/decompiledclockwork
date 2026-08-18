using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020003D5 RID: 981
	[__DynamicallyInvokable]
	[Serializable]
	public class BarrierPostPhaseException : Exception
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x000AF904 File Offset: 0x000ADB04
		[__DynamicallyInvokable]
		public BarrierPostPhaseException() : this(null)
		{
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000AF90D File Offset: 0x000ADB0D
		[__DynamicallyInvokable]
		public BarrierPostPhaseException(Exception innerException) : this(null, innerException)
		{
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000AF917 File Offset: 0x000ADB17
		[__DynamicallyInvokable]
		public BarrierPostPhaseException(string message) : this(message, null)
		{
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000AF921 File Offset: 0x000ADB21
		[__DynamicallyInvokable]
		public BarrierPostPhaseException(string message, Exception innerException) : base((message == null) ? SR.GetString("BarrierPostPhaseException") : message, innerException)
		{
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000AF93A File Offset: 0x000ADB3A
		[SecurityCritical]
		protected BarrierPostPhaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
