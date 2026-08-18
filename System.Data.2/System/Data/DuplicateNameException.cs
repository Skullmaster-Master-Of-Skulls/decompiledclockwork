using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class DuplicateNameException : DataException
	{
		// Token: 0x0600093C RID: 2364 RVA: 0x0005C410 File Offset: 0x0005B810
		protected DuplicateNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0005C428 File Offset: 0x0005B828
		public DuplicateNameException() : base(Res.GetString("DataSet_DefaultDuplicateNameException"))
		{
			base.HResult = -2146232030;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0005C450 File Offset: 0x0005B850
		public DuplicateNameException(string s) : base(s)
		{
			base.HResult = -2146232030;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0005C470 File Offset: 0x0005B870
		public DuplicateNameException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232030;
		}
	}
}
