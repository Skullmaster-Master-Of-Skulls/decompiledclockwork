using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x001ED708 File Offset: 0x001ECB08
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x001ED728 File Offset: 0x001ECB28
		public InRowChangingEventException() : base(Res.GetString("DataSet_DefaultInRowChangingEventException"))
		{
			base.HResult = -2146232029;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x001ED758 File Offset: 0x001ECB58
		public InRowChangingEventException(string s) : base(s)
		{
			base.HResult = -2146232029;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x001ED778 File Offset: 0x001ECB78
		public InRowChangingEventException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232029;
		}
	}
}
