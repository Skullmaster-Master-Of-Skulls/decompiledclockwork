using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class DataException : SystemException
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x001ED4C8 File Offset: 0x001EC8C8
		protected DataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x001ED4E8 File Offset: 0x001EC8E8
		public DataException() : base(Res.GetString("DataSet_DefaultDataException"))
		{
			base.HResult = -2146232032;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x001ED518 File Offset: 0x001EC918
		public DataException(string s) : base(s)
		{
			base.HResult = -2146232032;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x001ED538 File Offset: 0x001EC938
		public DataException(string s, Exception innerException) : base(s, innerException)
		{
		}
	}
}
