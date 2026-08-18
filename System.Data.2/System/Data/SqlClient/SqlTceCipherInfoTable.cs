using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000217 RID: 535
	internal struct SqlTceCipherInfoTable
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x000EC0B4 File Offset: 0x000EB4B4
		internal SqlTceCipherInfoTable(int tabSize)
		{
			this.keyList = new SqlTceCipherInfoEntry[tabSize];
		}

		// Token: 0x17000563 RID: 1379
		internal SqlTceCipherInfoEntry this[int index]
		{
			get
			{
				return this.keyList[index];
			}
			set
			{
				this.keyList[index] = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000EC108 File Offset: 0x000EB508
		internal int Size
		{
			get
			{
				return this.keyList.Length;
			}
		}

		// Token: 0x04001422 RID: 5154
		private readonly SqlTceCipherInfoEntry[] keyList;
	}
}
