using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x002D3C28 File Offset: 0x002D3028
		public SqlAlreadyFilledException() : this(SQLResource.AlreadyFilledMessage, null)
		{
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x002D3C48 File Offset: 0x002D3048
		public SqlAlreadyFilledException(string message) : this(message, null)
		{
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x002D3C68 File Offset: 0x002D3068
		public SqlAlreadyFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x002D3C88 File Offset: 0x002D3088
		private SqlAlreadyFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
