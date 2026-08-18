using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x0200002F RID: 47
	public class DpapiDataProtectionProvider : IDataProtectionProvider
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004430 File Offset: 0x00002630
		public DpapiDataProtectionProvider() : this(Guid.NewGuid().ToString())
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004456 File Offset: 0x00002656
		public DpapiDataProtectionProvider(string appName)
		{
			if (appName == null)
			{
				throw new ArgumentNullException("appName");
			}
			this._appName = appName;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004473 File Offset: 0x00002673
		public IDataProtector Create(params string[] purposes)
		{
			if (purposes == null)
			{
				throw new ArgumentNullException("purposes");
			}
			return new DpapiDataProtector(this._appName, purposes);
		}

		// Token: 0x0400004A RID: 74
		private readonly string _appName;
	}
}
