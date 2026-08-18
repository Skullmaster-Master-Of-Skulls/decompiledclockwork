using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x002121B8 File Offset: 0x002115B8
		private OperationAbortedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x002121D8 File Offset: 0x002115D8
		private OperationAbortedException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x002121F8 File Offset: 0x002115F8
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
