using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class CallbackException : FatalException
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000035FE File Offset: 0x000017FE
		public CallbackException()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003606 File Offset: 0x00001806
		public CallbackException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003610 File Offset: 0x00001810
		protected CallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
