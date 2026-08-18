using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020D RID: 525
	internal class ActiveDirectoryAuthentication
	{
		// Token: 0x040013C0 RID: 5056
		internal const string AdoClientId = "4d079b4c-cab7-4b7c-a115-8fd51b6f8239";

		// Token: 0x040013C1 RID: 5057
		internal const string AdalGetAccessTokenFunctionName = "ADALGetAccessToken";

		// Token: 0x040013C2 RID: 5058
		internal const int GetAccessTokenSuccess = 0;

		// Token: 0x040013C3 RID: 5059
		internal const int GetAccessTokenInvalidGrant = 1;

		// Token: 0x040013C4 RID: 5060
		internal const int GetAccessTokenTansisentError = 2;

		// Token: 0x040013C5 RID: 5061
		internal const int GetAccessTokenOtherError = 3;
	}
}
