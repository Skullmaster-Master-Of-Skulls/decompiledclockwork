using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000192 RID: 402
	internal class WebService
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x000546D2 File Offset: 0x000528D2
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x000546DA File Offset: 0x000528DA
		public string Class { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000546E3 File Offset: 0x000528E3
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x000546EB File Offset: 0x000528EB
		public string CodeBehind { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000546F4 File Offset: 0x000528F4
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x000546FC File Offset: 0x000528FC
		public bool Debug { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00054705 File Offset: 0x00052905
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0005470D File Offset: 0x0005290D
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }
	}
}
