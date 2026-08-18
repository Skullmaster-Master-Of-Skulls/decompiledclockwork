using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200018B RID: 395
	internal class ServiceHost
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000545AA File Offset: 0x000527AA
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x000545B2 File Offset: 0x000527B2
		public string CodeBehind { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000545BB File Offset: 0x000527BB
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x000545C3 File Offset: 0x000527C3
		public bool Debug { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000545CC File Offset: 0x000527CC
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x000545D4 File Offset: 0x000527D4
		public string Factory { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000545DD File Offset: 0x000527DD
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000545E5 File Offset: 0x000527E5
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000545EE File Offset: 0x000527EE
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000545F6 File Offset: 0x000527F6
		public string Service { get; set; }
	}
}
