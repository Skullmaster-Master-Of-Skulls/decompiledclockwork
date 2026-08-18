using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public class DuplicateNameException : DataException
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x001ED678 File Offset: 0x001ECA78
		protected DuplicateNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x001ED698 File Offset: 0x001ECA98
		public DuplicateNameException() : base(Res.GetString("DataSet_DefaultDuplicateNameException"))
		{
			base.HResult = -2146232030;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x001ED6C8 File Offset: 0x001ECAC8
		public DuplicateNameException(string s) : base(s)
		{
			base.HResult = -2146232030;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x001ED6E8 File Offset: 0x001ECAE8
		public DuplicateNameException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232030;
		}
	}
}
