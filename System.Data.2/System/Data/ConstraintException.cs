using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public class ConstraintException : DataException
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x0005C310 File Offset: 0x0005B710
		protected ConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0005C328 File Offset: 0x0005B728
		public ConstraintException() : base(Res.GetString("DataSet_DefaultConstraintException"))
		{
			base.HResult = -2146232022;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0005C350 File Offset: 0x0005B750
		public ConstraintException(string s) : base(s)
		{
			base.HResult = -2146232022;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0005C370 File Offset: 0x0005B770
		public ConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232022;
		}
	}
}
