using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E9 RID: 233
	public class AuthenticationPrompt
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000205EC File Offset: 0x0001E7EC
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x000205F4 File Offset: 0x0001E7F4
		public int Id { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000205FD File Offset: 0x0001E7FD
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00020605 File Offset: 0x0001E805
		public bool IsEchoed { get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002060E File Offset: 0x0001E80E
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00020616 File Offset: 0x0001E816
		public string Request { get; private set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002061F File Offset: 0x0001E81F
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00020627 File Offset: 0x0001E827
		public string Response { get; set; }

		// Token: 0x060009CA RID: 2506 RVA: 0x00020630 File Offset: 0x0001E830
		public AuthenticationPrompt(int id, bool isEchoed, string request)
		{
			this.Id = id;
			this.IsEchoed = isEchoed;
			this.Request = request;
		}
	}
}
