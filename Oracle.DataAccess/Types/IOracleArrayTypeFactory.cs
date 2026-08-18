using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000069 RID: 105
	public interface IOracleArrayTypeFactory
	{
		// Token: 0x060004EA RID: 1258
		Array CreateArray(int numElems);

		// Token: 0x060004EB RID: 1259
		Array CreateStatusArray(int numElems);
	}
}
