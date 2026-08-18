using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000401 RID: 1025
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x000B1E94 File Offset: 0x000B0094
		public InternalBufferOverflowException()
		{
			base.HResult = -2146232059;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000B1EA7 File Offset: 0x000B00A7
		public InternalBufferOverflowException(string message) : base(message)
		{
			base.HResult = -2146232059;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000B1EBB File Offset: 0x000B00BB
		public InternalBufferOverflowException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232059;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000B1ED0 File Offset: 0x000B00D0
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
