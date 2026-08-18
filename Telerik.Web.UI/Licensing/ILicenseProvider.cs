using System;

namespace Telerik.Licensing
{
	// Token: 0x02000415 RID: 1045
	internal interface ILicenseProvider
	{
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060025DC RID: 9692
		// (remove) Token: 0x060025DD RID: 9693
		event ProductUsedEventHandler ProductUsed;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060025DE RID: 9694
		// (remove) Token: 0x060025DF RID: 9695
		event ComponentUsedEventHandler ComponentUsed;
	}
}
