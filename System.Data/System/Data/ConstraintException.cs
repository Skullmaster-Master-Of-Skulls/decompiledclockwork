using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class ConstraintException : DataException
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x001ED558 File Offset: 0x001EC958
		protected ConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x001ED578 File Offset: 0x001EC978
		public ConstraintException() : base(Res.GetString("DataSet_DefaultConstraintException"))
		{
			base.HResult = -2146232022;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x001ED5A8 File Offset: 0x001EC9A8
		public ConstraintException(string s) : base(s)
		{
			base.HResult = -2146232022;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x001ED5C8 File Offset: 0x001EC9C8
		public ConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232022;
		}
	}
}
