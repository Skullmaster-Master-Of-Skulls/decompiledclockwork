using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		// Token: 0x06002F31 RID: 12081 RVA: 0x002D3AE8 File Offset: 0x002D2EE8
		public SqlTruncateException() : this(SQLResource.TruncationMessage, null)
		{
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x002D3B08 File Offset: 0x002D2F08
		public SqlTruncateException(string message) : this(message, null)
		{
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x002D3B28 File Offset: 0x002D2F28
		public SqlTruncateException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x002D3B48 File Offset: 0x002D2F48
		private SqlTruncateException(SerializationInfo si, StreamingContext sc) : base(SqlTruncateException.SqlTruncateExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x002D3B68 File Offset: 0x002D2F68
		private static SerializationInfo SqlTruncateExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				string @string = si.GetString("SqlTruncateExceptionMessage");
				SqlTruncateException ex = new SqlTruncateException(@string);
				ex.GetObjectData(si, sc);
			}
			return si;
		}
	}
}
