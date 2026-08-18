using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0005C510 File Offset: 0x0005B910
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0005C528 File Offset: 0x0005B928
		public InvalidConstraintException() : base(Res.GetString("DataSet_DefaultInvalidConstraintException"))
		{
			base.HResult = -2146232028;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0005C550 File Offset: 0x0005B950
		public InvalidConstraintException(string s) : base(s)
		{
			base.HResult = -2146232028;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0005C570 File Offset: 0x0005B970
		public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232028;
		}
	}
}
