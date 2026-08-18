using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x000A8814 File Offset: 0x000A7C14
		public SqlAlreadyFilledException() : this(SQLResource.AlreadyFilledMessage, null)
		{
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000A8830 File Offset: 0x000A7C30
		public SqlAlreadyFilledException(string message) : this(message, null)
		{
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000A8848 File Offset: 0x000A7C48
		public SqlAlreadyFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000A8868 File Offset: 0x000A7C68
		private SqlAlreadyFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
