using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000373 RID: 883
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException
	{
		// Token: 0x06002F2C RID: 12076 RVA: 0x002D3A28 File Offset: 0x002D2E28
		public SqlNullValueException() : this(SQLResource.NullValueMessage, null)
		{
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x002D3A48 File Offset: 0x002D2E48
		public SqlNullValueException(string message) : this(message, null)
		{
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x002D3A68 File Offset: 0x002D2E68
		public SqlNullValueException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x002D3A88 File Offset: 0x002D2E88
		private SqlNullValueException(SerializationInfo si, StreamingContext sc) : base(SqlNullValueException.SqlNullValueExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x002D3AA8 File Offset: 0x002D2EA8
		private static SerializationInfo SqlNullValueExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				string @string = si.GetString("SqlNullValueExceptionMessage");
				SqlNullValueException ex = new SqlNullValueException(@string);
				ex.GetObjectData(si, sc);
			}
			return si;
		}
	}
}
