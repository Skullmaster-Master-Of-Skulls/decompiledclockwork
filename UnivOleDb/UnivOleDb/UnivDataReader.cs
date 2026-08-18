using System;
using System.Collections;
using System.Data;

namespace UnivOleDb
{
	// Token: 0x02000010 RID: 16
	public interface UnivDataReader
	{
		// Token: 0x060000CF RID: 207
		bool Read();

		// Token: 0x17000028 RID: 40
		object this[int index]
		{
			get;
		}

		// Token: 0x17000029 RID: 41
		object this[string name]
		{
			get;
		}

		// Token: 0x060000D2 RID: 210
		void Close();

		// Token: 0x060000D3 RID: 211
		ArrayList ToItemArrays();

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D4 RID: 212
		int FieldCount { get; }

		// Token: 0x060000D5 RID: 213
		IDataReader GetNativeDataReader();
	}
}
