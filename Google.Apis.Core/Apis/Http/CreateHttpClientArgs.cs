using System;
using System.Collections.Generic;

namespace Google.Apis.Http
{
	// Token: 0x0200002D RID: 45
	public class CreateHttpClientArgs
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004289 File Offset: 0x00002489
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004291 File Offset: 0x00002491
		public bool GZipEnabled { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000429A File Offset: 0x0000249A
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000042A2 File Offset: 0x000024A2
		public string ApplicationName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000042AB File Offset: 0x000024AB
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000042B3 File Offset: 0x000024B3
		public IList<IConfigurableHttpClientInitializer> Initializers { get; private set; }

		// Token: 0x060000FA RID: 250 RVA: 0x000042BC File Offset: 0x000024BC
		public CreateHttpClientArgs()
		{
			this.Initializers = new List<IConfigurableHttpClientInitializer>();
		}
	}
}
