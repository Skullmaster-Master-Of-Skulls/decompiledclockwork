using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x000831A0 File Offset: 0x000825A0
		private OperationAbortedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000831C0 File Offset: 0x000825C0
		private OperationAbortedException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000831D8 File Offset: 0x000825D8
		internal static OperationAbortedException Aborted(Exception inner)
		{
			OperationAbortedException ex;
			if (inner == null)
			{
				ex = new OperationAbortedException(Res.GetString("ADP_OperationAborted"), null);
			}
			else
			{
				ex = new OperationAbortedException(Res.GetString("ADP_OperationAbortedExceptionMessage"), inner);
			}
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}
	}
}
