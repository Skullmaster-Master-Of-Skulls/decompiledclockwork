using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	public class MissingPrimaryKeyException : DataException
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x001ED828 File Offset: 0x001ECC28
		protected MissingPrimaryKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x001ED848 File Offset: 0x001ECC48
		public MissingPrimaryKeyException() : base(Res.GetString("DataSet_DefaultMissingPrimaryKeyException"))
		{
			base.HResult = -2146232027;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x001ED878 File Offset: 0x001ECC78
		public MissingPrimaryKeyException(string s) : base(s)
		{
			base.HResult = -2146232027;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x001ED898 File Offset: 0x001ECC98
		public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232027;
		}
	}
}
