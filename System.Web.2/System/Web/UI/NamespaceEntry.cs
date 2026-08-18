using System;

namespace System.Web.UI
{
	// Token: 0x02000316 RID: 790
	internal class NamespaceEntry : SourceLineInfo
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x0007A4FF File Offset: 0x000786FF
		internal NamespaceEntry()
		{
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x0007A507 File Offset: 0x00078707
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x0007A50F File Offset: 0x0007870F
		internal string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x04001D5D RID: 7517
		private string _namespace;
	}
}
