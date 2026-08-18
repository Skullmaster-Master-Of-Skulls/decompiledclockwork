using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x001EDA68 File Offset: 0x001ECE68
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x001EDA88 File Offset: 0x001ECE88
		public VersionNotFoundException() : base(Res.GetString("DataSet_DefaultVersionNotFoundException"))
		{
			base.HResult = -2146232023;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x001EDAB8 File Offset: 0x001ECEB8
		public VersionNotFoundException(string s) : base(s)
		{
			base.HResult = -2146232023;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x001EDAD8 File Offset: 0x001ECED8
		public VersionNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232023;
		}
	}
}
