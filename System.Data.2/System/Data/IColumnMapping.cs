using System;

namespace System.Data
{
	// Token: 0x020000FE RID: 254
	public interface IColumnMapping
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001057 RID: 4183
		// (set) Token: 0x06001058 RID: 4184
		string DataSetColumn { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001059 RID: 4185
		// (set) Token: 0x0600105A RID: 4186
		string SourceColumn { get; set; }
	}
}
