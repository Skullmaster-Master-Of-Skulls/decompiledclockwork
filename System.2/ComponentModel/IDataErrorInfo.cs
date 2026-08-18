using System;

namespace System.ComponentModel
{
	// Token: 0x02000560 RID: 1376
	public interface IDataErrorInfo
	{
		// Token: 0x17000CA1 RID: 3233
		string this[string columnName]
		{
			get;
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060033A4 RID: 13220
		string Error { get; }
	}
}
