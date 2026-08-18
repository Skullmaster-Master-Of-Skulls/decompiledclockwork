using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException
	{
		// Token: 0x0600178C RID: 6028 RVA: 0x000A87A8 File Offset: 0x000A7BA8
		public SqlNotFilledException() : this(SQLResource.NotFilledMessage, null)
		{
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000A87C4 File Offset: 0x000A7BC4
		public SqlNotFilledException(string message) : this(message, null)
		{
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000A87DC File Offset: 0x000A7BDC
		public SqlNotFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000A87FC File Offset: 0x000A7BFC
		private SqlNotFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
