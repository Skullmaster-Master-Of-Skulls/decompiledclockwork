using System;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x0200021B RID: 539
	internal sealed class SqlLogin
	{
		// Token: 0x0400143E RID: 5182
		internal SqlAuthenticationMethod authentication;

		// Token: 0x0400143F RID: 5183
		internal int timeout;

		// Token: 0x04001440 RID: 5184
		internal bool userInstance;

		// Token: 0x04001441 RID: 5185
		internal string hostName = "";

		// Token: 0x04001442 RID: 5186
		internal string userName = "";

		// Token: 0x04001443 RID: 5187
		internal string password = "";

		// Token: 0x04001444 RID: 5188
		internal string applicationName = "";

		// Token: 0x04001445 RID: 5189
		internal string serverName = "";

		// Token: 0x04001446 RID: 5190
		internal string language = "";

		// Token: 0x04001447 RID: 5191
		internal string database = "";

		// Token: 0x04001448 RID: 5192
		internal string attachDBFilename = "";

		// Token: 0x04001449 RID: 5193
		internal string newPassword = "";

		// Token: 0x0400144A RID: 5194
		internal bool useReplication;

		// Token: 0x0400144B RID: 5195
		internal bool useSSPI;

		// Token: 0x0400144C RID: 5196
		internal int packetSize = 8000;

		// Token: 0x0400144D RID: 5197
		internal bool readOnlyIntent;

		// Token: 0x0400144E RID: 5198
		internal SqlCredential credential;

		// Token: 0x0400144F RID: 5199
		internal SecureString newSecurePassword;
	}
}
