using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E6 RID: 230
	public class AuthenticationBannerEventArgs : AuthenticationEventArgs
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00020579 File Offset: 0x0001E779
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00020581 File Offset: 0x0001E781
		public string BannerMessage { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002058A File Offset: 0x0001E78A
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00020592 File Offset: 0x0001E792
		public string Language { get; private set; }

		// Token: 0x060009BB RID: 2491 RVA: 0x0002059B File Offset: 0x0001E79B
		public AuthenticationBannerEventArgs(string username, string message, string language) : base(username)
		{
			this.BannerMessage = message;
			this.Language = language;
		}
	}
}
