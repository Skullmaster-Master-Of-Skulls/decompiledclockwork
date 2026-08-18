using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class RowNotInTableException : DataException
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x0005C710 File Offset: 0x0005BB10
		protected RowNotInTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0005C728 File Offset: 0x0005BB28
		public RowNotInTableException() : base(Res.GetString("DataSet_DefaultRowNotInTableException"))
		{
			base.HResult = -2146232024;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0005C750 File Offset: 0x0005BB50
		public RowNotInTableException(string s) : base(s)
		{
			base.HResult = -2146232024;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0005C770 File Offset: 0x0005BB70
		public RowNotInTableException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232024;
		}
	}
}
