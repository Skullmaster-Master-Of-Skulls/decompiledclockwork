using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class DataException : SystemException
	{
		// Token: 0x06000930 RID: 2352 RVA: 0x0005C298 File Offset: 0x0005B698
		protected DataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0005C2B0 File Offset: 0x0005B6B0
		public DataException() : base(Res.GetString("DataSet_DefaultDataException"))
		{
			base.HResult = -2146232032;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0005C2D8 File Offset: 0x0005B6D8
		public DataException(string s) : base(s)
		{
			base.HResult = -2146232032;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0005C2F8 File Offset: 0x0005B6F8
		public DataException(string s, Exception innerException) : base(s, innerException)
		{
		}
	}
}
