using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class ReadOnlyException : DataException
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x001ED948 File Offset: 0x001ECD48
		protected ReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x001ED968 File Offset: 0x001ECD68
		public ReadOnlyException() : base(Res.GetString("DataSet_DefaultReadOnlyException"))
		{
			base.HResult = -2146232025;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x001ED998 File Offset: 0x001ECD98
		public ReadOnlyException(string s) : base(s)
		{
			base.HResult = -2146232025;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x001ED9B8 File Offset: 0x001ECDB8
		public ReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232025;
		}
	}
}
