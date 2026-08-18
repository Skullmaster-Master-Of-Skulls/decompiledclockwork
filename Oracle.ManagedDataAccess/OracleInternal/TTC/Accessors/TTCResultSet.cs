using System;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x0200020D RID: 525
	internal class TTCResultSet
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000CEA94 File Offset: 0x000CCC94
		internal SQLMetaData SqlMetaData
		{
			get
			{
				return this.m_sqlMetaData;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x000CEA9C File Offset: 0x000CCC9C
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x000CEAA4 File Offset: 0x000CCCA4
		internal int CursorId
		{
			get
			{
				return this.m_cursorId;
			}
			set
			{
				this.m_cursorId = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000CEAB0 File Offset: 0x000CCCB0
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x000CEAB8 File Offset: 0x000CCCB8
		internal Accessor[] DefineAccessors
		{
			get
			{
				return this.m_defineAccessor;
			}
			set
			{
				this.m_defineAccessor = value;
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000CEAC4 File Offset: 0x000CCCC4
		internal TTCResultSet()
		{
			this.m_sqlMetaData = new SQLMetaData();
		}

		// Token: 0x0400149E RID: 5278
		private SQLMetaData m_sqlMetaData;

		// Token: 0x0400149F RID: 5279
		private int m_cursorId;

		// Token: 0x040014A0 RID: 5280
		private Accessor[] m_defineAccessor;
	}
}
