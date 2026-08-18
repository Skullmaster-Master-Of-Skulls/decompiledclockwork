using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x001ED8B8 File Offset: 0x001ECCB8
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x001ED8D8 File Offset: 0x001ECCD8
		public NoNullAllowedException() : base(Res.GetString("DataSet_DefaultNoNullAllowedException"))
		{
			base.HResult = -2146232026;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x001ED908 File Offset: 0x001ECD08
		public NoNullAllowedException(string s) : base(s)
		{
			base.HResult = -2146232026;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x001ED928 File Offset: 0x001ECD28
		public NoNullAllowedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232026;
		}
	}
}
