using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A5B RID: 2651
	internal interface IRecord
	{
		// Token: 0x060066E1 RID: 26337
		byte[] GetData();

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x060066E2 RID: 26338
		ushort RecordType { get; }
	}
}
