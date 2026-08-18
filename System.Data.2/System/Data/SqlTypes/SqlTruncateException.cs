using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x000A8700 File Offset: 0x000A7B00
		public SqlTruncateException() : this(SQLResource.TruncationMessage, null)
		{
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000A871C File Offset: 0x000A7B1C
		public SqlTruncateException(string message) : this(message, null)
		{
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000A8734 File Offset: 0x000A7B34
		public SqlTruncateException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000A8754 File Offset: 0x000A7B54
		private SqlTruncateException(SerializationInfo si, StreamingContext sc) : base(SqlTruncateException.SqlTruncateExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x000A8770 File Offset: 0x000A7B70
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
