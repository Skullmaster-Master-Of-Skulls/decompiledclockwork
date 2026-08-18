using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public class RowNotInTableException : DataException
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x001ED9D8 File Offset: 0x001ECDD8
		protected RowNotInTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x001ED9F8 File Offset: 0x001ECDF8
		public RowNotInTableException() : base(Res.GetString("DataSet_DefaultRowNotInTableException"))
		{
			base.HResult = -2146232024;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x001EDA28 File Offset: 0x001ECE28
		public RowNotInTableException(string s) : base(s)
		{
			base.HResult = -2146232024;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x001EDA48 File Offset: 0x001ECE48
		public RowNotInTableException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232024;
		}
	}
}
