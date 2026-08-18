using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000319 RID: 793
	internal enum SniContext
	{
		// Token: 0x04001B2B RID: 6955
		Undefined,
		// Token: 0x04001B2C RID: 6956
		Snix_Connect,
		// Token: 0x04001B2D RID: 6957
		Snix_PreLoginBeforeSuccessfullWrite,
		// Token: 0x04001B2E RID: 6958
		Snix_PreLogin,
		// Token: 0x04001B2F RID: 6959
		Snix_LoginSspi,
		// Token: 0x04001B30 RID: 6960
		Snix_ProcessSspi,
		// Token: 0x04001B31 RID: 6961
		Snix_Login,
		// Token: 0x04001B32 RID: 6962
		Snix_EnableMars,
		// Token: 0x04001B33 RID: 6963
		Snix_AutoEnlist,
		// Token: 0x04001B34 RID: 6964
		Snix_GetMarsSession,
		// Token: 0x04001B35 RID: 6965
		Snix_Execute,
		// Token: 0x04001B36 RID: 6966
		Snix_Read,
		// Token: 0x04001B37 RID: 6967
		Snix_Close,
		// Token: 0x04001B38 RID: 6968
		Snix_SendRows
	}
}
