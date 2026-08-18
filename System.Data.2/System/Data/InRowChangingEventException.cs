using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x0005C490 File Offset: 0x0005B890
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0005C4A8 File Offset: 0x0005B8A8
		public InRowChangingEventException() : base(Res.GetString("DataSet_DefaultInRowChangingEventException"))
		{
			base.HResult = -2146232029;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0005C4D0 File Offset: 0x0005B8D0
		public InRowChangingEventException(string s) : base(s)
		{
			base.HResult = -2146232029;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0005C4F0 File Offset: 0x0005B8F0
		public InRowChangingEventException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232029;
		}
	}
}
