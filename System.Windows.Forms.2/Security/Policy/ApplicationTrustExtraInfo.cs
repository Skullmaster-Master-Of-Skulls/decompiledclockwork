using System;

namespace System.Security.Policy
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	internal class ApplicationTrustExtraInfo
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000DD45 File Offset: 0x0000BF45
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000DD4D File Offset: 0x0000BF4D
		public bool RequestsShellIntegration
		{
			get
			{
				return this.requestsShellIntegration;
			}
			set
			{
				this.requestsShellIntegration = value;
			}
		}

		// Token: 0x04000443 RID: 1091
		private bool requestsShellIntegration = true;
	}
}
