using System;
using System.Security.Cryptography;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x0200000F RID: 15
	internal class DpapiDataProtector : IDataProtector
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000248C File Offset: 0x0000068C
		public DpapiDataProtector(string appName, string[] purposes)
		{
			this._protector = new DpapiDataProtector(appName, "Microsoft.Owin.Security.IDataProtector", purposes)
			{
				Scope = DataProtectionScope.CurrentUser
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024BA File Offset: 0x000006BA
		public byte[] Protect(byte[] userData)
		{
			return this._protector.Protect(userData);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024C8 File Offset: 0x000006C8
		public byte[] Unprotect(byte[] protectedData)
		{
			return this._protector.Unprotect(protectedData);
		}

		// Token: 0x0400000E RID: 14
		private readonly DpapiDataProtector _protector;
	}
}
