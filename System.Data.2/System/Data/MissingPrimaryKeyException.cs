using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class MissingPrimaryKeyException : DataException
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x0005C590 File Offset: 0x0005B990
		protected MissingPrimaryKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0005C5A8 File Offset: 0x0005B9A8
		public MissingPrimaryKeyException() : base(Res.GetString("DataSet_DefaultMissingPrimaryKeyException"))
		{
			base.HResult = -2146232027;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0005C5D0 File Offset: 0x0005B9D0
		public MissingPrimaryKeyException(string s) : base(s)
		{
			base.HResult = -2146232027;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0005C5F0 File Offset: 0x0005B9F0
		public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232027;
		}
	}
}
