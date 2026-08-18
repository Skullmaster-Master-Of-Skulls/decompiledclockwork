using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x020000F8 RID: 248
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidMessageContractException : SystemException
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x00018447 File Offset: 0x00016647
		[__DynamicallyInvokable]
		public InvalidMessageContractException()
		{
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001844F File Offset: 0x0001664F
		[__DynamicallyInvokable]
		public InvalidMessageContractException(string message) : base(message)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00018458 File Offset: 0x00016658
		[__DynamicallyInvokable]
		public InvalidMessageContractException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018462 File Offset: 0x00016662
		protected InvalidMessageContractException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
