using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000181 RID: 385
	internal class Msgx
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00053FDB File Offset: 0x000521DB
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x00053FE3 File Offset: 0x000521E3
		public string Class { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00053FEC File Offset: 0x000521EC
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x00053FF4 File Offset: 0x000521F4
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }
	}
}
