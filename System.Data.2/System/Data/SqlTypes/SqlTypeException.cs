using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000183 RID: 387
	[Serializable]
	public class SqlTypeException : SystemException
	{
		// Token: 0x0600177D RID: 6013 RVA: 0x000A85AC File Offset: 0x000A79AC
		public SqlTypeException() : this(Res.GetString("SqlMisc_SqlTypeMessage"), null)
		{
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000A85CC File Offset: 0x000A79CC
		public SqlTypeException(string message) : this(message, null)
		{
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000A85E4 File Offset: 0x000A79E4
		public SqlTypeException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232016;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000A8604 File Offset: 0x000A7A04
		protected SqlTypeException(SerializationInfo si, StreamingContext sc) : base(SqlTypeException.SqlTypeExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000A8620 File Offset: 0x000A7A20
		private static SerializationInfo SqlTypeExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				string @string = si.GetString("SqlTypeExceptionMessage");
				SqlTypeException ex = new SqlTypeException(@string);
				ex.GetObjectData(si, sc);
			}
			return si;
		}
	}
}
