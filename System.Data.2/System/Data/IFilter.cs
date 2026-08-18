using System;

namespace System.Data
{
	// Token: 0x020000F7 RID: 247
	internal interface IFilter
	{
		// Token: 0x06000FF4 RID: 4084
		bool Invoke(DataRow row, DataRowVersion version);
	}
}
