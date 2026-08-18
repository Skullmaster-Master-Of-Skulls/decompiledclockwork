using System;
using OracleInternal.Common;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B8 RID: 440
	internal class OracleRefCursorImpl
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x000BD358 File Offset: 0x000BB558
		internal OracleRefCursorImpl(TTCResultSet resultSet)
		{
			this.m_sqlMetaData = resultSet.SqlMetaData;
			this.m_cursorId = resultSet.CursorId;
			this.m_accessors = resultSet.DefineAccessors;
		}

		// Token: 0x0400135C RID: 4956
		internal SQLMetaData m_sqlMetaData;

		// Token: 0x0400135D RID: 4957
		internal int m_cursorId;

		// Token: 0x0400135E RID: 4958
		internal Accessor[] m_accessors;
	}
}
