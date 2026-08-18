using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000214 RID: 532
	internal struct FederatedAuthenticationFeatureExtensionData
	{
		// Token: 0x0400140F RID: 5135
		internal TdsEnums.FedAuthLibrary libraryType;

		// Token: 0x04001410 RID: 5136
		internal bool fedAuthRequiredPreLoginResponse;

		// Token: 0x04001411 RID: 5137
		internal SqlAuthenticationMethod authentication;

		// Token: 0x04001412 RID: 5138
		internal byte[] accessToken;
	}
}
