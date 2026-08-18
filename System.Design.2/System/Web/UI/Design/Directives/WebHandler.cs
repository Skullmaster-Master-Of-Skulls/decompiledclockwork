using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200018F RID: 399
	internal class WebHandler
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00054642 File Offset: 0x00052842
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0005464A File Offset: 0x0005284A
		public string Class { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00054653 File Offset: 0x00052853
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0005465B File Offset: 0x0005285B
		public string CodeBehind { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00054664 File Offset: 0x00052864
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0005466C File Offset: 0x0005286C
		public string CompilerOptions { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00054675 File Offset: 0x00052875
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0005467D File Offset: 0x0005287D
		[Filterable(false)]
		public bool Debug { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00054686 File Offset: 0x00052886
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0005468E File Offset: 0x0005288E
		[ReadOnly(true)]
		[Directive(ServerLanguageNames = true)]
		public string Language { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00054697 File Offset: 0x00052897
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0005469F File Offset: 0x0005289F
		[Filterable(false)]
		[TypeConverter(typeof(WarningLevelConverter))]
		public WarningLevel WarningLevel { get; set; }
	}
}
