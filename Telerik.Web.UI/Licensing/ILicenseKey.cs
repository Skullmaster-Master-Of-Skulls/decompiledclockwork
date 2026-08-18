using System;

namespace Telerik.Licensing
{
	// Token: 0x02000413 RID: 1043
	internal interface ILicenseKey
	{
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060025D6 RID: 9686
		// (set) Token: 0x060025D7 RID: 9687
		string Key { get; set; }

		// Token: 0x060025D8 RID: 9688
		bool IsValid();
	}
}
