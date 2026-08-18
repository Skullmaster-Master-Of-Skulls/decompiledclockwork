using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000372 RID: 882
	[Serializable]
	public class SqlTypeException : SystemException
	{
		// Token: 0x06002F27 RID: 12071 RVA: 0x002D3968 File Offset: 0x002D2D68
		public SqlTypeException() : this(Res.GetString("SqlMisc_SqlTypeMessage"), null)
		{
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x002D3988 File Offset: 0x002D2D88
		public SqlTypeException(string message) : this(message, null)
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x002D39A8 File Offset: 0x002D2DA8
		public SqlTypeException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232016;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x002D39C8 File Offset: 0x002D2DC8
		protected SqlTypeException(SerializationInfo si, StreamingContext sc) : base(SqlTypeException.SqlTypeExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x002D39E8 File Offset: 0x002D2DE8
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
