using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x02000412 RID: 1042
	internal interface ILicenseContextData
	{
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060025D0 RID: 9680
		// (set) Token: 0x060025D1 RID: 9681
		LicenseContext Context { get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060025D2 RID: 9682
		// (set) Token: 0x060025D3 RID: 9683
		Type Type { get; set; }

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060025D4 RID: 9684
		// (set) Token: 0x060025D5 RID: 9685
		bool AllowExceptions { get; set; }
	}
}
