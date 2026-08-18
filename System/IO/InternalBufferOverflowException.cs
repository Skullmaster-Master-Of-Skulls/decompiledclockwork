using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200072E RID: 1838
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		// Token: 0x0600383B RID: 14395 RVA: 0x000ED694 File Offset: 0x000EC694
		public InternalBufferOverflowException()
		{
			base.HResult = -2146232059;
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000ED6A7 File Offset: 0x000EC6A7
		public InternalBufferOverflowException(string message) : base(message)
		{
			base.HResult = -2146232059;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000ED6BB File Offset: 0x000EC6BB
		public InternalBufferOverflowException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146232059;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000ED6D0 File Offset: 0x000EC6D0
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
