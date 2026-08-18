using System;
using System.Security.Principal;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000123 RID: 291
	public class SelectingProviderEventArgs : EventArgs
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00036AAE File Offset: 0x00034CAE
		public IPrincipal User
		{
			get
			{
				return this._user;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00036AB6 File Offset: 0x00034CB6
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x00036ABE File Offset: 0x00034CBE
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00036AC7 File Offset: 0x00034CC7
		internal SelectingProviderEventArgs(IPrincipal user, string providerName)
		{
			this._user = user;
			this._providerName = providerName;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00035E5A File Offset: 0x0003405A
		private SelectingProviderEventArgs()
		{
		}

		// Token: 0x04000449 RID: 1097
		private IPrincipal _user;

		// Token: 0x0400044A RID: 1098
		private string _providerName;
	}
}
