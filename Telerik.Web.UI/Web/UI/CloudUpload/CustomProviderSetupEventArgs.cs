using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x02000130 RID: 304
	public class CustomProviderSetupEventArgs : EventArgs
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002D7B2 File Offset: 0x0002B9B2
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0002D7BA File Offset: 0x0002B9BA
		public Type ProviderType { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002D7C3 File Offset: 0x0002B9C3
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0002D7CB File Offset: 0x0002B9CB
		public string Name { get; set; }
	}
}
