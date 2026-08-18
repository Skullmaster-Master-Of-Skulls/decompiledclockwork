using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000208 RID: 520
	internal enum SniContext
	{
		// Token: 0x040013A0 RID: 5024
		Undefined,
		// Token: 0x040013A1 RID: 5025
		Snix_Connect,
		// Token: 0x040013A2 RID: 5026
		Snix_PreLoginBeforeSuccessfullWrite,
		// Token: 0x040013A3 RID: 5027
		Snix_PreLogin,
		// Token: 0x040013A4 RID: 5028
		Snix_LoginSspi,
		// Token: 0x040013A5 RID: 5029
		Snix_ProcessSspi,
		// Token: 0x040013A6 RID: 5030
		Snix_Login,
		// Token: 0x040013A7 RID: 5031
		Snix_EnableMars,
		// Token: 0x040013A8 RID: 5032
		Snix_AutoEnlist,
		// Token: 0x040013A9 RID: 5033
		Snix_GetMarsSession,
		// Token: 0x040013AA RID: 5034
		Snix_Execute,
		// Token: 0x040013AB RID: 5035
		Snix_Read,
		// Token: 0x040013AC RID: 5036
		Snix_Close,
		// Token: 0x040013AD RID: 5037
		Snix_SendRows
	}
}
