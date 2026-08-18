using System;

namespace System.Web.Configuration
{
	// Token: 0x020006F6 RID: 1782
	public class HttpConfigurationContext
	{
		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x06005603 RID: 22019 RVA: 0x0012DC48 File Offset: 0x0012BE48
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x0012DC50 File Offset: 0x0012BE50
		internal HttpConfigurationContext(string vpath)
		{
			this.vpath = vpath;
		}

		// Token: 0x04002DAD RID: 11693
		private string vpath;
	}
}
