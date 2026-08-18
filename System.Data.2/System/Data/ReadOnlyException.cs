using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public class ReadOnlyException : DataException
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x0005C690 File Offset: 0x0005BA90
		protected ReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0005C6A8 File Offset: 0x0005BAA8
		public ReadOnlyException() : base(Res.GetString("DataSet_DefaultReadOnlyException"))
		{
			base.HResult = -2146232025;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0005C6D0 File Offset: 0x0005BAD0
		public ReadOnlyException(string s) : base(s)
		{
			base.HResult = -2146232025;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0005C6F0 File Offset: 0x0005BAF0
		public ReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232025;
		}
	}
}
