using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x000A8658 File Offset: 0x000A7A58
		public SqlNullValueException() : this(SQLResource.NullValueMessage, null)
		{
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000A8674 File Offset: 0x000A7A74
		public SqlNullValueException(string message) : this(message, null)
		{
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000A868C File Offset: 0x000A7A8C
		public SqlNullValueException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000A86AC File Offset: 0x000A7AAC
		private SqlNullValueException(SerializationInfo si, StreamingContext sc) : base(SqlNullValueException.SqlNullValueExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000A86C8 File Offset: 0x000A7AC8
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
