using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200002F RID: 47
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoConRefCtx
	{
		// Token: 0x04000150 RID: 336
		public string serverVersion;

		// Token: 0x04000151 RID: 337
		public string userID;

		// Token: 0x04000152 RID: 338
		public string password;

		// Token: 0x04000153 RID: 339
		public string dataSource;

		// Token: 0x04000154 RID: 340
		public string newPassword;

		// Token: 0x04000155 RID: 341
		public string proxyUserId;

		// Token: 0x04000156 RID: 342
		public string proxyPassword;

		// Token: 0x04000157 RID: 343
		public string dbName;

		// Token: 0x04000158 RID: 344
		public string dbDomainName;

		// Token: 0x04000159 RID: 345
		public string hostName;

		// Token: 0x0400015A RID: 346
		public string instanceName;

		// Token: 0x0400015B RID: 347
		public string serviceName;

		// Token: 0x0400015C RID: 348
		public string clientID;

		// Token: 0x0400015D RID: 349
		public string appEdition;

		// Token: 0x0400015E RID: 350
		public ITransaction pITransaction;

		// Token: 0x0400015F RID: 351
		public string moduleName;

		// Token: 0x04000160 RID: 352
		public string actionName;

		// Token: 0x04000161 RID: 353
		public string clientInfo;

		// Token: 0x04000162 RID: 354
		public string ttOpsConOpenErrMssg;
	}
}
