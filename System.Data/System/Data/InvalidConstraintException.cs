using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x001ED798 File Offset: 0x001ECB98
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x001ED7B8 File Offset: 0x001ECBB8
		public InvalidConstraintException() : base(Res.GetString("DataSet_DefaultInvalidConstraintException"))
		{
			base.HResult = -2146232028;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x001ED7E8 File Offset: 0x001ECBE8
		public InvalidConstraintException(string s) : base(s)
		{
			base.HResult = -2146232028;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x001ED808 File Offset: 0x001ECC08
		public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232028;
		}
	}
}
