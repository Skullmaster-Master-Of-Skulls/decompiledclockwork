using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		// Token: 0x0600094C RID: 2380 RVA: 0x0005C610 File Offset: 0x0005BA10
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0005C628 File Offset: 0x0005BA28
		public NoNullAllowedException() : base(Res.GetString("DataSet_DefaultNoNullAllowedException"))
		{
			base.HResult = -2146232026;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0005C650 File Offset: 0x0005BA50
		public NoNullAllowedException(string s) : base(s)
		{
			base.HResult = -2146232026;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0005C670 File Offset: 0x0005BA70
		public NoNullAllowedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232026;
		}
	}
}
